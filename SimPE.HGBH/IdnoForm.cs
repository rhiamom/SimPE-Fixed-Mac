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
using Avalonia.Controls;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for IdnoForm.
	/// </summary>
	public class IdnoForm
	{
		public object Tag { get; set; }

		public IdnoForm()
		{
            InitializeComponent();
            ThemeManager.Global.AddControl(this.pnidno);
		}

		#region Component initialization
		/// <summary>
		/// Required method for initialization — do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pnidno = new Avalonia.Controls.StackPanel();
            this.panel2 = new Avalonia.Controls.StackPanel();

            this.label1 = new Avalonia.Controls.TextBlock { Text = "Neighbourhood Type:" };
            this.label2 = new Avalonia.Controls.TextBlock { Text = "Version:" };
            this.label3 = new Avalonia.Controls.TextBlock { Text = "UID:" };
            this.label4 = new Avalonia.Controls.TextBlock { Text = "Subname:" };
            this.label5 = new Avalonia.Controls.TextBlock { Text = "(parent) Name:" };
            this.label6 = new Avalonia.Controls.TextBlock { Text = "Required EP:" };
            this.label7 = new Avalonia.Controls.TextBlock { Text = "Affiliated EP:" };
            this.label8 = new Avalonia.Controls.TextBlock { Text = "Flags:" };
            this.label9 = new Avalonia.Controls.TextBlock { Text = "Season Quadrants :" };
            this.lbVer = new Avalonia.Controls.TextBlock();

            this.cbtype = new Avalonia.Controls.ComboBox();
            this.cbtype.SelectionChanged += (s, e) => SelectType(s, EventArgs.Empty);

            this.tbtype = new Avalonia.Controls.TextBox { Text = "0x00000000", IsReadOnly = true };

            this.tbversion = new Avalonia.Controls.TextBox { Text = "0x00000000", IsReadOnly = true };

            this.tbid = new Avalonia.Controls.TextBox { Text = "0" };
            this.tbid.TextChanged += (s, e) => Change(s, EventArgs.Empty);

            this.tbname = new Avalonia.Controls.TextBox { Text = "N000" };
            this.tbname.TextChanged += (s, e) => Change(s, EventArgs.Empty);

            this.tbsubname = new Avalonia.Controls.TextBox { Text = "U000" };
            this.tbsubname.TextChanged += (s, e) => Change(s, EventArgs.Empty);

            this.llunique = new Avalonia.Controls.Button { Content = "make unique" };
            this.llunique.Click += (s, e) => MakeUnique(s, EventArgs.Empty);

            this.cbreqtp = new Avalonia.Controls.ComboBox();
            this.cbreqtp.SelectionChanged += (s, e) => SelectRtp(s, EventArgs.Empty);

            this.tbreqep = new Avalonia.Controls.TextBox { Text = "0x00000000", IsReadOnly = true };

            this.cbsubtp = new Avalonia.Controls.ComboBox();
            this.cbsubtp.SelectionChanged += (s, e) => SelectAtp(s, EventArgs.Empty);

            this.tbsubep = new Avalonia.Controls.TextBox { Text = "0x00000000", IsReadOnly = true };

            this.tbidflags = new Avalonia.Controls.TextBox();
            this.tbidflags.TextChanged += (s, e) => Change(s, EventArgs.Empty);

            this.cbquada = new Avalonia.Controls.ComboBox();
            this.cbquada.SelectionChanged += (s, e) => ChangSeasoa(s, EventArgs.Empty);

            this.cbquadb = new Avalonia.Controls.ComboBox();
            this.cbquadb.SelectionChanged += (s, e) => ChangSeasob(s, EventArgs.Empty);

            this.cbquadc = new Avalonia.Controls.ComboBox();
            this.cbquadc.SelectionChanged += (s, e) => ChangSeasoc(s, EventArgs.Empty);

            this.cbquadd = new Avalonia.Controls.ComboBox();
            this.cbquadd.SelectionChanged += (s, e) => ChangSeasod(s, EventArgs.Empty);

            this.pnidno.Children.Add(this.panel2);
            this.pnidno.Children.Add(this.label2);
            this.pnidno.Children.Add(this.tbversion);
            this.pnidno.Children.Add(this.lbVer);
            this.pnidno.Children.Add(this.label3);
            this.pnidno.Children.Add(this.tbid);
            this.pnidno.Children.Add(this.llunique);
            this.pnidno.Children.Add(this.label8);
            this.pnidno.Children.Add(this.tbidflags);
            this.pnidno.Children.Add(this.label5);
            this.pnidno.Children.Add(this.tbname);
            this.pnidno.Children.Add(this.label4);
            this.pnidno.Children.Add(this.tbsubname);
            this.pnidno.Children.Add(this.label1);
            this.pnidno.Children.Add(this.cbtype);
            this.pnidno.Children.Add(this.tbtype);
            this.pnidno.Children.Add(this.label6);
            this.pnidno.Children.Add(this.cbreqtp);
            this.pnidno.Children.Add(this.tbreqep);
            this.pnidno.Children.Add(this.label7);
            this.pnidno.Children.Add(this.cbsubtp);
            this.pnidno.Children.Add(this.tbsubep);
            this.pnidno.Children.Add(this.label9);
            this.pnidno.Children.Add(this.cbquada);
            this.pnidno.Children.Add(this.cbquadb);
            this.pnidno.Children.Add(this.cbquadc);
            this.pnidno.Children.Add(this.cbquadd);
		}
		#endregion

        internal Avalonia.Controls.StackPanel pnidno;
        internal Avalonia.Controls.TextBox tbtype;
        internal Avalonia.Controls.ComboBox cbtype;
        private Avalonia.Controls.TextBlock label1;
        internal Avalonia.Controls.StackPanel panel2;
        private Avalonia.Controls.TextBlock label2;
        private Avalonia.Controls.TextBlock label3;
        internal Avalonia.Controls.TextBlock label4;
        internal Avalonia.Controls.TextBox tbid;
        internal Avalonia.Controls.TextBox tbname;
        internal Avalonia.Controls.TextBox tbsubname;
        internal Avalonia.Controls.TextBox tbversion;
        private Avalonia.Controls.TextBlock label5;
        internal Avalonia.Controls.Button llunique;
        internal Avalonia.Controls.TextBlock lbVer;
        internal Avalonia.Controls.TextBox tbsubep;
        internal Avalonia.Controls.ComboBox cbsubtp;
        internal Avalonia.Controls.TextBlock label7;
        internal Avalonia.Controls.TextBox tbreqep;
        internal Avalonia.Controls.ComboBox cbreqtp;
        internal Avalonia.Controls.TextBlock label6;
        internal Avalonia.Controls.TextBlock label8;
        internal Avalonia.Controls.TextBox tbidflags;
        internal Avalonia.Controls.TextBlock label9;
        internal Avalonia.Controls.ComboBox cbquadd;
        internal Avalonia.Controls.ComboBox cbquadc;
        internal Avalonia.Controls.ComboBox cbquadb;
        internal Avalonia.Controls.ComboBox cbquada;

		internal Idno wrapper;

		private void SelectType(object sender, System.EventArgs e)
		{
			if (cbtype.SelectedIndex<0) return;

			NeighborhoodType nt = (NeighborhoodType)cbtype.SelectedItem;
			if (nt!=NeighborhoodType.Unknown) this.tbtype.Text = "0x"+Helper.HexString((uint)nt);

			tbsubname.IsEnabled = (nt==NeighborhoodType.University);

			if (this.Tag!=null) return;
			wrapper.Type = nt;
			wrapper.Changed = true;
        }

        private void SelectRtp(object sender, System.EventArgs e)
        {
            if (cbreqtp.SelectedIndex < 0) return;

            Data.MetaData.NeighbourhoodEP nr = (Data.LocalizedNeighbourhoodEP)cbreqtp.SelectedItem;
            this.tbreqep.Text = "0x" + Helper.HexString((uint)nr);

            if (this.Tag != null) return;
            wrapper.Reqep = nr;
            wrapper.Changed = true;
            // SelectRep(sender, e);
        }

        private void SelectAtp(object sender, System.EventArgs e)
        {
            if (cbsubtp.SelectedIndex < 0) return;

            Data.MetaData.NeighbourhoodEP ns = (Data.LocalizedNeighbourhoodEP)cbsubtp.SelectedItem;
            this.tbsubep.Text = "0x" + Helper.HexString((uint)ns);

            if (this.Tag != null) return;
            wrapper.Subep = ns;
            wrapper.Changed = true;
        }

        private void ChangSeasoa(object sender, System.EventArgs e)
        {
            if (cbquada.SelectedIndex < 0) return;

            NhSeasons sa = (NhSeasons)cbquada.SelectedItem;
            if (this.Tag != null) return;
            wrapper.Quada = sa;
            wrapper.Changed = true;
        }
        private void ChangSeasob(object sender, System.EventArgs e)
        {
            if (cbquadb.SelectedIndex < 0) return;

            NhSeasons sb = (NhSeasons)cbquadb.SelectedItem;
            if (this.Tag != null) return;
            wrapper.Quadb = sb;
            wrapper.Changed = true;
        }
        private void ChangSeasoc(object sender, System.EventArgs e)
        {
            if (cbquadc.SelectedIndex < 0) return;

            NhSeasons sc = (NhSeasons)cbquadc.SelectedItem;
            if (this.Tag != null) return;
            wrapper.Quadc = sc;
            wrapper.Changed = true;
        }
        private void ChangSeasod(object sender, System.EventArgs e)
        {
            if (cbquadd.SelectedIndex < 0) return;

            NhSeasons sd = (NhSeasons)cbquadd.SelectedItem;
            if (this.Tag != null) return;
            wrapper.Quadd = sd;
            wrapper.Changed = true;
        }

		private void MakeUnique(object sender, System.EventArgs e)
		{
			try
			{
				wrapper.MakeUnique();
				this.tbid.Text = wrapper.Uid.ToString();
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		private void Change(object sender, System.EventArgs e)
		{
			if (this.Tag!=null) return;
			try
			{
				wrapper.OwnerName = tbname.Text;
				wrapper.SubName = tbsubname.Text;
				wrapper.Changed = true;
                wrapper.Uid = Convert.ToUInt32(tbid.Text);
                wrapper.Idflags = Helper.StringToUInt32(tbidflags.Text, wrapper.Idflags, 16);
			}
			catch  (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		private void IdnoForm_Click(object sender, System.EventArgs e)
		{
			wrapper.SynchronizeUserData();
        }
	}
}
