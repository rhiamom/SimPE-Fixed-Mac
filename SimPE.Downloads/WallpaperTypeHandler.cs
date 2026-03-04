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
	/// Summary description for WallpaperTypeHandler.
	/// </summary>
	public class WallpaperTypeHandler : LotTypeHandler
	{
		public WallpaperTypeHandler() : base()
		{			
		}

		internal static System.Drawing.Image SetFromTxtr(SimPe.Interfaces.Files.IPackageFile pkg)
		{
			SimPe.Interfaces.Files.IPackedFileDescriptor[] pfds = pkg.FindFiles(Data.MetaData.TXTR);
			if (pfds.Length>0) 
			{
				SimPe.Interfaces.Files.IPackedFileDescriptor pfd = pfds[0];
				foreach (SimPe.Interfaces.Files.IPackedFileDescriptor p in pfds)				
					if (p.Size>pfd.Size) pfd = p;
				
				SimPe.Plugin.Rcol rcol = new SimPe.Plugin.GenericRcol();
				rcol.ProcessData(pfd, pkg);
				if (rcol.Blocks.Length>0) 
				{
					SimPe.Plugin.ImageData id = rcol.Blocks[0] as SimPe.Plugin.ImageData;
					if (id!=null)
					{
						SimPe.Plugin.MipMap m = id.GetLargestTexture(new System.Drawing.Size(PackageInfo.IMAGESIZE, PackageInfo.IMAGESIZE));
						if (m!=null) return m.Texture;
					}
				}
			}	
		
			return null;
		}

		protected override void SetImage(SimPe.Interfaces.Files.IPackageFile pkg)
		{
			nfo.Image = SetFromTxtr(pkg);
			nfo.KnockoutThumbnail = false;
		}
	}
}
