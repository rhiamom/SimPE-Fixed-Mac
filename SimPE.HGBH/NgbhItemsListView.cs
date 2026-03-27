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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using Avalonia.Controls;
using SimPe.Scenegraph.Compat;

namespace SimPe.Plugin
{
	/// <summary>
	/// Replacement for WinForms ListViewItemCollection used by NgbhItemsListView.
	/// </summary>
	public class NgbhListViewItemCollection : IEnumerable
	{
		private readonly ObservableCollection<NgbhItemsListViewItem> _list;

		internal NgbhListViewItemCollection(ObservableCollection<NgbhItemsListViewItem> list)
		{
			_list = list;
		}

		public void Add(NgbhItemsListViewItem item) { _list.Add(item); }
		public void Insert(int index, NgbhItemsListViewItem item) { _list.Insert(index, item); }
		public void Remove(NgbhItemsListViewItem item) { _list.Remove(item); }
		public void Clear() { _list.Clear(); }
		public int Count => _list.Count;
		public NgbhItemsListViewItem this[int index] { get => _list[index]; set => _list[index] = value; }
		public IEnumerator GetEnumerator() => _list.GetEnumerator();
	}

	/// <summary>
	/// Summary description for NgbhItemsListView.
	/// </summary>
	public class NgbhItemsListView : Avalonia.Controls.UserControl
    {
        private IContainer components;
		private Avalonia.Controls.StackPanel panel1;
		private Avalonia.Controls.ComboBox cbadd;
		private Avalonia.Controls.Button lladd;
		private Avalonia.Controls.Button lldel;
		private Avalonia.Controls.Button btUp;
		private Avalonia.Controls.Button btDown;
		private Avalonia.Controls.MenuItem miCopy;
		private Avalonia.Controls.MenuItem miPaste;
		private Avalonia.Controls.ContextMenu menu;
		private Avalonia.Controls.MenuItem miPasteGossip;
		private Avalonia.Controls.MenuItem miClone;
		private Avalonia.Controls.MenuItem miDelCascade;
		private Avalonia.Controls.ListBox lv;
        private Avalonia.Controls.CheckBox cbnogoss;

		private ObservableCollection<NgbhItemsListViewItem> _lvItems
			= new ObservableCollection<NgbhItemsListViewItem>();
		private NgbhListViewItemCollection _itemsCollection;

        ThemeManager tm;
		public NgbhItemsListView()
		{
			InitializeComponent();

			SmallImageList = new ImageList();
			SmallImageList.ImageSize = new Size(NgbhItem.ICON_SIZE, NgbhItem.ICON_SIZE);
			SmallImageList.ColorDepth = ColorDepth.Depth32Bit;

			lv.SelectionChanged += (s, e) => lv_SelectedIndexChanged(s, EventArgs.Empty);

			SlotType = Data.NeighborhoodSlots.Sims;

            tm = ThemeManager.Global.CreateChild();
            tm.AddControl(menu);
			InitTheo();
		}

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
			_itemsCollection = new NgbhListViewItemCollection(_lvItems);

            this.components    = new System.ComponentModel.Container();
            this.lv            = new Avalonia.Controls.ListBox();
            this.menu          = new Avalonia.Controls.ContextMenu();
            this.miCopy        = new Avalonia.Controls.MenuItem { Header = "Copy" };
            this.miPaste       = new Avalonia.Controls.MenuItem { Header = "Paste" };
            this.miPasteGossip = new Avalonia.Controls.MenuItem { Header = "Paste as Gossip" };
            this.miClone       = new Avalonia.Controls.MenuItem { Header = "Clone" };
            this.miDelCascade  = new Avalonia.Controls.MenuItem { Header = "Delete Cascade" };
            this.panel1        = new Avalonia.Controls.StackPanel();
            this.cbnogoss      = new Avalonia.Controls.CheckBox { Content = "Hide Gossip" };
            this.lladd         = new Avalonia.Controls.Button { Content = "Add" };
            this.cbadd         = new Avalonia.Controls.ComboBox();
            this.lldel         = new Avalonia.Controls.Button { Content = "Delete" };
            this.btUp          = new Avalonia.Controls.Button { Content = "Up" };
            this.btDown        = new Avalonia.Controls.Button { Content = "Down" };

