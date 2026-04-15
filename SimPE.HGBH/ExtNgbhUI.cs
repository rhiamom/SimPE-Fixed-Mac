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
using SimPe;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces;
using SimPe.PackedFiles.Wrapper.Supporting;
using SimPe.Data;
using Ambertation.Windows.Forms;
using SimPe.Windows.Forms;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for ExtNgbhUI.
	/// </summary>
	public class ExtNgbhUI : 
		//System.Windows.Forms.UserControl
		SimPe.Windows.Forms.WrapperBaseControl, SimPe.Interfaces.Plugin.IPackedFileUI
    {
        private IContainer components;
		private StackPanel pnSims;
		SimPe.PackedFiles.Wrapper.SimPoolControl spc = null;
		private StackPanel pnDebug;
		private NgbhSlotSelection nssel;
		private NgbhSlotUI nsui;
		private ItemsControl toolBar1;
		private StackPanel pnBadge;
		private ToggleButton biSim;
		private ToggleButton biBadge;
		private ToggleButton biDebug;
		private NgbhSkillHelper shelper;
		private Menu menuBar1;
		private ContextMenu menu;
		private MenuItem miNuke;
		private MenuItem miFix;
		NgbhSlotUI simslot = null;

		public ExtNgbhUI()
		{
			InitializeComponent();

			biSim.Tag = pnSims;
			biDebug.Tag = pnDebug;
			biBadge.Tag = pnBadge;

            biDebug.IsVisible = true;

			this.SelectButton(biSim);

            biBadge.IsEnabled = (SimPe.PathProvider.Global.EPInstalled >= 3 || SimPe.PathProvider.Global.STInstalled >= 28);

			SimPe.RemoteControl.HookToMessageQueue(0x4E474248, new SimPe.RemoteControl.ControlEvent(ControlEvent));
		}

		protected void ControlEvent(object sender, SimPe.RemoteControl.ControlEventArgs e)
		{			
			object[] os = e.Items as object[];
			if (os!=null) 
			{
				Data.NeighborhoodSlots st = (Data.NeighborhoodSlots)os[1];				
				uint inst = (uint)os[0];

				if (st== Data.NeighborhoodSlots.SimsIntern && biBadge.IsEnabled) this.ChoosePage(biBadge, null);
				else this.ChoosePage(biSim, null);

				PackedFiles.Wrapper.ExtSDesc sdesc = FileTable.ProviderRegistry.SimDescriptionProvider.FindSim((ushort)inst) as PackedFiles.Wrapper.ExtSDesc;
				bool found = SelectSimByInstance(sdesc);
				
				if (!found && sdesc!=null) 
				{
					spc.SelectHousehold(sdesc.HouseholdName);
					SelectSimByInstance(sdesc);
				}

				spc.Refresh(false);
			}			
		}

		protected bool SelectSimByInstance(PackedFiles.Wrapper.SDesc sdesc)
		{
			bool ret = false;
			if (sdesc!=null) 
			{
				spc.SelectedElement = sdesc;
				if (spc.SelectedElement!=null) return true;
			}

			return ret;
		}


		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.pnSims    = new StackPanel { IsVisible = false };
            this.pnDebug   = new StackPanel { IsVisible = false };
            this.pnBadge   = new StackPanel { IsVisible = false };
            this.menuBar1  = new Menu      { IsVisible = false };
            this.spc       = new SimPe.PackedFiles.Wrapper.SimPoolControl();
            this.simslot   = new SimPe.Plugin.NgbhSlotUI();
            this.nsui      = new SimPe.Plugin.NgbhSlotUI();
            this.nssel     = new SimPe.Plugin.NgbhSlotSelection();
            this.shelper   = new SimPe.Plugin.NgbhSkillHelper();
            this.menu      = new ContextMenu();
            this.miNuke    = new MenuItem { Header = "Nuke Memories" };
            this.miFix     = new MenuItem { Header = "Fix Memories" };
            this.toolBar1  = new ItemsControl();
            this.biSim     = new ToggleButton { Content = "Memories" };
            this.biBadge   = new ToggleButton { Content = "Badges" };
            this.biDebug   = new ToggleButton { Content = "Debug" };

            // spc
            this.spc.Name = "spc";
            this.spc.Package = null;
            this.spc.Padding = new Avalonia.Thickness(1);
            this.spc.RightClickSelect = false;
            this.spc.SelectedElement = null;
            this.spc.SelectedSim = null;
            this.spc.SimDetails = false;
            this.spc.TileColumns = new int[] { 1 };
            this.spc.SelectedSimChanged += new SimPe.PackedFiles.Wrapper.SimPoolControl.SelectedSimHandler(this.spc_SelectedSimChanged);

            // context menu
            this.menu.Items.Add(this.miNuke);
            this.menu.Items.Add(this.miFix);
            this.menu.Opening += (s, e) => this.menu_VisibleChanged(s, EventArgs.Empty);
            this.miNuke.Click += (s, e) => this.miNuke_Activate(s, EventArgs.Empty);
            this.miFix.Click  += (s, e) => this.miFix_Activate(s, EventArgs.Empty);

            // simslot
            this.simslot.Name = "simslot";
            this.simslot.NgbhResource = null;
            this.simslot.SimPoolControl = this.spc;
            this.simslot.Slot = null;
            this.simslot.SlotType = SimPe.Data.NeighborhoodSlots.Sims;

            // nsui / nssel
            this.nsui.Name = "nsui";
            this.nsui.NgbhResource = null;
            this.nsui.SimPoolControl = null;
            this.nsui.tabPage3.IsEnabled = false;
            this.nsui.Slot = null;
            this.nsui.SlotType = SimPe.Data.NeighborhoodSlots.Sims;
            this.nssel.Name = "nssel";
            this.nssel.NgbhResource = null;
            this.nssel.SelectedSlotChanged += (s, e) => this.nssel_SelectedSlotChanged(s, EventArgs.Empty);

            // shelper
            this.shelper.Name = "shelper";
            this.shelper.NgbhResource = null;
            this.shelper.SimPoolControl = this.spc;
            this.shelper.Slot = null;
            this.shelper.ChangedItem    += (s, e) => this.shelper_ChangedItem(s, EventArgs.Empty);
            this.shelper.AddedNewItem   += (s, e) => this.shelper_AddedNewItem(s, EventArgs.Empty);

            // toolbar buttons
            this.biSim.Click   += (s, e) => this.ChoosePage(s, EventArgs.Empty);
            this.biBadge.Click += (s, e) => this.ChoosePage(s, EventArgs.Empty);
            this.biDebug.Click += (s, e) => this.ChoosePage(s, EventArgs.Empty);
            this.toolBar1.Items.Add(this.biSim);
            this.toolBar1.Items.Add(this.biBadge);
            this.toolBar1.Items.Add(this.biDebug);

            // pnBadge.VisibleChanged — no direct Avalonia equivalent without Reactive; skip subscription

            this.HeaderText = "Sim Memory Editor";
            this.Name = "ExtNgbhUI";
            this.Controls.Add(this.pnSims);
            this.Controls.Add(this.pnBadge);
            this.Controls.Add(this.pnDebug);
            this.Controls.Add(this.toolBar1);
		}
        #endregion

		public ExtNgbh Ngbh
		{
			get { return (ExtNgbh)Wrapper; }
		}

		public override void RefreshGUI()
		{
			simslot.NgbhResource = Ngbh;
			spc_SelectedSimChanged(spc, null, null);
			spc.Package = Ngbh.Package;	
			this.nssel.NgbhResource = Ngbh;
			this.shelper.NgbhResource = Ngbh;
		}

		public override void OnCommit()
		{
			Ngbh.SynchronizeUserData(true, false);
		}

		public void SelectButton(ToggleButton b)
		{
			for (int i=0; i<this.toolBar1.Items.Count; i++)
			{
				if (toolBar1.Items[i] is ToggleButton)
				{
					ToggleButton item = (ToggleButton)toolBar1.Items[i];
					item.IsChecked = (item==b);

					if (item.Tag!=null)
					{
						StackPanel pn = (StackPanel)item.Tag;
						pn.IsVisible = item.IsChecked == true;
					}
				}
			}

			UpdateEnabledState();
		}

		void UpdateEnabledState()
		{
		}
		
		private void ChoosePage(object sender, System.EventArgs e)
		{
			SelectButton((ToggleButton)sender);

			if (pnSims.IsVisible) pnSims.Children.Add(this.spc);
			else if (pnBadge.IsVisible) pnBadge.Children.Add(this.spc);
		}

        private void spc_SelectedSimChanged(object sender, object thumb, SimPe.PackedFiles.Wrapper.SDesc sdesc)
        {
            if (spc.SelectedSim != null)
            {
                Collections.NgbhSlots slots = this.Ngbh.GetSlots(Data.NeighborhoodSlots.Sims);
                if (slots != null)
                {
                    NgbhSlot slot = slots.GetInstanceSlot(spc.SelectedSim.Instance);
                    if (slot == null)
                    {
                        slots.AddNew(spc.SelectedSim.Instance);
                    }
                }
            }
		}

		private void nssel_SelectedSlotChanged(object sender, System.EventArgs e)
		{
			nsui.Slot = nssel.SelectedSlot;
		}

		bool updateitems;
		private void shelper_AddedNewItem(object sender, System.EventArgs e)
		{
			updateitems = true;
		}
		private void shelper_ChangedItem(object sender, System.EventArgs e)
		{
			updateitems = true;
		}

		protected void RefreshContent()
		{
			nsui.Refresh();
			simslot.Refresh();
		}

		private void pnBadge_VisibleChanged(object sender, System.EventArgs e)
		{
			if (pnBadge.IsVisible == true) updateitems=false;
			else if (updateitems)				
				RefreshContent();			
			
		}

		#region Extensions by Theo
        void menu_VisibleChanged(object sender, EventArgs e)
        {
            miFix.IsEnabled = (this.Ngbh != null) && Helper.XmlRegistry.HiddenMode;
            miNuke.IsEnabled = (spc.SelectedSim != null);
        }

		private void miNuke_Activate(object sender, System.EventArgs e)
		{
			if (spc.SelectedSim != null) 
			{
				Collections.NgbhSlots slots = this.Ngbh.GetSlots(Data.NeighborhoodSlots.Sims);
				if (slots!=null) 
				{
					NgbhSlot slot = slots.GetInstanceSlot(spc.SelectedSim.Instance);
					if (slot!=null)
					{
						slot.RemoveMyMemories();
						int deletedCount = slot.RemoveMemoriesAboutMe();

						if (deletedCount > 0)
                            SimPe.Message.Show(String.Format("Deleted {0} memories from the sim pool", deletedCount), "Advice", MessageBoxButtons.OK);
					
						spc.Refresh();
					}
				}
			}
		}		

		private void miFix_Activate(object sender, System.EventArgs e)
		{
			EnhancedNgbh ngbh = this.Ngbh as EnhancedNgbh;
			if (ngbh!=null) 
			{
				ngbh.FixNeighborhoodMemories();
				this.RefreshGUI();
			}
		}
		#endregion				
	}
}
