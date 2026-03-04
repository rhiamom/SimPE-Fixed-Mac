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
	public abstract class SimpleTypeHandler : Downloads.ITypeHandler, System.IDisposable
	{
		protected PackageInfo nfo;
		public SimpleTypeHandler()
		{
			
		}

		protected abstract void SetName(SimPe.Interfaces.Files.IPackageFile pkg);
		protected abstract void SetImage(SimPe.Interfaces.Files.IPackageFile pkg);

		protected void SetName(uint type, SimPe.Interfaces.Files.IPackageFile pkg)
		{
			SimPe.Interfaces.Files.IPackedFileDescriptor[] pfds = pkg.FindFiles(type);

			if (pfds.Length>0)
			{
				SimPe.PackedFiles.Wrapper.StrItemList items = Downloads.DefaultTypeHandler.GetCtssItems(pfds[0], pkg);
				if (items.Length>0) nfo.Name = items[0].Title;
			}
		}

		protected void SetImage(uint type, SimPe.Interfaces.Files.IPackageFile pkg)
		{
			SimPe.Interfaces.Files.IPackedFileDescriptor[] pfds = pkg.FindFiles(type);
			if (pfds.Length>0) 
			{
				SimPe.PackedFiles.Wrapper.Picture pic = new SimPe.PackedFiles.Wrapper.Picture();
				pic.ProcessData(pfds[0], pkg);
				nfo.Image = pic.Image;
			}

			nfo.KnockoutThumbnail = false;
		}

		protected virtual void BeforeLoadContent(SimPe.Cache.PackageType type, SimPe.Interfaces.Files.IPackageFile pkg)
		{
		}

		protected virtual void AfterLoadContent(SimPe.Cache.PackageType type, SimPe.Interfaces.Files.IPackageFile pkg)
		{
		}

		#region ITypeHandler Member

		

		public void LoadContent(SimPe.Cache.PackageType type, SimPe.Interfaces.Files.IPackageFile pkg)
		{
			nfo = new PackageInfo(pkg);
			BeforeLoadContent(type, pkg);			
			SetName(pkg);
			SetImage(pkg);
			AfterLoadContent(type, pkg);
		}		

		public IPackageInfo[] Objects
		{
			get
			{
				return new IPackageInfo[] {nfo};
			}
		}

		#endregion

		#region IDisposable Member

		public virtual void Dispose()
		{
			this.nfo = null;
		}

		#endregion
	}
}
