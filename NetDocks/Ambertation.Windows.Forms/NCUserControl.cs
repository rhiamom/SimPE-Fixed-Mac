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

// Ported from WinForms NCUserControl.
// Original used Windows non-client area APIs (WM_NCCALCSIZE, WM_NCPAINT, WM_NCHITTEST,
// WM_NCMOUSEMOVE, P/Invoke) for custom caption/border rendering.
// On Avalonia: WndProc and NC messages removed; rendering is via Render(DrawingContext).
// The dock caption/tab rendering will be ported to Avalonia DrawingContext in a future pass.

using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Ambertation.Windows.Forms;

/// <summary>
/// Base class for dock panels and containers.
/// Ported from WinForms NCUserControl (non-client area custom rendering).
/// On Avalonia, rendering uses Render(DrawingContext) instead of GDI Graphics.
/// </summary>
public abstract class NCUserControl : Avalonia.Controls.Control
{
    // Non-client margin (caption height + side borders).
    // On Avalonia these inform layout; actual drawing is in Render().
    private int _ncTop = 20, _ncLeft = 6, _ncRight = 6, _ncBottom = 6;

    protected bool DoubleBuffer  { get; set; } = true;
    protected bool NeedRepaint   { get; set; } = true;
    protected bool NCNeedRepaint { get; set; } = true;

    // DragBorder: whether the user can drag-resize this control.
    // Full drag-resize via Avalonia pointer events is handled in a future pass.
    public bool DragBorder { get; set; } = true;

    // NCResizeBorders: exposed for API compatibility; resize behavior not yet wired.
    public NCResizeBorders ResizeBorder { get; } = new NCResizeBorders();

    // ── WinForms-compatible Font and Size ─────────────────────────────────
    // Avalonia uses FontFamily/FontSize; WinForms code assigns System.Drawing.Font/Size.
    public System.Drawing.Font Font { get; set; }
    public System.Drawing.Size Size
    {
        get => new System.Drawing.Size(Width, Height);
        set { Width = value.Width; Height = value.Height; }
    }

    // ── WinForms-compatible int dimensions ────────────────────────────────
    // Avalonia exposes Width/Height as double; the rendering pipeline expects int.
    public new int Width  { get => (int)base.Width;  set => base.Width  = value; }
    public new int Height { get => (int)base.Height; set => base.Height = value; }

    // ── WinForms-compatible position (no-op layout on Mac) ────────────────
    public int Left  { get; set; }
    public int Right => Left + Width;
    public new NCUserControl? Parent => base.Parent as NCUserControl;

    // ── WinForms-compatible Bounds (System.Drawing.Rectangle) ─────────────
    // Avalonia returns Avalonia.Rect; rendering code (BaseDockPanelRenderer etc.)
    // expects System.Drawing.Rectangle. Avalonia reads layout bounds via
    // LayoutInformation/Visual, never via this property, so shadowing is safe.
    public new System.Drawing.Rectangle Bounds
        => new System.Drawing.Rectangle(Left, 0, Width, Height);

    // ── Thread marshalling stubs ───────────────────────────────────────────
    // On Mac we run single-threaded; animation/invoke are no-ops for now.
    public bool InvokeRequired => false;
    public void Invoke(System.Delegate method, params object[] args) => method.DynamicInvoke(args);
    public void BeginInvoke(System.Delegate method, params object[] args)
        => Avalonia.Threading.Dispatcher.UIThread.Post(() => method.DynamicInvoke(args));

    // ── IDisposable compat (WinForms Control pattern) ─────────────────────
    // Subclasses generated by WinForms designer override Dispose(bool).
    protected virtual void Dispose(bool disposing) { }

    protected NCUserControl()
    {
    }

    // ── Invalidation helpers ───────────────────────────────────────────────

    protected void DoInvalidateWindow()
    {
        NCNeedRepaint = true;
        NeedRepaint   = true;
        InvalidateVisual();
    }

    /// <summary>Mark the NC area as needing repaint and invalidate.</summary>
    public void NCRefresh() => NCRefresh(false);

    public void NCRefresh(bool force)
    {
        NCNeedRepaint = true;
        if (IsVisible || force)
            InvalidateVisual();
    }

    // ── Rendering ─────────────────────────────────────────────────────────

    /// <summary>
    /// Override in subclasses to draw the dock panel's caption and content area.
    /// Replaces WinForms OnNCPaint(NCPaintEventArgs) + OnPaint(PaintEventArgs).
    /// </summary>
    public override void Render(DrawingContext context)
    {
        base.Render(context);
    }
}
