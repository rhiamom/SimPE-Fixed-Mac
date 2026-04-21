/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   peter@users.sf.net                                                    *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSweatShop                                 *
 *   Rhiamom@mac.com                                                       *
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
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using Avalonia.Controls;
using SimPe.Scenegraph.Compat;
using SimPe.Plugin;
using DialogResult = SimPe.DialogResult;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace pjse
{
    /// <summary>
    /// Summary description for ResourceChooser.
    /// </summary>
    public class ResourceChooser : Window
    {
        #region Form variables

        private ButtonCompat OK;
        private ButtonCompat Cancel;
        private TabControlCompat tcResources;
        private Avalonia.Controls.TabItem tpBuiltIn;
        private Avalonia.Controls.TabItem tpGlobalGroup;
        private Avalonia.Controls.TabItem tpSemiGroup;
        private Avalonia.Controls.TabItem tpGroup;
        private Avalonia.Controls.TabItem tpPackage;
        private ListView lvPackage;
        private ListView lvGlobal;
        private ListView lvGroup;
        private ListView lvSemi;
        private ListView lvPrim;
        private ButtonCompat btnViewBHAV;
        #endregion

        private bool _dialogResult = false;

        public ResourceChooser()
        {
            InitializeComponent();
        }

        public void Dispose() { }


        #region ResourceChooser

        const string BASENAME = "PJSE\\Bhav";
        private static int ChooserOrder
        {
            get
            {
                SimPe.XmlRegistryKey rkf = SimPe.Helper.XmlRegistry.PluginRegistryKey.CreateSubKey(BASENAME);
                object o = rkf.GetValue("chooserOrder", 0);
                return (int)Math.Max(Convert.ToUInt32(o), 1);
            }

            set
            {
                SimPe.XmlRegistryKey rkf = SimPe.Helper.XmlRegistry.PluginRegistryKey.CreateSubKey(BASENAME);
                rkf.SetValue("chooserOrder", value);
            }
        }

        private static Size ChooserSize
        {
            get
            {
                SimPe.XmlRegistryKey rkf = SimPe.Helper.XmlRegistry.PluginRegistryKey.CreateSubKey(BASENAME);
                ResourceChooser rc = new ResourceChooser();
                object w = rkf.GetValue("chooserSize.Width", 500);
                object h = rkf.GetValue("chooserSize.Height", 400);
                return new Size(Convert.ToInt32(w), Convert.ToInt32(h));
            }

            set
            {
                SimPe.XmlRegistryKey rkf = SimPe.Helper.XmlRegistry.PluginRegistryKey.CreateSubKey(BASENAME);
                rkf.SetValue("chooserSize.Width", value.Width);
                rkf.SetValue("chooserSize.Height", value.Height);
            }
        }

        private class ListViewItemComparer : IComparer
        {
            private int col;
            public ListViewItemComparer() { col = ChooserOrder; }
            public ListViewItemComparer(int column) { col = column; }
            public int Compare(object x, object y) { return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text); }
        }

        private bool CanDoEA;

        public static int PersistentTab
        {
            get
            {
                SimPe.XmlRegistryKey rkf = SimPe.Helper.XmlRegistry.PluginRegistryKey.CreateSubKey(BASENAME);
                object o = rkf.GetValue("rcPersistentTab", false);
                return Convert.ToInt32(o);
            }

            set
            {
                SimPe.XmlRegistryKey rkf = SimPe.Helper.XmlRegistry.PluginRegistryKey.CreateSubKey(BASENAME);
                rkf.SetValue("rcPersistentTab", value);
            }

        }

        private ListView getListView()
        {
            if (this.tcResources.SelectedItem == this.tpPackage && lvPackage.SelectedItems != null)
                return lvPackage;

            if (this.tcResources.SelectedItem == this.tpGroup && lvGroup.SelectedItems != null)
                return lvGroup;

            if (this.tcResources.SelectedItem == this.tpSemiGroup && lvSemi.SelectedItems != null)
                return lvSemi;

            if (this.tcResources.SelectedItem == this.tpGlobalGroup && lvGlobal.SelectedItems != null)
                return lvGlobal;

            if (this.tcResources.SelectedItem == this.tpBuiltIn && lvPrim.SelectedItems != null)
                return lvPrim;

            return null;
        }

        /// <summary>
        /// List available resources of a given type, allowing the user to select one.
        /// </summary>
        public pjse.FileTable.Entry Execute(uint resourceType, uint group, Avalonia.Controls.Control form, bool canDoEA)
        {
            return Execute(resourceType, group, form, canDoEA, 0);
        }

        /// <summary>
        /// List available resources of a given type, allowing the user to select one.
        /// </summary>
        public pjse.FileTable.Entry Execute(uint resourceType, uint group, Avalonia.Controls.Control form, bool canDoEA, Boolset skip_pages)
        {
            CanDoEA = canDoEA;


            List<Avalonia.Controls.TabItem> ltp = new List<Avalonia.Controls.TabItem>(new Avalonia.Controls.TabItem[] { tpPackage, tpGroup, tpSemiGroup, tpGlobalGroup, tpBuiltIn });

            btnViewBHAV.IsVisible = resourceType == SimPe.Data.MetaData.BHAV_FILE;

            this.tcResources.Items.Clear();

            if (!skip_pages[0]
                && pjse.FileTable.GFT.CurrentPackage != null
                && pjse.FileTable.GFT.CurrentPackage.FileName != null
                && !pjse.FileTable.GFT.CurrentPackage.FileName.ToLower().EndsWith("objects.package"))
                FillPackage(resourceType, this.lvPackage, this.tpPackage);

            if (!skip_pages[1])
                FillGroup(resourceType, group, this.lvGroup, this.tpGroup);

            if (!skip_pages[2])
            {
                Glob g = pjse.BhavWiz.GlobByGroup(group);
                if (g != null)
                {
                    FillGroup(resourceType, g.SemiGlobalGroup, this.lvSemi, this.tpSemiGroup);
                    this.tpSemiGroup.Header = g.SemiGlobalName;
                }
            }

            if (!skip_pages[3] && group != (uint)Group.Global)
                FillGroup(resourceType, (uint)Group.Global, this.lvGlobal, this.tpGlobalGroup);

            if (!skip_pages[4] && resourceType == SimPe.Data.MetaData.BHAV_FILE)
                FillBuiltIn(resourceType, this.lvPrim, this.tpBuiltIn);

            if (this.tcResources.Items.Count > 0)
            {
                int persistIdx = PersistentTab;
                if (persistIdx >= 0 && persistIdx < ltp.Count && this.tcResources.Items.Contains(ltp[persistIdx]))
                    tcResources.SelectedItem = ltp[persistIdx];
                else
                    this.tcResources.SelectedIndex = 0;
            }


            this.ShowDialog(null).GetAwaiter().GetResult();

            PersistentTab = ltp.IndexOf(this.tcResources.SelectedItem as Avalonia.Controls.TabItem);
            Close();

            if (_dialogResult)
            {
                ListView lv = getListView();

                if (lv != null)
                {
                    if (lv != lvPrim)
                        return (pjse.FileTable.Entry)lv.SelectedItems[0].Tag;
                    else
                    {
                        IPackedFileDescriptor pfd = new SimPe.Packages.PackedFileDescriptor();
                        pfd.Instance = (uint)lvPrim.SelectedItems[0].Tag;
                        return new pjse.FileTable.Entry(null, pfd, true, true);
                    }
                }
            }
            return null;
        }

        private void FillPackage(uint type, ListView list, Avalonia.Controls.TabItem tab)
        {
            Fill(pjse.FileTable.GFT[pjse.FileTable.GFT.CurrentPackage, type], list, tab);
        }

        private void FillGroup(uint type, uint group, ListView list, Avalonia.Controls.TabItem tab)
        {
            Fill(pjse.FileTable.GFT[type, group], list, tab);
        }

        private void Fill(pjse.FileTable.Entry[] items, ListView list, Avalonia.Controls.TabItem tab)
        {
            list.Items.Clear();
            ListViewItem lvi;

            foreach (pjse.FileTable.Entry item in items)
            {
                lvi = new ListViewItem(new string[] { "0x" + SimPe.Helper.HexString((ushort)item.Instance), item });
                lvi.Tag = item;
                list.Items.Add(lvi);
            }
            this.tcResources.Items.Add(tab);
            list.ListViewItemSorter = new ListViewItemComparer();
            if (list.Items.Count > 0)
                list.SelectedIndices.Add(0);
        }

        private void FillBuiltIn(uint type, ListView list, Avalonia.Controls.TabItem tab)
        {
            list.Items.Clear();
            ListViewItem lvi;

            uint i = 0;
            foreach (string s in BhavWiz.readStr(pjse.GS.BhavStr.Primitives))
            {
                if (!s.StartsWith("~"))
                {
                    lvi = new ListViewItem(new string[] { "0x" + SimPe.Helper.HexString((ushort)i), s });
                    lvi.Tag = i;
                    list.Items.Add(lvi);
                }
                i++;
            }

            this.tcResources.Items.Add(tab);
            list.ListViewItemSorter = new ListViewItemComparer();
            if (list.Items.Count > 0)
                list.SelectedIndices.Add(0);
        }

        #endregion

        #region InitializeComponent
        private void InitializeComponent()
        {
            this.tcResources = new TabControlCompat();
            this.tpPackage = new Avalonia.Controls.TabItem { Header = "This Package" };
            this.tpGlobalGroup = new Avalonia.Controls.TabItem { Header = "Global Group" };
            this.tpGroup = new Avalonia.Controls.TabItem { Header = "Group" };
            this.tpSemiGroup = new Avalonia.Controls.TabItem { Header = "Semi-Global" };
            this.tpBuiltIn = new Avalonia.Controls.TabItem { Header = "Built In" };
            this.lvPackage = new ListView();
            this.lvGlobal = new ListView();
            this.lvGroup = new ListView();
            this.lvSemi = new ListView();
            this.lvPrim = new ListView();
            this.OK = new ButtonCompat { Content = "OK" };
            this.Cancel = new ButtonCompat { Content = "Cancel" };
            this.btnViewBHAV = new ButtonCompat { Content = "View BHAV" };

            this.lvPackage.DoubleClick += (s, e) => this.listView_DoubleClick(s, e);
            this.lvPackage.ColumnClick += (s, e) => this.listView_ColumnClick(s, e as ColumnClickEventArgs ?? new ColumnClickEventArgs(0));
            this.lvGlobal.DoubleClick += (s, e) => this.listView_DoubleClick(s, e);
            this.lvGlobal.ColumnClick += (s, e) => this.listView_ColumnClick(s, e as ColumnClickEventArgs ?? new ColumnClickEventArgs(0));
            this.lvGroup.DoubleClick += (s, e) => this.listView_DoubleClick(s, e);
            this.lvGroup.ColumnClick += (s, e) => this.listView_ColumnClick(s, e as ColumnClickEventArgs ?? new ColumnClickEventArgs(0));
            this.lvSemi.DoubleClick += (s, e) => this.listView_DoubleClick(s, e);
            this.lvSemi.ColumnClick += (s, e) => this.listView_ColumnClick(s, e as ColumnClickEventArgs ?? new ColumnClickEventArgs(0));
            this.lvPrim.DoubleClick += (s, e) => this.listView_DoubleClick(s, e);
            this.lvPrim.ColumnClick += (s, e) => this.listView_ColumnClick(s, e as ColumnClickEventArgs ?? new ColumnClickEventArgs(0));

            this.tpPackage.Content = this.lvPackage;
            this.tpGlobalGroup.Content = this.lvGlobal;
            this.tpGroup.Content = this.lvGroup;
            this.tpSemiGroup.Content = this.lvSemi;
            this.tpBuiltIn.Content = this.lvPrim;

            this.tcResources.SelectionChanged += (s, e) => this.tcResources_SelectedIndexChanged(s, e);

            this.OK.Click += (s, e) => { _dialogResult = true; Close(); };
            this.Cancel.Click += (s, e) => { _dialogResult = false; Close(); };
            this.btnViewBHAV.Click += (s, e) => this.btnViewBHAV_Click(s, e);

            var buttonPanel = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
            buttonPanel.Children.Add(this.btnViewBHAV);
            buttonPanel.Children.Add(this.Cancel);
            buttonPanel.Children.Add(this.OK);

            var mainPanel = new StackPanel();
            mainPanel.Children.Add(this.tcResources);
            mainPanel.Children.Add(buttonPanel);
            this.Content = mainPanel;
            this.Name = "ResourceChooser";
        }
        #endregion

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ChooserOrder = e.Column;
            foreach (Avalonia.Controls.TabItem tp in tcResources.Items)
                if (tp.Content is ListView lv)
                    lv.ListViewItemSorter = new ListViewItemComparer(e.Column);
        }

        private void tcResources_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (btnViewBHAV.IsVisible)
                btnViewBHAV.IsEnabled = tcResources.SelectedItem != this.tpBuiltIn;
        }

        private void listView_DoubleClick(object sender, System.EventArgs e)
        {
            _dialogResult = true;
            OK_Click(sender, e);
            Close();
        }

        private void OK_Click(object sender, EventArgs ev)
        {
            ListView lv = getListView();

            if (lv != null && lv != lvPrim)
            {
                pjse.FileTable.Entry e = (pjse.FileTable.Entry)lv.SelectedItems[0].Tag;

                if (CanDoEA && e.Group != 0xffffff && !e.IsFixed)
                    foreach (pjse.FileTable.Entry f in pjse.FileTable.GFT[e.Type, e.Group, e.Instance, FileTable.Source.Fixed])
                        if (f.IsFixed)
                        {
                            var dr = SimPe.Scenegraph.Compat.MessageBox.ShowAsync(
                                Localization.GetString("rc_override", e.Package.FileName),
                                Localization.GetString("rc_overridesEA"),
                                MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Question
                            ).GetAwaiter().GetResult();

                            if (dr == DialogResult.Yes) { }
                            else if (dr == DialogResult.No) { lv.SelectedItems[0].Tag = f; }
                            else { _dialogResult = false; }
                            break;
                        }
            }
        }

        private void btnViewBHAV_Click(object sender, EventArgs e)
        {
            ListView lv = getListView();
            if (lv == null) return;

            pjse.FileTable.Entry item = (pjse.FileTable.Entry)lv.SelectedItems[0].Tag;
            Bhav b = new Bhav();
            b.ProcessData(item.PFD, item.Package);

            SimPe.PackedFiles.UserInterface.BhavForm ui = (SimPe.PackedFiles.UserInterface.BhavForm)b.UIHandler;
            ui.Tag = "Popup";
            string bhavTitle = pjse.Localization.GetString("viewbhav") + ": " + b.FileName + " [" + b.Package.SaveFileName + "]";
            b.RefreshUI();
            new Avalonia.Controls.Window { Title = bhavTitle, Content = ui }.Show();
        }

    }

}
