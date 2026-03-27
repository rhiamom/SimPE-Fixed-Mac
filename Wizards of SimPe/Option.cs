/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSweatShop                                 *
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using Ambertation.Windows.Forms;
using Avalonia.Controls;

namespace SimPe.Wizards
{
	/// <summary>
	/// Summary description for Option.
	/// </summary>
	public class Option
	{
		private SimPe.Scenegraph.Compat.PictureBox pbtop;
		private SimPe.Scenegraph.Compat.PictureBox pbbottom;
		internal Canvas pnopt;
		private SimPe.Scenegraph.Compat.PictureBox pbstretch;
		private Button linkLabel1;
		private TextBlock lbmsg;
		private TextBlock label1;
		private TextBlock label2;
		internal TextBox tbsims;
		private Button linkLabel2;
		private Button linkLabel3;
		internal TextBox tbsave;
		private Button linkLabel4;
		internal TextBox tbdds;
		internal Button llsims;
		internal Button llsave;
		private System.Windows.Forms.FolderBrowserDialog fbd;
		private TextBlock lldds;
		private Button lldds2;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.ComponentModel.Container components = null;
		const string FONT_FAMILY = "Verdana";
		const string FONT_FAMILY_SERIF = "Georgia";
        private XPTaskBoxSimple taskBox1;
        private Button linkLabel5;

		// WinForms-compat layout stubs
		public System.Drawing.Point Location { get; set; }
		public System.Drawing.Size Size { get; set; }

