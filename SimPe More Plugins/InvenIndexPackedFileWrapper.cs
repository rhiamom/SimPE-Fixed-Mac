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
	public class InvenIndexPackedFileWrapper        
		: AbstractWrapper, IFileWrapper, IFileWrapperSaveExtension
    {
        #region CreationIndex Attribute
        private uint sciname;
        public uint Sciname
        {
            get { return sciname; }
            set { sciname = value; }
        }
		#endregion

		public InvenIndexPackedFileWrapper() : base()
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
            return new InvenIndexPackedFileUI();
		}

		protected override IWrapperInfo CreateWrapperInfo()
        {
                return new AbstractWrapperInfo(
                    "Invenory Item Index Wrapper",
                    "Chris",
                    "To View/Edit the Invenory Item Index",
                    2,
                    null
                    );
		}

		protected override void Unserialize(System.IO.BinaryReader reader)
        {
            reader.BaseStream.Seek(0x4, System.IO.SeekOrigin.Begin);
            sciname = reader.ReadUInt32();
		}

		protected override void Serialize(System.IO.BinaryWriter writer)
        {
            uint scivers = 0x4;
            writer.Write(scivers);
            writer.Write(sciname);
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
                uint[] types = { 0x0F9F0C21 }; //handles the Invenory Item Index
                return types;
            }
		}

		#endregion		
	}
}
