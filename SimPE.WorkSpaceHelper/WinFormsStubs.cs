/***************************************************************************
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

// Minimal WinForms stubs required to compile LoadFileWrappers.cs (Mac/Avalonia port).
// Only the members actually used by ToolMenuItem are provided.

using System;

namespace System.Windows.Forms
{
    [Flags]
    public enum AnchorStyles { None = 0, Top = 1, Bottom = 2, Left = 4, Right = 8 }

    public enum DockStyle { None, Top, Bottom, Left, Right, Fill }

    // MessageBoxButtons, MessageBoxIcon, MessageBoxDefaultButton — defined in SimPe namespace (Message.cs).

    /// <summary>Minimal stub for System.Windows.Forms.Padding.</summary>
    public struct Padding
    {
        public int Left, Top, Right, Bottom;
        public Padding(int all) { Left = Top = Right = Bottom = all; }
        public Padding(int left, int top, int right, int bottom)
        { Left = left; Top = top; Right = right; Bottom = bottom; }
    }

    /// <summary>
    /// Minimal stub for System.Windows.Forms.ToolStripItem.
    /// </summary>
    public class ToolStripItem
    {
        public string Text    { get; set; } = string.Empty;
        public bool   Enabled { get; set; } = true;
        public bool   Visible { get; set; } = true;
        public object Tag     { get; set; }
        public object Image   { get; set; }
        public string Name    { get; set; } = "";
        public System.Drawing.Size Size { get; set; }
        public event EventHandler Click;
        protected virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);
    }

    // ToolStrip and ToolStripButton are defined in SimPE.GMDCExporterbase (ListViewEx.cs).
    // Do not redefine them here — it causes CS0433 ambiguity in projects that reference both.
    //
    // ToolStripItemCollection and ToolStripMenuItem are NOT in GMDCExporterbase, so define them here.

    /// <summary>Minimal stub for System.Windows.Forms.ToolStripItemCollection.</summary>
    public class ToolStripItemCollection : System.Collections.IEnumerable
    {
        private readonly System.Collections.Generic.List<ToolStripItem> _items = new();
        public int Count => _items.Count;
        public ToolStripItem this[int i] => _items[i];
        public void Add(ToolStripItem item) { _items.Add(item); }
        public void Insert(int index, ToolStripItem item) { _items.Insert(index, item); }
        public void Remove(ToolStripItem item) { _items.Remove(item); }
        public void Clear() { _items.Clear(); }
        public System.Collections.IEnumerator GetEnumerator() => _items.GetEnumerator();
    }

    /// <summary>Minimal stub for System.Windows.Forms.ToolStripMenuItem.</summary>
    public class ToolStripMenuItem : ToolStripItem
    {
        public ToolStripMenuItem() { }
        public ToolStripMenuItem(string text) { Text = text; }
        public bool Checked { get; set; }
        public string ToolTipText { get; set; } = "";
        public object ShortcutKeys { get; set; }
        public ToolStripItemCollection DropDownItems { get; } = new ToolStripItemCollection();
    }

    // Control and ControlCollection are defined in SimPE.GMDCExporterbase (ListViewEx.cs).
    // WorkSpaceHelper now references GMDCExporterbase via Wizardbase, so they must not be
    // redefined here to avoid CS0433 ambiguity.

    /// <summary>Minimal stub for System.Windows.Forms.FormClosingEventArgs.</summary>
    public class FormClosingEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
        public CloseReason CloseReason { get; }
    }
    public delegate void FormClosingEventHandler(object sender, FormClosingEventArgs e);

    public enum CloseReason { None, WindowsShutDown, MdiFormClosing, UserClosing, TaskManagerClosing, FormOwnerClosing, ApplicationExitCall }

    // Shortcut is defined in SimPE.GMDCExporterbase (ListViewEx.cs) — do not redefine here.
}