		public Option()
		{
			InitializeComponent();
            ThemeManager.Global.AddControl(this.taskBox1);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		public void Dispose()
		{
			if(components != null)
			{
				components.Dispose();
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pbtop      = new SimPe.Scenegraph.Compat.PictureBox();
            this.pbbottom   = new SimPe.Scenegraph.Compat.PictureBox();
            this.pnopt      = new Canvas();
            this.taskBox1   = new XPTaskBoxSimple();
            this.linkLabel4 = new Button();
            this.tbsims     = new TextBox();
            this.llsave     = new Button();
            this.lldds2     = new Button();
            this.label2     = new TextBlock();
            this.label1     = new TextBlock();
            this.linkLabel2 = new Button();
            this.lbmsg      = new TextBlock();
            this.lldds      = new TextBlock();
            this.tbsave     = new TextBox();
            this.llsims     = new Button();
            this.linkLabel3 = new Button();
            this.tbdds      = new TextBox();
            this.linkLabel5 = new Button();
            this.linkLabel1 = new Button();
            this.pbstretch  = new SimPe.Scenegraph.Compat.PictureBox();
            this.fbd        = new System.Windows.Forms.FolderBrowserDialog();
            this.ofd        = new System.Windows.Forms.OpenFileDialog();
            //
            // linkLabel4
            //
            this.linkLabel4.Content = "Browse";
            this.linkLabel4.Click += (s, e) => FldDds(s, EventArgs.Empty);
            //
            // tbsims
            //
            this.tbsims.TextChanged += (s, e) => Change(s, EventArgs.Empty);
            //
            // llsave
            //
            this.llsave.Content = "suggest";
            this.llsave.Click += (s, e) => SugSave(s, EventArgs.Empty);
            //
            // lldds2
            //
            this.lldds2.Content = "You can download them here";
            this.lldds2.Click += (s, e) => LinkDDS(s, EventArgs.Empty);
            //
            // label2
            //
            this.label2.Text = "Nvidia DDS Utilities:";
            //
            // label1
            //
            this.label1.Text = "Sims 2 Savegame Folder:";
            //
            // linkLabel2
            //
            this.linkLabel2.Content = "Browse";
            this.linkLabel2.Click += (s, e) => FldSims(s, EventArgs.Empty);
            //
            // lbmsg
            //
            this.lbmsg.Text = "Sims 2 Installation Folder:";
            //
            // lldds
            //
            this.lldds.Text = "The Nvidia DDS Utilities were not found. You should install them in order to get " +
                "a higher quality for your recolours.";
            //
            // tbsave
            //
            this.tbsave.TextChanged += (s, e) => Change(s, EventArgs.Empty);
            //
            // llsims
            //
            this.llsims.Content = "suggest";
            this.llsims.Click += (s, e) => SugSims(s, EventArgs.Empty);
            //
            // linkLabel3
            //
            this.linkLabel3.Content = "Browse";
            this.linkLabel3.Click += (s, e) => FldSave(s, EventArgs.Empty);
            //
            // tbdds
            //
            this.tbdds.TextChanged += (s, e) => Change(s, EventArgs.Empty);
            //
            // linkLabel5
            //
            this.linkLabel5.Content = "System Test";
            this.linkLabel5.Click += (s, e) => linkLabel5_LinkClicked(s, EventArgs.Empty);
            //
            // linkLabel1
            //
            this.linkLabel1.Content = "Close";
            this.linkLabel1.Click += (s, e) => Hide(s, EventArgs.Empty);
            //
            // ofd
            //
            this.ofd.Filter = "DDS Utilities (nvdxt.exe)|nvdxt.exe";
            this.ofd.Title = "Locate Nvidia DDS Tools";
		}
		#endregion

		internal Form1 form1;
		private void Hide(object sender, EventArgs e)
		{
            if (PathProvider.RealSavegamePath != tbsave.Text) PathProvider.SimSavegameFolder = tbsave.Text;
            if (PathProvider.Global[Expansions.BaseGame].RealInstallFolder != tbsims.Text) PathProvider.Global[Expansions.BaseGame].InstallFolder = tbsims.Text;
            if (PathProvider.Global.NvidiaDDSPath != tbdds.Text) PathProvider.Global.NvidiaDDSPath = tbdds.Text;
			form1.HideOptions(sender, e);
		}

		public static bool HaveObjects
		{
            get { return System.IO.File.Exists(System.IO.Path.Combine(PathProvider.Global[Expansions.BaseGame].InstallFolder, "TSData" + Helper.PATH_SEP + "Res" + Helper.PATH_SEP + "Objects" + Helper.PATH_SEP + "objects.package")); }
		}

		public static bool HaveSavefolder
		{
            get { return System.IO.Directory.Exists(System.IO.Path.Combine(PathProvider.SimSavegameFolder, "Downloads")); }
		}

		public static bool HaveDDS
		{
            get { return System.IO.File.Exists(PathProvider.Global.NvidiaDDSTool); }
		}

		private void Change(object sender, System.EventArgs e)
		{
			llsims.IsVisible = !System.IO.File.Exists(System.IO.Path.Combine(tbsims.Text, "TSData"+Helper.PATH_SEP+"Res"+Helper.PATH_SEP+"Objects"+Helper.PATH_SEP+"objects.package"));
			llsave.IsVisible = !System.IO.Directory.Exists(System.IO.Path.Combine(tbsave.Text, "Downloads"));
			lldds.IsVisible = !System.IO.File.Exists(System.IO.Path.Combine(tbdds.Text, "nvdxt.exe"));
			lldds2.IsVisible = lldds.IsVisible;
		}

		private void SugSims(object sender, EventArgs e)
		{
            tbsims.Text = PathProvider.Global[Expansions.BaseGame].RealInstallFolder;
		}

		private void SugSave(object sender, EventArgs e)
		{
            tbsave.Text = PathProvider.RealSavegamePath;
		}

		private void FldSims(object sender, EventArgs e)
		{
            if (System.IO.Directory.Exists(PathProvider.Global[Expansions.BaseGame].RealInstallFolder)) fbd.SelectedPath = PathProvider.Global[Expansions.BaseGame].RealInstallFolder;
			if (fbd.ShowDialog()==SimPe.DialogResult.OK) tbsims.Text = fbd.SelectedPath;
		}

		private void FldSave(object sender, EventArgs e)
		{
            if (System.IO.Directory.Exists(PathProvider.RealSavegamePath)) fbd.SelectedPath = PathProvider.RealSavegamePath;
			if (fbd.ShowDialog()==SimPe.DialogResult.OK) tbsave.Text = fbd.SelectedPath;
		}

		private void LinkDDS(object sender, EventArgs e)
		{
            // Open NVIDIA DDS tools download page in the default browser
            try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://developer.nvidia.com/legacy-texture-tools") { UseShellExecute = true }); } catch { }
		}

		private void FldDds(object sender, EventArgs e)
		{
			if (ofd.ShowDialog()==SimPe.DialogResult.OK) tbdds.Text = System.IO.Path.GetDirectoryName(ofd.FileName);
		}

		private void linkLabel5_LinkClicked(object sender, EventArgs e)
		{
			CheckForm f = new CheckForm();
			f.ShowDialog();
		}
	}
}
