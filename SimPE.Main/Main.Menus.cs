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
using System.Windows.Forms;
using DialogResult = System.Windows.Forms.DialogResult;
using System.Data;
using SimPe.Events;

namespace SimPe
{
    partial class MainForm
    {
        // Maps the short TabText set by each dock panel to the full name used in the Window menu.
        static readonly System.Collections.Generic.Dictionary<string, string> _tabMenuNames =
            new System.Collections.Generic.Dictionary<string, string>(System.StringComparer.OrdinalIgnoreCase)
            {
                { "Hex",        "HexEditor" },
                { "Package",    "Package Information" },
                { "Wrapper",    "Wrapper Information" },
                { "Resource",   "Resource Information" },
                { "Converter",  "Number Converter" },
                { "Finder",     "Scenegraph Resource Finder" },
                { "Details",    "Package Details" },
                { "Plugin View","Plugin View" },
            };

        // Window-menu item tracking (for checked-state sync).
        private System.Collections.Generic.List<(string Label, Avalonia.Media.Imaging.Bitmap Icon, Avalonia.Controls.TabItem Tab)>
            _bottomTabDefs = new System.Collections.Generic.List<(string, Avalonia.Media.Imaging.Bitmap, Avalonia.Controls.TabItem)>();
        private Avalonia.Controls.MenuItem _miResourceList;
        private Avalonia.Controls.MenuItem _miResourceTree;
        private Avalonia.Controls.MenuItem _miObjectWorkshop;
        private Avalonia.Controls.MenuItem _miFilterResources;

        /// <summary>
        /// Builds the Window-menu items to match the original SimPE layout.
        ///
        /// TOP SECTION — navigation panels (replaced the icon toolbar):
        ///   Resource List · Resource Tree · Resource Actions · Plugin View
        ///
        /// BOTTOM SECTION — content / editor panels:
        ///   Filter Resources · [dock-derived bottom tabs] · Object Workshop
        ///
        /// Must be called after SetupMainForm (_bottomTabDefs already populated).
        /// </summary>
        void AddTabMenus()
        {
            // ── TOP SECTION ─────────────────────────────────────────────────
            AddNavItem("Resource List",
                "Main_dcResourceList.TabImage.png",
                () =>
                {
                    lv.IsVisible = !lv.IsVisible;
                    if (_miResourceList != null) _miResourceList.IsChecked = lv.IsVisible;
                },
                out _miResourceList, isChecked: true);

            AddNavItem("Resource Tree",
                "Main_dcResource.TabImage.png",
                () =>
                {
                    tv.IsVisible = !tv.IsVisible;
                    if (_miResourceTree != null) _miResourceTree.IsChecked = tv.IsVisible;
                },
                out _miResourceTree, isChecked: true);

            // Resource Actions — present in original toolbar; not yet implemented on Mac.
            var miActions = new Avalonia.Controls.MenuItem
            {
                Header    = "Resource Actions",
                IsChecked = false,
                IsEnabled = false,
            };
            var actIcon = SimPe.LoadIcon.LoadAvaloniaBitmap("Main_dcAction.TabImage.png");
            if (actIcon != null)
                miActions.Icon = new Avalonia.Controls.Image { Source = actIcon, Width = 16, Height = 16 };
            avlnWindow.Items.Add(miActions);

            // Plugin View — the first entry in _bottomTabDefs.
            var pvDef = _bottomTabDefs.Count > 0 ? _bottomTabDefs[0] : default;
            if (pvDef.Tab != null)
                avlnWindow.Items.Add(MakeTabMenuItem(pvDef.Label, pvDef.Icon, pvDef.Tab));

            // ── BOTTOM SECTION ───────────────────────────────────────────────
            avlnWindow.Items.Add(new Avalonia.Controls.Separator());

            // Filter Resources — right-side toggle panel.
            _miFilterResources = new Avalonia.Controls.MenuItem
            {
                Header    = "Filter Resources",
                IsChecked = false,
            };
            var filterIcon = SimPe.LoadIcon.LoadAvaloniaBitmap("Main_dcFilter.TabImage.png");
            if (filterIcon != null)
                _miFilterResources.Icon = new Avalonia.Controls.Image { Source = filterIcon, Width = 16, Height = 16 };
            _miFilterResources.Click += (_, _) => ActivateRightPanel("Filter Resources");
            avlnWindow.Items.Add(_miFilterResources);

            // All dock-derived bottom tabs (skip Plugin View at index 0 — already in top section).
            for (int i = 1; i < _bottomTabDefs.Count; i++)
            {
                var (label, icon, tab) = _bottomTabDefs[i];
                avlnWindow.Items.Add(MakeTabMenuItem(label, icon, tab));
            }

            // Object Workshop — right-side toggle panel.
            _miObjectWorkshop = new Avalonia.Controls.MenuItem
            {
                Header    = "Object Workshop",
                IsChecked = true,
            };
            var owIcon = SimPe.LoadIcon.LoadAvaloniaBitmap("OWDockForm_dcObjectWorkshop.TabImage.png");
            if (owIcon != null)
                _miObjectWorkshop.Icon = new Avalonia.Controls.Image { Source = owIcon, Width = 16, Height = 16 };
            _miObjectWorkshop.Click += (_, _) => ActivateRightPanel("Object Workshop");
            avlnWindow.Items.Add(_miObjectWorkshop);
        }

