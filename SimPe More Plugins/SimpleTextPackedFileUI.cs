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
    public class SimpleTextPackedFileUI : SimPe.Windows.Forms.WrapperBaseControl, IPackedFileUI
    {
        private Avalonia.Controls.TextBlock label2 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox TBsting = new Avalonia.Controls.TextBox();

        private void InitializeComponent() { }

        protected new SimpleTextPackedFileWrapper Wrapper
        {
            get { return base.Wrapper as SimpleTextPackedFileWrapper; }
        }
        public SimpleTextPackedFileWrapper TPFW
        {
            get { return (SimpleTextPackedFileWrapper)Wrapper; }
        }

        #region WrapperBaseControl Member

        public SimpleTextPackedFileUI()
        {
            InitializeComponent();
        }

        public override void RefreshGUI()
        {
            base.RefreshGUI();

            this.TBsting.Text = Wrapper.Strung;
        }

        public override void OnCommit()
        {
            base.OnCommit();
            TPFW.SynchronizeUserData(true, false);
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

        private void TBsting_TextChanged(object sender, EventArgs e)
        {
            Wrapper.Strung = TBsting.Text;
        }
    }
}
