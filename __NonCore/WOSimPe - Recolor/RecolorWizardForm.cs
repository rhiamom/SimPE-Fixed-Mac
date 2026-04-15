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
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using SkiaSharp;
using SimPe.Scenegraph.Compat;
using Control        = System.Object;
using Panel          = SimPe.Scenegraph.Compat.PanelCompat;
using Label          = SimPe.Scenegraph.Compat.LabelCompat;
using Button         = SimPe.Scenegraph.Compat.ButtonCompat;
using CheckBox       = SimPe.Scenegraph.Compat.CheckBoxCompat2;
using TextBox        = SimPe.Scenegraph.Compat.TextBoxCompat;
using SaveFileDialog = SimPe.Scenegraph.Compat.SaveFileDialogCompat;
using OpenFileDialog = SimPe.Scenegraph.Compat.OpenFileDialogCompat;
using DialogResult   = SimPe.DialogResult;

namespace SimPe.Wizards
{
	/// <summary>
	/// Zusammenfassung f�r RecolourWizardForm.
	/// </summary>
	public class RecolourWizardForm : Avalonia.Controls.Window
	{
		private Avalonia.Controls.TabControl tabControl1       = new Avalonia.Controls.TabControl();
		private Avalonia.Controls.TabItem    tabPage1          = new Avalonia.Controls.TabItem();
		private Avalonia.Controls.TabItem    tabPage2          = new Avalonia.Controls.TabItem();
		internal Panel                       pnwizard1         = new Panel();
		internal Panel                       pnwizard2         = new Panel();
		private Avalonia.Controls.TabItem    tabPage3          = new Avalonia.Controls.TabItem();
		internal Panel                       pnwizard3         = new Panel();
		private Label                        label3            = new Label();
		private Avalonia.Controls.TabItem    tabPage4          = new Avalonia.Controls.TabItem();
		internal PictureBox                  pb                = new PictureBox();
		private PictureBox                   pbmisc            = new PictureBox();
		private PictureBox                   pbsurfaces        = new PictureBox();
		private PictureBox                   pbplumbing        = new PictureBox();
		private PictureBox                   pbappliances      = new PictureBox();
		private PictureBox                   pblight           = new PictureBox();
		private PictureBox                   pbelectronics     = new PictureBox();
		private PictureBox                   pbdecorations     = new PictureBox();
		private PictureBox                   pbseating         = new PictureBox();
		private PictureBox                   pbgeneral         = new PictureBox();
		private Panel                        pnSelect          = new Panel();
		private ImageList                    iObjects          = new ImageList();
		internal ListView                    lv                = new ListView();
		private ImageList                    iTxtrs            = new ImageList();
		private Label                        lbno              = new Label();
		private ToolTip                      toolTip1          = new ToolTip();
		private SaveFileDialog               sfd               = new SaveFileDialog();
		private Button                       llexp             = new Button();
		private Button                       llimp             = new Button();
		private CheckBox                     cbalpha           = new CheckBox();
		private OpenFileDialog               ofd               = new OpenFileDialog();
		internal TextBox                     tbflname          = new TextBox();
		internal Label                       lberr             = new Label();
		internal CheckBox                    cbover            = new CheckBox();
		private Avalonia.Controls.TabItem    tabPage5          = new Avalonia.Controls.TabItem();
		internal Panel                       pnwizard1b        = new Panel();
		private Panel                        pnwizard1b_sub    = new Panel();
		private CheckBox                     cbauto            = new CheckBox();
		private System.ComponentModel.IContainer components    = null;

		public RecolourWizardForm()
		{
			// InitializeComponent replaced with no-op; fields are inline-initialized above.
			ShowSelection();
			loaded = false;
            if (Helper.XmlRegistry.UseBigIcons)
            {
                this.cbalpha.Location = new System.Drawing.Point(395, 105);
                this.cbalpha.Size = new System.Drawing.Size(153, 22);
                this.llexp.Location = new System.Drawing.Point(425, 76);
                this.llexp.Size = new System.Drawing.Size(103, 28);
                this.llimp.Location = new System.Drawing.Point(425, 135);
                this.llimp.Size = new System.Drawing.Size(103, 28);
                this.cbover.Location = new System.Drawing.Point(407, 54);
                this.cbover.Size = new System.Drawing.Size(107, 22);
                this.iObjects.ImageSize = new System.Drawing.Size(120, 120);
            }
		}

