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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ambertation.Windows.Forms;
using Compat = SimPe.Scenegraph.Compat;
using System.Media;


namespace SimPe.Plugin.Tool.Dockable
{
    /// <summary>
    /// Summary description for DockableWindow1.
    /// </summary>
    public partial class FinderDock : Ambertation.Windows.Forms.DockPanel, SimPe.Interfaces.IDockableTool, SimPe.Interfaces.IFinderResultGui
    {

        ThemeManager tm;
        SimPe.ColumnSorter sorter;

        System.Collections.Generic.List<string> packages;
        System.Threading.Thread[] threads;
        private Avalonia.Controls.StackPanel pnContainer;
        SimPe.Interfaces.AFinderTool searchtool;
        int runningthreads;
        public FinderDock()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // Designer.cs is excluded from compilation; set identity strings here (like DebugDock does).
            this.TabText     = "Finder";
            this.ButtonText  = "Finder";
            this.CaptionText = "Scenegraph Resource Finder";
            this.TabIconBitmap = SimPe.LoadIcon.LoadAvaloniaBitmap("FinderDockTabImage.png");

            tm = ThemeManager.Global.CreateChild();
            tm.AddControl(this.xpGradientPanel1);
            tm.AddControl(this.tbResult);
            tm.AddControl(this.toolBar1);
            // toolBar1.ImageScalingSize = new System.Drawing.Size(32, 32); // n/a in Avalonia

            sorter = new ColumnSorter();
            sorter.CurrentColumn = 0;
            lv.ListViewItemSorter = sorter;

            lv.View = SimPe.Scenegraph.Compat.View.Details;

            packages = new System.Collections.Generic.List<string>();
            threads = new System.Threading.Thread[Helper.XmlRegistry.SortProcessCount / 2];

            runningthreads = 0;
            CreateThreads(false);

            foreach (SimPe.Interfaces.AFinderTool tl in Finder.FinderToolRegistry.Global.CreateToolInstances(this))
            {
                this.cbTask.Items.Add(tl);
            }
            if (cbTask.Items.Count > 0) this.cbTask.SelectedIndex = 0;
        }

