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
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using SimPe.Scenegraph.Compat;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for SubsetSelectForm.
	/// </summary>
	public class SubsetSelectForm : Avalonia.Controls.Window
	{
		private Avalonia.Controls.Panel panel1;
		public PanelCompat pnselect;
		public ButtonCompat button1;
		public CheckBoxCompat2 cbauto;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		internal SubsetSelectForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected void Dispose(bool disposing)
		{
			// base.Dispose(disposing); // Avalonia Window does not have Dispose(bool)
		}

			#region Avalonia layout
		private Avalonia.Controls.StackPanel _subsetStack;

		private void InitializeComponent()
		{
			this.Title = "Select Subsets";
			this.Width = 500;
			this.Height = 450;
			this.WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterOwner;

			panel1 = new Avalonia.Controls.Panel();
			pnselect = new PanelCompat();

			cbauto = new CheckBoxCompat2();
			cbauto.Content = "Auto-select matching textures";
			cbauto.IsChecked = true;
			cbauto.Margin = new Avalonia.Thickness(4);

			button1 = new ButtonCompat();
			button1.Content = "Continue";
			button1.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right;
			button1.Margin = new Avalonia.Thickness(4);
			button1.Padding = new Avalonia.Thickness(16, 4);
			button1.Click += button1_Click;

			_subsetStack = new Avalonia.Controls.StackPanel();

			var scroll = new Avalonia.Controls.ScrollViewer
			{
				Content = _subsetStack,
				VerticalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Auto,
			};

			var bottomBar = new Avalonia.Controls.StackPanel
			{
				Orientation = Avalonia.Layout.Orientation.Horizontal,
				HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
				Margin = new Avalonia.Thickness(4),
			};
			bottomBar.Children.Add(cbauto);
			bottomBar.Children.Add(button1);

			var root = new Avalonia.Controls.DockPanel { Margin = new Avalonia.Thickness(4) };
			Avalonia.Controls.DockPanel.SetDock(bottomBar, Avalonia.Controls.Dock.Bottom);
			root.Children.Add(bottomBar);
			root.Children.Add(scroll);

			this.Content = root;
		}
		#endregion


		ArrayList listviews;
		/// <summary>
		/// Returns a list of all availabel ListViews
		/// </summary>
		public ArrayList ListViews
		{
			get {return listviews;}
		}

		public static Size ImageSize = new Size(128, 128);

		/// <summary>
		/// Add a new Selection for a Subset
		/// </summary>
		/// <param name="ssf">The for you want to add the Selection to</param>
		/// <param name="subset">The name of the Subset</param>
		/// <param name="top">the top coordinate for the Selection Panel</param>
		/// <returns>the ListView you can use to add Items to</returns>
		protected ListView AddSelection(SubsetSelectForm ssf, string subset, ref int top)
		{
			ListView lv = new ListView();
			lv.Tag = subset;
			lv.HideSelection = false;
			lv.MultiSelect = false;
			lv.SelectedIndexChanged += new EventHandler(SelectedIndexChanged);

			ImageList il = new ImageList();
			il.ImageSize = ImageSize;
			il.ColorDepth = ColorDepth.Depth32Bit;
			lv.LargeImageList = il;

			top += ImageSize.Height + 64 + 8;

			listviews.Add(lv);
			return lv;
		}

		/// <summary>
		/// Return the Thumbnail for the mmat with the passed Index
		/// </summary>
		/// <param name="index"></param>
		/// <param name="mmats"></param>
		/// <param name="sz">Size of the Thumbnail</param>
		/// <returns>a valid Image</returns>
		protected System.Drawing.Image GetItemThumb(int index, ArrayList mmats, Size sz)
		{
			if ((index<0) || (index>=mmats.Count)) return new Bitmap(sz.Width, sz.Height);

			SimPe.Plugin.MmatWrapper mmat = (SimPe.Plugin.MmatWrapper)mmats[index];
			GenericRcol txtr = mmat.TXTR;
			if (txtr!=null)
			{
				ImageData id = (ImageData)txtr.Blocks[0];
				MipMap mm = id.LargestTexture;

				if (mm!=null)
				{
					SkiaSharp.SKBitmap skBmp = ImageLoader.Preview(mm.Texture, sz);
					if (skBmp != null)
					{
						using var skImage = SkiaSharp.SKImage.FromBitmap(skBmp);
						using var encoded = skImage.Encode(SkiaSharp.SKEncodedImageFormat.Png, 100);
						using var ms = new System.IO.MemoryStream();
						encoded.SaveTo(ms);
						ms.Position = 0;
						return new Bitmap(ms);
					}
				}
			}

			return new Bitmap(sz.Width, sz.Height);
		}

		/// <summary>
		/// Creates a Thumbnail for the current Texture
		/// </summary>
		/// <param name="il">The ImageList that will hold the Thumbnail</param>
		/// <param name="lvi">The ListView Item that will get the Thumbnail Image</param>
		/// <param name="mmats">The Materials</param>
		protected void MakePreview(ImageList il, ListViewItem lvi, ArrayList mmats)
		{
			if (mmats.Count == 1)
			{
				System.Drawing.Image img = GetItemThumb(0, mmats, il.ImageSize);
				lvi.ImageIndex = il.Images.Count;
				il.Images.Add(img);
			}
			else if (mmats.Count>1)
			{
				System.Drawing.Image img1 = GetItemThumb(0, mmats, il.ImageSize);
				System.Drawing.Image img2 = GetItemThumb(1, mmats, il.ImageSize);

				Bitmap bm = new Bitmap(il.ImageSize.Width, il.ImageSize.Height);
				Graphics gr = Graphics.FromImage(bm);
				gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

				Rectangle source = new Rectangle(0, 0, il.ImageSize.Width / 2, il.ImageSize.Height);
				gr.DrawImage(img1, source, source, GraphicsUnit.Pixel);

				source = new Rectangle(il.ImageSize.Width / 2, 0, il.ImageSize.Width / 2, il.ImageSize.Height);
				gr.DrawImage(img2, source, source, GraphicsUnit.Pixel);

				gr.DrawLine(new Pen(Color.Orange, 2), il.ImageSize.Width / 2, 0, il.ImageSize.Width / 2, il.ImageSize.Height);

				gr.FillEllipse(new Pen(Color.Orange, 1).Brush, (ImageSize.Width-24)/2, 4, 24, 24);
				System.Drawing.Font ft = new System.Drawing.Font("Verdana", 16.0f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);

				gr.DrawString(mmats.Count.ToString(), ft, new Pen(Color.White).Brush, new RectangleF((ImageSize.Width-24)/2+2, 6, 20, 20));

				lvi.ImageIndex = il.Images.Count;
				il.Images.Add(bm);
			}
		}

		Hashtable txmtnames;
        /// <summary>
        /// Add a New Item to the ListView
        /// </summary>
        /// <param name="lv">the list view you want to add the items to</param>
        /// <param name="mmats">an array of MmatWraper Objects having all possible states</param>
        protected void AddItem(ListView lv, ArrayList mmats)
        {
            if (mmats.Count == 0) return;

            ListViewItem lvi = new ListViewItem();
            GenericRcol txtr = ((SimPe.Plugin.MmatWrapper)mmats[0]).TXTR;
            GenericRcol txmt = ((SimPe.Plugin.MmatWrapper)mmats[0]).TXMT;
			if (txmt!=null)
			{
				string txmtname = Hashes.StripHashFromName(txmt.FileName.Trim().ToLower());
				if (!txmtnames.ContainsKey(txmtname))
				{
					if (txtr!=null)
					{
						lvi.Text = txtr.FileName;
						lvi.Tag = mmats;

						MakePreview(lv.LargeImageList, lvi, mmats);

						lv.Items.Add(lvi);
					}
					else
					{
						lvi.Text = txmt.FileName;
						lvi.Tag = mmats;
						lv.Items.Add(lvi);
					}

					txmtnames.Add(txmtname, lvi);
				} //txmtnames
				else
				{
					ListViewItem l = (ListViewItem)txmtnames[txmtname];
					ArrayList ls = (ArrayList)l.Tag;
					ls.AddRange(mmats);
				}
			}
		}

        /// <summary>
        /// Setup the Form
        /// </summary>
        /// <param name="map">The subset map</param>
        /// <param name="subsets">the subsets you want to present</param>
        /// <returns>Returns a New Instance of the selection Form</returns>
        public static SubsetSelectForm Prepare(Hashtable map, ArrayList subsets)
        {
            SubsetSelectForm ssf = new SubsetSelectForm();
            ssf.listviews = new ArrayList();
            ssf.txmtnames = new Hashtable();
            WaitingScreen.Wait();
            try
            {
                WaitingScreen.UpdateMessage("Show Subset Selection");
                ssf.button1.IsEnabled = false;

                int top = 0;

                foreach (string subset in map.Keys)
                {
                    if (!subsets.Contains(subset)) continue;

                    ListView lv = ssf.AddSelection(ssf, subset, ref top);
                    Hashtable families = (Hashtable)map[subset];
                    foreach (string family in families.Keys)
                    {
                        ArrayList mmats = (ArrayList)families[family];
                        mmats.Sort(new MmatListCompare());
                        ssf.AddItem(lv, mmats);
                    }

                    if (lv.Items.Count > 0) lv.Items[0].Selected = true;
                }
            }
            finally
            {
                WaitingScreen.Stop();
            }

            return ssf;
        }

        /// <summary>
        /// Builds a new Hashtable based on the Users Selection
        /// </summary>
        /// <param name="ssf">The Form that was used</param>
        /// <returns>The new Hashtable</returns>
        public static Hashtable Finish(SubsetSelectForm ssf)
		{
			//now rebuild the Hashtable with the stored Infos
			Hashtable ret = new Hashtable();
			foreach (ListView lv in ssf.listviews)
			{
				if (!lv.Enabled) continue;

				if (lv.SelectedItems.Count>0)
				{
					Hashtable sub = new Hashtable();
					sub["user-selection"] = lv.SelectedItems[0].Tag;
					ret[(string)lv.Tag] =  sub;
				}
			}

			return ret;
		}

		/// <summary>
		/// Show the Selection Form
		/// </summary>
		/// <param name="map">The subset map</param>
		/// <param name="package">the opened source package</param>
		/// <param name="subsets">List of all Subsets you want to present</param>
		/// <returns>the map with all the selected Items</returns>
		/// <summary>
		/// Populates the Avalonia visual tree from the compat ListView data.
		/// </summary>
		private void BuildVisualTree()
		{
			_subsetStack.Children.Clear();
			foreach (ListView lv in listviews)
			{
				string subset = (string)lv.Tag;
				var cb = new Avalonia.Controls.CheckBox
				{
					Content = subset,
					IsChecked = true,
					FontWeight = Avalonia.Media.FontWeight.Bold,
					Margin = new Avalonia.Thickness(0, 4, 0, 2),
					Tag = lv,
				};
				cb.IsCheckedChanged += (s, e) => CheckedChanged(s, EventArgs.Empty);

				var avList = new Avalonia.Controls.ListBox
				{
					MinHeight = 100,
					MaxHeight = 250,
					Margin = new Avalonia.Thickness(0, 2, 0, 8),
				};
				// Use horizontal wrap layout for thumbnail-style display
				avList.ItemsPanel = new FuncTemplate<Avalonia.Controls.Panel>(() =>
					new Avalonia.Controls.WrapPanel { Orientation = Avalonia.Layout.Orientation.Horizontal });
				foreach (object o in (System.Collections.IEnumerable)lv.Items)
				{
					var item = (ListViewItem)o;
					var panel = new Avalonia.Controls.StackPanel
					{
						Width = ImageSize.Width + 16,
						Margin = new Avalonia.Thickness(4),
					};
					// Show thumbnail if available from ImageList
					if (item.ImageIndex >= 0 && lv.LargeImageList != null
						&& item.ImageIndex < lv.LargeImageList.Images.Count)
					{
						var avBmp = SimPe.Helper.ToAvaloniaBitmap(lv.LargeImageList.Images[item.ImageIndex]);
						if (avBmp != null)
						{
							var img = new Avalonia.Controls.Image
							{
								Source = avBmp,
								Width = ImageSize.Width,
								Height = ImageSize.Height,
								Stretch = Avalonia.Media.Stretch.Uniform,
							};
							panel.Children.Add(img);
						}
					}
					var label = new Avalonia.Controls.TextBlock
					{
						Text = item.Text ?? "",
						TextTrimming = Avalonia.Media.TextTrimming.CharacterEllipsis,
						FontSize = 10,
						HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
					};
					panel.Children.Add(label);
					var lbi = new Avalonia.Controls.ListBoxItem { Content = panel, Tag = item };
					avList.Items.Add(lbi);
				}
				avList.SelectionChanged += (s, e) =>
				{
					// Sync selection back to compat ListView
					foreach (object o in (System.Collections.IEnumerable)lv.Items)
						((ListViewItem)o).Selected = false;
					if (avList.SelectedItem is Avalonia.Controls.ListBoxItem sel && sel.Tag is ListViewItem si)
						si.Selected = true;
					SelectedIndexChanged(lv, EventArgs.Empty);
				};
				// Pre-select first item
				if (avList.Items.Count > 0)
					avList.SelectedIndex = 0;

				_subsetStack.Children.Add(cb);
				_subsetStack.Children.Add(avList);
			}
		}

		public static Hashtable Execute(Hashtable map, ArrayList subsets)
		{
			return ExecuteAsync(map, subsets).GetAwaiter().GetResult();
		}

		public static async System.Threading.Tasks.Task<Hashtable> ExecuteAsync(Hashtable map, ArrayList subsets)
		{
			SubsetSelectForm ssf = Prepare(map, subsets);
			ssf.BuildVisualTree();
			var owner = (Avalonia.Application.Current?.ApplicationLifetime
				as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime)?.MainWindow;

			await ssf.ShowDialog(owner);

			return Finish(ssf);
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		/// <summary>
		/// true, if a SubSet is selected in each ListView
		/// </summary>
		private bool CanContinue
		{
			get
			{
				if (listviews.Count==0) return true;

				foreach (ListView lv in listviews)
				{
					if ((lv.SelectedItems.Count==0) && lv.Enabled && lv.Items.Count!=0) return false;
				}

				return true;
			}
		}

		private void CheckedChanged(object sender, EventArgs e)
		{
			Avalonia.Controls.CheckBox cb = (Avalonia.Controls.CheckBox)sender;
			ListView lv = (ListView)cb.Tag;

			lv.Enabled = cb.IsChecked == true;
			button1.IsEnabled = CanContinue;
		}

		bool internalupdate = false;
		private void SelectedIndexChanged(object sender, EventArgs e)
		{
			if (internalupdate) return;

			internalupdate = true;
			try
			{
				ListView lv = (ListView)sender;

				//autoselect matching Textures
				if ((cbauto.IsChecked == true) && (lv.SelectedItems.Count>0))
				{
					string name = lv.SelectedItems[0].Text;
					foreach (ListView lv2 in listviews)
					{
						if (lv2==lv) continue;

						foreach (object o in (System.Collections.IEnumerable)lv2.Items)
						{
							ListViewItem lvi = (ListViewItem)o;
							if (lvi.Text == name) lvi.Selected = true;
						}
					}
				}

				button1.IsEnabled = CanContinue;
			}
			finally
			{
				internalupdate = false;
			}
		}

		private void DoClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (!CanContinue)
			{
				SimPe.Message.Show("Please select a Texture in each Subset!", "Warning");
				e.Cancel = true;
			}
		}

	}
}
