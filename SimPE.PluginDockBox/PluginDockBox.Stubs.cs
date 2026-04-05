// PluginDockBox.Stubs.cs — Partial class stubs replacing excluded Designer.cs files.
// InitializeComponent() bodies build the Avalonia layout for each Finder control.

namespace SimPe.Interfaces
{
    partial class AFinderTool
    {
        private System.ComponentModel.IContainer components = null;
        private Avalonia.Controls.Button btStart = new Avalonia.Controls.Button();
        private Ambertation.Windows.Forms.XPTaskBoxSimple grp = new Ambertation.Windows.Forms.XPTaskBoxSimple();

        // Shared by all subclasses: the TextBlock in the dark header, and the
        // StackPanel into which each subclass adds its search fields.
        internal  Avalonia.Controls.TextBlock  _titleBlock;
        protected Avalonia.Controls.StackPanel ContentPanel;

        private void InitializeComponent()
        {
            // ── Dark gradient header ─────────────────────────────────────────
            _titleBlock = new Avalonia.Controls.TextBlock
            {
                Foreground        = Avalonia.Media.Brushes.White,
                FontWeight        = Avalonia.Media.FontWeight.SemiBold,
                FontSize          = 11,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                Margin            = new Avalonia.Thickness(8, 0),
            };
            var headerBar = new Avalonia.Controls.Border
            {
                Background = new Avalonia.Media.LinearGradientBrush
                {
                    StartPoint    = new Avalonia.RelativePoint(0, 0, Avalonia.RelativeUnit.Relative),
                    EndPoint      = new Avalonia.RelativePoint(0, 1, Avalonia.RelativeUnit.Relative),
                    GradientStops =
                    {
                        new Avalonia.Media.GradientStop(Avalonia.Media.Color.FromRgb(100, 116, 140), 0.0),
                        new Avalonia.Media.GradientStop(Avalonia.Media.Color.FromRgb( 80,  96, 120), 1.0),
                    },
                },
                MinHeight = 28,
                Child     = _titleBlock,
            };

            // ── Search-field area (subclasses populate ContentPanel) ──────────
            ContentPanel = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Vertical,
                Margin      = new Avalonia.Thickness(6, 4),
                Spacing     = 4,
            };

            // ── Start button ─────────────────────────────────────────────────
            btStart.Content             = "Start";
            btStart.Padding             = new Avalonia.Thickness(10, 3);
            btStart.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right;
            btStart.Margin              = new Avalonia.Thickness(0, 4, 6, 4);
            btStart.Click              += (s, e) => btStart_Click(s, System.EventArgs.Empty);

            // ── Outer layout: header | fields | start button ─────────────────
            var outer = new Avalonia.Controls.DockPanel { LastChildFill = false };
            Avalonia.Controls.DockPanel.SetDock(headerBar,   Avalonia.Controls.Dock.Top);
            Avalonia.Controls.DockPanel.SetDock(ContentPanel, Avalonia.Controls.Dock.Top);
            Avalonia.Controls.DockPanel.SetDock(btStart,     Avalonia.Controls.Dock.Bottom);
            outer.Children.Add(headerBar);
            outer.Children.Add(ContentPanel);
            outer.Children.Add(btStart);

            this.Content = outer;
        }

        // Helper used by subclasses to add a label + control row to ContentPanel.
        protected void AddRow(string labelText, Avalonia.Controls.Control field)
        {
            var lbl = new Avalonia.Controls.TextBlock
            {
                Text              = labelText,
                FontSize          = 11,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                Width             = 60,
            };
            var row = new Avalonia.Controls.DockPanel { LastChildFill = true };
            Avalonia.Controls.DockPanel.SetDock(lbl, Avalonia.Controls.Dock.Left);
            row.Children.Add(lbl);
            row.Children.Add(field);
            ContentPanel.Children.Add(row);
        }
    }
}

namespace SimPe.Plugin.Tool.Dockable.Finder
{
    partial class FindInStr
    {
        private System.ComponentModel.IContainer components = null;
        private Avalonia.Controls.TextBlock label1 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label2 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox   tbMatch = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.ComboBox  cbType  = new Avalonia.Controls.ComboBox();

