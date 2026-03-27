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
	/// Summary description for BnfoCustomerItemsUI.
	/// </summary>
	[System.ComponentModel.DefaultEvent("SelectedItemChanged")]
	public class BnfoCustomerItemsUI : Avalonia.Controls.UserControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BnfoCustomerItemsUI()
		{
			InitializeComponent();

			SetContent();
		}

		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lb = new Avalonia.Controls.ListBox();
			this.lb.Name = "lb";
			this.lb.SelectionChanged += (s, e) => lb_SelectedIndexChanged(s, System.EventArgs.Empty);
			this.Content = this.lb;
			this.Name = "BnfoCustomerItemsUI";
		}
		#endregion

		private Avalonia.Controls.ListBox lb;

		Collections.BnfoCustomerItems items;
		[System.ComponentModel.Browsable(false)]
		public Collections.BnfoCustomerItems Items
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
			lb.Items.Clear();
			if (items!=null)
			{				
				foreach (Plugin.BnfoCustomerItem item in items)
					lb.Items.Add(item);				
			}
			lb_SelectedIndexChanged(lb, new EventArgs());
		}

		public BnfoCustomerItem SelectedItem
		{
			get 
			{
				return lb.SelectedItem as BnfoCustomerItem;
			}
		}

		public new void Refresh()
		{
			SetContent();
			InvalidateVisual();
		}

		public event System.EventHandler SelectedItemChanged;
		private void lb_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (SelectedItemChanged!=null) SelectedItemChanged(this, new EventArgs());
		}
	}
}
