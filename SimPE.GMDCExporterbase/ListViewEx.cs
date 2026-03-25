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

// Ported from WinForms to Avalonia (Mac port).
// The P/Invoke embedded-control ListView is replaced with a stub that compiles
// but no-ops all WinForms-specific APIs.  Business logic in callers is unchanged.

using System;
using System.Collections.Generic;
using System.Drawing;

namespace System.Windows.Forms
{
    // DockStyle is defined in SimPE.WorkSpaceHelper/WinFormsStubs.cs (which GMDCExporterbase references).

    // ─── View enum ─────────────────────────────────────────────────────────────
    public enum View { LargeIcon, SmallIcon, List, Details, Tile }

    // ─── BorderStyle enum ──────────────────────────────────────────────────────
    public enum BorderStyle { None, FixedSingle, Fixed3D }

    // ─── ColumnHeaderStyle enum ────────────────────────────────────────────────
    public enum ColumnHeaderStyle { None, Nonclickable, Clickable }

    // ─── FlatStyle enum ────────────────────────────────────────────────────────
    public enum FlatStyle { Flat, Popup, Standard, System }

    // ─── ComboBoxStyle enum ────────────────────────────────────────────────────
    public enum ComboBoxStyle { Simple, DropDown, DropDownList }

    // ─── ScrollBars enum ──────────────────────────────────────────────────────
    public enum ScrollBars { None, Horizontal, Vertical, Both }

    // ─── RichTextBoxScrollBars enum ───────────────────────────────────────────
    public enum RichTextBoxScrollBars { None, Horizontal, Vertical, Both, ForcedHorizontal, ForcedVertical, ForcedBoth }

    // ─── ImeMode enum ─────────────────────────────────────────────────────────
    public enum ImeMode { NoControl, On, Off, Disable, Hiragana, Katakana, KatakanaHalf, AlphaFull, Alpha, HangulFull, Hangul, Close, OnHalf, Inherit }

    // ─── DragDropEffects enum ─────────────────────────────────────────────────
    public enum DragDropEffects { None = 0, Copy = 1, Move = 2, Link = 4, Scroll = unchecked((int)0x80000000), All = -1 }

    // ─── DragEventHandler ────────────────────────────────────────────────────
    public delegate void DragEventHandler(object sender, DragEventArgs e);

    // ContentAlignment: use System.Drawing.ContentAlignment directly (available in .NET 8)

    // AnchorStyles is defined in SimPE.WorkSpaceHelper/WinFormsStubs.cs

    // ─── MessageBoxButtons / MessageBoxIcon / MessageBoxDefaultButton ─────────
    // Defined in SimPe namespace (Message.cs). Use SimPe.MessageBoxButtons etc.

    // ─── DialogResult ─────────────────────────────────────────────────────────
    // Also defined as SimPe.DialogResult in SimPE.Helper/DialogResult.cs with matching values.
    // Callers that fully qualify System.Windows.Forms.DialogResult use this definition.
    // Callers inside SimPe.* namespaces use SimPe.DialogResult via namespace scoping.
    public enum DialogResult { None, OK, Cancel, Abort, Retry, Ignore, Yes, No }

    // ─── AccessibleRole enum ──────────────────────────────────────────────────
    public enum AccessibleRole { Default = -1, None = 0, TitleBar = 1, MenuBar = 2, ScrollBar = 3, Grip = 4, Sound = 5, Cursor = 6, Caret = 7, Alert = 8, Window = 9, Client = 10, MenuPopup = 11, MenuItem = 12, Tooltip = 13, Application = 14, Document = 15, Pane = 16, Chart = 17, Dialog = 18, Border = 19, Grouping = 20, Separator = 21, ToolBar = 22, StatusBar = 23, Table = 24, ColumnHeader = 25, RowHeader = 26, Column = 27, Row = 28, Cell = 29, Link = 30, HelpBalloon = 31, Character = 32, List = 33, ListItem = 34, Outline = 35, OutlineItem = 36, PageTab = 37, PropertyPage = 38, Indicator = 39, Graphic = 40, StaticText = 41, Text = 42, PushButton = 43, CheckButton = 44, RadioButton = 45, ComboBox = 46, DropList = 47, ProgressBar = 48, Dial = 49, HotkeyField = 50, Slider = 51, SpinButton = 52, Diagram = 53, Animation = 54, Equation = 55, ButtonDropDown = 56, ButtonMenu = 57, ButtonDropDownGrid = 58, WhiteSpace = 59, PageTabList = 60, Clock = 61, SplitButton = 62, IpAddress = 63, OutlineButton = 64 }

    // ─── Appearance enum ──────────────────────────────────────────────────────
    public enum Appearance { Normal = 0, Button = 1 }

    // ─── HorizontalAlignment enum ─────────────────────────────────────────────
    public enum HorizontalAlignment { Left = 0, Right = 1, Center = 2 }

    // ─── RightToLeft enum ─────────────────────────────────────────────────────
    public enum RightToLeft { No = 0, Yes = 1, Inherit = 2 }

    // ─── PropertySort enum ────────────────────────────────────────────────────
    public enum PropertySort { NoSort = 0, Alphabetical = 1, Categorized = 2, CategorizedAlphabetical = 3 }

