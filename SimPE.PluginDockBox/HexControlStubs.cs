/***************************************************************************
 *   Copyright (C) 2025 by GramzeSweatshop                                 *
 *   rhiamom@mac.com                                                       *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 ***************************************************************************/

namespace Ambertation.Windows.Forms
{
    // HexViewControl wraps AvaloniaHex.HexEditor — a full-featured, properly
    // virtualized hex editor control for Avalonia, already included as a package
    // dependency. Its styles are loaded in App.axaml:
    //   <StyleInclude Source="avares://AvaloniaHex/Themes/Simple/AvaloniaHex.axaml"/>
    //
    // Three columns are shown: offset address | hex bytes (16-wide) | ASCII.
    public class HexViewControl : Avalonia.Controls.Border
    {
        public enum ViewState { Hex, SignedDec, UnsignedDec }

        private readonly AvaloniaHex.HexEditor _editor;
        private byte[] _data;

        public HexViewControl()
        {
            _editor = new AvaloniaHex.HexEditor
            {
                FontFamily = new Avalonia.Media.FontFamily("Courier New, Courier, monospace"),
                FontSize   = 11,
            };

            // Three standard columns.
            // Offset column gets an orange background to match original SimPE styling.
            var offsetCol = new AvaloniaHex.Rendering.OffsetColumn
            {
                Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromRgb(230, 120, 0)),
                Foreground = Avalonia.Media.Brushes.White,
            };
            _editor.Columns.Add(offsetCol);
            _editor.Columns.Add(new AvaloniaHex.Rendering.HexColumn());
            _editor.Columns.Add(new AvaloniaHex.Rendering.AsciiColumn());

            // Set a non-null empty document so the column headers always render
            // even before any resource is selected.
            _editor.Document = new AvaloniaHex.Document.ByteArrayBinaryDocument(System.Array.Empty<byte>());

            // Fix bytes-per-line at 16 to match the original SimPE layout.
            _editor.HexView.BytesPerLine = 16;

            Child = _editor;
        }

        // ── Data property ─────────────────────────────────────────────────────
        public byte[] Data
        {
            get => _data;
            set
            {
                _data = value;
                _editor.Document = (value != null && value.Length > 0)
                    ? new AvaloniaHex.Document.ByteArrayBinaryDocument(value)
                    : null;
                DataChanged?.Invoke(this, System.EventArgs.Empty);
            }
        }

        // ── Stub properties kept for callers ─────────────────────────────────
        public System.Drawing.Color BackGroundColour    { get; set; }
        public byte                  Blocks             { get; set; }
        public int                   CharBoxWidth        { get; set; }
        public int                   CurrentRow          { get; set; }
        public System.Drawing.Color  FocusedForeColor   { get; set; }
        public System.Drawing.Color  GridColor           { get; set; }
        public System.Drawing.Color  HeadColor           { get; set; }
        public System.Drawing.Color  HeadForeColor       { get; set; }
        public System.Drawing.Color  HighlightColor      { get; set; }
        public System.Drawing.Color  HighlightForeColor  { get; set; }
        public bool                  HighlightZeros      { get; set; }
        public int                   Offset              { get; set; }
        public int                   OffsetBoxWidth      { get; set; }
        public byte                  SelectedByte        { get; set; }
        public char                  SelectedChar        { get; set; }
        public double                SelectedDouble      { get; set; }
        public float                 SelectedFloat       { get; set; }
        public int                   SelectedInt         { get; set; }
        public long                  SelectedLong        { get; set; }
        public short                 SelectedShort       { get; set; }
        public uint                  SelectedUInt        { get; set; }
        public ulong                 SelectedULong       { get; set; }
        public ushort                SelectedUShort      { get; set; }
        public byte[]                Selection           { get; set; }
        public System.Drawing.Color  SelectionColor      { get; set; }
        public System.Drawing.Color  SelectionForeColor  { get; set; }
        public System.Drawing.Color  ZeroCellColor       { get; set; }
        public bool                  ShowGrid            { get; set; }
        public ViewState             View                { get; set; }
        public int                   SelectionLength     => 0;
        public bool                  Visible             { get => IsVisible; set => IsVisible = value; }

        public event System.EventHandler DataChanged;
        public event System.EventHandler SelectionChanged;

        public void Highlight(byte[] data) { }
        public void Refresh(bool force = false) { }

        public static string SetLength(string s, int len, char pad)
        {
            while (s.Length < len) s = pad + s;
            return s;
        }
    }

    public class HexEditControl : Avalonia.Controls.Control
    {
        public System.Drawing.Font        LabelFont   { get; set; }
        public System.Drawing.Font        TextBoxFont { get; set; }
        public bool                       Vertical    { get; set; }
        public HexViewControl.ViewState   View        { get; set; }
        public HexViewControl             Viewer      { get; set; }
    }
}
