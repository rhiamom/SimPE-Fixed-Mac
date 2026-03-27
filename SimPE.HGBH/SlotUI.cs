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

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for SlotUI.
	/// </summary>
	public class NgbhSlotUI : Avalonia.Controls.UserControl
	{
		private TabControl tabControl1;
		private TabItem tabPage1;
        private TabItem tabPage2;
        internal TabItem tabPage3;
		private NgbhItemsListView lv;
        private NgbhItemsListView lvint;
        private NgbhItemsListView lvfam;
		private GridSplitter splitter1;
		private MemoryProperties memprop;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NgbhSlotUI()
		{
			// Required designer variable.
			InitializeComponent();

			SlotType = Data.NeighborhoodSlots.Sims;
            tabPage2_VisibleChanged(null, null);

            if (Helper.XmlRegistry.HiddenMode)
            {
                this.tabControl1.Items.Remove(this.tabPage3);
            }
		}

		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lv        = new SimPe.Plugin.NgbhItemsListView();
			this.lvint     = new SimPe.Plugin.NgbhItemsListView();
			this.lvfam     = new SimPe.Plugin.NgbhItemsListView();
			this.memprop   = new SimPe.Plugin.MemoryProperties();
			this.splitter1 = new GridSplitter();
			this.tabPage1  = new TabItem { Header = "Memories" };
			this.tabPage2  = new TabItem { Header = "Tokens (Skills, Badges...)" };
			this.tabPage3  = new TabItem { Header = "Family Inventory" };
			this.tabControl1 = new TabControl();

			// lv
			this.lv.NgbhItems = null;
			this.lv.Slot = null;
			this.lv.ShowGossip = true;
			this.lv.SlotType = SimPe.Data.NeighborhoodSlots.Sims;

			// lvint
			this.lvint.NgbhItems = null;
			this.lvint.Slot = null;
			this.lvint.SlotType = SimPe.Data.NeighborhoodSlots.Sims;

			// lvfam
			this.lvfam.NgbhItems = null;
			this.lvfam.Slot = null;
			this.lvfam.SlotType = SimPe.Data.NeighborhoodSlots.Families;

			// tabPage2/3 selection change → tabPage2_VisibleChanged
			this.tabControl1.SelectionChanged += (s, e) => this.tabPage2_VisibleChanged(s, EventArgs.Empty);

			// memprop
			this.memprop.Item = null;
			this.memprop.NgbhItemsListView = null;

			// tab pages
			this.tabPage1.Content = this.lv;
			this.tabPage2.Content = this.lvint;
			this.tabPage3.Content = this.lvfam;
			this.tabControl1.Items.Add(this.tabPage1);
			this.tabControl1.Items.Add(this.tabPage2);
			this.tabControl1.Items.Add(this.tabPage3);

			// layout: tabControl + splitter + memprop in a DockPanel
			var dock = new DockPanel();
			DockPanel.SetDock(this.memprop,   Dock.Bottom);
			DockPanel.SetDock(this.splitter1, Dock.Bottom);
			dock.Children.Add(this.memprop);
			dock.Children.Add(this.splitter1);
			dock.Children.Add(this.tabControl1);
			this.Content = dock;
			this.Name = "NgbhSlotUI";
		}
		#endregion

		#region Properties		
		Data.NeighborhoodSlots st;
		public Data.NeighborhoodSlots SlotType 
		{
			get {return st;}
			set 
			{
				st = value;
				lv.NgbhItems = null;
                lvint.NgbhItems = null;
                lvfam.NgbhItems = null;
                lvfam.SlotType = SimPe.Data.NeighborhoodSlots.Families;
				if (st== SimPe.Data.NeighborhoodSlots.Sims || st==SimPe.Data.NeighborhoodSlots.SimsIntern)
				{
					this.tabPage1.Header = SimPe.Localization.GetString("SimPe.Data.NeighborhoodSlots.Sims");
					this.tabPage2.Header = SimPe.Localization.GetString("SimPe.Data.NeighborhoodSlots.SimsIntern");
					
					lv.SlotType = SimPe.Data.NeighborhoodSlots.Sims;
					lvint.SlotType = SimPe.Data.NeighborhoodSlots.SimsIntern;
				} 
				else if (st== SimPe.Data.NeighborhoodSlots.Families || st==SimPe.Data.NeighborhoodSlots.FamiliesIntern)
				{
					this.tabPage1.Header = SimPe.Localization.GetString("SimPe.Data.NeighborhoodSlots.Families");
					this.tabPage2.Header = SimPe.Localization.GetString("SimPe.Data.NeighborhoodSlots.FamiliesIntern");
					
					lv.SlotType = SimPe.Data.NeighborhoodSlots.Families;
					lvint.SlotType = SimPe.Data.NeighborhoodSlots.FamiliesIntern;
				}
				else 
				{
					this.tabPage1.Header = SimPe.Localization.GetString("SimPe.Data.NeighborhoodSlots.Lots");
					this.tabPage2.Header = SimPe.Localization.GetString("SimPe.Data.NeighborhoodSlots.LotsIntern");
					
					lv.SlotType = SimPe.Data.NeighborhoodSlots.Lots;
					lvint.SlotType = SimPe.Data.NeighborhoodSlots.LotsIntern;
				}
				SetContent();
			}			
		}

        NgbhSlot slot;
        NgbhSlot Slut;
		[System.ComponentModel.Browsable(false)]
		public NgbhSlot Slot
		{
			get {return slot;}
			set 
			{
				slot = value;
				SetContent();			
			}
		}

		Ngbh ngbh;
		[System.ComponentModel.Browsable(false)]
		public Ngbh NgbhResource
		{
			get {return ngbh;}
			set 
			{
				ngbh = value;
				SetContent();
				pc_SelectedSimChanged(pc, null, null);
			}
		}

		SimPe.PackedFiles.Wrapper.SimPoolControl pc;
		public SimPe.PackedFiles.Wrapper.SimPoolControl SimPoolControl
		{
			get {return pc;}
			set {
				if (pc!=null) pc.SelectedSimChanged -= new SimPe.PackedFiles.Wrapper.SimPoolControl.SelectedSimHandler(pc_SelectedSimChanged);
				pc = value;
				if (pc!=null) 
				{
					pc.SelectedSimChanged += new SimPe.PackedFiles.Wrapper.SimPoolControl.SelectedSimHandler(pc_SelectedSimChanged);
					pc_SelectedSimChanged(pc, null, null);
				}
			}
		}

		#endregion

		void SetContent()
		{
			lv.Slot = slot;
            lvint.Slot = slot;
            lvfam.Slot = Slut;
		}

		public new void Refresh()
        {
			lv.Refresh();
            lvint.Refresh();
            lvfam.Refresh();
		}

		private void pc_SelectedSimChanged(object sender, System.Drawing.Image thumb, SimPe.PackedFiles.Wrapper.SDesc sdesc)
		{
			if (ngbh!=null && pc!=null) 
			{
                if (pc.SelectedSim != null)
                {
                    this.Slut = ngbh.GetSlots(SimPe.Data.NeighborhoodSlots.Families).GetInstanceSlot(pc.SelectedSim.FamilyInstance);
                    this.Slot = ngbh.GetSlots(st).GetInstanceSlot(pc.SelectedSim.FileDescriptor.Instance);
                }
                else
                {
                    this.Slut = null;
                    this.Slot = null;
                }
			}
		}

		private void tabPage2_VisibleChanged(object sender, System.EventArgs e)
		{
			if (tabControl1.SelectedItem == this.tabPage1)
				memprop.NgbhItemsListView = lv;
            else if (tabControl1.SelectedItem == this.tabPage3)
                memprop.NgbhItemsListView = lvfam;
			else
				memprop.NgbhItemsListView = lvint;
		}
	}
}
