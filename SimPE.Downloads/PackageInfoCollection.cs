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
	/// Summary description for PackageInfoCollection.
	/// </summary>
	public class PackageInfoCollection : System.IDisposable, System.Collections.IEnumerable
	{
		ArrayList list;
		public PackageInfoCollection()
		{
			list = new ArrayList();
		}

		public int Count
		{
			get {return list.Count;}
		}

		public void Clear()
		{
			list.Clear();
		}

		public void Add(IPackageInfo item)
		{
			list.Add(item);
		}

		public void AddRange(IPackageInfo[] items)
		{
			list.AddRange(items);
		}

		public void AddRange(PackageInfoCollection items)
		{
			foreach (PackageInfo item in items)
				list.Add(item);
		}

		public void Remove(IPackageInfo item)
		{
			list.Remove(item);
		}

		public bool Contains(IPackageInfo item)
		{
			return list.Contains(item);
		}

		public IPackageInfo this[int index]
		{
			get {return list[index] as IPackageInfo;}
			set {list[index] = value;}
		}

		public IPackageInfo[] ToArray()
		{
			IPackageInfo[] ret = new IPackageInfo[list.Count];
			list.CopyTo(ret);
			return ret;
		}

		#region IDisposable Member

		public void Dispose()
		{
			if (list!=null) list.Clear();
			list = null;
		}

		#endregion

		#region IEnumerable Member

		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		#endregion
	}
}