		public void Dispose()
		{
			if (components != null)
			{
				components.Dispose();
			}
		}

		internal Step1 step1;
		internal Step1b step1b;
		internal Step2 step2;
		internal Step3 step3;
		bool loaded;


		#region Image Selection Buttons
		private enum State
		{
			off,
			on,
			over
		}


		internal PictureBox selected = null;
		internal ListView[] lvobjs;
		internal ListView selectedlv = null;

		/// <summary>
		/// Show a diffrent slice
		/// </summary>
		/// <param name="img"></param>
		/// <param name="select"></param>
		/// <returns></returns>
		object ShowImage(object imgObj, State select)
		{
			if (imgObj is not Image img) return null;
			int wd = img.Width;

			Bitmap bm = new Bitmap(img.Width / 4, img.Height);
			Graphics gr = Graphics.FromImage(bm);

			int mul=0;
			if (select==State.on) mul=2;
			else if (select==State.over) mul=3;

			Rectangle src = new Rectangle(bm.Width * mul, 0, bm.Width, bm.Height);
			Rectangle dst = new Rectangle(0, 0, bm.Width, bm.Height);
			gr.DrawImage(img, dst, src, GraphicsUnit.Pixel);

			SimPe.Plugin.ImageLoader.Preview(bm, new Size(40, 40));
			return null;
		}

		/// <summary>
		/// Bild a PictureBox
		/// </summary>
		/// <param name="top"></param>
		/// <returns></returns>
		public PictureBox BuildImage(int left, ref int top, object img, int index)
		{
			PictureBox pb = new PictureBox();
			pb.Parent = this.pnSelect;
			pb.Size = new Size(40, 40);
			pb.Left = left;
			pb.Top = top;
			top += pb.Height+2;

			pb.Image = ShowImage(img, State.off);
			pb.Tag = img;
			pb.Cursor = Cursors.Hand;

			pb.MouseEnter += new EventHandler(SelectButtonEnter);
			pb.MouseLeave += new EventHandler(SelectButtonLeave);
			pb.Click += new EventHandler(SelectButtonClick);

			ListView lv = new ListView();
			lv.Tag = pb;
			lv.Parent = pnwizard1;
			lv.Left = pnSelect.Width + 2;
			lv.Top = 0;
			lv.Height = pnwizard1.Height;
			lv.Width = pnwizard1.Width - lv.Left;
			lv.BorderStyle = BorderStyle.None;
			lv.View = View.LargeIcon;
			lv.LargeImageList = iObjects;
			lv.HideSelection = false;
			lv.MultiSelect = false;

			lv.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
			lv.SelectedIndexChanged += new EventHandler(ObjectSelectionChanged);
			lv.Visible = false;

			lvobjs[index] = lv;

			return pb;
		}

		/// <summary>
		/// Display the Selection buttons
		/// </summary>
		public void ShowSelection()
		{
			lvobjs = new ListView[9];

			int top = (250 - (42*5)) / 2;
			PictureBox pbfirst = BuildImage(0, ref top, this.pbseating.Image, 0);
			BuildImage(0, ref top, this.pbsurfaces.Image, 1);
			BuildImage(0, ref top, this.pbdecorations.Image, 5);
			BuildImage(0, ref top, this.pbplumbing.Image, 4);
			BuildImage(0, ref top, this.pbappliances.Image, 2);
			top = (250 - (42*4)) / 2;
			BuildImage(42, ref top, this.pbelectronics.Image, 3);
			BuildImage(42, ref top, this.pblight.Image, 7);
			BuildImage(42, ref top, this.pbgeneral.Image, 6);
			BuildImage(42, ref top, this.pbmisc.Image, 8);

			SelectButtonClick(pbfirst, null);
		}

		private void SelectButtonEnter(object sender, EventArgs e)
		{
			PictureBox pb = (PictureBox)sender;
			if (selected!=pb) pb.Image = this.ShowImage(pb.Tag, State.over);
		}

		private void SelectButtonLeave(object sender, EventArgs e)
		{
			PictureBox pb = (PictureBox)sender;
			if (selected!=pb) pb.Image = this.ShowImage(pb.Tag, State.off);
			else pb.Image = this.ShowImage(pb.Tag, State.on);
		}

