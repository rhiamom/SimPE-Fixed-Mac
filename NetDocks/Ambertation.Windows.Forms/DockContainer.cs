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

// Ported from WinForms DockContainer.
// Original: 1465-line WinForms NCUserControl : IButtonContainer with WndProc-based
//   resize, drag-docking, WinForms Controls collection management.
// On Avalonia: base changed to NCUserControl (Avalonia.Controls.Control).
//   IButtonContainer removed (WinForms designer interface).
//   WndProc, OnNcPaint, OnMouseUp, WinForms Controls overrides removed.
//   Panel registry (Add/Remove/GetPanels) kept.
//   Drag-dock and resize interactions are no-op on Mac.

using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;

namespace Ambertation.Windows.Forms;

/// <summary>
/// A container that DockPanels can be assigned to.
/// Ported from WinForms DockContainer (NCUserControl : IButtonContainer).
/// On Avalonia, rendering and docking interactions are no-op.
/// </summary>
public class DockContainer : NCUserControl
{
    // ── State ─────────────────────────────────────────────────────────────

    protected List<DockContainer> containers;
    protected List<DockPanel>    panels;
    protected BaseDockManager    manager;

    private bool   _collapsed;
    private DockPanel _highlight;

    // ── Properties ────────────────────────────────────────────────────────

    public BaseDockManager Manager
    {
        get => manager;
        set => SetManager(value);
    }

    public DockManager DockManager => manager as DockManager;

    public bool Collapsed
    {
        get => _collapsed;
        set { if (_collapsed != value) { _collapsed = value; NCRefresh(); } }
    }

    public bool Expanded => !_collapsed;

    public DockPanel Highlight => _highlight;

    public virtual bool OneChild => panels.Count <= 1;

    public DockStyle Dock   { get; set; }
    public object TabText  { get; set; } = "";
    public object TabImage { get; set; }

    public bool HideSingleButton { get; set; } = true;

    public int MinimumDockSize => 20;

    // WinForms designer compat
    public System.Drawing.Size MinimumSize { get; set; }
    public bool NoCleanup { get; set; }

    // Controls collection — wraps child panels for enumeration by callers.
    public DockPanelControlCollection Controls { get; } = new DockPanelControlCollection();

    // ── Constructors ──────────────────────────────────────────────────────

    public DockContainer()
        : this(null)
    {
    }

    internal DockContainer(BaseDockManager manager)
    {
        containers = new List<DockContainer>();
        panels     = new List<DockPanel>();
        SetManager(manager);
    }

    // ── Manager wiring ────────────────────────────────────────────────────

    internal void SetManager(BaseDockManager m)
    {
        manager = m;
    }

    // ── Panel registry ────────────────────────────────────────────────────

    /// <summary>Add a DockPanel to this container's panel list.</summary>
    public void Add(DockPanel dp)
    {
        if (dp == null || panels.Contains(dp)) return;
        panels.Add(dp);
        if (_highlight == null) _highlight = dp;
        Controls.Add(dp);
        PanelCollectionChanged?.Invoke(this, EventArgs.Empty);
        NCRefresh();
    }

    /// <summary>Remove a DockPanel from this container's panel list.</summary>
    public void Remove(DockPanel dp)
    {
        if (dp == null) return;
        panels.Remove(dp);
        if (_highlight == dp) _highlight = panels.Count > 0 ? panels[0] : null;
        Controls.Remove(dp);
        PanelCollectionChanged?.Invoke(this, EventArgs.Empty);
        NCRefresh();
    }

    /// <summary>Returns the DockPanels directly docked in this container (not children).</summary>
    public List<DockPanel> GetDockedPanels() => new List<DockPanel>(panels);

    /// <summary>Returns all DockPanels registered in this container and its children.</summary>
    public List<DockPanel> GetPanels()
    {
        var dict = new Dictionary<string, DockPanel>();
        GetPanels(dict);
        return new List<DockPanel>(dict.Values);
    }

    protected virtual void GetPanels(Dictionary<string, DockPanel> list)
    {
        foreach (DockPanel dp in panels)
        {
            if (dp.Name == "") dp.Name = "dp_" + list.Count;
            list[dp.Name] = dp;
        }
        foreach (DockContainer dc in containers)
            dc.GetPanels(list);
    }

    // ── Collapse helpers ──────────────────────────────────────────────────

    public virtual void Collapse(bool animated = true) { Collapsed = true; }
    public virtual void Expand(bool animated = true)   { Collapsed = false; }

    protected virtual void CleanUp() { }

    protected virtual void RearrangeControls() { }

    // ── Layout no-ops ─────────────────────────────────────────────────────

    public void SuspendLayout()             { }
    public void ResumeLayout(bool b = true) { }
    public void RepaintAll()                { NCRefresh(); }

    internal void SetNoCleanUpIntern(bool val)  { }
    internal void SetForceUseAsTarget(bool val) { }

    // ── Events ────────────────────────────────────────────────────────────

    public event EventHandler PanelCollectionChanged;

    // ── Rendering ─────────────────────────────────────────────────────────

    public override void Render(Avalonia.Media.DrawingContext context)
    {
        // DockContainer grip/caption rendering will be implemented here
        // using Avalonia DrawingContext in a future pass.
        base.Render(context);
    }
}
