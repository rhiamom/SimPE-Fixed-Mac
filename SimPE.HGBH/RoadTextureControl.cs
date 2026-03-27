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
using SimPe.Interfaces.Plugin;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for RoadTextureControl.
	/// </summary>
	public class RoadTextureControl : 
		//System.Windows.Forms.UserControl
		SimPe.Windows.Forms.WrapperBaseControl
	{
		
		private Avalonia.Controls.TextBlock label1;
		private Avalonia.Controls.TextBlock label2;
		private Avalonia.Controls.TextBlock label3;
		private Avalonia.Controls.TextBlock label4;
		private Avalonia.Controls.ListBox lb;
		private Avalonia.Controls.TextBlock label5;
        private XPTaskBoxSimple xpTaskBoxSimple1;
		private Avalonia.Controls.TextBlock label6;
		private Avalonia.Controls.TextBox tbFlname;
		private Avalonia.Controls.TextBox tbUk1;
		private Avalonia.Controls.TextBox tbUk2;
		private Avalonia.Controls.TextBox tbUk3;
		private Avalonia.Controls.TextBox tbId;
		private Avalonia.Controls.TextBox tbTxmt;
		private Avalonia.Controls.TextBlock label7;
		private Ambertation.Windows.Forms.EnumComboBox cbType;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RoadTextureControl() : base()
		{
			// Required designer variable.
			InitializeComponent();

            ThemeManager.Global.AddControl(this.xpTaskBoxSimple1);
			cbType.Enum = typeof(RoadTexture.RoadTextureType);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1    = new Avalonia.Controls.TextBlock { Text = "Resource ID:" };
            this.label2    = new Avalonia.Controls.TextBlock { Text = "Unknown2:" };
            this.label3    = new Avalonia.Controls.TextBlock { Text = "Unknown3:" };
            this.label4    = new Avalonia.Controls.TextBlock { Text = "Resourcename:" };
            this.label5    = new Avalonia.Controls.TextBlock { Text = "ID:" };
            this.label6    = new Avalonia.Controls.TextBlock { Text = "Name:" };
            this.label7    = new Avalonia.Controls.TextBlock { Text = "Type:" };
            this.lb        = new Avalonia.Controls.ListBox();
            this.tbFlname  = new Avalonia.Controls.TextBox { Text = "textBox1" };
            this.tbUk1     = new Avalonia.Controls.TextBox { Text = "0x00000000", IsReadOnly = true };
            this.tbUk2     = new Avalonia.Controls.TextBox { Text = "0x00000000" };
            this.tbUk3     = new Avalonia.Controls.TextBox { Text = "0x00000000" };
            this.tbId      = new Avalonia.Controls.TextBox();
            this.tbTxmt    = new Avalonia.Controls.TextBox();
            this.xpTaskBoxSimple1 = new XPTaskBoxSimple();
            this.cbType    = new Ambertation.Windows.Forms.EnumComboBox();

            this.lb.SelectionChanged += (s, e) => lb_SelectedIndexChanged(s, System.EventArgs.Empty);

            this.xpTaskBoxSimple1.HeaderText = "Properties";
            this.xpTaskBoxSimple1.IconLocation = new System.Drawing.Point(4, 12);
            this.xpTaskBoxSimple1.IconSize = new System.Drawing.Size(32, 32);
            this.xpTaskBoxSimple1.Padding = new Avalonia.Thickness(4, 44, 4, 4);

            this.cbType.Enum = null;
            this.cbType.ResourceManager = null;

            this.CanCommit = false;
            this.HeaderText = "Road Texture";
            this.Name = "RoadTextureControl";
		}
		#endregion

		#region IPackedFileUI Member

		public RoadTexture RoadTextureWrapper
		{
			get {return (RoadTexture)this.Wrapper; }
		}

		public override void RefreshGUI()
		{
			base.RefreshGUI ();

			this.tbId.Text = "";
			this.tbTxmt.Text = "";

			this.tbFlname.Text = RoadTextureWrapper.FileName;
			this.tbUk1.Text = "0x"+Helper.HexString(RoadTextureWrapper.Id);
			this.tbUk2.Text = "0x"+Helper.HexString(RoadTextureWrapper.Unknown2);
			this.tbUk3.Text = "0x"+Helper.HexString(RoadTextureWrapper.Unknown3);

			cbType.SelectedValue = RoadTextureWrapper.Type;

			this.lb.Items.Clear();
			foreach (object o in RoadTextureWrapper)
				lb.Items.Add(o);

			if (lb.Items.Count>0) lb.SelectedIndex = 0;
		}
			

		#endregion

		private void lb_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lb.SelectedIndex<0) return;
			if (RoadTextureWrapper==null) return;

			if (lb.SelectedItem is uint) 
			{
				this.tbId.Text = "0x"+Helper.HexString((uint)lb.SelectedItem);
				this.tbTxmt.Text = "0x"+Helper.HexString((uint)RoadTextureWrapper[lb.SelectedItem]);
			} 
			else 
			{
				this.tbId.Text = lb.SelectedItem.ToString();
				this.tbTxmt.Text = RoadTextureWrapper[lb.SelectedItem].ToString();
			}
		}
	}
}
