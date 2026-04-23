using Avalonia.Controls;
using Avalonia.Layout;
using SimPe.Scenegraph.Compat;

namespace pjse
{
    partial class LabelledDataOwner
    {
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary>
        /// Builds the Avalonia visual tree for LabelledDataOwner.
        ///
        /// Original WinForms used a 3×3 TableLayoutPanel:
        ///   (0,0)=lbLabel   (1,0)=cbDataOwner   (2,0)=flpValue(cbPicker|tbVal)
        ///                   (1,1)=lbInstance
        ///                   (1,2..2,2)=flpCheckBoxes(ckbDecimal, ckbUseInstancePicker)
        /// </summary>
        private void InitializeComponent()
        {
            this.lbLabel              = new LabelCompat     { Name = "lbLabel",              Content = "label1",            VerticalAlignment = VerticalAlignment.Center, Margin = new Avalonia.Thickness(0, 0, 6, 0) };
            this.cbDataOwner          = new ComboBoxCompat  { Name = "cbDataOwner",          MinWidth = 150, MinHeight = 22 };
            this.cbPicker             = new ComboBoxCompat  { Name = "cbPicker",             MinWidth = 150, MinHeight = 22, IsVisible = false };
            this.tbVal                = new TextBoxCompat   { Name = "tbVal",                MinWidth = 90,  MinHeight = 22, Text = "0x0000" };
            this.lbInstance           = new LabelCompat     { Name = "lbInstance",           Content = "Const value",       VerticalAlignment = VerticalAlignment.Center };
            this.ckbDecimal           = new CheckBoxCompat2 { Name = "ckbDecimal",           Content = "Decimal (except Consts)" };
            this.ckbUseInstancePicker = new CheckBoxCompat2 { Name = "ckbUseInstancePicker", Content = "use Instance Picker",  Margin = new Avalonia.Thickness(12, 0, 0, 0) };

            // Row 0 col 2: cbPicker and tbVal share the same slot (one visible at a time).
            // Use a Grid rather than bare Panel — Avalonia's base Panel is abstract.
            var valueCell = new Grid();
            valueCell.Children.Add(this.cbPicker);
            valueCell.Children.Add(this.tbVal);

            // Row 2 cols 1-2: horizontal checkbox row.
            var checkBoxes = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Avalonia.Thickness(0, 2, 0, 0),
            };
            checkBoxes.Children.Add(this.ckbDecimal);
            checkBoxes.Children.Add(this.ckbUseInstancePicker);

            this.tableLayoutPanel1 = new Grid
            {
                ColumnDefinitions = new ColumnDefinitions("Auto,Auto,Auto"),
                RowDefinitions    = new RowDefinitions("Auto,Auto,Auto"),
                Margin            = new Avalonia.Thickness(4),
            };

            Grid.SetColumn(this.lbLabel,     0); Grid.SetRow(this.lbLabel,     0);
            Grid.SetColumn(this.cbDataOwner, 1); Grid.SetRow(this.cbDataOwner, 0);
            Grid.SetColumn(valueCell,        2); Grid.SetRow(valueCell,        0);
            this.cbDataOwner.Margin = new Avalonia.Thickness(0, 0, 6, 0);

            Grid.SetColumn(this.lbInstance,  1); Grid.SetRow(this.lbInstance,  1);
            Grid.SetColumnSpan(this.lbInstance, 2);

            Grid.SetColumn(checkBoxes,       1); Grid.SetRow(checkBoxes,       2);
            Grid.SetColumnSpan(checkBoxes,   2);

            this.tableLayoutPanel1.Children.Add(this.lbLabel);
            this.tableLayoutPanel1.Children.Add(this.cbDataOwner);
            this.tableLayoutPanel1.Children.Add(valueCell);
            this.tableLayoutPanel1.Children.Add(this.lbInstance);
            this.tableLayoutPanel1.Children.Add(checkBoxes);

            // NOTE: this file is excluded from compilation by Directory.Build.targets
            // (<Compile Remove="**\*.Designer.cs" />). The real InitializeComponent that
            // actually runs lives in SimPe.pjsecoder.Stubs.cs. This file is kept for
            // reference only.
            this.MinWidth  = 260;
            this.MinHeight = 72;
            this.Name = "LabelledDataOwner";
            this.Child = this.tableLayoutPanel1;
        }

        #endregion

        // NOTE: kept the original field name `tableLayoutPanel1` so nothing else needs to change,
        // but it's really a Grid now.
        private Grid              tableLayoutPanel1;
        private LabelCompat       lbLabel;
        private LabelCompat       lbInstance;
        private ComboBoxCompat    cbPicker;
        private TextBoxCompat     tbVal;
        private ComboBoxCompat    cbDataOwner;
        private CheckBoxCompat2   ckbDecimal;
        private CheckBoxCompat2   ckbUseInstancePicker;
    }
}