		private void SelectButtonClick(object sender, EventArgs e)
		{
			if (selected!=null) selected.Image = ShowImage(selected.Tag, State.off);

			PictureBox pb = (PictureBox)sender;
			pb.Image = this.ShowImage(pb.Tag, State.on);

			selected = pb;
			selectedlv = null;
			foreach (Control c in pnwizard1.Controls)
			{
				if (c.GetType()==typeof(ListView))
				{
					ListView lv = (ListView)c;
					lv.Visible = (lv.Tag == selected);

					if (lv.Tag == selected)
						selectedlv = lv;
				}
			}

			if (step1!=null) step1.Update();
		}

		private void ObjectSelectionChanged(object sender, EventArgs e)
		{
			step1.Update();
		}
		#endregion

		ListViewItem CreateItem(string modelname, object img, SimPe.PackedFiles.Wrapper.ExtObjd objd, string name)
		{
			ListViewItem lvi = new ListViewItem(name);
			lvi.Tag = objd;
			if (img is SKBitmap skbImg)
			{
				lvi.ImageIndex = iObjects.Images.Count;
				iObjects.Images.Add(skbImg);
			}

			lvi.SubItems.Add(modelname);
			return lvi;
		}

		SimPe.Packages.File objects;

		/// <summary>
		/// Load all available Objects
		/// </summary>
		public void BuildList()
		{
			if (loaded) return;


			string sourcefile = System.IO.Path.Combine(PathProvider.Global[Expansions.BaseGame].InstallFolder, "TSData"+Helper.PATH_SEP+"Res"+Helper.PATH_SEP+"Objects"+Helper.PATH_SEP+"objects.package");
			if (!System.IO.File.Exists(sourcefile))
			{
				MessageBox.Show("The objects.package was not found.\n\nPlease set the Path to your Sims 2 installation in SimPe in the Extra->Options... Dialog.");
				return;
			}

			//initialize the FileTable if needed
			if (SimPe.FileTable.FileIndex==null) SimPe.FileTable.FileIndex = new SimPe.Plugin.FileIndex();
			SimPe.FileTable.FileIndex.Load();

			iObjects.Images.Clear();
			objects = SimPe.Packages.File.LoadFromFile(sourcefile);
			SimPe.Plugin.Tool.Dockable.ObjectLoader ol = new SimPe.Plugin.Tool.Dockable.ObjectLoader(null);
			ol.Finished += new EventHandler(ol_Finished);
			ol.LoadedItem += new SimPe.Plugin.Tool.Dockable.ObjectLoader.LoadItemHandler(ol_LoadedItem);
			ol.LoadData();
		}

		protected SKBitmap GetImageFile(SimPe.Plugin.Rcol txtr)
		{
			SimPe.Plugin.ImageData id = (SimPe.Plugin.ImageData)txtr.Blocks[0];
			SimPe.Plugin.MipMap mipmap = null;
			foreach (SimPe.Plugin.MipMapBlock mmb in id.MipMapBlocks)
			{
				foreach (SimPe.Plugin.MipMap mm in mmb.MipMaps)
				{
					if (mm.Texture!=null) mipmap = mm;
				}
			}

			if (mipmap!=null) return mipmap.Texture;

			return null;
		}

		protected void MakeTexturePreview(SimPe.Packages.GeneratableFile npackage)
		{
			iTxtrs.Images.Clear();
			Interfaces.Files.IPackedFileDescriptor[] txtrs = npackage.FindFiles(0x1C4A276C);
			foreach (Interfaces.Files.IPackedFileDescriptor pfd in txtrs)
			{
				SimPe.Plugin.Rcol txtr = new SimPe.Plugin.GenericRcol(null, false);
				txtr.ProcessData(pfd, npackage);

				ListViewItem lvi = new ListViewItem(Hashes.StripHashFromName(txtr.FileName));
				lvi.Tag = txtr;

				SKBitmap img = this.GetImageFile(txtr);

				if (img!=null)
				{
					lvi.ImageIndex = iTxtrs.Images.Count;
					iTxtrs.Images.Add(img);
				}

				lv.Items.Add(lvi);
			}
		}

