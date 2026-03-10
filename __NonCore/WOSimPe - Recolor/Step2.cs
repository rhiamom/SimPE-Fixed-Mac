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
	public class Step2 : AWizardForm
	{
		

		public Step2()
		{
			
		}

		#region IWizardForm Member

		public override System.Windows.Forms.Panel WizardWindow
		{
			get
			{
				return Step1.Form.pnwizard2;
			}
		}

		protected override bool Init()
		{
			Step1.Form.Recolor2();			
			return true;
		}

		public override string WizardMessage
		{
			get
			{
				return "Recolour the loaded texture Files";
			}
		}

		public override bool CanContinue
		{
			get
			{
				return (Step1.Form.lv.Items.Count>0);
			}
		}

		public override int WizardStep
		{
			get
			{
				return 4;
			}
		}

		public override IWizardForm Next
		{
			get
			{
				if (Step1.Form.step3==null) Step1.Form.step3 = new Step3();
				return Step1.Form.step3;
			}
		}

		#endregion
	}
}