        protected void InitializeComponent()
        {
            // Title set here — each tool's Title property writes back to grp.HeaderText
            // and to _titleBlock via the updated setter in AFinderTool.cs.
            this.Title = "String Search";

            cbType.ItemsSource = new string[]
            {
                "Equal [match]",
                "Starts with [match]",
                "Ends with [match]",
                "Contains [match]",
                "RegExp [match]",
            };

            tbMatch.Watermark = "search text";
            tbMatch.Padding   = new Avalonia.Thickness(3, 2);

            AddRow("Type:",  cbType);
            AddRow("Match:", tbMatch);
        }
    }

    partial class FindInSG
    {
        private System.ComponentModel.IContainer components = null;
        private Avalonia.Controls.TextBlock label3   = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.ComboBox  cbtypes  = new Avalonia.Controls.ComboBox();

        private new void InitializeComponent()
        {
            this.Title = "Scenegraph Search";
            cbtypes.Padding = new Avalonia.Thickness(3, 2);
            AddRow("RCOL:", cbtypes);
        }
    }

    partial class FindObj
    {
        private System.ComponentModel.IContainer components = null;
        private Avalonia.Controls.TextBlock label1 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox   tbGUID = new Avalonia.Controls.TextBox();

        private new void InitializeComponent()
        {
            this.Title    = "Object Search";
            tbGUID.Padding = new Avalonia.Thickness(3, 2);
            tbGUID.Width   = 120;
            tbGUID.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            AddRow("GUID:", tbGUID);
        }
    }

    partial class FindInCpf
    {
        private System.ComponentModel.IContainer components = null;
        private Avalonia.Controls.TextBlock label3 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label4 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox   tbName = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox   tbType = new Avalonia.Controls.TextBox();

        private new void InitializeComponent()
        {
            this.Title    = "CPF / BCON Search";
            tbType.Padding = new Avalonia.Thickness(3, 2);
            tbName.Padding = new Avalonia.Thickness(3, 2);
            AddRow("Type:",  tbType);
            AddRow("Field:", tbName);
        }
    }

    partial class FindInNmap
    {
        private System.ComponentModel.IContainer components = null;
        private new void InitializeComponent() { this.Title = "Name Map Search"; }
    }

    partial class FindInNref
    {
        private System.ComponentModel.IContainer components = null;
        private new void InitializeComponent() { this.Title = "Name Reference Search"; }
    }

    partial class FindTGI
    {
        private System.ComponentModel.IContainer components = null;
        private Avalonia.Controls.TextBlock label1   = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label2   = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label3   = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label4   = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label5   = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox   tbName   = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox   tbInstHi = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox   tbInstLo = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox   tbGroup  = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox   tbType   = new Avalonia.Controls.TextBox();

        private void InitializeComponent()
        {
            this.Title = "TGI Search";

            foreach (var tb in new[] { tbName, tbInstHi, tbInstLo, tbGroup, tbType })
                tb.Padding = new Avalonia.Thickness(3, 2);

            tbName.TextChanged += (s, e) => tbName_TextChanged(s, System.EventArgs.Empty);

            AddRow("Name:",    tbName);
            AddRow("Type:",    tbType);
            AddRow("Group:",   tbGroup);
            AddRow("Inst Hi:", tbInstHi);
            AddRow("Inst Lo:", tbInstLo);
        }
    }
}

