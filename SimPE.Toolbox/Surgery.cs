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
using SkiaSharp;
using SimPe.Scenegraph.Compat;
using ListView  = SimPe.Scenegraph.Compat.ListView;
using ImageList = SimPe.Scenegraph.Compat.ImageList;
using Button    = Avalonia.Controls.Button;
using Label     = SimPe.Scenegraph.Compat.LabelCompat;
using GroupBox  = SimPe.Scenegraph.Compat.GroupBox;
using PictureBox = SimPe.Scenegraph.Compat.PictureBox;
using LinkLabel = SimPe.Scenegraph.Compat.LinkLabel;
using ToolTip   = SimPe.Scenegraph.Compat.ToolTip;
using CheckBox  = Avalonia.Controls.CheckBox;
using LinkLabelLinkClickedEventArgs = SimPe.Scenegraph.Compat.LinkLabelLinkClickedEventArgs;
using ColorDepth = SimPe.Scenegraph.Compat.ColorDepth;

namespace SimPe.Plugin
{
	/// <summary>
	/// Zusammenfassung f�r Sims.
	/// </summary>
	public class Surgery : Avalonia.Controls.Window
	{
		private ImageList ilist;
		private ListView lv;
		private Button button1;
		private Label label1;
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private Label label2;
		private Label label3;
		private PictureBox pbpatient;
		private PictureBox pbarche;
		private LinkLabel llusepatient;
		private LinkLabel llusearche;
		private Label lbpatname;
		private Label lbpatlife;
		private Label lbarchlife;
		private Label lbarchname;
		private LinkLabel llexport;
		private SimPe.Scenegraph.Compat.SaveFileDialogCompat sfd;
		private ToolTip toolTip1;
		private CheckBox cbskin;
		private GroupBox groupBox3;
		private ListView lvskin;
		private ImageList iskin;
		private CheckBox cbface;
		private CheckBox cbmakeup;
		private CheckBox cbeye;
		private System.ComponentModel.IContainer components;

