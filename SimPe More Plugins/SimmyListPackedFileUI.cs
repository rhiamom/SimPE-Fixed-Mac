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
using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
    public class SimmyListPackedFileUI : SimPe.Windows.Forms.WrapperBaseControl, IPackedFileUI
    {
        private Avalonia.Controls.TextBlock label2 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox TBsting = new Avalonia.Controls.TextBox();
        private System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
        private System.ComponentModel.IContainer components = new System.ComponentModel.Container();
        private Avalonia.Controls.TextBlock lbInfo = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.CheckBox checkBox1 = new Avalonia.Controls.CheckBox();

        private void InitializeComponent() { }

        protected new SimmyListPackedFileWrapper Wrapper
        {
            get { return base.Wrapper as SimmyListPackedFileWrapper; }
        }
        public SimmyListPackedFileWrapper TPFW
        {
            get { return (SimmyListPackedFileWrapper)Wrapper; }
        }

        #region WrapperBaseControl Member

        public SimmyListPackedFileUI()
        {
            InitializeComponent();
        }

        public override void RefreshGUI()
        {
            base.RefreshGUI();

            this.checkBox1.IsChecked = false;
            this.TBsting.Text = Wrapper.Strung;
        }

        public override void OnCommit()
        {
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.IsChecked.GetValueOrDefault() == true) this.TBsting.Text = Wrapper.Twine;
            else this.TBsting.Text = Wrapper.Strung;
//             this.TBsting.Refresh();  // no Refresh in Avalonia
        }
    }
}
