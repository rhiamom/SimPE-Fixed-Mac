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

// MainForm constructor — renamed from Main.Designer.cs so it is NOT excluded
// by the <Compile Remove="**\*.Designer.cs" /> rule in Directory.Build.targets.
// All field declarations live in SimPE.Main.Stubs.cs.

namespace SimPe
{
    partial class MainForm : Avalonia.Controls.Window
    {
        public MainForm()
        {
            InitializeComponent();

            // Wire ResourceViewManager to the tree and list controls from AXAML.
            resourceViewManager1.TreeView  = tv;
            resourceViewManager1.ListView  = lv;

            // Wire list selection and resource-load events.
            lv.SelectionChanged  += lv_SelectionChanged;
            lv.SelectedResource  += lv_SelectResource;

            // Wire filter controls.
            tbInst.TextChanged       += SetFilter;
            tbGrp.TextChanged        += SetFilter;
            cbsemig.SelectionChanged += SetSemiGlobalFilter;

            // Wire menu clicks (Avalonia MenuItem.Click cannot be bound in AXAML via attribute).
            avlnNew.Click            += Activate_miNew;
            avlnOpen.Click           += Activate_miOpen;
            avlnOpenSimsRes.Click    += Activate_miOpenSimsRes;
            avlnOpenDownloads.Click  += Activate_miOpenDownloads;
            avlnSave.Click           += Activate_miSave;
            avlnSaveAs.Click         += Activate_miSaveAs;
            avlnSaveCopyAs.Click     += Activate_miSaveCopyAs;
            avlnClose.Click          += Activate_miClose;
            avlnExit.Click           += Activate_miExit;
            avlnPreferences.Click    += ShowPreferences;
            avlnGameRoot.Click       += miGameRoot_Click;
            avlnMetaInfo.Click       += Activate_miNoMeta;
            avlnFileNames.Click      += Activate_miFileNames;
            avlnSaveProfile.Click    += tsmiSaveProfile_Click;
            avlnSavePrefs.Click      += tsmiSavePrefs_Click;
            avlnStopWaiting.Click    += tsmiStopWaiting_Click;
            avlnWelcome.Click        += Activate_miWelcome;
            avlnAbout.Click          += Activate_miAbout;
            avlnKBase.Click          += miKBase_Clicked;

            tabBtnObjectWorkshop.Click  += (_, _) => ActivateRightPanel("Object Workshop");
            tabBtnFilterResources.Click += (_, _) => ActivateRightPanel("Filter Resources");

            SetupMainForm();
            this.Opened += LoadForm;
        }
    }
}
