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
	/// Summary description for TypedPackageHandler.
	/// </summary>
	public class PackageHandler : Downloads.IPackageHandler, System.IDisposable
	{
		SimPe.Cache.PackageType type;
		string flname;
		Downloads.ITypeHandler hnd;
		public PackageHandler(string filename): this(SimPe.Packages.File.LoadFromFile(filename))			
		{
			
		}

		public PackageHandler(SimPe.Interfaces.Files.IPackageFile pkg)			
		{	
			this.flname = pkg.SaveFileName;
			type = SimPe.Cache.PackageType.Undefined;
			DeterminType(pkg);
			Reset();

            if (type == SimPe.Cache.PackageType.CustomObject || type == SimPe.Cache.PackageType.Sim || type == SimPe.Cache.PackageType.Object)
				SimPe.PackedFiles.Wrapper.ObjectComboBox.ObjectCache.ReloadCache(SimPe.Plugin.DownloadsToolFactory.FileIndex, false);
			
			hnd = HandlerRegistry.Global.LoadTypeHandler(type, pkg);
			LoadContent( pkg);
		}	

		protected virtual void DeterminType(SimPe.Interfaces.Files.IPackageFile pkg)
        {
            if (System.IO.File.Exists(System.IO.Path.Combine(SimPe.Helper.SimPePluginPath, "simpe.scanfolder.plugin.dll")))
                type = PackageInfo.ClassifyPackage(pkg);
            else
                type = SimPe.Cache.PackageType.Undefined;
		}		

		protected virtual void OnLoadContent(SimPe.Interfaces.Files.IPackageFile pkg, SimPe.Cache.PackageType type)
		{
		}

		protected virtual void OnReset(SimPe.Cache.PackageType type)
		{
		}

		protected void LoadContent(SimPe.Interfaces.Files.IPackageFile pkg)
		{
			hnd.LoadContent(type, pkg);
			foreach (Downloads.IPackageInfo nfo in hnd.Objects)
				if (nfo is Downloads.PackageInfo)
					((Downloads.PackageInfo)nfo).Type = type;

			OnLoadContent(pkg, type);
		}
		
		protected void Reset()
		{
			OnReset(type);
		}

		#region IPackageHandler Member
		public void FreeResources()
		{
			SimPe.Packages.StreamFactory.CloseStream(this.flname);
		}
		public IPackageInfo[] Objects
		{
			get
			{
				return hnd.Objects;
			}
		}

		#endregion

		#region IDisposable Member

		public void Dispose()
		{
			flname = null;
			hnd = null;
		}

		#endregion
	}
}
