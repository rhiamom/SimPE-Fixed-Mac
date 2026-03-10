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
using System.Collections;

namespace SimPe.Wizards
{
	/// <summary>
	/// Used to find all plugin Wizards
	/// </summary>
	public class WizardSelector
	{
		ArrayList wizards;


		/// <summary>
		/// Create a new Instance
		/// </summary>
		public WizardSelector()
		{
			wizards = new ArrayList();
			//Console.WriteLine("Loading Plugins from : "+this.WizardFolder);
			LoadWizards();
		}

		/// <summary>
		/// Returns all Loded Wizards
		/// </summary>
		public ArrayList Wizards
		{
			get { return wizards; }
		}

		/// <summary>
		/// Returns the Folder where Wizard Plugins are stored
		/// </summary>
		public string WizardFolder
		{
			get 
			{				
				return System.IO.Path.Combine(Helper.SimPePath, "Plugins"+SimPe.Helper.PATH_SEP);
			}
		}

		protected void LoadWizards()
		{
			wizards.Clear();
			if (!System.IO.Directory.Exists(WizardFolder)) return;

			string[] plugins = System.IO.Directory.GetFiles(WizardFolder, "*.wizard.dll");
			foreach (string plugin in plugins) 
			{
				object[] objs = SimPe.LoadFileWrappers.LoadPlugins(plugin, typeof(IWizardEntry));
				foreach (object o in objs) 
				{
					IWizardEntry bid = (IWizardEntry)o;
					wizards.Add(bid);
				}
			}
		}
	}
}
