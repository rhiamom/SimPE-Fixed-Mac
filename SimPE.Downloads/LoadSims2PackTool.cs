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
using SimPe.Events;
using Avalonia.Controls;

namespace SimPe.Plugin.Tool
{
	/// <summary>
	/// Summary description for LoadSims2PackTool.
	/// </summary>
	public class LoadSims2PackTool : SimPe.Interfaces.IToolPlus	
	{
		internal LoadSims2PackTool() 
		{
			
		}		

		#region ITool Member

		public bool ChangeEnabledStateEventHandler(object sender, ResourceEventArgs e)
		{
			return true;
		}

        public void Execute(object sender, ResourceEventArgs es)
        {
            if (!ChangeEnabledStateEventHandler(sender, es)) return;

            SimPe.Scenegraph.Compat.OpenFileDialogCompat ofd = new SimPe.Scenegraph.Compat.OpenFileDialogCompat();
            ofd.Filter = SimPe.ExtensionProvider.BuildFilterString(new SimPe.ExtensionType[] { ExtensionType.Sim2Pack, ExtensionType.AllFiles });
            if (ofd.ShowDialog() == SimPe.DialogResult.OK)
            {
                SimPe.Packages.S2CPDescriptor[] ds = SimPe.Packages.Sims2CommunityPack.ShowSimpleOpenDialog(ofd.FileName, Avalonia.Controls.SelectionMode.Single);
                if (ds != null)
                    foreach (SimPe.Packages.S2CPDescriptor d in ds)
                        SimPe.RemoteControl.OpenMemoryPackage(d.Package);
            }
        }


		public override string ToString()
		{
			return "Package Tool\\Open Sims2Pack...";
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

		public object Icon
		{
			get
            {
                return SimPe.LoadIcon.load("Main_biOpen.Image.png");
			}
		}

		public virtual bool Visible 
		{
			get {return true;}
		}

		#endregion
	}
}
