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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using Avalonia.Controls;
using SimPe.Cache;
using SimPe.Scenegraph.Compat;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for MemoryProperties.
	/// </summary>
	public class MemoryProperties : Avalonia.Controls.UserControl
	{
		public MemoryProperties()
		{
			try
			{
				// Required designer variable.
				InitializeComponent();

				this.cbtype.Enum = typeof(SimMemoryType);
				this.cbtype.ResourceManager = SimPe.Localization.Manager;

				SetContent();
				this.IsEnabled = false;
				cbCtrl.IsEnabled = Helper.XmlRegistry.HiddenMode;
			}
			catch {}
		}

		#region Avalonia Designer generated code
		private void InitializeComponent()
		{
			// Instantiate all controls
			pg = new PropertyGridStub();
			tabControl2 = new TabControl();
			tabPage3 = new TabItem();
			tabPage4 = new TabItem();
			tbRawLength = new TextBox();
			label1 = new TextBlock { Text = "Raw Length:" };
			llSetRawLength = new Button { Content = "Set" };
			lbtype = new TextBlock { Text = "" };
			label2 = new TextBlock { Text = "" };
			label3 = new TextBlock { Text = "Owner:" };
			label4 = new TextBlock { Text = "Subject:" };
			label5 = new TextBlock { Text = "Object Guid:" };
			label6 = new TextBlock { Text = "Sub:" };
			label7 = new TextBlock { Text = "Inventory:" };
			label8 = new TextBlock { Text = "Value:" };
			label9 = new TextBlock { Text = "Flags:" };
			label10 = new TextBlock { Text = "Type:" };
			label11 = new TextBlock { Text = "Unknown:" };
			cbtype = new Ambertation.Windows.Forms.EnumComboBox();
			cbMems = new SimPe.PackedFiles.Wrapper.ObjectComboBox();
			cbToks = new SimPe.PackedFiles.Wrapper.ObjectComboBox();
			cbObjs = new SimPe.PackedFiles.Wrapper.ObjectComboBox();
			pb = new PictureBoxCompat();
			cbOwner = new SimPe.PackedFiles.Wrapper.SimComboBox();
			llme = new Button { Content = "Me" };
			cbSubject = new SimPe.PackedFiles.Wrapper.SimComboBox();
			cbSubjectObj = new SimPe.PackedFiles.Wrapper.ObjectComboBox();
			llme2 = new Button { Content = "Me" };
			rbObjs = new RadioButton { Content = "Objects" };
			rbToks = new RadioButton { Content = "Tokens" };
			rbMems = new RadioButton { Content = "Memories" };
			cbVis = new CheckBox { Content = "Visible" };
			cbCtrl = new CheckBox { Content = "Controller" };
			tbFlag = new TextBox { IsReadOnly = true };
			tbInv = new TextBox();
			tbValue = new TextBox();
			tbUnk = new TextBox { IsReadOnly = true };

			// Configure combo boxes
			cbSubjectObj.SelectedGuid = 0xffffffff;
			cbSubjectObj.SelectedItem = null;
			cbSubjectObj.ShowAspiration = true;
			cbSubjectObj.ShowBadge = true;
			cbSubjectObj.ShowInventory = true;
			cbSubjectObj.ShowJobData = true;
			cbSubjectObj.ShowMemories = true;
			cbSubjectObj.ShowSkill = true;
			cbSubjectObj.ShowTokens = false;
			cbSubjectObj.SelectedObjectChanged += new System.EventHandler(this.cbSubjectObj_SelectedObjectChanged);

			cbSubject.SelectedSim = null;
			cbSubject.SelectedSimId = 0xffffffff;
			cbSubject.SelectedSimInstance = 0xffff;
			cbSubject.SelectedSimChanged += new System.EventHandler(this.cbSubject_SelectedSimChanged);

			cbOwner.SelectedSim = null;
			cbOwner.SelectedSimId = 0xffffffff;
			cbOwner.SelectedSimInstance = 0xffff;
			cbOwner.SelectedSimChanged += new System.EventHandler(this.cbOwner_SelectedSimChanged);

			cbObjs.SelectedGuid = 0xffffffff;
			cbObjs.SelectedItem = null;
			cbObjs.ShowAspiration = false;
			cbObjs.ShowBadge = false;
			cbObjs.ShowInventory = true;
			cbObjs.ShowJobData = false;
			cbObjs.ShowMemories = false;
			cbObjs.ShowSkill = false;
			cbObjs.ShowTokens = false;
			cbObjs.SelectedObjectChanged += new System.EventHandler(this.ChangeGuid);

			cbToks.SelectedGuid = 0xffffffff;
			cbToks.SelectedItem = null;
			cbToks.ShowAspiration = false;
			cbToks.ShowBadge = false;
			cbToks.ShowInventory = false;
			cbToks.ShowJobData = false;
			cbToks.ShowMemories = false;
			cbToks.ShowSkill = false;
			cbToks.ShowTokens = true;
			cbToks.SelectedObjectChanged += new System.EventHandler(this.ChangeGuid);

			cbMems.SelectedGuid = 0xffffffff;
			cbMems.SelectedItem = null;
			cbMems.ShowAspiration = false;
			cbMems.ShowBadge = false;
			cbMems.ShowInventory = false;
			cbMems.ShowJobData = false;
			cbMems.ShowMemories = true;
			cbMems.ShowSkill = false;
			cbMems.ShowTokens = false;
			cbMems.SelectedObjectChanged += new System.EventHandler(this.ChangeGuid);

			cbtype.Enum = null;
			cbtype.ResourceManager = null;
			cbtype.SelectionChanged += (s, e) => cbtype_SelectedIndexChanged(s, System.EventArgs.Empty);

			// Wire events
			llSetRawLength.Click += (s, e) => llSetRawLength_Click(s, EventArgs.Empty);
			llme.Click += (s, e) => llme_Click(s, EventArgs.Empty);
			llme2.Click += (s, e) => llme2_Click(s, EventArgs.Empty);

			rbObjs.IsCheckedChanged += (s, e) => rbObjs_CheckedChanged(s, EventArgs.Empty);
			rbToks.IsCheckedChanged += (s, e) => rbToks_CheckedChanged(s, EventArgs.Empty);
			rbMems.IsCheckedChanged += (s, e) => rbMems_CheckedChanged(s, EventArgs.Empty);

			cbVis.IsCheckedChanged += (s, e) => cbVis_CheckedChanged(s, EventArgs.Empty);
			cbCtrl.IsCheckedChanged += (s, e) => cbAct_CheckedChanged(s, EventArgs.Empty);

			tbValue.TextChanged += (s, e) => tbValue_TextChanged(s, EventArgs.Empty);
			tbInv.TextChanged += (s, e) => tbInv_TextChanged(s, EventArgs.Empty);
			tbFlag.TextChanged += (s, e) => tbFlag_TextChanged(s, EventArgs.Empty);

			// ── Panel layout ─────────────────────────────────────────────────

			// pnObjectGuid
			pnObjectGuid = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
			pnObjectGuid.Children.Add(label5);
			pnObjectGuid.Children.Add(cbSubjectObj);

			// pnSubject
			pnSubject = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
			pnSubject.Children.Add(label4);
			pnSubject.Children.Add(cbSubject);
			pnSubject.Children.Add(llme2);

			// pnSub1
			pnSub1 = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
			pnSub1.Children.Add(label6);

			// pnSub2
			pnSub2 = new StackPanel();

			// panel2
			panel2 = new StackPanel();
			panel2.Children.Add(pnObjectGuid);
			panel2.Children.Add(pnSubject);
			panel2.Children.Add(pnSub2);
			panel2.Children.Add(pnSub1);

			// pnValue
			pnValue = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
			pnValue.Children.Add(label8);
			pnValue.Children.Add(tbValue);

			// pnInventory
			pnInventory = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
			pnInventory.Children.Add(label7);
			pnInventory.Children.Add(tbInv);

			// pnOwner
			pnOwner = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
			pnOwner.Children.Add(label3);
			pnOwner.Children.Add(cbOwner);
			pnOwner.Children.Add(llme);

			// pnSelection
			pnSelection = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
			pnSelection.Children.Add(lbtype);
			pnSelection.Children.Add(label2);
			pnSelection.Children.Add(pb);
			pnSelection.Children.Add(cbtype);
			pnSelection.Children.Add(cbObjs);
			pnSelection.Children.Add(cbToks);
			pnSelection.Children.Add(cbMems);

			// pnListing
			pnListing = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
			pnListing.Children.Add(label10);
			pnListing.Children.Add(rbObjs);
			pnListing.Children.Add(rbToks);
			pnListing.Children.Add(rbMems);

			// pnFlags
			pnFlags = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
			pnFlags.Children.Add(label9);
			pnFlags.Children.Add(cbVis);
			pnFlags.Children.Add(cbCtrl);
			pnFlags.Children.Add(tbFlag);

			// panel3 (unknown field)
			panel3 = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
			panel3.Children.Add(label11);
			panel3.Children.Add(tbUnk);

			// tabPage3 content (main properties tab)
			var tab3Content = new StackPanel();
			tab3Content.Children.Add(panel3);
			tab3Content.Children.Add(panel2);
			tab3Content.Children.Add(pnValue);
			tab3Content.Children.Add(pnInventory);
			tab3Content.Children.Add(pnOwner);
			tab3Content.Children.Add(pnSelection);
			tab3Content.Children.Add(pnListing);
			tab3Content.Children.Add(pnFlags);

			tabPage3.Header = "Properties";
			tabPage3.Content = tab3Content;

			// panel1 (raw data tab top panel)
			panel1 = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
			panel1.Children.Add(label1);
			panel1.Children.Add(tbRawLength);
			panel1.Children.Add(llSetRawLength);

			// tabPage4 content (raw/property grid tab)
			var tab4Content = new StackPanel();
			tab4Content.Children.Add(panel1);
			tab4Content.Children.Add(pg);

			tabPage4.Header = "Raw";
			tabPage4.Content = tab4Content;

			// Wire up TabControl
			tabControl2.Items.Add(tabPage3);
			tabControl2.Items.Add(tabPage4);

			this.Content = tabControl2;
		}
		#endregion


		NgbhItem item;
		private PropertyGridStub pg;
		private TabControl tabControl2;
		private TabItem tabPage3;
		private TabItem tabPage4;
		private TextBox tbRawLength;
		private TextBlock label1;
		private StackPanel panel1;
		private Button llSetRawLength;
		private TextBlock lbtype;
		private Ambertation.Windows.Forms.EnumComboBox cbtype;
		private SimPe.PackedFiles.Wrapper.ObjectComboBox cbMems;
		private SimPe.PackedFiles.Wrapper.ObjectComboBox cbToks;
		private TextBlock label2;
		private SimPe.PackedFiles.Wrapper.ObjectComboBox cbObjs;
		private PictureBoxCompat pb;
		private StackPanel pnSelection;
		private StackPanel pnOwner;
		private TextBlock label3;
		private Button llme;
		SimPe.PackedFiles.Wrapper.SimComboBox cbOwner;
		private TextBlock label4;
		private StackPanel pnSubject;
		private SimPe.PackedFiles.Wrapper.SimComboBox cbSubject;
		private SimPe.PackedFiles.Wrapper.ObjectComboBox cbSubjectObj;
		private StackPanel pnObjectGuid;
		private TextBlock label5;
		private Button llme2;
		private StackPanel panel2;
		private StackPanel pnSub2;
		private StackPanel pnSub1;
		private TextBlock label6;
		private StackPanel pnInventory;
		private TextBlock label7;
		private TextBox tbInv;
		private StackPanel pnValue;
		private TextBox tbValue;
		private TextBlock label8;
		private StackPanel pnFlags;
		private TextBlock label9;
		private CheckBox cbVis;
		private TextBox tbFlag;
		private CheckBox cbCtrl;
		private StackPanel pnListing;
		private TextBlock label10;
		private RadioButton rbMems;
		private RadioButton rbToks;
		private RadioButton rbObjs;
		private StackPanel panel3;
		private TextBox tbUnk;
		private TextBlock label11;

		[System.ComponentModel.Browsable(false)]
		public NgbhItem Item
		{
			get {return item;}
			set
			{
				item = value;
				SetContent();
			}
		}

		Plugin.NgbhItemsListView nilv;
		public Plugin.NgbhItemsListView NgbhItemsListView
		{
			get {return nilv;}
			set
			{
				if (nilv!=null) nilv.SelectedIndexChanged -= new EventHandler(nilv_SelectedIndexChanged);
				nilv = value;
				if (nilv!=null) nilv.SelectedIndexChanged += new EventHandler(nilv_SelectedIndexChanged);

				nilv_SelectedIndexChanged(null, null);
			}
		}

		public event EventHandler ChangedItem;
		protected void UpdateNgbhItemsListView()
		{
			if (nilv!=null) nilv.UpdateSelected(item);
		}
		protected void FireChangeEvent()
		{
			UpdateNgbhItemsListView();
			if (ChangedItem!=null) ChangedItem(this, new EventArgs());
		}

		bool inter;
		bool chgraw;
		void SetContent()
		{
			if (inter) return;	inter = true;
			chgraw = false;
			pg.SelectedObject = null;
			pb.Image = null;
			if (item!=null)
			{
				this.IsEnabled = true;
				Hashtable ht = new Hashtable();
				byte ct=0;
				foreach (string v in item.MemoryCacheItem.ValueNames)
					ht[Helper.HexString(ct)+": "+v] = new Ambertation.BaseChangeableNumber(item.GetValue(ct++));

				while (ct<item.Data.Length)
					ht[Helper.HexString(ct)+":"] = new Ambertation.BaseChangeableNumber(item.GetValue(ct++));

				Ambertation.PropertyObjectBuilderExt pob = new Ambertation.PropertyObjectBuilderExt(ht);

				pg.SelectedObject = pob.Instance;

				this.tbRawLength.Text = item.Data.Length.ToString();
				this.cbtype.SelectedValue = item.MemoryType;

				UpdateSelectedItem();

				pb.Image = item.MemoryCacheItem.Image;

				SelectOwner(this.cbOwner, item);
				SelectSubject(item);

				tbInv.Text = item.InventoryNumber.ToString();
				this.tbValue.Text = item.Value.ToString();
				tbUnk.Text = SimPe.Helper.HexString(item.UnknownNumber);
				UpdateFlagsValue();
			}
			else
			{
				this.IsEnabled = false;
			}
			inter = false;
		}

		void UpdateFlagsValue()
		{
			this.tbFlag.Text = "0x" + Helper.HexString(item.Flags.Value);
		}

		void UpdateSelectedItem()
		{
			bool use = (!item.MemoryCacheItem.IsToken && !item.MemoryCacheItem.IsInventory);
			this.cbMems.IsVisible = use;
			this.rbMems.IsChecked = use;
			if (use) SelectNgbhItem(cbMems, item);


			use = item.MemoryCacheItem.IsToken && !item.MemoryCacheItem.IsInventory;
			this.cbToks.IsVisible = use;
			this.rbToks.IsChecked = use;
			if (use)  SelectNgbhItem(cbToks, item);

			use = (!item.MemoryCacheItem.IsToken && item.MemoryCacheItem.IsInventory);
			this.cbObjs.IsVisible = use;
			this.rbObjs.IsChecked = use;
			if (use) SelectNgbhItem(cbObjs, item);
		}

		void SelectNgbhItem(SimPe.PackedFiles.Wrapper.ObjectComboBox cb, NgbhItem item)
		{
			cb.SelectedGuid = item.Guid;
		}

		void SelectOwner(SimPe.PackedFiles.Wrapper.SimComboBox cb, NgbhItem item)
		{
			cb.SelectedSimInstance = item.OwnerInstance;
		}

		void SelectSubject(NgbhItem item)
		{
			this.cbSubject.SelectedSimId = item.SubjectGuid;
			if (item.MemoryType == SimMemoryType.Object)
				this.cbSubjectObj.SelectedGuid = item.ReferencedObjectGuid;
			else
				this.cbSubjectObj.SelectedGuid = item.SubjectGuid;
		}

		private void nilv_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (nilv!=null)
			{
				Plugin.NgbhItemsListViewItem lvi = nilv.SelectedItem;
				if (lvi!=null && !nilv.SelectedMultiple)
					Item = lvi.Item;
				else
					Item = null;
			}
			else
				Item = null;
		}

		private void llSetRawLength_Click(object sender, System.EventArgs e)
		{
			if (this.item!=null)
			{
				ushort[] ndata = new ushort[Helper.StringToInt32(this.tbRawLength.Text, item.Data.Length, 10)];
				for (int i=0; i<ndata.Length; i++)
					if (i<item.Data.Length) ndata[i] = item.Data[i];
					else ndata[i] = 0;
				item.Data = ndata;
				SetContent();
			}
		}

		private void ChangeGuid(object sender, System.EventArgs e)
		{
			if (inter) return;
			if (item==null) return;
			SimPe.PackedFiles.Wrapper.ObjectComboBox cb = sender as SimPe.PackedFiles.Wrapper.ObjectComboBox;
			item.Guid = cb.SelectedGuid;
			SetContent();
			this.FireChangeEvent();
		}

		private void pg_PropertyValueChanged(object s, EventArgs e)
		{
			// PropertyGrid not available on Mac; this event never fires.
		}

		private void tabPage3_VisibleChanged(object sender, System.EventArgs e)
		{
			if (this.tabPage3.IsSelected && chgraw)
			{
				SetContent();
			}
		}

		private void cbtype_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SimMemoryType smt = (SimMemoryType)cbtype.SelectedValue;

			this.pnOwner.IsVisible = (smt==SimMemoryType.Memory || smt==SimMemoryType.Gossip || smt == SimMemoryType.GossipInventory);
			this.pnSub1.IsVisible = (smt==SimMemoryType.Memory || smt==SimMemoryType.Gossip);
			this.pnSub2.IsVisible = this.pnSub1.IsVisible;
			this.pnSubject.IsVisible = (smt==SimMemoryType.Memory || smt==SimMemoryType.Gossip);
			this.pnObjectGuid.IsVisible = (smt==SimMemoryType.Memory || smt==SimMemoryType.Gossip || smt== SimMemoryType.Object);

			this.pnInventory.IsVisible = (smt==SimMemoryType.Inventory || smt==SimMemoryType.GossipInventory);
			this.pnValue.IsVisible = (smt==SimMemoryType.Skill || smt == SimMemoryType.Badge || smt==SimMemoryType.ValueToken);
			this.pnFlags.IsVisible = true;

			this.pnListing.IsVisible = Helper.XmlRegistry.HiddenMode;
		}

		void SetMe(SimPe.PackedFiles.Wrapper.SimComboBox cb)
		{
			if (item==null) return;
			cb.SelectedSimInstance = (ushort)item.ParentSlot.SlotID;
		}

		private void llme_Click(object sender, System.EventArgs e)
		{
			SetMe(this.cbOwner);
		}

		private void llme2_Click(object sender, System.EventArgs e)
		{
			SetMe(this.cbSubject);
		}

		private void cbOwner_SelectedSimChanged(object sender, System.EventArgs e)
		{
			if (inter) return;
			if (item==null) return;

			item.OwnerInstance = cbOwner.SelectedSimInstance;

			SetContent();
			this.FireChangeEvent();
		}

		private void cbSubject_SelectedSimChanged(object sender, System.EventArgs e)
		{
			if (inter) return;
			if (item==null) return;

			inter = true;
			this.cbSubjectObj.SelectedGuid = 0xffffffff;
			item.SetSubject(this.cbSubject.SelectedSimInstance, this.cbSubject.SelectedSimId);
			inter = false;

			SetContent();
			this.FireChangeEvent();
		}

		private void cbSubjectObj_SelectedObjectChanged(object sender, System.EventArgs e)
		{
			if (inter) return;
			if (item==null) return;

			inter = true;

			this.cbSubject.SelectedSimId = 0xffffffff;
			if (item.MemoryType == SimMemoryType.Object)
				item.ReferencedObjectGuid = this.cbSubjectObj.SelectedGuid;
			else
				item.SetSubject(0, this.cbSubjectObj.SelectedGuid);
			inter = false;

			SetContent();
			this.FireChangeEvent();
		}

		private void tbInv_TextChanged(object sender, System.EventArgs e)
		{
			if (inter) return;
			if (item==null) return;

			item.InventoryNumber = Helper.StringToUInt32(this.tbInv.Text, item.InventoryNumber, 10);
			SetContent();
			this.FireChangeEvent();
		}

		private void tbValue_TextChanged(object sender, System.EventArgs e)
		{
			if (inter) return;
			if (item==null) return;

			item.Value = Helper.StringToUInt16(this.tbValue.Text, item.Value, 10);
			this.FireChangeEvent();
		}

		private void tbFlag_TextChanged(object sender, System.EventArgs e)
		{
			if (item==null) return;
			this.cbCtrl.IsChecked = item.Flags.IsControler;
			this.cbVis.IsChecked = item.Flags.IsVisible;
		}

		private void cbVis_CheckedChanged(object sender, System.EventArgs e)
		{
			if (inter) return;
			if (item==null) return;

			inter = true;
			item.Flags.IsVisible = this.cbVis.IsChecked == true;
			UpdateFlagsValue();
			inter = false;
			SetContent();
			this.FireChangeEvent();
		}

		private void cbAct_CheckedChanged(object sender, System.EventArgs e)
		{
			if (inter) return;
			if (item==null) return;

			inter = true;
			item.Flags.IsControler = this.cbCtrl.IsChecked == true;
			UpdateFlagsValue();
			inter = false;
			SetContent();
			this.FireChangeEvent();
		}

		private void rbObjs_CheckedChanged(object sender, System.EventArgs e)
		{
			if (inter) return;
			cbObjs.IsVisible = true;
			cbMems.IsVisible = false;
			cbToks.IsVisible = false;
		}

		private void rbMems_CheckedChanged(object sender, System.EventArgs e)
		{
			if (inter) return;
			cbObjs.IsVisible = false;
			cbMems.IsVisible = true;
			cbToks.IsVisible = false;
		}

		private void rbToks_CheckedChanged(object sender, System.EventArgs e)
		{
			if (inter) return;
			cbObjs.IsVisible = false;
			cbMems.IsVisible = false;
			cbToks.IsVisible = true;
		}


	}
}
