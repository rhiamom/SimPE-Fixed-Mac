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
    public class JobDescPanel : Avalonia.Controls.UserControl
    {
        // Fields
        private TextBox   tbDesc  = new TextBox { AcceptsReturn = true };
        private TextBlock lbDesc  = new TextBlock { Text = "Description" };
        private TextBlock lbTitle = new TextBlock { Text = "Title" };
        private TextBox   tbTitle = new TextBox();

        public JobDescPanel()
        {
            var titleRow = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
            titleRow.Children.Add(lbTitle);
            titleRow.Children.Add(tbTitle);

            var descRow = new StackPanel { Orientation = Avalonia.Layout.Orientation.Vertical };
            descRow.Children.Add(lbDesc);
            descRow.Children.Add(tbDesc);

            var stack = new StackPanel { Orientation = Avalonia.Layout.Orientation.Vertical };
            stack.Children.Add(titleRow);
            stack.Children.Add(descRow);
            Content = stack;

            tbTitle.TextChanged += tbTitle_TextChanged;
            tbDesc.TextChanged  += tbDesc_TextChanged;
        }

        public string TitleLabel { get { return lbTitle.Text; } set { lbTitle.Text = value; } }
        public string TitleValue { get { return tbTitle.Text; } set { tbTitle.Text = value; } }

        public string DescLabel { get { return lbDesc.Text; } set { lbDesc.Text = value; } }
        public string DescValue { get { return tbDesc.Text; } set { tbDesc.Text = value; } }

        public event EventHandler TitleValueChanged;
        public virtual void OnTitleValueChanged(object sender, EventArgs e)
        {
            TitleValueChanged?.Invoke(sender, e);
        }
        private void tbTitle_TextChanged(object sender, EventArgs e)
        {
            OnTitleValueChanged(sender, e);
        }

        public event EventHandler DescValueChanged;
        public virtual void OnDescValueChanged(object sender, EventArgs e)
        {
            DescValueChanged?.Invoke(sender, e);
        }
        private void tbDesc_TextChanged(object sender, EventArgs e)
        {
            OnDescValueChanged(sender, e);
        }
    }
}
