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
	public class SimpleTextPackedFileWrapper        
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

		public SimpleTextPackedFileWrapper() : base()
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
            return new SimpleTextPackedFileUI();
		}

		protected override IWrapperInfo CreateWrapperInfo()
        {
                return new AbstractWrapperInfo(
                    "Simple Text Viewer Wrapper",
                    "Chris",
                    "To View the Simple Text Files",
                    1,
                    null
                    );
		}

		protected override void Unserialize(System.IO.BinaryReader reader)
        {
            reader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            strung = "";
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                char b = reader.ReadChar();
                strung += b;
            }
        }
		
		protected override void Serialize(System.IO.BinaryWriter writer)
        {
            if (strung != null) foreach (char c in strung) writer.Write(c);
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
                        0xA2E3D533, //handles the Accelerator Key Definitions
                        0x50544250 //handles the Silly Package Text
                    };
                return types;
            }
		}

		#endregion		
	}
}
