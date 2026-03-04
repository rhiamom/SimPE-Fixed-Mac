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
	/// GLoabl regisry, that contains a listing of all Package Content Handler
	/// </summary>
	public sealed class HandlerRegistry
	{
		static HandlerRegistry glob;
		public static HandlerRegistry Global
		{
			get 
			{
				if (glob==null) glob = new HandlerRegistry();
				return glob;
			}
		}		

		Hashtable reg, subreg;
		HandlerRegistry()
		{
			reg = new Hashtable();
			subreg = new Hashtable();
			
			AddFilehandler(ExtensionType.Package, typeof(PackageHandler));
			AddFilehandler(ExtensionType.DisabledPackage, typeof(PackageHandler));
            AddFilehandler(ExtensionType.Sim2Pack, typeof(Sims2PackHandler));
            AddFilehandler(ExtensionType.Sim2PackCommunity, typeof(Sims2PackHandler));
            // Nothing is 'Supported For Unpack' if SimPe folder is Windows protected
            Ambertation.SevenZip.IO.CommandlineArchive a = new Ambertation.SevenZip.IO.CommandlineArchive("");
            foreach (string ext in a.SupportedForUnpack)
                this.AddFileHandler(ext, typeof(SevenZipHandler));

			this.AddTypeHandler(SimPe.Cache.PackageType.Lot, typeof(LotTypeHandler));
			this.AddTypeHandler(SimPe.Cache.PackageType.Wallpaper, typeof(WallpaperTypeHandler));
			this.AddTypeHandler(SimPe.Cache.PackageType.Floor, typeof(WallpaperTypeHandler));
			this.AddTypeHandler(SimPe.Cache.PackageType.Roof, typeof(WallpaperTypeHandler));
			this.AddTypeHandler(SimPe.Cache.PackageType.Terrain, typeof(WallpaperTypeHandler));
			this.AddTypeHandler(SimPe.Cache.PackageType.Sim, typeof(SimTypeHandler));
			this.AddTypeHandler(SimPe.Cache.PackageType.Neighbourhood, typeof(NeighborhoodTypeHandler));
			this.AddTypeHandler(SimPe.Cache.PackageType.Recolour, typeof(RecolorTypeHandler));
		}

		void AddFilehandler(ExtensionType ext, Type handler)
		{
			ExtensionDescriptor ed = ExtensionProvider.ExtensionMap[ext] as ExtensionDescriptor;
			foreach (string mext in ed.Extensions)	
			{			
				string fext = mext.Replace("*", "");
				if (!fext.StartsWith(".")) fext = "."+fext;
				AddFileHandler(fext, handler);	
			}
		}

		public void AddTypeHandler(SimPe.Cache.PackageType type, Type handler)
		{						
			subreg[type] = handler;
		}

		public ITypeHandler LoadTypeHandler(SimPe.Cache.PackageType type, SimPe.Interfaces.Files.IPackageFile pkg)
		{			
			Type t = subreg[type] as Type;
			if (t==null) return new XTypeHandler();

			return System.Activator.CreateInstance(t, new object[] {}) as ITypeHandler;
		}

		string FixedExtension(string extension)
		{
			extension = extension.Trim().ToLower();
			if (!extension.StartsWith(".")) extension = "."+extension;
			return extension;
		}

		public void AddFileHandler(string extension, Type handler)
		{			
			extension = FixedExtension(extension);
			reg[extension] = handler;
		}

		public bool HasFileHandler(string filename)
		{
			string ext = System.IO.Path.GetExtension(filename).Trim().ToLower();;
			object o = reg[ext];
			return (o!=null);
		}

		public IPackageHandler LoadFileHandler(string filename)
		{
			string ext = System.IO.Path.GetExtension(filename).Trim().ToLower();;
			Type t = reg[ext] as Type;
			if (t==null) return null;

			if (!FileTable.FileIndex.Loaded) FileTable.FileIndex.Load();
			return System.Activator.CreateInstance(t, new object[] {filename}) as IPackageHandler;
		}
	}
}
