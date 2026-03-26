/***************************************************************************
 *   Copyright (C) 2025 by GramzeSweatShop                                 *
 *   rhiamom@mac.com                                                       *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or    *
 *   (at your option) any later version.                                   *
 ***************************************************************************/

// Data model types for SimPE.Scenegraph forms (Avalonia port).
// These are plain C# data classes used by form code-behind to hold list/tree data.
// They have no dependency on System.Windows.Forms — they mimic the relevant API
// so the business logic compiles unchanged while the actual Avalonia UI is wired up
// separately via AXAML layouts.

using System;
using System.Collections;
using System.Collections.Generic;
using Avalonia.Controls;

namespace SimPe.Scenegraph.Compat
{
    // ── ListViewItem / SubItem ───────────────────────────────────────────────

    /// <summary>Minimal ListViewItem — replaces System.Windows.Forms.ListViewItem.</summary>
    public class ListViewItem
    {
        public class ListViewSubItemCollection
        {
            private readonly List<SubItem> _items = new List<SubItem>();
            public SubItem this[int index] => _items[index];
            public void Add(string text) => _items.Add(new SubItem(text));
            public int Count => _items.Count;
            public void Clear() => _items.Clear();
        }

        public class SubItem
        {
            public string Text { get; set; }
            public SubItem(string text) { Text = text; }
        }

        public string Text { get; set; }
        public object Tag { get; set; }
        public int ImageIndex { get; set; } = -1;
        public bool Selected { get; set; }
        public ListViewSubItemCollection SubItems { get; } = new ListViewSubItemCollection();

        public ListViewItem() { Text = ""; }
        public ListViewItem(string text) { Text = text; }
    }

    // ── ListView ─────────────────────────────────────────────────────────────

    /// <summary>Minimal ListView — replaces System.Windows.Forms.ListView.</summary>
    public class ListView
    {
        public class ListViewItemCollection
        {
            private readonly List<ListViewItem> _items = new List<ListViewItem>();
            public ListViewItem this[int index] { get => _items[index]; set => _items[index] = value; }
            public int Count => _items.Count;
            public void Add(ListViewItem item) => _items.Add(item);
            public void Clear() => _items.Clear();
            public void Remove(ListViewItem item) => _items.Remove(item);
            public IEnumerator GetEnumerator() => _items.GetEnumerator();
        }

        public class SelectedListViewItemCollection
        {
            private readonly ListViewItemCollection _all;
            public SelectedListViewItemCollection(ListViewItemCollection all) { _all = all; }
            public int Count
            {
                get { int c = 0; foreach (ListViewItem i in _all) { if (((ListViewItem)i).Selected) c++; } return c; }
            }
            public ListViewItem this[int index]
            {
                get { int c = 0; foreach (object o in _all) { var i = (ListViewItem)o; if (i.Selected) { if (c == index) return i; c++; } } throw new IndexOutOfRangeException(); }
            }
        }

        public class ColumnHeaderCollection
        {
            private readonly List<ColumnHeader> _columns = new List<ColumnHeader>();
            public int Count => _columns.Count;
            public void Add(ColumnHeader col) => _columns.Add(col);
            public void Clear() => _columns.Clear();
        }

        public ListViewItemCollection Items { get; } = new ListViewItemCollection();
        public SelectedListViewItemCollection SelectedItems { get; }
        public ColumnHeaderCollection Columns { get; } = new ColumnHeaderCollection();
        public object Tag { get; set; }
        public bool Enabled { get; set; } = true;
        public bool MultiSelect { get; set; } = true;
        public bool HideSelection { get; set; }
        public bool FullRowSelect { get; set; }
        public bool GridLines { get; set; }
        public ImageList LargeImageList { get; set; }
        public System.Drawing.Font Font { get; set; }

        // Layout/position properties (no-ops in Avalonia port)
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Control Parent { get; set; }
        public bool Visible { get; set; } = true;
        public Avalonia.Layout.HorizontalAlignment Anchor { get; set; }

        public event EventHandler SelectedIndexChanged;

        // No-ops: update batching has no effect in this compat stub
        public void BeginUpdate() { }
        public void EndUpdate() { }

        public ListView()
        {
            SelectedItems = new SelectedListViewItemCollection(Items);
        }
    }

    // ── ColumnHeader ─────────────────────────────────────────────────────────

    /// <summary>Minimal ColumnHeader — replaces System.Windows.Forms.ColumnHeader.</summary>
    public class ColumnHeader
    {
        public string Text { get; set; }
        public int Width { get; set; }
    }

    // ── ImageList ────────────────────────────────────────────────────────────

    /// <summary>Minimal ImageList — replaces System.Windows.Forms.ImageList.</summary>
    public class ImageList
    {
        public class ImageCollection
        {
            private readonly List<System.Drawing.Image> _images = new List<System.Drawing.Image>();
            public int Count => _images.Count;
            public void Add(System.Drawing.Image img) => _images.Add(img);
        }

        public System.Drawing.Size ImageSize { get; set; } = new System.Drawing.Size(16, 16);
        public ColorDepth ColorDepth { get; set; }
        public ImageCollection Images { get; } = new ImageCollection();
    }

    // ── ColorDepth enum ──────────────────────────────────────────────────────
    public enum ColorDepth { Depth4Bit, Depth8Bit, Depth16Bit, Depth24Bit, Depth32Bit }

    // ── CheckBox compat (for SubsetSelectForm programmatic use) ──────────────

    /// <summary>
    /// Extends Avalonia CheckBox with WinForms-compatible API used in SubsetSelectForm.
    /// </summary>
    public class CheckBoxCompat : Avalonia.Controls.CheckBox
    {
        public bool Checked
        {
            get => IsChecked == true;
            set => IsChecked = value;
        }
        public Avalonia.Layout.HorizontalAlignment Anchor { get; set; }
        // FlatStyle is ignored; no-op.
        public object FlatStyle { get; set; }
    }

    // ── PictureBoxCompat ─────────────────────────────────────────────────────

    /// <summary>
    /// Minimal PictureBox compat — replaces System.Windows.Forms.PictureBox.
    /// Holds a System.Drawing.Image; Avalonia rendering is a separate concern.
    /// </summary>
    public class PictureBoxCompat : Avalonia.Controls.Control
    {
        /// <summary>The current image (System.Drawing.Image).</summary>
        public System.Drawing.Image Image { get; set; }
        public System.Drawing.Size Size { get; set; } = new System.Drawing.Size(100, 100);
    }

    // ── Panel compat ──────────────────────────────────────────────────────────

    /// <summary>
    /// Extends Avalonia Panel with WinForms-compatible layout API for SubsetSelectForm.
    /// </summary>
    public class PanelCompat : Avalonia.Controls.Panel
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public new int Width { get; set; }
        public new int Height { get; set; }
        public bool Visible { get; set; } = true;
        public new Avalonia.Layout.HorizontalAlignment Anchor { get; set; }
        public Control ParentControl { get; set; }
    }
}
