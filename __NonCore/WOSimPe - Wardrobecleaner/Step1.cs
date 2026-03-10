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
using System.Collections.Generic;
using System.Text;
using SimPe.Wizards;
using SimPe.Plugin.UI;

namespace SimPe.Plugin
{
    public class Step1 : StepBase, IWizardEntry
    {
        NeighborhoodBrowser form;

        #region IWizardEntry Members

        public string WizardCaption
        {
            get { return "Wardrobe Cleaner"; }
        }

        public string WizardDescription
        {
            get { return "Deletes clothing entries from households in a neighbourhood."; }
        }

        public System.Drawing.Image WizardImage
        {
            get { return global::SimPe.Wizards.Properties.Resources.WizardIcon; }
            // get { return SetKeyName(0, "WizardIcon.png"); }
        }

        private System.Drawing.Image SetKeyName(int p, string p_2)
        {
           throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        public override bool CanContinue
        {
            get
            {
                if (this.NeighborhoodPackage != null)
                    return true;
                return false;
            }
        }

        protected override bool Init()
        {
            this.NeighborhoodPackage = null;

            if (this.form != null)
            {
                this.form.UpdateList();
                this.form.PackageChanged += delegate(object sender, EventArgs e)
                {
                    this.NeighborhoodPackage = this.form.NeighborhoodPackage;
                    this.FamilyInstances.Clear();
                    this.Update();
                };
            }
            return true;
        }

        public override IWizardForm Next
        {
            get
            {
                if (!this.CanContinue)
                    return null;
                return new Step2();
            }
        }

        public override string WizardMessage
        {
            get { return "Select Neighbourhood"; }
        }

        public override int WizardStep
        {
            get { return 2; }
        }

        public override System.Windows.Forms.Panel WizardWindow
        {
            get
            {
                if (this.form == null)
                    this.form = new NeighborhoodBrowser();
                return this.form;
            }
        }
    }
}
