// SimPe.pjsecoder.Stubs.cs — Designer stubs replacing excluded Designer.cs files.
// WinForms types replaced with Avalonia equivalents.

using Avalonia.Controls;
using SimPe.Scenegraph.Compat;

namespace pjse
{
    partial class CompareButton
    {
        private System.ComponentModel.IContainer components = null;
        private ContextMenu cmenuCompare = new ContextMenu();
        private MenuItem currentObjectspackageToolStripMenuItem = new MenuItem();
        private void InitializeComponent() { }
    }

    partial class PickANumber
    {
        private System.ComponentModel.IContainer components = null;
        private Grid tableLayoutPanel1 = new Grid();
        private TextBoxCompat textBox1 = new TextBoxCompat();
        private RadioButton radioButton1 = new RadioButton();
        private ButtonCompat btnCancel = new ButtonCompat();
        private ButtonCompat btnOK = new ButtonCompat();
        private LabelCompat label1 = new LabelCompat();
        private void InitializeComponent() { }
    }

    partial class LabelledDataOwner
    {
        private System.ComponentModel.IContainer components = null;
        private Grid tableLayoutPanel1 = new Grid();
        private LabelCompat lbLabel = new LabelCompat();
        private StackPanel flpValue = new StackPanel();
        private ComboBoxCompat cbPicker = new ComboBoxCompat();
        private TextBoxCompat tbVal = new TextBoxCompat();
        private LabelCompat lbInstance = new LabelCompat();
        private ComboBoxCompat cbDataOwner = new ComboBoxCompat();
        private StackPanel flpCheckBoxes = new StackPanel();
        private CheckBoxCompat2 ckbDecimal = new CheckBoxCompat2();
        private CheckBoxCompat2 ckbUseInstancePicker = new CheckBoxCompat2();
        private void InitializeComponent() { }
    }

    partial class TtabAnimalMotiveWiz
    {
        private System.ComponentModel.IContainer components = null;
        private ButtonCompat btnOkay = new ButtonCompat();
        private ButtonCompat btnCancel = new ButtonCompat();
        private StackPanel panel1 = new StackPanel();
        private LabelCompat label1 = new LabelCompat();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI1 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private ButtonCompat btnPlus = new ButtonCompat();
        private ButtonCompat btnMinus = new ButtonCompat();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI2 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI3 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI4 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI5 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI6 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI7 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI8 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI9 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI10 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI11 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI12 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI13 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI14 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI15 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI ttabSingleMotiveUI16 = new SimPe.PackedFiles.UserInterface.TtabSingleMotiveUI();
        private void InitializeComponent() { }
    }