            // lv
            this.lv.ItemsSource   = this._lvItems;
            this.lv.SelectionMode = Avalonia.Controls.SelectionMode.Multiple;
            this.lv.ContextMenu   = this.menu;
            this.lv.Name          = "lv";
            this.lv.SelectionChanged += (s, e) => lv_SelectedIndexChanged_1(s, EventArgs.Empty);

            // context menu
            this.menu.Items.Add(this.miCopy);
            this.menu.Items.Add(this.miPaste);
            this.menu.Items.Add(this.miPasteGossip);
            this.menu.Items.Add(this.miClone);
            this.menu.Items.Add(this.miDelCascade);
            this.menu.Opening        += (s, e) => this.menu_VisibleChanged(s, EventArgs.Empty);
            this.miCopy.Click        += (s, e) => this.CopyItems(s, EventArgs.Empty);
            this.miPaste.Click       += (s, e) => this.PasteItems(s, EventArgs.Empty);
            this.miPasteGossip.Click += (s, e) => this.PasteItemsAsGossip(s, EventArgs.Empty);
            this.miClone.Click       += (s, e) => this.CloneItem(s, EventArgs.Empty);
            this.miDelCascade.Click  += (s, e) => this.DeleteCascadeItems(s, EventArgs.Empty);

            // cbnogoss
            this.cbnogoss.Name = "cbnogoss";
            this.cbnogoss.IsCheckedChanged += (s, e) => this.cbnogoss_CheckedChanged(s, EventArgs.Empty);

            // lladd
            this.lladd.Name = "lladd";
            this.lladd.Click += (s, e) => lladd_Click(s, EventArgs.Empty);

            // cbadd
            this.cbadd.Name = "cbadd";
            this.cbadd.SelectionChanged += (s, e) => cbadd_SelectedIndexChanged(s, EventArgs.Empty);

            // lldel
            this.lldel.Name = "lldel";
            this.lldel.Click += (s, e) => lldel_Click(s, EventArgs.Empty);

            // btUp / btDown
            this.btUp.Name   = "btUp";
            this.btUp.Click  += (s, e) => this.btUp_Click(s, EventArgs.Empty);
            this.btDown.Name = "btDown";
            this.btDown.Click += (s, e) => this.btDown_Click(s, EventArgs.Empty);

            // panel1 — toolbar strip
            this.panel1.Orientation = Avalonia.Layout.Orientation.Horizontal;
            this.panel1.Children.Add(this.cbnogoss);
            this.panel1.Children.Add(this.lladd);
            this.panel1.Children.Add(this.cbadd);
            this.panel1.Children.Add(this.lldel);
            this.panel1.Children.Add(this.btUp);
            this.panel1.Children.Add(this.btDown);

            // layout: panel1 at bottom, lv fills the rest
            var dock = new DockPanel();
            DockPanel.SetDock(this.panel1, Dock.Bottom);
            dock.Children.Add(this.panel1);
            dock.Children.Add(this.lv);
            this.Content = dock;
            this.Name = "NgbhItemsListView";
		}
		#endregion

		SimPe.Data.NeighborhoodSlots st;
		public SimPe.Data.NeighborhoodSlots SlotType
		{
			get {return st;}
			set
			{
				if (st!=value)
				{
					st = value;
					SetContent();
				}
			}
		}

        bool cc = false;

        [Category("Appearance")]
        [DefaultValue(typeof(bool), "false")]
        [Browsable(true)]
        public bool ShowGossip
        { get { return cc; } set { cc = value; this.cbnogoss.IsVisible = cc; } }

		public NgbhSlotList Slot
		{
			get
			{
				if (NgbhItems==null) return null;
				else return NgbhItems.Parent;
			}
			set
			{
				if (value!=null)
					NgbhItems = value.GetItems(this.SlotType);
				else
					NgbhItems = null;
			}
		}

		Collections.NgbhItems items;
		[System.ComponentModel.Browsable(false)]
		public Collections.NgbhItems NgbhItems
		{
			get {return items;}
			set
            {
				items = value;
				SetContent();
			}
		}

