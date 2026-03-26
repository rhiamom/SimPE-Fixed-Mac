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
using Avalonia.Interactivity;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Scenegraph;
using SimPe.Windows.Forms;
using Avalonia.Platform.Storage;
using SimPe.Scenegraph.Compat;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for TxtrForm.
	/// </summary>
	public class TxtrForm : Avalonia.Controls.Window
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public TxtrForm()
		{
            InitializeComponent();
            if (Helper.XmlRegistry.UseBigIcons)
            {
                if (lbimg != null) lbimg.FontSize = 13;
                if (tblifo != null) tblifo.FontSize = 14;
                if (tbflname != null) tbflname.FontSize = 14;
            }

			tbwidth.IsReadOnly = true;
			tbheight.IsReadOnly = true;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
		}

		#region Avalonia AXAML layout placeholder
		private void InitializeComponent()
		{
			// TODO: Avalonia AXAML layout
			txtrPanel = new Avalonia.Controls.Panel();
			linkLabel4 = new Avalonia.Controls.Button();
			linkLabel3 = new Avalonia.Controls.Button();
			linkLabel1 = new Avalonia.Controls.Button();
			tblevel = new Avalonia.Controls.TextBox();
			label8 = new Avalonia.Controls.TextBlock();
			linkLabel2 = new Avalonia.Controls.Button();
			lldel = new Avalonia.Controls.Button();
			tblifo = new Avalonia.Controls.TextBox();
			label6 = new Avalonia.Controls.TextBlock();
			label5 = new Avalonia.Controls.TextBlock();
			tbheight = new Avalonia.Controls.TextBox();
			tbwidth = new Avalonia.Controls.TextBox();
			label4 = new Avalonia.Controls.TextBlock();
			label3 = new Avalonia.Controls.TextBlock();
			cbformats = new Avalonia.Controls.ComboBox();
			tbflname = new Avalonia.Controls.TextBox();
			label2 = new Avalonia.Controls.TextBlock();
			cbitem = new Avalonia.Controls.ComboBox();
			cbmipmaps = new Avalonia.Controls.ComboBox();
			panel1 = new Avalonia.Controls.Panel();
			label7 = new Avalonia.Controls.TextBlock();
			pb = new PictureBoxCompat();
			contextMenu1 = new Avalonia.Controls.ContextMenu();
			menuItem1 = new Avalonia.Controls.MenuItem();
			milifo = new Avalonia.Controls.MenuItem();
			menuItem4 = new Avalonia.Controls.MenuItem();
			menuItem6 = new Avalonia.Controls.MenuItem();
			menuItem7 = new Avalonia.Controls.MenuItem();
			mibuild = new Avalonia.Controls.MenuItem();
			menuItem3 = new Avalonia.Controls.MenuItem();
			menuItem2 = new Avalonia.Controls.MenuItem();
			menuItem5 = new Avalonia.Controls.MenuItem();
			lbimg = new Avalonia.Controls.ListBox();
			panel2 = new SimPe.Windows.Forms.WrapperBaseControl();
			btex = new Avalonia.Controls.Button();
			btim = new Avalonia.Controls.Button();
			label1 = new Avalonia.Controls.TextBlock();
		}
		#endregion

        internal Avalonia.Controls.Panel txtrPanel;
		internal Avalonia.Controls.ListBox lbimg;
        private SimPe.Windows.Forms.WrapperBaseControl panel2;
		private Avalonia.Controls.Panel panel1;
        private PictureBoxCompat pb;
		private Avalonia.Controls.Button btim;
		internal Avalonia.Controls.Button btex;
		private Avalonia.Controls.TextBlock label1;
		internal Avalonia.Controls.ComboBox cbmipmaps;
		internal Avalonia.Controls.ComboBox cbitem;
		private Avalonia.Controls.TextBlock label2;
		private Avalonia.Controls.TextBox tbflname;
		internal Avalonia.Controls.ComboBox cbformats;
		private Avalonia.Controls.TextBlock label3;
		private Avalonia.Controls.TextBlock label4;
		private Avalonia.Controls.TextBox tbwidth;
		private Avalonia.Controls.TextBox tbheight;
		private Avalonia.Controls.TextBlock label5;
		private Avalonia.Controls.TextBox tblifo;
		private Avalonia.Controls.TextBlock label6;
		private Avalonia.Controls.Button linkLabel2;
		internal Avalonia.Controls.Button lldel;
		private Avalonia.Controls.ContextMenu contextMenu1;
		private Avalonia.Controls.MenuItem menuItem1;
		private Avalonia.Controls.MenuItem menuItem2;
		private Avalonia.Controls.MenuItem menuItem3;
		private Avalonia.Controls.MenuItem menuItem4;
		private Avalonia.Controls.MenuItem menuItem5;
		private Avalonia.Controls.MenuItem menuItem6;
		private Avalonia.Controls.TextBlock label7;
		private Avalonia.Controls.TextBox tblevel;
		private Avalonia.Controls.TextBlock label8;
		private Avalonia.Controls.Button linkLabel1;
		private Avalonia.Controls.Button linkLabel3;
		private Avalonia.Controls.MenuItem milifo;
		private Avalonia.Controls.Button linkLabel4;
		private Avalonia.Controls.MenuItem menuItem7;
		private Avalonia.Controls.MenuItem mibuild;

		internal Txtr wrapper = null;

		private void PictureSelect(object sender, System.EventArgs e)
		{
			pb.Image = null;
			btex.IsEnabled = false;
			lldel.IsEnabled = false;
			try
			{
				lbimg.Tag = true;
				MipMap mm = (MipMap)lbimg.Items[lbimg.SelectedIndex];
				pb.Image = mm.Texture;
				if (mm.Texture==null) tblifo.Text = mm.LifoFile;
				else tblifo.Text = "";

				btex.IsEnabled = (pb.Image!=null);
				lldel.IsEnabled = true;
			}
			catch (Exception) {}
			finally
			{
				lbimg.Tag = null;
			}
		}

		private void btcommit_Click(object sender, System.EventArgs e)
		{
			try
			{
				Txtr wrp = (Txtr)wrapper;
				wrp.SynchronizeUserData();
				SimPe.Message.Show(Localization.Manager.GetString("commited"));
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}
		}

		private async void btex_Click(object sender, System.EventArgs e)
		{
			if (pb.Image == null) return;

			var topLevel = TopLevel.GetTopLevel(this);
			if (topLevel == null) return;
			var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
			{
				Title = "Export Texture",
				SuggestedFileName = tbflname.Text + "_" + pb.Image.Size.Width + "x" + pb.Image.Size.Height + ".png",
				FileTypeChoices = new[] { new FilePickerFileType("PNG Image") { Patterns = new[] { "*.png" } } }
			});
			if (file != null)
			{
				try
				{
					string path = file.Path.LocalPath;
					pb.Image.Save(path, ImageLoader.GetImageFormat(path));
				}
				catch (Exception ex)
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
				}
			}
		}

		private async void btim_Click(object sender, System.EventArgs e)
		{
			if (lbimg.SelectedIndex<0) return;

			var topLevel = TopLevel.GetTopLevel(this);
			if (topLevel == null) return;
			var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
			{
				Title = "Import Texture",
				AllowMultiple = false,
				FileTypeFilter = new[] { new FilePickerFileType("Image Files") { Patterns = new[] { "*.png", "*.bmp", "*.gif", "*.jpg", "*.jpeg", "*.tif", "*.tiff" } } }
			});
			if (files.Count > 0)
			{
				try
				{
					ImageData id = (ImageData)cbitem.Items[cbitem.SelectedIndex];
					System.IO.Stream s = System.IO.File.OpenRead(files[0].Path.LocalPath);
					System.Drawing.Image img = Helper.LoadImage(s);
					s.Close();
					s.Dispose();
					s = null;

					img = this.CropImage(id, img);
					if (img==null) return;

					lbimg.Tag = true;
					MipMap mm = (MipMap)lbimg.Items[lbimg.SelectedIndex];
					mm.LifoFile = "";
					mm.Texture = img;
					pb.Image = img;
					lbimg.Items[lbimg.SelectedIndex] = mm;
				}
				catch (Exception ex)
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("erropenfile"), ex);
				}
				finally
				{
					lbimg.Tag = null;
				}
			}
		}

		private void SelectItem(object sender, System.EventArgs e)
		{
			if (cbitem.Tag!=null) return;
			this.cbmipmaps.Items.Clear();
			this.lbimg.Items.Clear();
			if (cbitem.SelectedIndex<0) return;
			try
			{
				cbitem.Tag = true;
				ImageData selecteditem = (ImageData)cbitem.Items[cbitem.SelectedIndex];
				foreach (MipMapBlock mmp in selecteditem.MipMapBlocks)
				{
					this.cbmipmaps.Items.Add(mmp);
				}

				if (cbmipmaps.Items.Count>0) cbmipmaps.SelectedIndex = 0;
				this.tbflname.Text = selecteditem.NameResource.FileName;
				this.tbwidth.Text = selecteditem.TextureSize.Width.ToString();
				this.tbheight.Text = selecteditem.TextureSize.Height.ToString();
				this.tblevel.Text = selecteditem.MipMapLevels.ToString();

				this.cbformats.SelectedIndex = 0;
				for (int i=0; i<cbformats.Items.Count; i++)
				{
					ImageLoader.TxtrFormats f = (ImageLoader.TxtrFormats)cbformats.Items[i];
					if (f==selecteditem.Format)
					{
						cbformats.SelectedIndex = i;
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("erropenfile"), ex);
			}
			finally
			{
				cbitem.Tag = null;
			}
		}

		private void FileNameChanged(object sender, System.EventArgs e)
		{
			if (cbitem.Tag!=null) return;
			if (cbitem.SelectedIndex<0) return;
			try
			{
				cbitem.Tag = true;
				ImageData selecteditem = (ImageData)cbitem.Items[cbitem.SelectedIndex];
				selecteditem.NameResource.FileName = tbflname.Text.Trim();
				if (tbflname.Text.ToLower().EndsWith("_txtr"))
				{
					selecteditem.FileNameRepeat = selecteditem.NameResource.FileName.Substring(0, selecteditem.NameResource.FileName.Length - 5);
				}
				cbitem.Items[cbitem.SelectedIndex] = selecteditem;
				// cbitem.Text not available in Avalonia ComboBox — display updates via selected item
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("erropenfile"), ex);
			}

			finally
			{
				cbitem.Tag = null;
			}
		}

		private void SelectMipMapBlock(object sender, System.EventArgs e)
		{
			if (cbmipmaps.Tag!=null) return;
			this.lbimg.Items.Clear();
			if (cbmipmaps.SelectedIndex<0) return;
			try
			{
				cbmipmaps.Tag = true;
				MipMapBlock mmp = (MipMapBlock)cbmipmaps.Items[cbmipmaps.SelectedIndex];
				int minindex = -1;
				for (int i=0; i<mmp.MipMaps.Length; i++)
				{
					MipMap mm = mmp.MipMaps[i];
					mm.ReloadTexture();
					this.lbimg.Items.Add(mm);
					if (mm.Texture != null) minindex = i;
				}

				lbimg.SelectedIndex = minindex;
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("erropenfile"), ex);
			}
			finally
			{
				cbmipmaps.Tag = null;
			}
		}

		private void ChangeFormat(object sender, System.EventArgs e)
		{
			if (cbitem.Tag!=null) return;
			if (cbitem.SelectedIndex<0) return;
			if (cbformats.SelectedIndex<1) return;
			try
			{
				cbitem.Tag = true;
				ImageData selecteditem = (ImageData)cbitem.Items[cbitem.SelectedIndex];
				selecteditem.Format = (ImageLoader.TxtrFormats)cbformats.Items[cbformats.SelectedIndex];

				//make sure images are resaved when the Format was changed!
				foreach (MipMapBlock mmp in selecteditem.MipMapBlocks)
				{
					foreach (MipMap mm in mmp.MipMaps)
					{
						mm.Data = null;
					}
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("erropenfile"), ex);
			}

			finally
			{
				cbitem.Tag = null;
			}
		}

		private void SetLifo(object sender, System.EventArgs e)
		{
			if (lbimg.Tag !=null) return;
			try
			{
				MipMap mm = (MipMap)lbimg.Items[lbimg.SelectedIndex];
				pb.Image = null;
				mm.Texture = null;
				mm.LifoFile = tblifo.Text;
				lbimg.Items[lbimg.SelectedIndex] = mm;
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("erropenfile"), ex);
			}
		}

		private void Delete(object sender, EventArgs e)
		{
			if (lbimg.SelectedIndex<0) return;
			lbimg.Items.Remove(lbimg.Items[lbimg.SelectedIndex]);
			UpdateMimMaps();
		}

		private void Add(object sender, EventArgs e)
		{
			MipMap mm = new MipMap(SelectedImageData());
			mm.LifoFile = null;
			mm.Texture = new Bitmap(512, 256);
			lbimg.Items.Add(mm);
			UpdateMimMaps();
		}

		protected ImageData SelectedImageData()
		{
			//add a MipMapBlock if it doesnt already exist
			ImageData id = null;
			if (cbitem.SelectedIndex<0)
			{
				Txtr wrp = (Txtr)wrapper;
				id = new ImageData(wrp);
				id.NameResource.FileName = "Unknown";

				IRcolBlock[] irc = new IRcolBlock[wrp.Blocks.Length+1];
				wrp.Blocks.CopyTo(irc, 0);
				irc[irc.Length-1] = id;
				wrp.Blocks = irc;
				cbitem.Items.Add(id);
				cbitem.SelectedIndex = cbitem.Items.Count-1;
			}
			else
			{
				id = (ImageData)cbitem.Items[cbitem.SelectedIndex];
			}

			return id;
		}

		protected MipMapBlock SelectedMipMapBlock(ImageData id)
		{
			//add a MipMapBlock if it doesnt already exist
			if (this.cbmipmaps.SelectedIndex<0)
			{
				MipMapBlock[] mmp = new MipMapBlock[id.MipMapBlocks.Length+1];
				id.MipMapBlocks.CopyTo(mmp, 0);
				mmp[mmp.Length-1] = new MipMapBlock(id);
				id.MipMapBlocks = mmp;
				cbmipmaps.Items.Add(mmp[mmp.Length-1]);
				cbmipmaps.SelectedIndex = cbmipmaps.Items.Count-1;

				return mmp[mmp.Length-1];
			}
			else
			{
				object o = cbmipmaps.SelectedItem;
				if (o is MipMapBlock) return o as MipMapBlock;

				try
				{
					MipMapBlock[] mmb = o as MipMapBlock[];
					return mmb[mmb.Length-1];
				}
				catch
				{
					return new MipMapBlock(id);
				}
			}
		}

		protected void UpdateMimMaps()
		{
			ImageData id = SelectedImageData();
			MipMapBlock mmp = SelectedMipMapBlock(id);

			MipMap[] mm = new MipMap[lbimg.Items.Count];
			for (int i=0; i<mm.Length; i++)
			{
				mm[i] = (MipMap)lbimg.Items[i];
			}
			mmp.MipMaps = mm;
			id.MipMapLevels = (uint)mm.Length;
			tblevel.Text = id.MipMapLevels.ToString();
		}

		private void UpdateAllSizes(object sender, System.EventArgs e)
		{
			try
			{
				lbimg.Tag = true;
				MipMap map = null;
				Size sz = new Size(0, 0);

				//Find biggest Texture
				for (int i=0; i< lbimg.Items.Count; i++)
				{
					MipMap mm = (MipMap)lbimg.Items[i];

					if (mm.Texture!=null)
					{
						if (mm.Texture.Size.Width > sz.Width)
						{
							sz = mm.Texture.Size;
							map = mm;
						}
					}
				} // for i

				if (map==null) return;

				//create a Scaled Version for each testure
				for (int i=0; i< lbimg.Items.Count; i++)
				{
					MipMap mm = (MipMap)lbimg.Items[i];

					if (mm.Texture!=null)
					{
						//don't change the original Picture
						if (mm != map)
						{
							Bitmap bm = new Bitmap(mm.Texture.Size.Width, mm.Texture.Size.Height);
							Graphics gr = Graphics.FromImage(bm);

							gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
							gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
							gr.DrawImage(map.Texture, new Rectangle(new Point(0,0), bm.Size), new Rectangle(new Point(0,0), map.Texture.Size), GraphicsUnit.Pixel);
							mm.Texture = bm;
						}
					}
				} // for i
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		protected System.Drawing.Image GetAlpha(System.Drawing.Image img)
		{
			Bitmap bm = new Bitmap(pb.Image.Size.Width, pb.Image.Size.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

			Bitmap src = (Bitmap)img;
			for (int y=0; y<bm.Size.Height; y++)
			{
				for (int x=0; x<bm.Size.Width; x++)
				{
					byte a = src.GetPixel(x, y).A;
					bm.SetPixel(x, y, Color.FromArgb(a, a, a));
				} // for x
			} //for y

			return bm;
		}

		protected System.Drawing.Image ChangeAlpha(System.Drawing.Image img, System.Drawing.Image alpha)
		{
			Bitmap bm = new Bitmap(pb.Image.Size.Width, pb.Image.Size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			Bitmap src = (Bitmap)img;
			Bitmap asrc = (Bitmap)alpha;
			for (int y=0; y<bm.Size.Height; y++)
			{
				for (int x=0; x<bm.Size.Width; x++)
				{
					byte a = asrc.GetPixel(x, y).R;
					Color cl = src.GetPixel(x, y);
					bm.SetPixel(x, y, Color.FromArgb(a, cl));
				} // for x
			} //for y

			return bm;
		}

		protected System.Drawing.Image CropImage(ImageData id, System.Drawing.Image img)
		{
			double ratio = (double)id.TextureSize.Width / (double)id.TextureSize.Height;
			double newratio = (double)img.Width / (double)img.Height;

			if (ratio != newratio)
			{
				if (SimPe.Message.Show("The File you want to import does not have the correct aspect Ratio!\n\nDo you want SimPe to crop the Image?", "Warning", MessageBoxButtons.YesNo) == SimPe.DialogResult.Yes)
				{
					int w = Convert.ToInt32(img.Height * ratio);
					int h = img.Height;
					if (w>img.Width)
					{
						w = img.Width;
						h = Convert.ToInt32(img.Width / ratio);
					}

					System.Drawing.Image img2 = new Bitmap(w, h);
					Graphics gr = Graphics.FromImage(img2);
					gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

					gr.DrawImageUnscaled(img, 0, 0);
					img = img2;
				}
				else
				{
					return null;
				}

			}

			return img;
		}

		private async void ExportAlpha(object sender, System.EventArgs e)
		{
			if (pb.Image == null) return;

			var topLevel = TopLevel.GetTopLevel(this);
			if (topLevel == null) return;
			var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
			{
				Title = "Export Alpha Channel",
				SuggestedFileName = tbflname.Text + "_alpha_" + pb.Image.Size.Width + "x" + pb.Image.Size.Height + ".png",
				FileTypeChoices = new[] { new FilePickerFileType("PNG Image") { Patterns = new[] { "*.png" } } }
			});
			if (file != null)
			{
				try
				{
					string path = file.Path.LocalPath;
					System.Drawing.Image bm = GetAlpha(pb.Image);
					bm.Save(path, ImageLoader.GetImageFormat(path));
				}
				catch (Exception ex)
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
				}
			}
		}

		private async void ImportAlpha(object sender, System.EventArgs e)
		{
			if (lbimg.SelectedIndex<0) return;

			var topLevel = TopLevel.GetTopLevel(this);
			if (topLevel == null) return;
			var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
			{
				Title = "Import Alpha Channel",
				AllowMultiple = false,
				FileTypeFilter = new[] { new FilePickerFileType("Image Files") { Patterns = new[] { "*.png", "*.bmp", "*.gif", "*.jpg" } } }
			});
			if (files.Count > 0)
			{
				try
				{
					ImageData id = (ImageData)cbitem.Items[cbitem.SelectedIndex];
					System.IO.Stream s = System.IO.File.OpenRead(files[0].Path.LocalPath);
					System.Drawing.Image img = Helper.LoadImage(s);
					s.Close();
					s.Dispose();
					s = null;

					img = this.CropImage(id, img);
					if (img==null) return;

					lbimg.Tag = true;
					MipMap mm = (MipMap)lbimg.Items[lbimg.SelectedIndex];
					mm.LifoFile = "";
					mm.Texture = this.ChangeAlpha(mm.Texture, img);
					pb.Image = mm.Texture;
					lbimg.Items[lbimg.SelectedIndex] = mm;
				}
				catch (Exception ex)
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("erropenfile"), ex);
				}
				finally
				{
					lbimg.Tag = null;
				}
			}
		}

		private void Changedlevel(object sender, System.EventArgs e)
		{
			if (cbitem.Tag!=null) return;
			if (cbitem.SelectedIndex<0) return;
			try
			{
				cbitem.Tag = true;
				ImageData selecteditem = (ImageData)cbitem.Items[cbitem.SelectedIndex];
				selecteditem.MipMapLevels = Convert.ToUInt32(tblevel.Text);
				cbitem.Items[cbitem.SelectedIndex] = selecteditem;
				// cbitem.Text not available in Avalonia ComboBox — display updates via selected item
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("erropenfile"), ex);
			}

			finally
			{
				cbitem.Tag = null;
			}
		}

		private void BuildFilename(object sender, EventArgs e)
		{
			string fl = Hashes.StripHashFromName(this.tbflname.Text);
			this.tbflname.Text = Hashes.AssembleHashedFileName(wrapper.Package.FileGroupHash, fl);
		}

		private void FixTGI(object sender, EventArgs e)
		{
			string fl = Hashes.StripHashFromName(this.tbflname.Text);
			wrapper.FileDescriptor.Instance = Hashes.InstanceHash(fl);
			wrapper.FileDescriptor.SubType = Hashes.SubTypeHash(fl);
		}

		protected Interfaces.Files.IPackedFileDescriptor GetLocalLifo(MipMap mm)
		{
			if (mm.Texture==null)
			{
				uint st = Hashes.SubTypeHash(mm.LifoFile);
				uint inst = Hashes.InstanceHash(mm.LifoFile);

				Interfaces.Files.IPackedFileDescriptor pfd = wrapper.Package.FindFile(0xED534136, st, wrapper.FileDescriptor.Group, inst);
				return pfd;
			}

			return null;
		}

		private void ContextPopUp(object sender, System.ComponentModel.CancelEventArgs e)
		{
			milifo.IsEnabled = false;
            this.mibuild.IsEnabled = System.IO.File.Exists(PathProvider.Global.NvidiaDDSTool);
			if (lbimg.SelectedIndex<0) return;
			try
			{
				if (lbimg.SelectedIndex>=0)
				{
					MipMap mm = (MipMap)lbimg.Items[lbimg.SelectedIndex];
					Interfaces.Files.IPackedFileDescriptor pfd = GetLocalLifo(mm);
					milifo.IsEnabled = (pfd != null);
				}
				else
				{
					milifo.IsEnabled = false;
				}
                mibuild.IsEnabled = (System.IO.File.Exists(PathProvider.Global.NvidiaDDSTool));
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		private void ImportLifo(object sender, System.EventArgs e)
		{
			if (lbimg.SelectedIndex<0) return;
			try
			{
				cbitem.Tag = true;
				MipMap mm = (MipMap)lbimg.Items[lbimg.SelectedIndex];
				Interfaces.Files.IPackedFileDescriptor pfd = GetLocalLifo(mm);
				Lifo lifo = new Lifo(null, false);
				lifo.ProcessData(pfd, wrapper.Package);
				mm.Texture = null;
				mm.Data = ((LevelInfo)lifo.Blocks[0]).Data;
				pb.Image = mm.Texture;
				lbimg.Items[lbimg.SelectedIndex] = mm;
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
			finally
			{
				cbitem.Tag = null;
			}
		}

		private async void BuildMipMap(object sender, EventArgs e)
		{
			try
			{
				Size sz = await SimPe.Plugin.ImageSize.Execute(SelectedImageData().TextureSize);
				cbitem.Tag = true;
				lbimg.Items.Clear();
				int wd = 1;
				int hg = 1;

				int levels = Convert.ToInt32(tblevel.Text);
				for (int i=0; i<levels; i++)
				{
					MipMap mm = new MipMap(SelectedImageData());
					mm.Texture = new Bitmap(wd, hg);

					if (i==levels-1)
					{
						SelectedImageData().TextureSize = new Size(wd, hg);
					}

					if ((wd==hg) && (wd==1))
					{
						wd =  Math.Max(1, (sz.Width / Math.Max(1, sz.Height)));
						hg =  Math.Max(1, (sz.Height / Math.Max(1, sz.Width)));

						if ((wd==hg) && (wd==1))
						{
							wd *= 2; hg *= 2;
						}
					}
					else
					{
						wd *= 2; hg *= 2;
					}

					lbimg.Items.Add(mm);
				}

				UpdateMimMaps();
				if (cbitem.Tag==null)
				{
					tbwidth.Text = SelectedImageData().TextureSize.Width.ToString();
					tbheight.Text = SelectedImageData().TextureSize.Height.ToString();
					lbimg.SelectedIndex = lbimg.Items.Count-1;
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
			finally
			{
				cbitem.Tag = null;
			}
		}

		void LoadDDS(DDSData[] data)
		{
			if (data==null) return;
			if (data.Length>0)
			{
				try
				{
					cbitem.Tag = true;
					ImageData id = SelectedImageData();

					id.TextureSize = data[0].ParentSize;
					id.Format = data[0].Format;
					id.MipMapLevels = (uint)data.Length;

					this.lbimg.Items.Clear();
					for (int i=data.Length-1; i>=0; i--)
					{
						DDSData item = data[i];
						MipMap mm = new MipMap(id);
						mm.Texture = item.Texture;
						mm.Data = item.Data;

						lbimg.Items.Add(mm);
					}

					tbwidth.Text = id.TextureSize.Width.ToString();
					tbheight.Text = id.TextureSize.Height.ToString();

					this.cbformats.SelectedIndex = 0;
					for (int i=0; i<cbformats.Items.Count; i++)
					{
						if ((ImageLoader.TxtrFormats)cbformats.Items[i]==id.Format)
						{
							cbformats.SelectedIndex = i;
							break;
						}
					}
				}
				finally
				{
					cbitem.Tag = null;
				}
			}

			UpdateMimMaps();
			lbimg.SelectedIndex = lbimg.Items.Count-1;
		}

		private async void ImportDDS(object sender, System.EventArgs e)
		{
			var topLevel = TopLevel.GetTopLevel(this);
			if (topLevel == null) return;
			var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
			{
				Title = "Import DDS Texture",
				AllowMultiple = false,
				FileTypeFilter = new[] { new FilePickerFileType("DDS Texture") { Patterns = new[] { "*.dds" } } }
			});
			if (files.Count > 0)
			{
				try
				{
					cbitem.Tag = true;
					ImageData id = SelectedImageData();
					DDSData[] data = ImageLoader.ParesDDS(files[0].Path.LocalPath);

					LoadDDS(data);
				}
				catch (Exception ex)
				{
					Helper.ExceptionMessage("", ex);
				}
				finally
				{
					cbitem.Tag = null;
				}
			}
		}

		private void ChangedSize(object sender, System.EventArgs e)
		{
			if (cbitem.Tag!=null) return;
			if (cbitem.SelectedIndex<0) return;
			try
			{
				cbitem.Tag = true;
				ImageData id = (ImageData)cbitem.Items[cbitem.SelectedIndex];
				id.TextureSize = new Size(Convert.ToInt32(tbwidth.Text), Convert.ToInt32(tbheight.Text));

				BuildMipMap(null, null);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("erropenfile"), ex);
			}

			finally
			{
				cbitem.Tag = null;
			}
		}

		private void BuildDXT(object sender, System.EventArgs e)
		{
			DDSTool dds = new DDSTool();

			ImageData id = SelectedImageData();
			LoadDDS(dds.Execute(Convert.ToInt32(this.tblevel.Text), id.TextureSize, id.Format));
			id.Refresh();
		}
	}
}
