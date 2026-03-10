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
    public partial class SimmyListPackedFileUI : SimPe.Windows.Forms.WrapperBaseControl, IPackedFileUI
    {
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

            this.checkBox1.Checked = false;
            this.TBsting.Text = Wrapper.Strung;
        }

        public override void OnCommit()
        {
        }
        #endregion

        #region IPackedFileUI Member
        System.Windows.Forms.Control IPackedFileUI.GUIHandle
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
            if (checkBox1.Checked == true) this.TBsting.Text = Wrapper.Twine;
            else this.TBsting.Text = Wrapper.Strung;
            this.TBsting.Refresh();
        }
    }
}