		SimPe.Packages.GeneratableFile npackage;
		internal SimPe.Plugin.SubsetSelectForm ssf;
		internal Hashtable fullmap;
		bool showselect;
		protected void SelectCallback(SimPe.Plugin.SubsetSelectForm ssf, bool show, Hashtable fullmap)
		{
			showselect = show;
			this.cbauto.Checked = ssf.cbauto.Checked;
			this.ssf = ssf;
			this.fullmap = fullmap;

			this.pnwizard1b_sub.Controls.Clear();
			ssf.pnselect.Parent = this.pnwizard1b_sub;
			ssf.pnselect.Left = 0;
			ssf.pnselect.Top = 0;
			ssf.pnselect.Width = pnwizard1b_sub.Width;
			ssf.pnselect.Height = pnwizard1b_sub.Height;

			foreach (ListView lv in ssf.ListViews)
			{
				lv.BorderStyle = BorderStyle.None;
				lv.SelectedIndexChanged += new EventHandler(SubsetSelectedIndexChanged);
			}

			//show the next step
			if (!show)
			{
				ssf.button1.Enabled = true;
				step1b.Update(true);
			}
		}

		SimPe.Plugin.ColorOptions cs;
		public bool Recolor()
		{
			if (selectedlv==null) return true;
			if (selectedlv.SelectedItems.Count==0) return true;

			llexp.Enabled = false;
			llimp.Enabled = false;

			WaitingScreen.Wait();
            try
            {
                WaitingScreen.UpdateMessage("Preparing Recolour Package");
                lv.Items.Clear();

                SimPe.PackedFiles.Wrapper.ExtObjd objd = (SimPe.PackedFiles.Wrapper.ExtObjd)selectedlv.SelectedItems[0].Tag;

                SimPe.Packages.GeneratableFile pkg = SimPe.Packages.GeneratableFile.LoadFromStream((System.IO.BinaryReader)null);
                pkg.FileName = "WOS";

                //Create the Basic Clone
                WaitingScreen.UpdateMessage("Collecting needed Files");
                string[] modelnames = SimPe.Plugin.Workshop.BaseClone(objd.FileDescriptor, objd.FileDescriptor.Group, pkg);
                SimPe.Plugin.ObjectCloner objclone = new SimPe.Plugin.ObjectCloner(pkg);
                objclone.Setup.OnlyDefaultMmats = false;
                objclone.Setup.UpdateMmatGuids = false;
                objclone.RcolModelClone(modelnames);

                WaitingScreen.UpdateMessage("Loading additional References");
                SimPe.Plugin.ObjectRecolor or = new SimPe.Plugin.ObjectRecolor(pkg);
                or.LoadReferencedMATDs();

                //Build the Recolour
                WaitingScreen.UpdateMessage("Building Recolour");
                npackage = SimPe.Packages.GeneratableFile.LoadFromStream((System.IO.BinaryReader)null);
                npackage.FileName = "WOS";

                cs = new SimPe.Plugin.ColorOptions(pkg);
                cs.Create(npackage, new SimPe.Plugin.ColorOptions.CreateSelectionCallback(SelectCallback));
            }
            finally
            {
                WaitingScreen.Stop();
            }

			return showselect;
		}

		public void Recolor2()
		{
			WaitingScreen.Wait();
            try
            {
                lv.Items.Clear();
                if ((ssf != null) && (fullmap != null))
                {
                    Hashtable map = SimPe.Plugin.SubsetSelectForm.Finish(ssf);
                    cs.ProcessMmatMap(npackage, map, fullmap);
                }
                //Select all Textures the package Contains
                WaitingScreen.UpdateMessage("Select Textures"); // CJH

                MakeTexturePreview(npackage);

                step2.Update();
                lbno.Visible = !step2.CanContinue;
                cbauto.Visible = lv.Visible = llexp.Visible = llimp.Visible = cbalpha.Visible = step2.CanContinue;
            }
            finally { WaitingScreen.Stop(); }
        }

		private void SelectTexture(object sender, System.EventArgs e)
		{
			llexp.Enabled = (lv.SelectedItems.Count!=0);
			llimp.Enabled = llexp.Enabled;
		}