		public Surgery()
		{
			//
			// Erforderlich f�r die Windows Form-Designerunterst�tzung
			//
			InitializeComponent();

			LoadSkins();
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
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
            this.Title  = "Sims Surgery Tool";
            this.Width  = 1260;
            this.Height = 589;

            this.components = new System.ComponentModel.Container();
            this.sfd        = new SimPe.Scenegraph.Compat.SaveFileDialogCompat();
            this.toolTip1   = new ToolTip();

            // ── Image lists ──────────────────────────────────────────────────────
            this.ilist = new ImageList { ColorDepth = ColorDepth.Depth32Bit };
            this.ilist.ImageSize = new System.Drawing.Size(64, 64);
            this.iskin = new ImageList { ColorDepth = ColorDepth.Depth32Bit };
            this.iskin.ImageSize = new System.Drawing.Size(48, 48);

            // ── Sim pool list view ───────────────────────────────────────────────
            this.lv = new ListView();
            this.lv.HideSelection = false;
            this.lv.LargeImageList = this.ilist;
            this.lv.MultiSelect = false;
            this.lv.SmallImageList = this.ilist;
            this.lv.Sorting = SimPe.SortOrder.Ascending;
            this.lv.StateImageList = this.ilist;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.SelectedIndexChanged += this.SelectSim;
            this.lv.DoubleClick += this.Open;

            // ── Button ───────────────────────────────────────────────────────────
            this.button1 = new Button { Content = "Surgery", IsEnabled = false };
            this.button1.Click += (s, e) => Open(s, e);

            // ── Patient labels + picture ─────────────────────────────────────────
            this.label1    = new Label { Text = "Sim Pool:" };
            this.label2    = new Label { Text = "The look of this Sim will be changed." };
            this.lbpatname = new Label { Text = "Name" };
            this.lbpatlife = new Label { Text = "Lifestage" };
            this.pbpatient = new PictureBox { Width = 96, Height = 96 };
            this.llusepatient = new LinkLabel { Text = "use", IsEnabled = false };
            this.llusepatient.LinkClicked += (s, e) => UsePatient(s, e);
            this.llexport = new LinkLabel { Text = "Export (thanks to Pinhead)", IsEnabled = false };
            this.llexport.LinkClicked += (s, e) => Export(s, e);
            this.cbface   = new CheckBox { Content = "Face only" };
            this.cbmakeup = new CheckBox { Content = "Makeup only" };
            this.cbeye    = new CheckBox { Content = "Eyes only" };
            this.cbface.IsCheckedChanged   += (s, e) => cbskin_CheckedChanged(s, EventArgs.Empty);
            this.cbmakeup.IsCheckedChanged += (s, e) => cbskin_CheckedChanged(s, EventArgs.Empty);
            this.cbeye.IsCheckedChanged    += (s, e) => cbskin_CheckedChanged(s, EventArgs.Empty);

            // ── Archetype labels + picture ────────────────────────────────────────
            this.label3    = new Label { Text = "That's the way the Sim will look like afterwards." };
            this.lbarchname = new Label { Text = "Name" };
            this.lbarchlife = new Label { Text = "Lifestage" };
            this.pbarche   = new PictureBox { Width = 96, Height = 96 };
            this.llusearche = new LinkLabel { Text = "use", IsEnabled = false };
            this.llusearche.LinkClicked += (s, e) => UseArchetype(s, e);

            // ── Skin options ──────────────────────────────────────────────────────
            this.cbskin = new CheckBox { Content = "Skintone only" };
            this.cbskin.IsCheckedChanged += (s, e) => cbskin_CheckedChanged(s, EventArgs.Empty);
            this.lvskin = new ListView();
            this.lvskin.HideSelection = false;
            this.lvskin.LargeImageList = this.iskin;
            this.lvskin.MultiSelect = false;
            this.lvskin.UseCompatibleStateImageBehavior = false;
            this.lvskin.SelectedIndexChanged += this.lvskin_SelectedIndexChanged;

            // ── GroupBox contents ─────────────────────────────────────────────────
            this.groupBox1 = new GroupBox { Text = "Patient Sim" };
            var gb1inner = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical, Spacing = 2 };
            var gb1top = new Avalonia.Controls.StackPanel { Orientation = Orientation.Horizontal, Spacing = 4 };
            gb1top.Children.Add(this.pbpatient);
            var gb1info = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical, Spacing = 2 };
            gb1info.Children.Add(this.lbpatname);
            gb1info.Children.Add(this.lbpatlife);
            gb1info.Children.Add(this.llusepatient);
            gb1info.Children.Add(this.llexport);
            gb1top.Children.Add(gb1info);
            gb1inner.Children.Add(gb1top);
            gb1inner.Children.Add(this.label2);
            var gb1checks = new Avalonia.Controls.StackPanel { Orientation = Orientation.Horizontal, Spacing = 4 };
            gb1checks.Children.Add(this.cbface);
            gb1checks.Children.Add(this.cbmakeup);
            gb1checks.Children.Add(this.cbeye);
            gb1inner.Children.Add(gb1checks);
            this.groupBox1.Content = gb1inner;

