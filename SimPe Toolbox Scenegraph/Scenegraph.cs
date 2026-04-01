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
using Avalonia.Interactivity;
using SimPe.Interfaces;
using SimPe.Scenegraph.Compat;
using Ambertation.Windows.Forms;
using Ambertation.Windows.Forms.Graph;

namespace SimPe.Plugin
{
	/// <summary>
	/// Scenegrapher window.
	/// </summary>
	public class ScenegraphForm : Avalonia.Controls.Window
	{
		private Avalonia.Controls.Panel panel2;
		private SimPe.Scenegraph.Compat.GroupBox groupBox1;
		private Avalonia.Controls.ComboBox cbrefnames;
		private Avalonia.Controls.TextBox tbflname;
		private Avalonia.Controls.TextBlock label2;
		private Avalonia.Controls.TextBlock label1;
		private Avalonia.Controls.Panel panel1;
		private Avalonia.Controls.Button llopen;
		private SimPe.Scenegraph.Compat.GroupBox groupBox2;
		private Avalonia.Controls.TextBlock label3;
		private Avalonia.Controls.ComboBox cbLineStyle;
		private Avalonia.Controls.CheckBox cbQuality;
		private Avalonia.Controls.CheckBox cbPriority;

		public ScenegraphForm()
		{
			BuildLayout();

			gb = new GraphBuilder(panel1, new EventHandler(GraphItemClick));
			LinkControlLineMode[] ls = (LinkControlLineMode[])System.Enum.GetValues(typeof(LinkControlLineMode));
			foreach (LinkControlLineMode l in ls)
			{
				this.cbLineStyle.Items.Add(l);
				if ((int)l == Helper.XmlRegistry.GraphLineMode) this.cbLineStyle.SelectedIndex = cbLineStyle.Items.Count - 1;
			}

			cbQuality.IsChecked = Helper.XmlRegistry.GraphQuality;
			cbPriority.IsChecked = Helper.XmlRegistry.CresPrioritize;

			cbQuality_CheckedChanged(cbQuality, null);
			cbLineStyle_SelectedIndexChanged(cbLineStyle, null);
			SimPe.ThemeManager tm = SimPe.ThemeManager.Global.CreateChild();
			tm.AddControl(this.panel2);
		}

		private void BuildLayout()
		{
			label1 = new Avalonia.Controls.TextBlock { Text = "FileName:", Margin = new Avalonia.Thickness(4) };
			tbflname = new Avalonia.Controls.TextBox { IsReadOnly = true, Margin = new Avalonia.Thickness(4) };
			label2 = new Avalonia.Controls.TextBlock { Text = "Reference Name:", Margin = new Avalonia.Thickness(4) };
			cbrefnames = new Avalonia.Controls.ComboBox { Margin = new Avalonia.Thickness(4), HorizontalAlignment = HorizontalAlignment.Stretch };
			llopen = new Avalonia.Controls.Button { Content = "open", IsEnabled = false, Margin = new Avalonia.Thickness(4) };
			llopen.Click += OpenPfd;

			var gb1Inner = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical };
			gb1Inner.Children.Add(label1);
			gb1Inner.Children.Add(tbflname);
			gb1Inner.Children.Add(label2);
			gb1Inner.Children.Add(cbrefnames);
			gb1Inner.Children.Add(llopen);
			groupBox1 = new SimPe.Scenegraph.Compat.GroupBox { Text = "Properties" };
			groupBox1.Content = gb1Inner;

			cbQuality = new Avalonia.Controls.CheckBox { Content = "High Quality", Margin = new Avalonia.Thickness(4) };
			cbQuality.IsCheckedChanged += (s, e) => cbQuality_CheckedChanged(s, EventArgs.Empty);
			cbPriority = new Avalonia.Controls.CheckBox { Content = "CRES First", Margin = new Avalonia.Thickness(4) };
			cbPriority.IsCheckedChanged += (s, e) => cbPriority_CheckedChanged(s, EventArgs.Empty);
			label3 = new Avalonia.Controls.TextBlock { Text = "Connector Style:", Margin = new Avalonia.Thickness(4) };
			cbLineStyle = new Avalonia.Controls.ComboBox { Margin = new Avalonia.Thickness(4), HorizontalAlignment = HorizontalAlignment.Stretch };
			cbLineStyle.SelectionChanged += (s, e) => cbLineStyle_SelectedIndexChanged(s, EventArgs.Empty);

			var gb2Inner = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical };
			gb2Inner.Children.Add(cbQuality);
			gb2Inner.Children.Add(cbPriority);
			gb2Inner.Children.Add(label3);
			gb2Inner.Children.Add(cbLineStyle);
			groupBox2 = new SimPe.Scenegraph.Compat.GroupBox { Text = "Graph" };
			groupBox2.Content = gb2Inner;