    partial class pjse_banner
    {
        private System.ComponentModel.IContainer components = null;
        private ButtonCompat btnHelp = new ButtonCompat();
        private ButtonCompat btnFloat = new ButtonCompat();
        private ButtonCompat btnView = new ButtonCompat();
        private StackPanel flpButtons = new StackPanel();
        private ButtonCompat btnTree = new ButtonCompat();
        private ButtonCompat btnSibling = new ButtonCompat();
        private ButtonCompat btnExtract = new ButtonCompat();
        private ButtonCompat btnRefreshFT = new ButtonCompat();
        private LabelCompat lbLabel = new LabelCompat();
        private void InitializeComponent()
        {
            // Banner label
            lbLabel.Content = "PJSE: file type Editor";
            lbLabel.FontWeight = Avalonia.Media.FontWeight.Bold;
            lbLabel.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            lbLabel.Margin = new Avalonia.Thickness(6, 0);

            // Buttons
            btnTree.Content = "Comments"; btnTree.IsVisible = false;
            btnTree.Click += (s, e) => this.btnTree_Click(s, e);
            btnSibling.Content = "{Type}"; btnSibling.IsVisible = false;
            btnSibling.Click += (s, e) => this.btnSibling_Click(s, e);
            btnView.Content = "View"; btnView.IsVisible = false;
            btnView.Click += (s, e) => this.btnView_Click(s, e);
            btnFloat.Content = "Float"; btnFloat.IsVisible = false;
            btnFloat.Click += (s, e) => this.btnFloat_Click(s, e);
            btnExtract.Content = "Extract"; btnExtract.IsVisible = false;
            btnExtract.Click += (s, e) => this.btnExtract_Click(s, e);
            btnRefreshFT.Content = "RFT"; btnRefreshFT.IsVisible = true;
            btnRefreshFT.Click += (s, e) => this.btnRefreshFT_Click(s, e);
            btnHelp.Content = "Help";
            btnHelp.Click += (s, e) => this.btnHelp_Click(s, e);

            // Button panel (horizontal)
            var headerButtonStyle = new Avalonia.Thickness(2, 2);
            btnTree.Margin = headerButtonStyle; btnTree.Background = Avalonia.Media.Brushes.White;
            btnSibling.Margin = headerButtonStyle; btnSibling.Background = Avalonia.Media.Brushes.White;
            btnView.Margin = headerButtonStyle; btnView.Background = Avalonia.Media.Brushes.White;
            btnFloat.Margin = headerButtonStyle; btnFloat.Background = Avalonia.Media.Brushes.White;
            btnExtract.Margin = headerButtonStyle; btnExtract.Background = Avalonia.Media.Brushes.White;
            btnRefreshFT.Margin = headerButtonStyle; btnRefreshFT.Background = Avalonia.Media.Brushes.White;
            btnHelp.Margin = headerButtonStyle; btnHelp.Background = Avalonia.Media.Brushes.White;

            flpButtons.Orientation = Avalonia.Layout.Orientation.Horizontal;
            flpButtons.Spacing = 4;
            flpButtons.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            flpButtons.Children.Add(btnTree);
            flpButtons.Children.Add(btnSibling);
            flpButtons.Children.Add(btnView);
            flpButtons.Children.Add(btnFloat);
            flpButtons.Children.Add(btnExtract);
            flpButtons.Children.Add(btnRefreshFT);
            flpButtons.Children.Add(btnHelp);

            // Layout: label on left, buttons on right, gray background
            var dock = new Avalonia.Controls.DockPanel
            {
                Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromRgb(200, 200, 200)),
                MinHeight = 27
            };
            Avalonia.Controls.DockPanel.SetDock(flpButtons, Avalonia.Controls.Dock.Right);
            dock.Children.Add(flpButtons);
            dock.Children.Add(lbLabel);
            this.Content = dock;
        }
    }
}

namespace SimPe.Plugin
{
    using Avalonia.Controls;
    using SimPe.Scenegraph.Compat;
    using Ambertation.Windows.Forms;

    partial class GUIDChooser
    {
        private System.ComponentModel.IContainer components = null;
        private FlowLayoutPanel flpMain = new FlowLayoutPanel();
        private LabelCompat lbLabel = new LabelCompat();
        private ComboBoxCompat cbKnownObjects = new ComboBoxCompat();
        private TextBoxCompat tbGUID = new TextBoxCompat();
        private void InitializeComponent()
        {
            this.lbLabel.Content = "LabelCompat";
            this.cbKnownObjects.SelectionChanged += (s, e) => this.cbKnownObjects_SelectedIndexChanged(s, e);
            this.tbGUID.Text = "0xDDDDDDDD";
            this.tbGUID.TextChanged += (s, e) => this.tbGUID_TextChanged(s, e);
            this.tbGUID.LostFocus += (s, e) => this.hex32_Validated(s, e);
        }
    }

