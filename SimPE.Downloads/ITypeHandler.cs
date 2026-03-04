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
	/// This Interface is provided by classes, that read the Content of a certain Object Type
	/// </summary>
	/// <remarks>Defining classes must have a public Constructur that takes no Arguments</remarks>
	public interface ITypeHandler
	{
		/// <summary>
		/// Load the content of the passed package
		/// </summary>
		/// <param name="type"></param>
		/// <param name="pkg"></param>
		void LoadContent(SimPe.Cache.PackageType type, SimPe.Interfaces.Files.IPackageFile pkg);

		/// <summary>
		/// Returns informations about the Content stored in the package
		/// </summary>
		IPackageInfo[] Objects
		{
			get;
		}
	}
}
