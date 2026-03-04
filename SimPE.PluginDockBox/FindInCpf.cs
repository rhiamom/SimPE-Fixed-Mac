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
    public partial class FindInCpf : FindInStr
    {
        public FindInCpf(SimPe.Interfaces.IFinderResultGui rgui)
            :base(rgui)
        {
            InitializeComponent();
            type = 0;
            field = "";
        }

        public FindInCpf() : this(null) { }

        uint type;
        string field;
        protected override bool OnPrepareStart()
        {
            bool res = base.OnPrepareStart();

            if (res)
            {
                type = Helper.StringToUInt32(tbType.Text, 0, 16);
                field = tbName.Text.ToLower().Trim();
            }
            return res;
        }

        public override void SearchPackage(SimPe.Interfaces.Files.IPackageFile pkg, SimPe.Interfaces.Files.IPackedFileDescriptor pfd)
        {
            if (type != 0)
            {
                if (pfd.Type != type) return;
            }
            else
            {
                if (pfd.Type != Data.MetaData.GZPS && pfd.Type != Data.MetaData.MMAT) return;
            }

            SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
            cpf.ProcessData(pfd, pkg);


            bool found = false;
            if (field!="") found = FindInField(cpf, found, field);
            else {
                foreach (SimPe.PackedFiles.Wrapper.CpfItem item in cpf.Items)
                {
                    found = FindInField(cpf, found, item.Name);
                    if (found) break;
                }
            }

            //we have a match, so add the result item
            if (found)
            {
                ResultGui.AddResult(pkg, pfd);
            }
        }

        private bool FindInField(SimPe.PackedFiles.Wrapper.Cpf cpf, bool found, string fldname)
        {
            string n = cpf.GetSaveItem(fldname).StringValue.ToLower();
            if (compareType == CompareType.Equal)
            {
                found = n == name;
            }
            else if (compareType == CompareType.Start)
            {
                found = n.StartsWith(name);
            }
            else if (compareType == CompareType.End)
            {
                found = n.EndsWith(name);
            }
            else if (compareType == CompareType.Contain)
            {
                found = n.IndexOf(name) > -1;
            }
            else if (compareType == CompareType.RegExp && reg != null)
            {
                found = reg.IsMatch(n);
            }
            return found;
        }
    }
}
