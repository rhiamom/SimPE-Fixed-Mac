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

namespace SimPe.Plugin
{
    public class ChoicePanel : Avalonia.Controls.UserControl
    {
        // Fields
        private TextBlock lbChoice = new TextBlock();
        private LabelledNumericUpDown lnudCooking = new LabelledNumericUpDown();
        private LabelledNumericUpDown lnudMechanical = new LabelledNumericUpDown();
        private LabelledNumericUpDown lnudCharisma = new LabelledNumericUpDown();
        private LabelledNumericUpDown lnudBody = new LabelledNumericUpDown();
        private LabelledNumericUpDown lnudCreativity = new LabelledNumericUpDown();
        private LabelledNumericUpDown lnudLogic = new LabelledNumericUpDown();
        private LabelledNumericUpDown lnudCleaning = new LabelledNumericUpDown();
        private TextBox tbChoice = new TextBox();

        public ChoicePanel()
        {
            var stack = new StackPanel { Orientation = Avalonia.Layout.Orientation.Vertical };
            var row1 = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
            row1.Children.Add(lbChoice);
            row1.Children.Add(tbChoice);
            var row2 = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
            row2.Children.Add(lnudCooking);
            row2.Children.Add(lnudMechanical);
            row2.Children.Add(lnudBody);
            row2.Children.Add(lnudCharisma);
            row2.Children.Add(lnudCreativity);
            row2.Children.Add(lnudLogic);
            row2.Children.Add(lnudCleaning);
            stack.Children.Add(row1);
            stack.Children.Add(row2);
            Content = stack;
        }

        private const int mm = 100;

        public void setValues(bool labels, string label, string value, SimPe.PackedFiles.Wrapper.Bcon[] bcon, ushort level)
        {
            Labels = labels;
            lbChoice.Text = label;
            tbChoice.Text = value;
            if (bcon != null)
            {
                lnudCooking.Value = bcon[0][level] / mm;
                lnudMechanical.Value = bcon[1][level] / mm;
                lnudBody.Value = bcon[2][level] / mm;
                lnudCharisma.Value = bcon[3][level] / mm;
                lnudCreativity.Value = bcon[4][level] / mm;
                lnudLogic.Value = bcon[5][level] / mm;
                lnudCleaning.Value = bcon[6][level] / mm;
            }
            else
            {
                lnudCooking.IsEnabled = lnudMechanical.IsEnabled = lnudBody.IsEnabled = lnudCharisma.IsEnabled =
                    lnudCreativity.IsEnabled = lnudLogic.IsEnabled = lnudCleaning.IsEnabled = false;
            }
        }

        public void getValues(SimPe.PackedFiles.Wrapper.Bcon[] bcon, ushort level)
        {
            bcon[0][level] = (short)(lnudCooking.Value * mm);
            bcon[1][level] = (short)(lnudMechanical.Value * mm);
            bcon[2][level] = (short)(lnudBody.Value * mm);
            bcon[3][level] = (short)(lnudCharisma.Value * mm);
            bcon[4][level] = (short)(lnudCreativity.Value * mm);
            bcon[5][level] = (short)(lnudLogic.Value * mm);
            bcon[6][level] = (short)(lnudCleaning.Value * mm);
        }

        private bool labels = true;
        public bool Labels
        {
            get { return labels; }
            set
            {
                labels = value;
                lnudCooking.NoLabel = lnudMechanical.NoLabel = lnudBody.NoLabel = lnudCharisma.NoLabel =
                    lnudCreativity.NoLabel = lnudLogic.NoLabel = lnudCleaning.NoLabel = !labels;
            }
        }

        private bool hsk = true;
        public bool HaveSkills
        {
            get { return hsk; }
            set
            {
                if (hsk != value)
                {
                    hsk = value;
                    lnudCooking.IsVisible = lnudMechanical.IsVisible = lnudBody.IsVisible = lnudCharisma.IsVisible =
                        lnudCreativity.IsVisible = lnudLogic.IsVisible = lnudCleaning.IsVisible = hsk;
                }
            }
        }

        public string Label { get { return lbChoice.Text; } set { lbChoice.Text = value; } }
        public string Value { get { return tbChoice.Text; } set { tbChoice.Text = value; } }

        public decimal Cooking { get { return lnudCooking.Value; } set { lnudCooking.Value = value; } }
        public decimal Mechanical { get { return lnudMechanical.Value; } set { lnudMechanical.Value = value; } }
        public decimal Charisma { get { return lnudCharisma.Value; } set { lnudCharisma.Value = value; } }
        public decimal Body { get { return lnudBody.Value; } set { lnudBody.Value = value; } }
        public decimal Creativity { get { return lnudCreativity.Value; } set { lnudCreativity.Value = value; } }
        public decimal Logic { get { return lnudLogic.Value; } set { lnudLogic.Value = value; } }
        public decimal Cleaning { get { return lnudCleaning.Value; } set { lnudCleaning.Value = value; } }
    }
}