		void SetContent()
		{
			this.Clear();
			this.cbadd.Items.Clear();

			if (items!=null)
			{
                foreach (NgbhItem i in items)
                {
                    if (cbnogoss.IsChecked == true) { if (!i.IsGossip) AddItemToList(i); }
                    else AddItemToList(i);
                }

				SetAvailableAddTypes();
			}
		}

		public void Refresh(bool full)
		{
			if (full) SetContent();
			InvalidateVisual();
		}
		public new void Refresh()
        {
			Refresh(true);
		}

		void AddItemToList(NgbhItem item)
		{
			if (item==null) return;

			NgbhItemsListViewItem lvi = new NgbhItemsListViewItem(this, item);
		}

		void InsertItemToList(int index, NgbhItem item)
		{
			if (item==null) return;

			NgbhItemsListViewItem lvi = new NgbhItemsListViewItem(this, item, false);
			Items.Insert(index, lvi);
		}

		void SetAvailableAddTypes()
		{
			string prefix = typeof(SimMemoryType).Namespace+"."+typeof(SimMemoryType).Name+".";
			SimMemoryType[] sts = Ngbh.AllowedMemoryTypes(st);
			foreach (SimMemoryType mst in sts)
				cbadd.Items.Add(new Data.Alias((uint)mst, SimPe.Localization.GetString(prefix+ mst.ToString()), "{name}"));
			if (cbadd.Items.Count>0) cbadd.SelectedIndex = 0;
		}

		public void Clear()
		{
			_lvItems.Clear();
			sil?.Images.Clear();
		}

		[System.ComponentModel.Browsable(false)]
		public NgbhItemsListViewItem SelectedItem
		{
			get
			{
				return lv.SelectedItem as NgbhItemsListViewItem;
			}
		}

		[System.ComponentModel.Browsable(false)]
		public NgbhItem SelectedNgbhItem
		{
			get
			{
				if (SelectedItem==null) return null;
				return SelectedItem.Item;
			}
		}

		[System.ComponentModel.Browsable(false)]
		public Collections.NgbhItems SelectedNgbhItems
		{
			get
			{
				NgbhSlotList parent = null;
				if (items != null) parent = items.Parent;
				Collections.NgbhItems ret = new Collections.NgbhItems(parent);
				if (lv.SelectedItems != null)
					foreach (NgbhItemsListViewItem lvi in lv.SelectedItems)
						ret.Add(lvi.Item);

				return ret;
			}
		}

		[System.ComponentModel.Browsable(false)]
		public bool SelectedMultiple
		{
			get {return (lv.SelectedItems?.Count ?? 0) > 1;}
		}

		internal void UpdateSelected(NgbhItem item)
		{
			if (item==null) return;
			if (SelectedItem==null) return;

			SelectedItem.Update();
			this.Refresh(false);
        }

		public NgbhListViewItemCollection Items
		{
			get { return _itemsCollection; }
		}

		ImageList sil;
		public ImageList SmallImageList
		{
			get { return sil;}
			set
			{
				// lv.SmallImageList not assigned (Avalonia ListBox does not use ImageList)
				sil = value;
			}
		}

		public event EventHandler SelectedIndexChanged;

