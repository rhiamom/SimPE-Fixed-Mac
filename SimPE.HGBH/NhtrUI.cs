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
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces;
using SimPe.PackedFiles.Wrapper.Supporting;
using SimPe.Data;
// using Ambertation.Windows.Forms;
using SimPe.Windows.Forms;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for NhtrUI.
	/// </summary>
	public class NhtrUI : 
		//System.Windows.Forms.UserControl
		SimPe.Windows.Forms.WrapperBaseControl, SimPe.Interfaces.Plugin.IPackedFileUI
	{
		
		
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		

		public NhtrUI()
		{			
			// Required designer variable.
			InitializeComponent();	
		
			

			this.CanCommit = Helper.XmlRegistry.HiddenMode;
			//ThemeManager.AddControl(this.toolBar1);
            
                ThemeManager tm = ThemeManager.Global.CreateChild();
                tm.AddControl(this.tb);
                tm.AddControl(this.lb);
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

		private void InitializeComponent()
		{
            this.lb = new Avalonia.Controls.ListBox();
            this.tb = new Avalonia.Controls.TextBox { IsReadOnly = true };
            this.cb = new Avalonia.Controls.ComboBox();

            this.lb.SelectionChanged += (s, e) => lb_SelectedIndexChanged(s, System.EventArgs.Empty);
            this.cb.SelectionChanged += (s, e) => comboBox1_SelectedIndexChanged(s, System.EventArgs.Empty);

            // Layout: 3-column grid — combo+list | splitter | textbox
            var grid = new Avalonia.Controls.Grid();
            grid.ColumnDefinitions = new Avalonia.Controls.ColumnDefinitions("*,4,*");

            var leftPanel = new Avalonia.Controls.DockPanel();
            Avalonia.Controls.DockPanel.SetDock(this.cb, Avalonia.Controls.Dock.Top);
            leftPanel.Children.Add(this.cb);
            leftPanel.Children.Add(this.lb);
            Avalonia.Controls.Grid.SetColumn(leftPanel, 0);
            grid.Children.Add(leftPanel);

            var splitter = new Avalonia.Controls.GridSplitter();
            Avalonia.Controls.Grid.SetColumn(splitter, 1);
            grid.Children.Add(splitter);

            Avalonia.Controls.Grid.SetColumn(this.tb, 2);
            grid.Children.Add(this.tb);

            Content = grid;
		}

		private Avalonia.Controls.ListBox lb;
		private Avalonia.Controls.TextBox tb;
		private Avalonia.Controls.ComboBox cb;

		public Nhtr Nhtr
		{
			get { return (Nhtr)Wrapper; }
		}

		bool intern;
		
		public override void RefreshGUI()
		{			
			if (intern) return;
			
			intern = true;
			lb.Items.Clear();
			cb.Items.Clear();
			if (Nhtr!=null) 
			{				
				foreach (NhtrList list in Nhtr.Items)				
					SimPe.CountedListItem.Add(cb, list);	
				
				if (cb.Items.Count>0) cb.SelectedIndex = 0;

				lb.IsEnabled = true;
				this.IsEnabled = true;
			} 
			else 
			{
				
			}

			intern=false;
		}

		public override void OnCommit()
		{
			Nhtr.SynchronizeUserData(true, false);
		}

		private void lb_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (lb.SelectedItem==null)
			{
				tb.Text = "";
			}
			else
			{
				tb.Text = ((lb.SelectedItem as CountedListItem ).Object as NhtrItem).ToLongString();
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			lb.Items.Clear();
			if (cb.SelectedItem==null) return;
			
			NhtrList list = (cb.SelectedItem as CountedListItem).Object as NhtrList;
			foreach (NhtrItem i in list)				
				SimPe.CountedListItem.Add(lb, i);
		}		
								
	}
}
