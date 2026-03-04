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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SimPe.Plugin.Tool.Dockable.Finder
{
    public partial class FindTGI : SimPe.Interfaces.AFinderTool
    {
        public FindTGI(SimPe.Interfaces.IFinderResultGui rgui)
            :base(rgui)
        {
            InitializeComponent();
        }

        public FindTGI() : this(null) { }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            tbInstHi.Text = "0x" + Helper.HexString(SimPe.Hashes.SubTypeHash(SimPe.Hashes.StripHashFromName(tbName.Text)));
            tbInstLo.Text = "0x" + Helper.HexString(SimPe.Hashes.InstanceHash(SimPe.Hashes.StripHashFromName(tbName.Text)));
        }

        struct Descriptor {
            public bool use;
            public uint val;
        };

        Descriptor t, g, hi, li;
        protected override bool OnPrepareStart()
        {
            t.val = Helper.StringToUInt32(tbType.Text, 0, 16);
            t.use = tbType.Text.Trim() != "";

            g.val = Helper.StringToUInt32(tbGroup.Text, 0, 16);
            g.use = tbGroup.Text.Trim() != "";

            hi.val = Helper.StringToUInt32(tbInstHi.Text, 0, 16);
            hi.use = tbInstHi.Text.Trim() != "";

            li.val = Helper.StringToUInt32(tbInstLo.Text, 0, 16);
            li.use = tbInstLo.Text.Trim() != "";

            return t.use || g.use || hi.use || li.use;
        }

        public override void SearchPackage(SimPe.Interfaces.Files.IPackageFile pkg, SimPe.Interfaces.Files.IPackedFileDescriptor pfd)
        {
            if ((t.val == pfd.Type || !t.use) &&
                (g.val == pfd.Group || !g.use) &&
                (hi.val == pfd.SubType || !hi.use) &&
                (li.val == pfd.Instance || !li.use))
            {
                ResultGui.AddResult(pkg, pfd);
            }
        }
    }
}