			var panel2Grid = new Avalonia.Controls.Grid();
			panel2Grid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = new Avalonia.Controls.GridLength(440) });
			panel2Grid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = new Avalonia.Controls.GridLength(1, Avalonia.Controls.GridUnitType.Star) });
			Avalonia.Controls.Grid.SetColumn(groupBox1, 0);
			Avalonia.Controls.Grid.SetColumn(groupBox2, 1);
			panel2Grid.Children.Add(groupBox1);
			panel2Grid.Children.Add(groupBox2);

			panel2 = new Avalonia.Controls.Panel { Height = 180 };
			panel2.Children.Add(panel2Grid);

			panel1 = new Avalonia.Controls.Panel();

			var root = new Avalonia.Controls.DockPanel { LastChildFill = true };
			Avalonia.Controls.DockPanel.SetDock(panel2, Avalonia.Controls.Dock.Bottom);
			root.Children.Add(panel2);
			root.Children.Add(panel1);

			Title = "Scenegrapher";
			Width = 800;
			Height = 470;
			Content = root;
		}


		public void GraphItemClick(object sender, EventArgs e)
		{
			GraphItem gi = (GraphItem)sender;
			Hashtable ht = null;
			llopen.IsEnabled = false;
			selpfd = null;
			if (gi.Tag.GetType() == typeof(string))
			{
				this.tbflname.Text = (string)gi.Tag;
				this.cbrefnames.Items.Clear();
				cbrefnames.SelectedItem = null;
			}
			else if (gi.Tag.GetType() == typeof(GenericRcol))
			{
				GenericRcol rcol = (GenericRcol)gi.Tag;
				this.tbflname.Text = rcol.FileName;
				this.cbrefnames.Items.Clear();
				cbrefnames.SelectedItem = null;
				ht = rcol.ReferenceChains;

				if (rcol.Package.FileName == open_pkg.FileName) selpfd = rcol.FileDescriptor;
			}
			else if (gi.Tag.GetType() == typeof(SimPe.Plugin.MmatWrapper))
			{
				SimPe.Plugin.MmatWrapper mmat = (SimPe.Plugin.MmatWrapper)gi.Tag;
				this.tbflname.Text = mmat.SubsetName;
				this.cbrefnames.Items.Clear();
				cbrefnames.SelectedItem = null;
				ht = mmat.ReferenceChains;

				if (mmat.Package.FileName == open_pkg.FileName) selpfd = mmat.FileDescriptor;
			}

			llopen.IsEnabled = (selpfd != null);

			if (ht != null)
				foreach (string s in ht.Keys)
					foreach (Interfaces.Files.IPackedFileDescriptor pfd in (ArrayList)ht[s])
					{
						this.cbrefnames.Items.Add(pfd.Filename);
					}

			if (cbrefnames.Items.Count > 0) cbrefnames.SelectedIndex = 0;
		}


		SimPe.Interfaces.Files.IPackedFileDescriptor pfd, selpfd;
		SimPe.Interfaces.Files.IPackageFile open_pkg;
		GraphBuilder gb;

		/// <summary>
		/// Build the SceneGraph
		/// </summary>
		public void Execute(IProviderRegistry prov, SimPe.Interfaces.Files.IPackageFile simpe_pkg, ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd)
		{
			this.pfd = pfd;
			this.open_pkg = simpe_pkg;
			WaitingScreen.Wait();
			try
			{
				llopen.IsEnabled = false;
				SimPe.Interfaces.Files.IPackageFile orgpkg = simpe_pkg;

				DateTime start = DateTime.Now;
				FileTable.FileIndex.Load();
				SimPe.Interfaces.Scenegraph.IScenegraphFileIndex fileindex = FileTable.FileIndex.Clone();
				fileindex.AddIndexFromPackage(simpe_pkg);

				SimPe.Interfaces.Scenegraph.IScenegraphFileIndex oldfileindex = FileTable.FileIndex;
				FileTable.FileIndex = fileindex;

				FileTable.FileIndex = oldfileindex;

				gb.BuildGraph(simpe_pkg, fileindex);
				gb.FindUnused(orgpkg);

				WaitingScreen.Stop();
				TimeSpan runtime = DateTime.Now.Subtract(start);
				if (Helper.XmlRegistry.HiddenMode)
					Title = "Runtime: " + runtime.TotalSeconds + " sek. = " + runtime.TotalMinutes + " min.";
				RemoteControl.ShowSubForm(this);

				pfd = this.pfd;
			}
#if !DEBUG
			catch (Exception ex) { Helper.ExceptionMessage("", ex); }
#endif
			finally { WaitingScreen.Stop(); }
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void OpenPfd(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			pfd = selpfd;
			Close();
		}

		private void cbQuality_CheckedChanged(object sender, System.EventArgs e)
		{
			gb.Graph.Quality = cbQuality.IsChecked.GetValueOrDefault();
			Helper.XmlRegistry.GraphQuality = gb.Graph.Quality;
		}

		private void cbLineStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cbLineStyle.SelectedIndex < 0) return;
			gb.Graph.LineMode = (LinkControlLineMode)cbLineStyle.SelectedItem;
			Helper.XmlRegistry.GraphLineMode = (int)gb.Graph.LineMode;
		}

		private void cbPriority_CheckedChanged(object sender, EventArgs e)
		{
			Helper.XmlRegistry.CresPrioritize = cbPriority.IsChecked.GetValueOrDefault();
		}
	}
}
