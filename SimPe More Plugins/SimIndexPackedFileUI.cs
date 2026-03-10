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
using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
    public partial class SimindexPackedFileUI : SimPe.Windows.Forms.WrapperBaseControl, IPackedFileUI
    {
        protected new SimindexPackedFileWrapper Wrapper
        {
            get { return base.Wrapper as SimindexPackedFileWrapper; }
        }
        public SimindexPackedFileWrapper TPFW
        {
            get { return (SimindexPackedFileWrapper)Wrapper; }
        }

        ushort scinstance;

        #region WrapperBaseControl Member

        public SimindexPackedFileUI()
        {
            InitializeComponent();
        }

        public override void RefreshGUI()
        {
            base.RefreshGUI();
            warnlbl.Visible = false;
            

            scinstance = Wrapper.Sciname;
            scinst.Text = "0x" + Helper.HexString(scinstance);
            if (!Wrapper.IsOK) this.desclbl.Text = "The Sim Creation Index\r\n Is only used in a Primary Neighourhood\r\nnot here!";
        }

        public override void OnCommit()
        {
            base.OnCommit();
            TPFW.SynchronizeUserData(true, false);
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

        private void scinst_TextChanged(object sender, EventArgs e)
        {
            try
            {
                scinstance = Convert.ToUInt16(scinst.Text, 16);
                if (scinstance < 1)
                {
                    scinstance = 1;
                    scinst.Text = "0x0001";
                    warnlbl.Visible = true;
                }
                Wrapper.Sciname = scinstance;
                scinst.ForeColor = System.Drawing.SystemColors.WindowText;
                this.CanCommit = true;
            }
            catch
            { this.CanCommit = false; scinst.ForeColor = System.Drawing.Color.DarkRed; }
        }
    }
}
