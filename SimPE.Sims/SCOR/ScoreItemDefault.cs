/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSweatShop                                 *
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


using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using Avalonia.Media;

namespace SimPe.PackedFiles.Wrapper.SCOR
{
    public partial class ScoreItemDefault : AScorItem
    {

        public ScoreItemDefault(ScorItem si)
            : base(si)
        {
            InitializeComponent();
            data = new byte[0];
        }

        protected override void DoSetData(string name, System.IO.BinaryReader reader)
        {
            textBox1.Text = name;
            data = reader.ReadBytes((int)reader.BaseStream.Length);

            tb.Text = Helper.BytesToHexList(data, 4);
        }

        byte[] data;
        internal override void Serialize(System.IO.BinaryWriter writer, bool last)
        {
            base.Serialize(writer, last);
            writer.Write(data);
        }

        #region Avalonia layout (ported from WinForms Designer)
        private void InitializeComponent()
        {
            // 1. Instantiate controls. Designer.cs has a header TextBox (Dock.Top, ReadOnly,
            //    height 20) showing the token name, and a multi-line content TextBox (Dock.Fill,
            //    ReadOnly, WordWrap defaults to true on a Multiline TextBox) showing a hex dump.
            this.textBox1 = new TextBox();
            this.tb = new TextBox();

            // 2. Per-control properties.
            this.textBox1.IsReadOnly = true;
            this.textBox1.Height = 20;

            this.tb.IsReadOnly = true;
            this.tb.AcceptsReturn = true;
            this.tb.TextWrapping = TextWrapping.Wrap;

            // 3. Build container hierarchy. WinForms docks the LAST-added child first, so the
            //    visual order is textBox1 (Top, h=20) then tb (Fill, fills remainder). In
            //    Avalonia DockPanel with LastChildFill=true, add the docked child first and the
            //    fill child last.
            var root = new Avalonia.Controls.DockPanel { LastChildFill = true };
            Avalonia.Controls.DockPanel.SetDock(this.textBox1, Avalonia.Controls.Dock.Top);
            root.Children.Add(this.textBox1);
            root.Children.Add(this.tb);

            // 4. No event hookups in Designer.cs — both TextBoxes are passive output surfaces.

            // 5. Mount root on the UserControl.
            this.Content = root;

            // 6. Form's own Size from Designer (this.Size = 261 x 150). Use MinWidth/MinHeight
            //    so the control can grow when its host gives it more room.
            this.MinWidth = 261;
            this.MinHeight = 150;
        }

        // Field declarations — moved from the Stubs.cs shim.
        private TextBox textBox1;
        private TextBox tb;
        #endregion
    }
}
