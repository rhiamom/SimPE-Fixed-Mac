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
using ColumnHeader = SimPe.Scenegraph.Compat.ColumnHeader;
using ImageList = SimPe.Scenegraph.Compat.ImageList;
using CheckBox  = Avalonia.Controls.CheckBox;
using Button    = Avalonia.Controls.Button;
using Label     = Avalonia.Controls.TextBlock;
using Panel     = SimPe.Scenegraph.Compat.PanelCompat;
using SortOrder = SimPe.SortOrder;
using ColumnClickEventArgs = SimPe.Scenegraph.Compat.ColumnClickEventArgs;
using FlowLayoutPanel = SimPe.Scenegraph.Compat.FlowLayoutPanel;
using ToolTip = SimPe.Scenegraph.Compat.ToolTip;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for Sims.
	/// </summary>
	public class Sims : Avalonia.Controls.Window
	{
		private ImageList ilist;
		private ListView lv;
		private Button button1;
        private ToolTip toolTip1;
        private ImageList iListSmall;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
		private ColumnHeader columnHeader4;
		private ColumnHeader columnHeader5;
		private ColumnHeader columnHeader6;
		private ColumnHeader columnHeader7;
		private ColumnHeader columnHeader8;
		private ColumnHeader columnHeader9;
        private Label lbUbi;
        private Panel panel1;
        private Label label1;
        private Label label2;
        private Panel panel2;
        private Label label3;
        private Panel panel3;
		private ColumnHeader chKind;
		private ColumnHeader columnHeader10;
		private System.ComponentModel.IContainer components;
        private FlowLayoutPanel flowLayoutPanel1;
        internal CheckBox cbNpc;
        internal CheckBox cbTownie;
        internal CheckBox ckbPlayable;
        internal CheckBox cbdetail;
        internal CheckBox ckbUnEditable;
        internal CheckBox cbgals;
        internal CheckBox cbmens;
        internal CheckBox cbadults;
        private ColumnHeader columnHeader11;

		SimsRegistry reg;
		public Sims()
		{
			//
			// Required designer variable.
			//
			InitializeComponent();
			sorter = new ColumnSorter();

			reg = new SimsRegistry(this);
            
            if (UseBigIcons) this.ilist.ImageSize = new System.Drawing.Size(96, 96);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected virtual void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (reg!=null) reg.Dispose();
				reg = null;
				if(components != null)
				{
					components.Dispose();
				}
			}
		}

		#region Avalonia layout
		private void InitializeComponent()
		{
            this.Title  = "Sim Browser";
            this.Width  = 1250;
            this.Height = 620;

            this.components = new System.ComponentModel.Container();

            // ── Image lists ──────────────────────────────────────────────────────
            this.ilist      = new ImageList { ColorDepth = ColorDepth.Depth32Bit };
            this.iListSmall = new ImageList { ColorDepth = ColorDepth.Depth32Bit };

            // ── Column headers ───────────────────────────────────────────────────
            this.columnHeader1  = new ColumnHeader { Text = "Name",      Width = 250 };
            this.columnHeader2  = new ColumnHeader { Text = "Household", Width = 150 };
            this.columnHeader3  = new ColumnHeader { Text = "Lifestage", Width =  70 };
            this.chKind         = new ColumnHeader { Text = "Kind",      Width =  83 };
            this.columnHeader10 = new ColumnHeader { Text = "Gender",    Width =  66 };
            this.columnHeader4  = new ColumnHeader { Text = "Uni",       Width =  42 };
            this.columnHeader5  = new ColumnHeader { Text = "Instance",  Width =  90 };
            this.columnHeader6  = new ColumnHeader { Text = "Available", Width =  80 };
            this.columnHeader9  = new ColumnHeader { Text = "GUID",      Width =  90 };
            this.columnHeader7  = new ColumnHeader { Text = "Filename",  Width = 134 };
            this.columnHeader8  = new ColumnHeader { Text = "Filesize",  Width =  90 };
            this.columnHeader11 = new ColumnHeader { Text = "Species",   Width =  76 };

            // ── ListView ─────────────────────────────────────────────────────────
            this.lv = new ListView();
            this.lv.Columns.AddRange(new ColumnHeader[] {
                this.columnHeader1, this.columnHeader2, this.columnHeader3,
                this.chKind, this.columnHeader10, this.columnHeader4,
                this.columnHeader5, this.columnHeader6, this.columnHeader9,
                this.columnHeader7, this.columnHeader8, this.columnHeader11
            });
            this.lv.FullRowSelect = true;
            this.lv.HideSelection = false;
            this.lv.LargeImageList = this.ilist;
            this.lv.MultiSelect = false;
            this.lv.SmallImageList = this.iListSmall;
            this.lv.StateImageList = this.iListSmall;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = View.Details;
            this.lv.ColumnClick += this.SortList;
            this.lv.DoubleClick += this.Open;

            // ── Button ───────────────────────────────────────────────────────────
            this.button1 = new Button { Content = "Open" };
            this.button1.Click += (s, e) => Open(s, e);

            // ── ToolTip ──────────────────────────────────────────────────────────
            this.toolTip1 = new ToolTip();

            // ── lbUbi (special neighbourhood warning) ────────────────────────────
            this.lbUbi = new Label
            {
                Text = "This is a special purpose Neighborhood, please open the main Neighborhood to Edit the Sims that live here.",
                IsVisible = false,
                Foreground = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Colors.Brown)
            };

            // ── Legend panels + labels (hidden by default) ────────────────────────
            this.panel1 = new Panel { Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.Parse("#4682B4")), IsVisible = false };
            this.label1 = new Label { Text = "Unlinked Character", IsVisible = false };
            this.panel2 = new Panel { Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.Parse("#F08080")), IsVisible = false };
            this.label2 = new Label { Text = "No Character Data",  IsVisible = false };
            this.panel3 = new Panel { Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.Parse("#9ACD32")), IsVisible = false };
            this.label3 = new Label { Text = "NPC Unique",         IsVisible = false };

            // ── Filter checkboxes ─────────────────────────────────────────────────
            this.ckbPlayable   = new CheckBox { Content = "Playable",         IsChecked = true };
            this.cbTownie      = new CheckBox { Content = "Townies" };
            this.cbNpc         = new CheckBox { Content = "Service Sims" };
            this.ckbUnEditable = new CheckBox { Content = "No Family" };
            this.cbgals        = new CheckBox { Content = "Just Girls" };
            this.cbmens        = new CheckBox { Content = "Just Boys" };
            this.cbadults      = new CheckBox { Content = "Adults Only" };

            this.ckbPlayable.IsCheckedChanged   += (s, e) => ckbFilter_CheckedChanged(s, EventArgs.Empty);
            this.cbTownie.IsCheckedChanged      += (s, e) => ckbFilter_CheckedChanged(s, EventArgs.Empty);
            this.cbNpc.IsCheckedChanged         += (s, e) => ckbFilter_CheckedChanged(s, EventArgs.Empty);
            this.ckbUnEditable.IsCheckedChanged += (s, e) => ckbFilter_CheckedChanged(s, EventArgs.Empty);
            this.cbgals.IsCheckedChanged        += (s, e) => ckbFilter_CheckedChanged(s, EventArgs.Empty);
            this.cbmens.IsCheckedChanged        += (s, e) => ckbFilter_CheckedChanged(s, EventArgs.Empty);
            this.cbadults.IsCheckedChanged      += (s, e) => ckbFilter_CheckedChanged(s, EventArgs.Empty);

            // ── Show Sim Details checkbox ─────────────────────────────────────────
            this.cbdetail = new CheckBox { Content = "Show Sim Details", IsChecked = true };
            this.cbdetail.IsCheckedChanged += (s, e) => checkBox1_CheckedChanged(s, EventArgs.Empty);

            // ── Filter bar (horizontal row) ───────────────────────────────────────
            this.flowLayoutPanel1 = new FlowLayoutPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
            this.flowLayoutPanel1.Children.Add(this.ckbPlayable);
            this.flowLayoutPanel1.Children.Add(this.cbTownie);
            this.flowLayoutPanel1.Children.Add(this.cbNpc);
            this.flowLayoutPanel1.Children.Add(this.ckbUnEditable);
            this.flowLayoutPanel1.Children.Add(this.cbgals);
            this.flowLayoutPanel1.Children.Add(this.cbmens);
            this.flowLayoutPanel1.Children.Add(this.cbadults);
            this.flowLayoutPanel1.Children.Add(this.cbdetail);

            // ── Bottom bar: legend + Open button ─────────────────────────────────
            var legendBar = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 4 };
            legendBar.Children.Add(this.panel1);
            legendBar.Children.Add(this.label1);
            legendBar.Children.Add(this.panel2);
            legendBar.Children.Add(this.label2);
            legendBar.Children.Add(this.panel3);
            legendBar.Children.Add(this.label3);

            var bottomBar = new Avalonia.Controls.Grid();
            bottomBar.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition(1, Avalonia.Controls.GridUnitType.Star));
            bottomBar.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition(Avalonia.Controls.GridLength.Auto));
            Avalonia.Controls.Grid.SetColumn(legendBar,     0);
            Avalonia.Controls.Grid.SetColumn(this.button1, 1);
            bottomBar.Children.Add(legendBar);
            bottomBar.Children.Add(this.button1);

            // ── Root layout ───────────────────────────────────────────────────────
            var root = new Avalonia.Controls.Grid();
            root.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(1, Avalonia.Controls.GridUnitType.Star));
            root.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            root.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            root.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            Avalonia.Controls.Grid.SetRow(this.lv,             0);
            Avalonia.Controls.Grid.SetRow(this.lbUbi,          1);
            Avalonia.Controls.Grid.SetRow(this.flowLayoutPanel1, 2);
            Avalonia.Controls.Grid.SetRow(bottomBar,           3);
            root.Children.Add(this.lv);
            root.Children.Add(this.lbUbi);
            root.Children.Add(this.flowLayoutPanel1);
            root.Children.Add(bottomBar);

            this.Content = root;
		}
		#endregion

		protected void AddImage(SimPe.PackedFiles.Wrapper.ExtSDesc sdesc)
        {
            Image img = null;

                img = Ambertation.Windows.Forms.Graph.ImagePanel.CreateThumbnail(img, this.ilist.ImageSize, 12, Color.FromArgb(90, Color.Black), SimPe.PackedFiles.Wrapper.SimPoolControl.GetImagePanelColor(sdesc), Color.White, Color.FromArgb(80, Color.White), true, 4, 0);
                this.ilist.Images.Add(img);
                this.iListSmall.Images.Add(img); // ImageLoader.Preview returns SKBitmap; use original

                if (sdesc.Unlinked != 0x00 || !sdesc.AvailableCharacterData || sdesc.IsNPC)
                {
                SKBitmap skBmp;
                if (sdesc.HasImage && sdesc.Image is SKBitmap existingBmp)
                    skBmp = existingBmp;
                else
                    skBmp = new SKBitmap(1, 1);
                using (var canvas = new SKCanvas(skBmp))
                {
                    //canvas.Clear() not needed — we draw on the existing image
                    int pos = 2;
                    if (sdesc.Unlinked != 0x00)
                    {
                        var c = Data.MetaData.UnlinkedSim;
                        using (var paint = new SKPaint { Color = new SKColor(c.R, c.G, c.B, c.A), Style = SKPaintStyle.Fill })
                            canvas.DrawRect(new SKRect(pos, 2, pos + 20, 22), paint);
                        pos += 22;
                    }
                    if (!sdesc.AvailableCharacterData)
                    {
                        var c = Data.MetaData.InactiveSim;
                        using (var paint = new SKPaint { Color = new SKColor(c.R, c.G, c.B, c.A), Style = SKPaintStyle.Fill })
                            canvas.DrawRect(new SKRect(pos, 2, pos + 20, 22), paint);
                        pos += 22;
                    }
                    if (sdesc.IsNPC)
                    {
                        var c = Data.MetaData.NPCSim;
                        using (var paint = new SKPaint { Color = new SKColor(c.R, c.G, c.B, c.A), Style = SKPaintStyle.Fill })
                            canvas.DrawRect(new SKRect(pos, 2, pos + 20, 22), paint);
                        pos += 22;
                    }
                }
                    this.ilist.Images.Add(skBmp);
                    this.iListSmall.Images.Add(skBmp);
                }
                else if (sdesc.HasImage) // if (sdesc.Image != null) -Chris H
                {
                    this.ilist.Images.Add(sdesc.Image as SkiaSharp.SKBitmap);
                    this.iListSmall.Images.Add(sdesc.Image as SkiaSharp.SKBitmap);
                }
                else
                {
                    this.ilist.Images.Add(new SKBitmap(1, 1));
                    this.iListSmall.Images.Add(new SKBitmap(1, 1));
                }
            }


        protected void AddSim(SimPe.PackedFiles.Wrapper.ExtSDesc sdesc)
        {
            AddImage(sdesc);
            ListViewItem lvi = new ListViewItem();
            lvi.Text = sdesc.SimName +" "+sdesc.SimFamilyName;
            lvi.ImageIndex = ilist.Images.Count -1;
            lvi.Tag = sdesc;

            if (sdesc.FamilyInstance == 0) lvi.SubItems.Add("None");
            else lvi.SubItems.Add(sdesc.HouseholdName);
            if (sdesc.University.OnCampus == 0x1)
                lvi.SubItems.Add(Localization.Manager.GetString("YoungAdult"));
            else
                lvi.SubItems.Add(new Data.LocalizedLifeSections(sdesc.CharacterDescription.LifeSection).ToString());

            string kind = "";
            if (System.IO.Path.GetFileNameWithoutExtension(sdesc.CharacterFileName) == "objects") kind = "NPC Unique";
            else if (realIsNPC(sdesc)) kind = "Service Sim";
            else if (realIsTownie(sdesc)) kind = "Townie";
            else if (realIsPlayable(sdesc)) kind = "Playable";
            else if (realIsUneditable(sdesc)) kind = "No Family";
            lvi.SubItems.Add(kind);

            if (sdesc.CharacterDescription.Gender == Data.MetaData.Gender.Female) lvi.SubItems.Add("Female"); else lvi.SubItems.Add("Male");

            if (sdesc.University.OnCampus==0x1) lvi.SubItems.Add(Localization.Manager.GetString("yes")); else lvi.SubItems.Add(Localization.Manager.GetString("no"));
            lvi.SubItems.Add("0x"+Helper.HexString(sdesc.FileDescriptor.Instance));

            string avl = "";
            if (!sdesc.AvailableCharacterData)
            {
                if (System.IO.File.Exists(sdesc.CharacterFileName)) avl += "no Character Data"; else avl += "no Character File";
            }
            if (sdesc.Unlinked!=0x00)
            {
                if (avl!="") avl+=", ";
                avl += "Unlinked";
            }
            if (sdesc.CharacterDescription.GhostFlag.IsGhost && avl == "") avl = "Deceased";
            if (avl=="") avl="OK";
            lvi.SubItems.Add(avl);
            lvi.SubItems.Add("0x"+Helper.HexString(sdesc.SimId));

            if (System.IO.File.Exists(sdesc.CharacterFileName))
            {
                System.IO.Stream s = System.IO.File.OpenRead(sdesc.CharacterFileName);
                double sz = s.Length / 1024.0;
                s.Close();
                s.Dispose();
                s = null;
                lvi.SubItems.Add(System.IO.Path.GetFileNameWithoutExtension(sdesc.CharacterFileName));
                lvi.SubItems.Add(sz.ToString("N2")+"kb");
            }
            else
            {
                lvi.SubItems.Add("---");
                lvi.SubItems.Add("---");
            }
            if (sdesc.Nightlife.Species == SimPe.PackedFiles.Wrapper.SdscNightlife.SpeciesType.Human)
                lvi.SubItems.Add("Human");
            else if (sdesc.Nightlife.Species == SimPe.PackedFiles.Wrapper.SdscNightlife.SpeciesType.LargeDog)
                lvi.SubItems.Add("Large Dog");
            else if (sdesc.Nightlife.Species == SimPe.PackedFiles.Wrapper.SdscNightlife.SpeciesType.SmallDog)
                lvi.SubItems.Add("Small Dog");
            else if (sdesc.Nightlife.Species == SimPe.PackedFiles.Wrapper.SdscNightlife.SpeciesType.Cat)
                lvi.SubItems.Add("Cat");
            else
                lvi.SubItems.Add("Unknown");

            lv.Items.Add(lvi);
        }


        protected void FillList()
		{
            this.Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Wait);
            WaitingScreen.Wait();
            ilist.Images.Clear();
			this.iListSmall.Images.Clear();
			lv.BeginUpdate();
			lv.Items.Clear();
			Interfaces.Files.IPackedFileDescriptor[] pfds = package.FindFiles(Data.MetaData.SIM_DESCRIPTION_FILE);
            try
            {
                foreach (Interfaces.Files.IPackedFileDescriptor spfd in pfds)
                {
                    PackedFiles.Wrapper.ExtSDesc sdesc = new SimPe.PackedFiles.Wrapper.ExtSDesc();
                    sdesc.ProcessData(spfd, package);

                    bool doAdd = false;
                    doAdd |= (this.cbNpc.IsChecked == true && realIsNPC(sdesc));
                    doAdd |= (this.cbTownie.IsChecked == true && realIsTownie(sdesc));
                    doAdd |= (this.ckbPlayable.IsChecked == true && realIsPlayable(sdesc));
                    doAdd |= (this.ckbUnEditable.IsChecked == true && realIsUneditable(sdesc));
                    doAdd &= (this.cbmens.IsChecked != true || !realIsWoman(sdesc));
                    doAdd &= (this.cbgals.IsChecked != true || realIsWoman(sdesc));
                    doAdd &= (this.cbadults.IsChecked != true || realIsAdult(sdesc));

                    if (doAdd) AddSim(sdesc);
                }

                sorter.Sorting = lv.Sorting;
                lv.Sort();
            }
            finally
            {
                lv.EndUpdate();
                WaitingScreen.Stop(this);
                this.Cursor = null;
            }
        }

        private bool realIsNPC(PackedFiles.Wrapper.ExtSDesc sdesc)
        {
            return sdesc.FamilyInstance == 0x7fff;
        }

        private bool realIsTownie(PackedFiles.Wrapper.ExtSDesc sdesc)
        {
            return sdesc.FamilyInstance < 0x7fff && sdesc.FamilyInstance >= 0x7f00;
        }

        private bool realIsPlayable(PackedFiles.Wrapper.ExtSDesc sdesc)
        {
            return sdesc.FamilyInstance < 0x7f00 && sdesc.FamilyInstance > 0;
        }

        private bool realIsUneditable(PackedFiles.Wrapper.ExtSDesc sdesc)
        {
            return sdesc.FamilyInstance == 0 || sdesc.FamilyInstance > 0x7fff;
        }

        private bool realIsWoman(PackedFiles.Wrapper.ExtSDesc sdesc)
        {
            return sdesc.CharacterDescription.Gender == Data.MetaData.Gender.Female;
        }

        private bool realIsAdult(PackedFiles.Wrapper.ExtSDesc sdesc)
        {
            return sdesc.CharacterDescription.LifeSection == Data.MetaData.LifeSections.Adult;
        }

		SimPe.Interfaces.Files.IPackedFileDescriptor pfd;
		SimPe.Interfaces.Files.IPackageFile package;
		public Interfaces.Plugin.IToolResult Execute(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package, Interfaces.IProviderRegistry prov) 
		{
			this.package = package;
			
			lv.ListViewItemSorter = sorter;
			this.Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Wait);
			
			SimPe.Plugin.Idno idno = SimPe.Plugin.Idno.FromPackage(package);
			if (idno!=null) this.lbUbi.IsVisible = (idno.Type != NeighborhoodType.Normal);
			this.pfd = null;
			
			
			lv.Sorting = SortOrder.Ascending;
			sorter.CurrentColumn = 3;

			FillList();
			
			this.Cursor = null;
			
			RemoteControl.ShowSubForm(this);

			this.package = null;

			if (this.pfd!=null) pfd = this.pfd;
			return new Plugin.ToolResult((this.pfd!=null), false);
		}

		private void Open(object sender, System.EventArgs e)
		{
			if (lv.SelectedItems.Count<1) return;
			
			pfd = (SimPe.Interfaces.Files.IPackedFileDescriptor)((PackedFiles.Wrapper.SDesc)lv.SelectedItems[0].Tag).FileDescriptor;
			Close();
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
            if (cbdetail.IsChecked == true)
                lv.View = View.Details;
            else
                lv.View = View.LargeIcon;
		}

		internal ColumnSorter sorter;
		private void SortList(object sender, EventArgs e)
		{
			int col = (e as ColumnClickEventArgs)?.Column ?? 0;
			if (sorter.CurrentColumn == col)
			{
				if (lv.Sorting == SortOrder.Ascending) lv.Sorting = SortOrder.Descending;
				else lv.Sorting = SortOrder.Ascending;
			}
			else
			{
				sorter.CurrentColumn = col;
				lv.ListViewItemSorter = sorter;
			}
			sorter.Sorting = lv.Sorting;
			lv.Sort();
		}

        private void ckbFilter_CheckedChanged(object sender, System.EventArgs e)
        {
            this.cbgals.IsEnabled = this.cbmens.IsChecked != true;
            this.cbmens.IsEnabled = this.cbgals.IsChecked != true;
            if (package != null)
                this.FillList();
        }

        private bool UseBigIcons
        {
            get
            {
                XmlRegistryKey rkf = Helper.XmlRegistry.PluginRegistryKey.CreateSubKey("SimBrowser");
                object o = rkf.GetValue("UseBiggerIcons", false);
                return Convert.ToBoolean(o);
            }
        }
	}
}
