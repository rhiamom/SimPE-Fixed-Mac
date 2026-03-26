/***************************************************************************
 *   Copyright (C) 2025 by GramzeSweatshop                                 *
 *   rhiamom@mac.com                                                       *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 ***************************************************************************/

// Minimal stubs for HexViewControl and HexEditControl, which were removed from
// SimPE.GraphControl when hex functionality moved to the main window via AvaloniaHex.
// ResourceDock.cs still declares fields of these types so it must compile.

namespace Ambertation.Windows.Forms
{
    public class HexViewControl : System.Windows.Forms.Control
    {
        public enum ViewState { Hex, SignedDec, UnsignedDec }

        public System.Drawing.Color BackGroundColour   { get; set; }
        public byte                  Blocks            { get; set; }
        public int                   CharBoxWidth       { get; set; }
        public int                   CurrentRow         { get; set; }
        public byte[]                Data              { get; set; }
        public System.Drawing.Color  FocusedForeColor  { get; set; }
        public System.Drawing.Color  GridColor          { get; set; }
        public System.Drawing.Color  HeadColor          { get; set; }
        public System.Drawing.Color  HeadForeColor      { get; set; }
        public System.Drawing.Color  HighlightColor     { get; set; }
        public System.Drawing.Color  HighlightForeColor { get; set; }
        public bool                  HighlightZeros     { get; set; }
        public int                   Offset             { get; set; }
        public int                   OffsetBoxWidth     { get; set; }
        public byte                  SelectedByte       { get; set; }
        public char                  SelectedChar       { get; set; }
        public double                SelectedDouble     { get; set; }
        public float                 SelectedFloat      { get; set; }
        public int                   SelectedInt        { get; set; }
        public long                  SelectedLong       { get; set; }
        public short                 SelectedShort      { get; set; }
        public uint                  SelectedUInt       { get; set; }
        public ulong                 SelectedULong      { get; set; }
        public ushort                SelectedUShort     { get; set; }
        public byte[]                Selection          { get; set; }
        public System.Drawing.Color  SelectionColor     { get; set; }
        public System.Drawing.Color  SelectionForeColor { get; set; }
        public System.Drawing.Color  ZeroCellColor      { get; set; }
        public bool                  ShowGrid           { get; set; }
        public ViewState             View               { get; set; }
        public int                   SelectionLength    => 0;

        public event System.EventHandler DataChanged;
        public event System.EventHandler SelectionChanged;

        public void Highlight(byte[] data) { }
        public new void Refresh(bool force = false) { }

        public static string SetLength(string s, int len, char pad)
        {
            while (s.Length < len) s = pad + s;
            return s;
        }
    }

    public class HexEditControl : System.Windows.Forms.Control
    {
        public System.Drawing.Font        LabelFont  { get; set; }
        public System.Drawing.Font        TextBoxFont { get; set; }
        public bool                       Vertical    { get; set; }
        public HexViewControl.ViewState   View        { get; set; }
        public HexViewControl             Viewer      { get; set; }
    }
}
