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

#nullable enable
#pragma warning disable CS8603, CS8618, CS8622, CS8625, CS8601, CS8600, CS8602, CS8604
namespace SimPe.Windows.Forms
{
    public partial class ResourceTreeViewExt : Avalonia.Controls.UserControl
    {
        ResourceTreeNodesByType typebuilder;
        ResourceTreeNodesByGroup groupbuilder;
        ResourceTreeNodesByInstance instbuilder;
        ResourceViewManager manager;
        IResourceTreeNodeBuilder builder;

        Avalonia.Controls.TreeView tv;
        Avalonia.Controls.Primitives.ToggleButton tbInst;
        Avalonia.Controls.Primitives.ToggleButton tbGroup;
        Avalonia.Controls.Primitives.ToggleButton tbType;

        // ITreeDataTemplate implementation for hierarchical TreeNode display
        sealed class TreeNodeTemplate : Avalonia.Controls.Templates.ITreeDataTemplate
        {
            public bool Match(object? data) => data is TreeNode;

            public Avalonia.Controls.Control? Build(object? param)
            {
                if (param is TreeNode node)
                    return new Avalonia.Controls.TextBlock { Text = node.Text ?? "" };
                return null;
            }

            public Avalonia.Data.InstancedBinding? ItemsSelector(object item)
            {
                if (item is TreeNode node && node.Nodes.Count > 0)
                    return Avalonia.Data.InstancedBinding.OneTime(node.Nodes);
                return null;
            }
        }

        public ResourceTreeViewExt()
        {
            allowselectevent = true;

            tv = new Avalonia.Controls.TreeView
            {
                ItemTemplate = new TreeNodeTemplate(),
            };
            tv.SelectionChanged += tv_AfterSelect;

            // Font size inherits from the app-wide default; don't bump it for UseBigIcons.

            tbType  = new Avalonia.Controls.Primitives.ToggleButton { Content = "T", Width = 22, Height = 22, Padding = new Avalonia.Thickness(0), HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center, VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Center };
            tbGroup = new Avalonia.Controls.Primitives.ToggleButton { Content = "G", Width = 22, Height = 22, Padding = new Avalonia.Thickness(0), HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center, VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Center };
            tbInst  = new Avalonia.Controls.Primitives.ToggleButton { Content = "I", Width = 22, Height = 22, Padding = new Avalonia.Thickness(0), HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center, VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Center };

            Avalonia.Controls.ToolTip.SetTip(tbType,  "By Type");
            Avalonia.Controls.ToolTip.SetTip(tbGroup, "By Group");
            Avalonia.Controls.ToolTip.SetTip(tbInst,  "By Instance");

            tbType.Click  += SelectTreeBuilder;
            tbGroup.Click += SelectTreeBuilder;
            tbInst.Click  += SelectTreeBuilder;

            var buttons = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                Spacing = 2,
                Margin = new Avalonia.Thickness(2),
            };
            buttons.Children.Add(tbType);
            buttons.Children.Add(tbGroup);
            buttons.Children.Add(tbInst);

            var headerTitle = new Avalonia.Controls.TextBlock
            {
                Text = "Resource Tree",
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                FontWeight = Avalonia.Media.FontWeight.SemiBold,
                Margin = new Avalonia.Thickness(4, 0),
            };

            var headerContent = new Avalonia.Controls.DockPanel { LastChildFill = true };
            Avalonia.Controls.DockPanel.SetDock(buttons, Avalonia.Controls.Dock.Right);
            headerContent.Children.Add(buttons);
            headerContent.Children.Add(headerTitle);

            var headerBar = new Avalonia.Controls.Border
            {
                Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromRgb(235, 235, 235)),
                BorderBrush = Avalonia.Media.Brushes.Gray,
                BorderThickness = new Avalonia.Thickness(0, 0, 0, 1),
                MinHeight = 26,
                Child = headerContent,
            };

            var dock = new Avalonia.Controls.DockPanel();
            Avalonia.Controls.DockPanel.SetDock(headerBar, Avalonia.Controls.Dock.Top);
            dock.Children.Add(headerBar);
            dock.Children.Add(tv);
            Content = dock;

