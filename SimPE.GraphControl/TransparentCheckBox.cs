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
 ***************************************************************************/

// Ported from WinForms.  The original class used a custom GDI+ rendering
// pipeline to simulate a transparent background for CheckBox on themed
// Windows controls.  Avalonia handles transparency natively, so the
// standard CheckBox is sufficient.

using System;

namespace Ambertation.Windows.Forms
{
    // ── Local enum stubs for WinForms compat (SimPE.GraphControl has no SWF ref) ──
    // These mirror the enums in SimPE.GMDCExporterbase/ListViewEx.cs but live here
    // to avoid a circular or missing project reference.
    public enum TransparentCheckBoxCheckState { Unchecked = 0, Checked = 1, Indeterminate = 2 }
    public enum TransparentCheckBoxDockStyle  { None, Top, Bottom, Left, Right, Fill }

    /// <summary>
    /// CheckBox with transparent background support.
    /// In Avalonia, transparency is handled natively.
    /// This class wraps WinForms-compat properties so InitializeComponent-style
    /// code in designer files compiles without modification.
    /// The callers (OWDockForm.cs etc.) use System.Windows.Forms.CheckState and
    /// System.Windows.Forms.DockStyle; those types are in a different assembly, so
    /// this stub uses object for those properties to stay assembly-neutral.
    /// </summary>
    public class TransparentCheckBox
    {
        // Avalonia CheckBox has Checked/Unchecked as events (via ToggleButton).
        // WinForms code sets .Checked = true/false as a boolean property.
        public bool Checked
        {
            get => _checked;
            set { _checked = value; }
        }
        private bool _checked;

        // Avalonia parity: Avalonia.Controls.CheckBox exposes IsChecked as bool? (tri-state).
        // Ported form code (e.g. SimPE.Sims/ExtSDescUI.cs) reads/writes .IsChecked on these
        // compat checkboxes — treat null as false on get and on set.
        public bool? IsChecked
        {
            get => _checked;
            set { _checked = (value == true); }
        }

        // CheckState stored as object so callers from assemblies with System.Windows.Forms
        // can assign System.Windows.Forms.CheckState values without type conflict.
        public object CheckState
        {
            get => _checked ? (object)1 : (object)0;
            set { if (value is int i) _checked = (i != 0); }
        }

        public bool   Enabled  { get; set; } = true;
        public bool   Visible  { get; set; } = true;
        public bool   TabStop  { get; set; } = true;
        public int    TabIndex { get; set; }
        public string Name     { get; set; } = "";
        public string Text     { get; set; } = "";
        public System.Drawing.Font  Font     { get; set; }
        public System.Drawing.Point Location { get; set; }
        public System.Drawing.Size  Size     { get; set; }
        // Dock stored as object so System.Windows.Forms.DockStyle assignment works
        public object Dock { get; set; }

        public void Refresh() { }

        public event EventHandler CheckedChanged;
        protected virtual void OnCheckedChanged(EventArgs e) => CheckedChanged?.Invoke(this, e);
    }
}
