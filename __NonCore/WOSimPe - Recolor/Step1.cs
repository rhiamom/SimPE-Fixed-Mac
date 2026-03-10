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

namespace SimPe.Wizards
{
	/// <summary>
	/// Zusammenfassung für Step1.
	/// </summary>
	public class Step1 : AWizardForm, IWizardEntry
	{
		static RecolourWizardForm dwf;

		/// <summary>
		/// Returns the Main Form
		/// </summary>
		public static RecolourWizardForm Form
		{
			get { 
				if (dwf==null) dwf = new RecolourWizardForm();
				return dwf; 
			}
		}

		public Step1()
		{
			
		}

		#region IWizardEntry Member

		public string WizardDescription
		{
			get
			{
				return "Use this Wizard to create Recolours of Objects that shipped with The Sims 2.";
			}
		}

		public string WizardCaption
		{
			get
			{
				return "Recolours";
			}
		}

		public System.Drawing.Image WizardImage
		{
			get
			{
				return Form.pb.Image;
			}
		}

		#endregion

		#region IWizardForm Member

		public override System.Windows.Forms.Panel WizardWindow
		{
			get
			{
				return Form.pnwizard1;
			}
		}

		protected override bool Init()
		{
			if (Form.step1==null) Form.step1 = this;
				
			Form.BuildList();
			return true;
		}

		public override string WizardMessage
		{
			get
			{
				return "Select the Object you want to Recolour";
			}
		}

		public override bool CanContinue
		{
			get
			{
				if (Form.selectedlv==null) return false;
				return (Form.selectedlv.SelectedItems.Count>0);
			}
		}

		public override int WizardStep
		{
			get
			{
				return 2;
			}
		}

		public override IWizardForm Next
		{
			get
			{
				if (Form.step1b==null) Form.step1b = new Step1b();
				return Form.step1b;
			}
		}

		#endregion
	}
}