        private void CreateThreads(bool start)
        {
            for (int ct = 0; ct < threads.Length; ct++)
            {
                threads[ct] = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadRunner));
                threads[ct].Name = "Search Thread " + (ct);
                if (start) threads[ct].Start();
            }
        }

        public Ambertation.Windows.Forms.DockPanel GetDockableControl()
        {
            return this;
        }

       public event SimPe.Events.ChangedResourceEvent ShowNewResource;

        public void RefreshDock(object sender, SimPe.Events.ResourceEventArgs es)
        {
            //code here	
        }

        #region IToolPlugin Member

        public override string ToString()
        {
            return this.Text;
        }

        #endregion

        private void cbTask_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            pnContainer?.Children.Clear();
            if (cbTask.SelectedItem == null) return;

            Avalonia.Controls.Control c = ((SimPe.Interfaces.AFinderTool)cbTask.SelectedItem).SearchGui;
            pnContainer?.Children.Add(c);
            c.IsVisible = true;
        }

        public void ClearResults()
        {
            lv.DoubleBuffering = false;
            lv.Items.Clear();
            lv.ShowGroups = false;
            lv.Groups.Clear();
            lv.TileColumns = new int[0];
            lv.Columns.Clear();
        }

        protected void CreateDefaultColumns()
        {
            ArrayList a = new ArrayList();
            a.AddRange(new string[] { "Resourcename", "Type", "Group", "Instance", "Offset", "Size", "Filename" });
            ArrayList b = new ArrayList();
            b.AddRange(new int[] { 200, 90, 90, 140, 90, 90, 600 });
            CreateColums(a, b);
        }

        protected void CreateColums(System.Collections.ArrayList strings, System.Collections.ArrayList widths)
        {
            for (int i = 0; i < strings.Count; i++)
            {

                Compat.ColumnHeader ch = new Compat.ColumnHeader();
                ch.Text = (string)strings[i];
                ch.Width = (int)widths[i];
                lv.Columns.Add(ch);
            }
        }
        
        protected int AddResultGroup(string name)
        {
            string cname = name.Trim().ToLower();
            foreach (SimPe.Scenegraph.Compat.ListViewGroup lvg in lv.Groups)
                if (lvg.Header.Trim().ToLower() == cname)
                    return lv.Groups.IndexOf(lvg);

            SimPe.Scenegraph.Compat.ListViewGroup g = new SimPe.Scenegraph.Compat.ListViewGroup(name);
            int idx = lv.Groups.Count;
            lv.Groups.Add(g);
            return idx;
        }
        
        private void lv_DoubleClick(object sender, System.EventArgs e)
        {
            if (lv.SelectedItems.Count!=1) return;

            IFinderResultItem fri = (IFinderResultItem)lv.SelectedItems[0];
            fri.OpenResource();
        }

        private void Activate_biList(object sender, System.EventArgs e)
        {
            lv.View = SimPe.Scenegraph.Compat.View.List;
            biList.IsChecked = true;
            biTile.IsChecked = false;
            biDetail.IsChecked = false;
        }

        private void Activate_biTile(object sender, System.EventArgs e)
        {
            lv.View = SimPe.Scenegraph.Compat.View.Tile;
            biList.IsChecked = false;
            biTile.IsChecked = true;
            biDetail.IsChecked = false;
        }

        private void Activate_biDetails(object sender, System.EventArgs e)
        {
            lv.View = SimPe.Scenegraph.Compat.View.Details;
            biList.IsChecked = false;
            biTile.IsChecked = false;
            biDetail.IsChecked = true;
        }

        private void lv_ColumnClick(object sender, System.EventArgs e)
        {
            // TODO: column click sorting not available in Avalonia port
        }                

        private void lv_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        #region IToolExt Member

        public int Shortcut
        {
            get
            {
                return 0;
            }
        }

        public System.Drawing.Image Icon
        {
            get
            {
                return this.TabImage;
            }
        }

        public new bool Visible
        {
            get { return this.IsDocked || this.IsFloating; }
        }

        #endregion

        #region IFinderResultGui Members
        bool truncated;
        bool forcestop;
        public bool ForcedStop
        {
            get { return forcestop; }
            set { if (value) StopSearch(); }
        }
        public void AddResult(SimPe.Interfaces.Files.IPackageFile pkg, SimPe.Interfaces.Files.IPackedFileDescriptor pfd)
        {
            AddResult(null, pkg, pfd);
        }
        delegate void InvokeAddResult(string group, SimPe.Interfaces.Files.IPackageFile pkg, SimPe.Interfaces.Files.IPackedFileDescriptor pfd);
        public void AddResult(string group, SimPe.Interfaces.Files.IPackageFile pkg, SimPe.Interfaces.Files.IPackedFileDescriptor pfd)
        {
            if (this.InvokeRequired) this.BeginInvoke(new InvokeAddResult(InvokedAddResult), new object[] {group, pkg, pfd });
            else InvokedAddResult(group, pkg, pfd);
        }

        protected void InvokedAddResult(string group, SimPe.Interfaces.Files.IPackageFile pkg, SimPe.Interfaces.Files.IPackedFileDescriptor pfd)
        {
            lock (lv)
            {
                if (lv.Items.Count > Helper.XmlRegistry.MaxSearchResults)
                {
                    truncated = true;
                    return;
                }

                ScenegraphResultItem sri = new ScenegraphResultItem(pkg, pfd);
                if (group == null) sri.GroupIndex = AddResultGroup(pkg.SaveFileName);
                else sri.GroupIndex = this.AddResultGroup(group);

                lv.Items.Add(sri);
            }
        }

        protected void SetPackageList()
        {
            FileTable.FileIndex.Load();
            packages.Clear();
            truncated = false;
            pnErr.IsVisible = false;

            foreach (FileTableItem fti in SimPe.FileTable.FileIndex.BaseFolders)
            {
                if (fti.Use)
                {
                    string name = fti.Name;
                    if (fti.IsFile) AddToPackageList(name);
                    else
                    {
                        string[] files = System.IO.Directory.GetFiles(name, "*.package");
                        foreach (string s in files)
                            AddToPackageList(s);
                    }
                }
            }
        }

        void AddToPackageList(string fl)
        {
            if (!packages.Contains(Helper.CompareableFileName(fl)))
                packages.Add(Helper.CompareableFileName(fl));
        }

        public void StartSearch(SimPe.Interfaces.AFinderTool sender)
        {
            StopSearch();
            lock (packages)
            {
                SetPackageList();
                Wait.Start(packages.Count+1);
                
                searchtool = sender;
                forcestop = false;
                
                ClearResults();
                lv.BeginUpdate();
                sorter.Sorting = SortOrder.None;
                CreateDefaultColumns();
            }

            if (sender.ProcessParalell)
            {
                CreateThreads(true);
            }
            else
            {
                CreateThreads(false);
                threads[0].Start();
            }
        }

        public bool Searching
        {
            get { return runningthreads > 0; }
        }

        public void StopSearch()
        {
            lock (packages)
            {
                packages.Clear();
                forcestop = true;
            }
            
            bool stopped =  !Searching;
            while (!stopped)
            {
                Wait.Message = "Stopping Search...";
                System.Threading.Thread.CurrentThread.Join(500);

                stopped = !Searching;
            }            
        }
        delegate void InvokeDoneSearching();
        void DoneSearching()
        {
            lv.TileColumns = new int[] { 1, 2, 3, 4, 5 };
            lv.ShowGroups = true;
            sorter.Sorting = SortOrder.Ascending;
            lv.Sort();
            lv.EndUpdate();
            lv.DoubleBuffering = true;

            if (searchtool!=null) searchtool.NotifyFinishedSearch();
            pnErr.Text = pnErr.Text.Replace("{nr}", Helper.XmlRegistry.MaxSearchResults.ToString());
            pnErr.IsVisible = truncated;
            Wait.Stop();

            System.Diagnostics.Debug.WriteLine("Done Searching");
        }

        internal void ThreadRunner()
        {
            lock (threads)
            {
                runningthreads++;
                System.Diagnostics.Debug.WriteLine("Started Search Thread nr " + runningthreads + ".");
            }
            while (true)
            {
                string name = "";
                lock (packages)
                {
                    if (packages.Count == 0 || truncated) break;
                    name = packages[0];
                    packages.RemoveAt(0);
                    Wait.Progress++;
                    Wait.Message = SimPe.Localization.GetString("Searching") + " " + System.IO.Path.GetFileNameWithoutExtension(name);
                }

                if (System.IO.File.Exists(name))
                {
                    SimPe.Packages.File pkg = SimPe.Packages.File.LoadFromFile(name);
                    searchtool.SearchPackage(pkg);
                }
            }

            lock (threads)
            {
                runningthreads--;
                System.Diagnostics.Debug.WriteLine("Finished Search Thread. " + runningthreads + " threads remain active.");
                Wait.Message = "Searching...";
                if (runningthreads == 1)
                    Wait.Progress++;
                if (runningthreads == 0)
                {
                    if (this.InvokeRequired) this.BeginInvoke(new InvokeDoneSearching(DoneSearching));
                    else DoneSearching();
                }
            }
        }
        #endregion

        // OnControlRemoved(ControlEventArgs) is WinForms-specific — not available in the Avalonia port.
    }

    /*internal class FinderThread : Ambertation.Threading.StoppableThread, System.IDisposable
    {
        FinderDock fd;
        internal FinderThread(FinderDock fd)
            : base(true)
        {
            this.fd = fd;
        }
        protected override void StartThread()
        {
            fd.FindByStringMatch();
        }

        public void Execute()
        {
            this.ExecuteThread(System.Threading.ThreadPriority.Normal, "Finder", false);
        }
        #region IDisposable Member

        public override void Dispose()
        {
            fd = null;
        }

        #endregion
    }*/
}
