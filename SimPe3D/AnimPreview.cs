/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSweatshop                                 *
 *   rhiamom@mac.com                                                       *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 *                                                                         *
 *   This program is distributed in the hope that it will be useful,       *
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of        *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
 *   GNU General Public License for more details.                          *
 *                                                                         *
 *   You should have received a copy of the GNU General Public License     *
 *   along with this program; if not, write to the                         *
 *   Free Software Foundation, Inc.,                                       *
 *   59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.             *
 ***************************************************************************/

using System;
using System.Collections;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for AnimPreview.
	/// </summary>
	public class AnimPreview : Avalonia.Controls.Window
	{
		private Avalonia.Controls.Panel xpGradientPanel1;
		private Avalonia.Controls.Panel panel1;
		private ThemeManager tm;
		private Avalonia.Controls.ListBox lb;
		private Avalonia.Controls.TreeView tv;
		private Avalonia.Controls.Button btPlay;
		private Avalonia.Threading.DispatcherTimer timer1;
		private Avalonia.Controls.ProgressBar pb;

		AnimPreview()
		{
			BuildLayout();
			ThemeManager.Global.AddControl(this.xpGradientPanel1);
		}

		private void BuildLayout()
		{
			lb = new Avalonia.Controls.ListBox { Height = 80 };
			lb.SelectionChanged += (s, e) => lb_SelectedIndexChanged(s, EventArgs.Empty);

			tv = new Avalonia.Controls.TreeView();

			btPlay = new Avalonia.Controls.Button { Content = "Play", HorizontalAlignment = HorizontalAlignment.Right, Margin = new Avalonia.Thickness(2) };
			btPlay.Click += (s, e) => btPlay_Click(s, EventArgs.Empty);

			pb = new Avalonia.Controls.ProgressBar { Minimum = 0, Maximum = 100, Value = 0, Height = 16 };

			timer1 = new Avalonia.Threading.DispatcherTimer();
			timer1.Tick += (s, e) => timer1_Tick(s, EventArgs.Empty);

			// Right panel layout: lb at top, tv fills middle, btPlay and pb at bottom
			var rightDock = new Avalonia.Controls.DockPanel { LastChildFill = true };
			Avalonia.Controls.DockPanel.SetDock(lb, Avalonia.Controls.Dock.Top);
			Avalonia.Controls.DockPanel.SetDock(pb, Avalonia.Controls.Dock.Bottom);
			Avalonia.Controls.DockPanel.SetDock(btPlay, Avalonia.Controls.Dock.Bottom);
			rightDock.Children.Add(lb);
			rightDock.Children.Add(pb);
			rightDock.Children.Add(btPlay);
			rightDock.Children.Add(tv);

			xpGradientPanel1 = new Avalonia.Controls.Panel { Width = 280 };
			xpGradientPanel1.Children.Add(rightDock);

			panel1 = new Avalonia.Controls.Panel();

			var root = new Avalonia.Controls.DockPanel { LastChildFill = true };
			Avalonia.Controls.DockPanel.SetDock(xpGradientPanel1, Avalonia.Controls.Dock.Right);
			root.Children.Add(xpGradientPanel1);
			root.Children.Add(panel1);

			Title = "Animation Preview";
			Width = 776;
			Height = 454;
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			Content = root;
		}

		public static void Execute(SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem animitem)
		{
			SimPe.Plugin.GenericRcol rcol = new GenericRcol();
			rcol.ProcessData(animitem);
			Execute(rcol);
		}

		public static void Execute(SimPe.Plugin.Rcol anim)
		{
			if (anim.Blocks.Length > 0)
				if (anim.Blocks[0] is SimPe.Plugin.Anim.AnimResourceConst)
					Execute((SimPe.Plugin.Anim.AnimResourceConst)anim.Blocks[0]);
		}

		public static void Execute(SimPe.Plugin.Anim.AnimResourceConst anim)
		{
			AnimPreview f = new AnimPreview();

			WaitingScreen.Wait();
			Wait.SubStart(anim.MeshBlock.Length);
			try
			{
				WaitingScreen.UpdateMessage(SimPe.Localization.GetString("Loading Meshes"));
				int ct = 0;
				foreach (SimPe.Plugin.Anim.AnimationMeshBlock amb in anim.MeshBlock)
				{
					f.lb.Items.Add(new ListedMeshBlocks(amb));
					Wait.Progress = ct++;
				}
			}
			finally
			{
				Wait.SubStop();
				WaitingScreen.Stop();
			}

			f.ShowDialog(null).GetAwaiter().GetResult();

			f.timer1.Stop();
		}

		SimPe.Plugin.Gmdc.ElementOrder eo = new SimPe.Plugin.Gmdc.ElementOrder(SimPe.Plugin.Gmdc.ElementSorting.XYZ);

		void AddJoint(ListedMeshBlocks lmb, SimPe.Interfaces.Scenegraph.ICresChildren bl, Ambertation.Graphics.MeshList parent, System.Collections.IList nodes)
		{
			SimPe.Plugin.TransformNode tn = bl.StoredTransformNode;

			if (tn != null)
			{
				Ambertation.Scenes.Transformation trans = new Ambertation.Scenes.Transformation();
				trans.Rotation.X = tn.Rotation.GetEulerAngles().X;
				trans.Rotation.Y = tn.Rotation.GetEulerAngles().Y;
				trans.Rotation.Z = tn.Rotation.GetEulerAngles().Z;

				trans.Translation.X = tn.TransformX;
				trans.Translation.Y = tn.TransformY;
				trans.Translation.Z = tn.TransformZ;

				var tnode = new Avalonia.Controls.TreeViewItem { Header = tn.ToString() };
				nodes.Add(tnode);
			}
			else
			{
				foreach (SimPe.Interfaces.Scenegraph.ICresChildren cld in bl)
					AddJoint(lmb, cld, parent, nodes);
			}
		}

		private void dx_ResetDevice(object sender, EventArgs e)
		{
			if (!inter)
			{
				inter = true;
				lb_SelectedIndexChanged(null, null);
				inter = false;
			}
		}

		Ambertation.Graphics.MeshList root;
		Hashtable jointmap = new Hashtable();
		bool inter;

		private void lb_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lb.SelectedItem == null) return;
			jointmap.Clear();

			ListedMeshBlocks lmb = (ListedMeshBlocks)lb.SelectedItem;
			if (lmb.CRES == null || lmb.GMDC == null) return;

			SimPe.Plugin.ResourceNode rn = (SimPe.Plugin.ResourceNode)lmb.CRES.Blocks[0];

			if (root != null) root.Dispose();
			root = new Ambertation.Graphics.MeshList();

			AddJoint(lmb, rn, root, (System.Collections.IList)tv.Items);

			animdata.Clear();
			foreach (SimPe.Plugin.Anim.AnimationFrameBlock afb in lmb.ANIMBlock.Part2)
			{
				SimPe.Plugin.Anim.AnimationFrameBlock afb2 = afb.CloneBase(true);
				object o = jointmap[afb.Name];
				if (o == null) continue;
				Ambertation.Graphics.MeshBox mb = (Ambertation.Graphics.MeshBox)o;

				animdata.Add(new AnimationData(afb2, mb, lmb.ANIMBlock.Animation.TotalTime));
			}

			if (inter) return;
			inter = true;
			inter = false;
		}

		private void tv_AfterSelect(object sender, EventArgs e)
		{
			// body is intentionally empty — DirectX rendering not implemented on Mac
		}

		ArrayList animdata = new ArrayList();
		private void btPlay_Click(object sender, System.EventArgs e)
		{
			if (lb.SelectedItem == null) return;
			ListedMeshBlocks lmb = (ListedMeshBlocks)lb.SelectedItem;

			timer1.Interval = TimeSpan.FromMilliseconds(1000.0 / 25);
			timecode = 0;
			pb.Value = 0;
			pb.Maximum = lmb.ANIMBlock.Animation.TotalTime;

			timer1.Start();
		}

		int timecode;
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			if (lb.SelectedItem == null)
			{
				timer1.Stop();
				return;
			}

			ListedMeshBlocks lmb = (ListedMeshBlocks)lb.SelectedItem;
			if (timecode > lmb.ANIMBlock.Animation.TotalTime)
			{
				timer1.Stop();
				pb.Value = 0;
				return;
			}

			pb.Value = timecode;

			foreach (AnimationData ad in animdata)
				ad.SetFrame(timecode);

			timecode += 40;
		}
	}
}
