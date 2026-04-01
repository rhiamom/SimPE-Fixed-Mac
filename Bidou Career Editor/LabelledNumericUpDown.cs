/***************************************************************************
 *   Copyright (C) 2007 Peter L Jones                                      *
 *   pljones@users.sf.net                                                  *
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
using Avalonia.Controls;
using Avalonia.Layout;

namespace SimPe.Plugin
{
    public class LabelledNumericUpDown : Avalonia.Controls.UserControl
    {
        // Fields
        private StackPanel flpMain = new StackPanel { Orientation = Orientation.Horizontal };
        private TextBlock lbLabel = new TextBlock { Text = "label", VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center };
        private NumericUpDown nudValue = new NumericUpDown { Minimum = 0, Maximum = 100 };

        public LabelledNumericUpDown()
        {
            flpMain.Children.Add(lbLabel);
            flpMain.Children.Add(nudValue);
            nudValue.ValueChanged += nudValue_ValueChanged;
            Content = flpMain;
            lbLabel.IsVisible = !noLabel;
        }

        private bool noLabel = false;
        public bool NoLabel
        {
            get { return noLabel; }
            set
            {
                if (noLabel != value)
                {
                    noLabel = value;
                    if (noLabel)
                        flpMain.Children.Remove(lbLabel);
                    else
                    {
                        if (!flpMain.Children.Contains(lbLabel))
                        {
                            flpMain.Children.Insert(0, lbLabel);
                        }
                    }
                }
            }
        }

        public string Label
        {
            get { return lbLabel.Text; }
            set { lbLabel.Text = value; }
        }

        // No-op: Avalonia doesn't have AnchorStyles on child elements
        public System.Windows.Forms.AnchorStyles LabelAnchor
        {
            get { return System.Windows.Forms.AnchorStyles.None; }
            set { }
        }

        public decimal Value
        {
            get { return nudValue.Value ?? 0m; }
            set { nudValue.Value = value; }
        }

        // No-op: size is handled by layout
        public System.Drawing.Size ValueSize
        {
            get { return System.Drawing.Size.Empty; }
            set { }
        }

        public bool ReadOnly
        {
            get { return nudValue.IsReadOnly; }
            set { nudValue.IsReadOnly = value; }
        }

        public decimal Maximum
        {
            get { return nudValue.Maximum; }
            set { nudValue.Maximum = value; }
        }

        public decimal Minimum
        {
            get { return nudValue.Minimum; }
            set { nudValue.Minimum = value; }
        }

        public event EventHandler ValueChanged;
        public virtual void OnValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(sender, e);
        }
        private void nudValue_ValueChanged(object sender, NumericUpDownValueChangedEventArgs e)
        {
            OnValueChanged(this, EventArgs.Empty);
        }
    }
}
