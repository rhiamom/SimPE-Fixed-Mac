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

using Ambertation.Windows.Forms;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for NgbhSkillHelper.
	/// </summary>
	[System.ComponentModel.DefaultEvent("AddedNewItem")]
	public class NgbhSkillHelper : Avalonia.Controls.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        ThemeManager tm;
		public NgbhSkillHelper()
		{
			// Required designer variable.
			InitializeComponent();
		
			try 
			{
				tm = ThemeManager.Global.CreateChild();
				tm.AddControl(this.xpBadges);
				tm.AddControl(this.xpSkills);

                this.xpBadges.IsVisible = (SimPe.PathProvider.Global.EPInstalled >= 3 || SimPe.PathProvider.Global.STInstalled >= 28);
				SetContent();
			} 
			catch {}
		}

		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
	private void InitializeComponent()
	{
		// Avalonia port: instantiate controls and wire events only
		this.badges = new SimPe.Plugin.NgbhSkillHelperElement();
		this.xpBadges = new XPTaskBoxSimple();
		this.xpSkills = new XPTaskBoxSimple();
		this.skills = new SimPe.Plugin.NgbhSkillHelperElement();

		// badges setup
		this.badges.Name = "badges";
		this.badges.NgbhResource = null;
		this.badges.ShowBadges = true;
		this.badges.ShowSkills = false;
		this.badges.ShowToddlerSkills = false;
		this.badges.Slot = null;
		this.badges.AddedNewItem += new System.EventHandler(this.skills_AddedNewItem);
		this.badges.ChangedItem += new System.EventHandler(this.skills_ChangedItem);

		// xpBadges setup (XPTaskBoxSimple - Avalonia Control)
		this.xpBadges.BodyColor = System.Drawing.SystemColors.ControlLight;
		this.xpBadges.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
		this.xpBadges.Controls.Add(this.badges);
		this.xpBadges.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
		this.xpBadges.HeaderText = "Badges";
		this.xpBadges.HeaderTextColor = System.Drawing.SystemColors.ControlText;
		this.xpBadges.IconLocation = new System.Drawing.Point(4, 0);
		this.xpBadges.IconSize = new System.Drawing.Size(48, 48);
		this.xpBadges.LeftHeaderColor = System.Drawing.SystemColors.ControlDark;
		this.xpBadges.Name = "xpBadges";
		this.xpBadges.RightHeaderColor = System.Drawing.SystemColors.ControlDark;

		// xpSkills setup (XPTaskBoxSimple - Avalonia Control)
		this.xpSkills.BodyColor = System.Drawing.SystemColors.ControlLight;
		this.xpSkills.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
		this.xpSkills.Controls.Add(this.skills);
		this.xpSkills.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold);
		this.xpSkills.HeaderText = "Skills";
		this.xpSkills.HeaderTextColor = System.Drawing.SystemColors.ControlText;
		this.xpSkills.IconLocation = new System.Drawing.Point(4, 0);
		this.xpSkills.IconSize = new System.Drawing.Size(48, 48);
		this.xpSkills.LeftHeaderColor = System.Drawing.SystemColors.ControlDark;
		this.xpSkills.Name = "xpSkills";
		this.xpSkills.RightHeaderColor = System.Drawing.SystemColors.ControlDark;

		// skills setup
		this.skills.Name = "skills";
		this.skills.NgbhResource = null;
		this.skills.ShowBadges = false;
		this.skills.ShowSkills = true;
		this.skills.ShowToddlerSkills = true;
		this.skills.Slot = null;
		this.skills.AddedNewItem += new System.EventHandler(this.skills_AddedNewItem);
		this.skills.ChangedItem += new System.EventHandler(this.skills_ChangedItem);

		// this (NgbhSkillHelper) setup — Avalonia layout
		var panel = new Avalonia.Controls.StackPanel();
		panel.Children.Add(this.xpBadges);
		panel.Children.Add(this.xpSkills);
		this.Content = panel;
		this.Name = "NgbhSkillHelper";
	}
		#endregion

		NgbhSlot slot;
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
		private SimPe.Plugin.NgbhSkillHelperElement badges;
		private SimPe.Plugin.NgbhSkillHelperElement skills;
        private XPTaskBoxSimple xpBadges;
        private XPTaskBoxSimple xpSkills;
				
		[System.ComponentModel.Browsable(false)]
		public Ngbh NgbhResource
		{
			get {return ngbh;}
			set 
			{
				ngbh = value;
				pc_SelectedSimChanged(pc, null, null);
				SetContent();				
			}
		}

		SimPe.PackedFiles.Wrapper.SimPoolControl pc;
		public SimPe.PackedFiles.Wrapper.SimPoolControl SimPoolControl
		{
			get {return pc;}
			set 
			{
				if (pc!=null) pc.SelectedSimChanged -= new SimPe.PackedFiles.Wrapper.SimPoolControl.SelectedSimHandler(pc_SelectedSimChanged);
				pc = value;
				
				if (pc!=null) 
				{
					pc.SelectedSimChanged += new SimPe.PackedFiles.Wrapper.SimPoolControl.SelectedSimHandler(pc_SelectedSimChanged);
					pc_SelectedSimChanged(pc, null, null);
				}
			}
		}

		void SetContent()
		{
			badges.Slot = slot;
			skills.Slot = slot;

			if (pc!=null) 
			{
				if (pc.SelectedSim!=null) SetImage(pc.SelectedSim.Image);
				else SetImage(new Bitmap(1,1));
			}
		}

		void SetImage(Image img)
		{
			img = Ambertation.Drawing.GraphicRoutines.KnockoutImage(img, new Point(0), Color.Transparent, true);
			img = Ambertation.Drawing.GraphicRoutines.ScaleImage(img, 48, 48, true);

			this.xpBadges.Icon = img;
			this.xpSkills.Icon = img;			
		}

		private void pc_SelectedSimChanged(object sender, Image thumb, SimPe.PackedFiles.Wrapper.SDesc sdesc)
		{
			
			if (ngbh!=null && pc!=null) 
			{
				
				if (pc.SelectedSim!=null) {
					this.Slot = ngbh.GetSlots(Data.NeighborhoodSlots.SimsIntern).GetInstanceSlot(pc.SelectedSim.FileDescriptor.Instance);	
					SetImage(pc.SelectedSim.Image);
				}
				else 
				{
					this.Slot = null;
					
				}
			}
		}
		

		private void skills_AddedNewItem(object sender, System.EventArgs e)
		{
			if (AddedNewItem!=null) AddedNewItem(this, e);
		}	

		private void skills_ChangedItem(object sender, System.EventArgs e)
		{
			if (ChangedItem!=null) ChangedItem(this, e);
		}
	
		public event EventHandler AddedNewItem;
		public event EventHandler ChangedItem;
	}
}
