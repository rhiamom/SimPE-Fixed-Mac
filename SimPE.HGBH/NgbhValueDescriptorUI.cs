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
using Ambertation.Windows.Forms;
using Avalonia.Controls;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for NgbhValueDescriptorUI.
	/// </summary>
	[System.ComponentModel.DefaultEvent("AddedNewItem")]
	public class NgbhValueDescriptorUI : Avalonia.Controls.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NgbhValueDescriptorUI()
		{
			// Required designer variable.
			InitializeComponent();
			pb.TokenCount = 10;
			SetContent();
		}

		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
	private void InitializeComponent()
	{
		this.pb     = new LabeledProgressBar();
		this.cb     = new CheckBox();
		this.lb     = new TextBlock();
		this.ll     = new Button { Content = "Add" };
		this.panel1 = new StackPanel();
		this.panel2 = new StackPanel();
		this.panel3 = new StackPanel();

		// pb
		this.pb.DisplayOffset = 0;
		this.pb.LabelText = "";
		this.pb.Maximum = 100;
		this.pb.NumberFormat = "N0";
		this.pb.NumberOffset = 0;
		this.pb.NumberScale = 1;
		this.pb.SelectedColor = System.Drawing.Color.YellowGreen;
		this.pb.UnselectedColor = System.Drawing.Color.Black;
		this.pb.Value = 0;
		this.pb.Changed += new System.EventHandler(this.pb_Changed);

		// cb
		this.cb.IsCheckedChanged += (s, e) => cb_CheckedChanged(s, EventArgs.Empty);

		// ll
		this.ll.Click += (s, e) => ll_Click(s, System.EventArgs.Empty);

		// panels
		this.panel1.Children.Add(this.pb);
		this.panel2.Children.Add(this.cb);
		this.panel3.Children.Add(this.lb);
		this.panel3.Children.Add(this.ll);

		// layout
		var root = new StackPanel();
		root.Children.Add(this.panel1);
		root.Children.Add(this.panel2);
		root.Children.Add(this.panel3);
		this.Content = root;
		this.Name = "NgbhValueDescriptorUI";
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

		NgbhValueDescriptor des;
		private StackPanel panel1;
        private LabeledProgressBar pb;
		private StackPanel panel2;
        private CheckBox cb;
		private StackPanel panel3;
		private Button ll;
		private TextBlock lb;
		
				
		[System.ComponentModel.Browsable(false)]
		public NgbhValueDescriptor NgbhValueDescriptor
		{
			get {return des;}
			set 
			{
				des = value;
				SetContent();				
			}
		}

		NgbhValueDescriptorSelection vds;
		public NgbhValueDescriptorSelection NgbhValueDescriptorSelection
		{
			get {return vds;}
			set 
			{
				if (vds!=null) vds.SelectedDescriptorChanged -= new EventHandler(vds_SelectedDescriptorChanged);
				vds = value;
				if (vds!=null) vds.SelectedDescriptorChanged += new EventHandler(vds_SelectedDescriptorChanged);
			}
		}

		void SetVisible()
		{
			panel1.IsVisible = item!=null;
			if (des!=null)
				panel2.IsVisible = des.HasComplededFlag && item!=null;
			else
				panel2.IsVisible = false;

			panel3.IsVisible = des!=null && item==null;
		}

		NgbhItem item;
		bool inter;
		void SetContent()
		{
			if (inter) return;
			inter = true;
			if (des!=null && slot!=null)
			{
				item = slot.FindItem(des.Guid);
				pb.NumberOffset = des.Minimum;
				pb.Maximum = des.Maximum;				
				
				if (item!=null) 			
				{	
					pb.Value = item.GetValue(des.DataNumber);
					if (des.HasComplededFlag)
						cb.IsChecked = item.GetValue(des.CompletedDataNumber)!=0;
				}
				else
					lb.Text = des.ToString();

				this.IsEnabled = true;
			}
			else
			{
				this.IsEnabled = false;
			}

			SetVisible();
			inter = false;
		}

		private void vds_SelectedDescriptorChanged(object sender, EventArgs e)
		{
			this.NgbhValueDescriptor = vds.SelectedDescriptor;
		}

		private void pb_Resize(object sender, System.EventArgs e)
		{
			
		}

		// OnResize removed — pb.TokenCount set once in constructor

		public event EventHandler AddedNewItem;
		public event EventHandler ChangedItem;

		private void ll_Click(object sender, System.EventArgs e)
		{
			if (item!=null) return;
			if (slot==null) return;
			if (des==null) return;
			
			if (des.Intern) item = slot.ItemsA.AddNew(SimMemoryType.Skill);
			else item = slot.ItemsB.AddNew(SimMemoryType.Skill);

			item.Guid = des.Guid;
			item.PutValue(des.DataNumber, 0);
			if (des.HasComplededFlag) item.PutValue(des.CompletedDataNumber, 0);
								
			SetContent();

			if (AddedNewItem!=null) AddedNewItem(this, new EventArgs());
		}

		private void cb_CheckedChanged(object sender, System.EventArgs e)
		{
			if (inter) return;
			if (item==null) return;
			if (des==null) return;
			if (!des.HasComplededFlag) return;

			if (cb.IsChecked == true) item.PutValue(des.CompletedDataNumber, 1);
			else item.PutValue(des.CompletedDataNumber, 0);

			if (ChangedItem!=null) ChangedItem(this, new EventArgs());
		}

		private void pb_Changed(object sender, System.EventArgs e)
		{
			if (inter) return;
			if (item==null) return;
			if (des==null) return;			

			item.PutValue(des.DataNumber, (ushort)pb.Value);
			
			if (ChangedItem!=null) ChangedItem(this, new EventArgs());
		}

		private void pb_Load(object sender, System.EventArgs e)
		{		
		}
	}
}