            this.groupBox2 = new GroupBox { Text = "Archetype Sim" };
            var gb2inner = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical, Spacing = 2 };
            var gb2top = new Avalonia.Controls.StackPanel { Orientation = Orientation.Horizontal, Spacing = 4 };
            gb2top.Children.Add(this.pbarche);
            var gb2info = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical, Spacing = 2 };
            gb2info.Children.Add(this.lbarchname);
            gb2info.Children.Add(this.lbarchlife);
            gb2info.Children.Add(this.llusearche);
            gb2top.Children.Add(gb2info);
            gb2inner.Children.Add(gb2top);
            gb2inner.Children.Add(this.label3);
            this.groupBox2.Content = gb2inner;

            this.groupBox3 = new GroupBox { Text = "Skin Options" };
            var gb3inner = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical, Spacing = 2 };
            gb3inner.Children.Add(this.cbskin);
            gb3inner.Children.Add(this.lvskin);
            this.groupBox3.Content = gb3inner;

            // ── Root layout ───────────────────────────────────────────────────────
            var rightPanel = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical, Spacing = 4 };
            rightPanel.Children.Add(this.groupBox1);
            rightPanel.Children.Add(this.groupBox2);
            rightPanel.Children.Add(this.groupBox3);
            rightPanel.Children.Add(this.button1);

            var leftPanel = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical, Spacing = 2 };
            leftPanel.Children.Add(this.label1);
            leftPanel.Children.Add(this.lv);

            var root = new Avalonia.Controls.Grid();
            root.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition(252, Avalonia.Controls.GridUnitType.Pixel));
            root.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition(1, Avalonia.Controls.GridUnitType.Star));
            Avalonia.Controls.Grid.SetColumn(leftPanel,  0);
            Avalonia.Controls.Grid.SetColumn(rightPanel, 1);
            root.Children.Add(leftPanel);
            root.Children.Add(rightPanel);

            this.Content = root;
		}
		#endregion

		protected void AddImage(SimPe.PackedFiles.Wrapper.ExtSDesc sdesc) 
		{
			if (sdesc.Image!=null) 
			{
				if ((sdesc.Unlinked!=0x00) || (!sdesc.AvailableCharacterData) || sdesc.IsNPC)
				{
					SKBitmap skBmp;
					if (sdesc.Image is SKBitmap existingBmp)
						skBmp = existingBmp.Copy();
					else
						skBmp = new SKBitmap(1, 1);
					using (var canvas = new SKCanvas(skBmp))
					{
						var sc = Data.MetaData.SpecialSimColor;
						using (var bgPaint = new SKPaint { Color = new SKColor(sc.R, sc.G, sc.B, sc.A), Style = SKPaintStyle.Fill })
							canvas.DrawRect(0, 0, skBmp.Width, skBmp.Height, bgPaint);

						int pos = 2;
						if (sdesc.Unlinked!=0x00)
						{
							var c = Data.MetaData.UnlinkedSim;
							using (var paint = new SKPaint { Color = new SKColor(c.R, c.G, c.B, c.A), Style = SKPaintStyle.Fill })
								canvas.DrawRect(new SKRect(pos, 2, pos + 25, 27), paint);
							pos += 28;
						}
						if (!sdesc.AvailableCharacterData)
						{
							var c = Data.MetaData.InactiveSim;
							using (var paint = new SKPaint { Color = new SKColor(c.R, c.G, c.B, c.A), Style = SKPaintStyle.Fill })
								canvas.DrawRect(new SKRect(pos, 2, pos + 25, 27), paint);
							pos += 28;
						}
						if (sdesc.IsNPC)
						{
							var c = Data.MetaData.NPCSim;
							using (var paint = new SKPaint { Color = new SKColor(c.R, c.G, c.B, c.A), Style = SKPaintStyle.Fill })
								canvas.DrawRect(new SKRect(pos, 2, pos + 25, 27), paint);
							pos += 28;
						}
					}

					this.ilist.Images.Add(skBmp);
				} 
				else 
				{
					this.ilist.Images.Add(sdesc.Image as SkiaSharp.SKBitmap);
				}
			} 
			else 
			{
				this.ilist.Images.Add(SKBitmap.Decode(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.Network.png")));
			}
		}

		protected void AddSim(SimPe.PackedFiles.Wrapper.ExtSDesc sdesc) 
		{
			//if (!sdesc.HasImage) return;
			if (!sdesc.AvailableCharacterData) return;
#if DEBUG
#else
			if (sdesc.IsNPC) return;
#endif
			

			AddImage(sdesc);
			ListViewItem lvi = new ListViewItem();
			lvi.Text = sdesc.SimName +" "+sdesc.SimFamilyName;
			lvi.ImageIndex = ilist.Images.Count -1;
			lvi.Tag = sdesc;

			lv.Items.Add(lvi);
		}

		Hashtable skinfiles;
		void LoadSkins()
		{
			WaitingScreen.Wait();
			try 
			{
				skinfiles = new Hashtable();
				ArrayList tones = new ArrayList();
				iskin.Images.Add(new SKBitmap(iskin.ImageSize.Width, iskin.ImageSize.Height));
				ListViewItem lvia = new ListViewItem("* from Archetype");
				lvia.ImageIndex = 0;
				this.lvskin.Items.Add(lvia);
				lvia.Selected = true;

				FileTable.FileIndex.Load();
				SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem[] items = FileTable.FileIndex.FindFile(Data.MetaData.GZPS, true);				
				foreach (SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem item in items)
				{
					SimPe.PackedFiles.Wrapper.Cpf skin = new SimPe.PackedFiles.Wrapper.Cpf();
					skin.ProcessData(item);

					//Maintain a List of all availabe SkinsFiles per skintone
					ArrayList files = null;
					string st = skin.GetSaveItem("skintone").StringValue;
					if (skinfiles.ContainsKey(st)) 
					{
						files = (ArrayList)skinfiles[st];
					}
					else 
					{
						files = new ArrayList();
						skinfiles[st] = files;
					}
					files.Add(skin);

					if ((skin.GetSaveItem("override0subset").StringValue=="top") && (skin.GetSaveItem("type").StringValue=="skin") && ((skin.GetSaveItem("category").UIntegerValue&(uint)Data.SkinCategories.Skin)==(uint)Data.SkinCategories.Skin))
					{
						WaitingScreen.UpdateMessage(skin.GetSaveItem("name").StringValue);

						if (tones.Contains(st)) continue;
						else tones.Add(st);

						SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem[] idr = FileTable.FileIndex.FindFile(0xAC506764, item.FileDescriptor.Group, item.FileDescriptor.LongInstance, null);
						if (idr.Length>0) 
						{
							SimPe.Plugin.RefFile reffile = new RefFile();
							reffile.ProcessData(idr[0]);

							ListViewItem lvi = new ListViewItem(skin.GetSaveItem("name").StringValue);
							if (Helper.DebugMode) lvi.Text += " ("+skin.GetSaveItem("skintone").StringValue+")";
							lvi.Tag = skin.GetSaveItem("skintone").StringValue;
							foreach (Interfaces.Files.IPackedFileDescriptor pfd in reffile.Items) 
							{								
								if (pfd.Type == Data.MetaData.TXMT) 
								{
									SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem[] txmts = FileTable.FileIndex.FindFile(pfd, null);
									if (txmts.Length>0) 
									{
										SimPe.Plugin.Rcol rcol = new GenericRcol(null, false);
										rcol.ProcessData(txmts[0]);

										MaterialDefinition md = (MaterialDefinition)rcol.Blocks[0];
										string txtrname = md.FindProperty("stdMatBaseTextureName").Value+"_txtr";

										SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem txtri = FileTable.FileIndex.FindFileByName(txtrname, Data.MetaData.TXTR, Data.MetaData.LOCAL_GROUP, true);
										if (txtri!=null) 
										{
											rcol = new GenericRcol(null, false);
											rcol.ProcessData(txtri);

											ImageData id = (ImageData)rcol.Blocks[0];
											MipMap mm = id.GetLargestTexture(iskin.ImageSize);

											if (mm!=null) 
											{
												// mm.Texture is SKBitmap; iskin.Images.Add takes System.Drawing.Image — skip preview
											iskin.Images.Add((SkiaSharp.SKBitmap)null);
												lvi.ImageIndex = iskin.Images.Count-1;
											}
										}
									
									}
								}
							} //foreach reffile.Items
							
							lvskin.Items.Add(lvi);
						} //if idr
					}
				} //foreach items
			} 
			finally 
			{
				WaitingScreen.Stop();
			}
		}

		SimPe.Interfaces.Files.IPackedFileDescriptor pfd;
		Interfaces.IProviderRegistry prov;
		SimPe.Interfaces.Files.IPackageFile ngbh;
		public Interfaces.Plugin.IToolResult Execute(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package, Interfaces.IProviderRegistry prov) 
		{
			this.Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Wait);
			
			this.pfd = null;
			this.prov = prov;
			this.ngbh = package;

			this.pbarche.Image = null;
			this.pbpatient.Image = null;

			this.lbpatlife.Text = "Lifestage";
			this.lbpatname.Text = "Name";

			this.lbarchlife.Text = "Lifestage";
			this.lbarchname.Text = "Name";

			this.spatient = null;
			this.sarche = null;

			button1.IsEnabled = CanDo();

			ilist.Images.Clear();
			lv.Items.Clear();

			

			Interfaces.Files.IPackedFileDescriptor[] pfds = package.FindFiles(Data.MetaData.SIM_DESCRIPTION_FILE);
			WaitingScreen.Wait();
            try
            {
                foreach (Interfaces.Files.IPackedFileDescriptor spfd in pfds)
                {

                    PackedFiles.Wrapper.ExtSDesc sdesc = new SimPe.PackedFiles.Wrapper.ExtSDesc();
                    sdesc.ProcessData(spfd, package);

                    //WaitingScreen.UpdateImage(ImageLoader.Preview(sdesc.Image, new Size(64, 64)));
                    AddSim(sdesc);
                }

                this.Cursor = null;
                this.llusearche.Enabled = false;
                this.llusepatient.Enabled = false;
                this.llexport.Enabled = false;
                button1.IsEnabled = false;
                if (lv.Items.Count > 0) lv.Items[0].Selected = true;



            }
            finally { WaitingScreen.Stop(this); }
			RemoteControl.ShowSubForm(this);

			if (this.pfd!=null) pfd = this.pfd;
			return new Plugin.ToolResult((this.pfd!=null), false);
		}

		private void Open(object sender, System.EventArgs e)
		{
			if (!CanDo())  return;
			

			SimPe.Packages.File patient = SimPe.Packages.File.LoadFromFile(spatient.CharacterFileName);
			SimPe.Packages.File archetype = null;
			if (sarche!=null) archetype = SimPe.Packages.File.LoadFromFile(sarche.CharacterFileName);
			else archetype = SimPe.Packages.File.LoadFromFile(null);

			SimPe.Packages.GeneratableFile newpackage = null;
			PlasticSurgery ps = new PlasticSurgery(ngbh, patient, spatient, archetype, sarche);

			if (this.cbskin.IsChecked != true && this.cbface.IsChecked != true && this.cbmakeup.IsChecked != true && this.cbeye.IsChecked != true) newpackage = ps.CloneSim();

			if (this.cbskin.IsChecked == true)
			{
				if (lvskin.SelectedItems.Count==0) return;
				string skin = (string)lvskin.SelectedItems[0].Tag;
				if (skin==null) newpackage = ps.CloneSkinTone(skinfiles);
				else newpackage = ps.CloneSkinTone(skin, skinfiles);
			}

			if (this.cbface.IsChecked == true)
			{
				if (this.cbskin.IsChecked == true) ps = new PlasticSurgery(ngbh, newpackage, spatient, archetype, sarche);
				newpackage = ps.CloneFace();
			}

			if (this.cbmakeup.IsChecked == true)
			{
				if (this.cbskin.IsChecked == true || this.cbface.IsChecked == true) ps = new PlasticSurgery(ngbh, newpackage, spatient, archetype, sarche);
				newpackage = ps.CloneMakeup(false, true);
			}

			if (this.cbeye.IsChecked == true)
			{
				if (this.cbskin.IsChecked == true || this.cbface.IsChecked == true || this.cbmakeup.IsChecked == true) ps = new PlasticSurgery(ngbh, newpackage, spatient, archetype, sarche);
				newpackage = ps.CloneMakeup(true, false);
			}
			

			if (newpackage != null) 
			{
				newpackage.Save(spatient.CharacterFileName);
				prov.SimNameProvider.StoredData = null;
				Close();
			}
		}

		private void SelectSim(object sender, System.EventArgs e)
		{
			this.llusearche.Enabled = false;
			this.llusepatient.Enabled = false;
			if (lv.SelectedItems.Count==0) return;
			this.llusearche.Enabled = true;


			this.llusepatient.Enabled = !((SimPe.PackedFiles.Wrapper.ExtSDesc)lv.SelectedItems[0].Tag).IsNPC;
		}

		SimPe.PackedFiles.Wrapper.SDesc spatient = null;
		SimPe.PackedFiles.Wrapper.SDesc sarche = null;
		private void UsePatient(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.llexport.Enabled = (spatient!=null);
			if (lv.SelectedItems.Count==0) return;
			if (lv.SelectedItems[0].ImageIndex>=0) pbpatient.Image = ilist.Images[lv.SelectedItems[0].ImageIndex];

			this.lbpatname.Text = lv.SelectedItems[0].Text;
			
			spatient = (SimPe.PackedFiles.Wrapper.SDesc)lv.SelectedItems[0].Tag;
			lbpatlife.Text = spatient.CharacterDescription.LifeSection.ToString();
			lbpatlife.Text += ", " + spatient.CharacterDescription.Gender.ToString();

			button1.IsEnabled = (pbpatient.Image!=null) && (pbarche.Image!=null);
			pfd = (SimPe.Interfaces.Files.IPackedFileDescriptor)spatient.FileDescriptor;
			this.llexport.Enabled = (spatient!=null);
		}

		private void UseArchetype(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (lv.SelectedItems.Count==0) return;
			if (lv.SelectedItems[0].ImageIndex>=0) this.pbarche.Image = ilist.Images[lv.SelectedItems[0].ImageIndex];

			// pbarche.Image is System.Drawing.Image; Preview returns SKBitmap — skip preview
			iskin.Images[0] = pbarche.Image as SkiaSharp.SKBitmap;
			lvskin.Refresh();

			this.lbarchname.Text = lv.SelectedItems[0].Text;
			
			sarche = (SimPe.PackedFiles.Wrapper.SDesc)lv.SelectedItems[0].Tag;
			lbarchlife.Text = sarche.CharacterDescription.LifeSection.ToString();
			lbarchlife.Text += ", " + sarche.CharacterDescription.Gender.ToString();

			button1.IsEnabled = (pbpatient.Image!=null) && (pbarche.Image!=null);
		}

		protected void FaceSurgery()
		{
			try 
			{
				SimPe.Packages.GeneratableFile patient = SimPe.Packages.GeneratableFile.LoadFromFile(spatient.CharacterFileName);
				SimPe.Packages.File archetype = SimPe.Packages.File.LoadFromFile(sarche.CharacterFileName);

				//Load Facial Data
				Interfaces.Files.IPackedFileDescriptor[] apfds = archetype.FindFiles(0xCCCEF852); //LxNR, Face
				if (apfds.Length==0) return;
				Interfaces.Files.IPackedFile file = archetype.Read(apfds[0]);

				Interfaces.Files.IPackedFileDescriptor[] ppfds = patient.FindFiles(0xCCCEF852); //LxNR, Face
				if (ppfds.Length==0) return;

				ppfds[0].UserData = file.UncompressedData;

#if DEBUG
				//Load Shape Data
				/*apfds = archetype.FindFiles(0xFC6EB1F7); //SHPE
				if (apfds.Length==0) return;
				file = archetype.Read(apfds[0]);

				ppfds = patient.FindFiles(0xFC6EB1F7); //SHPE
				if (ppfds.Length==0) return;

				ppfds[0].UserData = file.UncompressedData;*/
#endif
				
				//System.IO.MemoryStream ms = patient.Build();
				//patient.Reader.Close();
				patient.Save(spatient.CharacterFileName);
			} 
			catch (Exception ex)
			{
				Helper.ExceptionMessage("Unable to update the new Character Package.", ex);
			}
		}

		private void Export(object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (spatient==null) return;


			try 
			{
				//list of all Files top copy from the Archetype
				ArrayList list = new ArrayList();
				list.Add((uint)0xAC506764); //3IDR
				list.Add((uint)0xE519C933); //CRES
				list.Add((uint)0xEBCF3E27); //GZPS, Property Set
				list.Add((uint)0xAC598EAC); //AGED
				list.Add((uint)0xCCCEF852); //LxNR, Face
				list.Add((uint)0x0C560F39); //BINX
				list.Add((uint)0xAC4F8687); //GMDC
				list.Add((uint)0x7BA3838C); //GMND				
				list.Add((uint)0x49596978); //MATD
				list.Add((uint)0xFC6EB1F7); //SHPE

				System.IO.BinaryReader br1 = new System.IO.BinaryReader(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.3d.simpe"));
				System.IO.BinaryReader br2 = new System.IO.BinaryReader(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.bin.simpe"));

				SimPe.Packages.PackedFileDescriptor pfd1 = new SimPe.Packages.PackedFileDescriptor();
				pfd1.Group = 0xffffffff; pfd1.SubType = 0x00000000; pfd1.Instance = 0xFF123456; pfd1.Type = 0xAC506764; //3IDR
				pfd1.UserData = br1.ReadBytes((int)br1.BaseStream.Length);

				SimPe.Packages.PackedFileDescriptor pfd2 = new SimPe.Packages.PackedFileDescriptor();
				pfd2.Group = 0xffffffff; pfd2.SubType = 0x00000000; pfd2.Instance = 0xFF123456; pfd2.Type = 0x0C560F39; //BINX
				pfd2.UserData = br2.ReadBytes((int)br2.BaseStream.Length);

                sfd.InitialDirectory = System.IO.Path.Combine(PathProvider.SimSavegameFolder, "SavedSims");
				sfd.FileName = System.IO.Path.GetFileNameWithoutExtension(spatient.CharacterFileName);

				SimPe.Packages.GeneratableFile source = SimPe.Packages.GeneratableFile.LoadFromFile(spatient.CharacterFileName);
				if (sfd.ShowDialog()==DialogResult.OK) 
				{
					SimPe.Packages.GeneratableFile patient = SimPe.Packages.GeneratableFile.LoadFromStream((System.IO.BinaryReader)null);
					patient.FileName = "";
					patient.Add(pfd1);
					patient.Add(pfd2);

					foreach (Interfaces.Files.IPackedFileDescriptor pfd in source.Index) 
					{
						if (list.Contains(pfd.Type)) 
						{
							Interfaces.Files.IPackedFile file = source.Read(pfd);
							pfd.UserData = file.UncompressedData;
							patient.Add(pfd);

							if ((pfd.Type == Data.MetaData.GZPS) || (pfd.Type == 0xAC598EAC)) //property set and 3IDR
							{
								SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
								cpf.ProcessData(pfd, patient);

								SimPe.PackedFiles.Wrapper.CpfItem ci = new SimPe.PackedFiles.Wrapper.CpfItem();
								ci.Name = "product";
								ci.UIntegerValue = 0;
								cpf.AddItem(ci, false);

								ci = cpf.GetItem("version");
								if (ci==null) 
								{
									ci = new SimPe.PackedFiles.Wrapper.CpfItem();
									ci.Name = "version";
									if ((cpf.GetSaveItem("age").UIntegerValue&(uint)Data.Ages.YoungAdult)!=0) ci.UIntegerValue = 2;
									else ci.UIntegerValue = 1;
									cpf.AddItem(ci);
								}
								

								cpf.SynchronizeUserData();
							}
						}
					}

					patient.Save(sfd.FileName);
				}
			} 
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
			
		}

		bool CanDo()
		{
			if (spatient == null) return false;

			bool ret = true;
			if (cbskin.IsChecked == true)
			{
				ret = (lvskin.SelectedItems.Count==1);
				if (ret) if (lv.Items[0].Selected && (sarche == null)) ret=false;
			}

			if (cbskin.IsChecked != true || cbface.IsChecked == true || cbmakeup.IsChecked == true || cbeye.IsChecked == true)
			{
				ret = ret && (sarche != null);
			}

			return ret;
		}

		private void cbskin_CheckedChanged(object sender, System.EventArgs e)
		{
			lvskin.Enabled = this.cbskin.IsChecked == true;
			lvskin_SelectedIndexChanged(null, null);
			button1.IsEnabled = CanDo();
		}

		private void lvskin_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			button1.IsEnabled = CanDo();
		}
	}
}
