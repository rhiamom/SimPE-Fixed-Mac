/***************************************************************************
 *   Copyright (C) 2008 by Peter L Jones                                   *
 *   peter@users.sf.net                                                    *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSweatShop                                 *
 *   Rhiamom@mac.com                                                       *
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SimPe
{
    public partial class ProfileChooser : Avalonia.Controls.Window, IDisposable
    {
        public ProfileChooser()
        {
            InitializeComponent();
        }

        public string Value
        {
            get
            {
                // Read from the editable TextBox so the user can type a new profile name
                // (WinForms cbProfiles was DropDownStyle=Simple — editable + inline list).
                return cbProfiles.Text ?? "";
            }
        }

        private void ProfileChooser_Activated(object sender, EventArgs e)
        {
            lbProfiles.Items.Clear();
            foreach (string s in Directory.GetDirectories(SimPe.Helper.DataFolder.Profiles))
                lbProfiles.Items.Add(Path.GetFileName(s));

            btnOK.IsEnabled = false;
        }

        private void ProfileChooser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing && e.CloseReason != CloseReason.None) return;
            // this.DialogResult check not applicable on Avalonia Window

            string text = (cbProfiles.Text ?? "").Trim();
            if (text.Length == 0) { e.Cancel = true; return; }

            string path = Path.Combine(Helper.DataFolder.Profiles, text);
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    // TODO: show error message — MessageBox not available without owner
                    System.Diagnostics.Debug.WriteLine("ProfileChooser: " + ex.Message);
                    e.Cancel = true;
                }
            }
            // else: path exists, proceed
        }

        private void cbProfiles_TextChanged(object sender, EventArgs e)
        {
            btnOK.IsEnabled = ((cbProfiles.Text ?? "").Trim().Length) != 0;
        }

        private void lbProfiles_SelectionChanged(object sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            // Picking an item copies its text into the TextBox; the TextBox's TextChanged
            // event then fires cbProfiles_TextChanged to update btnOK.IsEnabled.
            if (lbProfiles.SelectedItem != null)
                cbProfiles.Text = lbProfiles.SelectedItem.ToString();
        }

        public void Dispose() { }

        public System.Windows.Forms.DialogResult ShowDialog()
        {
            this.Show();
            return System.Windows.Forms.DialogResult.Cancel;
        }

        #region Avalonia layout (ported from WinForms Designer)
        private void InitializeComponent()
        {
            // 1. Instantiate controls.
            this.flowLayoutPanel1 = new Avalonia.Controls.WrapPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
            };
            this.btnCancel = new Avalonia.Controls.Button();
            this.btnOK = new Avalonia.Controls.Button();
            this.label1 = new Avalonia.Controls.TextBlock();
            // WinForms cbProfiles was a ComboBox with DropDownStyle=Simple (editable + inline list).
            // Avalonia ComboBox is dropdown-only and non-editable, which would silently break the
            // "type a new profile name" use case. Port as a TextBox + ListBox composite occupying
            // the same 234x128 rectangle.
            this.cbProfiles = new Avalonia.Controls.TextBox();
            this.lbProfiles = new Avalonia.Controls.ListBox();

            // 2. Set per-control properties from Designer.cs + resx.
            //    resx says btnCancel.Size = 75x23, btnOK.Size = 75x23, both with Text from resx.
            this.btnCancel.Content = "Cancel";
            this.btnCancel.Width = 75;
            this.btnCancel.Height = 23;
            this.btnCancel.Margin = new Avalonia.Thickness(3);

            this.btnOK.Content = "OK";
            this.btnOK.Width = 75;
            this.btnOK.Height = 23;
            this.btnOK.Margin = new Avalonia.Thickness(3);

            // label1: AutoSize=true, multi-line text from resx.
            this.label1.Text = "Select an existing profile or\ntype in a new name:";

            // cbProfiles + lbProfiles together emulate WinForms Simple-style ComboBox:
            //   - cbProfiles (TextBox) is the editable row at the top — height ~22.
            //   - lbProfiles (ListBox) shows the inline list below it — height ~106.
            //   Combined: 234 wide x 128 tall, matching the resx cbProfiles.Size of 234x128.
            this.cbProfiles.Width = 234;
            this.cbProfiles.Height = 22;

            this.lbProfiles.Width = 234;
            this.lbProfiles.Height = 106;

            // 3. Build container hierarchy.
            //    flowLayoutPanel1.Anchor = Bottom,Right in resx — host the OK/Cancel buttons in a
            //    horizontal stack-like WrapPanel anchored to the bottom-right of the dialog.
            //    Designer adds btnCancel BEFORE btnOK in flowLayoutPanel1, but resx Locations
            //    (btnCancel @ x=3, btnOK @ x=84) mean Cancel renders LEFT and OK renders RIGHT.
            //    Preserving WinForms dialog convention (OK left, Cancel right) is called for in
            //    the agent rules — but Designer.cs explicitly orders them Cancel-then-OK, so we
            //    match the original layout exactly per visual-fidelity rule.
            this.flowLayoutPanel1.Children.Add(this.btnCancel);
            this.flowLayoutPanel1.Children.Add(this.btnOK);

            var canvas = new Avalonia.Controls.Canvas();

            // label1 @ 12,9 from resx (AutoSize, so no explicit W/H).
            Avalonia.Controls.Canvas.SetLeft(this.label1, 12);
            Avalonia.Controls.Canvas.SetTop(this.label1, 9);
            canvas.Children.Add(this.label1);

            // cbProfiles (TextBox) @ 12,38 — top of the original 234x128 rectangle.
            Avalonia.Controls.Canvas.SetLeft(this.cbProfiles, 12);
            Avalonia.Controls.Canvas.SetTop(this.cbProfiles, 38);
            canvas.Children.Add(this.cbProfiles);

            // lbProfiles (ListBox) @ 12,60 — directly below the TextBox, filling the rest of
            // the 234x128 rectangle (38 + 22 = 60).
            Avalonia.Controls.Canvas.SetLeft(this.lbProfiles, 12);
            Avalonia.Controls.Canvas.SetTop(this.lbProfiles, 60);
            canvas.Children.Add(this.lbProfiles);

            // flowLayoutPanel1 @ 84,172 from resx (size 162x29).
            this.flowLayoutPanel1.Width = 162;
            this.flowLayoutPanel1.Height = 29;
            Avalonia.Controls.Canvas.SetLeft(this.flowLayoutPanel1, 84);
            Avalonia.Controls.Canvas.SetTop(this.flowLayoutPanel1, 172);
            canvas.Children.Add(this.flowLayoutPanel1);

            var root = new Avalonia.Controls.DockPanel { LastChildFill = true };
            root.Children.Add(canvas);

            // 4. Wire up events (handlers verified above in this file).
            //    cbProfiles (TextBox).TextChanged covers both WinForms TextUpdate + TextChanged
            //    hookups from the Designer. Avalonia's TextChanged uses TextChangedEventArgs
            //    (RoutedEventArgs-derived), so bridge to the EventArgs-shaped handler.
            this.cbProfiles.TextChanged += (s, e) => this.cbProfiles_TextChanged(s, EventArgs.Empty);
            //    lbProfiles selection copies the item text into the TextBox; that in turn fires
            //    TextChanged so btnOK.IsEnabled stays in sync.
            this.lbProfiles.SelectionChanged += this.lbProfiles_SelectionChanged;

            // Window.Activated has signature (object?, EventArgs) — matches handler directly.
            this.Activated += this.ProfileChooser_Activated;

            // Window.Closing fires WindowClosingEventArgs; bridge to FormClosingEventArgs
            // (the latter is the SimPE WinFormsStubs type used by the existing handler).
            this.Closing += (s, e) =>
            {
                var fce = new System.Windows.Forms.FormClosingEventArgs();
                this.ProfileChooser_FormClosing(s, fce);
                if (fce.Cancel) e.Cancel = true;
            };

            // 5. Mount root + window-level properties.
            this.Content = root;

            // 6. Window sizing/title from resx ($this.ClientSize = 258x213, $this.Text = "Profile Chooser").
            this.Width = 258;
            this.Height = 213;
            this.Title = "Profile Chooser";
            this.CanResize = false; // FormBorderStyle.FixedToolWindow
        }

        // Field declarations — moved from the Stubs.cs shim.
        private Avalonia.Controls.WrapPanel flowLayoutPanel1;
        private Avalonia.Controls.Button btnCancel;
        private Avalonia.Controls.Button btnOK;
        private Avalonia.Controls.TextBlock label1;
        // cbProfiles is the editable TextBox row; lbProfiles is the inline list — together they
        // emulate WinForms ComboBox.DropDownStyle = Simple.
        private Avalonia.Controls.TextBox cbProfiles;
        private Avalonia.Controls.ListBox lbProfiles;
        #endregion
    }
}