		private void lv_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedIndexChanged!=null) SelectedIndexChanged(this, e);
		}

		private void cbadd_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			lladd.IsEnabled = cbadd.SelectedIndex>=0 && items!=null;
		}

		private void lv_SelectedIndexChanged_1(object sender, System.EventArgs e)
		{
			int selCount = lv.SelectedItems?.Count ?? 0;
			lldel.IsEnabled = (selCount > 0 && items!=null);
			if (_lvItems.Count==0 || selCount!=1 || items==null)
			{
				btUp.IsEnabled = false;
				btDown.IsEnabled = false;
			}
			else
			{
				bool topSelected    = lv.SelectedItems?.Contains(_lvItems[0]) == true;
				bool bottomSelected = lv.SelectedItems?.Contains(_lvItems[_lvItems.Count-1]) == true;
				btUp.IsEnabled   = lldel.IsEnabled && !topSelected;
				btDown.IsEnabled = lldel.IsEnabled && !bottomSelected;
			}
		}

		private void lladd_Click(object sender, System.EventArgs e)
		{
			if (items==null || cbadd.SelectedIndex<0) return;

			Data.Alias a = cbadd.SelectedItem as Data.Alias;
			SimMemoryType smt = (SimMemoryType)a.Id;

			int index = this.NextItemIndex(true);
			NgbhItem item = items.InsertNew(index, smt);
			item.SetInitialGuid(smt);
			InsertItemToList(index, item);

			if (index < _lvItems.Count) lv.SelectedItem = _lvItems[index];
		}

		private void lldel_Click(object sender, System.EventArgs e)
		{
			if ((lv.SelectedItems?.Count ?? 0)==0 || items==null) return;
			Collections.NgbhItems nitems = this.SelectedNgbhItems;
			items.Remove(nitems);

			var toRemove = new List<NgbhItemsListViewItem>();
			if (lv.SelectedItems != null)
				foreach (NgbhItemsListViewItem li in lv.SelectedItems)
					toRemove.Add(li);
			foreach (var li in toRemove)
				_lvItems.Remove(li);
		}

		void SwapListViewItem(int i1, int i2)
		{
			if (i1<0 || i2<0 || i1>=_lvItems.Count || i2>=_lvItems.Count) return;
			NgbhItemsListViewItem o1 = _lvItems[i1];
			NgbhItemsListViewItem o2 = _lvItems[i2];
			_lvItems[i1] = o2;
			_lvItems[i2] = o1;
		}

		int SelectedIndex
		{
			get
			{
				var sel = lv.SelectedItem as NgbhItemsListViewItem;
				if (sel == null) return -1;
				return _lvItems.IndexOf(sel);
			}
		}

		private void btUp_Click(object sender, System.EventArgs e)
		{
			int index = SelectedIndex;
			items.Swap(index, index-1);
			SwapListViewItem(index, index-1);
		}

		private void btDown_Click(object sender, System.EventArgs e)
		{
			int index = SelectedIndex;
			items.Swap(index, index+1);
			SwapListViewItem(index, index+1);
        }

        private void cbnogoss_CheckedChanged(object sender, EventArgs e)
        {
            SetContent();
        }

		#region Extensions by Theo
		System.Collections.Queue clipboard;

		void InitTheo()
		{
			clipboard = new Queue();
		}


		void CopyItems(object sender, EventArgs e)
		{
			CopyItems();
		}

		void CopyItems()
		{
			Collections.NgbhItems selitems = SelectedNgbhItems;
			if (selitems.Count > 0)
			{
				try
				{
					clipboard.Clear();
					foreach (NgbhItem item in selitems)
					{
						clipboard.Enqueue(item);
					}
				}
				catch (Exception exception1)
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), exception1);
				}
			}
        }

		void PasteItems(object sender, EventArgs e)
		{
			PasteItems(false);
		}

		void PasteItemsAsGossip(object sender, EventArgs e)
		{
			PasteItems(true);
		}

		void PasteItems(bool asgossip)
		{
			System.Collections.Queue newq = new Queue();
			try
			{
				while (clipboard.Count > 0)
				{
					NgbhItem item = clipboard.Dequeue() as NgbhItem;
					newq.Enqueue(item);

					if (item != null)
					{
						item = item.Clone(this.Slot);

						if (item.IsMemory && item.OwnerInstance > 0 && !asgossip)
							item.OwnerInstance = (ushort)items.Parent.SlotID;

						if (asgossip)
							item.Flags.IsVisible = false;

						AddItemAfterSelected(item);
					}
				}
			}
			catch (Exception exception1)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), exception1);
			}

			clipboard.Clear();
			clipboard = newq;
		}

		void AddItemAfterSelected(NgbhItem item)
		{
			try
			{
				int selectedIndex = this.NextItemIndex(true);

				NgbhItems.Insert(selectedIndex, item);
				this.AddItemAt(item, selectedIndex);
			}
			catch (Exception exception1)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), exception1);
			}
		}

		private void AddItemAt(NgbhItem item, int index)
		{
			this.InsertItemToList(index, item);
			lv.SelectedItems?.Clear();
			if (index < _lvItems.Count) lv.SelectedItem = _lvItems[index];
			if (this.SelectedIndexChanged!=null) SelectedIndexChanged(this, new EventArgs());
		}

		int NextItemIndex(bool clearSelection)
		{
			int selectedIndex = _lvItems.Count - 1;

			// get index of the last selected item (if any)
			if (lv.SelectedItems != null && lv.SelectedItems.Count > 0)
			{
				int maxIdx = -1;
				foreach (NgbhItemsListViewItem item in lv.SelectedItems)
				{
					int idx = _lvItems.IndexOf(item);
					if (idx > maxIdx) maxIdx = idx;
				}
				if (maxIdx >= 0) selectedIndex = maxIdx;
			}

			// deselect previous (if applicable)
			if (clearSelection)
				lv.SelectedItems?.Clear();

			// should not exceed the number of items
			selectedIndex = Math.Min(++selectedIndex, _lvItems.Count);

			return selectedIndex;
		}

		void CloneItem(object sender, EventArgs e)
		{
			CloneItem();
		}

		void CloneItem()
		{
			if ((lv.SelectedItems?.Count ?? 0) > 0)
			{
				try
				{
					NgbhItem item = this.GetFocusedItem();
					if (item != null)
					{
						int itemIndex = SelectedIndex + 1;
						NgbhItem item1 = item.Clone();

						items.Insert(itemIndex, item1);
						this.AddItemAt(item1, itemIndex);
					}
				}
				catch (Exception exception1)
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), exception1);
				}
			}

		}

		NgbhItem GetFocusedItem()
		{
			NgbhItemsListViewItem li = this.SelectedItem;
			if (li==null) return null;
			return li.Item;
		}

		public void SelectMemoriesByGuid(Collections.NgbhItems items)
		{
			if (items.Length > 0)
			{
				this.lv.IsEnabled = false;

				ArrayList guidList = new ArrayList();
				foreach (NgbhItem item in items)
					if (!guidList.Contains(item.Guid))
						guidList.Add(item.Guid);

				foreach (NgbhItemsListViewItem li in _lvItems)
				{
					NgbhItem item = li.Tag as NgbhItem;
					if (item != null && guidList.Contains(item.Guid))
						lv.SelectedItems?.Add(li);
				}

				this.lv.IsEnabled = true;
			}
		}

		void DeleteItems(object sender, EventArgs e)
		{
			this.DeleteItems(false);
		}

		void DeleteCascadeItems(object sender, EventArgs e)
		{
			this.DeleteItems(true);
		}

		void DeleteItems(bool cascade)
		{
			if ((lv.SelectedItems?.Count ?? 0) != 0)
			{
				try
				{
					var itemsCopy = new List<NgbhItemsListViewItem>();
					if (lv.SelectedItems != null)
						foreach (NgbhItemsListViewItem li in lv.SelectedItems)
							itemsCopy.Add(li);

					Collections.NgbhItems memoryItems = this.SelectedNgbhItems;

					if (cascade)
						((EnhancedNgbh)Slot.Parent).DeleteMemoryEchoes(memoryItems, Slot.SlotID);

					memoryItems[0].ParentSlot.ItemsB.Remove(memoryItems);

					foreach (NgbhItemsListViewItem li in itemsCopy)
						_lvItems.Remove(li);

					lv.SelectedItems?.Clear();
				}
				catch (Exception exception1)
				{
					Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), exception1);
				}
			}
		}

        void menu_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
				int selCount = lv.SelectedItems?.Count ?? 0;
                miCopy.IsEnabled    = selCount > 0;
                miClone.IsEnabled   = miCopy.IsEnabled;
                miPaste.IsEnabled   = clipboard.Count > 0;

                if (((NgbhSlot)items.Parent).Type == Data.NeighborhoodSlots.Sims || ((NgbhSlot)items.Parent).Type == Data.NeighborhoodSlots.SimsIntern)
                {
                    miDelCascade.IsEnabled  = miCopy.IsEnabled;
                    miPasteGossip.IsEnabled = miPaste.IsEnabled;
                }
                else
                {
                    miDelCascade.IsEnabled  = false;
                    miPasteGossip.IsEnabled = false;
                }
            }
            catch { miCopy.IsEnabled = miPaste.IsEnabled = miPasteGossip.IsEnabled = miClone.IsEnabled = miDelCascade.IsEnabled = false; }
		}
        #endregion
    }
}
