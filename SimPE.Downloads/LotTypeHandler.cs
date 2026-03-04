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

namespace SimPe.Plugin.Downloads
{
	/// <summary>
	/// Summary description for LotTypeHandler.
	/// </summary>
	public class LotTypeHandler : SimpleTypeHandler
	{		
		public LotTypeHandler() : base()
		{
			
		}

		protected override void SetName(SimPe.Interfaces.Files.IPackageFile pkg)
		{
			SetName(Data.MetaData.STRING_FILE, pkg);			
		}

		protected override void SetImage(SimPe.Interfaces.Files.IPackageFile pkg)
		{
			SimPe.Interfaces.Files.IPackedFileDescriptor pfd = pkg.FindFile(0x856DDBAC, 0, Data.MetaData.LOCAL_GROUP, 0x35CA0002);
			if (pfd==null) 
			{
				SimPe.Interfaces.Files.IPackedFileDescriptor[] pfds = pkg.FindFiles(0x856DDBAC);
				if (pfds.Length>0) pfd = pfds[0];
			}

			if (pfd!=null)
			{
				SimPe.PackedFiles.Wrapper.Picture pic = new SimPe.PackedFiles.Wrapper.Picture();
				pic.ProcessData(pfd, pkg);
				nfo.Image = pic.Image;
			}

			nfo.KnockoutThumbnail = false;
		}
	}
}
