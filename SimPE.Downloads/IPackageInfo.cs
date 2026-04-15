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
using System.Drawing;

namespace SimPe.Plugin.Downloads
{
	/// <summary>
	/// This Interface is used to Describe the Content of a given Package
	/// </summary>
	public interface IPackageInfo: System.IDisposable
	{
		/// <summary>
		/// Name of this Object
		/// </summary>
		string Name
		{
			get;
		}

		/// <summary>
		/// Description of this Object
		/// </summary>
		string Description
		{
			get;
		}

		/// <summary>
		/// Categorystring of this Object
		/// </summary>
        string Category
        {
            get;
        }		

		object GetThumbnail();
		object GetThumbnail(System.Drawing.Size sz);

		/// <summary>
		/// Returns the gameThumbnail or a SimPe Created 3D Preview, or null
		/// </summary>
		object Image
		{
			get;
		}

		/// <summary>
		/// Makes sure, the 3D Preview ic created
		/// </summary>
		void CreatePostponed3DPreview();

		/// <summary>
		/// A Game-Thumbnail was defined
		/// </summary>
        bool HasThumbnail
        {
            get;
        }

		/// <summary>
		/// Price of the Object
		/// </summary>
        int Price
        {
            get;
        }

		/// <summary>
		/// Number of vertices defined by GMDCs in the package
		/// </summary>
		int VertexCount
        {
            get;
        }

		/// <summary>
		/// Number of Vertices is high
		/// </summary>
		bool HighVertexCount
		{
			get;
		}

		/// <summary>
		/// Number of faces defined by GMDCs in the package
		/// </summary>
        int FaceCount
        {
            get;
        }

		/// <summary>
		/// Number of Faces is high
		/// </summary>
		bool HighFaceCount
		{
			get;
		}

		/// <summary>
		/// List of GUIDs used in the package
		/// </summary>
		uint[] Guids
		{
			get;
		}

		/// <summary>
		/// Type of the Package content
		/// </summary>
		SimPe.Cache.PackageType Type
		{
			get;
		}
	}
}
