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
using SimPe.Scenegraph.Compat;
using SelectionMode = Avalonia.Controls.SelectionMode;
using System.Collections;
using System.ComponentModel;
using SimPe;
using DialogResult = System.Windows.Forms.DialogResult;
using SimPe.Data;
using SimPe.Interfaces;


namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for Workshop.
	/// </summary>
	public class Workshop : Avalonia.Controls.Window
	{
		private Avalonia.Controls.TabControl tabControl2;
		private Avalonia.Controls.TabItem tabPage1;
		private Avalonia.Controls.TabItem tabPage2;
		private Avalonia.Controls.TextBox tbflname;
		private Avalonia.Controls.Button button1;
		private System.Windows.Forms.OpenFileDialog ofd;
		private Avalonia.Controls.CheckBox cbfix;
		private Avalonia.Controls.CheckBox cbclean;
		private Avalonia.Controls.CheckBox cbdefault;
		private Avalonia.Controls.TabItem tabPage3;
        private SimPe.Scenegraph.Compat.TreeView tv;
        private SimPe.Scenegraph.Compat.ImageList ilist;
        private Avalonia.Controls.CheckBox cbparent;
        private Avalonia.Controls.CheckBox cbwallmask;
        private Avalonia.Controls.CheckBox cbanim;
        private System.ComponentModel.IContainer components;
        private System.ComponentModel.ComponentResourceManager resources1;

		TreeNode tviapl;
		TreeNode tvideco;
		TreeNode tvielectro;
		TreeNode tvigeneral;
		TreeNode tvilight;
		TreeNode tviplumb;
		TreeNode tviseating;
		TreeNode tvisurfaces;
		TreeNode tvihobby;
		TreeNode tviaspiration;
		TreeNode tvicareer;
		TreeNode tviother;

		TreeNode tvistairs;
		TreeNode tviperson;
		TreeNode tviarchsup;
		TreeNode tvisimtype;
		TreeNode tvidoor;
		TreeNode tvimodstair;
		TreeNode tvimodstairport;
		TreeNode tvivehicle;
		TreeNode tvioutfit;
		TreeNode tvimemory;
		TreeNode tvitemplate;
		TreeNode tviwindow;

        TreeNode tvigarden;

        public Workshop()
        {
            //
            // Required designer variable.
            //
            InitializeComponent();

            btclone.IsEnabled = false;
            this.tabControl1.SelectedIndex = 1;
            lbobj.SelectionMode = SelectionMode.Single;
            this.cbColor.IsVisible = false;
            this.cbColor.IsChecked = false;
            this.cbColorExt.IsEnabled = true;
            this.cbColorExt.IsChecked = true;
            this.rbColor.IsEnabled = true;

            this.tviapl = new SimPe.Scenegraph.Compat.TreeNode("Appliances"); tv.Nodes.Add(tviapl);
            this.tvideco = new SimPe.Scenegraph.Compat.TreeNode("Decorative"); tv.Nodes.Add(tvideco);
            this.tvielectro = new SimPe.Scenegraph.Compat.TreeNode("Electronics"); tv.Nodes.Add(tvielectro);
            this.tvigeneral = new SimPe.Scenegraph.Compat.TreeNode("General"); tv.Nodes.Add(tvigeneral);
            this.tvilight = new SimPe.Scenegraph.Compat.TreeNode("Lights"); tv.Nodes.Add(tvilight);
            this.tviplumb = new SimPe.Scenegraph.Compat.TreeNode("Plumbing"); tv.Nodes.Add(tviplumb);
            this.tviseating = new SimPe.Scenegraph.Compat.TreeNode("Seating"); tv.Nodes.Add(tviseating);
            this.tvisurfaces = new SimPe.Scenegraph.Compat.TreeNode("Surfaces"); tv.Nodes.Add(tvisurfaces);
            this.tvihobby = new SimPe.Scenegraph.Compat.TreeNode("Hobbies"); tv.Nodes.Add(tvihobby);
            this.tviaspiration = new SimPe.Scenegraph.Compat.TreeNode("Aspiration Rewards"); tv.Nodes.Add(tviaspiration);
            this.tvicareer = new SimPe.Scenegraph.Compat.TreeNode("Career Rewards"); tv.Nodes.Add(tvicareer);
            this.tviother = new SimPe.Scenegraph.Compat.TreeNode("Others"); tv.Nodes.Add(tviother);

            this.tvistairs = new SimPe.Scenegraph.Compat.TreeNode("Stairs"); tv.Nodes.Add(tvistairs);
            this.tviperson = new SimPe.Scenegraph.Compat.TreeNode("Persons"); tv.Nodes.Add(tviperson);
            this.tviarchsup = new SimPe.Scenegraph.Compat.TreeNode("ArchitecturalSupports"); tv.Nodes.Add(tviarchsup);
            this.tvisimtype = new SimPe.Scenegraph.Compat.TreeNode("SimTypes"); tv.Nodes.Add(tvisimtype);
            this.tvidoor = new SimPe.Scenegraph.Compat.TreeNode("Doors"); tv.Nodes.Add(tvidoor);
            this.tvimodstair = new SimPe.Scenegraph.Compat.TreeNode("ModularStairs"); tv.Nodes.Add(tvimodstair);
            this.tvimodstairport = new SimPe.Scenegraph.Compat.TreeNode("ModularStairPorts"); tv.Nodes.Add(tvimodstairport);
            this.tvivehicle = new SimPe.Scenegraph.Compat.TreeNode("Vehicles"); tv.Nodes.Add(tvivehicle);
            this.tvioutfit = new SimPe.Scenegraph.Compat.TreeNode("Outfits"); tv.Nodes.Add(tvioutfit);
            this.tvimemory = new SimPe.Scenegraph.Compat.TreeNode("Memories"); tv.Nodes.Add(tvimemory);
            this.tvitemplate = new SimPe.Scenegraph.Compat.TreeNode("Templates"); tv.Nodes.Add(tvitemplate);
            this.tviwindow = new SimPe.Scenegraph.Compat.TreeNode("Windows"); tv.Nodes.Add(tviwindow);

            this.tvigarden = new SimPe.Scenegraph.Compat.TreeNode("Garden"); tv.Nodes.Add(tvigarden);
        }

		/// <summary>
		/// Clean the Group Tree
		/// </summary>
		void CleanTree()
		{
			this.tviapl.Nodes.Clear();
			this.tvideco.Nodes.Clear();
			this.tvielectro.Nodes.Clear();
			this.tvigeneral.Nodes.Clear();
			this.tvilight.Nodes.Clear();
			this.tviplumb.Nodes.Clear();
			this.tviseating.Nodes.Clear();			
			this.tvisurfaces.Nodes.Clear();
			this.tvihobby.Nodes.Clear();
			this.tviaspiration.Nodes.Clear();
			this.tvicareer.Nodes.Clear();
			this.tviother.Nodes.Clear();

			this.tvistairs.Nodes.Clear();
			this.tviperson.Nodes.Clear();
			this.tviarchsup.Nodes.Clear();
			this.tvisimtype.Nodes.Clear();
			this.tvidoor.Nodes.Clear();
			this.tvimodstair.Nodes.Clear();
			this.tvimodstairport.Nodes.Clear();
			this.tvivehicle.Nodes.Clear();
			this.tvioutfit.Nodes.Clear();
			this.tvimemory.Nodes.Clear();
			this.tvitemplate.Nodes.Clear();
			this.tviwindow.Nodes.Clear();
            this.tvigarden.Nodes.Clear();

			this.ilist.Images.Clear();
            this.ilist.Images.Add(new Bitmap(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.Tool.Dockable.subitems.png")));
            this.ilist.Images.Add(new Bitmap(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.Tool.Dockable.nothumb.png")));
            this.ilist.Images.Add(new Bitmap(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.Tool.Dockable.custom.png")));
		}

		/// <summary>
		/// Sort this Item to the Tree
		/// </summary>		
		/// <param name="a"></param>
		/// <param name="img"></param>
		/// <param name="type">The Type of the Object</param>
		/// <param name="functionsort">The function SOrt Value</param>
		/// <param name="name">The name of the package where this Object was found in</param>
		/// <param name="group">The group for this Object</param>
        void PutItemToTree(Data.Alias a, Image img, SimPe.Data.ObjectTypes type, SimPe.PackedFiles.Wrapper.ObjFunctionSort functionsort, SimPe.PackedFiles.Wrapper.ObjBuildType buildtype, string name, uint group)
        {
            TreeNode node = new SimPe.Scenegraph.Compat.TreeNode(a.Name + " (0x" + Helper.HexString(group) + ")");
			node.Tag = a;
			if (img!=null) 
			{
				node.ImageIndex = ilist.Images.Count;
				node.SelectedImageIndex = ilist.Images.Count;
				ilist.Images.Add(img); // ImageLoader.Preview returns SKBitmap; Images.Add expects System.Drawing.Image — use original
			} 
			else 
			{
				node.ImageIndex = 1;
				if (name!=null) 
				{
					name = name.Trim().ToLower();
                    if (name.StartsWith(PathProvider.SimSavegameFolder.Trim().ToLower())) 
						node.ImageIndex = 2;
				}
				node.SelectedImageIndex = node.ImageIndex;
			}

			if (type == Data.ObjectTypes.Stairs) 
			{
				this.tvistairs.Nodes.Add(node);
			} 
			else if (type == Data.ObjectTypes.ArchitecturalSupport)
			{
				this.tviarchsup.Nodes.Add(node);
			} 
			else if (type == Data.ObjectTypes.Door)
			{
				this.tvidoor.Nodes.Add(node);
			} 
			else if (type == Data.ObjectTypes.Memory)
			{
				this.tvimemory.Nodes.Add(node);
			} 
			else if (type == Data.ObjectTypes.ModularStairs)
			{
				this.tvimodstair.Nodes.Add(node);
			} 
			else if (type == Data.ObjectTypes.ModularStairsPortal)
			{
				this.tvimodstairport.Nodes.Add(node);
			} 
			else if (type == Data.ObjectTypes.Outfit)
			{
				this.tvioutfit.Nodes.Add(node);
			} 
			else if (type == Data.ObjectTypes.Person)
			{
				this.tviperson.Nodes.Add(node);
			} 
			else if (type == Data.ObjectTypes.SimType)
			{
				this.tvisimtype.Nodes.Add(node);
			} 
			else if (type == Data.ObjectTypes.Template)
			{
				this.tvitemplate.Nodes.Add(node);
			} 
			else if (type == Data.ObjectTypes.Vehicle)
			{
				this.tvivehicle.Nodes.Add(node);
			} 
			else if (type == Data.ObjectTypes.Window)
			{
				this.tviwindow.Nodes.Add(node);
			}

			else 
			{
				bool added = false;
				if (functionsort.InAppliances) { this.tviapl.Nodes.Add(node); added=true; }
				else if (functionsort.InDecorative) { this.tvideco.Nodes.Add(node); added=true; }
				else if (functionsort.InElectronics) { this.tvielectro.Nodes.Add(node); added=true; }
				else if (functionsort.InGeneral) { this.tvigeneral.Nodes.Add(node); added=true; }
				else if (functionsort.InLighting) { this.tvilight.Nodes.Add(node); added=true; }
				else if (functionsort.InPlumbing) { this.tviplumb.Nodes.Add(node); added=true; }
				else if (functionsort.InSeating) { this.tviseating.Nodes.Add(node); added=true; }
				else if (functionsort.InSurfaces) { this.tvisurfaces.Nodes.Add(node); added=true; }
				else if (functionsort.InHobbies) { this.tvihobby.Nodes.Add(node); added=true; }
				else if (functionsort.InAspirationRewards) { this.tviaspiration.Nodes.Add(node); added=true; }
				else if (functionsort.InCareerRewards) { this.tvicareer.Nodes.Add(node); added=true; }
                else if (buildtype.InGarden) { this.tvigarden.Nodes.Add(node); added = true; }

				if (!added) this.tviother.Nodes.Add(node);
			}
		}


		#region Cache Handling		
        SimPe.Cache.ObjectLoaderCacheFile cachefile;
		bool cachechg;

		/// <summary>
		/// Get the Name of the Object Cache File
		/// </summary>
		string CacheFileName 
		{
			get {return Helper.SimPeLanguageCache;}
		}

		/// <summary>
		/// Load the Object Cache
		/// </summary>
		void LoadCachIndex()
		{
			if (cachefile!=null) return;
			
			cachechg = false;
            cachefile = new SimPe.Cache.ObjectLoaderCacheFile();
		
			if (!Helper.XmlRegistry.UseCache) return;
			WaitingScreen.UpdateMessage("Loading Cache - Please Wait");
            try
            {
                cachefile.Load(CacheFileName);
                cachefile.LoadObjects();
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
            finally { WaitingScreen.Stop(); }
		}

		/// <summary>
		/// Save the Cache to the Disk
		/// </summary>
		void SaveCacheIndex()
		{
			if (!Helper.XmlRegistry.UseCache) return;
			if (!cachechg) return;
			if (WaitingScreen.Running) WaitingScreen.UpdateMessage("Saving Cache");

			cachefile.Save(CacheFileName);
		}		
		#endregion

		void BuildListing()
		{
			//build Object List
			if (lbobj.Items.Count==0) 
			{								
				DateTime start = DateTime.Now;						
				WaitingScreen.Wait();
				this.ilist.ImageSize = new Size(Helper.XmlRegistry.OWThumbSize, Helper.XmlRegistry.OWThumbSize);
				//LoadCachIndex();
				tv.BeginUpdate();
				lbobj.Items.Clear();
				this.CleanTree();		
				lbobj.IsEnabled = false;
				this.tv.IsEnabled = false;
				tv.Sorted = false;

				SimPe.Plugin.Tool.Dockable.ObjectLoader ol = new SimPe.Plugin.Tool.Dockable.ObjectLoader(this.ilist);
				ol.Finished += new EventHandler(ol_Finished);
				ol.LoadedItem += new SimPe.Plugin.Tool.Dockable.ObjectLoader.LoadItemHandler(ol_LoadedItem);
				ol.LoadData();
				WaitingScreen.Stop();
				return;
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected virtual void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lbobj = new Avalonia.Controls.ListBox();
            this.btclone = new Avalonia.Controls.Button();
            this.groupBox1 = new SimPe.Scenegraph.Compat.GroupBox();
            this.pb = new SimPe.Scenegraph.Compat.PictureBox();
            this.rbColor = new Avalonia.Controls.RadioButton();
            this.rbClone = new Avalonia.Controls.RadioButton();
            this.tabControl1 = new Avalonia.Controls.TabControl();
            this.tClone = new Avalonia.Controls.TabItem();
            this.cbanim = new Avalonia.Controls.CheckBox();
            this.cbwallmask = new Avalonia.Controls.CheckBox();
            this.cbparent = new Avalonia.Controls.CheckBox();
            this.cbclean = new Avalonia.Controls.CheckBox();
            this.cbfix = new Avalonia.Controls.CheckBox();
            this.cbdefault = new Avalonia.Controls.CheckBox();
            this.cbgid = new Avalonia.Controls.CheckBox();
            this.tColor = new Avalonia.Controls.TabItem();
            this.cbColorExt = new Avalonia.Controls.CheckBox();
            this.cbColor = new Avalonia.Controls.CheckBox();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.tbseek = new Avalonia.Controls.TextBox();
            this.tabControl2 = new Avalonia.Controls.TabControl();
            this.tabPage1 = new Avalonia.Controls.TabItem();
            this.tabPage3 = new Avalonia.Controls.TabItem();
            this.tv = new SimPe.Scenegraph.Compat.TreeView();
            this.ilist = new SimPe.Scenegraph.Compat.ImageList();
            this.tabPage2 = new Avalonia.Controls.TabItem();
            this.button1 = new Avalonia.Controls.Button();
            this.tbflname = new Avalonia.Controls.TextBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            // 
            // lbobj
            // 
            this.lbobj.SelectionChanged += (s, e) => this.Select(s, EventArgs.Empty);
            // 
            // btclone
            // 
            this.btclone.Content = "Start";
            this.btclone.Click += (s, e) => this.Start(s, e);
            // 
            // groupBox1
            // 
            this.groupBox1.Text = "Settings";
            // 
            // pb
            // 
            // 
            // rbColor
            // 
            this.rbColor.IsChecked = true;
            this.rbColor.Content = "Colour Options";
            this.rbColor.IsCheckedChanged += (s, e) => this.rbColor_CheckedChanged(s, EventArgs.Empty);
            // 
            // rbClone
            // 
            this.rbClone.Content = "Clone";
            this.rbClone.IsCheckedChanged += (s, e) => this.rbClone_CheckedChanged(s, EventArgs.Empty);
            // 
            // tabControl1
            // 
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectionChanged += (s, e) => this.TabChanged(s, EventArgs.Empty);
            // 
            // tClone
            // 
            this.tClone.Header = "Clone Settings";
            // 
            // cbanim
            // 
            this.cbanim.Content = "Pull Animations";
            // 
            // cbwallmask
            // 
            this.cbwallmask.IsChecked = true;
            this.cbwallmask.Content = "Pull Wallmasks (as described by Numenor)";
            // 
            // cbparent
            // 
            this.cbparent.Content = "Create a stand-alone object";
            this.cbparent.IsCheckedChanged += (s, e) => this.cbfix_CheckedChanged(s, EventArgs.Empty);
            // 
            // cbclean
            // 
            this.cbclean.IsChecked = true;
            this.cbclean.Content = "Remove useless Files (sug. by.  Numenor)";
            // 
            // cbfix
            // 
            this.cbfix.IsChecked = true;
            this.cbfix.Content = "Fix Cloned Files (sug. by.  wes_h)";
            this.cbfix.IsCheckedChanged += (s, e) => this.cbfix_CheckedChanged(s, EventArgs.Empty);
            // 
            // cbdefault
            // 
            this.cbdefault.IsChecked = true;
            this.cbdefault.Content = "Pull only default Colour";
            this.cbdefault.IsCheckedChanged += (s, e) => this.cbRCOLClone_CheckedChanged(s, EventArgs.Empty);
            // 
            // cbgid
            // 
            this.cbgid.IsChecked = true;
            this.cbgid.Content = "Set Custom Group ID (0x1c050000)";
            // 
            // tColor
            // 
            this.tColor.Header = "Colour Options";
            // 
            // cbColorExt
            // 
            this.cbColorExt.IsChecked = true;
            this.cbColorExt.Content = "Create Colour Extension Package";
            // 
            // cbColor
            // 
            this.cbColor.Content = "Enable All Colour Options";
            // 
            // sfd
            // 
            this.sfd.Filter = "Package File (*.package)|*.package|All Files (*.*)|*.*";
            this.sfd.Title = "Alternative Colour Pacakge";
            // 
            // tbseek
            // 
            this.tbseek.TextChanged += (s, e) => this.SeekItem(s, EventArgs.Empty);
            // 
            // tabControl2
            // 
            this.tabControl2.SelectedIndex = 1;
            this.tabControl2.SelectionChanged += (s, e) => this.TabChange(s, EventArgs.Empty);
            // 
            // tabPage1
            // 
            this.tabPage1.Header = "Object Listing";
            // 
            // tabPage3
            // 
            this.tabPage3.Header = "Grouped Objects";
            // 
            // tv
            // 
            this.tv.HideSelection = false;
            this.tv.ImageIndex = 0;
            this.tv.ImageList = this.ilist;
            this.tv.SelectedImageIndex = 0;
            this.tv.AfterSelect += (s, e) => this.SelectTv(s, e);
            // 
            // ilist
            // 
            this.ilist.ColorDepth = SimPe.Scenegraph.Compat.ColorDepth.Depth24Bit;
            this.ilist.ImageSize = new System.Drawing.Size(16, 16);
            // 
            // tabPage2
            // 
            this.tabPage2.Header = "Load Object";
            // 
            // button1
            // 
            this.button1.Content = "Browse...";
            this.button1.Click += (s, e) => this.button1_Click(s, e);
            // 
            // tbflname
            // 
            this.tbflname.IsReadOnly = true;
            // 
            // ofd
            // 
            this.ofd.Filter = "Package File (*.package)|*.package|All Files (*.*)|*.*";
            // 
            // Workshop
            // 
            this.Title = "Object Workshop (biggest thanks to RGiles and Numenor)";
		}
		#endregion

		private Avalonia.Controls.ListBox lbobj;
		private Avalonia.Controls.Button btclone;

		SimPe.Packages.GeneratableFile package;
		private SimPe.Scenegraph.Compat.GroupBox groupBox1;
		private Avalonia.Controls.CheckBox cbgid;
		private Avalonia.Controls.TabControl tabControl1;
		private Avalonia.Controls.TabItem tClone;
		private Avalonia.Controls.TabItem tColor;
		private Avalonia.Controls.RadioButton rbClone;
		private Avalonia.Controls.RadioButton rbColor;
		private Avalonia.Controls.CheckBox cbColor;
		private Avalonia.Controls.CheckBox cbColorExt;
		private System.Windows.Forms.SaveFileDialog sfd;
		private Avalonia.Controls.TextBox tbseek;
		private SimPe.Scenegraph.Compat.PictureBox pb;
		IProviderRegistry prov;
		SimPe.Interfaces.Files.IPackageFile simpe_pkg;
		public Interfaces.Files.IPackageFile Execute(IProviderRegistry prov, SimPe.Interfaces.Files.IPackageFile simpe_pkg) 		
		{			 
			this.prov = prov;
			this.simpe_pkg = simpe_pkg;

			prov.OpcodeProvider.LoadPackage();
			if (prov.OpcodeProvider.BasePackage == null) 
			{
				System.Console.WriteLine("The objects.package was not found. Please set the Path to your Sims 2 installation in the Extra->Options... Dialog.");
				return null;
			}
			package = null;

			if (!Helper.XmlRegistry.LoadOWFast) BuildListing();
			else tabControl2.SelectedIndex = 2;
			RemoteControl.ShowSubForm(this);

			WaitingScreen.Stop(this);
			return package;
		}
	

		private void Select(object sender, System.EventArgs e)
		{
			if (tbseek.Tag != null) return;
			btclone.IsEnabled = false;
			if (lbobj.SelectedIndex<0) return;
			btclone.IsEnabled = true;
			IAlias a = (IAlias)lbobj.Items[lbobj.SelectedIndex];
			tbseek.Tag = true;
			if (sender!=null) tbseek.Text = a.Name;
			tbseek.Tag = null;

			if (a.Tag[2]==null) pb.Image = null;
			else pb.Image = GetThumbnail((uint)a.Tag[1], (string)a.Tag[2]);
		}

		private void Start(object sender, System.EventArgs e)
		{
			try 
			{
				IAlias a = new Data.Alias(0, "");
				Interfaces.Files.IPackedFileDescriptor pfd = null;
				uint localgroup = Data.MetaData.LOCAL_GROUP;
				
				if ((lbobj.SelectedIndex>=0) && (tabControl2.SelectedIndex==0))
				{
					a = (IAlias)lbobj.Items[lbobj.SelectedIndex];
					pfd = (Interfaces.Files.IPackedFileDescriptor)a.Tag[0];
					localgroup = (uint)a.Tag[1];
				}

				if ((tv.SelectedNode!=null) && (tabControl2.SelectedIndex==1))
				{
					if (tv.SelectedNode.Tag!=null) 
					{
						a = (IAlias)tv.SelectedNode.Tag;
						pfd = (Interfaces.Files.IPackedFileDescriptor)a.Tag[0];
						localgroup = (uint)a.Tag[1];
					} else return;
				}

                if ((this.rbClone.IsChecked == true))
                {
                    if (tabControl2.SelectedIndex < 2)
                    {
                        WaitingScreen.Wait();
                        try { this.RecolorClone(pfd, localgroup, (this.cbdefault.IsChecked == true), (this.cbwallmask.IsChecked == true), (this.cbanim.IsChecked == true)); }
                        finally { WaitingScreen.Stop(this); }
                    }


                    FixObject fo = new FixObject(package, FixVersion.UniversityReady, true);
                    System.Collections.Hashtable map = null;

                    if ((this.cbfix.IsChecked == true))
                    {
                        map = fo.GetNameMap(true);
                        if (map == null) return;
                    }


                    if ((this.cbfix.IsChecked == true))
                    {
                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            WaitingScreen.Wait();
                            try
                            {
                                package.FileName = sfd.FileName;
                                fo.Fix(map, true);

                                if ((cbclean.IsChecked == true)) fo.CleanUp();
                                package.Save();


                            }
                            finally { WaitingScreen.Stop(this); }
                        }
                        else
                        {
                            package = null;
                        }
                    }

                    if (((this.cbgid.IsChecked == true)) && (package != null))
                    {
                        WaitingScreen.Wait();
                        try
                        {
                            fo.FixGroup();
                            if ((this.cbfix.IsChecked == true)) package.Save();

                        }
                        finally { WaitingScreen.Stop(this); }
                    }
                }
                else //if Recolor
                {
                    if ((this.cbColor.IsChecked == true)) cbColorExt.IsChecked = false;

                    if (tabControl2.SelectedIndex == 0)
                    {
                        foreach (IAlias ia in lbobj.SelectedItems)
                        {
                            pfd = (Interfaces.Files.IPackedFileDescriptor)ia.Tag[0];
                            localgroup = (uint)ia.Tag[1];
                            package = null;
                            ReColor(pfd, localgroup);
                        }
                    }
                    else if (tabControl2.SelectedIndex == 1)
                    {
                        package = null;
                        ReColor(pfd, localgroup);
                    }
                    else
                    {
                        ReColor(null, Data.MetaData.LOCAL_GROUP);
                    }
                }

				Close();
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			}
			finally 
			{
				WaitingScreen.Stop(this);			
				this.Cursor = null;
			}
		}		

		#region Thumbnails
		/// <summary>
		/// Returns the Instance Number for the assigned Thumbnail
		/// </summary>
		/// <param name="group">The Group of the Object</param>
		/// <param name="modelname">The Name of teh Model (inst 0x86)</param>
		/// <returns>Instance of the Thumbnail</returns>
		public static uint ThumbnailHash(uint group, string modelname) 
		{
			string name = group.ToString()+modelname;
			return (uint)Hashes.ToLong(Hashes.Crc32.ComputeHash(Helper.ToBytes(name.Trim().ToLower())));
		}

		static SimPe.Packages.File thumbs = null;

		/// <summary>
		/// Returns the Thumbnail of an Object
		/// </summary>
		/// <param name="group"></param>
		/// <param name="modelname"></param>
		/// <returns>The Thumbnail</returns>
		public static Image GetThumbnail(uint group, string modelname) 
		{
			return GetThumbnail(group, modelname, null);
		}
		/// <summary>
		/// Returns the Thumbnail of an Object
		/// </summary>
		/// <param name="group"></param>
		/// <param name="modelname"></param>
		/// <returns>The Thumbnail</returns>
		public static Image GetThumbnail(uint group, string modelname, string message) 
		{
			uint inst = ThumbnailHash(group, modelname);
			if (thumbs==null) 
			{
                thumbs = SimPe.Packages.File.LoadFromFile(System.IO.Path.Combine(PathProvider.SimSavegameFolder, "Thumbnails\\ObjectThumbnails.package"));
				thumbs.Persistent = true;
			}

			//0x6C2A22C3
			Interfaces.Files.IPackedFileDescriptor[] pfds = thumbs.FindFile(0xAC2950C1, 0, inst);
			if (pfds.Length>0) 
			{
				Interfaces.Files.IPackedFileDescriptor pfd = pfds[0];
				try 
				{
					SimPe.PackedFiles.Wrapper.Picture pic = new SimPe.PackedFiles.Wrapper.Picture();
					pic.ProcessData(pfd, thumbs);
					SkiaSharp.SKBitmap bm = ImageLoader.Preview(pic.Image, WaitingScreen.ImageSize);
					if (WaitingScreen.Running) WaitingScreen.Update((System.Drawing.Bitmap)null, message);
					return pic.Image;
				}
				catch(Exception){}
			}
			return null;
		}
		#endregion

		//Returns all MMAT Files in a Package containing a reference to the MATD
		protected static Interfaces.Files.IPackedFileDescriptor[] FindMMAT(SimPe.Plugin.Rcol matd, string filename) 
		{
			SimPe.Packages.File pkg = SimPe.Packages.File.LoadFromFile(filename);

			ArrayList list = new ArrayList();
			Interfaces.Files.IPackedFileDescriptor[] pfds = pkg.FindFiles(0x4C697E5A);

			string name = matd.FileName.Trim().ToLower();

			foreach (Interfaces.Files.IPackedFileDescriptor pfd in pfds)
			{
				pfd.UserData = pkg.Read(pfd).UncompressedData;
				SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
				cpf.ProcessData(pfd, pkg);

				string mmatname = cpf.GetItem("name").StringValue.Trim().ToLower()+"_txmt";
				if (mmatname==name) list.Add(pfd);
			}

			pfds = new Interfaces.Files.IPackedFileDescriptor[list.Count];
			list.CopyTo(pfds);

			return pfds;
		}

		
		//SimPe.Packages.File[] objpkgs = null;
		/// <summary>
		/// Reads all Data from the Objects.package blonging to the same group as the passed pfd
		/// </summary>
		/// <param name="pfd">Desciptor for one of files belonging to the Object (Name Map)</param>
		/// <param name="objpkg">The Object Package you wanna process</param>
		/// <param name="package">The package that should get the Files</param>
		/// <returns>The Modlename of that Object or null if none</returns>
		public static string[] BaseClone(Interfaces.Files.IPackedFileDescriptor pfd, uint localgroup, SimPe.Packages.File package) 
		{
			//Get the Base Object Data from the Objects.package File
			
			Interfaces.Scenegraph.IScenegraphFileIndexItem[] files = FileTable.FileIndex.FindFileByGroup(localgroup);

			ArrayList list = new ArrayList();
			foreach (Interfaces.Scenegraph.IScenegraphFileIndexItem item in files) 
			{
				Interfaces.Files.IPackedFile file = item.Package.Read(item.FileDescriptor);

				SimPe.Packages.PackedFileDescriptor npfd = new SimPe.Packages.PackedFileDescriptor();

				npfd.UserData = file.UncompressedData;
				npfd.Group = item.FileDescriptor.Group;
				npfd.Instance = item.FileDescriptor.Instance;
				npfd.SubType = item.FileDescriptor.SubType;
				npfd.Type = item.FileDescriptor.Type;

				if (package.FindFile(npfd)==null)
					package.Add(npfd);

				if ((npfd.Instance == 0x85) && (npfd.Type == Data.MetaData.STRING_FILE)) 
				{
					SimPe.PackedFiles.Wrapper.Str str = new SimPe.PackedFiles.Wrapper.Str();
					str.ProcessData(npfd, item.Package);
					SimPe.PackedFiles.Wrapper.StrItemList items = str.LanguageItems(1);
					for (int i=1; i<items.Length; i++) list.Add(items[i].Title);
					//if (items.Length>1) refname = items[1].Title;
				}
			}

			string[] refname = new string[list.Count];
			list.CopyTo(refname);

			return refname;
		}


		/// <summary>
		/// Clone an object based on way Files are linked
		/// </summary>
		/// <param name="pfd"></param>
		/// <param name="localgroup"></param>
		/// <param name="onlydefault"></param>
		protected void RecolorClone(Interfaces.Files.IPackedFileDescriptor pfd, uint localgroup, bool onlydefault, bool wallmask, bool anim) 
		{
			package = SimPe.Packages.GeneratableFile.LoadFromStream((System.IO.BinaryReader)null);

			//Get the Base Object Data from the Objects.package File
			string[] modelname = BaseClone(pfd, localgroup, package);
			ObjectCloner objclone = new ObjectCloner(package);
			ArrayList exclude = new ArrayList();

			

			//allways for recolors
			if ((this.rbColor.IsChecked == true)) 
			{
				exclude.Add("stdMatEnvCubeTextureName");
				exclude.Add("TXTR");
			} 
			else 
			{
				exclude.Add("tsMaterialsMeshName");
				

				//for clones only when cbparent is not checked
				//if (!(this.cbparent.IsChecked == true)) 
				{					
					exclude.Add("TXTR");
					exclude.Add("stdMatEnvCubeTextureName");					
				} 
			}

			//do the recolor
			objclone.Setup.OnlyDefaultMmats = onlydefault;
			objclone.Setup.UpdateMmatGuids = onlydefault;
			objclone.Setup.IncludeWallmask = wallmask;
			objclone.Setup.IncludeAnimationResources = anim;
			objclone.RcolModelClone(modelname, exclude);

			//for clones only when cbparent is checked
			if (((this.cbparent.IsChecked == true)) && (!(this.rbColor.IsChecked == true))) 
			{
				string[] names = Scenegraph.LoadParentModelNames(package, true);
				SimPe.Packages.File pkg = SimPe.Packages.File.LoadFromFile(null);

				ObjectCloner pobj = new ObjectCloner(pkg);
				pobj.Setup.OnlyDefaultMmats = onlydefault;
				pobj.Setup.UpdateMmatGuids = onlydefault;
				pobj.Setup.IncludeWallmask = wallmask;
				pobj.Setup.IncludeAnimationResources = anim;
				pobj.RcolModelClone(names, exclude);
				pobj.AddParentFiles(modelname, package);				
			}

			//for clones only when cbparent is not checked
			if ((!(this.cbparent.IsChecked == true)) && (!(this.rbColor.IsChecked == true))) 
			{
				string[] modelnames = modelname;
				if (!(cbclean.IsChecked == true)) modelnames = null;
				objclone.RemoveSubsetReferences(Scenegraph.GetParentSubsets(package), modelnames);
			}
		}

        protected void ReColor(Interfaces.Files.IPackedFileDescriptor pfd, uint localgroup)
        {
            // we need packages in the Gmaes and the Download Folder

            if (SimPe.PathProvider.Global.EPInstalled < 16 && ((!System.IO.File.Exists(ScenegraphHelper.GMND_PACKAGE)) || (!System.IO.File.Exists(ScenegraphHelper.MMAT_PACKAGE))) && (!(this.cbColor.IsChecked == true)))
            {
                if (SimPe.Message.Show(Localization.Manager.GetString("OW_Warning"), "Warning", MessageBoxButtons.YesNo) == SimPe.DialogResult.No) return;
            }

            if ((this.cbColorExt.IsChecked == true)) if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //create a Cloned Object to get all needed Files for the Process
            bool old = (cbgid.IsChecked == true);
            cbgid.IsChecked = false;
            WaitingScreen.Wait();
            try
            {
                WaitingScreen.UpdateMessage("Collecting needed Files");
                if ((package == null) && (pfd != null)) RecolorClone(pfd, localgroup, false, false, false);

                cbgid.IsChecked = old;

                if ((this.cbColor.IsChecked == true))
                {
                    ObjectRecolor or = new ObjectRecolor(package);
                    or.EnableColorOptions();
                    or.LoadReferencedMATDs();

                    //load all Pending Textures
                    ObjectCloner oc = new ObjectCloner(package);
                }

                SimPe.Packages.GeneratableFile dn_pkg = null;
                if (System.IO.File.Exists(ScenegraphHelper.GMND_PACKAGE)) dn_pkg = SimPe.Packages.GeneratableFile.LoadFromFile(ScenegraphHelper.GMND_PACKAGE);
                else dn_pkg = SimPe.Packages.GeneratableFile.LoadFromStream((System.IO.BinaryReader)null);

                SimPe.Packages.GeneratableFile gm_pkg = null;
                if (System.IO.File.Exists(ScenegraphHelper.MMAT_PACKAGE)) gm_pkg = SimPe.Packages.GeneratableFile.LoadFromFile(ScenegraphHelper.MMAT_PACKAGE);
                else gm_pkg = SimPe.Packages.GeneratableFile.LoadFromStream((System.IO.BinaryReader)null);

                SimPe.Packages.GeneratableFile npackage = SimPe.Packages.GeneratableFile.LoadFromStream((System.IO.BinaryReader)null);
                //Create the Templae for an additional MMAT
                if ((this.cbColorExt.IsChecked == true))
                {

                    npackage.FileName = sfd.FileName;

                    ColorOptions cs = new ColorOptions(package);
                    cs.Create(npackage);

                    npackage.Save();
                    package = npackage;
                }
                if (package != npackage) package = null;
            }
            finally { WaitingScreen.Stop(this); }
        }

		private void rbClone_CheckedChanged(object sender, System.EventArgs e)
		{
			if (tabControl1.Tag!=null) return;
			tabControl1.Tag = true;
			tabControl1.SelectedIndex = 0;
			tabControl1.Tag = null;
		}

		private void rbColor_CheckedChanged(object sender, System.EventArgs e)
		{
			if (tabControl1.Tag!=null) return;
			tabControl1.Tag = true;
			tabControl1.SelectedIndex = 1;
			tabControl1.Tag = null;
		}

		private void SeekItem(object sender, System.EventArgs e)
		{
			if (tbseek.Tag!=null) return;

			tbseek.Tag = true;
			try 
			{
				string name = tbseek.Text.TrimStart().ToLower();
				if (lbobj.SelectionMode != SelectionMode.Single)				for (int i=0; i<lbobj.Items.Count; i++)
				{
					IAlias a = (IAlias)lbobj.Items[i];
					if (a.Name!=null) 
					{
						if (a.Name.TrimStart().ToLower().StartsWith(name))
						{
							tbseek.Text = a.Name.TrimStart();
							tbseek.SelectionStart = name.Length;
							lbobj.SelectedIndex = i;
							break;
						}

						if (a.Name.TrimStart().ToLower().StartsWith("* "+name))
						{
							tbseek.Text = a.Name.TrimStart();
							tbseek.SelectionStart = name.Length+2;
							lbobj.SelectedIndex = i;
							break;
						}
					}
				}
			} 
			catch (Exception) {}
			finally 
			{
				tbseek.Tag = null;
				this.Select(null, null);
			}
		}

		private void TabChanged(object sender, System.EventArgs e)
		{
			if (tabControl1.Tag!=null) return;

			tabControl1.Tag = true;
			if (this.tabControl1.SelectedIndex==0) rbClone.IsChecked = true;
			else rbColor.IsChecked = true;
			tabControl1.Tag = null;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (ofd.ShowDialog()==System.Windows.Forms.DialogResult.OK) 
			{
				package = SimPe.Packages.GeneratableFile.LoadFromFile(ofd.FileName);
				tbflname.Text = ofd.FileName;
				this.btclone.IsEnabled = System.IO.File.Exists(tbflname.Text);
			}
		}

		private void cbRCOLClone_CheckedChanged(object sender, System.EventArgs e)
		{
			//cbparent.Enabled = (cbRCOLClone.IsChecked == true);
		}

		private void cbfix_CheckedChanged(object sender, System.EventArgs e)
		{
			cbclean.IsEnabled = ((cbfix.IsChecked == true) || (cbparent.IsChecked == true));
		}

		private void SelectTv(object sender, SimPe.Scenegraph.Compat.TreeViewEventArgs e)
		{
			btclone.IsEnabled = false;
			if (tv.SelectedNode==null) return;
			if (tv.SelectedNode.Tag == null) return;
			btclone.IsEnabled = true;
			IAlias a = (IAlias)tv.SelectedNode.Tag;
			
			if (a.Tag[2]==null) pb.Image = null;
			else pb.Image = GetThumbnail((uint)a.Tag[1], (string)a.Tag[2]);
		}

		private void TabChange(object sender, System.EventArgs e)
		{
			if (tabControl2.SelectedIndex==0) {BuildListing(); this.Select(null, null);}
			else if (tabControl2.SelectedIndex==1) {BuildListing(); this.SelectTv(null, null);}
			else this.btclone.IsEnabled = System.IO.File.Exists(tbflname.Text);
		}

		delegate void InvokeTargetLoad(SimPe.Cache.ObjectCacheItem oci, SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem fii, Alias a);
		delegate void InvokeTargetFinish(object sender, EventArgs e);
		
		private void ol_Finished(object sender, EventArgs e)
		{
			Avalonia.Threading.Dispatcher.UIThread.Post(() => invoke_Finished(sender, e));
		}

		private void invoke_Finished(object sender, EventArgs e)
		{
			lbobj.IsEnabled = true;
			this.tv.IsEnabled = true;

			tv.Sorted = true;
			tv.EndUpdate();
			WaitingScreen.Stop(this);			
		}

		private void ol_LoadedItem(SimPe.Cache.ObjectCacheItem oci, SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem fii, Alias a)
		{
			Avalonia.Threading.Dispatcher.UIThread.Post(() => invoke_LoadedItem(oci, fii, a));
		}

		private void invoke_LoadedItem(SimPe.Cache.ObjectCacheItem oci, SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem fii, Alias a)
		{
			if (oci.ObjectVersion == SimPe.Cache.ObjectCacheItemVersions.DockableOW)
                PutItemToTree(a, oci.Thumbnail, (SimPe.Data.ObjectTypes)oci.ObjectType, new SimPe.PackedFiles.Wrapper.ObjFunctionSort((oci.ObjectFunctionSort >> 8) & 0xfff), new SimPe.PackedFiles.Wrapper.ObjBuildType(oci.ObjBuildType), oci.FileDescriptor.Filename, oci.FileDescriptor.Group);
			else
                PutItemToTree(a, oci.Thumbnail, (SimPe.Data.ObjectTypes)oci.ObjectType, new SimPe.PackedFiles.Wrapper.ObjFunctionSort(oci.ObjectFunctionSort), new SimPe.PackedFiles.Wrapper.ObjBuildType(oci.ObjBuildType), oci.FileDescriptor.Filename, oci.FileDescriptor.Group);

			if (oci.Thumbnail!=null) a.Name = "* "+a.Name;
			lbobj.Items.Add(a);
		}
	}
}