        /// <summary>Adds one navigation-panel item to the top section of avlnWindow.</summary>
        void AddNavItem(string label, string iconFile, System.Action onClick,
                        out Avalonia.Controls.MenuItem miOut, bool isChecked)
        {
            var mi = new Avalonia.Controls.MenuItem { Header = label, IsChecked = isChecked };
            var icon = SimPe.LoadIcon.LoadAvaloniaBitmap(iconFile);
            if (icon != null)
                mi.Icon = new Avalonia.Controls.Image { Source = icon, Width = 16, Height = 16 };
            mi.Click += (_, _) => onClick();
            avlnWindow.Items.Add(mi);
            miOut = mi;
        }

        /// <summary>Creates a checked menu item that toggles a bottom tab in/out of the strip.</summary>
        Avalonia.Controls.MenuItem MakeTabMenuItem(string label, Avalonia.Media.Imaging.Bitmap icon,
                                                   Avalonia.Controls.TabItem tab)
        {
            // Use the full Window-menu name if we have one; fall back to the raw tab label.
            string menuLabel = _tabMenuNames.TryGetValue(label, out var mapped) ? mapped : label;
            var mi = new Avalonia.Controls.MenuItem { Header = menuLabel, IsChecked = true };
            if (icon != null)
                mi.Icon = new Avalonia.Controls.Image { Source = icon, Width = 16, Height = 16 };

            mi.Click += (_, _) =>
            {
                if (bottomViewTabs.Items.Contains(tab))
                {
                    bottomViewTabs.Items.Remove(tab);
                    mi.IsChecked = false;
                }
                else
                {
                    // Re-insert at original relative position.
                    int origIdx = _bottomTabDefs.FindIndex(d => d.Tab == tab);
                    int insertAt = 0;
                    for (int i = 0; i < origIdx; i++)
                        if (bottomViewTabs.Items.Contains(_bottomTabDefs[i].Tab))
                            insertAt++;
                    if (insertAt >= bottomViewTabs.Items.Count)
                        bottomViewTabs.Items.Add(tab);
                    else
                        bottomViewTabs.Items.Insert(insertAt, tab);
                    mi.IsChecked = true;
                }
            };
            return mi;
        }

        /// <summary>
        /// Switches the right-side panel and keeps toggle buttons + Window-menu items in sync.
        /// </summary>
        internal void ActivateRightPanel(string which)
        {
            bool showOW = which == "Object Workshop";
            tabBtnObjectWorkshop.IsChecked  = showOW;
            tabBtnFilterResources.IsChecked = !showOW;
            pnlObjectWorkshop.IsVisible     = showOW;
            pnlFilterResources.IsVisible    = !showOW;
            lblRightPanelTitle.Text         = which;
            if (_miObjectWorkshop  != null) _miObjectWorkshop.IsChecked  = showOW;
            if (_miFilterResources != null) _miFilterResources.IsChecked = !showOW;
        }

        /// <summary>
        /// Add one Dock to the List
        /// </summary>
        /// <param name="c"></param>
        /// <param name="first"></param>
        void AddDockItem(Ambertation.Windows.Forms.DockPanel c, bool first)
        {
            ToolStripMenuItem mi = new ToolStripMenuItem(c.Text);
            if (first) miWindow.DropDownItems.Add("-");
            mi.Image = c.TabImage;            

            mi.Click += new EventHandler(Activate_miWindowDocks);
            mi.Tag = c;
            mi.Checked = c.IsOpen;

            // Shortcut key registration will be rewired to Avalonia key bindings in a future pass.

            /*c.VisibleChanged += new EventHandler(CloseDockControl);
            c.Closed += new EventHandler(CloseDockControl);*/
            c.OpenedStateChanged += new EventHandler(CloseDockControl);
            c.Tag = mi;

            miWindow.DropDownItems.Add(mi);
        }

        /// <summary>
        /// this will create all needed Dock MenuItems to Display a hidden Dock
        /// </summary>
        void AddDockMenus()
        {
            System.Collections.Generic.List<Ambertation.Windows.Forms.DockPanel> ctrls = manager.GetPanels();

            bool first = true;
            foreach (Ambertation.Windows.Forms.DockPanel c in ctrls)
            {
                if (c.Tag != null) continue;
                //System.Diagnostics.Debug.WriteLine("##1# "+c.ButtonText);
                AddDockItem(c, first);
                first = false;
            }

            first = true;
            foreach (Ambertation.Windows.Forms.DockPanel c in ctrls)
            {
                if (c.Tag == null) continue;
                if (c.Tag is ToolStripMenuItem) continue;
                //System.Diagnostics.Debug.WriteLine("##2# " + c.ButtonText);
                AddDockItem(c, first);
                first = false;
            }
        }

