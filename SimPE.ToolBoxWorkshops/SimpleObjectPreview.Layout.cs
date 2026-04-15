// SimPE.ToolBoxWorkshops.Stubs.cs — Designer stubs replacing excluded Designer.cs files.

using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using SimPe.Scenegraph.Compat;

namespace SimPe.Plugin.Tool.Dockable
{
    partial class SimpleObjectPreview
    {
        private System.ComponentModel.IContainer components = null;
        private TextBlock label1 = new TextBlock();
        private TextBlock label2 = new TextBlock();
        private TextBlock label3 = new TextBlock();
        private TextBlock label4 = new TextBlock();
        private TextBlock label7 = new TextBlock();
        protected PictureBox pb = new PictureBox();
        protected TextBlock lbName = new TextBlock();
        protected TextBlock lbAbout = new TextBlock();
        protected TextBlock lbPrice = new TextBlock();
        protected TextBlock lbCat = new TextBlock();
        protected ComboBox cbCat = new ComboBox();
        protected TextBlock lbVert = new TextBlock();
        protected TextBlock lbEPs = new TextBlock();
        protected TextBlock lbEPList = new TextBlock();

        private void InitializeComponent()
        {
            // ── Label text ──
            label1.Text = "Name:";
            label1.FontWeight = FontWeight.Bold;
            label2.Text = "Price:";
            label2.FontWeight = FontWeight.Bold;
            label3.Text = "Category:";
            label3.FontWeight = FontWeight.Bold;
            label4.Text = "Required EPs:";
            label4.FontWeight = FontWeight.Bold;
            label7.Text = "Vertex Count:";
            label7.FontWeight = FontWeight.Bold;

            var lblDesc = new TextBlock { Text = "Description:", FontWeight = FontWeight.Bold, Margin = new Avalonia.Thickness(0, 4, 0, 2) };

            lbAbout.TextWrapping = TextWrapping.Wrap;
            lbAbout.FontSize = 11;

            cbCat.HorizontalAlignment = HorizontalAlignment.Stretch;
            cbCat.MinWidth = 100;

            // ── Info grid: labels + values ──
            var infoGrid = new Grid();
            infoGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
            infoGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
            for (int i = 0; i < 5; i++)
                infoGrid.RowDefinitions.Add(new RowDefinition(GridLength.Auto));

            var margin = new Avalonia.Thickness(0, 1);
            var labelMargin = new Avalonia.Thickness(0, 1, 8, 1);

            void PlaceRow(int row, Control lbl, Control val)
            {
                lbl.SetValue(Avalonia.Controls.Grid.RowProperty, row);
                lbl.SetValue(Avalonia.Controls.Grid.ColumnProperty, 0);
                if (lbl is TextBlock tb) tb.Margin = labelMargin;
                lbl.VerticalAlignment = VerticalAlignment.Center;

                val.SetValue(Avalonia.Controls.Grid.RowProperty, row);
                val.SetValue(Avalonia.Controls.Grid.ColumnProperty, 1);
                if (val is TextBlock vt) vt.Margin = margin;
                val.VerticalAlignment = VerticalAlignment.Center;

                infoGrid.Children.Add(lbl);
                infoGrid.Children.Add(val);
            }

            PlaceRow(0, label1, lbName);
            PlaceRow(1, label2, lbPrice);
            PlaceRow(2, label3, cbCat);
            PlaceRow(3, label4, lbEPs);
            PlaceRow(4, label7, lbVert);

            // ── Thumbnail + info side by side ──
            var thumbImage = new Avalonia.Controls.Image
            {
                Width = 80, Height = 80,
                Stretch = Stretch.Uniform,
            };
            var thumbBorder = new Border
            {
                Width = 84, Height = 84,
                Background = Brushes.White,
                BorderBrush = new SolidColorBrush(Color.Parse("#AAAAAA")),
                BorderThickness = new Avalonia.Thickness(1),
                Margin = new Avalonia.Thickness(0, 0, 8, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Child = thumbImage,
            };

            // Update the Avalonia Image when pb.Image changes
            pb.ImageChanged += (s, e) =>
            {
                var avBmp = SimPe.Helper.ToAvaloniaBitmap(pb.Image as SkiaSharp.SKBitmap);
                Avalonia.Threading.Dispatcher.UIThread.Post(() => thumbImage.Source = avBmp);
            };

            var topRow = new Grid();
            topRow.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
            topRow.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
            Grid.SetColumn(thumbBorder, 0);
            Grid.SetColumn(infoGrid, 1);
            topRow.Children.Add(thumbBorder);
            topRow.Children.Add(infoGrid);

            // ── Assemble ──
            var outer = new StackPanel { Margin = new Avalonia.Thickness(6, 4) };
            outer.Children.Add(topRow);
            outer.Children.Add(lblDesc);
            outer.Children.Add(lbAbout);

            this.Content = outer;
        }
    }
}
