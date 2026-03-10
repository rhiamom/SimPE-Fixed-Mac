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
	public class LastEPusePackedFileWrapper        
		: AbstractWrapper, IFileWrapper, IFileWrapperSaveExtension
    {
        #region CreationIndex Attribute
        private ushort vershin = 0;
        private ushort prevep;
        private bool gotmore = false;

        public Array vdata = Array.CreateInstance(typeof(uint), 24, 14); // would never be more than 12 but have allowed 14 in case

        public bool GotMore
        {
            get { return gotmore; }
            set { gotmore = value; }
        }

        public ushort Vershin
        {
            get { return vershin; }
            set { vershin = value; }
        }
        public ushort Prevep
        {
            get { return prevep; }
            set { prevep = value; }
        }
		#endregion

		public LastEPusePackedFileWrapper() : base()
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
            return new LastEPusePackedFileUI();
		}

		protected override IWrapperInfo CreateWrapperInfo()
        {
                return new AbstractWrapperInfo(
                    "Last EP Used Wrapper",
                    "Chris",
                    "To View the Last EP Used in NeighborhoodManager.package",
                    2,
                    null
                    );
		}

        protected override void Unserialize(System.IO.BinaryReader reader) // if vershin == 9 then is Castaway Story (28) Pet Story is 3
        {
            gotmore = false;
            vershin = reader.ReadUInt16();
            if (vershin > 1)
            {
                reader.BaseStream.Seek(4, System.IO.SeekOrigin.Begin);
                prevep = reader.ReadUInt16();
            }
            else prevep = 0;

            // Castaway has lots more stuff
            if (vershin == 9 && prevep == 7 && reader.BaseStream.Length > 600 && PathProvider.Global.GetExpansion(SimPe.Expansions.IslandStories).Exists)
            {
                int nuffin;
                int numba = 0;
                uint tempo;
                int c = 0;
                while ( numba != -1) // find first block
                    numba = reader.ReadInt32(); //will now have read the FFFFFFFF block start

                numba = reader.ReadInt32();
                if (numba == -1) return; // uninitialized this has to have a different value if story mode has ever run.
                gotmore = true;
                for (int n = 0; n < 24; n++) // clear previous use in case going from one player profile to another
                {
                    for (int i = 0; i < 12; i++)
                        vdata.SetValue((uint)0, n, i);
                }
                // have read past header and first block so go back 1x4 bytes, now every is the same
                reader.BaseStream.Seek(-4, System.IO.SeekOrigin.Current);
                for (int n = 0; n < 24; n++) // 1st loop is zero last loop is when n < 24 so is 23
                {
                    nuffin = reader.ReadInt32();// first (useless) line, will be FFFFFFFF if Uninited
                    if (nuffin == -1) // uninitialized block will be 2 zeros
                    {
                        vdata.SetValue((uint)0xFFFFFFFF, n, 0);
                        nuffin = reader.ReadInt32();
                        while (nuffin != -1)
                            nuffin = reader.ReadInt32(); //will now have read the FFFFFFFF block start
                    }
                    else
                    {
                        nuffin = reader.ReadInt32(); // always zero before numba
                        numba = reader.ReadInt32();
                        if (numba == 0) // no data and no more in this block, also no pre header for next block
                        {
                            vdata.SetValue((uint)0xFFFFFFFF, n, 0);
                            nuffin = reader.ReadInt32(); //will now have passed the next pre header and read next header
                            if (nuffin != -1) nuffin = reader.ReadInt32(); // just to be sure read next header
                        }
                        else
                        {
                            if (n == 0) numba++;
                            c = numba;
                            for (int i = 0; i < numba; i++)
                            {
                                tempo = reader.ReadUInt32();
                                if (1 < 12) { vdata.SetValue(tempo, n, i); }
                            }
                            //should now be at the next pre header, but check - some don't have and some have lots more
                            nuffin = reader.ReadInt32();
                            if (nuffin != -1) nuffin = reader.ReadInt32(); // now at next header
                            if (nuffin != -1) // found more
                            {
                                nuffin = reader.ReadInt32(); // should be zero
                                nuffin = reader.ReadInt32(); // next control item
                                numba = reader.ReadInt32();
                                if (numba > 0)
                                {
                                    numba += c;
                                    for (int i = c; i < numba; i++)
                                    {
                                        tempo = reader.ReadUInt32();
                                        if (1 < 12) { vdata.SetValue(tempo, n, i); }
                                    }
                                }
                                nuffin = reader.ReadInt32(); // locate next header
                                if (nuffin != -1) nuffin = reader.ReadInt32(); // now at next header
                            }
                        }
                        // nuffin should always be -1 here, perhaps add a check
                        if (nuffin != -1) nuffin = reader.ReadInt32(); // add a check
                    }
                }
            }
		}

		protected override void Serialize(System.IO.BinaryWriter writer)
        {
            ushort nuffin = 0;
            writer.Write(vershin);
            if (vershin > 1)
            {
                writer.BaseStream.Seek(0x4, System.IO.SeekOrigin.Begin);
                writer.Write(prevep);
            }
            writer.Write(nuffin);
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
                uint[] types = { 0xCC8A6A69 }; //handles the Previous EP Version Run
                return types;
            }
		}

		#endregion		
	}
}
