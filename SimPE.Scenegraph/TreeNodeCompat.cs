/***************************************************************************
 *   Copyright (C) 2025 by GramzeSweatShop                                 *
 *   rhiamom@mac.com                                                       *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or    *
 *   (at your option) any later version.                                   *
 ***************************************************************************/

// Avalonia port — TreeNode and TreeViewEventArgs compat shims for Scenegraph.
// The TreeView control is now Avalonia.Controls.TreeView but business logic
// still uses these lightweight data-holder types.

using System;

namespace SimPe.Plugin
{
    /// <summary>Minimal TreeNode data holder — replaces System.Windows.Forms.TreeNode.</summary>
    internal class TreeNode
    {
        public string Text { get; set; }
        public object Tag { get; set; }
        public TreeNode Parent { get; set; }
        public System.Collections.Generic.List<TreeNode> Nodes { get; } = new System.Collections.Generic.List<TreeNode>();

        public TreeNode(string text = "") { Text = text; }
    }

    /// <summary>Minimal TreeViewEventArgs — replaces System.Windows.Forms.TreeViewEventArgs.</summary>
    internal class TreeViewEventArgs : EventArgs
    {
        public TreeNode Node { get; set; }
        public TreeViewEventArgs(TreeNode node) { Node = node; }
    }
}
