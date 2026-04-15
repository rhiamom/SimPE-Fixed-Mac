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
using System.Collections;
using System.ComponentModel;
// using System.Windows.Forms; — removed for Avalonia port


namespace SimPe.Plugin.Tool.Window
{
	/// <summary>
	/// Tool that should automatically repair corrupted packages
	/// </summary>
	public class InstallerTool : SimPe.Interfaces.IToolPlus
	{		
		public InstallerTool()
		{
		}
		#region IToolPlus Member

		public void Execute(object sender, SimPe.Events.ResourceEventArgs e)
		{
			InstallerForm f = new InstallerForm();
			f.Show();
		}

		public bool ChangeEnabledStateEventHandler(object sender, SimPe.Events.ResourceEventArgs e)
		{
			return true;
		}

		#endregion

		#region IToolExt Member

		public int Shortcut
		{
			get
			{				
				return 0; // WinForms shortcut — rewired to Avalonia key bindings in a future pass
			}
		}

		public bool Visible
		{
			get
			{
				return true;
			}
		}

		public object Icon
		{
			get
			{
                return SimPe.LoadIcon.load ("contents.png");
			}
		}

		#endregion

		#region IToolPlugin Member

		public override string ToString()
		{
			return "Content Preview...";
		}

		#endregion
	}
}
