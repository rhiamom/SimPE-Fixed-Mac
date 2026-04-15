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
using System.IO;
using Avalonia.Controls;
using Avalonia.Layout;
using SkiaSharp;
using SimPe.Scenegraph.Compat;
using ImageList   = SimPe.Scenegraph.Compat.ImageList;
using ToolTip     = SimPe.Scenegraph.Compat.ToolTip;
using TabControl      = SimPe.Scenegraph.Compat.TabControlCompat;
using TabPageCompat   = SimPe.Scenegraph.Compat.TabPage;
using Image           = System.Drawing.Image;
using Label       = SimPe.Scenegraph.Compat.LabelCompat;
using PictureBox  = SimPe.Scenegraph.Compat.PictureBox;
using Button      = Avalonia.Controls.Button;
using LinkLabel   = SimPe.Scenegraph.Compat.LinkLabel;
using ListView    = SimPe.Scenegraph.Compat.ListView;
using CheckBox    = Avalonia.Controls.CheckBox;
using Panel       = SimPe.Scenegraph.Compat.PanelCompat;
using ListViewItem = SimPe.Scenegraph.Compat.ListViewItem;
using LinkLabelLinkClickedEventArgs = SimPe.Scenegraph.Compat.LinkLabelLinkClickedEventArgs;
using OpenFileDialog = SimPe.Scenegraph.Compat.OpenFileDialogCompat;
using SaveFileDialog = SimPe.Scenegraph.Compat.SaveFileDialogCompat;
using MessageBox     = SimPe.Scenegraph.Compat.MessageBox;
using MessageBoxButtons = SimPe.Scenegraph.Compat.MessageBoxButtons;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for PhotoStudio.
	/// </summary>
	public class PhotoStudio : Avalonia.Controls.Window
	{
		private ImageList ilist;
		private ToolTip toolTip1;
		private TabControl tabControl1;
		private TabPageCompat tabPage1;
		private Label label1;
		private PictureBox pb;
		private Button btopen;
		private OpenFileDialog ofd;
		private SaveFileDialog sfd;
		private Label lbname;
		private Label lbsize;
		private LinkLabel llcreate;
		private TabPageCompat tabPage2;
		private ListView lv;
		private Label label2;
		private ComboBox cbquality;
		private ListView lvbase;
		private ImageList ibase;
		private PictureBox pbpreview;
		private CheckBox cbprev;
		private CheckBox cbflip;
        private Panel panel1;
		private System.ComponentModel.IContainer components;

        public PhotoStudio()
        {
            InitializeComponent();
            ThemeManager tm = ThemeManager.Global.CreateChild();
            tm.AddControl(this.panel1);

            //load all additional Package Templates
            string[] files = System.IO.Directory.GetFiles(Helper.SimPeDataPath, "*.template");


            if (files.Length == 0)
            {
                SimPe.WaitingScreen.Stop();
                MessageBox.Show("PhotoStudio can't be used because SimPe couldn't\nfind any PhotoStudio Templates in the Data Folder.", "Information", MessageBoxButtons.OK);
            }

            try
            {
                SimPe.WaitingScreen.Wait();

                if (files.Length > 0)
                {
                    foreach (string file in files)
                    {
                        SimPe.Packages.File pkg = SimPe.Packages.File.LoadFromFile(file);
                        PhotoStudioTemplate pst = new PhotoStudioTemplate(pkg);
                        ListViewItem lvi = new ListViewItem(pst.ToString());
                        lvi.ImageIndex = ibase.Images.Count;
                        lvi.Tag = pst;
                        // pst.Texture is System.Drawing.Image; Preview returns SKBitmap — skip preview
                        SimPe.WaitingScreen.UpdateImage(null);
                        ibase.Images.Add((SkiaSharp.SKBitmap)null);
                        lvbase.Items.Add(lvi);
                    }
                }

                if (lvbase.Items.Count > 0) lvbase.Items[0].Selected = true;

                sfd.InitialDirectory = System.IO.Path.Combine(PathProvider.SimSavegameFolder, "Downloads");

                cbquality.SelectedIndex = 0;
                if (System.IO.File.Exists(PathProvider.Global.NvidiaDDSTool))
                {
                    cbquality.Items.Add("Use Nvidia DDS Tools");
                    cbquality.SelectedIndex = cbquality.Items.Count - 1;
                }
            }
            finally
            {
                SimPe.WaitingScreen.UpdateImage(null); SimPe.WaitingScreen.Stop();
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
			this.Title  = "Photo Studio";
			this.Width  = 820;
			this.Height = 560;

			// ── Image lists ───────────────────────────────────────────────────────
			this.ilist = new ImageList { ColorDepth = SimPe.Scenegraph.Compat.ColorDepth.Depth32Bit, ImageSize = new System.Drawing.Size(64, 64) };
			this.ibase = new ImageList { ColorDepth = SimPe.Scenegraph.Compat.ColorDepth.Depth32Bit, ImageSize = new System.Drawing.Size(96, 96) };
			this.toolTip1 = new ToolTip();

			// ── Tab page 1: Images ────────────────────────────────────────────────
			this.lbname = new Label { Text = "No Image" };
			this.lbsize = new Label { Text = "0x0" };
			this.btopen = new Button { Content = "Open..." };
			this.btopen.Click += (s, e) => OpenImage(s, e);

			this.pb = new PictureBox { Size = new System.Drawing.Size(430, 160) };

			var tab1Inner = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical, Spacing = 4, Margin = new Avalonia.Thickness(4) };
			tab1Inner.Children.Add(this.pb);
			tab1Inner.Children.Add(this.lbname);
			tab1Inner.Children.Add(this.lbsize);
			tab1Inner.Children.Add(this.btopen);

			this.tabPage1 = new TabPageCompat { Text = "Images" };
			this.tabPage1.Content = tab1Inner;

			// ── Tab page 2: Sims ──────────────────────────────────────────────────
			this.lv = new ListView();
			this.lv.LargeImageList = this.ilist;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.SelectedIndexChanged += (s, e) => lvbase_SelectedIndexChanged(s, e);

			this.tabPage2 = new TabPageCompat { Text = "Sims" };
			this.tabPage2.Content = this.lv;

			// ── TabControl ────────────────────────────────────────────────────────
			this.tabControl1 = new TabControl();
			this.tabControl1.Items.Add(this.tabPage1);
			this.tabControl1.Items.Add(this.tabPage2);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.SelectionChanged += (s, e) => lvbase_SelectedIndexChanged(s, e);

			// ── Template list (lvbase) ────────────────────────────────────────────
			this.lvbase = new ListView();
			this.lvbase.HideSelection = false;
			this.lvbase.LargeImageList = this.ibase;
			this.lvbase.MultiSelect = false;
			this.lvbase.UseCompatibleStateImageBehavior = false;
			this.lvbase.SelectedIndexChanged += (s, e) => lvbase_SelectedIndexChanged(s, e);

			// ── Preview + quality + flip controls ────────────────────────────────
			this.pbpreview = new PictureBox { Size = new System.Drawing.Size(200, 200) };

			this.cbflip = new CheckBox { Content = "Flip Image:", IsChecked = true };
			this.cbflip.IsCheckedChanged += (s, e) => lvbase_SelectedIndexChanged(s, e);

			this.cbprev = new CheckBox { Content = "Preview" };
			this.cbprev.IsCheckedChanged += (s, e) => lvbase_SelectedIndexChanged(s, e);

			this.label1   = new Label { Text = "Picture Base:" };
			this.label2   = new Label { Text = "Quality:" };
			this.llcreate = new LinkLabel { Text = "create" };
			this.llcreate.LinkClicked += (s, e) => CreateImage(s, e);

			this.cbquality = new ComboBox();
			this.cbquality.Items.Add("High Quality (RAW24)");
			this.cbquality.Items.Add("Compressed Format");

			// ── Dialog stubs ──────────────────────────────────────────────────────
			this.ofd = new OpenFileDialog();
			this.ofd.Filter = "All Image Files (*.jpg;*.bmp;*.gif;*.png)|*.jpg;*.bmp;*.gif;*.png|All Files (*.*)|*.*";
			this.sfd = new SaveFileDialog();
			this.sfd.Filter = "Package File (*.package)|*.package|All Files (*.*)|*.*";

			// ── Right column layout ───────────────────────────────────────────────
			var rightCol = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical, Spacing = 4, Width = 220 };
			rightCol.Children.Add(this.label1);
			rightCol.Children.Add(this.lvbase);
			rightCol.Children.Add(this.cbflip);
			rightCol.Children.Add(this.cbprev);
			rightCol.Children.Add(this.label2);
			rightCol.Children.Add(this.cbquality);
			rightCol.Children.Add(this.llcreate);
			rightCol.Children.Add(this.pbpreview);

			// ── Main grid ─────────────────────────────────────────────────────────
			var mainGrid = new Avalonia.Controls.Grid { Margin = new Avalonia.Thickness(8) };
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(228) });
			Avalonia.Controls.Grid.SetColumn(this.tabControl1, 0);
			Avalonia.Controls.Grid.SetColumn(rightCol, 1);
			mainGrid.Children.Add(this.tabControl1);
			mainGrid.Children.Add(rightCol);

			this.panel1 = new Panel();
			this.panel1.Children.Add(mainGrid);

			this.Content = this.panel1;
		}
		#endregion

		protected void AddImage(SimPe.PackedFiles.Wrapper.SDesc sdesc)
        {

            if (sdesc.HasImage)
			{
                this.ilist.Images.Add(sdesc.Image as SkiaSharp.SKBitmap);
			}
			else
			{
                this.ilist.Images.Add(new SKBitmap(1, 1));
			}
		}

		protected void AddSim(SimPe.PackedFiles.Wrapper.SDesc sdesc)
		{
            if (!sdesc.AvailableCharacterData || sdesc.HasImage == false) return;

			AddImage(sdesc);
			ListViewItem lvi = new ListViewItem();
			lvi.Text = sdesc.SimName +" "+sdesc.SimFamilyName;
			lvi.ImageIndex = ilist.Images.Count -1;
			lvi.Tag = sdesc;


			lv.Items.Add(lvi);
		}

		SimPe.Interfaces.Files.IPackedFileDescriptor pfd;
		SimPe.Interfaces.Files.IPackageFile package;
		public Interfaces.Plugin.IToolResult Execute(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package, Interfaces.IProviderRegistry prov)
		{
			this.Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Wait);

			this.pfd = null;
			this.package = null;

			ilist.Images.Clear();
			lv.Items.Clear();

			if (package!=null)
			{
				Interfaces.Files.IPackedFileDescriptor[] pfds = package.FindFiles(Data.MetaData.SIM_DESCRIPTION_FILE);
				if (pfds.Length>0) WaitingScreen.Wait();
                try
                {
                    foreach (Interfaces.Files.IPackedFileDescriptor spfd in pfds)
                    {
                        PackedFiles.Wrapper.SDesc sdesc = new SimPe.PackedFiles.Wrapper.SDesc(prov.SimNameProvider, prov.SimFamilynameProvider, null);
                        sdesc.ProcessData(spfd, package);

                        WaitingScreen.UpdateImage(null); // ImageLoader.Preview returns SKBitmap; UpdateImage takes System.Drawing.Image — skip
                        AddSim(sdesc);
                    } //foreach
                }
                finally
                {
                    WaitingScreen.UpdateImage(null); WaitingScreen.Stop(this);
                }
			}

			this.Cursor = null;
			RemoteControl.ShowSubForm(this);

			if (this.pfd!=null) pfd = this.pfd;
			if (this.package!=null) package = this.package;
			return new Plugin.ToolResult((this.pfd!=null), (this.package!=null));
		}

		/// <summary>
		/// Returns the selected Format
		/// </summary>
		/// <returns></returns>
		ImageLoader.TxtrFormats SelectedFormat()
		{
			ImageLoader.TxtrFormats format = ImageLoader.TxtrFormats.Raw32Bit;
			if (cbquality.SelectedIndex==1) format = ImageLoader.TxtrFormats.DXT1Format;
			else if (cbquality.SelectedIndex==2) format = ImageLoader.TxtrFormats.DXT1Format;

			return format;
		}

		/// <summary>
		/// builds a preview Image
		/// </summary>
		/// <param name="img">The Image you want to use for the build process</param>
		/// <returns>Preview Image </returns>
		object ShowPreview(Image img)
		{
			if ((cbprev.IsChecked != true) || (img==null) || (lvbase.SelectedItems.Count==0)) return new SKBitmap(1, 1);


			SimPe.Interfaces.Files.IPackageFile pkg = BuildPicture("dummy.package", img, (PhotoStudioTemplate)lvbase.SelectedItems[0].Tag, ImageLoader.TxtrFormats.Raw32Bit, false, false, cbflip.IsChecked == true);
			try
			{
				SimPe.Plugin.Txtr txtr = new Txtr(null, false);

				//load TXTR
				Interfaces.Files.IPackedFileDescriptor[] pfd = pkg.FindFile(((PhotoStudioTemplate)lvbase.SelectedItems[0].Tag).TxtrFile+"_txtr", 0x1C4A276C);
				if (pfd.Length>0)
				{
					txtr.ProcessData(pfd[0], pkg);
				}

				SimPe.Plugin.ImageData id = (SimPe.Plugin.ImageData)txtr.Blocks[0];
				return null; // MipMap.Texture is now SKBitmap; ShowPreview returns System.Drawing.Image — skip
			}
			catch (Exception)
			{
				return new SKBitmap(1, 1);
			}

		}

		Image loadimg = null;
		private async void OpenImage(object sender, System.EventArgs e)
		{
			var files = await StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
			{
				AllowMultiple = false,
				FileTypeFilter = new[]
				{
					new Avalonia.Platform.Storage.FilePickerFileType("Image Files") { Patterns = new[] { "*.jpg", "*.bmp", "*.gif", "*.png" } },
					new Avalonia.Platform.Storage.FilePickerFileType("All Files")   { Patterns = new[] { "*.*" } }
				}
			});
			if (files.Count > 0)
			{
				try
				{
					string fileName = files[0].Path.LocalPath;
					loadimg = Image.FromFile(fileName);
					lbname.Text = System.IO.Path.GetFileName(fileName);
					lbsize.Text = loadimg.Width.ToString() + "x" + loadimg.Height.ToString();
					pb.Image = loadimg; // ImageLoader.Preview returns SKBitmap; pb.Image is System.Drawing.Image — use original
					preview = this.ShowPreview(loadimg);
					pbpreview.Image = preview; // same — skip Preview call
				}
				catch (Exception)
				{
					pb.Image = null;
				}
			}
		}

		static string BuildName(string name, string unique)
		{
			name = Hashes.StripHashFromName(name);
			name = RenameForm.ReplaceOldUnique(name, unique, true);

			return name;
		}

		/// <summary>
		/// Creates a new Picture using the passed Template and the passed Image
		/// </summary>
		/// <param name="filename">FileName for the new package</param>
		/// <param name="img">The Image you want to use</param>
		/// <param name="template">The Template to use</param>
		/// <param name="format">The Format to save the Imag ein</param>
		/// <param name="ddstool">true if you want to use the DDS Tools (if available)</param>
		/// <param name="rename">true, if the Texture should be renamed</param>
		/// <param name="flip">true if the Image should be flipped</param>
		/// <returns>The package with the Recolor</returns>
		protected static SimPe.Packages.GeneratableFile BuildPicture(string filename, Image img, PhotoStudioTemplate template, ImageLoader.TxtrFormats format, bool ddstool, bool rename, bool flip)
		{
			WaitingScreen.Wait();
            try
            {
                SimPe.Plugin.Txtr txtr = new Txtr(null, false);
                SimPe.Plugin.Rcol matd = new GenericRcol(null, false);
                SimPe.PackedFiles.Wrapper.Cpf mmat = new SimPe.PackedFiles.Wrapper.Cpf();

                SimPe.Packages.GeneratableFile pkg = SimPe.Packages.GeneratableFile.LoadFromStream((System.IO.BinaryReader)null);
                if (UserVerification.HaveValidUserId)
                    pkg.Header.Created = UserVerification.UserId;
                pkg.FileName = filename;

                string family = System.Guid.NewGuid().ToString();
                string unique = RenameForm.GetUniqueName();

                SimPe.Packages.GeneratableFile tpkg = SimPe.Packages.GeneratableFile.LoadFromFile(template.Package.FileName);

                //load MMAT
                WaitingScreen.UpdateMessage("Loading Material Override");
                Interfaces.Files.IPackedFileDescriptor pfd = tpkg.FindFile(0x4C697E5A, 0x0, 0xffffffff, template.MmatInstance);
                if (pfd != null)
                {
                    mmat.ProcessData(pfd, tpkg);
                    mmat.GetSaveItem("family").StringValue = family;
                    if (rename) mmat.GetSaveItem("name").StringValue = "##0x1c050000!" + BuildName(template.MatdFile, unique);

                    mmat.SynchronizeUserData();
                    pkg.Add(mmat.FileDescriptor);
                }

                //load MATD
                WaitingScreen.UpdateMessage("Loading Material Definition");
                pfd = tpkg.FindFile(0x49596978, Hashes.SubTypeHash(Hashes.StripHashFromName(template.MatdFile + "_txmt")), 0x1c050000, Hashes.InstanceHash(Hashes.StripHashFromName(template.MatdFile + "_txmt")));
                if (pfd == null) pfd = tpkg.FindFile(0x49596978, Hashes.SubTypeHash(Hashes.StripHashFromName(template.MatdFile + "_txmt")), 0xffffffff, Hashes.InstanceHash(Hashes.StripHashFromName(template.MatdFile + "_txmt")));
                if (pfd != null)
                {
                    matd.ProcessData(pfd, tpkg);
                    if (rename) matd.FileName = "##0x1c050000!" + BuildName(template.MatdFile, unique) + "_txmt";
                    SimPe.Plugin.MaterialDefinition md = (SimPe.Plugin.MaterialDefinition)matd.Blocks[0];
                    if (rename) md.GetProperty("stdMatBaseTextureName").Value = "##0x1c050000!" + BuildName(template.TxtrFile, unique);
                    if (rename) md.Listing[0] = md.GetProperty("stdMatBaseTextureName").Value;

                    matd.FileDescriptor = new Packages.PackedFileDescriptor();
                    matd.FileDescriptor.Type = 0x49596978; //TXMT
                    matd.FileDescriptor.SubType = Hashes.SubTypeHash(Hashes.StripHashFromName(matd.FileName));
                    matd.FileDescriptor.Instance = Hashes.InstanceHash(Hashes.StripHashFromName(matd.FileName));
                    matd.FileDescriptor.Group = 0x1c050000;
                    matd.SynchronizeUserData();
                    pkg.Add(matd.FileDescriptor);
                }

                //load TXTR
                WaitingScreen.UpdateMessage("Loading Texture Image");
                pfd = tpkg.FindFile(0x1C4A276C, Hashes.SubTypeHash(Hashes.StripHashFromName(template.TxtrFile + "_txtr")), 0x1c050000, Hashes.InstanceHash(Hashes.StripHashFromName(template.TxtrFile + "_txtr")));
                if (pfd == null) pfd = tpkg.FindFile(0x1C4A276C, Hashes.SubTypeHash(Hashes.StripHashFromName(template.TxtrFile + "_txtr")), 0xffffffff, Hashes.InstanceHash(Hashes.StripHashFromName(template.TxtrFile + "_txtr")));
                if (pfd != null)
                {
                    txtr.ProcessData(pfd, tpkg);
                    if (rename) txtr.FileName = "##0x1c050000!" + BuildName(template.TxtrFile, unique) + "_txtr";

                    SimPe.Plugin.ImageData id = (SimPe.Plugin.ImageData)txtr.Blocks[0];
                    SimPe.Plugin.MipMapBlock mmp = id.MipMapBlocks[0];
                    SimPe.Plugin.MipMap mm = mmp.MipMaps[mmp.MipMaps.Length - 1];

                    WaitingScreen.UpdateMessage("Updating Image");
                    // MipMap.Texture is now SKBitmap; GDI+ Graphics operations skipped
                    // The complex drawing of img onto mm.Texture is not currently supported without a full SkiaSharp rewrite
                    id.Format = format;

                    txtr.FileDescriptor = new Packages.PackedFileDescriptor();
                    txtr.FileDescriptor.Type = 0x1C4A276C; //TXTR
                    txtr.FileDescriptor.SubType = Hashes.SubTypeHash(Hashes.StripHashFromName(txtr.FileName));
                    txtr.FileDescriptor.Instance = Hashes.InstanceHash(Hashes.StripHashFromName(txtr.FileName));
                    txtr.FileDescriptor.Group = 0x1c050000;
                    txtr.SynchronizeUserData();
                    pkg.Add(txtr.FileDescriptor);
                }

                return pkg;
            }
            finally { WaitingScreen.Stop(); }
		}

		private async void CreateImage(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (lvbase.SelectedItems.Count==0) return;
			Image img = null;

			//get the Image depending on the Active Tab
			if (tabControl1.SelectedIndex==0)
			{
				img = loadimg;
			}
			else if (tabControl1.SelectedIndex==1)
			{
				if (lv.SelectedItems.Count<1) return;

				PackedFiles.Wrapper.SDesc sdesc = (PackedFiles.Wrapper.SDesc)lv.SelectedItems[0].Tag;
				img = sdesc.Image as Image;
			}

			if (img == null) return;

			var saveFiles = await StorageProvider.SaveFilePickerAsync(new Avalonia.Platform.Storage.FilePickerSaveOptions
			{
				FileTypeChoices = new[]
				{
					new Avalonia.Platform.Storage.FilePickerFileType("Package File") { Patterns = new[] { "*.package" } },
					new Avalonia.Platform.Storage.FilePickerFileType("All Files")    { Patterns = new[] { "*.*" } }
				},
				SuggestedStartLocation = await StorageProvider.TryGetFolderFromPathAsync(
					new Uri(System.IO.Path.Combine(PathProvider.SimSavegameFolder, "Downloads")))
			});

			if (saveFiles != null)
			{
				try
				{
					this.Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Wait);
					this.package = BuildPicture(saveFiles.Path.LocalPath, img, (PhotoStudioTemplate)lvbase.SelectedItems[0].Tag, SelectedFormat(), (cbquality.SelectedIndex==2), true, cbflip.IsChecked == true);
					((SimPe.Packages.GeneratableFile)this.package).Save();
					this.Cursor = null;
					Close();
				}
				catch (Exception ex)
				{
					Helper.ExceptionMessage("", ex);
				}
			}
		}

		object preview;
		private void lvbase_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Wait);
			if (tabControl1.SelectedIndex==0)
			{
				preview = this.ShowPreview(loadimg);
			}
			else
			{
				if (lv.SelectedItems.Count>0)
				{
					PackedFiles.Wrapper.SDesc sdesc = (PackedFiles.Wrapper.SDesc)lv.SelectedItems[0].Tag;
					preview = this.ShowPreview(sdesc.Image as Image);
				}
				else
				{
					preview = null;
				}
			}


			pbpreview.Image = preview; // ImageLoader.Preview returns SKBitmap; pbpreview.Image is System.Drawing.Image — use original
			this.Cursor = null;
		}

		private void pbpreview_Click(object sender, System.EventArgs e)
		{
			if (preview==null) return;

			int pw = 256, ph = 256;
			if (preview is SkiaSharp.SKBitmap skPrev) { pw = skPrev.Width; ph = skPrev.Height; }

			var win = new Avalonia.Controls.Window();
			win.Width  = pw;
			win.Height = ph;
			win.Title  = "Preview";

			var previewBox = new PictureBox();
			previewBox.Size  = new System.Drawing.Size(pw, ph);
			previewBox.Image = preview;

			win.Content = previewBox;
			win.Show();
		}
	}
}
