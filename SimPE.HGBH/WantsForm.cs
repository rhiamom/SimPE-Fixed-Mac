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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using SkiaSharp;
using SimPe.Windows.Forms;
using SimPe.Scenegraph.Compat;
using Avalonia.Controls;
using TreeView = SimPe.Scenegraph.Compat.TreeView;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for WantsForm.
	/// </summary>
	public class WantsForm
	{
        private WrapperBaseControl panel2;
        internal StackPanel wantsPanel;
		internal TabControl tabControl1;
		internal TabItem tblife;
		private TabItem tbwant;
		private TabItem tbfear;
		private TabItem tbhist;
		internal ImageList iwant;
		internal ImageList ifear;
		internal ImageList ihist;
		internal ImageList ilife;
		internal ListView lvwant;
		internal ListView lvfear;
		internal ListView lvlife;
		internal TreeView tvhist;
		internal TextBlock lbsimname;
		private TextBlock label1;
		private TextBlock label2;
		private TextBlock label3;
		private TextBlock label4;
		private TextBlock label5;
		private TextBlock label6;
		private TextBlock label7;
		private TextBlock label8;
		private TextBlock label9;
		private TextBlock label10;
		private TextBox tbversion;
		private TextBox tbguid;
		private TextBox tbval;
		private TextBox tbprop;
		private TextBox tbsiminst;
		private TextBox tbindex;
		private TextBox tbunknown1;
		internal ComboBox cbtype;
		private TextBox tbunknown2;
		private TextBox tbpoints;
		private GroupBox gbprop;
		private PictureBox pb;
		private TreeView tv;
        internal ImageList itv;
		private CheckBox cblock;
        private ComboBox cbsel;

		public WantsForm()
		{
            InitializeComponent();
			wrapper = null;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.wantsPanel  = new StackPanel();
            this.gbprop      = new GroupBox();
            this.cbsel       = new ComboBox();
            this.cblock      = new CheckBox { Content = "Locked:" };
            this.tv          = new TreeView();
            this.itv         = new ImageList();
            this.cbtype      = new ComboBox();
            this.tbpoints    = new TextBox { Text = "0" };
            this.tbunknown2  = new TextBox { Text = "0" };
            this.tbunknown1  = new TextBox { IsReadOnly = true, Text = "0x00" };
            this.tbindex     = new TextBox { IsReadOnly = true, Text = "0x00000000" };
            this.tbsiminst   = new TextBox { IsReadOnly = true, Text = "0x0000" };
            this.tbprop      = new TextBox { Text = "0" };
            this.tbval       = new TextBox { Text = "0x00000000" };
            this.tbguid      = new TextBox { IsReadOnly = true, Text = "0x00000000" };
            this.tbversion   = new TextBox { IsReadOnly = true, Text = "0x00000000" };
            this.label10     = new TextBlock { Text = "Influence:" };
            this.label9      = new TextBlock { Text = "Flags:" };
            this.label8      = new TextBlock { Text = "Points:" };
            this.label7      = new TextBlock { Text = "Index:" };
            this.label6      = new TextBlock { Text = "Amount:" };
            this.label4      = new TextBlock { Text = "Type:" };
            this.label3      = new TextBlock { Text = "Want GUID:" };
            this.label2      = new TextBlock { Text = "Sim Inst.:" };
            this.label1      = new TextBlock { Text = "Version:" };
            this.pb          = new PictureBox();
            this.label5      = new TextBlock { Text = "Value:" };
            this.tabControl1 = new TabControl();
            this.tbwant      = new TabItem { Header = "Wants" };
            this.lvwant      = new ListView();
            this.iwant       = new ImageList();
            this.tbfear      = new TabItem { Header = "Fears" };
            this.lvfear      = new ListView();
            this.ifear       = new ImageList();
            this.tbhist      = new TabItem { Header = "History" };
            this.tvhist      = new TreeView();
            this.ihist       = new ImageList();
            this.tblife      = new TabItem { Header = "Lifetime Wants" };
            this.lvlife      = new ListView();
            this.ilife       = new ImageList();
            this.panel2      = new WrapperBaseControl();
            this.lbsimname   = new TextBlock { Text = "---" };

            // cbsel
            this.cbsel.SelectionChanged += (s, e) => this.cbsel_SelectedIndexChanged(s, EventArgs.Empty);

            // cblock
            this.cblock.IsCheckedChanged += (s, e) => this.ChangedText(s, EventArgs.Empty);

            // tv
            this.tv.HideSelection = false;
            this.tv.ImageList = this.itv;
            this.tv.AfterSelect += new TreeViewEventHandler(this.SelectWant);

            // itv
            this.itv.ColorDepth = ColorDepth.Depth32Bit;
            this.itv.ImageSize = new System.Drawing.Size(16, 16);

            // cbtype
            this.cbtype.IsEnabled = false;
            this.cbtype.SelectionChanged += (s, e) => this.ChangeType(s, EventArgs.Empty);

            // textbox events
            this.tbpoints.TextChanged   += (s, e) => this.ChangedText(s, EventArgs.Empty);
            this.tbunknown2.TextChanged += (s, e) => this.ChangedText(s, EventArgs.Empty);
            this.tbprop.TextChanged     += (s, e) => this.ChangedText(s, EventArgs.Empty);
            this.tbval.TextChanged      += (s, e) => this.ChangedText(s, EventArgs.Empty);

            // lvwant
            this.lvwant.HideSelection = false;
            this.lvwant.LargeImageList = this.iwant;
            this.lvwant.MultiSelect = false;
            this.lvwant.UseCompatibleStateImageBehavior = false;
            this.lvwant.SelectedIndexChanged += new System.EventHandler(this.SelectWant);

            // iwant
            this.iwant.ColorDepth = ColorDepth.Depth32Bit;
            this.iwant.ImageSize = new System.Drawing.Size(44, 44);

            // lvfear
            this.lvfear.HideSelection = false;
            this.lvfear.LargeImageList = this.ifear;
            this.lvfear.MultiSelect = false;
            this.lvfear.UseCompatibleStateImageBehavior = false;
            this.lvfear.SelectedIndexChanged += new System.EventHandler(this.SelectWant);

            // ifear
            this.ifear.ColorDepth = ColorDepth.Depth32Bit;
            this.ifear.ImageSize = new System.Drawing.Size(44, 44);

            // tvhist
            this.tvhist.HideSelection = false;
            this.tvhist.ImageList = this.ihist;
            this.tvhist.AfterSelect += new TreeViewEventHandler(this.SeletTv);

            // ihist
            this.ihist.ColorDepth = ColorDepth.Depth32Bit;
            this.ihist.ImageSize = new System.Drawing.Size(16, 16);

            // lvlife
            this.lvlife.HideSelection = false;
            this.lvlife.LargeImageList = this.ilife;
            this.lvlife.MultiSelect = false;
            this.lvlife.UseCompatibleStateImageBehavior = false;
            this.lvlife.SelectedIndexChanged += new System.EventHandler(this.SelectWant);

            // ilife
            this.ilife.ColorDepth = ColorDepth.Depth32Bit;
            this.ilife.ImageSize = new System.Drawing.Size(44, 44);

            // tab pages
            this.tbwant.Content = this.lvwant;
            this.tbfear.Content = this.lvfear;
            this.tbhist.Content = this.tvhist;
            this.tblife.Content = this.lvlife;
            this.tabControl1.Items.Add(this.tbwant);
            this.tabControl1.Items.Add(this.tbfear);
            this.tabControl1.Items.Add(this.tbhist);
            this.tabControl1.Items.Add(this.tblife);
            this.tabControl1.SelectionChanged += (s, e) => this.SelectTab(s, EventArgs.Empty);

            // gbprop
            this.gbprop.Text = "Properties:";
            this.gbprop.TabStop = false;
            this.gbprop.Enabled = false;

            // panel2
            this.panel2.CanCommit = true;
            this.panel2.HeaderText = "Wants and Fears Viewer for";
            this.panel2.Commited += (s, e) => this.Commit(s, EventArgs.Empty);

            // lbsimname
            this.lbsimname.PointerPressed += (s, e) => lbsimname_Click(s, System.EventArgs.Empty);
		}
		#endregion

		internal Want wrapper;

		internal void AddWantToList(ListView lv, ImageList il, WantItem wnt)
		{
			ListViewItem lvi = new ListViewItem();
			lvi.Text = wnt.ToString();
			lvi.Tag = wnt;

			if (wnt.Information.Icon!=null)
			{
				lvi.ImageIndex = il.Images.Count;
				il.Images.Add(wnt.Information.Icon as SkiaSharp.SKBitmap);
                Wait.Message = wnt.ToString();
			}

			lv.Items.Add(lvi);
		}

		void LoadHistory()
		{
			lasttve = null;
            Wait.SubStart();
			tvhist.BeginUpdate();
			foreach (WantItemContainer wic in wrapper.History) this.AddWant(tvhist, wic);
			tvhist.EndUpdate();
            Wait.SubStop();
		}

		internal void AddWant(TreeView tv, WantItemContainer wc)
		{
			TreeNode parent = new TreeNode(wc.ToString());
			parent.Tag = wc;

			if (wc.Information.Icon!=null) {
				parent.SelectedImageIndex = ihist.Images.Count;
				parent.ImageIndex = ihist.Images.Count;
				ihist.Images.Add(wc.Information.Icon as SkiaSharp.SKBitmap);

                Wait.Message = wc.ToString();
			}

			foreach (WantItem wi in wc.Items)
			{
				TreeNode node = new TreeNode(wi.ToString());
				node.ImageIndex = parent.ImageIndex;
				node.SelectedImageIndex = parent.SelectedImageIndex;
				node.Tag = wi;

				parent.Nodes.Add(node);
			}

			tv.Nodes.Add(parent);
		}

		void SelectTvNode(WantItem wi)
		{
			foreach (TreeNode parent in tv.Nodes)
			{
				foreach (TreeNode node in parent.Nodes)
				{
					WantInformation winfo = (WantInformation)node.Tag;
					if (wi!=null)
					{
						if (winfo.Guid == wi.Guid)
						{
							tv.SelectedNode = node;
							node.Parent.Expand();
							node.EnsureVisible();
							return;
						}
					}
				}
			}
		}

		void ShowWantItem(WantItem wi)
		{
			lastwi = wi;

			this.tbversion.Text = "0x"+Helper.HexString(wi.Version);
			this.tbsiminst.Text = "0x"+Helper.HexString(wi.SimInstance);
			this.tbguid.Text = "0x"+Helper.HexString(wi.Guid);
			this.tbprop.Text = wi.Property.ToString();
			this.tbindex.Text = "0x"+Helper.HexString(wi.Index);
			this.tbpoints.Text = wi.Score.ToString();
			this.tbunknown1.Text = "0x"+Helper.HexString((byte)wi.Flag.Value);
			this.tbunknown2.Text = wi.Influence.ToString();
			this.cblock.IsChecked = wi.Flag.Locked;

			this.pb.Image = wi.Information.Icon;

			this.cbtype.SelectedIndex=0;
			for (int i=1; i<cbtype.Items.Count; i++)
			{
				WantType wt = (WantType)cbtype.Items[i];
				if (wt==wi.Type)
				{
					cbtype.SelectedIndex = i;
					break;
				}
			}

			this.tbval.Text = "0x"+Helper.HexString(wi.Value);

			this.Tag = true;
			try
			{
				SelectTvNode(wi);
			}
			finally
			{
				this.Tag = null;
			}
		}

        public object Tag { get; set; }

		internal WantItem lastwi;
		internal ListViewItem lastlvi;
		private void SelectWant(object sender, System.EventArgs e)
		{
			ListView lv = (ListView)sender;
			gbprop.Enabled = false;
			lastwi = null;
			if (lv.SelectedItems.Count==0) return;
			gbprop.Enabled = true;

			WantItem wi = (WantItem)lv.SelectedItems[0].Tag;
			lastlvi = lv.SelectedItems[0];

			if (this.Tag!=null) return;
			this.Tag = true;
			ShowWantItem(wi);
			this.Tag = null;
		}

		private void cbsel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cbsel.SelectedIndex<0) return;
			Data.Alias a = (Data.Alias)cbsel.Items[cbsel.SelectedIndex];
			tbval.Text = "0x"+Helper.HexString(a.Id);
		}

		private void ChangeType(object sender, System.EventArgs e)
		{
			this.cbsel.Items.Clear();
			ArrayList list = WantLoader.WantNameLoader.GetNames((WantType)cbtype.Items[cbtype.SelectedIndex]);
			foreach (Data.Alias a in list) cbsel.Items.Add(a);

			int ct=0;
			foreach (Data.Alias a in cbsel.Items)
			{
				if (lastwi!=null) if (a.Id == lastwi.Value) cbsel.SelectedIndex=ct;
				ct++;
			}
		}

		private void lbsimname_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (Helper.StartedGui==Executable.Classic && wrapper.Changed)
				{
					if (SimPe.Message.Show(SimPe.Localization.Manager.GetString("open_wnt_from_sdsc"), SimPe.Localization.Manager.GetString("question"), MessageBoxButtons.YesNo)==SimPe.DialogResult.Yes)
						wrapper.SynchronizeUserData();
				}

				Interfaces.Files.IPackedFileDescriptor pfd = wrapper.Package.NewDescriptor(Data.MetaData.SIM_DESCRIPTION_FILE, wrapper.FileDescriptor.SubType, wrapper.FileDescriptor.Group, wrapper.FileDescriptor.Instance);
				pfd = wrapper.Package.FindFile(pfd);
				SimPe.RemoteControl.OpenPackedFile(pfd, wrapper.Package);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}

        private void Commit(object sender, System.EventArgs e)
		{
			wrapper.SynchronizeUserData();
		}

		private void ChangedText(object sender, System.EventArgs e)
		{
			if (lastwi==null) return;
			if (this.Tag != null) return;
			this.Tag = true;

			try
			{
				lastwi.Influence = Convert.ToInt32(this.tbunknown2.Text);
				lastwi.Score = Convert.ToInt32(this.tbpoints.Text);
				lastwi.Value = Convert.ToUInt32(this.tbval.Text, 16);
				lastwi.Property = Convert.ToUInt16(this.tbprop.Text);

				lastwi.Flag.Locked = cblock.IsChecked == true;
				wrapper.Changed = true;

				if (this.lastlvi!=null) lastlvi.Text = lastwi.ToString();
			}
			catch {}
			finally
			{
				this.Tag = null;
			}
		}

		private void SelectTab(object sender, System.EventArgs e)
		{
			if (tabControl1.SelectedIndex==2 && tvhist.Nodes.Count==0) LoadHistory();

			if (tabControl1.SelectedIndex==0) SelectWant(lvwant, (System.EventArgs)null);
			else if (tabControl1.SelectedIndex==1) SelectWant(lvfear, (System.EventArgs)null);
			else if (tabControl1.SelectedIndex==3) SelectWant(lvlife, (System.EventArgs)null);
			else SeletTv(null, lasttve);
		}

		TreeViewEventArgs lasttve;
		private void SeletTv(object sender, TreeViewEventArgs e)
		{
			lastwi = null;
			gbprop.Enabled = false;
			if (e==null) return;
			if (e.Node == null) return;


			lasttve = e;
			TreeNode node = e.Node;


			if (node.Tag.GetType() == typeof(WantItem))
			{
				WantItem wi = (WantItem)node.Tag;
				ShowWantItem(wi);
			}
		}

		internal void ListWants()
		{
			if (tv.Nodes.Count>0) return;

			itv.Images.Add(SKBitmap.Decode(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.subitems.png")));
			itv.Images.Add(SKBitmap.Decode(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.nothumb.png")));
            Wait.SubStart();
			System.Collections.Hashtable ht = new Hashtable();
			string max = " / "+WantLoader.Wants.Keys.Count.ToString();
			int ct = 0;
            Wait.MaxProgress = WantLoader.Wants.Keys.Count;
            Wait.Message = "Loading Wants";
			foreach (uint guid in WantLoader.Wants.Keys)
			{
				ct++;
				WantInformation wi = WantInformation.LoadWant(guid);
				ArrayList al = (ArrayList)ht[wi.XWant.Folder];
				if (al==null)
				{
					al = new ArrayList();
					ht[wi.XWant.Folder] = al;
				}

				wi.prefix = "    ";
				al.Add(wi);

                if ((ct % 3) == 1)
                {
                    Wait.Progress = ct;
                }
			}

			foreach (string k in ht.Keys)
			{
				TreeNode parent = new TreeNode(k);

				foreach (WantInformation wi in (ArrayList)ht[k])
				{
					TreeNode node = new TreeNode(wi.Name);
					node.Tag = wi;

					if (wi.Icon!=null)
					{
						node.ImageIndex = itv.Images.Count;
						itv.Images.Add(wi.Icon as SkiaSharp.SKBitmap);
					}
					else
					{
						node.ImageIndex = 1;
					}
					node.SelectedImageIndex = node.ImageIndex;
					parent.Nodes.Add(node);
				}
				tv.Nodes.Add(parent);
			}
			tv.Sorted = true;

            Wait.SubStop();
		}

		private void SelectWant(object sender, TreeViewEventArgs e)
		{
			if (this.Tag!=null) return;
			if (e==null) return;
			if (e.Node == null) return;
			if (e.Node.Tag == null) return;

			WantInformation wi = (WantInformation)e.Node.Tag;
			this.tbguid.Text = "0x"+Helper.HexString(wi.Guid);
			pb.Image = wi.Icon;

			if (lastlvi!=null)
			{
				if (lastlvi.ImageIndex>=0)
					// ImageLoader.Preview now returns SKBitmap; ImageList.Images expects System.Drawing.Image — skip preview
				ImageLoader.Preview(wi.Icon as SkiaSharp.SKBitmap, lastlvi.ListView.LargeImageList.ImageSize);
				lastlvi.Text = wi.Name;
			}

			if (lastwi!=null)
			{
				lastwi.Guid = wi.Guid;
				lastwi.Type = wi.XWant.WantType;
				lastwi.Influence = wi.XWant.Influence;
				lastwi.Score = wi.XWant.Score;
				lastlvi.Text = lastwi.ToString();

				wrapper.Changed = true;

				this.ShowWantItem(lastwi);
			}

			if (lastlvi!=null)  lastlvi.ListView.Refresh();
		}
	}

}
