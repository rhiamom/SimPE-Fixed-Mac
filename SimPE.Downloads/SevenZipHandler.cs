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
using System.Collections;

namespace SimPe.Plugin.Downloads
{
	/// <summary>
	/// Summary description for SevenZipHandler.
	/// </summary>
	public class SevenZipHandler : ArchiveHandler
	{		
		public SevenZipHandler(string filename) : base(filename)
		{		
		
		}								

		protected override StringArrayList ExtractArchive()
		{
			StringArrayList ret = new StringArrayList();					
			Ambertation.SevenZip.IO.CommandlineArchive a = new Ambertation.SevenZip.IO.CommandlineArchive(this.ArchiveName);
			Ambertation.SevenZip.IO.ArchiveFile[] content = a.ListContent();
			a.Extract(SimPe.Helper.SimPeTeleportPath, false);

			foreach (Ambertation.SevenZip.IO.ArchiveFile desc in content)
			{				
				string rname = System.IO.Path.Combine(Helper.SimPeTeleportPath, desc.Name);	
				if (System.IO.File.Exists(rname))
					ret.Add(rname);
			}
			return ret;
		}		
	}
}
