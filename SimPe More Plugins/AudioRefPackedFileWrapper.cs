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
	public class AudioRefPackedFileWrapper
		: AbstractWrapper, IFileWrapper, IFileWrapperSaveExtension
    {
        #region CreationIndex Attribute
        public string strung; // the actaul string,
        public string Strung
        {
            get { return strung; }
            set { strung = value; }
        }
		#endregion

		public AudioRefPackedFileWrapper() : base()
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
            return new AudioRefPackedFileUI();
		}

		protected override IWrapperInfo CreateWrapperInfo()
        {
                return new AbstractWrapperInfo(
                    "FWAV and CATS Wrapper",
                    "Chris",
                    "To View or Edit (unused) Audio Reference and Catalogue String Files",
                    1,
                    null
                    );
		}

		protected override void Unserialize(System.IO.BinaryReader reader)
        {
            reader.BaseStream.Seek(0x40, System.IO.SeekOrigin.Begin);
            strung = "";
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                char b = reader.ReadChar();
                if (b != 0) strung += b; else strung += "\n";
            }
        }
		
		protected override void Serialize(System.IO.BinaryWriter writer)
        {
            byte f = 0;
            writer.BaseStream.Seek(0x40, System.IO.SeekOrigin.Begin);
            if (strung != null) foreach (char c in strung)
                {
                    if (c != 10) writer.Write(c); else writer.Write(f);
                }
            writer.Write(f);
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
                uint[] types = 
                    {
                        0x43415453, // Catalogue String
                        0x46574156 // Audio Reference
                    };
                return types;
            }
		}

		#endregion		
	}
}
