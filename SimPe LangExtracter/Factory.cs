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
using SimPe.Interfaces;

namespace SimPe.Plugin
{
	public class WrapperFactory : SimPe.Interfaces.Plugin.AbstractWrapperFactory, SimPe.Interfaces.Plugin.IToolFactory
	{
		#region AbstractWrapperFactory Member
		public override SimPe.Interfaces.IWrapper[] KnownWrappers
		{
			get 
			{
				IWrapper[] wrappers = {};
				return wrappers;
			}
		}
		#endregion

		#region IToolFactory Member
		public IToolPlugin[] KnownTools
		{
			get
            {
                return new IToolPlugin[] { new ExtractTool(this.LinkedRegistry, this.LinkedProvider) };
			}
		}
        #endregion
	}
}