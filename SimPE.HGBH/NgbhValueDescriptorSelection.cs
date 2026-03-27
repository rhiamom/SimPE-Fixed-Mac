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
	/// Summary description for NgbhValueDescriptorSelection.
	/// </summary>
	[System.ComponentModel.DefaultEvent("SelectedDescriptorChanged")]
	public class NgbhValueDescriptorSelection : Avalonia.Controls.UserControl
	{
		private Avalonia.Controls.ComboBox cb;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NgbhValueDescriptorSelection()
		{
			// Required designer variable.
			InitializeComponent();

			skill = true;
			tskill = true;
			badge = true;

			SetContent();
		}

		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cb = new Avalonia.Controls.ComboBox();
			this.cb.Name = "cb";
			this.cb.SelectionChanged += (s, e) => cb_SelectedIndexChanged(s, System.EventArgs.Empty);
			this.Content = this.cb;
			this.Name = "NgbhValueDescriptorSelection";
		}
		#endregion

		bool badge, skill, tskill;
		public bool ShowBadges
		{
			get { return badge;}
			set {
				if (badge!=value) 
				{
					badge = value; 
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
					SetContent();
				}
			}
		}

		void SetContent()
		{
			cb.Items.Clear();
			try 
			{
				if (!Avalonia.Controls.Design.IsDesignMode)
				{
					foreach (NgbhValueDescriptor nvd in ExtNgbh.ValueDescriptors)
					{
						if (nvd.Type == NgbhValueDescriptorType.Badge && badge) this.cb.Items.Add(nvd);
						else if (nvd.Type == NgbhValueDescriptorType.Skill && skill) this.cb.Items.Add(nvd);
						else if (nvd.Type == NgbhValueDescriptorType.ToddlerSkill && tskill) this.cb.Items.Add(nvd);
					}
				}

				if (cb.Items.Count>0) 
					cb.SelectedIndex = 0;
			} 
			catch {}
		}

		public NgbhValueDescriptor SelectedDescriptor
		{
			get 
			{
				return cb.SelectedItem as NgbhValueDescriptor;
			}
		}

		public event EventHandler SelectedDescriptorChanged;
		private void cb_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (SelectedDescriptorChanged!=null) SelectedDescriptorChanged(this, e);
		}

		
	}
}
