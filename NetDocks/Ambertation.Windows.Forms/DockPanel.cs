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

// Ported from WinForms DockPanel.
// Original: 1350-line WinForms NCUserControl with custom caption rendering,
//   drag-dock, floating windows, tab management, and binary serialization.
// On Avalonia: base class is NCUserControl (Avalonia.Controls.Control).
//   Rendering will be implemented in Render(DrawingContext) in a future pass.
//   Drag-dock and floating use Avalonia pointer events in a future pass.
//   The fixed Mac layout means docking interactions are no-op for now.

using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using Avalonia.Controls;

namespace Ambertation.Windows.Forms;

/// <summary>
/// A dockable panel that can be assigned to a DockContainer.
/// Ported from WinForms to Avalonia; rendering and docking interactions
/// will be completed in a future pass.
/// </summary>
public class DockPanel : NCUserControl
{
    // ── Closing event ─────────────────────────────────────────────────────

    /// <summary>Arguments for the Closing event. Set Cancel = true to prevent close.</summary>
    public class DockPanelClosingEvent : EventArgs
    {
        public bool Cancel { get; set; }
    }

    /// <summary>Delegate type for the DockPanel.Closing event.</summary>
    public delegate void ClosingHandler(object sender, DockPanelClosingEvent e);

    // ── State ─────────────────────────────────────────────────────────────

    private string _text          = "";
    private string _btext         = "";
    private string _tabText       = "";
    private bool   _isOpen        = true;
    private bool   _collapsed;
    private DockContainer _dockContainer;
    private DockStyle _dock;
    private Guid?  _guid;

    // ── Properties ────────────────────────────────────────────────────────

    public new string Name
    {
        get => base.Name ?? "";
        set => base.Name = value;
    }

    /// <summary>Caption text shown in the dock panel header.</summary>
    public new string Text
    {
        get => _text;
        set { if (_text != value) { _text = value; if (_btext == "") _btext = _text; NCRefresh(); } }
    }

    /// <summary>The visible caption text (same as Text in the base class).</summary>
    public virtual string CaptionText
    {
        get => _text;
        set => Text = value;
    }

    public virtual string ButtonText
    {
        get => _btext;
        set { if (_btext != value) { _btext = value; NCRefresh(); } }
    }

    public string TabText
    {
        get => _tabText != "" ? _tabText : _text;
        set => _tabText = value;
    }

    /// <summary>Icon shown in the panel tab.</summary>
    public System.Drawing.Image Image    { get; set; }
    public System.Drawing.Image TabImage { get; set; }

    public bool IsOpen
    {
        get => _isOpen;
        set { if (_isOpen != value) { _isOpen = value; NCRefresh(); } }
    }

    public bool Collapsed
    {
        get => _collapsed;
        set { if (_collapsed != value) { _collapsed = value; NCRefresh(); } }
    }

    public bool IsDocked   => _dockContainer != null;
    public bool Floating   => false;   // floating not yet implemented on Mac
    public bool IsFloating => false;

    public object ActiveDocument => null;

    /// <summary>The DockContainer this panel belongs to.</summary>
    public DockContainer DockContainer
    {
        get => _dockContainer;
        set { if (value != _dockContainer) DockControl(value); }
    }

    /// <summary>The BaseDockManager that manages this panel (via its container).</summary>
    /// <summary>Dock position within the container.</summary>
    public DockStyle Dock
    {
        get => _dock;
        set { if (_dock != value) { _dock = value; OnDockChanged(EventArgs.Empty); NCRefresh(); } }
    }

    public new object Parent { get; set; }

    public System.Drawing.Size AutoScrollMinSize { get; set; }
    public System.Drawing.Size FloatingSize      { get; set; }
    public System.Drawing.Size MinimumSize       { get; set; }

    public bool CanUndock  { get; set; } = true;
    public bool CanResize  { get; set; } = true;
    public int  UndockByCaptionThreshold { get; set; } = 8;

    public virtual bool ShowCollapseButton { get; set; } = true;
    public virtual bool ShowCloseButton    { get; set; } = true;

    // Allow-flags: no-op on Mac (docking/floating interactions not implemented)
    public bool AllowClose       { get; set; } = true;
    public bool AllowCollapse    { get; set; } = true;
    public bool AllowDockBottom  { get; set; } = true;
    public bool AllowDockCenter  { get; set; } = true;
    public bool AllowDockLeft    { get; set; } = true;
    public bool AllowDockRight   { get; set; } = true;
    public bool AllowDockTop     { get; set; } = true;
    public bool AllowFloat       { get; set; } = true;

