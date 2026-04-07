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

using SimPe.Events;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SimPe.Packages;


namespace SimPe
{
    partial class MainForm
    {
        private void SetupMainForm()
        {
            SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString("Creating GUI"));
            if (Helper.DebugMode)
            {
                ToolStripButton tbDebug = new ToolStripButton();
                tbDebug.Text = "Debug docks";
                toolBar1.Items.Add(tbDebug);
                tbDebug.Click += new EventHandler(tbDebug_Click);
            }
            manager.Visible = false;
            tbContainer.Visible = false;
            createdmenus = false;
            
            Wait.Bar = this.waitControl1;

            
            package = new LoadedPackage();
            package.BeforeFileLoad += new PackageFileLoadEvent(BeforeFileLoad);
            package.AfterFileLoad += new PackageFileLoadedEvent(AfterFileLoad);
            package.BeforeFileSave += new PackageFileSaveEvent(BeforeFileSave);
            package.AfterFileSave += new PackageFileSavedEvent(AfterFileSave);
            package.IndexChanged += new EventHandler(ChangedActiveIndex);
            //package.AddedResource += new EventHandler(AddedRemovedIndexResource);
            //package.RemovedResource += new EventHandler(AddedRemovedIndexResource);

            SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString("Building View Filter"));
            filter = new ViewFilter();
            SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString("Starting Resource Loader"));
            resloader = new ResourceLoader(dc, package);
            SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString("Enabling RemoteControl"));
            remote = new RemoteHandler(this, package, resloader, miWindow);

            SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString("Loading Plugins..."));
            try
            {
                System.IO.File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pluginlog.txt"),
                    "Before PluginManager\n");

                plugger = new PluginManager(
                    miTools,
                    tbTools,
                    dc,
                    package,
                    tbDefaultAction,
                    miAction,
                    tbExtAction,
                    tbPlugAction,
                    tbAction,
                    dockBottom,
                    this.mbiTopics,
                    lv
                );

                System.IO.File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pluginlog.txt"),
                    "After PluginManager\n");
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pluginlog.txt"),
                    "PluginManager EXCEPTION:\n" + ex + "\n");

                throw; // let it crash so you see the normal crash path too
            }

            //MessageBox.Show("After PluginManager");
            SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString("Loaded Plugins"));
            plugger.ClosedToolPlugin += new ToolMenuItemExt.ExternalToolNotify(ClosedToolPlugin);
            remote.SetPlugger(plugger);

            remote.LoadedResource += new ChangedResourceEvent(rh_LoadedResource);
            
            // TODO: package.UpdateRecentFileMenu(this.miRecent);

            InitTheme();

            dockBottom.Height = (int)((this.Height * 3) / 4);
            this.Title += " (Version " + Helper.SimPeVersion.ProductVersion + ")";



            // Right-side panel toggle button icons.
            imgObjectWorkshopIcon.Source  = SimPe.LoadIcon.LoadAvaloniaBitmap("OWDockForm_dcObjectWorkshop.TabImage.png");
            imgFilterResourcesIcon.Source = SimPe.LoadIcon.LoadAvaloniaBitmap("Main_dcFilter.TabImage.png");

            // Plugin View — first tab; dc is the per-resource editor inner TabControl.
            var pvIcon = SimPe.LoadIcon.LoadAvaloniaBitmap("Main_dcPlugin.TabImage.png");
            var pvTab = new Avalonia.Controls.TabItem
            {
                Header  = MakeTabHeader("Plugin View", pvIcon),
                Content = dc,
            };
            bottomViewTabs.Items.Add(pvTab);
            _bottomTabDefs.Add(("Plugin View", pvIcon, pvTab));

            // One tab per registered dock panel.
            string logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pluginlog.txt");
            System.IO.File.AppendAllText(logPath, $"[TabBuild] dockBottom.Controls.Count = {dockBottom.Controls.Count}\n");
            foreach (object item in dockBottom.Controls)
            {
                if (item is Ambertation.Windows.Forms.DockPanel dp)
                {
                    string label = dp.TabText?.ToString() ?? "";
                    System.IO.File.AppendAllText(logPath,
                        $"[TabBuild] dp={dp.GetType().Name} TabText='{label}' AvaloniaContent={dp.AvaloniaContent?.GetType().Name ?? "null"} Controls.Count={dp.Controls.Count}\n");
                    if (string.IsNullOrWhiteSpace(label)) continue;

                    // Prefer the explicit AvaloniaContent set by the dock constructor;
                    // fall back to scanning Controls for the first Avalonia control.
                    Avalonia.Controls.Control content = dp.AvaloniaContent;
                    if (content == null)
                        foreach (object child in dp.Controls)
                            if (child is Avalonia.Controls.Control ac) { content = ac; break; }
                    System.IO.File.AppendAllText(logPath,
                        $"[TabBuild]   → content={content?.GetType().Name ?? "null"}\n");

                    // Object Workshop goes into the right-side panel, not the bottom tabs.
                    if (dp is SimPe.Plugin.Tool.Dockable.dcObjectWorkshop)
                    {
                        if (content != null)
                            pnlObjectWorkshop.Child = content;
                        continue;
                    }

                    var tab = new Avalonia.Controls.TabItem
                    {
                        Header  = MakeTabHeader(label, dp.TabIconBitmap),
                        Content = (object)content,
                    };
                    bottomViewTabs.Items.Add(tab);
                    _bottomTabDefs.Add((label, dp.TabIconBitmap, tab));
                }
            }

            InitMenuItems();
            this.dcPlugin.Open();
            // TODO: Ambertation.Windows.Forms.ToolStripRuntimeDesigner.Add(tbContainer);
            // TODO: Ambertation.Windows.Forms.ToolStripRuntimeDesigner.LineUpToolBars(tbContainer);
            // TODO: this.menuBar1.ContextMenuStrip = tbContainer.TopToolStripPanel.ContextMenuStrip;

            // TODO: Ambertation.Windows.Forms.Serializer.Global.Register(tbContainer);
            // TODO: Ambertation.Windows.Forms.Serializer.Global.Register(manager);

            this.Closing += (_, _) => saveProfile();

            manager.NoCleanup = false;
            //manager.ForceCleanUp();
            //this.dcResource.BringToFront();
            //this.dcResourceList.BringToFront();
            lv.Filter = filter;

            waitControl1.ShowProgress = false;
            waitControl1.Progress = 0;
            waitControl1.Message = "";
            waitControl1.Visible = Helper.XmlRegistry.ShowWaitBarPermanent;
            // Debug aid � useful when diagnosing game path / FileTable issues
            //System.Diagnostics.Debug.WriteLine("[SetupMainForm] GameRootPath = '" + (Helper.GameRootPath ?? "<null>") + "'");
        }

        void LoadForm(object sender, System.EventArgs e)
        {
            SimPe.Splash.Screen.SetMessage(SimPe.Localization.GetString("Starting Main Form"));

            dcFilter.Collapse(false);

            cbsemig.Items.Add("[Group Filter]");
            cbsemig.Items.Add(new SimPe.Data.SemiGlobalAlias(true, 0x7FD46CD0, "Globals"));
            cbsemig.Items.Add(new SimPe.Data.SemiGlobalAlias(true, 0x7FE59FD0, "Behaviour"));
            foreach (Data.SemiGlobalAlias sga in Data.MetaData.SemiGlobals)
                if (sga.Known) this.cbsemig.Items.Add(sga);
            if (cbsemig.Items.Count > 0) cbsemig.SelectedIndex = 0;

            //System.Diagnostics.Debug.WriteLine("SimPeLayout path = " + SimPe.Helper.DataFolder.SimPeLayout);

            // Bump this constant whenever a code change makes old layout files incompatible.
            const int CurrentLayoutVersion = 1;

            if (!System.IO.File.Exists(SimPe.Helper.DataFolder.SimPeLayout)
                || Helper.XmlRegistry.LayoutVersion < CurrentLayoutVersion)
            {
                ResetLayout(this, null);
                Helper.XmlRegistry.LayoutVersion = CurrentLayoutVersion;
            }
            else
                ReloadLayout();

            //Set the Lock State of the Docks
            MakeFloatable(!Helper.XmlRegistry.LockDocks);

            manager.Visible = true;
            tbContainer.Visible = true;

            SimPe.Splash.Screen.Stop();

            this.Opened += MainForm_FirstShown;

            // Welcome window is non-functional on Mac (RTF content not ported); skip for now.
            if (Helper.XmlRegistry.ShowWelcomeOnStartup &&
                System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(
                    System.Runtime.InteropServices.OSPlatform.Windows))
                About.ShowWelcome();

            //if (Helper.XmlRegistry.CheckForUpdates)
                //About.ShowUpdate();
        }

        /// <summary>Builds an icon + label StackPanel for use as a TabItem Header.</summary>
        static Avalonia.Controls.Control MakeTabHeader(string label, Avalonia.Media.Imaging.Bitmap icon)
        {
            var sp = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 3,
            };
            if (icon != null)
                sp.Children.Add(new Avalonia.Controls.Image
                {
                    Source = icon,
                    Width = 16,
                    Height = 16,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                });
            sp.Children.Add(new Avalonia.Controls.TextBlock
            {
                Text = label,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            });
            return sp;
        }
    }
}
