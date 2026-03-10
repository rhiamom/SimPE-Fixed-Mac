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
	/// <summary>
	/// This is the actual FileWrapper
	/// </summary>
	/// <remarks>
	/// The wrapper is used to (un)serialize the Data of a file into it's Attributes. So Basically it reads 
	/// a BinaryStream and translates the data into some userdefine Attributes.
	/// </remarks>
	public class InventItemPackedFileWrapper        
		: AbstractWrapper, IFileWrapper, IFileWrapperSaveExtension
    {
        #region InvItem Attribute
        /// <summary>
		/// Contains the Data of the File
		/// </summary>
        private string lbldisp;
        private UInt32 guide;
        private UInt32 guidnx;

        /// <summary>
        /// Returns if Hub Bug was found
        /// </summary>
        public string DispLabel
        {
            get { return lbldisp; }
            set { lbldisp = value; }
        }

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public InventItemPackedFileWrapper() : base()
		{
			///
			/// Add your Contructor Stuff here (if needed)
			///
		}

        #region IWrapper member
        public override bool CheckVersion(uint version)
        {
            return true;
        }
        #endregion

		#region AbstractWrapper Member
		protected override IPackedFileUI CreateDefaultUIHandler()
		{
			return new InventItemPackedFileUI();
		}
		/// <summary>
        /// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
		protected override IWrapperInfo CreateWrapperInfo()
		{
                return new AbstractWrapperInfo(
                    "Inventory Item Wrapper",
                    "Chris",
                    "Reads the Inventory Items",
                    2,
                    null
                    );
		}

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		protected override void Unserialize(System.IO.BinaryReader reader)
        {
            if (!pjse.GUIDIndex.TheGUIDIndex.IsLoaded) pjse.GUIDIndex.TheGUIDIndex.Load(pjse.GUIDIndex.DefaultGUIDFile);

            reader.BaseStream.Seek(16, System.IO.SeekOrigin.Begin); // Begin at first GUID
            guide = reader.ReadUInt32();
            lbldisp = "GUID 0x" + SimPe.Helper.HexString(guide) + "  " + pjse.GUIDIndex.TheGUIDIndex[guide];
            guidnx = reader.ReadUInt32();
            if (guidnx != guide)
            {
                lbldisp += "\n Contains\n GUID 0x" + SimPe.Helper.HexString(guidnx) + "  " + pjse.GUIDIndex.TheGUIDIndex[guidnx];

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    guidnx = reader.ReadUInt32();
                    if (guidnx != guide) lbldisp += "\n GUID 0x" + SimPe.Helper.HexString(guidnx) + "  " + pjse.GUIDIndex.TheGUIDIndex[guidnx];
                    else break;
                }
            }
		}


		/// <summary>
		/// Serializes a the Attributes stored in this Instance to the BinaryStream
		/// </summary>
		/// <param name="writer">The Stream the Data should be stored to</param>
		/// <remarks>
		/// Be sure that the Position of the stream is Proper on 
		/// return (i.e. must point to the first Byte after your actual File)
		/// </remarks>
		protected override void Serialize(System.IO.BinaryWriter writer)
		{
		}
		#endregion

		#region IFileWrapperSaveExtension Member		
			//all covered by Serialize()
		#endregion

		#region IFileWrapper Member

		/// <summary>
		/// Returns the Signature that can be used to identify Files processable with this Plugin
		/// </summary>
		public byte[] FileSignature
		{
			get
			{
				/// 
				/// Add the Signature Array if needed
				/// 
				return new byte[0];
			}
		}

		/// <summary>
		/// Returns a list of File Type this Plugin can process
		/// </summary>
		public uint[] AssignableTypes
		{
			get
			{
                uint[] types = { 0x4F6FD33D }; // Inventory Item
				return types;
			}
		}

        #endregion
    }
}
