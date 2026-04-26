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
using System.ComponentModel;
using System.Text;
using Avalonia.Controls;

namespace SimPe.PackedFiles.Wrapper
{
    public partial class PetTraitSelect : Avalonia.Controls.UserControl
    {
        public enum Levels { High, Normal, Low };
        
        public PetTraitSelect()
        {            
            InitializeComponent();

            Level = Levels.Normal;
        }

        public Levels Level
        {
            get
            {
                if (rb1.IsChecked == true) return Levels.High;
                if (rb3.IsChecked == true) return Levels.Low;
                return Levels.Normal;
            }
            set
            {
                if (value == Levels.High) rb1.IsChecked = true;
                else if (value == Levels.Low) rb3.IsChecked = true;
                else rb2.IsChecked = true;
            }
        }

        public event EventHandler LevelChanged;
        private void CheckedChanged(object sender, EventArgs e)
        {
            if (LevelChanged != null) LevelChanged(this, new EventArgs());
        }

        public void UpdateTraits(int high, int low, PetTraits traits)
        {
            if (traits == null) return;
            Levels lv = Level;
            traits.SetTrait(high, false);
            traits.SetTrait(low, false);
            if (lv == Levels.High) traits.SetTrait(high, true);
            if (lv == Levels.Low) traits.SetTrait(low, true);
        }

        public void SetTraitLevel(int high, int low, PetTraits traits)
        {
            if (traits == null) return;
            if (traits.GetTrait(high)) Level = Levels.High;
            else if (traits.GetTrait(low)) Level = Levels.Low;
            else Level = Levels.Normal;
        }

        #region Avalonia layout (ported from WinForms Designer)
        private void InitializeComponent()
        {
            // 1. Instantiate controls.
            this.rb1 = new RadioButton();
            this.rb2 = new RadioButton();
            this.rb3 = new RadioButton();

            // 2. Build container hierarchy: DockPanel root with a Canvas for absolute positions.
            //    Mutual exclusion of rb1/rb2/rb3 comes for free from sharing the Canvas as
            //    parent — do NOT set GroupName, as it would group ALL PetTraitSelect instances
            //    together (GroupName is window-scoped in Avalonia, not parent-scoped).
            //    Designer.cs uses pure Point-based Location; AutoSize=true on each RB so we omit
            //    Width/Height and let Avalonia size to content.
            var canvas = new Avalonia.Controls.Canvas();
            Avalonia.Controls.Canvas.SetLeft(this.rb1, 3);
            Avalonia.Controls.Canvas.SetTop(this.rb1, 3);
            Avalonia.Controls.Canvas.SetLeft(this.rb2, 39);
            Avalonia.Controls.Canvas.SetTop(this.rb2, 3);
            Avalonia.Controls.Canvas.SetLeft(this.rb3, 75);
            Avalonia.Controls.Canvas.SetTop(this.rb3, 3);
            canvas.Children.Add(this.rb1);
            canvas.Children.Add(this.rb2);
            canvas.Children.Add(this.rb3);

            var root = new Avalonia.Controls.DockPanel { LastChildFill = true };
            root.Children.Add(canvas);

            // 3. Wire up events (handler verified at PetTraitSelect.cs line 60).
            //    Avalonia's IsCheckedChanged passes RoutedEventArgs; CheckedChanged expects
            //    EventArgs, so adapt with lambdas.
            this.rb1.IsCheckedChanged += (s, e) => this.CheckedChanged(s, EventArgs.Empty);
            this.rb2.IsCheckedChanged += (s, e) => this.CheckedChanged(s, EventArgs.Empty);
            this.rb3.IsCheckedChanged += (s, e) => this.CheckedChanged(s, EventArgs.Empty);

            // 4. Mount root on the UserControl.
            this.Content = root;

            // 5. Form's own Size from Designer (this.Size = 93 x 20).
            //    Use MinWidth/MinHeight so the control can grow if its host gives it more room.
            this.MinWidth = 93;
            this.MinHeight = 20;
        }

        // Field declarations — moved from the Stubs.cs shim.
        private RadioButton rb1;
        private RadioButton rb2;
        private RadioButton rb3;
        #endregion
    }
}