namespace SimPe.Plugin.Tool.Dockable
{
    partial class FinderDock
    {
        private System.ComponentModel.IContainer components = null;
        // These fields are referenced from FinderDock.cs event handlers.
        private Avalonia.Controls.StackPanel xpGradientPanel1 = new Avalonia.Controls.StackPanel();
        private Avalonia.Controls.StackPanel panel1           = new Avalonia.Controls.StackPanel();
        private Avalonia.Controls.StackPanel panel2           = new Avalonia.Controls.StackPanel();
        private Avalonia.Controls.ComboBox   cbTask           = new Avalonia.Controls.ComboBox();
        private Avalonia.Controls.TextBlock  label1           = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock  pnErr            = new Avalonia.Controls.TextBlock();
        private Ambertation.Windows.Forms.XPTaskBoxSimple tbResult = new Ambertation.Windows.Forms.XPTaskBoxSimple();
        private SimPe.Scenegraph.Compat.ListView lv           = new SimPe.Scenegraph.Compat.ListView();
        private Avalonia.Controls.StackPanel toolBar1         = new Avalonia.Controls.StackPanel();
        private Avalonia.Controls.CheckBox   biList           = new Avalonia.Controls.CheckBox();
        private Avalonia.Controls.CheckBox   biTile           = new Avalonia.Controls.CheckBox();
        private Avalonia.Controls.CheckBox   biDetail         = new Avalonia.Controls.CheckBox();
        // Avalonia ListBox used in place of lv for actual display of results.
        private Avalonia.Controls.ListBox _resultsGrid;

        private void InitializeComponent()
        {
            // ── Toolbar ──────────────────────────────────────────────────────
            Avalonia.Controls.Button MakeToolBtn(string tip,
                System.EventHandler handler,
                Avalonia.Media.Imaging.Bitmap icon)
            {
                var btn = new Avalonia.Controls.Button
                {
                    Padding         = new Avalonia.Thickness(3),
                    Margin          = new Avalonia.Thickness(1, 0),
                    Background      = Avalonia.Media.Brushes.Transparent,
                    BorderThickness = new Avalonia.Thickness(0),
                };
                Avalonia.Controls.ToolTip.SetTip(btn, tip);
                if (icon != null)
                    btn.Content = new Avalonia.Controls.Image
                    {
                        Source              = icon,
                        Width               = 16,
                        Height              = 16,
                        Stretch             = Avalonia.Media.Stretch.Uniform,
                    };
                else
                    btn.Content = tip;
                btn.Click += (s, e) => handler(s, System.EventArgs.Empty);
                return btn;
            }

            var toolbar = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Background  = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromRgb(240, 240, 240)),
                Spacing     = 0,
            };
            toolbar.Children.Add(MakeToolBtn("List View",     Activate_biList,    SimPe.LoadIcon.LoadAvaloniaBitmap("FinderDock_biList.Image.png")));
            toolbar.Children.Add(MakeToolBtn("Tiled View",    Activate_biTile,    SimPe.LoadIcon.LoadAvaloniaBitmap("FinderDock_biTile.Image.png")));
            toolbar.Children.Add(MakeToolBtn("Detailed View", Activate_biDetails, SimPe.LoadIcon.LoadAvaloniaBitmap("FinderDock_biDetail.Image.png")));

            // ── "Find:" row ──────────────────────────────────────────────────
            var findLabel = new Avalonia.Controls.TextBlock
            {
                Text              = "Find:",
                FontSize          = 11,
                FontWeight        = Avalonia.Media.FontWeight.Bold,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                Margin            = new Avalonia.Thickness(6, 0, 4, 0),
            };
            cbTask.FontSize = 11;
            cbTask.Margin   = new Avalonia.Thickness(0, 2, 6, 2);
            cbTask.SelectionChanged += (s, e) => cbTask_SelectedIndexChanged(s, System.EventArgs.Empty);
            var findRow = new Avalonia.Controls.DockPanel
            {
                LastChildFill = true,
                Margin        = new Avalonia.Thickness(0, 10, 0, 0),
            };
            Avalonia.Controls.DockPanel.SetDock(findLabel, Avalonia.Controls.Dock.Left);
            findRow.Children.Add(findLabel);
            findRow.Children.Add(cbTask);