        /// <summary>
        /// this will update the Checked State of a Dock menu Item
        /// </summary>
        void UpdateDockMenus()
        {
            foreach (object o in miWindow.DropDownItems)
            {
                ToolStripMenuItem mi = o as ToolStripMenuItem;
                if (mi == null) continue;
                if (mi.Tag is Ambertation.Windows.Forms.DockPanel)
                {
                    Ambertation.Windows.Forms.DockPanel c = (Ambertation.Windows.Forms.DockPanel)mi.Tag;
                    mi.Checked = c.IsDocked || c.IsFloating;
                }
            }
        }

        /// <summary>
        /// Called when a close Event was sent to a DockControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseDockControl(object sender, EventArgs e)
        {
            if (sender is Ambertation.Windows.Forms.DockPanel)
            {
                Ambertation.Windows.Forms.DockPanel c = (Ambertation.Windows.Forms.DockPanel)sender;
                if (c.Tag is ToolStripMenuItem)
                {
                    ToolStripMenuItem mi = (ToolStripMenuItem)c.Tag;
                    mi.Checked = c.IsOpen;
                }
            }
        }

        /// <summary>
        /// Called when a MenuItem that represents a Dock is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Activate_miWindowDocks(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem mi = (ToolStripMenuItem)sender;

                if (mi.Tag is Ambertation.Windows.Forms.DockPanel)
                {
                    Ambertation.Windows.Forms.DockPanel c = (Ambertation.Windows.Forms.DockPanel)mi.Tag;
                    if (mi.Checked)
                    {
                        c.Close();
                        mi.Checked = c.IsOpen;
                    }
                    else
                    {
                        c.Open();
                        mi.Checked = c.IsOpen;
                        plugger.ChangedGuiResourceEventHandler();
                    }
                }
            }
        }

        /// <summary>
        /// Called when we need to set up the MenuItems (checked state)
        /// </summary>
        void InitMenuItems()
        {
            this.miMetaInfo.Checked = !Helper.XmlRegistry.LoadMetaInfo;
            this.miFileNames.Checked = Helper.XmlRegistry.DecodeFilenamesState;

            AddDockMenus();
            AddTabMenus();
            UpdateMenuItems();

            tbAction.Visible = true;
            tbTools.Visible = true;
            tbWindow.Visible = false;

            ArrayList exclude = new ArrayList();
            exclude.Add(this.miNewDc);
            SimPe.LoadFileWrappersExt.BuildToolBar(tbWindow, miWindow.DropDownItems, exclude);            
        }

        bool createdmenus;
        /// <summary>
        /// Called whenever we need to set the enabled state of a MenuItem
        /// </summary>
        void UpdateMenuItems()
        {
            this.miSave.Enabled = System.IO.File.Exists(package.FileName);
            this.miSaveCopyAs.Enabled = this.miSave.Enabled;
            this.miSaveAs.Enabled = package.Loaded;
            this.miClose.Enabled = package.Loaded;
            this.miShowName.Enabled = package.Loaded;

            if (!createdmenus)
            {
                foreach (ExpansionItem ei in PathProvider.Global.Expansions)
                {
                    if (ei.Flag.Class != ExpansionItem.Classes.BaseGame)
                    {
                        ToolStripMenuItem mi = new ToolStripMenuItem();
                        //mi.Text = SimPe.Localization.GetString("OpenInCaption").Replace("{where}", ei.Expansion.ToString());
                        mi.Text = SimPe.Localization.GetString("OpenInCaption").Replace("{where}", ei.NameShort);
                        mi.Tag = ei;
                        mi.Click += new EventHandler(this.Activate_miOpenInEp);
                        mi.Enabled = ei.Exists;

                        this.miOpenIn.DropDownItems.Insert(miOpenIn.DropDownItems.Count - 1, mi);
                    }
                }
                createdmenus = true;
            }
        }

        /// <summary>
        /// Allows the user to change the Sims 2 Game Root location.
        /// </summary>
        private void miGameRoot_Click(object sender, EventArgs e)
        {
            using (var dlg = new GameRootDialog())
            {
                // Optional future enhancement:
                // If Helper.GameRootPath is already set,
                // you can prefill the dialog here.

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // Values have already been saved inside GameRootDialog:
                    // Helper.GameRootPath
                    // Helper.GameEdition
                    // Helper.SaveGameRootToFile(...)

                    // (Optional future step: reload FileTable or anything dependent on game root)
                    try
                    {
                        // We just changed paths: rebuild the global file index for this run
                        Helper.LocalMode = false;

                        // Clear any cached index first if your FileIndex supports it
                        // SimPe.FileTable.FileIndex.Clear();

                        SimPe.FileTable.FileIndex.Load();   // or Reload()

                        // Now refresh whatever UI depends on it
                        // e.g. refresh catalog, resource list, plugins, etc.
                        // RefreshCatalog();
                        // ReloadCurrentPackageView();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Failed to reload FileTable/FileIndex");
                    }
                }
            }
        }

    }
}