		private void Export(object sender, System.EventArgs e)
		{
			if (lv.SelectedItems.Count==0) return;

			sfd.Filter = "PNG Image (*.png)|*.png|Bitmap (*.bmp)|*.bmp|GIF Image (*.gif)|*.gif|Jpeg Image (*.jpg)|*.jpg|All Files (*.*)|*.*";
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				SimPe.Plugin.Rcol txtr = (SimPe.Plugin.Rcol)lv.SelectedItems[0].Tag;
				SKBitmap img = this.GetImageFile(txtr);
				if (img != null)
				{
					using (var fs = System.IO.File.OpenWrite(sfd.FileName))
					using (var data = img.Encode(SKEncodedImageFormat.Png, 100))
						data.SaveTo(fs);
				}

				if (cbalpha.Checked && img != null)
				{
					SKBitmap bm = new SKBitmap(img.Width, img.Height);
					for (int y=0; y<img.Height; y++)
					{
						for (int x=0; x<img.Width; x++)
						{
							int a = 0xff;
							try
							{
								SKColor pixel = img.GetPixel(x, y);
								a = pixel.Alpha;
								bm.SetPixel(x, y, new SKColor((byte)a, (byte)a, (byte)a, 255));
							}
#if DEBUG
							catch (Exception ex)
							{
							Helper.ExceptionMessage("", ex);
#else
							catch (Exception){
#endif

							}
						}
					}
					string alphaPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(sfd.FileName), System.IO.Path.GetFileNameWithoutExtension(sfd.FileName))+"_alpha"+System.IO.Path.GetExtension(sfd.FileName);
					using (var fs = System.IO.File.OpenWrite(alphaPath))
					using (var data = bm.Encode(SKEncodedImageFormat.Png, 100))
						data.SaveTo(fs);
				}
			}
		}

