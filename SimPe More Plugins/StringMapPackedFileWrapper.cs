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
	public class StringMapPackedFileWrapper        
		: AbstractWrapper, IFileWrapper, IFileWrapperSaveExtension
    {
        #region Attribute
        /// <summary>
		/// Contains the Data of the File
		/// </summary>
        /// 
        private byte[] filename;
        private byte[] filetype;
        private ushort[] datas;
        private uint[] types;
        private string[] strins;
        private uint desc = 3401907264;
        private int version = 1;

        public ushort[] Datas
        {
            get { return datas; }
            set { datas = value; }
        }
        public uint[] TyPes
        {
            get { return types; }
            set { types = value; }
        }

        /// <summary>
        /// Returns/Sets the Strings in the File
        /// </summary>
        /// 
        public string[] Strings
        {
            get { return strins; }
            set { strins = value; }
        }

        /// <summary>
        /// Returns the Filename
        /// </summary>
        public string FileName
        {
            get { return Helper.ToString(filename); }
            set
            {
                if (!Helper.ToString(filename).Equals(value))
                {
                    filename = Helper.ToBytes(value, 0x40);
                }
            }
        }

		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		public StringMapPackedFileWrapper() : base()
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
			return new StringMapPackedFileUI();
		}
		/// <summary>
        /// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
        protected override IWrapperInfo CreateWrapperInfo()
        {
            return new AbstractWrapperInfo(
                "String Map Wrapper",
                "Chris",
                "To View/Edit String Map Files",
                1,
                null
                );
        }

		/// <summary>
		/// Unserializes a BinaryStream into the Attributes of this Instance
		/// </summary>
		/// <param name="reader">The Stream that contains the FileData</param>
		protected override void Unserialize(System.IO.BinaryReader reader)
        {
            filename = reader.ReadBytes(64);
            desc = reader.ReadUInt32();
            version = reader.ReadInt32();
            filetype = reader.ReadBytes(11);
            int len = reader.ReadInt32();
            Array.Resize<ushort>(ref datas, len);
            Array.Resize<uint>(ref types, len);
            Array.Resize<string>(ref strins, len);
            for (int i = 0; i < len; i++)
            {
                strins[i] = reader.ReadString();
                datas[i] = reader.ReadUInt16();
                types[i] = reader.ReadUInt32();
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
            writer.Write(filename);
            writer.Write(desc);
            writer.Write(version);
            writer.Write(filetype);
            writer.Write(datas.Length);
            for (int i = 0; i < strins.Length; i++)
            {
                writer.Write(strins[i]);
                writer.Write(datas[i]);
                writer.Write(types[i]);
            }
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
                uint[] types = { 0xCAC4FC40 }; //handles the String Map File
				return types;
			}
		}

        #endregion
    }
}
