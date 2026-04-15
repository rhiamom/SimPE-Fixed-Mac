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
using Avalonia.Controls;
using Image = System.Drawing.Image;
using Avalonia.Layout;
using SimPe.Scenegraph.Compat;
using SimPe;
using SimPe.Data;
using SimPe.Interfaces;
using Button    = Avalonia.Controls.Button;
using CheckBox  = Avalonia.Controls.CheckBox;
using Panel     = SimPe.Scenegraph.Compat.PanelCompat;
using PictureBox = SimPe.Scenegraph.Compat.PictureBox;
using TextBox   = SimPe.Scenegraph.Compat.TextBoxCompat;
using ListBox   = SimPe.Scenegraph.Compat.ListBoxCompat;


namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for SkinWorkshop.
	/// </summary>
	public class SkinWorkshop : Avalonia.Controls.Window
    {
		private SimPe.Scenegraph.Compat.OpenFileDialogCompat ofd;
        private CheckBox cbfix;
        private System.ComponentModel.IContainer components;
        private Panel panel1;
        private Panel taskBox2;
        private Panel taskBox1;
        private ListBox lbobj;
        private Button btclone;
        private CheckBox cbgid;
        private SimPe.Scenegraph.Compat.SaveFileDialogCompat sfd;
        private TextBox tbseek;
        private PictureBox pb;

        SimPe.Packages.GeneratableFile package;
        SimPe.Interfaces.Files.IPackageFile simpe_pkg;

		public SkinWorkshop()
		{
            InitializeComponent();
            ThemeManager tm = ThemeManager.Global.CreateChild();
            tm.AddControl(this.panel1);
            tm.AddControl(this.taskBox1);
            tm.AddControl(this.taskBox2);
			btclone.IsEnabled = false;
            lbobj.SelectionMode = Avalonia.Controls.SelectionMode.Single;
		}

		#region Cache Handling		
		SimPe.Cache.ObjectCacheFile cachefile;
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
			cachefile = new SimPe.Cache.ObjectCacheFile();
		
			if (!Helper.XmlRegistry.UseCache) return;
            if (WaitingScreen.Running) WaitingScreen.UpdateMessage("Loading Cache");
			try 
			{
				cachefile.Load(CacheFileName);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage("", ex);
			}			

			cachefile.LoadObjects();
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

        void BuildListing(uint type)
        {
            //build Object List
            if (lbobj.Items.Count == 0)
            {
                WaitingScreen.Wait();
                try
                {
                    LoadCachIndex();
                    lbobj.BeginUpdate();
                    lbobj.Items.Clear();
                    FileTable.FileIndex.Load();
                    Interfaces.Scenegraph.IScenegraphFileIndexItem[] nrefitems = FileTable.FileIndex.Sort(FileTable.FileIndex.FindFile(type, true));
                    WaitingScreen.UpdateMessage("Loading Items");
                    foreach (Interfaces.Scenegraph.IScenegraphFileIndexItem nrefitem in nrefitems)
                    {
                        if (nrefitem.LocalGroup == Data.MetaData.LOCAL_GROUP) continue;
                        Interfaces.Scenegraph.IScenegraphFileIndexItem[] cacheitems = cachefile.FileIndex.FindFile(nrefitem.FileDescriptor, nrefitem.Package);
                        //find the correct File
                        int cindex = -1;
                        string pname = nrefitem.Package.FileName.Trim().ToLower();
                        for (int i = 0; i < cacheitems.Length; i++)
                        {
                            Interfaces.Scenegraph.IScenegraphFileIndexItem citem = cacheitems[i];

                            if (citem.FileDescriptor.Filename.Trim().ToLower() == pname)
                            {
                                cindex = i;
                                break;
                            }
                        }

                        if (cindex != -1) //found in the cache
                        {
                            SimPe.Cache.ObjectCacheItem oci = (SimPe.Cache.ObjectCacheItem)cacheitems[cindex].FileDescriptor.Tag;
                            if (oci.Name.Length < 3) continue;
                            if (!oci.Useable) continue;
                            Data.Alias a = new Data.Alias(oci.FileDescriptor.Instance, oci.Name);
                            object[] o = new object[3];
                            o[0] = nrefitem.FileDescriptor;
                            o[1] = nrefitem.LocalGroup;
                            o[2] = nrefitem.FileDescriptor.Instance;
                            a.Tag = o;
                            if (Helper.XmlRegistry.ShowObjdNames) a.Name = oci.ObjectFileName;
                            else a.Name = oci.Name;
                            object img = oci.Thumbnail;
                            lbobj.Items.Add(a);
                        }
                        else //not found in chache 
                        {
                            try
                            {
                                SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
                                nrefitem.FileDescriptor.UserData = nrefitem.Package.Read(nrefitem.FileDescriptor).UncompressedData;
                                cpf.ProcessData(nrefitem);
                                if (cpf.GetItem("name").StringValue.Length < 3) continue;

                                SimPe.Cache.ObjectCacheItem oci = new SimPe.Cache.ObjectCacheItem();
                                oci.FileDescriptor = nrefitem.FileDescriptor;
                                oci.LocalGroup = nrefitem.LocalGroup;
                                oci.ObjectType = ObjectTypes.Outfit;
                                oci.ObjectFileName = cpf.GetItem("name").StringValue;
                                oci.Useable = true;
                                oci.Class = SimPe.Cache.ObjectClass.Skin;

                                Data.Alias a = new Data.Alias(nrefitem.FileDescriptor.Instance, cpf.GetItem("name").StringValue);
                                object[] o = new object[3];
                                o[0] = nrefitem.FileDescriptor;
                                o[1] = nrefitem.LocalGroup;
                                o[2] = nrefitem.FileDescriptor.Instance;

                                a.Name = cpf.GetItem("name").StringValue;

                                a.Tag = o;
                                // Image img = GetFumbnail(nrefitem.FileDescriptor.Group, nrefitem.FileDescriptor.SubType, nrefitem.FileDescriptor.Instance);

                                //create a cache Item
                                cachechg = true;
                                oci.Name = a.Name;
                                oci.ModelName = "";
                                oci.Thumbnail = null;

                                cachefile.AddItem(oci, nrefitem.Package.FileName);
                                lbobj.Items.Add(a);
                            }
                            catch { }
                        } // if not in cache
                    } //foreach txt

                    SaveCacheIndex();
                    WaitingScreen.UpdateMessage("Sorting Items");
                    lbobj.EndUpdate();
                }
                finally { WaitingScreen.Stop(this); }
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

		#region Avalonia layout
		private void InitializeComponent()
		{
            this.Width  = 795;
            this.Height = 518;
            this.MinWidth  = 817;
            this.MinHeight = 480;
            this.WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterOwner;
            this.Title = "Skin Workshop (biggest thanks to RGiles and Numenor)";

            this.sfd = new SimPe.Scenegraph.Compat.SaveFileDialogCompat
            {
                Filter = "Package File (*.package)|*.package|All Files (*.*)|*.*",
                Title  = "Alternative Colour Package"
            };
            this.ofd = new SimPe.Scenegraph.Compat.OpenFileDialogCompat
            {
                Filter = "Package File (*.package)|*.package|All Files (*.*)|*.*"
            };

            // ── Search box + list ─────────────────────────────────────────────────
            this.tbseek = new TextBox();
            this.tbseek.TextChanged += this.SeekItem;

            this.lbobj = new ListBox();
            this.lbobj.SelectionMode = Avalonia.Controls.SelectionMode.Multiple;
            this.lbobj.SelectedIndexChanged += this.Select;

            this.taskBox1 = new Panel();
            var task1inner = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical, Spacing = 2 };
            task1inner.Children.Add(this.tbseek);
            task1inner.Children.Add(this.lbobj);
            this.taskBox1.Children.Add(task1inner);

            // ── Options + button ──────────────────────────────────────────────────
            this.pb = new PictureBox { Width = 201, Height = 192 };
            this.cbgid  = new CheckBox { Content = "Set Custom Group ID (0x1c050000)" };
            this.cbfix  = new CheckBox { Content = "Fix Cloned Files (sug. by wes_h)" };
            this.btclone = new Button  { Content = "Start" };
            this.btclone.Click += (s, e) => Start(s, e);

            this.taskBox2 = new Panel();
            var task2inner = new Avalonia.Controls.Grid();
            task2inner.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition(Avalonia.Controls.GridLength.Auto));
            task2inner.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition(1, Avalonia.Controls.GridUnitType.Star));
            var task2opts = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical, Spacing = 4 };
            task2opts.Children.Add(this.cbgid);
            task2opts.Children.Add(this.cbfix);
            var task2right = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical, Spacing = 4 };
            task2right.Children.Add(task2opts);
            task2right.Children.Add(this.btclone);
            Avalonia.Controls.Grid.SetColumn(this.pb, 0);
            Avalonia.Controls.Grid.SetColumn(task2right, 1);
            task2inner.Children.Add(this.pb);
            task2inner.Children.Add(task2right);
            this.taskBox2.Children.Add(task2inner);

            // ── Root layout ───────────────────────────────────────────────────────
            this.panel1 = new Panel();
            var rootStack = new Avalonia.Controls.Grid();
            rootStack.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(1, Avalonia.Controls.GridUnitType.Star));
            rootStack.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            Avalonia.Controls.Grid.SetRow(this.taskBox1, 0);
            Avalonia.Controls.Grid.SetRow(this.taskBox2, 1);
            rootStack.Children.Add(this.taskBox1);
            rootStack.Children.Add(this.taskBox2);
            this.panel1.Children.Add(rootStack);

            this.Content = this.panel1;
		}
		#endregion
		public Interfaces.Files.IPackageFile Execute(SimPe.Interfaces.Files.IPackageFile simpe_pkg) 		
		{			 			
			this.simpe_pkg = simpe_pkg;

            // WaitingScreen.Wait();
			FileTable.FileIndex.Load();
			package = null;

			BuildListing();			
			RemoteControl.ShowSubForm(this);

			WaitingScreen.Stop(this);
			return package;
		}

		void BuildListing() 
		{
			BuildListing(Data.MetaData.GZPS);
		}
	

		private void Select(object sender, System.EventArgs e) // Fuck
		{
			if (tbseek.Tag != null) return;
			btclone.IsEnabled = false;
			if (lbobj.SelectedIndex<0) return;
			btclone.IsEnabled = true;

			IAlias a = (IAlias)lbobj.Items[lbobj.SelectedIndex];
			tbseek.Tag = true;
			if (sender!=null) tbseek.Text = a.Name;
			tbseek.Tag = null;
            // pb.Image = GetFumbnail((uint)a.Tag[1], 0, (uint)a.Tag[2]);
		}

        private void Start(object sender, System.EventArgs e)
        {
            try
            {
                this.Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Wait);
                IAlias a = new Data.Alias(0, "");
                Interfaces.Files.IPackedFileDescriptor pfd = null;
                uint localgroup = Data.MetaData.LOCAL_GROUP;

                if (lbobj.SelectedIndex >= 0)
                {
                    a = (IAlias)lbobj.Items[lbobj.SelectedIndex];
                    pfd = (Interfaces.Files.IPackedFileDescriptor)a.Tag[0];
                    localgroup = (uint)a.Tag[1];
                }
                
                WaitingScreen.Wait();
                try { this.RecolorClone(pfd, localgroup, false, false, false); }
                finally { WaitingScreen.Stop(this); }

                FixObject fo = new FixObject(package, FixVersion.UniversityReady, true);
                System.Collections.Hashtable map = null;

                if (this.cbfix.IsChecked == true)
                {
                    map = fo.GetNameMap(true);
                    if (map == null) return;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        WaitingScreen.Wait();
                        try
                        {
                            package.FileName = sfd.FileName;
                            fo.Fix(map, true);
                            package.Save();
                        }
                        finally { WaitingScreen.Stop(this); }

                    }
                    else
                    {
                        package = null;
                    }
                }

                if ((this.cbgid.IsChecked == true) && (package != null))
                {
                    WaitingScreen.Wait();
                    try
                    {
                        fo.FixGroup();
                        if (this.cbfix.IsChecked == true) package.Save();

                    }
                    finally { WaitingScreen.Stop(this); }
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
        // Not used, I can't resolve FumbnailHash yet
        /*
        public static Image GetFumbnail(uint group, uint instahi, uint instans)
        {
            uint inst = FumbnailHash(group, instahi, instans);

            Interfaces.Files.IPackedFileDescriptor pfd = fumbs.FindFile(0x0C7E9A76, instans, 0xFFFFFFFF, inst);
            if (pfd!=null)
            {
                try
                {
                    SimPe.PackedFiles.Wrapper.Picture pic = new SimPe.PackedFiles.Wrapper.Picture();
                    pic.ProcessData(pfd, fumbs);
                    return pic.Image;
                }
                catch (Exception) { }
            }
            return null;
        }

        public static uint FumbnailHash(uint group, uint instahi, uint instans)
        {
            string name = group.ToString() + instahi.ToString() + instans.ToString();
            return (uint)Hashes.ToLong(Hashes.Crc32.ComputeHash(Helper.ToBytes(name.Trim().ToLower())));
        }

        static SimPe.Packages.File fumbs = SimPe.Packages.File.LoadFromFile(System.IO.Path.Combine(PathProvider.SimSavegameFolder, "Thumbnails/CASThumbnails.package"));
        */

        /*
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
                thumbs = SimPe.Packages.File.LoadFromFile(System.IO.Path.Combine(PathProvider.SimSavegameFolder, "Thumbnails/CASThumbnails.package"));
				thumbs.Persistent = true;
			}

            //0x6C2A22C3 0x0C7E9A76
            Interfaces.Files.IPackedFileDescriptor[] pfds = thumbs.FindFile(0x0C7E9A76, 0, inst);
			if (pfds.Length>0) 
			{
				Interfaces.Files.IPackedFileDescriptor pfd = pfds[0];
				try 
				{
					SimPe.PackedFiles.Wrapper.Picture pic = new SimPe.PackedFiles.Wrapper.Picture();
					pic.ProcessData(pfd, thumbs);
					Bitmap bm = (Bitmap)ImageLoader.Preview(pic.Image, WaitingScreen.ImageSize);
					if (WaitingScreen.Running) WaitingScreen.Update(bm, message);
					return pic.Image;
				}
				catch(Exception){}
			}
			return null;
		}
        */
		#endregion
        /*
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
        */
		
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
			
			Interfaces.Scenegraph.IScenegraphFileIndexItem[] files = FileTable.FileIndex.FindFileByGroupAndInstance(localgroup, pfd.LongInstance);

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
			}

			string[] refname = new string[0];

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

            exclude.Add("tsMaterialsMeshName");
            exclude.Add("TXTR");

			//do the recolor
			objclone.Setup.OnlyDefaultMmats = onlydefault;
			objclone.Setup.UpdateMmatGuids = onlydefault;
			objclone.Setup.IncludeWallmask = wallmask;
			objclone.Setup.IncludeAnimationResources = anim;
			objclone.Setup.BaseResource = CloneSettings.BaseResourceType.Ref;
			objclone.RcolModelClone(modelname, exclude);

            string[] modelnames = modelname;
            modelnames = null;
            objclone.RemoveSubsetReferences(Scenegraph.GetParentSubsets(package), modelnames);
		}

        protected void ReColor(Interfaces.Files.IPackedFileDescriptor pfd, uint localgroup)
        {
            if (sfd.ShowDialog() != DialogResult.OK) return;

            //create a Cloned Object to get all needed Files for the Process
            bool old = cbgid.IsChecked == true;
            cbgid.IsChecked = false;
            WaitingScreen.Wait();
            try
            {
                WaitingScreen.UpdateMessage("Collecting needed Files");
                if ((package == null) && (pfd != null)) RecolorClone(pfd, localgroup, false, false, false);
            }
            finally { WaitingScreen.Stop(this); }

            cbgid.IsChecked = old;
            /*
			SimPe.Packages.GeneratableFile dn_pkg = null;
			if (System.IO.File.Exists(ScenegraphHelper.GMND_PACKAGE)) dn_pkg = SimPe.Packages.GeneratableFile.LoadFromFile(ScenegraphHelper.GMND_PACKAGE);
			else dn_pkg = SimPe.Packages.GeneratableFile.LoadFromStream((System.IO.BinaryReader)null);
            */
            /*
			SimPe.Packages.GeneratableFile gm_pkg = null;
			if (System.IO.File.Exists(ScenegraphHelper.MMAT_PACKAGE)) gm_pkg = SimPe.Packages.GeneratableFile.LoadFromFile(ScenegraphHelper.MMAT_PACKAGE);
			else gm_pkg = SimPe.Packages.GeneratableFile.LoadFromStream((System.IO.BinaryReader)null);
			*/
            SimPe.Packages.GeneratableFile npackage = SimPe.Packages.GeneratableFile.LoadFromStream((System.IO.BinaryReader)null);

            npackage.FileName = sfd.FileName;

            ColorOptions cs = new ColorOptions(package);
            cs.Create(npackage);

            npackage.Save();
            package = npackage;

            WaitingScreen.Stop(this);
            if (package != npackage) package = null;
        }

		private void SeekItem(object sender, System.EventArgs e)
		{
			if (tbseek.Tag!=null) return;

			tbseek.Tag = true;
			try 
			{
				string name = tbseek.Text.TrimStart().ToLower();
				if (lbobj.SelectionMode != Avalonia.Controls.SelectionMode.Single) lbobj.ClearSelected();
				for (int i=0; i<lbobj.Items.Count; i++)
				{
					IAlias a = (IAlias)lbobj.Items[i];
					if (a.Name!=null) 
					{
						if (a.Name.TrimStart().ToLower().StartsWith(name))
						{
							tbseek.Text = a.Name.TrimStart();
							tbseek.SelectionStart = name.Length;
							tbseek.SelectionLength = Math.Max(0, tbseek.Text.Length - name.Length);
							lbobj.SelectedIndex = i;
							break;
						}

						if (a.Name.TrimStart().ToLower().StartsWith("* "+name))
						{
							tbseek.Text = a.Name.TrimStart();
							tbseek.SelectionStart = name.Length+2;
							tbseek.SelectionLength = Math.Max(0, tbseek.Text.Length - (name.Length+2));
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
	}
}
