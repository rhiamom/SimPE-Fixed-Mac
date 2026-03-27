/***************************************************************************
 *   Copyright (C) 2025 by GramzeSweatShop                                 *
 *   rhiamom@mac.com                                                       *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 ***************************************************************************/

// Minimal TreeNode data holder for ResourceTreeView (Avalonia port).
// Replaces System.Windows.Forms.TreeNode. The Avalonia TreeView uses this
// as its data model via ITreeDataTemplate in ResourceTreeViewExt.

namespace SimPe.Windows.Forms
{
    public class TreeNode
    {
        public string Text { get; set; } = "";
        public object Tag { get; set; }
        public int ImageIndex { get; set; }
        public int SelectedImageIndex { get; set; }
        public System.Collections.Generic.List<TreeNode> Nodes { get; } =
            new System.Collections.Generic.List<TreeNode>();

        public TreeNode() { }
        public TreeNode(string text) { Text = text; }

        // No-op: Avalonia TreeView expands via ItemsSource binding, not this method.
        public void Expand() { }
    }
}
