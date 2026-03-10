/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSweatShop                                 *
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
    public class MoreWrapperFactory : SimPe.Interfaces.Plugin.AbstractWrapperFactory
    {
        #region AbstractWrapperFactory Member
        public override SimPe.Interfaces.IWrapper[] KnownWrappers
		{
			get
            {
                if (Helper.StartedGui == Executable.Classic || Helper.SimPeVersionLong < 330717003793) // requires updated simpe.workspace and GDF
                {
                    return new IWrapper[0];
                }
                else
                {
                    IWrapper[] wrappers = {
										  new SimindexPackedFileWrapper()
										  ,new FunctionPackedFileWrapper()
										  ,new SimpleTextPackedFileWrapper()
										  ,new SimmyListPackedFileWrapper()
										  ,new HugBugPackedFileWrapper()
										  ,new AudioRefPackedFileWrapper()
										  ,new InvenIndexPackedFileWrapper()
										  ,new InventItemPackedFileWrapper()
										  ,new WinfoPackedFileWrapper()
										  ,new LotexturePackedFileWrapper()
										  ,new CregPackedFileWrapper()
										  ,new WallLayerPackedFileWrapper()
										  ,new StringMapPackedFileWrapper()
									  };
                    return wrappers;
                }
			}
		}
        #endregion
    }
}
