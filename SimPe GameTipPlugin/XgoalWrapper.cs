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
using System.Collections;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for MmatWrapper.
	/// </summary>
	public class XGoal : SimPe.PackedFiles.Wrapper.Cpf
	{

		/// <summary>
		/// creates a new Instance
		/// </summary>
		public XGoal():base()
		{
		}

		/// <summary>
		/// Returns a Human Readable Description of this Wrapper
		/// </summary>
		/// <returns>Human Readable Description</returns>
		protected override SimPe.Interfaces.Plugin.IWrapperInfo CreateWrapperInfo()
		{
			return new SimPe.Interfaces.Plugin.AbstractWrapperInfo(
				"Goal Wrapper",
				"Chris",
				"To view Castaway Story Goals",
                1,
                null
				);
		}

		/// <summary>
		/// Returns a list of File Type this Plugin can process
		/// </summary>
		public override uint[] AssignableTypes
		{
			get
			{
				uint[] types = {
								   0xBEEF7B4D
							   };
			
				return types;
			}
		}		

		#region Default Attribute
		public uint StringInstance
		{
			get { return this.GetSaveItem("stringSet").UIntegerValue; }
			set { this.GetSaveItem("stringSet").UIntegerValue = value; }
		}

		public uint Guid
		{
			get { return this.GetSaveItem("id").UIntegerValue; }
			set { this.GetSaveItem("id").UIntegerValue = value; }
		}

		public uint IconInstance
		{
			get { return this.GetSaveItem("primaryIcon").UIntegerValue; }
			set { this.GetSaveItem("primaryIcon").UIntegerValue = value; }
		}

		public uint SecondaryIconInstance
		{
			get { return this.GetSaveItem("secondaryIcon").UIntegerValue; }
			set { this.GetSaveItem("secondaryIcon").UIntegerValue = value; }
		}

		public int Score
		{
			get { return this.GetSaveItem("score").IntegerValue; }
			set { this.GetSaveItem("score").IntegerValue = value; }
		}

		public int Influence
		{
			get { return this.GetSaveItem("influence").IntegerValue; }
			set { this.GetSaveItem("influence").IntegerValue = value; }
		}
		public string NodeText
		{
			get { return this.GetSaveItem("nodeText").StringValue; }
			set { this.GetSaveItem("nodeText").StringValue = value; }
		}

		public Interfaces.Files.IPackedFileDescriptor IconFileDescriptor
		{
			get 
			{
				SimPe.Packages.PackedFileDescriptor pfd = new SimPe.Packages.PackedFileDescriptor();
				pfd.Type = Data.MetaData.SIM_IMAGE_FILE;
				pfd.LongInstance = IconInstance;
				if (pfd.Instance==0) pfd.Instance = SecondaryIconInstance;
				pfd.Group = 0x499DB772;

				return pfd;
			}
		}
		#endregion

		public override string Description
		{
			get
			{
				return "GUID=0x"+Helper.HexString(this.FileDescriptor.Instance);
			}
		}

		protected override string GetResourceName(SimPe.Data.TypeAlias ta)
		{
			if (!this.Processed) ProcessData(FileDescriptor, Package);
			return this.NodeText;
		}
	}
}