    // ─── Cursors static class ────────────────────────────────────────────────
    public static class Cursors
    {
        public static Avalonia.Input.Cursor Default => new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Arrow);
        public static Avalonia.Input.Cursor Hand => new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Hand);
        public static Avalonia.Input.Cursor WaitCursor => new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Wait);
        public static Avalonia.Input.Cursor Arrow => new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Arrow);
    }

    // ─── ContentAlignment enum ───────────────────────────────────────────────
    public enum ContentAlignment { TopLeft = 1, TopCenter = 2, TopRight = 4, MiddleLeft = 16, MiddleCenter = 32, MiddleRight = 64, BottomLeft = 256, BottomCenter = 512, BottomRight = 1024 }

    // ─── ToolTip stub ────────────────────────────────────────────────────────
    public class ToolTip : IDisposable
    {
        public bool Active { get; set; } = true;
        public ToolTip() { }
        public ToolTip(System.ComponentModel.IContainer container) { }
        public void SetToolTip(object control, string caption) { }
        public void Dispose() { }
    }

    // ─── ToolStripRenderer stub ───────────────────────────────────────────────
    public class ToolStripRenderer { }

    // ─── ColumnHeader ─────────────────────────────────────────────────────────
    public class ColumnHeader
    {
        public string Text  { get; set; } = "";
        public int    Width { get; set; } = 80;
        public int    Index { get; private set; }
        public int    DisplayIndex { get; set; }
        public ListView ListView { get; set; }

        internal void SetIndex(int i) { Index = i; }
    }

    // ─── ListViewItem ─────────────────────────────────────────────────────────
    public class ListViewItem
    {
        public string Text     { get; set; } = "";
        public Color  ForeColor { get; set; } = Color.Black;
        public System.Drawing.Font Font { get; set; }
        public object Tag        { get; set; }
        public bool   Selected   { get; set; }
        public int    ImageIndex { get; set; } = -1;
        public int    Index      { get; set; } = -1;
        public ListView ListView { get; set; }
        public void EnsureVisible() { }
        public ListViewItem Clone() { return (ListViewItem)MemberwiseClone(); }

        private readonly List<SubItem> _subItems = new List<SubItem>();
        public SubItemCollection SubItems { get; }

        public ListViewItem()           { SubItems = new SubItemCollection(_subItems); }
        public ListViewItem(string text) : this() { Text = text; }

        public class SubItem
        {
            public string Text { get; set; }
            public SubItem(string text) { Text = text; }
        }

        public class SubItemCollection
        {
            private readonly List<SubItem> _items;
            public SubItemCollection(List<SubItem> items) { _items = items; }
            public SubItem this[int i] => _items[i];
            public int    Count        => _items.Count;
            public SubItem Add(string text) { var s = new SubItem(text); _items.Add(s); return s; }
            public void    Clear()          { _items.Clear(); }
        }
    }

    // ─── ColumnHeaderCollection ───────────────────────────────────────────────
    public class ColumnHeaderCollection : System.Collections.Generic.IEnumerable<ColumnHeader>
    {
        private readonly List<ColumnHeader> _cols = new List<ColumnHeader>();
        public int Count => _cols.Count;
        public ColumnHeader this[int i] => _cols[i];

        public void Add(ColumnHeader h)
        {
            h.SetIndex(_cols.Count);
            _cols.Add(h);
        }

        public void AddRange(ColumnHeader[] headers)
        {
            foreach (var h in headers) Add(h);
        }

        public void Clear() { _cols.Clear(); }
        public void Remove(ColumnHeader h) { _cols.Remove(h); }
        public System.Collections.Generic.IEnumerator<ColumnHeader> GetEnumerator()
            => _cols.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => _cols.GetEnumerator();
    }

    // ─── ListViewItemCollection ───────────────────────────────────────────────
    public class ListViewItemCollection : System.Collections.Generic.IEnumerable<ListViewItem>
    {
        private readonly List<ListViewItem> _items = new List<ListViewItem>();
        public int Count => _items.Count;
        public ListViewItem this[int i] { get => _items[i]; set => _items[i] = value; }
        public void Add(ListViewItem item)    { _items.Add(item); }
        public void Insert(int index, ListViewItem item) { _items.Insert(index, item); }
        public void Remove(ListViewItem item) { _items.Remove(item); }
        public void Clear()                   { _items.Clear(); }
        public bool Contains(ListViewItem item) => _items.Contains(item);

        // stub for SelectedItems.Clear() called by old code
        public void ClearSelection() { foreach (var it in _items) it.Selected = false; }

        public System.Collections.Generic.IEnumerator<ListViewItem> GetEnumerator()
            => _items.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => _items.GetEnumerator();
    }

    // ─── SelectedListViewItemCollection ───────────────────────────────────────
    public class SelectedListViewItemCollection : System.Collections.Generic.IEnumerable<ListViewItem>
    {
        private readonly ListViewItemCollection _all;
        public SelectedListViewItemCollection(ListViewItemCollection all) { _all = all; }
        public int         Count    => _all.Count;
        public ListViewItem this[int i] => _all[i];
        public void        Clear()  { }
        public System.Collections.Generic.IEnumerator<ListViewItem> GetEnumerator()
            => _all.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => _all.GetEnumerator();
    }

    // ─── SelectedIndexCollection ──────────────────────────────────────────────
    /// WinForms ListView.SelectedIndices returns int indices, not items.
    public class SelectedIndexCollection : System.Collections.Generic.IEnumerable<int>
    {
        private readonly List<int> _indices = new List<int>();
        public int  Count        => _indices.Count;
        public int  this[int i]  => _indices[i];
        public void Add(int index)    { if (!_indices.Contains(index)) _indices.Add(index); }
        public void Remove(int index) { _indices.Remove(index); }
        public void Clear()           { _indices.Clear(); }
        public bool Contains(int i)   => _indices.Contains(i);
        public System.Collections.Generic.IEnumerator<int> GetEnumerator() => _indices.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => _indices.GetEnumerator();
    }

    // ─── Form ─────────────────────────────────────────────────────────────────
    /// <summary>
    /// Stub base class for WinForms Form; backs the Mac Avalonia port.
    /// Extend Avalonia.Controls.UserControl for real UI; this is for compile compatibility.
    /// </summary>
    public class Form : Avalonia.Controls.UserControl
    {
        public string Text     { get; set; } = "";
        public bool   ShowInTaskbar { get; set; } = true;
        public FormBorderStyle FormBorderStyle { get; set; }
        public FormStartPosition StartPosition { get; set; }
        public bool   MaximizeBox { get; set; } = true;
        public bool   MinimizeBox { get; set; } = true;
        public System.Drawing.Size  AutoScaleBaseSize    { get; set; }
        public System.Drawing.SizeF AutoScaleDimensions  { get; set; }
        public AutoScaleMode        AutoScaleMode        { get; set; }
        public System.Drawing.Size  ClientSize           { get; set; }
        public System.Drawing.Color BackColor            { get; set; }
        public System.Drawing.Image BackgroundImage      { get; set; }
        public System.Drawing.Icon  Icon                 { get; set; }
        public double               Opacity              { get; set; } = 1.0;
        public System.Drawing.Size  Size                 { get; set; }
        public System.Drawing.Point Location             { get; set; }

        public Panel.ControlCollection Controls    { get; } = new Panel.ControlCollection();
        public System.Drawing.Font     Font       { get; set; }
        public FormWindowState         WindowState { get; set; }

        public void ShowDialog()  { }
        public void Close()       { }

        public SimPe.DialogResult DialogResult { get; set; }
        public event EventHandler                          Load;
        public event System.ComponentModel.CancelEventHandler Closing;
        public event EventHandler Closed;

        public void Invoke(System.Delegate d, object[] args = null)
            => Avalonia.Threading.Dispatcher.UIThread.Post(() => d.DynamicInvoke(args));
        public void BeginInvoke(System.Delegate d, object[] args = null)
            => Avalonia.Threading.Dispatcher.UIThread.Post(() => d.DynamicInvoke(args));

        protected virtual void SuspendLayout()   { }
        protected virtual void ResumeLayout(bool performLayout = true) { }
        protected virtual void PerformLayout()   { }
        protected virtual void Dispose(bool disposing) { }
        public    void         Dispose() { Dispose(true); }
        protected virtual void OnActivated(EventArgs e) { }
        public event EventHandler Activated;
        protected virtual void WndProc(ref Message m) { }
    }

    // ─── SelectionMode enum ───────────────────────────────────────────────────
    public enum SelectionMode { One, MultiSimple, MultiExtended, None }

    // ─── CheckState enum ─────────────────────────────────────────────────────
    public enum CheckState { Unchecked, Checked, Indeterminate }

    // ─── FormWindowState enum ─────────────────────────────────────────────────
    public enum FormWindowState { Normal, Minimized, Maximized }

    // ─── MessageBox stub ──────────────────────────────────────────────────────
    public static class MessageBox
    {
        public static DialogResult Show(string text) { return DialogResult.OK; }
        public static DialogResult Show(string text, string caption) { return DialogResult.OK; }
        public static DialogResult Show(string text, string caption, SimPe.MessageBoxButtons buttons) { return DialogResult.OK; }
        public static DialogResult Show(string text, string caption, SimPe.MessageBoxButtons buttons, SimPe.MessageBoxIcon icon) { return DialogResult.OK; }
        public static DialogResult Show(string text, string caption, SimPe.MessageBoxButtons buttons, SimPe.MessageBoxIcon icon, SimPe.MessageBoxDefaultButton def) { return DialogResult.OK; }
    }

    // ─── FormBorderStyle enum ─────────────────────────────────────────────────
    public enum FormBorderStyle
    {
        None, FixedSingle, Fixed3D, FixedDialog,
        Sizable, FixedToolWindow, SizableToolWindow
    }

    // ─── FormStartPosition enum ───────────────────────────────────────────────
    public enum FormStartPosition { Manual, CenterScreen, WindowsDefaultLocation, WindowsDefaultBounds, CenterParent }

    // ─── PictureBoxSizeMode enum ──────────────────────────────────────────────
    public enum PictureBoxSizeMode { Normal, StretchImage, AutoSize, CenterImage, Zoom }

    // ─── ColorDepth enum ─────────────────────────────────────────────────────
    public enum ColorDepth { Depth4Bit, Depth8Bit, Depth16Bit, Depth24Bit, Depth32Bit }

    // ─── LinkArea ─────────────────────────────────────────────────────────────
    public struct LinkArea
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public LinkArea(int start, int length) { Start = start; Length = length; }
    }

    // ─── TreeNode ─────────────────────────────────────────────────────────────
    public class TreeNode
    {
        public string Text { get; set; }
        public object Tag  { get; set; }
        public int    ImageIndex { get; set; } = -1;
        public int    SelectedImageIndex { get; set; } = -1;
        public TreeNode Parent { get; set; }
        public bool   IsExpanded { get; set; }
        public string Name { get; set; } = "";
        public string ToolTipText { get; set; } = "";
        private readonly List<TreeNode> _nodes = new List<TreeNode>();
        public TreeNodeCollection Nodes { get; }
        public TreeNode() { Nodes = new TreeNodeCollection(_nodes); Text = ""; }
        public TreeNode(string text) : this() { Text = text; }

        public void EnsureVisible() { }
        public void Expand() { IsExpanded = true; }
        public void ExpandAll() { IsExpanded = true; foreach (var n in _nodes) n.ExpandAll(); }
        public void Collapse() { IsExpanded = false; }
        public void Remove() { Parent?.Nodes.Remove(this); }

        public class TreeNodeCollection : System.Collections.IEnumerable
        {
            private readonly List<TreeNode> _items;
            public TreeNodeCollection(List<TreeNode> items) { _items = items; }
            public TreeNode this[int i] => _items[i];
            public int Count => _items.Count;
            public void Add(TreeNode node) { _items.Add(node); }
            public void Remove(TreeNode node) { _items.Remove(node); }
            public void RemoveAt(int index) { _items.RemoveAt(index); }
            public void Clear() { _items.Clear(); }
            public System.Collections.Generic.IEnumerator<TreeNode> GetEnumerator()
                => _items.GetEnumerator();
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
                => _items.GetEnumerator();
        }
    }

    // ─── TreeViewEventArgs ────────────────────────────────────────────────────
    public class TreeViewEventArgs : EventArgs
    {
        public TreeNode Node { get; }
        public TreeViewEventArgs(TreeNode node) { Node = node; }
    }

    // ─── TreeViewEventHandler ─────────────────────────────────────────────────
    public delegate void TreeViewEventHandler(object sender, TreeViewEventArgs e);

    // ─── LinkLabelLinkClickedEventArgs ────────────────────────────────────────
    public class LinkLabelLinkClickedEventArgs : EventArgs
    {
        public LinkLabelLinkClickedEventArgs() { }
    }

    // ─── LinkLabelLinkClickedEventHandler ────────────────────────────────────
    public delegate void LinkLabelLinkClickedEventHandler(object sender, LinkLabelLinkClickedEventArgs e);

    // ─── CancelEventArgs / CancelEventHandler (if not in System.ComponentModel) ──
    // System.ComponentModel.CancelEventArgs already exists in .NET 8; no need to stub.

    // ─── ImageList ────────────────────────────────────────────────────────────
    public class ImageList
    {
        public System.Drawing.Size ImageSize { get; set; } = new System.Drawing.Size(16, 16);
        public ColorDepth ColorDepth { get; set; } = ColorDepth.Depth32Bit;
        public System.Drawing.Color TransparentColor { get; set; }
        public ImageListCollection Images { get; } = new ImageListCollection();
        public ImageList() { }
        public ImageList(System.ComponentModel.IContainer container) { }

        public class ImageListCollection
        {
            private readonly List<System.Drawing.Image> _list = new List<System.Drawing.Image>();
            public int Count => _list.Count;
            public System.Drawing.Image this[int i] { get => _list[i]; set => _list[i] = value; }
            public void Add(System.Drawing.Image img) { _list.Add(img); }
            public void Clear() { _list.Clear(); }
        }
    }

    // ─── ListView ─────────────────────────────────────────────────────────────
    public class ListView
    {
        public ColumnHeaderCollection Columns   { get; } = new ColumnHeaderCollection();
        public ListViewItemCollection Items      { get; } = new ListViewItemCollection();
        public SelectedListViewItemCollection SelectedItems
            => new SelectedListViewItemCollection(Items);

        public View              View          { get; set; } = View.Details;
        public bool              FullRowSelect  { get; set; }
        public bool              HideSelection  { get; set; }
        public bool              MultiSelect    { get; set; } = true;
        public bool              LabelEdit      { get; set; }
        public ColumnHeaderStyle HeaderStyle    { get; set; }
        public BorderStyle       BorderStyle    { get; set; }
        public ImageList         LargeImageList { get; set; }
        public object            Tag            { get; set; }
        public bool              IsEnabled      { get; set; } = true;
        public bool              Enabled        { get => IsEnabled; set => IsEnabled = value; }
        public System.Collections.IComparer ListViewItemSorter { get; set; }

        public System.Drawing.Font  Font     { get; set; }
        public System.Drawing.Color BackColor { get; set; }
        public System.Drawing.Point Location  { get; set; }
        public System.Drawing.Size  Size      { get; set; }
        public string  Name     { get; set; } = "";
        public int     TabIndex { get; set; }
        public bool    Visible  { get; set; } = true;
        public bool    TabStop  { get; set; }
        public bool    GridLines { get; set; }
        public bool    UseCompatibleStateImageBehavior { get; set; }
        public DockStyle Dock   { get; set; }
        public AnchorStyles Anchor { get; set; }
        public int     Left     { get; set; }
        public int     Top      { get; set; }
        public int     Width    { get => Size.Width;  set => Size = new System.Drawing.Size(value, Size.Height); }
        public int     Height   { get => Size.Height; set => Size = new System.Drawing.Size(Size.Width, value); }
        public object  Parent   { get; set; }
        public ContextMenuStrip ContextMenuStrip { get; set; }

        // SmallImageList stores Avalonia bitmaps (matches WrapperRegistry.WrapperImageList type)
        public System.Collections.Generic.List<Avalonia.Media.Imaging.Bitmap> SmallImageList { get; set; }
        public System.Collections.Generic.List<Avalonia.Media.Imaging.Bitmap> StateImageList { get; set; }
        public ListViewItem FocusedItem { get; set; }
        private readonly SelectedIndexCollection _selectedIndices = new SelectedIndexCollection();
        public SelectedIndexCollection SelectedIndices => _selectedIndices;
        public void Clear() { Items.Clear(); Columns.Clear(); }
        public void Refresh() { }
        public void Sort() { }
        public void EnsureVisible(int index) { }
        public ListViewItem GetItemAt(int x, int y) => null;
        public bool ShowItemToolTips { get; set; }
        public int VirtualListSize { get; set; }
        public bool VirtualMode { get; set; }
        public event EventHandler ItemActivate;
        public event EventHandler SelectedIndexChanged;
        public event EventHandler ColumnClick;
        public void BeginUpdate() { }
        public void EndUpdate()   { }
        protected virtual void SetStyle(ControlStyles s, bool v) { }
    }

    // ─── TreeView ─────────────────────────────────────────────────────────────
    public class TreeView
    {
        public TreeNode.TreeNodeCollection Nodes { get; }
        public object Tag { get; set; }
        public bool HideSelection { get; set; } = true;
        public string Name { get; set; } = "";
        public System.Drawing.Font Font { get; set; }
        public System.Drawing.Size Size { get; set; }
        public System.Drawing.Point Location { get; set; }
        public int TabIndex { get; set; }
        public AnchorStyles Anchor { get; set; }
        public ImageList ImageList { get; set; }
        public int ImageIndex { get; set; } = -1;
        public int SelectedImageIndex { get; set; } = -1;
        public bool Sorted { get; set; }
        public TreeNode SelectedNode { get; set; }
        public bool LabelEdit { get; set; }
        public bool CheckBoxes { get; set; }
        public bool ShowLines { get; set; } = true;
        public bool ShowPlusMinus { get; set; } = true;
        public bool ShowRootLines { get; set; } = true;
        public bool FullRowSelect { get; set; }
        public int Indent { get; set; } = 19;
        public int ItemHeight { get; set; }
        public bool Scrollable { get; set; } = true;
        public DockStyle    Dock        { get; set; }
        public BorderStyle  BorderStyle { get; set; }
        public bool         Enabled     { get; set; } = true;
        public bool         Visible     { get; set; } = true;
        public bool         TabStop     { get; set; } = true;
        public System.Drawing.Color BackColor { get; set; }
        public System.Drawing.Color ForeColor { get; set; }

        private readonly List<TreeNode> _nodes = new List<TreeNode>();
        public TreeView() { Nodes = new TreeNode.TreeNodeCollection(_nodes); }

        public void BeginUpdate() { }
        public void EndUpdate() { }
        public void CollapseAll() { }
        public void ExpandAll() { }
        public TreeNode GetNodeAt(int x, int y) => null;
        public void Invoke(System.Delegate d, object[] args = null)
            => Avalonia.Threading.Dispatcher.UIThread.Post(() => d.DynamicInvoke(args));
        public object BeginInvoke(System.Delegate d, object[] args = null)
        {
            Avalonia.Threading.Dispatcher.UIThread.Post(() => d.DynamicInvoke(args));
            return null;
        }
        public bool InvokeRequired => false;
        public void Refresh() { }

        public event TreeViewEventHandler AfterSelect;
        public event TreeViewEventHandler BeforeExpand;
        public event TreeViewEventHandler AfterCollapse;
        public event TreeViewEventHandler BeforeCollapse;
        public event System.EventHandler MouseDown;
        public event System.EventHandler Click;
    }

    // ─── Panel ────────────────────────────────────────────────────────────────
    /// <summary>
    /// WinForms Panel stub backed by Avalonia.Controls.Panel so it can be
    /// returned as Avalonia.Controls.Control from GUIHandle implementations.
    /// </summary>
    public class Panel : Avalonia.Controls.Panel
    {
        public new ControlCollection Controls { get; } = new ControlCollection();
        public object Tag      { get; set; }
        public bool AutoScroll { get; set; }
        public int  Top        { get; set; }
        public int  Left       { get; set; }
        public new object Parent { get; set; }
        public AnchorStyles Anchor { get; set; }
        public new bool IsVisible { get; set; } = true;
        public bool Visible { get => IsVisible; set => IsVisible = value; }
        public System.Drawing.Color BackColor { get; set; }
        public System.Drawing.Font Font { get; set; }
        public int TabIndex { get; set; }
        public bool AutoSize { get; set; }
        public new System.Drawing.Size Size { get; set; } = new System.Drawing.Size(100, 100);
        public new System.Drawing.Point Location { get; set; }
        public new string Name { get; set; }
        public new Padding Margin { get; set; }

        public DockStyle Dock { get; set; }
        public System.Drawing.Size AutoScrollMinSize { get; set; }
        public System.Drawing.Size AutoScrollMargin { get; set; }
        public System.Drawing.Image BackgroundImage { get; set; }
        public bool Enabled { get; set; } = true;
        public System.Drawing.Color ForeColor { get; set; }
        public ImeMode ImeMode { get; set; }
        public RightToLeft RightToLeft { get; set; }
        public string Text { get; set; } = "";
        public event EventHandler VisibleChanged;
        public event EventHandler Resize;
        public string AccessibleDescription { get; set; } = "";
        public string AccessibleName { get; set; } = "";
        public AccessibleRole AccessibleRole { get; set; }
        private int _width = 100; private int _height = 100;
        public new int Width  { get => _width;  set { _width  = value; } }
        public new int Height { get => _height; set { _height = value; } }
        public new void SuspendLayout()  { }
        public new void ResumeLayout(bool performLayout = true) { }
        public new void PerformLayout()  { }
        public void Invoke(System.Delegate d, object[] args = null)
            => Avalonia.Threading.Dispatcher.UIThread.Post(() => d.DynamicInvoke(args));
        public void BeginInvoke(System.Delegate d, object[] args = null)
            => Avalonia.Threading.Dispatcher.UIThread.Post(() => d.DynamicInvoke(args));
        public System.Drawing.Size ClientSize { get; set; }
        public virtual System.Drawing.Rectangle DisplayRectangle => new System.Drawing.Rectangle(0, 0, Width, Height);
        public void Invalidate() { }
        public void Focus() { }
        protected virtual void OnPaint(PaintEventArgs e) { }
        protected virtual void OnBackColorChanged(EventArgs e) { }
        protected virtual void OnForeColorChanged(EventArgs e) { }
        protected virtual void OnFontChanged(EventArgs e) { }
        protected virtual void OnTextChanged(EventArgs e) { }
        protected virtual void OnMouseDown(MouseEventArgs e) { }

        public class ControlCollection : System.Collections.Generic.IEnumerable<object>
        {
            private readonly List<object> _list = new List<object>();
            public int Count => _list.Count;
            public object this[int i] => _list[i];
            public void Add(object c) { _list.Add(c); }
            public void Remove(object c) { _list.Remove(c); }
            public void Clear() { _list.Clear(); }
            public void SetChildIndex(object control, int index) { }
            public System.Collections.Generic.IEnumerator<object> GetEnumerator() => _list.GetEnumerator();
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => _list.GetEnumerator();
        }
    }

    // ─── Button ───────────────────────────────────────────────────────────────
    public class Button
    {
        public string Text     { get; set; } = "";
        public bool   IsEnabled { get; set; } = true;
        public bool   Enabled  { get => IsEnabled; set => IsEnabled = value; }
        public object Tag      { get; set; }
        public FlatStyle FlatStyle { get; set; }
        public AnchorStyles Anchor { get; set; }
        public System.Drawing.Font  Font     { get; set; }
        public System.Drawing.Color BackColor { get; set; }
        public System.Drawing.Point Location  { get; set; }
        public System.Drawing.Size  Size      { get; set; }
        public System.Drawing.Color ForeColor { get; set; }
        public string  Name     { get; set; } = "";
        public int     TabIndex { get; set; }
        public bool    Visible  { get; set; } = true;
        public DockStyle Dock   { get; set; }
        public ImeMode ImeMode  { get; set; }
        public bool UseVisualStyleBackColor { get; set; } = true;
        public bool FlatStyle_Field { get; set; }
        public System.Drawing.Image Image { get; set; }
        public ContentAlignment ImageAlign { get; set; }
        public int ImageIndex { get; set; } = -1;
        public ContentAlignment TextAlign { get; set; }
        public bool AutoSize { get; set; }
        public string AccessibleDescription { get; set; } = "";
        public string AccessibleName { get; set; } = "";
        public RightToLeft RightToLeft { get; set; }
        public System.Drawing.Image BackgroundImage { get; set; }
        public event EventHandler Click;
        protected virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);
        public void Refresh() { }
        public void Focus() { }
    }

    // ─── TextBox ──────────────────────────────────────────────────────────────
    public class TextBox
    {
        public string Text     { get; set; } = "";
        public bool   ReadOnly { get; set; }
        public bool   IsEnabled { get; set; } = true;
        public bool   Enabled  { get => IsEnabled; set => IsEnabled = value; }
        public AnchorStyles Anchor { get; set; }
        public System.Drawing.Font  Font      { get; set; }
        public System.Drawing.Color BackColor { get; set; }
        public System.Drawing.Point Location  { get; set; }
        public System.Drawing.Size  Size      { get; set; }
        public string  Name     { get; set; } = "";
        public int     TabIndex { get; set; }
        public bool    Visible  { get; set; } = true;
        public bool    Multiline { get; set; }
        public bool    TabStop  { get; set; }
        public DockStyle Dock   { get; set; }
        public ScrollBars ScrollBars { get; set; }
        public object  Tag      { get; set; }
        public string AccessibleDescription { get; set; } = "";
        public string AccessibleName { get; set; } = "";
        public bool AutoSize { get; set; }
        public System.Drawing.Image BackgroundImage { get; set; }
        public bool HideSelection { get; set; } = true;
        public int MaxLength { get; set; }
        public char PasswordChar { get; set; }
        public RightToLeft RightToLeft { get; set; }
        public HorizontalAlignment TextAlign { get; set; }
        public bool WordWrap { get; set; } = true;
        public bool AcceptsReturn { get; set; }
        public bool AcceptsTab { get; set; }
        public int  SelectionStart  { get; set; }
        public int  SelectionLength { get; set; }
        public event EventHandler TextChanged;
        protected virtual void OnTextChanged(EventArgs e) => TextChanged?.Invoke(this, e);
    }

    // ─── Label ────────────────────────────────────────────────────────────────
    public class Label
    {
        public string Text      { get; set; } = "";
        public bool   AutoSize  { get; set; }
        public AnchorStyles Anchor { get; set; }
        public System.Drawing.Font  Font      { get; set; }
        public System.Drawing.Color BackColor { get; set; }
        public System.Drawing.Size  Size      { get; set; }
        public System.Drawing.Point Location  { get; set; }
        public System.Drawing.Size  MinimumSize { get; set; }
        public System.Drawing.Size  MaximumSize { get; set; }
        public System.Drawing.ContentAlignment TextAlign { get; set; }
        public System.Drawing.Color ForeColor { get; set; }
        public string  Name     { get; set; } = "";
        public int     TabIndex { get; set; }
        public bool    Visible  { get; set; } = true;
        public ImeMode ImeMode  { get; set; }
        public object  Parent   { get; set; }
        public int     Left     { get; set; }
        public int     Top      { get; set; }
        public int     Width    { get => Size.Width;  set => Size = new System.Drawing.Size(value, Size.Height); }
        public int     Height   { get => Size.Height; set => Size = new System.Drawing.Size(Size.Width, value); }
        public void SendToBack() { }
        public string AccessibleDescription { get; set; } = "";
        public string AccessibleName { get; set; } = "";
        public event EventHandler Click;
        public object Cursor { get; set; }
        public DockStyle Dock { get; set; }
        public event EventHandler DoubleClick;
        public bool Enabled { get; set; } = true;
        public System.Drawing.Image Image { get; set; }
        public ContentAlignment ImageAlign { get; set; }
        public int ImageIndex { get; set; } = -1;
        public RightToLeft RightToLeft { get; set; }
    }

    // ─── LinkLabel ────────────────────────────────────────────────────────────
    public class LinkLabel
    {
        public string   Text      { get; set; } = "";
        public bool     AutoSize  { get; set; }
        public bool     Visible   { get; set; } = true;
        public bool     IsEnabled { get; set; } = true;
        public bool     Enabled   { get => IsEnabled; set => IsEnabled = value; }
        public bool     TabStop   { get; set; }
        public int      TabIndex  { get; set; }
        public LinkArea LinkArea  { get; set; }
        public AnchorStyles Anchor { get; set; }
        public bool     UseCompatibleTextRendering { get; set; }
        public System.Drawing.Font  Font          { get; set; }
        public System.Drawing.Color BackColor     { get; set; }
        public System.Drawing.Color ForeColor     { get; set; }
        public System.Drawing.Color LinkColor     { get; set; }
        public System.Drawing.Color VisitedLinkColor { get; set; }
        public System.Drawing.Point Location      { get; set; }
        public System.Drawing.Size  Size          { get; set; }
        public System.Drawing.ContentAlignment TextAlign { get; set; }
        public string   Name      { get; set; } = "";
        public ImeMode  ImeMode   { get; set; }
        public object   Parent    { get; set; }
        public object   Tag       { get; set; }
        public bool     LinkVisited { get; set; }
        public int      Left      { get; set; }
        public int      Top       { get; set; }
        public int      Width     { get => Size.Width;  set => Size = new System.Drawing.Size(value, Size.Height); }
        public int      Height    { get => Size.Height; set => Size = new System.Drawing.Size(Size.Width, value); }
        public string AccessibleDescription { get; set; } = "";
        public string AccessibleName { get; set; } = "";
        public DockStyle Dock { get; set; }
        public System.Drawing.Image Image { get; set; }
        public ContentAlignment ImageAlign { get; set; }
        public int ImageIndex { get; set; } = -1;
        public event LinkLabelLinkClickedEventHandler LinkClicked;
        protected virtual void OnLinkClicked(LinkLabelLinkClickedEventArgs e)
            => LinkClicked?.Invoke(this, e);
    }

    // ─── CheckBox ─────────────────────────────────────────────────────────────
    public class CheckBox
    {
        public string  Text      { get; set; } = "";
        public bool    Checked   { get; set; }
        public CheckState CheckState { get; set; }
        public FlatStyle FlatStyle  { get; set; }
        public object  Tag        { get; set; }
        public bool    Visible    { get; set; } = true;
        public bool    IsEnabled  { get; set; } = true;
        public bool    Enabled    { get => IsEnabled; set => IsEnabled = value; }
        public int     Width      { get; set; } = 100;
        public int     Height     { get; set; } = 24;
        public int     Top        { get; set; }
        public int     Left       { get; set; }
        public AnchorStyles Anchor { get; set; }
        public System.Drawing.Font  Font      { get; set; }
        public System.Drawing.Color BackColor { get; set; }
        public System.Drawing.Point Location  { get; set; }
        public System.Drawing.Size  Size      { get; set; }
        public string  Name     { get; set; } = "";
        public int     TabIndex { get; set; }
        public bool    TabStop  { get; set; }
        public object Parent { get; set; }
        public string AccessibleDescription { get; set; } = "";
        public string AccessibleName { get; set; } = "";
        public Appearance Appearance { get; set; }
        public System.Drawing.Image BackgroundImage { get; set; }
        public ContentAlignment CheckAlign { get; set; }
        public DockStyle Dock { get; set; }
        public System.Drawing.Image Image { get; set; }
        public ContentAlignment ImageAlign { get; set; }
        public int ImageIndex { get; set; } = -1;
        public ImeMode ImeMode { get; set; }
        public RightToLeft RightToLeft { get; set; }
        public ContentAlignment TextAlign { get; set; }
        public bool UseVisualStyleBackColor { get; set; } = true;
        public event EventHandler CheckedChanged;
        protected virtual void OnCheckedChanged(EventArgs e) => CheckedChanged?.Invoke(this, e);
        public void Refresh() { }
        public void BringToFront() { }
    }

    // ─── ComboBox ─────────────────────────────────────────────────────────────
    public class ComboBox
    {
        private readonly System.Collections.ArrayList _items = new System.Collections.ArrayList();
        public ComboBoxItemCollection Items { get; }
        public int    SelectedIndex { get; set; } = -1;
        public object SelectedItem  => SelectedIndex >= 0 && SelectedIndex < _items.Count ? _items[SelectedIndex] : null;
        public bool   IsEnabled     { get; set; } = true;
        public bool   Enabled       { get => IsEnabled; set => IsEnabled = value; }
        public ComboBoxStyle DropDownStyle { get; set; }
        public object Tag      { get; set; }
        public System.Drawing.Font Font { get; set; }
        public string Text     { get => SelectedItem?.ToString() ?? ""; set { } }
        public bool   Sorted   { get; set; }
        public int    ItemHeight { get; set; }
        public AnchorStyles Anchor { get; set; }
        public DockStyle Dock { get; set; }
        public System.Drawing.Color ForeColor { get; set; }
        public bool FormattingEnabled { get; set; }
        public object SelectedValue { get; set; }
        public string AccessibleDescription { get; set; } = "";
        public string AccessibleName { get; set; } = "";
        public bool AutoCompleteCustomSource_Stub { get; set; }
        public System.Drawing.Image BackgroundImage { get; set; }

        public System.Drawing.Point Location  { get; set; }
        public System.Drawing.Size  Size      { get; set; }
        public string  Name     { get; set; } = "";
        public int     TabIndex { get; set; }
        public bool    Visible  { get; set; } = true;

        public event EventHandler SelectedIndexChanged;
        public event EventHandler TextChanged;

        public ComboBox() { Items = new ComboBoxItemCollection(_items, this); }

        public class ComboBoxItemCollection
        {
            private readonly System.Collections.ArrayList _list;
            private readonly ComboBox _owner;
            public ComboBoxItemCollection(System.Collections.ArrayList list, ComboBox owner)
            { _list = list; _owner = owner; }
            public int Count => _list.Count;
            public object this[int i] { get => _list[i]; set => _list[i] = value; }
            public void Add(object item) { _list.Add(item); }
            public void Clear() { _list.Clear(); }
            public void AddRange(object[] items) { foreach (var it in items) _list.Add(it); }
            public bool Contains(object item) { return _list.Contains(item); }
            public int IndexOf(object item) { return _list.IndexOf(item); }
            public void Remove(object item) { _list.Remove(item); }
            public System.Collections.IEnumerator GetEnumerator() => _list.GetEnumerator();
        }

        protected virtual void OnSelectedIndexChanged(EventArgs e)
            => SelectedIndexChanged?.Invoke(this, e);
    }

    // ─── TabControl / TabPage ─────────────────────────────────────────────────
    public class TabControl
    {
        public TabPageCollection Controls { get; } = new TabPageCollection();
        public TabPageCollection TabPages => Controls;
        public int SelectedIndex { get; set; }
        public TabPage SelectedTab { get; set; }
        public object Tag { get; set; }
        public int Width { get; set; } = 300;
        public string Name { get; set; } = "";
        public int    TabIndex { get; set; }
        public System.Drawing.Size  Size     { get; set; }
        public System.Drawing.Point Location { get; set; }
        public AnchorStyles Anchor { get; set; }
        public DockStyle Dock { get; set; }
        public System.Drawing.Font  Font     { get; set; }
        public event EventHandler SelectedIndexChanged;
        public void SuspendLayout()  { }
        public void ResumeLayout(bool performLayout = true) { }
        public void PerformLayout()  { }

        public class TabPageCollection
        {
            private readonly List<TabPage> _items = new List<TabPage>();
            public int Count => _items.Count;
            public TabPage this[int i] => _items[i];
            public void Add(TabPage p) { _items.Add(p); }
            public void Remove(TabPage p) { _items.Remove(p); }
            public void Clear() { _items.Clear(); }
            public bool Contains(TabPage p) { return _items.Contains(p); }
        }
    }

    public class TabPage
    {
        public string Text     { get; set; } = "";
        public object Tag      { get; set; }
        public bool   Visible  { get; set; } = true;
        public string Name     { get; set; } = "";
        public int    TabIndex { get; set; }
        public System.Drawing.Size  Size     { get; set; }
        public System.Drawing.Point Location { get; set; }
        public AnchorStyles Anchor { get; set; }
        public Panel.ControlCollection Controls { get; } = new Panel.ControlCollection();
        public System.Drawing.Color BackColor { get; set; }
        public bool Enabled { get; set; } = true;
        public event EventHandler VisibleChanged;
        public string AccessibleDescription { get; set; } = "";
        public string AccessibleName { get; set; } = "";
        public RightToLeft RightToLeft { get; set; }
        public ImeMode ImeMode { get; set; }
        public void SuspendLayout()  { }
        public void ResumeLayout(bool performLayout = true) { }
        public void PerformLayout()  { }
    }

    // ─── GroupBox ─────────────────────────────────────────────────────────────
    public class GroupBox
    {
        public string Text     { get; set; } = "";
        public string Name     { get; set; } = "";
        public int    TabIndex { get; set; }
        public bool   TabStop  { get; set; }
        public System.Drawing.Size  Size      { get; set; }
        public System.Drawing.Point Location  { get; set; }
        public System.Drawing.Font  Font      { get; set; }
        public System.Drawing.Color BackColor { get; set; }
        public AnchorStyles Anchor { get; set; }
        public Panel.ControlCollection Controls { get; } = new Panel.ControlCollection();
        public bool Enabled { get; set; } = true;
        public bool Visible { get; set; } = true;
        public FlatStyle FlatStyle { get; set; }
        public string AccessibleDescription { get; set; } = "";
        public string AccessibleName { get; set; } = "";
        public RightToLeft RightToLeft { get; set; }
        public ImeMode ImeMode { get; set; }
        public System.Drawing.Image BackgroundImage { get; set; }
        public void SuspendLayout()  { }
        public void ResumeLayout(bool performLayout = true) { }
        public void PerformLayout()  { }
    }

    // ─── PictureBox ───────────────────────────────────────────────────────────
    public class PictureBox : System.ComponentModel.ISupportInitialize
    {
        public System.Drawing.Image Image            { get; set; }
        public System.Drawing.Image BackgroundImage  { get; set; }
        public PictureBoxSizeMode   SizeMode         { get; set; }
        public BorderStyle          BorderStyle      { get; set; }
        public System.Drawing.Size  Size             { get; set; } = new System.Drawing.Size(100, 100);
        public System.Drawing.Point Location         { get; set; }
        public System.Drawing.Color BackColor        { get; set; }
        public AnchorStyles         Anchor           { get; set; }
        public ImeMode              ImeMode          { get; set; }
        public ContextMenuStrip     ContextMenuStrip { get; set; }
        public string  Name     { get; set; } = "";
        public int     TabIndex { get; set; }
        public bool    TabStop  { get; set; }
        public bool    Visible  { get; set; } = true;
        public int     Width    { get => Size.Width;  set => Size = new System.Drawing.Size(value, Size.Height); }
        public int     Height   { get => Size.Height; set => Size = new System.Drawing.Size(Size.Width, value); }
        public object  Parent   { get; set; }
        public int     Left     { get; set; }
        public int     Top      { get; set; }
        public DockStyle Dock   { get; set; }
        public event EventHandler SizeChanged;
        public void BeginInit() { }
        public void EndInit()   { }
        public void SuspendLayout()  { }
        public void ResumeLayout(bool performLayout = true) { }
        public void SendToBack() { }
    }

    // ─── SaveFileDialog ───────────────────────────────────────────────────────
    public class SaveFileDialog
    {
        public string FileName   { get; set; } = "";
        public string Filter     { get; set; } = "";
        public string Title      { get; set; } = "";
        public bool   OverwritePrompt { get; set; }
        public SimPe.DialogResult ShowDialog() { return SimPe.DialogResult.Cancel; }
    }

    // ─── OpenFileDialog ───────────────────────────────────────────────────────
    public class OpenFileDialog
    {
        public string FileName        { get; set; } = "";
        public string[] FileNames     { get; set; } = System.Array.Empty<string>();
        public string Filter          { get; set; } = "";
        public string Title           { get; set; } = "";
        public bool   CheckFileExists { get; set; }
        public bool   Multiselect     { get; set; }
        public int    FilterIndex     { get; set; } = 1;
        public SimPe.DialogResult ShowDialog() { return SimPe.DialogResult.Cancel; }
        public SimPe.DialogResult ShowDialog(object owner) { return SimPe.DialogResult.Cancel; }
    }

    // ─── ContextMenuStrip ────────────────────────────────────────────────────
    public class ContextMenuStrip
    {
        public ToolStripItemCollection Items { get; } = new ToolStripItemCollection();
        public string Name { get; set; } = "";
        public string Text { get; set; } = "";
        public System.Drawing.Size Size { get; set; }
        public event System.ComponentModel.CancelEventHandler Opening;
        public event EventHandler VisibleChanged;
        public ContextMenuStrip() { }
        public ContextMenuStrip(System.ComponentModel.IContainer container) { }
        public void SuspendLayout() { }
        public void ResumeLayout(bool b = false) { }
    }

    public class ToolStripItemCollection
    {
        private readonly List<ToolStripItem> _items = new List<ToolStripItem>();
        public int Count => _items.Count;
        public ToolStripItem this[int i] => _items[i];
        public void Add(ToolStripItem item) { _items.Add(item); }
        public void AddRange(ToolStripItem[] items) { foreach (var it in items) _items.Add(it); }
        public void Clear() { _items.Clear(); }
    }

    // ─── ListBox ─────────────────────────────────────────────────────────────
    public class ListBox
    {
        private readonly System.Collections.ArrayList _items = new System.Collections.ArrayList();
        public ListBoxItemCollection Items { get; }
        public int  SelectedIndex  { get; set; } = -1;
        public object SelectedItem => SelectedIndex >= 0 && SelectedIndex < _items.Count ? _items[SelectedIndex] : null;
        public bool HorizontalScrollbar { get; set; }
        public bool IntegralHeight { get; set; }
        public bool Sorted         { get; set; }
        public bool AllowDrop      { get; set; }
        public SelectionMode SelectionMode { get; set; }
        public System.Drawing.Font  Font     { get; set; }
        public DockStyle            Dock     { get; set; }
        public AnchorStyles         Anchor   { get; set; }
        public System.Drawing.Size  Size     { get; set; }
        public System.Drawing.Point Location { get; set; }
        public string  Name     { get; set; } = "";
        public int     TabIndex { get; set; }
        public bool    Visible  { get; set; } = true;
        public bool    Enabled  { get; set; } = true;
        public object  Tag      { get; set; }
        public ContextMenuStrip ContextMenuStrip { get; set; }
        public string AccessibleDescription { get; set; } = "";
        public string AccessibleName { get; set; } = "";
        public AccessibleRole AccessibleRole { get; set; }
        public System.Drawing.Image BackgroundImage { get; set; }
        public BorderStyle BorderStyle { get; set; }
        public System.Drawing.Color ForeColor { get; set; }
        public bool FormattingEnabled { get; set; }
        public ImeMode ImeMode { get; set; }
        public RightToLeft RightToLeft { get; set; }
        public bool ScrollAlwaysVisible { get; set; }
        public int ItemHeight { get; set; }
        public bool MultiColumn { get; set; }
        public event EventHandler SelectedIndexChanged;
        public event DragEventHandler DragDrop;
        public event DragEventHandler DragEnter;

        public SelectedItemCollection SelectedItems { get; }

        public ListBox()
        {
            Items = new ListBoxItemCollection(_items, this);
            SelectedItems = new SelectedItemCollection(_items);
        }

        public void BeginUpdate() { }
        public void EndUpdate()   { if (Sorted) _items.Sort(); }
        public void ClearSelected() { }
        public object BeginInvoke(System.Delegate method, object[] args = null)
        {
            Avalonia.Threading.Dispatcher.UIThread.Post(() => method.DynamicInvoke(args));
            return null;
        }

        public class ListBoxItemCollection
        {
            private readonly System.Collections.ArrayList _list;
            private readonly ListBox _owner;
            public ListBoxItemCollection(System.Collections.ArrayList list, ListBox owner)
            { _list = list; _owner = owner; }
            public int Count => _list.Count;
            public object this[int i] { get => _list[i]; set => _list[i] = value; }
            public void Add(object item)    { _list.Add(item); }
            public void Clear()             { _list.Clear(); }
            public void Remove(object item) { _list.Remove(item); }
            public bool Contains(object item) { return _list.Contains(item); }
            public int IndexOf(object item) { return _list.IndexOf(item); }
            public void AddRange(object[] items) { foreach (var it in items) _list.Add(it); }
            public void RemoveAt(int index) { _list.RemoveAt(index); }
        }

        public class SelectedItemCollection : System.Collections.IEnumerable
        {
            private readonly System.Collections.ArrayList _all;
            public SelectedItemCollection(System.Collections.ArrayList all) { _all = all; }
            public int Count => 0; // stub — no actual selection tracking
            public object this[int i] => null;
            public System.Collections.IEnumerator GetEnumerator()
                => System.Array.Empty<object>().GetEnumerator();
        }
    }

    // ─── IDataObject ─────────────────────────────────────────────────────────
    public interface IDataObject
    {
        object GetData(string format);
        object GetData(Type format);
        bool   GetDataPresent(string format);
        bool   GetDataPresent(Type format);
    }

    // ─── DragEventArgs ────────────────────────────────────────────────────────
    public class DragEventArgs : EventArgs
    {
        public IDataObject     Data   { get; }
        public int             X      { get; }
        public int             Y      { get; }
        public DragDropEffects Effect { get; set; }
        public DragEventArgs(IDataObject data, int x, int y) { Data = data; X = x; Y = y; }
    }

    // ─── ListViewEx ───────────────────────────────────────────────────────────
    /// <summary>
    /// Avalonia-compatible stub for the WinForms ListViewEx with embedded controls.
    /// All P/Invoke, WM_PAINT overrides, and layout code are removed.
    /// AddEmbeddedControl is a no-op; callers (BoneListViewItem, MeshListViewItem)
    /// use it only as a visual hint that is not needed in Avalonia.
    /// </summary>
    public class ListViewEx
    {
        public ListViewEx() { }

        public ColumnHeaderCollection Columns { get; } = new ColumnHeaderCollection();
        public ListViewItemCollection Items   { get; } = new ListViewItemCollection();

        public SelectedListViewItemCollection SelectedItems
            => new SelectedListViewItemCollection(Items);

        public View              View         { get; set; } = View.Details;
        public bool              FullRowSelect { get; set; }
        public bool              HideSelection { get; set; }
        public ColumnHeaderStyle HeaderStyle  { get; set; }
        public BorderStyle       BorderStyle  { get; set; }
        public System.Collections.IComparer ListViewItemSorter { get; set; }

        // no-op: embedded controls are a WinForms-only concept
        public void AddEmbeddedControl(Avalonia.Controls.Control c, int column, int row) { }

        public void RemoveEmbeddedControl(Avalonia.Controls.Control c) { }

        public Avalonia.Controls.Control GetEmbeddedControl(int col, int row) => null;
    }

    // === Top-level ControlCollection (also used by Control/UserControl stubs) ===
    public class ControlCollection
    {
        private readonly List<object> _list = new List<object>();
        public int Count => _list.Count;
        public void Add(object c)    { _list.Add(c); }
        public void Remove(object c) { _list.Remove(c); }
        public void Clear()          { _list.Clear(); }
    }

    // === Control / UserControl stubs ===
    public class Control
    {
        public bool   Visible  { get; set; } = true;
        public bool   Enabled  { get; set; } = true;
        public string Name     { get; set; }
        public System.Drawing.Size  Size     { get; set; }
        public System.Drawing.Point Location { get; set; }
        public System.Drawing.Color BackColor { get; set; }
        public System.Drawing.Color ForeColor { get; set; }
        public System.Drawing.Font  Font     { get; set; }
        public DockStyle Dock    { get; set; }
        public int     TabIndex  { get; set; }
        public object  Tag       { get; set; }
        public object  Cursor    { get; set; }
        public bool    DesignMode => false;
        public ControlCollection Controls { get; } = new ControlCollection();
        public IntPtr  Handle    => IntPtr.Zero;
        public string AccessibleDescription { get; set; } = "";
        public string AccessibleName { get; set; } = "";
        public bool AutoSize { get; set; }
        public System.Drawing.Image BackgroundImage { get; set; }
        public AnchorStyles Anchor { get; set; }
        public bool DoubleBuffered { get; set; }
        public ImeMode ImeMode { get; set; }
        public RightToLeft RightToLeft { get; set; }
        public event EventHandler VisibleChanged;
        public event EventHandler Resize;
        public event EventHandler HandleCreated;
        public event EventHandler Click;
        public event EventHandler DoubleClick;
        public void SuspendLayout() { }
        public void ResumeLayout(bool b = false) { }
        public void PerformLayout() { }
        public void Refresh() { }
        public void Invoke(System.Delegate d, object[] args = null)      => Avalonia.Threading.Dispatcher.UIThread.Post(() => d.DynamicInvoke(args));
        public void BeginInvoke(System.Delegate d, object[] args = null) => Avalonia.Threading.Dispatcher.UIThread.Post(() => d.DynamicInvoke(args));
        protected virtual void SetStyle(ControlStyles s, bool v) { }
        protected virtual void WndProc(ref Message m) { }
        protected virtual void Dispose(bool disposing) { }
        protected virtual void OnResize(EventArgs e) { }
    }

    public class UserControl : Control
    {
        public System.Drawing.SizeF AutoScaleDimensions { get; set; }
        public AutoScaleMode AutoScaleMode { get; set; }
    }

    public enum AutoScaleMode { None, Font, Dpi, Inherit }

    // === ToolStrip stubs ===
    public class ToolStrip
    {
        public ToolStripItemCollection Items { get; } = new ToolStripItemCollection();
        public ToolStripGripStyle   GripStyle         { get; set; }
        public string               Name              { get; set; }
        public System.Drawing.Size  Size              { get; set; }
        public int                  TabIndex          { get; set; }
        public string               Text              { get; set; }
        public bool                 Visible           { get; set; } = true;
        public System.Drawing.Font  Font              { get; set; }
        public System.Drawing.Point Location          { get; set; }
        public ToolStripRenderer    Renderer          { get; set; }
        public System.Drawing.Size  ImageScalingSize  { get; set; } = new System.Drawing.Size(16, 16);
        public ToolStripLayoutStyle LayoutStyle       { get; set; }
        public bool                 ShowItemToolTips  { get; set; } = true;
        public DockStyle            Dock              { get; set; }
        public void SuspendLayout() { }
        public void ResumeLayout(bool b = false) { }
        public void PerformLayout() { }
        public void Refresh() { }
    }

    public class StatusStrip : ToolStrip { }

    public class ToolStripButton : ToolStripItem
    {
        public bool                      Checked       { get; set; }
        public bool                      CheckOnClick  { get; set; }
        public new System.Drawing.Image  Image         { get; set; }
        public ToolStripItemImageScaling ImageScaling  { get; set; }
        public TextImageRelation         TextImageRelation { get; set; }
        public ToolStripItemAlignment    Alignment     { get; set; }
        public CheckState                CheckState    { get; set; }
        public ToolStripItemDisplayStyle DisplayStyle  { get; set; }
        public System.Drawing.Font       Font          { get; set; }
    }

    public class ToolStripProgressBar : ToolStripItem
    {
        public int Value { get; set; }
        public int Maximum { get; set; }
        public int Minimum { get; set; }
    }

    public class ToolStripStatusLabel : ToolStripItem
    {
        public System.Drawing.ContentAlignment TextAlign { get; set; }
        public ToolStripStatusLabelBorderSides BorderSides { get; set; }
        public bool Spring { get; set; }
    }

    public class ToolStripSeparator : ToolStripItem { }

    public enum ToolStripGripStyle { Visible, Hidden }
    public enum ToolStripItemImageScaling { SizeToFit, None }
    public enum TextImageRelation { Overlay, ImageAboveText, TextAboveImage, ImageBeforeText, TextBeforeImage }
    public enum ToolStripStatusLabelBorderSides { None = 0, Left = 1, Top = 2, Right = 4, Bottom = 8, All = 15 }
    public enum ToolStripLayoutStyle { HorizontalStackWithOverflow = 0, VerticalStackWithOverflow, StackWithOverflow, Flow, Table }
    public enum ToolStripItemAlignment { Left = 0, Right = 1 }
    public enum ToolStripItemDisplayStyle { None = 0, Text, Image, ImageAndText }

    // === Windows Message stub ===
    public struct Message
    {
        public IntPtr HWnd;
        public uint   Msg;
        public IntPtr WParam;
        public IntPtr LParam;
    }

    // === Virtual ListView event args ===
    public class CacheVirtualItemsEventArgs : EventArgs
    {
        public int StartIndex { get; }
        public int EndIndex   { get; }
        public CacheVirtualItemsEventArgs(int start, int end) { StartIndex = start; EndIndex = end; }
    }

    public class RetrieveVirtualItemEventArgs : EventArgs
    {
        public int       ItemIndex { get; }
        public ListViewItem Item   { get; set; }
        public RetrieveVirtualItemEventArgs(int index) { ItemIndex = index; }
    }

    public class SearchForVirtualItemEventArgs : EventArgs
    {
        public int    StartIndex { get; }
        public string Text       { get; }
        public int    Index      { get; set; }
        public SearchForVirtualItemEventArgs(int start, string text) { StartIndex = start; Text = text; }
    }

    public class ListViewVirtualItemsSelectionRangeChangedEventArgs : EventArgs
    {
        public int  StartIndex { get; }
        public int  EndIndex   { get; }
        public bool IsSelected { get; }
        public ListViewVirtualItemsSelectionRangeChangedEventArgs(int start, int end, bool sel)
        { StartIndex = start; EndIndex = end; IsSelected = sel; }
    }

    // === Mouse stubs ===
    public class MouseEventArgs : EventArgs
    {
        public int          X      { get; }
        public int          Y      { get; }
        public MouseButtons Button { get; }
        public int          Clicks { get; }
        public int          Delta  { get; }
        public MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
        { Button = button; Clicks = clicks; X = x; Y = y; Delta = delta; }
    }
    public delegate void MouseEventHandler(object sender, MouseEventArgs e);
    public enum MouseButtons { None = 0, Left = 1048576, Right = 2097152, Middle = 4194304, XButton1 = 8388608, XButton2 = 16777216 }

    // === Keyboard stubs ===
    public class KeyEventArgs : EventArgs
    {
        public Keys KeyCode { get; }
        public bool Control { get; }
        public bool Shift   { get; }
        public bool Alt     { get; }
        public bool Handled { get; set; }
        public KeyEventArgs(Keys keyCode) { KeyCode = keyCode; }
    }
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);
    public enum Keys
    {
        None = 0, A = 65, B = 66, C = 67, D = 68, E = 69, F = 70, G = 71, H = 72, I = 73,
        J = 74, K = 75, L = 76, M = 77, N = 78, O = 79, P = 80, Q = 81, R = 82, S = 83,
        T = 84, U = 85, V = 86, W = 87, X = 88, Y = 89, Z = 90,
        Delete = 46, Return = 13, Enter = 13, Escape = 27, F5 = 116,
        Control = 131072, Shift = 65536, Alt = 262144,
        Down = 40, Up = 38, Left = 37, Right = 39,
        Home = 36, End = 35, PageUp = 33, PageDown = 34,
        Tab = 9, Back = 8, Insert = 45,
        F1 = 112, F2 = 113, F3 = 114, F4 = 115, F6 = 117, F7 = 118, F8 = 119, F9 = 120, F10 = 121,
        D0 = 48, D1 = 49, D2 = 50, D3 = 51, D4 = 52, D5 = 53, D6 = 54, D7 = 55, D8 = 56, D9 = 57
    }

    // === Column click ===
    public class ColumnClickEventArgs : EventArgs
    {
        public int Column { get; }
        public ColumnClickEventArgs(int column) { Column = column; }
    }

    // === Misc dialogs / controls ===
    public class FolderBrowserDialog : IDisposable
    {
        public string SelectedPath { get; set; } = "";
        public string Description  { get; set; } = "";
        public bool   ShowNewFolderButton { get; set; } = true;
        public SimPe.DialogResult ShowDialog() => SimPe.DialogResult.Cancel;
        public SimPe.DialogResult ShowDialog(object owner) => SimPe.DialogResult.Cancel;
        public void Dispose() { }
    }

    public class ProgressBar
    {
        public int    Value    { get; set; }
        public int    Maximum  { get; set; }
        public int    Minimum  { get; set; }
        public bool   Visible  { get; set; } = true;
        public string Name     { get; set; }
        public int    TabIndex { get; set; }
        public System.Drawing.Color BackColor { get; set; }
        public System.Drawing.Size  Size     { get; set; }
        public System.Drawing.Point Location { get; set; }
    }

    public enum Shortcut { None = 0 }

    public enum ImageLayout { None, Tile, Center, Stretch, Zoom }

    public enum ControlStyles
    {
        SupportsTransparentBackColor = 1,
        AllPaintingInWmPaint = 2,
        OptimizedDoubleBuffer = 4,
        UserPaint = 8,
        DoubleBuffer = 16,
        ResizeRedraw = 32
    }

    public static class Application
    {
        public static void DoEvents() { }
        public static void EnableVisualStyles() { }
        public static void Run(Form form) { }
        public static void ExitThread() { }
    }

    public static class Help
    {
        public static void ShowHelp(object parent, string url) { }
        public static void ShowHelp(object parent, string url, string keyword) { }
    }

    // === MenuStrip / MenuStrip stubs ===
    public class MenuStrip : ToolStrip { }

    // === Splitter stub ===
    public class Splitter
    {
        public DockStyle Dock { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public System.Drawing.Size Size { get; set; }
        public System.Drawing.Point Location { get; set; }
        public string Name { get; set; } = "";
        public int TabIndex { get; set; }
        public bool TabStop { get; set; }
        public System.Drawing.Color BackColor { get; set; }
    }

    // === RadioButton stub ===
    public class RadioButton
    {
        public string Text { get; set; } = "";
        public bool Checked { get; set; }
        public bool AutoCheck { get; set; } = true;
        public System.Drawing.Size Size { get; set; }
        public System.Drawing.Point Location { get; set; }
        public System.Drawing.Font Font { get; set; }
        public string Name { get; set; } = "";
        public int TabIndex { get; set; }
        public bool Visible { get; set; } = true;
        public bool Enabled { get; set; } = true;
        public AnchorStyles Anchor { get; set; }
        public object Tag { get; set; }
        public FlatStyle FlatStyle { get; set; }
        public bool TabStop { get; set; } = true;
        public event EventHandler CheckedChanged;
    }

    // === PropertyGrid stub ===
    public class PropertyGrid
    {
        public object SelectedObject { get; set; }
        public System.Drawing.Size Size { get; set; }
        public System.Drawing.Point Location { get; set; }
        public DockStyle Dock { get; set; }
        public string Name { get; set; } = "";
        public int TabIndex { get; set; }
        public bool Visible { get; set; } = true;
        public System.Drawing.Color BackColor { get; set; }
        public System.Drawing.Color CommandsBackColor { get; set; }
        public System.Drawing.Color LineColor { get; set; }
        public PropertySort PropertySort { get; set; }
        public bool ToolbarVisible { get; set; } = true;
        public event PropertyValueChangedEventHandler PropertyValueChanged;
    }
    public class PropertyValueChangedEventArgs : EventArgs
    {
        public object ChangedItem { get; }
        public object OldValue { get; }
    }
    public delegate void PropertyValueChangedEventHandler(object sender, PropertyValueChangedEventArgs e);

    // === RichTextBox stub ===
    public class RichTextBox
    {
        public string Text { get; set; } = "";
        public bool ReadOnly { get; set; }
        public DockStyle Dock { get; set; }
        public System.Drawing.Size Size { get; set; }
        public System.Drawing.Point Location { get; set; }
        public System.Drawing.Font Font { get; set; }
        public System.Drawing.Color BackColor { get; set; }
        public string Name { get; set; } = "";
        public int TabIndex { get; set; }
        public bool Visible { get; set; } = true;
        public RichTextBoxScrollBars ScrollBars { get; set; }
        public BorderStyle BorderStyle { get; set; }
        public AnchorStyles Anchor { get; set; }
        public object Cursor { get; set; }
        public int Height { get; set; }
        public int Top { get; set; }
        public string Rtf { get; set; } = "";
        public event EventHandler TextChanged;
        public event EventHandler Enter;
        public event LinkClickedEventHandler LinkClicked;
        public void BringToFront() { }
    }

    // === Timer stub ===
    public class Timer : IDisposable
    {
        public bool Enabled { get; set; }
        public int Interval { get; set; } = 100;
        public event EventHandler Tick;
        public void Start() { Enabled = true; }
        public void Stop()  { Enabled = false; }
        public Timer() { }
        public Timer(System.ComponentModel.IContainer container) { }
        public void Dispose() { Enabled = false; }
    }

    // === Form closed events ===
    public class FormClosedEventArgs : EventArgs { }
    public delegate void FormClosedEventHandler(object sender, FormClosedEventArgs e);

    // === Paint ===
    public class PaintEventArgs : EventArgs
    {
        public System.Drawing.Graphics Graphics => null;
    }

    // === DockPaddingEdges ===
    public class DockPaddingEdges
    {
        public int All    { get; set; }
        public int Left   { get; set; }
        public int Right  { get; set; }
        public int Top    { get; set; }
        public int Bottom { get; set; }
    }

    // === FlowLayoutPanel stub ===
    public class FlowLayoutPanel : Panel { }

    // === ToolStripContainer stub ===
    public class ToolStripContainer
    {
        public string Name { get; set; } = "";
        public System.Drawing.Size Size { get; set; }
        public System.Drawing.Point Location { get; set; }
        public int TabIndex { get; set; }
        public Panel.ControlCollection Controls { get; } = new Panel.ControlCollection();
        public ToolStripPanel TopToolStripPanel { get; } = new ToolStripPanel();
        public ToolStripPanel BottomToolStripPanel { get; } = new ToolStripPanel();
        public ToolStripPanel LeftToolStripPanel { get; } = new ToolStripPanel();
        public ToolStripPanel RightToolStripPanel { get; } = new ToolStripPanel();
        public ToolStripContentPanel ContentPanel { get; } = new ToolStripContentPanel();
        public DockStyle Dock { get; set; }
        public bool Visible { get; set; } = true;
        public void SuspendLayout() { }
        public void ResumeLayout(bool b = false) { }
        public void PerformLayout() { }
    }

    public class ToolStripPanel
    {
        public Panel.ControlCollection Controls { get; } = new Panel.ControlCollection();
        public void SuspendLayout() { }
        public void ResumeLayout(bool b = false) { }
        public void Join(ToolStrip ts, int row = 0) { }
    }

    public class ToolStripContentPanel : Panel { }

    // === LinkClickedEventArgs (for RichTextBox.LinkClicked) ===
    public class LinkClickedEventArgs : EventArgs
    {
        public string LinkText { get; }
        public LinkClickedEventArgs(string linkText) { LinkText = linkText; }
        public LinkClickedEventArgs() { LinkText = ""; }
    }
    public delegate void LinkClickedEventHandler(object sender, LinkClickedEventArgs e);

    // === WebBrowser stub ===
    public class WebBrowser
    {
        public string Url { get; set; } = "";
        public bool IsWebBrowserContextMenuEnabled { get; set; } = true;
        public bool AllowNavigation { get; set; } = true;
        public bool AllowWebBrowserDrop { get; set; } = false;
        public bool WebBrowserShortcutsEnabled { get; set; } = true;
        public bool Visible { get; set; } = true;
        public string Name { get; set; } = "";
        public System.Drawing.Size Size { get; set; }
        public System.Drawing.Size MinimumSize { get; set; }
        public System.Drawing.Point Location { get; set; }
        public int TabIndex { get; set; }
        public AnchorStyles Anchor { get; set; }
        public DockStyle Dock { get; set; }
        public void Navigate(string url) { }
        public void Navigate(Uri url, bool newWindow = false) { }
        public void DocumentText_Set(string html) { }
        public string DocumentText { get; set; } = "";
        public event WebBrowserNavigatedEventHandler Navigated;
        public event WebBrowserNavigatingEventHandler Navigating;
    }

    public class WebBrowserNavigatedEventArgs : EventArgs
    {
        public Uri Url { get; }
        public WebBrowserNavigatedEventArgs(Uri url) { Url = url; }
        public WebBrowserNavigatedEventArgs() { Url = new Uri("about:blank"); }
    }
    public delegate void WebBrowserNavigatedEventHandler(object sender, WebBrowserNavigatedEventArgs e);

    public class WebBrowserNavigatingEventArgs : System.ComponentModel.CancelEventArgs
    {
        public Uri    Url             { get; }
        public string TargetFrameName { get; }
        public WebBrowserNavigatingEventArgs(Uri url, string targetFrame) { Url = url; TargetFrameName = targetFrame; }
        public WebBrowserNavigatingEventArgs() { Url = new Uri("about:blank"); TargetFrameName = ""; }
    }
    public delegate void WebBrowserNavigatingEventHandler(object sender, WebBrowserNavigatingEventArgs e);
}