            // ── Search-tool container (filled when cbTask selection changes) ──
            // pnContainer is the StackPanel declared in FinderDock.cs.
            pnContainer = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Vertical,
            };
            var searchGroupBox = new Avalonia.Controls.Border
            {
                Background      = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromRgb(220, 228, 238)),
                BorderBrush     = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromRgb(170, 185, 205)),
                BorderThickness = new Avalonia.Thickness(1),
                CornerRadius    = new Avalonia.CornerRadius(3),
                Margin          = new Avalonia.Thickness(6, 4, 6, 4),
                Child           = pnContainer,
            };

            // ── pnErr (error/truncation notice) ──────────────────────────────
            pnErr.FontSize   = 11;
            pnErr.Foreground = Avalonia.Media.Brushes.DarkRed;
            pnErr.Margin     = new Avalonia.Thickness(6, 2);
            pnErr.IsVisible  = false;

            // ── "Results" groupbox ───────────────────────────────────────────
            var resultsHeader = new Avalonia.Controls.Border
            {
                Background = new Avalonia.Media.LinearGradientBrush
                {
                    StartPoint    = new Avalonia.RelativePoint(0, 0, Avalonia.RelativeUnit.Relative),
                    EndPoint      = new Avalonia.RelativePoint(0, 1, Avalonia.RelativeUnit.Relative),
                    GradientStops =
                    {
                        new Avalonia.Media.GradientStop(Avalonia.Media.Color.FromRgb(100, 116, 140), 0.0),
                        new Avalonia.Media.GradientStop(Avalonia.Media.Color.FromRgb( 80,  96, 120), 1.0),
                    },
                },
                CornerRadius = new Avalonia.CornerRadius(3, 3, 0, 0),
                MinHeight    = 28,
                Child        = new Avalonia.Controls.TextBlock
                {
                    Text              = "Results",
                    Foreground        = Avalonia.Media.Brushes.White,
                    FontWeight        = Avalonia.Media.FontWeight.SemiBold,
                    FontSize          = 11,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                    Margin            = new Avalonia.Thickness(8, 0),
                },
            };

            // ListBox for showing search results.
            _resultsGrid = new Avalonia.Controls.ListBox
            {
                Background = Avalonia.Media.Brushes.White,
                FontSize   = 11,
            };

            var resultsScroll = new Avalonia.Controls.ScrollViewer
            {
                Content                        = _resultsGrid,
                HorizontalScrollBarVisibility  = Avalonia.Controls.Primitives.ScrollBarVisibility.Auto,
                VerticalScrollBarVisibility    = Avalonia.Controls.Primitives.ScrollBarVisibility.Auto,
            };

            // Results section: header + scroll area inside a groupbox border.
            var resultsInner = new Avalonia.Controls.DockPanel { LastChildFill = true };
            Avalonia.Controls.DockPanel.SetDock(resultsHeader, Avalonia.Controls.Dock.Top);
            Avalonia.Controls.DockPanel.SetDock(pnErr,         Avalonia.Controls.Dock.Top);
            resultsInner.Children.Add(resultsHeader);
            resultsInner.Children.Add(pnErr);
            resultsInner.Children.Add(resultsScroll);

            var resultsGroupBox = new Avalonia.Controls.Border
            {
                BorderBrush     = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromRgb(170, 185, 205)),
                BorderThickness = new Avalonia.Thickness(1),
                CornerRadius    = new Avalonia.CornerRadius(3),
                Margin          = new Avalonia.Thickness(6, 4, 6, 4),
                Child           = resultsInner,
            };

            // ── Outer layout ─────────────────────────────────────────────────
            var outer = new Avalonia.Controls.DockPanel { LastChildFill = true };
            Avalonia.Controls.DockPanel.SetDock(toolbar,        Avalonia.Controls.Dock.Top);
            Avalonia.Controls.DockPanel.SetDock(findRow,        Avalonia.Controls.Dock.Top);
            Avalonia.Controls.DockPanel.SetDock(searchGroupBox, Avalonia.Controls.Dock.Top);
            outer.Children.Add(toolbar);
            outer.Children.Add(findRow);
            outer.Children.Add(searchGroupBox);
            outer.Children.Add(resultsGroupBox);  // LastChildFill — fills remaining space

            this.AvaloniaContent = outer;
            this.Controls.Add(outer);
        }
    }
}