    // Visible: WinForms compat; Avalonia uses IsVisible
    public new bool Visible
    {
        get => IsVisible;
        set => IsVisible = value;
    }

    // Manager: settable proxy so WinForms designer code can assign it
    public BaseDockManager Manager
    {
        get => _dockContainer?.Manager;
        set { /* no-op: manager is derived from DockContainer on Mac */ }
    }

    /// <summary>True when this panel is hosted inside a floating container.</summary>
    public bool FloatContainer { get; set; }

    /// <summary>Caption highlight state (focused/normal).</summary>
    public CaptionState CaptionState { get; set; } = CaptionState.Normal;

    /// <summary>Button orientation for collapsed dock-bar buttons, derived from Dock.</summary>
    public ButtonOrientation BestOrientation
    {
        get
        {
            if (Dock == DockStyle.Bottom) return ButtonOrientation.Top;
            if (Dock == DockStyle.Left)   return ButtonOrientation.Right;
            if (Dock == DockStyle.Top)    return ButtonOrientation.Bottom;
            return ButtonOrientation.Left;
        }
    }

    /// <summary>Client area rectangle (zero-based, integer pixels).</summary>
    public System.Drawing.Rectangle ClientRectangle
        => new System.Drawing.Rectangle(0, 0, Width, Height);

    public virtual bool OnlyChild => _dockContainer != null && _dockContainer.OneChild;

    /// <summary>Whether this panel is shown as a separate group in a collapsed dock bar.</summary>
    public bool SeperateInDockBar { get; set; }

    /// <summary>Unique identifier for layout serialization.</summary>
    public Guid Guid
    {
        get
        {
            if (!_guid.HasValue) _guid = System.Guid.NewGuid();
            return _guid.Value;
        }
    }

    // Controls collection — holds child controls (actual Avalonia children in full port).
    public DockPanelControlCollection Controls { get; } = new DockPanelControlCollection();

    // ── Constructors ──────────────────────────────────────────────────────

    public DockPanel()
    {
        ManagerSingelton.Global.AddPanel(this);
    }

    /// <summary>Construct and immediately assign to a DockContainer (or DockManager).</summary>
    internal DockPanel(DockContainer container) : this()
    {
        if (container != null)
            DockControl(container);
    }

    // ── Operations ────────────────────────────────────────────────────────

    public void Open()            { IsOpen    = true;  }
    public new void Close()       { IsOpen    = false; }
    public void Expand()          { Collapsed = false; }
    public void Expand(bool show) { Collapsed = !show; }
    public void Collapse()        { Collapsed = true;  }
    public void EnsureVisible()   { IsOpen = true; }
    public void OpenFloating()    { }   // floating not yet implemented

    public void SuspendLayout()             { }
    public void ResumeLayout(bool b = true) { }
    public void PerformLayout()             { }

    /// <summary>Assign this panel to a DockContainer.</summary>
    public void DockControl(DockContainer container)
    {
        _dockContainer?.Remove(this);
        _dockContainer = container;
        _dockContainer?.Add(this);
        NCRefresh();
    }

    /// <summary>Called when the Dock property changes.</summary>
    protected virtual void OnDockChanged(EventArgs e) { }

    // ── Floating stubs (not implemented on Mac) ───────────────────────────

    /// <summary>Unregister from a floating form. No-op on Mac.</summary>
    internal void UnFloat(DockPanelFloatingForm form) { }

    /// <summary>Close triggered by the floating form closing. No-op on Mac.</summary>
    internal void CloseFromForm()             { Close(); }

    /// <summary>Refresh margins after a layout change. No-op on Mac.</summary>
    internal void RefreshMargin()             { }

    // ── Serialization stubs ───────────────────────────────────────────────

    internal void Serialize(BinaryWriter writer) { }

    internal void Deserialize(BinaryReader reader,
        Dictionary<string, DockManager.DockContainerDescriptor> docks,
        uint version) { }

    // ── Events ────────────────────────────────────────────────────────────

    public new event EventHandler VisibleChanged;
    public event ClosingHandler   Closing;

    protected void RaiseClosing(DockPanelClosingEvent e) => Closing?.Invoke(this, e);
    protected void RaiseVisibleChanged()                  => VisibleChanged?.Invoke(this, EventArgs.Empty);

    // ── Rendering ─────────────────────────────────────────────────────────

    public override void Render(Avalonia.Media.DrawingContext context)
    {
        // Dock panel caption rendering will be implemented here in a future pass,
        // using Avalonia DrawingContext to draw the header bar, close/collapse buttons.
        base.Render(context);
    }
}
