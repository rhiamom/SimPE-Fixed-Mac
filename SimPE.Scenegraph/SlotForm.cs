/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSweatShop                                 *
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using SimPe.PackedFiles.Wrapper;
using SimPe.Windows.Forms;
using SimPe.Scenegraph.Compat;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for SlotForm.
	/// </summary>
	public class SlotForm : Avalonia.Controls.Window
	{
        internal Avalonia.Controls.Panel pnslot;
        private SimPe.Windows.Forms.WrapperBaseControl panel4;
		private Avalonia.Controls.TextBlock label1;
		private Avalonia.Controls.TextBlock label2;
		private Avalonia.Controls.TextBlock label3;
		private Avalonia.Controls.TextBlock label4;
		private Avalonia.Controls.TextBlock label5;
		private Avalonia.Controls.TextBlock label6;
		private Avalonia.Controls.TextBlock label7;
		private Avalonia.Controls.TextBlock label8;
		private Avalonia.Controls.TextBox tbf1;
		private Avalonia.Controls.TextBox tbf2;
		private Avalonia.Controls.TextBox tbf3;
		private Avalonia.Controls.TextBox tbi1;
		private Avalonia.Controls.TextBox tbi2;
		private Avalonia.Controls.TextBox tbi3;
		private Avalonia.Controls.TextBox tbi4;
		private Avalonia.Controls.TextBox tbi5;
		private Avalonia.Controls.TextBlock label9;
		internal Avalonia.Controls.ComboBox cbtype;
        internal Avalonia.Controls.TabControl tabControl1;
        internal Avalonia.Controls.TabItem tabPage1;
        internal Avalonia.Controls.TabItem tabPage2;
        internal Avalonia.Controls.TabItem tabPage3;
        internal Avalonia.Controls.TabItem tabPage4;
        internal Avalonia.Controls.TabItem tabPage5;
        internal Avalonia.Controls.TabItem tabPageA;
        internal Avalonia.Controls.TabItem tabPage6;
        internal Avalonia.Controls.TabItem tabPage7;
		private Avalonia.Controls.TextBlock label10;
		private Avalonia.Controls.TextBox tbf6;
		private Avalonia.Controls.TextBlock label11;
		private Avalonia.Controls.TextBox tbf5;
		private Avalonia.Controls.TextBlock label13;
		private Avalonia.Controls.TextBox tbf4;
		private Avalonia.Controls.TextBlock label14;
		private Avalonia.Controls.TextBox tbi6;
		private Avalonia.Controls.TextBlock label15;
		private Avalonia.Controls.TextBox tbs2;
		private Avalonia.Controls.TextBlock label16;
		private Avalonia.Controls.TextBox tbs1;
		private Avalonia.Controls.TextBlock label17;
		private Avalonia.Controls.TextBox tbf7;
		private Avalonia.Controls.TextBlock label18;
		private Avalonia.Controls.TextBox tbf8;
		private Avalonia.Controls.TextBlock label19;
		private Avalonia.Controls.TextBox tbi7;
		private Avalonia.Controls.TextBlock label20;
        private Avalonia.Controls.TextBlock label0A;
		private Avalonia.Controls.TextBox tbi8;
        private Avalonia.Controls.TextBox tbs3;
		private Avalonia.Controls.TextBlock label21;
		private Avalonia.Controls.TextBox tbi10;
		private Avalonia.Controls.TextBlock label22;
		private Avalonia.Controls.TextBox tbi9;
		private Avalonia.Controls.Border groupBox1;
		private Avalonia.Controls.TextBlock label23;
		internal Avalonia.Controls.TextBox tbver;
		private Avalonia.Controls.TextBlock label24;
		internal Avalonia.Controls.TextBox tbname;
        internal ListView lv;
        private Avalonia.Controls.TextBlock visualStyleLinkLabel1;
        private Avalonia.Controls.TextBlock visualStyleLinkLabel2;
        private Avalonia.Controls.TextBlock visualStyleLinkLabel3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public SlotForm()
		{
			InitializeComponent();

            if (SimPe.Helper.XmlRegistry.UseBigIcons)
                if (lv?.Font != null) lv.Font = new System.Drawing.Font("Verdana", 10F);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected new void Dispose( bool disposing )
		{
			// base.Dispose(disposing); // Avalonia Window does not have Dispose(bool)
		}

		#region Avalonia AXAML layout placeholder
		private void InitializeComponent()
		{
			// TODO: Avalonia AXAML layout
			pnslot = new Avalonia.Controls.Panel();
			panel4 = new SimPe.Windows.Forms.WrapperBaseControl();
			label1 = new Avalonia.Controls.TextBlock();
			label2 = new Avalonia.Controls.TextBlock();
			label3 = new Avalonia.Controls.TextBlock();
			label4 = new Avalonia.Controls.TextBlock();
			label5 = new Avalonia.Controls.TextBlock();
			label6 = new Avalonia.Controls.TextBlock();
			label7 = new Avalonia.Controls.TextBlock();
			label8 = new Avalonia.Controls.TextBlock();
			tbf1 = new Avalonia.Controls.TextBox();
			tbf2 = new Avalonia.Controls.TextBox();
			tbf3 = new Avalonia.Controls.TextBox();
			tbi1 = new Avalonia.Controls.TextBox();
			tbi2 = new Avalonia.Controls.TextBox();
			tbi3 = new Avalonia.Controls.TextBox();
			tbi4 = new Avalonia.Controls.TextBox();
			tbi5 = new Avalonia.Controls.TextBox();
			label9 = new Avalonia.Controls.TextBlock();
			cbtype = new Avalonia.Controls.ComboBox();
			tabControl1 = new Avalonia.Controls.TabControl();
			tabPage1 = new Avalonia.Controls.TabItem();
			tabPage2 = new Avalonia.Controls.TabItem();
			tabPage3 = new Avalonia.Controls.TabItem();
			tabPage4 = new Avalonia.Controls.TabItem();
			tabPage5 = new Avalonia.Controls.TabItem();
			tabPageA = new Avalonia.Controls.TabItem();
			tabPage6 = new Avalonia.Controls.TabItem();
			tabPage7 = new Avalonia.Controls.TabItem();
			label10 = new Avalonia.Controls.TextBlock();
			tbf6 = new Avalonia.Controls.TextBox();
			label11 = new Avalonia.Controls.TextBlock();
			tbf5 = new Avalonia.Controls.TextBox();
			label13 = new Avalonia.Controls.TextBlock();
			tbf4 = new Avalonia.Controls.TextBox();
			label14 = new Avalonia.Controls.TextBlock();
			tbi6 = new Avalonia.Controls.TextBox();
			label15 = new Avalonia.Controls.TextBlock();
			tbs2 = new Avalonia.Controls.TextBox();
			label16 = new Avalonia.Controls.TextBlock();
			tbs1 = new Avalonia.Controls.TextBox();
			label17 = new Avalonia.Controls.TextBlock();
			tbf7 = new Avalonia.Controls.TextBox();
			label18 = new Avalonia.Controls.TextBlock();
			tbf8 = new Avalonia.Controls.TextBox();
			label19 = new Avalonia.Controls.TextBlock();
			tbi7 = new Avalonia.Controls.TextBox();
			label20 = new Avalonia.Controls.TextBlock();
			label0A = new Avalonia.Controls.TextBlock();
			tbi8 = new Avalonia.Controls.TextBox();
			tbs3 = new Avalonia.Controls.TextBox();
			label21 = new Avalonia.Controls.TextBlock();
			tbi10 = new Avalonia.Controls.TextBox();
			label22 = new Avalonia.Controls.TextBlock();
			tbi9 = new Avalonia.Controls.TextBox();
			groupBox1 = new Avalonia.Controls.Border();
			label23 = new Avalonia.Controls.TextBlock();
			tbver = new Avalonia.Controls.TextBox();
			label24 = new Avalonia.Controls.TextBlock();
			tbname = new Avalonia.Controls.TextBox();
			lv = new ListView();
			visualStyleLinkLabel1 = new Avalonia.Controls.TextBlock();
			visualStyleLinkLabel2 = new Avalonia.Controls.TextBlock();
			visualStyleLinkLabel3 = new Avalonia.Controls.TextBlock();
		}
		#endregion

		internal Slot wrapper;

		internal void ShowItem(SlotItem si)
		{
			ListViewItem lvi = new ListViewItem();
			ShowItem(si, lvi);
			lv.Items.Add(lvi);
		}

		void ShowItem(SlotItem si, ListViewItem lvi)
		{
			lvi.Tag = si;
			lvi.SubItems.Clear();

			lvi.Text = si.Type.ToString();

			lvi.SubItems.Add(si.UnknownFloat1.ToString());
			lvi.SubItems.Add(si.UnknownFloat2.ToString());
			lvi.SubItems.Add(si.UnknownFloat3.ToString());

			lvi.SubItems.Add(si.UnknownInt1.ToString());
			lvi.SubItems.Add(si.UnknownInt2.ToString());
			lvi.SubItems.Add(si.UnknownInt3.ToString());
			lvi.SubItems.Add(si.UnknownInt4.ToString());
			lvi.SubItems.Add(si.UnknownInt5.ToString());

			if (wrapper.Version>=5)
			{
				lvi.SubItems.Add(si.UnknownFloat4.ToString());
				lvi.SubItems.Add(si.UnknownFloat5.ToString());
				lvi.SubItems.Add(si.UnknownFloat6.ToString());

				lvi.SubItems.Add(si.UnknownInt6.ToString());
			}

			if (wrapper.Version>=6)
			{
				lvi.SubItems.Add(si.UnknownShort1.ToString());
				lvi.SubItems.Add(si.UnknownShort2.ToString());
			}

			if (wrapper.Version>=7) lvi.SubItems.Add(si.UnknownFloat7.ToString());
			if (wrapper.Version>=8) lvi.SubItems.Add(si.UnknownInt7.ToString());
            if (wrapper.Version >= 9) lvi.SubItems.Add(si.UnknownInt8.ToString());
            if (wrapper.Version == 10) lvi.SubItems.Add(si.UnknownShort3.ToString());
			if (wrapper.Version>=0x10) lvi.SubItems.Add(si.UnknownFloat8.ToString());

			if (wrapper.Version>=0x40)
			{
				lvi.SubItems.Add(si.UnknownInt9.ToString());
				lvi.SubItems.Add(si.UnknownInt10.ToString());
			}
		}

		private void Select(object sender, System.EventArgs e)
		{
			if (lv.SelectedItems.Count==0) return;
			this.Tag = true;
			try
			{
				SimPe.PackedFiles.Wrapper.SlotItem si = (SimPe.PackedFiles.Wrapper.SlotItem)lv.SelectedItems[0].Tag;

				int ct = 0;
				foreach (SimPe.PackedFiles.Wrapper.SlotItemType sti in cbtype.Items)
				{
					if (sti.Equals(si.Type)) cbtype.SelectedIndex = ct;
					ct++;
				}

				tbf1.Text = si.UnknownFloat1.ToString();
				tbf2.Text = si.UnknownFloat2.ToString();
				tbf3.Text = si.UnknownFloat3.ToString();
				tbf4.Text = si.UnknownFloat4.ToString();
				tbf5.Text = si.UnknownFloat5.ToString();
				tbf6.Text = si.UnknownFloat6.ToString();
				tbf7.Text = si.UnknownFloat7.ToString();
				tbf8.Text = si.UnknownFloat8.ToString();

				tbi1.Text = si.UnknownInt1.ToString();
				tbi2.Text = si.UnknownInt2.ToString();
				tbi3.Text = si.UnknownInt3.ToString();
				tbi4.Text = si.UnknownInt4.ToString();
				tbi5.Text = si.UnknownInt5.ToString();
				tbi6.Text = si.UnknownInt6.ToString();
				tbi7.Text = si.UnknownInt7.ToString();
				tbi8.Text = si.UnknownInt8.ToString();
				tbi9.Text = si.UnknownInt9.ToString();
				tbi10.Text = si.UnknownInt10.ToString();

				tbs1.Text = si.UnknownShort1.ToString();
				tbs2.Text = si.UnknownShort2.ToString();
                tbs3.Text = si.UnknownShort3.ToString();
			}
			finally
			{
				this.Tag = null;
			}
		}

		private void Changed(object sender, System.EventArgs e)
		{
			if (Tag!=null) return;
			if (lv.SelectedItems.Count==0) return;
			try
			{
				SimPe.PackedFiles.Wrapper.SlotItem si = (SimPe.PackedFiles.Wrapper.SlotItem)lv.SelectedItems[0].Tag;

				if (cbtype.SelectedIndex>=0) si.Type = (SlotItemType)cbtype.Items[cbtype.SelectedIndex];

				si.UnknownFloat1 = Convert.ToSingle(tbf1.Text);
				si.UnknownFloat2 = Convert.ToSingle(tbf2.Text);
				si.UnknownFloat3 = Convert.ToSingle(tbf3.Text);
				si.UnknownFloat4 = Convert.ToSingle(tbf4.Text);
				si.UnknownFloat5 = Convert.ToSingle(tbf5.Text);
				si.UnknownFloat6 = Convert.ToSingle(tbf6.Text);
				si.UnknownFloat7 = Convert.ToSingle(tbf7.Text);
				si.UnknownFloat8 = Convert.ToSingle(tbf8.Text);

				si.UnknownInt1 = Convert.ToInt32(tbi1.Text);
				si.UnknownInt2 = Convert.ToInt32(tbi2.Text);
				si.UnknownInt3 = Convert.ToInt32(tbi3.Text);
				si.UnknownInt4 = Convert.ToInt32(tbi4.Text);
				si.UnknownInt5 = Convert.ToInt32(tbi5.Text);
				si.UnknownInt6 = Convert.ToInt32(tbi6.Text);
				si.UnknownInt7 = Convert.ToInt32(tbi7.Text);
				si.UnknownInt8 = Convert.ToInt32(tbi8.Text);
				si.UnknownInt9 = Convert.ToInt32(tbi9.Text);
				si.UnknownInt10 = Convert.ToInt32(tbi10.Text);

				si.UnknownShort1 = Convert.ToInt16(tbs1.Text);
				si.UnknownShort2 = Convert.ToInt16(tbs2.Text);
                si.UnknownShort3 = Convert.ToInt16(tbs3.Text);

				wrapper.Changed = true;

				ShowItem(si, lv.SelectedItems[0]);
			}
			catch {}
		}

		private void ChangeWrp(object sender, System.EventArgs e)
		{
			if (Tag!=null) return;
			wrapper.FileName = tbname.Text;
			wrapper.Changed = true;
        }

        private void btcommit_Click(object sender, EventArgs e)
        {
            wrapper.SynchronizeUserData();
        }

		private void Add(object sender, EventArgs e)
		{
			SlotItem si = new SlotItem(wrapper);
			wrapper.Items.Add(si);
			ShowItem(si);
			wrapper.Changed = true;
		}

		private void Delete(object sender, EventArgs e)
		{
			if (lv.SelectedItems.Count==0) return;
			try
			{
				SimPe.PackedFiles.Wrapper.SlotItem si = (SimPe.PackedFiles.Wrapper.SlotItem)lv.SelectedItems[0].Tag;

				wrapper.Items.Remove(si);
				lv.Items.Remove(lv.SelectedItems[0]);
				wrapper.Changed = true;
			}
			catch {}
		}

        private void Clone(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count == 0) return;
            SlotItem si = new SlotItem(wrapper);
            SimPe.PackedFiles.Wrapper.SlotItem sv = (SimPe.PackedFiles.Wrapper.SlotItem)lv.SelectedItems[0].Tag;
            si.Type = sv.Type;
            si.UnknownFloat1 = sv.UnknownFloat1;
            si.UnknownFloat2 = sv.UnknownFloat2;
            si.UnknownFloat3 = sv.UnknownFloat3;
            si.UnknownFloat4 = sv.UnknownFloat4;
            si.UnknownFloat5 = sv.UnknownFloat5;
            si.UnknownFloat6 = sv.UnknownFloat6;
            si.UnknownFloat7 = sv.UnknownFloat7;
            si.UnknownFloat8 = sv.UnknownFloat8;
            si.UnknownInt1 = sv.UnknownInt1;
            si.UnknownInt2 = sv.UnknownInt2;
            si.UnknownInt3 = sv.UnknownInt3;
            si.UnknownInt4 = sv.UnknownInt4;
            si.UnknownInt5 = sv.UnknownInt5;
            si.UnknownInt6 = sv.UnknownInt6;
            si.UnknownInt7 = sv.UnknownInt7;
            si.UnknownInt8 = sv.UnknownInt8;
            si.UnknownInt9 = sv.UnknownInt9;
            si.UnknownInt10 = sv.UnknownInt10;
            si.UnknownShort1 = sv.UnknownShort1;
            si.UnknownShort2 = sv.UnknownShort2;
            si.UnknownShort3 = sv.UnknownShort3;
            wrapper.Items.Add(si);
            ShowItem(si);
            wrapper.Changed = true;
        }
	}
}
