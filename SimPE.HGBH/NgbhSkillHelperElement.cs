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
	/// Summary description for NgbhSkillHelperElement.
	/// </summary>
	[System.ComponentModel.DefaultEvent("AddedNewItem")]
	public class NgbhSkillHelperElement : Avalonia.Controls.UserControl
	{
		private SimPe.Plugin.NgbhValueDescriptorSelection cb;
		private SimPe.Plugin.NgbhValueDescriptorUI ui;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NgbhSkillHelperElement()
		{
			// Required designer variable.
			InitializeComponent();

			ShowToddlerSkills = true;
			ShowSkills = true;
			ShowBadges = true;

		}

		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cb = new SimPe.Plugin.NgbhValueDescriptorSelection();
			this.ui = new SimPe.Plugin.NgbhValueDescriptorUI();
			// cb
			this.cb.Name = "cb";
			this.cb.ShowBadges = true;
			this.cb.ShowSkills = true;
			this.cb.ShowToddlerSkills = true;
			// ui
			this.ui.IsEnabled = false;
			this.ui.Name = "ui";
			this.ui.NgbhValueDescriptor = null;
			this.ui.NgbhValueDescriptorSelection = this.cb;
			this.ui.Slot = null;
			this.ui.AddedNewItem += new System.EventHandler(this.ui_AddedNewItem);
			this.ui.ChangedItem += new System.EventHandler(this.ui_ChangedItem);
			// layout
			var panel = new Avalonia.Controls.DockPanel();
			Avalonia.Controls.DockPanel.SetDock(this.cb, Avalonia.Controls.Dock.Top);
			panel.Children.Add(this.cb);
			panel.Children.Add(this.ui);
			this.Content = panel;
			this.Name = "NgbhSkillHelperElement";
		}
		#endregion

		bool badge, skill, tskill;
		public bool ShowBadges
		{
			get { return badge;}
			set 
			{
				if (badge!=value) 
				{
					badge = value; 
					cb.ShowBadges = value;
					SetContent();
				}
			}
		}
		public bool ShowSkills
		{
			get { return skill;}
			set 
			{
				if (skill!=value) 
				{
					skill = value; 
					cb.ShowSkills = value;
					SetContent();
				}
			}
		}
		public bool ShowToddlerSkills
		{
			get { return tskill;}
			set 
			{
				if (tskill!=value) 
				{
					tskill = value; 
					cb.ShowToddlerSkills = value;
					SetContent();
				}
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
			}
		}

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

		void SetContent()
		{
			this.ui.Slot = slot;
		}

				

		private void ui_AddedNewItem(object sender, System.EventArgs e)
		{
			if (AddedNewItem!=null) AddedNewItem(this, e);
		}	

		private void ui_ChangedItem(object sender, System.EventArgs e)
		{
			if (ChangedItem!=null) ChangedItem(this, e);
		}	

		public event EventHandler AddedNewItem;
		public event EventHandler ChangedItem;
	}
}
