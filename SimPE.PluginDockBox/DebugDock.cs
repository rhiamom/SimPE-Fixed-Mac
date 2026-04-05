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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ambertation.Windows.Forms;

namespace SimPe.Plugin.Tool.Dockable
{
	/// <summary>
	/// Summary description for DebugDock.
	/// </summary>
	public class DebugDock : Ambertation.Windows.Forms.DockPanel, SimPe.Interfaces.IDockableTool
	{
        bool dun = false;
        private Avalonia.Controls.StackPanel xpGradientPanel1;
        private Avalonia.Controls.TextBlock label1;
        private Avalonia.Controls.TextBlock lbMem;
        private Avalonia.Controls.TextBlock label2;
        private Avalonia.Controls.ListBox lbft;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DebugDock()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
            ThemeManager.Global.AddControl(this.xpGradientPanel1);
		}

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.xpGradientPanel1 = new Avalonia.Controls.StackPanel();
            this.lbft = new Avalonia.Controls.ListBox();
            this.label2 = new Avalonia.Controls.TextBlock { Text = "FileTable Content:" };
            this.lbMem = new Avalonia.Controls.TextBlock { Text = "0" };
            this.label1 = new Avalonia.Controls.TextBlock { Text = "Memory Usage:" };
            this.ButtonText = "Debug";
            this.CaptionText = "Debug Dock";
            this.TabText = "Debug";
            this.TabIconBitmap = SimPe.LoadIcon.LoadAvaloniaBitmap("DebugDock_$this.TabImage.png");
        }
        #endregion

        public Ambertation.Windows.Forms.DockPanel GetDockableControl()
		{
			return this;
		}

		public event SimPe.Events.ChangedResourceEvent ShowNewResource;

		public void RefreshDock(object sender, SimPe.Events.ResourceEventArgs es)
		{
			lbMem.Text = GC.GetTotalMemory(false).ToString("N0") + " Byte";					
		}


		#region IToolPlugin Member

		public override string ToString()
		{
			return this.Text;
		}

		#endregion

        private void label1_Click(object sender, System.EventArgs e)
        {
            RefreshDock(null, null);
		}

		private void label2_Click(object sender, System.EventArgs e)
		{
            if (dun) return; // prevent running while running
            this.label2.Foreground = Avalonia.Media.Brushes.Black;
            dun = true;
            string savey = "replicated";
            int savnum = 0;
            while (System.IO.File.Exists(System.IO.Path.Combine(PathProvider.SimSavegameFolder, savey + ".txt")))
            {
                savnum++;
                savey += Convert.ToString(savnum);
            }
            System.IO.StreamWriter sw = System.IO.File.CreateText(System.IO.Path.Combine(PathProvider.SimSavegameFolder, savey + ".txt"));
            string objname = System.IO.Path.Combine(PathProvider.Global.Latest.InstallFolder, @"TSData\Res\Objects\objects.package");
            sw.WriteLine(PathProvider.Global.Latest.DisplayName);
			sw.WriteLine(System.IO.Path.GetFileName(objname)+"----------------------------------------");
			SimPe.Interfaces.Files.IPackageFile pkg = SimPe.Packages.File.LoadFromFile(objname);
            Wait.Start(pkg.Index.Length);
            Wait.Message = "Loading " + System.IO.Path.GetFileName(objname);
			FileTable.FileIndex.Load();
			lbft.Items.Clear();
            lbft.Items.Add(PathProvider.Global.Latest.DisplayName + " : " + System.IO.Path.GetFileName(objname));

            foreach (SimPe.Interfaces.Files.IPackedFileDescriptor pfd in pkg.Index)
            {
                SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem[] items = FileTable.FileIndex.FindFile(pfd, null);
                lbft.Items.Add(pfd.ToString());
                sw.WriteLine(pfd.ToString());
                Wait.Progress++;
            }
				
			lbft.Items.Add(" m: "+pkg.Index.Length.ToString());

			sw.Close();
			sw.Dispose();
			sw = null;
            dun = false;
            Wait.Stop(true);
            this.label2.Foreground = Avalonia.Media.Brushes.Blue;
		}

		#region IToolExt Member

		public int Shortcut
		{
			get
			{
				return 0;
			}
		}

		public System.Drawing.Image Icon
		{
			get
			{
				return this.TabImage;
			}
		}	

		public new virtual bool Visible 
		{
			get { return this.IsDocked ||  this.IsFloating; }
		}

		#endregion
	}
}
