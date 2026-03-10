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
	public class GametipPackedFileWrapper        
		: AbstractWrapper, IFileWrapper, IFileWrapperSaveExtension
    {
        #region Gametip Attribute
        private ushort tipname;
        private ushort tipheader;
        private ushort tipbody;
        private ushort tipep;
        private uint tipicon;

        public ushort Tipname
        {
            get { return tipname; }
            set { tipname = value; }
        }
        public ushort Tipheader
        {
            get { return tipheader; }
            set { tipheader = value; }
        }
        public ushort Tipbody
        {
            get { return tipbody; }
            set { tipbody = value; }
        }
        public ushort Tipep
        {
            get { return tipep; }
            set { tipep = value; }
        }
        public uint Tipicon
        {
            get { return tipicon; }
            set { tipicon = value; }
        }
		#endregion

		public GametipPackedFileWrapper() : base()
		{ }

        #region IWrapper member
        public override bool CheckVersion(uint version)
        {
            return true;
        }
        #endregion
		
		#region AbstractWrapper Member
		protected override IPackedFileUI CreateDefaultUIHandler()
		{
            return new GametipPackedFileUI();
		}

		protected override IWrapperInfo CreateWrapperInfo()
        {
                return new AbstractWrapperInfo(
                    "Game Tip Wrapper",
                    "Chris",
                    "To aid unravelling Game Tip files",
                    2,
                    null
                    );
		}

		protected override void Unserialize(System.IO.BinaryReader reader)
        {
            reader.BaseStream.Seek(0x2, System.IO.SeekOrigin.Begin);
            tipname = reader.ReadUInt16();
            tipheader = reader.ReadUInt16();
            tipbody = reader.ReadUInt16();
            tipep = reader.ReadUInt16();
            tipicon = reader.ReadUInt32();
		}

		protected override void Serialize(System.IO.BinaryWriter writer)
        {
            ushort vershin = 2;
            writer.Write(vershin);
            writer.Write(tipname);
            writer.Write(tipheader);
            writer.Write(tipbody);
            writer.Write(tipep);
            writer.Write(tipicon);
		}
		#endregion

		#region IFileWrapperSaveExtension Member		
			//all covered by Serialize()
		#endregion

		#region IFileWrapper Member

		public byte[] FileSignature
		{
			get
			{
                return new byte[0];
			}
		}

		public uint[] AssignableTypes
        {
            get
            {
                uint[] types = { 0xB1827A47 }; //handles the Game Tip File
                return types;
            }
		}

		#endregion		
	}
}
