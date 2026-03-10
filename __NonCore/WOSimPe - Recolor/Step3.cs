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
	public class Step3 :AWizardForm, IWizardFinish
	{
		public Step3()
		{
			
		}
		#region IWizardFinish Member
		public void Finit()
		{
			Step1.Form.SaveRecolor();
			System.Windows.Forms.MessageBox.Show("The Recolour was saved.");
		}
		#endregion

		#region IWizardForm Member

		public override System.Windows.Forms.Panel WizardWindow
		{
			get
			{
				return Step1.Form.pnwizard3;
			}
		}

		protected override bool Init()
		{
			Step1.Form.lberr.Visible = System.IO.File.Exists(Step1.Form.GetPackageFilename);
			return true;
		}

		public override string WizardMessage
		{
			get
			{
				return "Specify the Name of your Recolour";
			}
		}

		public override bool CanContinue
		{
			get
			{
				return ((!System.IO.File.Exists(Step1.Form.GetPackageFilename)) || (Step1.Form.cbover.Checked));
			}
		}

		public override int WizardStep
		{
			get
			{
				return 5;
			}
		}

		public override IWizardForm Next
		{
			get
			{
				return null;
			}
		}

		#endregion
	}
}