            typebuilder  = new ResourceTreeNodesByType();
            groupbuilder = new ResourceTreeNodesByGroup();
            instbuilder  = new ResourceTreeNodesByInstance();

            builder = typebuilder;
            tbType.IsChecked = true;
            last = null;
        }

        internal void SetManager(ResourceViewManager manager)
        {
            last = null;
            if (this.manager != manager)
                this.manager = manager;
        }

        public void Clear()
        {
            tv.ItemsSource = null;
        }

        ResourceMaps last;
        void SetResourceMaps(bool nosave)
        {
            tv.ItemsSource = null;
            if (last != null) SetResourceMaps(last, true, nosave);
        }

        bool allowselectevent;
        TreeNode firstnode;
        public bool SetResourceMaps(ResourceMaps maps, bool selectevent, bool dontselect)
        {
            return SetResourceMaps(maps, selectevent, dontselect, false);
        }
        protected bool SetResourceMaps(ResourceMaps maps, bool selectevent, bool dontselect, bool nosave)
        {
            last = maps;
            if (!nosave) SaveLastSelection();

            this.Clear();
            firstnode = builder.BuildNodes(maps);
            tv.ItemsSource = new[] { firstnode };
            // Expand the root node after Avalonia creates the container
            Avalonia.Threading.Dispatcher.UIThread.Post(() =>
            {
                var root = tv.ContainerFromItem(firstnode) as Avalonia.Controls.TreeViewItem;
                if (root != null) root.IsExpanded = true;
            }, Avalonia.Threading.DispatcherPriority.Loaded);

            allowselectevent = selectevent;
            if (!dontselect && (maps.Everything.Count <= Helper.XmlRegistry.BigPackageResourceCount || Helper.XmlRegistry.ResoruceTreeAllwaysAutoselect))
            {
                if (!SelectID(firstnode, builder.LastSelectedId))
                {
                    SelectAll();
                    allowselectevent = true;
                    return false;
                }
            }
            else if (dontselect)
            {
                foreach (ResourceTreeNodeExt node in firstnode.Nodes)
                {
                    if (node.ID == 0x46414D49) { tv.SelectedItem = node; break; }
                }
            }

            allowselectevent = true;
            return true;
        }

        private void SaveLastSelection()
        {
            ResourceTreeNodeExt node = tv.SelectedItem as ResourceTreeNodeExt;
            if (node != null) builder.LastSelectedId = node.ID;
            else builder.LastSelectedId = 0;
        }

        protected bool SelectID(TreeNode node, ulong id)
        {
            ResourceTreeNodeExt rn = node as ResourceTreeNodeExt;
            if (rn != null)
            {
                if (rn.ID == id)
                {
                    tv.SelectedItem = rn;
                    return true;
                }
            }

            foreach (TreeNode sub in node.Nodes)
                if (SelectID(sub, id)) return true;

            return false;
        }

        public void SelectAll()
        {
            if (firstnode != null)
                tv.SelectedItem = firstnode;
        }

        private void tv_AfterSelect(object sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            if (!allowselectevent) return;
            ResourceTreeNodeExt node = e.AddedItems.Count > 0 ? e.AddedItems[0] as ResourceTreeNodeExt : null;
            if (node == null) return;
            if (this.manager != null && manager.ListView != null)
                manager.ListView.SetResources(node.Resources);
        }

        private void SelectTreeBuilder(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            tbType.IsChecked  = sender == tbType;
            tbGroup.IsChecked = sender == tbGroup;
            tbInst.IsChecked  = sender == tbInst;

            SaveLastSelection();

            IResourceTreeNodeBuilder old = builder;
            if (sender == tbInst)       builder = instbuilder;
            else if (sender == tbGroup) builder = groupbuilder;
            else                        builder = typebuilder;

            if (old != builder) SetResourceMaps(true);
        }

        internal void RestoreLayout()
        {
            SelectTreeBuilder(tbType, null);
        }
    }
}
