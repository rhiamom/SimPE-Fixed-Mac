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
using System.Media;
using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
	/// <summary>
	/// This class is used to fill the UI for this FileType with Data
	/// </summary>
    public class HugBugPackedFileUI : SimPe.Windows.Forms.WrapperBaseControl, IPackedFileUI
    {
        private Avalonia.Controls.TextBlock label1 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock lbFail = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock lbpass = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox TBsting = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.Button btShow = new Avalonia.Controls.Button();
        private Avalonia.Controls.Button btcustom = new Avalonia.Controls.Button();

        private void InitializeComponent() { }

        protected new HugBugPackedFileWrapper Wrapper
        {
            get { return base.Wrapper as HugBugPackedFileWrapper; }
        }
        public HugBugPackedFileWrapper TPFW
        {
            get { return (HugBugPackedFileWrapper)Wrapper; }
        }

        #region WrapperBaseControl Member

        public HugBugPackedFileUI()
		{
			InitializeComponent();
		}

        public override void RefreshGUI()
        {
            base.RefreshGUI();
            this.TBsting.Text = "There is " + Convert.ToString(Wrapper.isz) + " Items in this List,\n Press 'Show All Items' to display them all"; // clear previous values
            if (Wrapper.HasCustom) this.TBsting.Text += "\n Press 'Show Only CC' to display Items not in the pjse GUIDIndex";
            if (Wrapper.IsSims) this.TBsting.Text = "This Lot has sim(s) on it.\n\n" + this.TBsting.Text;
            this.btcustom.IsVisible = this.btcustom.IsEnabled = Wrapper.HasCustom;
            this.btShow.IsEnabled = true;
            
                if (Wrapper.IsCorrupt)
                {
                    this.label1.Text = "Super Duper Hug Found !!";
                    this.lbFail.IsVisible = true;
                    this.lbpass.IsVisible = false;
                }
                else
                {
                    this.label1.Text = "This Lot is Clean";
                    this.lbFail.IsVisible = false;
                    this.lbpass.IsVisible = true;
                }
        }
        

        public override void OnCommit()
        {
            // base.OnCommit();
            // TPFW.SynchronizeUserData(true, false);
        }
        #endregion

        #region IPackedFileUI Member
        Avalonia.Controls.Control IPackedFileUI.GUIHandle
        {
            get { return this; }
        }
        #endregion

        #region IDisposable Member

        void IDisposable.Dispose()
        {
            this.TPFW.Dispose();
        }
        #endregion

        private void btShow_Click(object sender, EventArgs e)
        {
            this.btShow.IsEnabled = false;
            this.btcustom.IsEnabled = Wrapper.HasCustom;
            this.TBsting.Text = "";
            for (int i = 0; i < Wrapper.isz; i++)
                this.TBsting.Text += Wrapper.objekts[i];
        }
        private void btcustom_Click(object sender, EventArgs e)
        {
            this.btcustom.IsEnabled = false;
            this.btShow.IsEnabled = true;
            this.TBsting.Text = "";
            for (int i = 0; i < Wrapper.isz; i++)
                if (Wrapper.objekts[i].Contains("**"))
                    this.TBsting.Text += Wrapper.objekts[i];
            if (this.TBsting.Text == "") this.TBsting.Text = " This Lot is CC Free"; // Should never be seen
        }
    }
}
