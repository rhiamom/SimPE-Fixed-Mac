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

namespace SimPe.Plugin.Tool.Dockable
{
	/// <summary>
	/// Tghe Object Workshop as new Dockable Tool
	/// </summary>
	public class ObectWorkshopDockTool : SimPe.Interfaces.IDockableTool
	{		
		dcObjectWorkshop dc;
		public ObectWorkshopDockTool()
		{
			dc = new dcObjectWorkshop();
		}

		#region IDockableTool Member

		public Ambertation.Windows.Forms.DockPanel GetDockableControl()
		{
			return dc;
		}		

		public event SimPe.Events.ChangedResourceEvent ShowNewResource;

		public void RefreshDock(object sender, SimPe.Events.ResourceEventArgs es)
		{
			
		}

		#endregion

		#region IToolPlugin Member

		public override string ToString()
		{
			return dc.Text;
		}

		#endregion

		#region IToolExt Member

		public int Shortcut
		{
			get { return 0; }
		}

		public object Icon
		{
			get
			{
				return dc.TabImage as System.Drawing.Image;
			}
		}

		public virtual bool Visible
		{
			get { return GetDockableControl().IsDocked ||  GetDockableControl().IsFloating; }
		}

		#endregion
	}
}