		private void Import(object sender, System.EventArgs e)
		{
			if (lv.SelectedItems.Count==0) return;

			ofd.Filter = "PNG Image (*.png)|*.png|Bitmap (*.bmp)|*.bmp|GIF Image (*.gif)|*.gif|Jpeg Image (*.jpg)|*.jpg|All Files (*.*)|*.*";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				SimPe.Plugin.Rcol txtr = (SimPe.Plugin.Rcol)lv.SelectedItems[0].Tag;
				SimPe.Plugin.ImageData oldid = (SimPe.Plugin.ImageData)txtr.Blocks[0];

				//build TXTR File
				SimPe.Plugin.ImageData id = new SimPe.Plugin.ImageData(null);
				id.NameResource = oldid.NameResource;
				id.Version = oldid.Version;
				id.BlockID = oldid.BlockID;
				id.BlockName = oldid.BlockName;

                if ((System.IO.File.Exists(PathProvider.Global.NvidiaDDSTool)) && ((oldid.Format == SimPe.Plugin.ImageLoader.TxtrFormats.DXT1Format) || (oldid.Format == SimPe.Plugin.ImageLoader.TxtrFormats.DXT3Format) || (oldid.Format == SimPe.Plugin.ImageLoader.TxtrFormats.DXT5Format)))
				{
                    SimPe.Plugin.BuildTxtr.LoadDDS(id, SimPe.Plugin.DDSTool.BuildDDS(ofd.FileName, (int)oldid.MipMapLevels, oldid.Format, "-sharpenMethod Smoothen"));
				}
				else
				{
					id.Format = oldid.Format;
					if ((oldid.Format == SimPe.Plugin.ImageLoader.TxtrFormats.DXT1Format) || (oldid.Format == SimPe.Plugin.ImageLoader.TxtrFormats.DXT3Format) || (oldid.Format == SimPe.Plugin.ImageLoader.TxtrFormats.DXT5Format))
						id.Format = SimPe.Plugin.ImageLoader.TxtrFormats.Raw32Bit;
                    SimPe.Plugin.BuildTxtr.LoadTXTR(id, ofd.FileName, oldid.TextureSize, (int)oldid.MipMapLevels, id.Format);
				}


				txtr.Blocks[0] = id;
				txtr.SynchronizeUserData();

				//Update the Image
				if ((lv.SelectedItems[0].ImageIndex>=0) && (lv.SelectedItems[0].ImageIndex<iTxtrs.Images.Count))
				{
					this.iTxtrs.Images[lv.SelectedItems[0].ImageIndex] = this.GetImageFile(txtr);
				}
			}
		}

		internal string GetPackageFilename
		{
			get
			{
                string down = System.IO.Path.Combine(PathProvider.SimSavegameFolder, "Downloads");
				down = System.IO.Path.Combine(down, tbflname.Text+".package");
				return down;
			}
		}

		internal void SaveRecolor()
		{
            string down = System.IO.Path.Combine(PathProvider.SimSavegameFolder, "Downloads");

			if (System.IO.Directory.Exists(down))
			{
				down = GetPackageFilename;
				npackage.Save(down);
			}
			else
			{
				sfd.FileName = tbflname.Text+".package";
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					npackage.Save(sfd.FileName);
				}
			}

			//npackage.Reader.Close();
		}

		private void ChangeText(object sender, System.EventArgs e)
		{
			lberr.Visible = System.IO.File.Exists(this.GetPackageFilename);
			step3.Update();
		}

		private void OverwriteState(object sender, System.EventArgs e)
		{
			step3.Update();
			lberr.Visible = System.IO.File.Exists(this.GetPackageFilename);
		}

		private void cbauto_CheckedChanged(object sender, System.EventArgs e)
		{
			if (ssf!=null)
			{
				ssf.cbauto.Checked = this.cbauto.Checked;
			}
		}

		private void SubsetSelectedIndexChanged(object sender, EventArgs e)
		{
			step1b.Update();
		}

		delegate void InvokeTargetLoad(SimPe.Cache.ObjectCacheItem oci, SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem fii, Data.Alias a);
		delegate void InvokeTargetFinish(object sender, EventArgs e);

		private void ol_Finished(object sender, EventArgs e)
		{
			//this.Invoke(new InvokeTargetFinish(invoke_Finished), new object[] {sender, e});
			invoke_Finished(sender, e);

		}

		private void invoke_Finished(object sender, EventArgs e)
		{
			loaded = true;
		}

		private void ol_LoadedItem(SimPe.Cache.ObjectCacheItem oci, SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem fii, Data.Alias a)
		{
			//this.Invoke(new InvokeTargetLoad(invoke_LoadedItem), new object[] {oci, fii, a});
			invoke_LoadedItem(oci, fii, a);
		}

		private void invoke_LoadedItem(SimPe.Cache.ObjectCacheItem oci, SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem fii, Data.Alias a)
		{
			//WaitingScreen.UpdateMessage(a.Name);
			SimPe.PackedFiles.Wrapper.ExtObjd objd = new SimPe.PackedFiles.Wrapper.ExtObjd();
			objd.ProcessData(fii);
			object imgObj = oci.Thumbnail;
			if (imgObj is Image sdi)
			{
				sdi = Ambertation.Drawing.GraphicRoutines.KnockoutImage(sdi, new Point(0,0), Color.Magenta);
				sdi = Ambertation.Windows.Forms.Graph.ImagePanel.CreateThumbnail(sdi, this.iObjects.ImageSize, 8, Color.FromArgb(90, Color.Black), Color.FromArgb(10, 10, 40), Color.White, Color.FromArgb(80, Color.White), true, 3, 3);
				imgObj = sdi;
			}
			ListViewItem item = this.CreateItem(a.Tag[2].ToString(), imgObj, objd, a.Name);

			bool added = false;
			if (objd.FunctionSort.InAppliances) { lvobjs[(int)Data.ObjFunctionSortBits.Appliances].Items.Add((ListViewItem)item.Clone()); added=true;}
			if (objd.FunctionSort.InDecorative) { lvobjs[(int)Data.ObjFunctionSortBits.Decorative].Items.Add((ListViewItem)item.Clone()); added=true;}
			if (objd.FunctionSort.InElectronics) { lvobjs[(int)Data.ObjFunctionSortBits.Electronics].Items.Add((ListViewItem)item.Clone()); added=true;}
			if (objd.FunctionSort.InGeneral) { lvobjs[(int)Data.ObjFunctionSortBits.General].Items.Add((ListViewItem)item.Clone()); added=true;}
			if (objd.FunctionSort.InLighting) { lvobjs[(int)Data.ObjFunctionSortBits.Lighting].Items.Add((ListViewItem)item.Clone()); added=true;}
			if (objd.FunctionSort.InPlumbing) { lvobjs[(int)Data.ObjFunctionSortBits.Plumbing].Items.Add((ListViewItem)item.Clone()); added=true;}
			if (objd.FunctionSort.InSeating) { lvobjs[(int)Data.ObjFunctionSortBits.Seating].Items.Add((ListViewItem)item.Clone()); added=true;}
			if (objd.FunctionSort.InSurfaces) { lvobjs[(int)Data.ObjFunctionSortBits.Surfaces].Items.Add((ListViewItem)item.Clone()); added=true;}
			if (!added) { lvobjs[8].Items.Add(item); }
		}
	}
}