    partial class TreesPackedFileUI
    {
        private System.ComponentModel.IContainer components = null;
        private StackPanel panel1 = new StackPanel();
        private ButtonCompat btMove = new ButtonCompat();
        private ButtonCompat btRemove = new ButtonCompat();
        private StackPanel pnhidim = new StackPanel();
        private TextBoxCompat tbheader = new TextBoxCompat();
        private TextBoxCompat tbunk0 = new TextBoxCompat();
        private TextBoxCompat tbunk1 = new TextBoxCompat();
        private TextBoxCompat tbunk2 = new TextBoxCompat();
        private TextBoxCompat tbunk3 = new TextBoxCompat();
        private TextBoxCompat tbunk4 = new TextBoxCompat();
        private TextBoxCompat tbunk5 = new TextBoxCompat();
        private TextBoxCompat tbversion = new TextBoxCompat();
        private TextBoxCompat tbcount = new TextBoxCompat();
        private TextBoxCompat tbfilename = new TextBoxCompat();
        private LabelCompat lbfilename = new LabelCompat();
        private ListView listList = new ListView();
        private ColumnHeader Comment = new ColumnHeader();
        private ColumnHeader Zero2 = new ColumnHeader();
        private ColumnHeader Block1 = new ColumnHeader();
        private ColumnHeader Block2 = new ColumnHeader();
        private ColumnHeader Block3 = new ColumnHeader();
        private ColumnHeader Block4 = new ColumnHeader();
        private ColumnHeader Block5 = new ColumnHeader();
        private ColumnHeader Block6 = new ColumnHeader();
        private ColumnHeader Block7 = new ColumnHeader();
        private ColumnHeader Block8 = new ColumnHeader();
        private ColumnHeader Block9 = new ColumnHeader();
        private ListView listLast = new ListView();
        private ColumnHeader Indecks = new ColumnHeader();
        private ColumnHeader Comment2 = new ColumnHeader();
        private XPTaskBoxSimple taskBox1 = new XPTaskBoxSimple();
        private XPTaskBoxSimple taskBox2 = new XPTaskBoxSimple();
        private TextBoxCompat textBox2 = new TextBoxCompat();
        private TextBoxCompat textBox3 = new TextBoxCompat();
        private TextBoxCompat textBox4 = new TextBoxCompat();
        private TextBoxCompat textBox5 = new TextBoxCompat();
        private TextBoxCompat textBox6 = new TextBoxCompat();
        private TextBoxCompat textBox7 = new TextBoxCompat();
        private TextBoxCompat textBox8 = new TextBoxCompat();
        private TextBoxCompat textBox9 = new TextBoxCompat();
        private TextBoxCompat textBox10 = new TextBoxCompat();
        private TextBoxCompat textBox11 = new TextBoxCompat();
        private TextBoxCompat tbComment = new TextBoxCompat();
        private TextBoxCompat tbComment2 = new TextBoxCompat();
        private LabelCompat lbComment = new LabelCompat();
        private LabelCompat lbComment2 = new LabelCompat();
        private ButtonCompat btDown = new ButtonCompat();
        private ButtonCompat btBhave = new ButtonCompat();
        private ButtonCompat btAdder = new ButtonCompat();
        private void InitializeComponent() { }
    }
}

namespace pjse.BhavOperandWizards.WizBhav
{
    using Avalonia.Controls;

    partial class UI
    {
        private System.ComponentModel.IContainer components = null;
        private LabelCompat label1 = new LabelCompat();
        private StackPanel pnWizBhav = new StackPanel();
        private Grid tlpHeader = new Grid();
        private pjse.LabelledDataOwner ldocArg1 = new pjse.LabelledDataOwner();
        private pjse.LabelledDataOwner ldocArg2 = new pjse.LabelledDataOwner();
        private pjse.LabelledDataOwner ldocArg3 = new pjse.LabelledDataOwner();
        private pjse.LabelledDataOwner ldocArg4 = new pjse.LabelledDataOwner();
        private pjse.LabelledDataOwner ldocArg5 = new pjse.LabelledDataOwner();
        private pjse.LabelledDataOwner ldocArg6 = new pjse.LabelledDataOwner();
        private pjse.LabelledDataOwner ldocArg7 = new pjse.LabelledDataOwner();
        private pjse.LabelledDataOwner ldocArg8 = new pjse.LabelledDataOwner();
        private LabelCompat lbArg1 = new LabelCompat();
        private LabelCompat lbArg2 = new LabelCompat();
        private LabelCompat lbArg3 = new LabelCompat();
        private LabelCompat lbArg4 = new LabelCompat();
        private LabelCompat lbArg5 = new LabelCompat();
        private LabelCompat lbArg6 = new LabelCompat();
        private LabelCompat lbArg7 = new LabelCompat();
        private LabelCompat lbArg8 = new LabelCompat();
        private LabelCompat lbArgC = new LabelCompat();
        private LabelCompat lbBhavName = new LabelCompat();
        private StackPanel flpFormat = new StackPanel();
        private RadioButton rbTemps = new RadioButton();
        private RadioButton rbCallers = new RadioButton();
        private RadioButton rbOld = new RadioButton();
        private RadioButton rbNew = new RadioButton();
        private StackPanel flpOptions = new StackPanel();
        private CheckBoxCompat2 ckbDecimal = new CheckBoxCompat2();
        private CheckBoxCompat2 ckbUseInstancePicker = new CheckBoxCompat2();
        private void InitializeComponent() { }
    }
}
