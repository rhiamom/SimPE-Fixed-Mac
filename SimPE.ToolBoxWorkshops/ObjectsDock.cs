/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSweatshop                                 *
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

using Ambertation.Windows.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using SkiaSharp;
using System.Windows.Forms;
using SimPe.Scenegraph.Compat;
using Image = System.Drawing.Image;
using TreeNode = SimPe.Scenegraph.Compat.TreeNode;
using TreeView = SimPe.Scenegraph.Compat.TreeView;
using TreeViewEventArgs = SimPe.Scenegraph.Compat.TreeViewEventArgs;
using TreeNodeCollection = SimPe.Scenegraph.Compat.TreeNode.TreeNodeCollection;
using LinkLabel = SimPe.Scenegraph.Compat.LinkLabel;
using ToolTip = SimPe.Scenegraph.Compat.ToolTip;
using DialogResult = System.Windows.Forms.DialogResult;
using LinkLabelLinkClickedEventArgs = SimPe.Scenegraph.Compat.LinkLabelLinkClickedEventArgs;

namespace SimPe.Plugin.Tool.Dockable
{
	/// <summary>
	/// Summary description for DoackableObjectWorkshop.
	/// </summary>
    public class dcObjectWorkshop : Ambertation.Windows.Forms.DockPanel
	{        
        class MyTreeView : SimPe.Scenegraph.Compat.TreeView
        {            
            public MyTreeView()
                : base()
            {                                
            }

            public void DoBeginUpdate()
            {
                this.BeginUpdate();
                //this.IsVisible = false;
            }

            public void DoEndUpdate(bool vis)
            {
                this.EndUpdate();
                //this.Visible = vis;
            }
                                   
        }
        private Avalonia.Controls.Grid xpGradientPanel1;
		private SimPe.Wizards.Wizard wizard1;
		private SimPe.Wizards.WizardStepPanel wizardStepPanel1;
		private Avalonia.Controls.Button button2;
		private Avalonia.Controls.TextBlock label1;
		private Avalonia.Controls.TextBlock label2;
		private Avalonia.Controls.Button button1;
		private SimPe.Wizards.WizardStepPanel wizardStepPanel2;
		private Avalonia.Controls.ListBox lb;
		private MyTreeView tv;
		private Avalonia.Controls.GridSplitter splitter1;
		private Avalonia.Controls.StackPanel panel1;
		private Avalonia.Controls.StackPanel xpTaskBoxSimple2;
		private SimPe.Wizards.WizardStepPanel wizardStepPanel3;
        private Avalonia.Controls.StackPanel  xpTaskBoxSimple1;
        private Avalonia.Controls.StackPanel gbRecolor;
        private Avalonia.Controls.CheckBox cbColorExt;
        private Avalonia.Controls.StackPanel gbClone;
        internal Avalonia.Controls.CheckBox cbanim;
        internal Avalonia.Controls.CheckBox cbwallmask;
        internal Avalonia.Controls.CheckBox cbparent;
        internal Avalonia.Controls.CheckBox cbclean;
        internal Avalonia.Controls.CheckBox cbfix;
        internal Avalonia.Controls.CheckBox cbdefault;
        internal Avalonia.Controls.CheckBox cbgid;
		private Avalonia.Controls.StackPanel panel2;
		private Avalonia.Controls.Button button3;
        internal Avalonia.Controls.ComboBox cbTask;
		private Avalonia.Controls.TextBlock label3;
		private SimPe.Wizards.WizardStepPanel wizardStepPanel4;
		private Avalonia.Controls.StackPanel pnWait;
		private Avalonia.Controls.TextBlock lberr;
		private Avalonia.Controls.TextBlock lbfinload;
        private Avalonia.Controls.TextBlock lbwait;
        private SimPe.Scenegraph.Compat.PictureBox pbWait;
        private Avalonia.Controls.TextBlock lbfinished;
		private Avalonia.Controls.TextBlock label4;
		private SimPe.Scenegraph.Compat.ImageList ilist;
		private SimPe.Plugin.Tool.Dockable.ObjectPreview op1;
		private SimPe.Plugin.Tool.Dockable.ObjectPreview op2;
        internal Avalonia.Controls.CheckBox cbRemTxt;
        internal Avalonia.Controls.CheckBox cbOrgGmdc;
		private SimPe.Wizards.WizardStepPanel wizardStepPanel5;
        private Avalonia.Controls.StackPanel xpTaskBoxSimple3;
		private Avalonia.Controls.TextBlock label5;
		private Avalonia.Controls.TextBlock label6;
		private Avalonia.Controls.TextBlock label7;
		private Avalonia.Controls.TextBox tbName;
		private Avalonia.Controls.TextBox tbPrice;
		private Avalonia.Controls.TextBox tbDesc;
        internal Avalonia.Controls.CheckBox cbDesc;
        internal Avalonia.Controls.CheckBox cbstrlink;
		private System.ComponentModel.IContainer components;
		private Avalonia.Controls.Button button4;
        private Avalonia.Controls.StackPanel xpAdvanced;
		private Avalonia.Controls.TextBox tbGroup;
		private Avalonia.Controls.Button button5;
		private Avalonia.Controls.TextBox tbCresName;
		private Avalonia.Controls.Button button6;
		private Avalonia.Controls.TextBox tbGUID;
        private ToolStrip toolStrip1;
        private ToolStripButton biPrev;
        private ToolStripButton biNext;
        private ToolStripButton biFinish;
        private ToolStripButton biAbort;
        private ToolStripButton biCatalog;
        // Avalonia toolbar buttons (visual counterparts to the ToolStripButton stubs)
        private Avalonia.Controls.Button _btnPrev, _btnNext, _btnFinish, _btnAbort, _btnCatalog;
        // Avalonia TreeView for the object catalog (renders compat tv.Nodes data)
        private Avalonia.Controls.TreeView _avCatalogTree;
        private LinkLabel llCloneDef;
        private ToolTip toolTip1;

		ObjectWorkshopRegistry registry;
		public dcObjectWorkshop()
		{
			this.op1 = new SimPe.Plugin.Tool.Dockable.ObjectPreview();
			this.op2 = new SimPe.Plugin.Tool.Dockable.ObjectPreview();
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
                // this.wizard1.BackgroundImageLayout = ImageLayout.Zoom; // WinForms-only, skipped

            this.xpAdvanced.IsVisible = UserVerification.HaveUserId;
            // op1.SuspendLayout(); - (prevented op1 layout, causung the title to be scrolled and the description to be cut off) Chris Hatch
			// 
			// op1
			// 
			this.op1.LoadCustomImage = true;
			this.op1.SelectedObject = null;
            // op2.ResumeLayout(); - (op2 layout was never suspended) Chris Hatch
			// 
			// op2
			// 
			this.op2.LoadCustomImage = true;
			this.op2.SelectedObject = null;

			//do the regular initialization Work
			wizard1.Start();
            ThemeManager tm = ThemeManager.Global.CreateChild();
			tm.AddControl(this.xpGradientPanel1);
			tm.AddControl(this.toolStrip1);
			tm.AddControl(this.splitter1);
			tm.AddControl(this.xpAdvanced);
			tm.AddControl(this.xpTaskBoxSimple1);
			tm.AddControl(this.xpTaskBoxSimple2);
			tm.AddControl(this.xpTaskBoxSimple3);
			tm.AddControl(this.gbRecolor);
			tm.AddControl(this.gbClone);
            
            if (Helper.XmlRegistry.UseBigIcons)
                toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);  

			biFinish.IsVisible = wizard1.FinishEnabled;
			this.biAbort.IsVisible = wizard1.PrevEnabled;
			biNext.Enabled = wizard1.NextEnabled;
			biPrev.Enabled = wizard1.PrevEnabled;
			if (_btnFinish != null) _btnFinish.IsVisible = wizard1.FinishEnabled;
			if (_btnAbort != null) _btnAbort.IsVisible = wizard1.PrevEnabled;
			if (_btnNext != null) _btnNext.IsEnabled = wizard1.NextEnabled;
			if (_btnPrev != null) _btnPrev.IsEnabled = wizard1.PrevEnabled;						
			ilist.ImageSize = new Size(Helper.XmlRegistry.OWThumbSize, Helper.XmlRegistry.OWThumbSize);
			registry = new ObjectWorkshopRegistry(this);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (registry!=null) registry.Dispose();
				registry = null;
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

        //make sure that this order is maintained after each edit of the GUI
        /*
         *  this.wizard1.Controls.Add(this.wizardStepPanel1);
            this.wizard1.Controls.Add(this.wizardStepPanel2);
            this.wizard1.Controls.Add(this.wizardStepPanel3);
            this.wizard1.Controls.Add(this.wizardStepPanel5);
            this.wizard1.Controls.Add(this.wizardStepPanel4);
         */

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.xpGradientPanel1 = new Avalonia.Controls.Grid();
            this.wizard1 = new SimPe.Wizards.Wizard();
            this.wizardStepPanel1 = new SimPe.Wizards.WizardStepPanel();
            this.xpAdvanced = new Avalonia.Controls.StackPanel();
            this.button6 = new Avalonia.Controls.Button();
            this.tbGUID = new Avalonia.Controls.TextBox();
            this.button5 = new Avalonia.Controls.Button();
            this.tbCresName = new Avalonia.Controls.TextBox();
            this.button4 = new Avalonia.Controls.Button();
            this.tbGroup = new Avalonia.Controls.TextBox();
            this.label4 = new Avalonia.Controls.TextBlock();
            this.button2 = new Avalonia.Controls.Button();
            this.label1 = new Avalonia.Controls.TextBlock();
            this.button1 = new Avalonia.Controls.Button();
            this.label2 = new Avalonia.Controls.TextBlock();
            this.wizardStepPanel2 = new SimPe.Wizards.WizardStepPanel();
            this.lb = new Avalonia.Controls.ListBox();
            this.tv = new SimPe.Plugin.Tool.Dockable.dcObjectWorkshop.MyTreeView();
            this.splitter1 = new Avalonia.Controls.GridSplitter();
            this.panel1 = new Avalonia.Controls.StackPanel();
            this.xpTaskBoxSimple2 = new Avalonia.Controls.StackPanel();
            this.wizardStepPanel3 = new SimPe.Wizards.WizardStepPanel();
            this.xpTaskBoxSimple1 = new Avalonia.Controls.StackPanel();
            this.gbRecolor = new Avalonia.Controls.StackPanel();
            this.cbColorExt = new Avalonia.Controls.CheckBox();
            this.gbClone = new Avalonia.Controls.StackPanel();
            this.llCloneDef = new SimPe.Scenegraph.Compat.LinkLabel();
            this.cbstrlink = new Avalonia.Controls.CheckBox();
            this.cbDesc = new Avalonia.Controls.CheckBox();
            this.cbOrgGmdc = new Avalonia.Controls.CheckBox();
            this.cbRemTxt = new Avalonia.Controls.CheckBox();
            this.cbanim = new Avalonia.Controls.CheckBox();
            this.cbwallmask = new Avalonia.Controls.CheckBox();
            this.cbparent = new Avalonia.Controls.CheckBox();
            this.cbclean = new Avalonia.Controls.CheckBox();
            this.cbfix = new Avalonia.Controls.CheckBox();
            this.cbdefault = new Avalonia.Controls.CheckBox();
            this.cbgid = new Avalonia.Controls.CheckBox();
            this.panel2 = new Avalonia.Controls.StackPanel();
            this.button3 = new Avalonia.Controls.Button();
            this.cbTask = new Avalonia.Controls.ComboBox();
            this.label3 = new Avalonia.Controls.TextBlock();
            this.wizardStepPanel5 = new SimPe.Wizards.WizardStepPanel();
            this.xpTaskBoxSimple3 = new Avalonia.Controls.StackPanel();
            this.tbDesc = new Avalonia.Controls.TextBox();
            this.tbPrice = new Avalonia.Controls.TextBox();
            this.tbName = new Avalonia.Controls.TextBox();
            this.label7 = new Avalonia.Controls.TextBlock();
            this.label6 = new Avalonia.Controls.TextBlock();
            this.label5 = new Avalonia.Controls.TextBlock();
            this.wizardStepPanel4 = new SimPe.Wizards.WizardStepPanel();
            this.pnWait = new Avalonia.Controls.StackPanel();
            this.pbWait = new SimPe.Scenegraph.Compat.PictureBox();
            this.lbfinished = new Avalonia.Controls.TextBlock();
            this.lberr = new Avalonia.Controls.TextBlock();
            this.lbfinload = new Avalonia.Controls.TextBlock();
            this.lbwait = new Avalonia.Controls.TextBlock();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.biPrev = new System.Windows.Forms.ToolStripButton();
            this.biNext = new System.Windows.Forms.ToolStripButton();
            this.biFinish = new System.Windows.Forms.ToolStripButton();
            this.biAbort = new System.Windows.Forms.ToolStripButton();
            this.biCatalog = new System.Windows.Forms.ToolStripButton();
            this.ilist = new SimPe.Scenegraph.Compat.ImageList();
            this.toolTip1 = new SimPe.Scenegraph.Compat.ToolTip();

            // Wizard events
            this.wizard1.FinishEnabled = false;
            this.wizard1.NextEnabled = false;
            this.wizard1.PrevEnabled = false;
            this.wizard1.ChangedFinishState += new SimPe.Wizards.WizardHandle(this.wizard1_ChangedFinishState);
            this.wizard1.ShowStep += new SimPe.Wizards.WizardChangeHandle(this.wizard1_ShowStep);
            this.wizard1.ChangedPrevState += new SimPe.Wizards.WizardHandle(this.wizard1_ChangedPrevState);
            this.wizard1.PrepareStep += new SimPe.Wizards.WizardStepChangeHandle(this.wizard1_PrepareStep);
            this.wizard1.ChangedNextState += new SimPe.Wizards.WizardHandle(this.wizard1_ChangedNextState);
            this.wizard1.ShowedStep += new SimPe.Wizards.WizardShowedHandle(this.wizard1_ShowedStep);

            this.wizardStepPanel2.Activate += new SimPe.Wizards.WizardChangeHandle(this.wizardStepPanel2_Activate);
            this.wizardStepPanel2.Prepare += new SimPe.Wizards.WizardStepChangeHandle(this.wizardStepPanel2_Prepare);
            this.wizardStepPanel3.Activate += new SimPe.Wizards.WizardChangeHandle(this.wizardStepPanel3_Activate);
            this.wizardStepPanel3.Activated += new SimPe.Wizards.WizardStepHandle(this.wizardStepPanel3_Activated);
            this.wizardStepPanel5.Activate += new SimPe.Wizards.WizardChangeHandle(this.wizardStepPanel5_Activate);
            this.wizardStepPanel5.Activated += new SimPe.Wizards.WizardStepHandle(this.wizardStepPanel5_Activated);
            this.wizardStepPanel4.Last = true;
            this.wizardStepPanel4.Activate += new SimPe.Wizards.WizardChangeHandle(this.wizardStepPanel4_Activate);
            this.wizardStepPanel4.Activated += new SimPe.Wizards.WizardStepHandle(this.wizardStepPanel4_Activated);

            // Button click events
            this.button6.Click += (s, e) => this.button6_Click(s, e);
            this.button5.Click += (s, e) => this.button5_Click(s, e);
            this.button4.Click += (s, e) => this.button4_Click(s, e);
            this.button2.Click += (s, e) => this.button2_Click(s, e);
            this.button1.Click += (s, e) => this.button1_Click(s, e);
            this.button3.Click += (s, e) => this.button3_Click(s, e);
            this.biPrev.Click += (s, e) => this.Activate_biPrev(s, e);
            this.biNext.Click += (s, e) => this.Activate_biNext(s, e);
            this.biFinish.Click += (s, e) => this.ActivateFinish(s, e);
            this.biAbort.Click += (s, e) => this.biAbort_Activate(s, e);
            this.biCatalog.Click += (s, e) => this.Activate_biCatalog(s, e);
            this.lberr.PointerPressed += (s, e) => this.lberr_Click(s, EventArgs.Empty);

            // List/tree events
            this.lb.SelectionChanged += (s, e) => this.lb_SelectedIndexChanged(s, EventArgs.Empty);
            this.tv.AfterSelect += (s, e) => this.tv_AfterSelect(s, e);

            // cbTask
            this.cbTask.SelectionChanged += (s, e) => this.cbTask_SelectedIndexChanged(s, EventArgs.Empty);

            // CheckBox events
            this.cbDesc.IsChecked = true;
            this.cbDesc.IsCheckedChanged += (s, e) => this.cbDesc_CheckedChanged(s, EventArgs.Empty);
            this.cbfix.IsChecked = true;
            this.cbfix.IsCheckedChanged += (s, e) => this.cbfix_CheckedChanged(s, EventArgs.Empty);
            this.cbColorExt.IsChecked = true;
            this.cbwallmask.IsChecked = true;
            this.cbRemTxt.IsChecked = true;
            this.cbclean.IsChecked = true;
            this.cbdefault.IsChecked = true;
            this.cbgid.IsChecked = true;
            this.biCatalog.Checked = true;

            // LinkLabel
            this.llCloneDef.LinkClicked += this.SetDefaultsForClone;

            // ImageList
            this.ilist.ColorDepth = SimPe.Scenegraph.Compat.ColorDepth.Depth32Bit;

            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Items.Add(this.biPrev);
            this.toolStrip1.Items.Add(this.biNext);
            this.toolStrip1.Items.Add(this.biFinish);
            this.toolStrip1.Items.Add(this.biAbort);
            this.toolStrip1.Items.Add(this.biCatalog);

            // ── Text properties (formerly set by WinForms designer via .resx) ──
            this.label1.Text = "Welcome to the Object Workshop.";
            this.label1.FontWeight = Avalonia.Media.FontWeight.Bold;
            this.label2.Text = "The Object Data is not yet loaded. Please choose \"Start\", to load the Object Browser. Or click \"Open...\", to load a custom Package.";
            this.label2.TextWrapping = Avalonia.Media.TextWrapping.Wrap;
            this.label4.Text = "Biggest thanks go to Numenor and RGiles for making the Object Workshop possible in the first place, and their constant help in the developing process.";
            this.label4.TextWrapping = Avalonia.Media.TextWrapping.Wrap;
            this.button1.Content = "Start";
            this.button2.Content = "Open...";
            this.button3.Content = "Start";
            this.button4.Content = "Open by Group ID";
            this.button5.Content = "Open by CRES-Name";
            this.button6.Content = "Open by GUID";
            this.tbGroup.Text = "0x7F000000";
            this.tbGUID.Text = "0x00000000";
            this.label3.Text = "Task:";
            this.label3.FontWeight = Avalonia.Media.FontWeight.Bold;
            this.label5.Text = "Title:";
            this.label5.FontWeight = Avalonia.Media.FontWeight.Bold;
            this.label6.Text = "Description:";
            this.label6.FontWeight = Avalonia.Media.FontWeight.Bold;
            this.label7.Text = "Price:";
            this.label7.FontWeight = Avalonia.Media.FontWeight.Bold;
            this.lbwait.Text = "Please Wait";
            this.lbwait.FontWeight = Avalonia.Media.FontWeight.Bold;
            this.lbfinished.Text = "Object was created.";
            this.lbfinished.FontWeight = Avalonia.Media.FontWeight.Bold;
            this.lbfinished.IsVisible = false;
            this.lbfinload.Text = "Object was created and loaded.";
            this.lbfinload.FontWeight = Avalonia.Media.FontWeight.Bold;
            this.lbfinload.IsVisible = false;
            this.lberr.Text = "SimPe was unable to create the Object.\nA possible reason could be that you Interrupted the creation process.\n\nAnother reason might be that the selected Object is not a recolour-able Object.\n\nAnother reason might be that the selected Object \"borrows\" it's Textures from a Parent Object. In that case you would need to work with the Parent Object.\n\nAnother reason might be SimPe's file table has not been configured to load some required parts";
            this.lberr.TextWrapping = Avalonia.Media.TextWrapping.Wrap;
            this.lberr.FontWeight = Avalonia.Media.FontWeight.Bold;
            this.lberr.IsVisible = false;
            this.cbgid.Content = "Set Custom Group ID (0x1c050000)";
            this.cbfix.Content = "Fix Cloned Files (by wes_h)";
            this.cbclean.Content = "Rem. useless Files (by Numenor)";
            this.cbRemTxt.Content = "Rem. All Languages from References except US";
            this.cbparent.Content = "Create a stand-alone object";
            this.cbdefault.Content = "Pull only default Colour";
            this.cbwallmask.Content = "Pull Wallmasks (by Numenor)";
            this.cbanim.Content = "Pull Animations";
            this.cbOrgGmdc.Content = "Reference original Mesh";
            this.cbDesc.Content = "Change Description";
            this.cbstrlink.Content = "Pull #STR-Linked Resources";
            this.cbColorExt.Content = "Create Colour Extension Package";
            this.cbColorExt.IsEnabled = false;
            this.cbTask.ItemsSource = new string[] { "Recolour", "Clone" };
            this.cbTask.SelectedIndex = 0;
            this.lb.IsVisible = false;

            // ── Avalonia toolbar (replaces WinForms ToolStrip) ──
            _btnPrev    = new Avalonia.Controls.Button { Content = "Previous",  Margin = new Avalonia.Thickness(0,0,2,0), Background = Avalonia.Media.Brushes.White };
            _btnNext    = new Avalonia.Controls.Button { Content = "Next",      Margin = new Avalonia.Thickness(0,0,2,0), Background = Avalonia.Media.Brushes.White };
            _btnFinish  = new Avalonia.Controls.Button { Content = "Finish",    Margin = new Avalonia.Thickness(0,0,2,0), Background = Avalonia.Media.Brushes.White, IsVisible = false };
            _btnAbort   = new Avalonia.Controls.Button { Content = "Startover", Margin = new Avalonia.Thickness(0,0,2,0), Background = Avalonia.Media.Brushes.White, IsVisible = false };
            _btnCatalog = new Avalonia.Controls.Button { Content = "Catalogue", Margin = new Avalonia.Thickness(0,0,2,0), Background = Avalonia.Media.Brushes.White, IsVisible = false };
            _btnPrev.Click    += (s, e) => this.Activate_biPrev(s, e);
            _btnNext.Click    += (s, e) => this.Activate_biNext(s, e);
            _btnFinish.Click  += (s, e) => this.ActivateFinish(s, e);
            _btnAbort.Click   += (s, e) => this.biAbort_Activate(s, e);
            _btnCatalog.Click += (s, e) => this.Activate_biCatalog(s, e);
            var toolbar = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Margin = new Avalonia.Thickness(4, 2),
            };
            toolbar.Children.Add(_btnPrev);
            toolbar.Children.Add(_btnNext);
            toolbar.Children.Add(_btnFinish);
            toolbar.Children.Add(_btnAbort);
            toolbar.Children.Add(_btnCatalog);

            // ── Step 1: Welcome ──
            var advancedGrid = new Avalonia.Controls.Grid();
            advancedGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition(Avalonia.Controls.GridLength.Star));
            advancedGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition(Avalonia.Controls.GridLength.Auto));
            advancedGrid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            advancedGrid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            advancedGrid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            Avalonia.Controls.Grid.SetRow(tbGroup, 0); Avalonia.Controls.Grid.SetColumn(tbGroup, 0);
            Avalonia.Controls.Grid.SetRow(button4, 0); Avalonia.Controls.Grid.SetColumn(button4, 1);
            Avalonia.Controls.Grid.SetRow(tbGUID, 1);  Avalonia.Controls.Grid.SetColumn(tbGUID, 0);
            Avalonia.Controls.Grid.SetRow(button6, 1); Avalonia.Controls.Grid.SetColumn(button6, 1);
            Avalonia.Controls.Grid.SetRow(tbCresName, 2); Avalonia.Controls.Grid.SetColumn(tbCresName, 0);
            Avalonia.Controls.Grid.SetRow(button5, 2);    Avalonia.Controls.Grid.SetColumn(button5, 1);
            tbGroup.Margin = tbGUID.Margin = tbCresName.Margin = new Avalonia.Thickness(0, 2);
            button4.Margin = button5.Margin = button6.Margin = new Avalonia.Thickness(4, 2, 0, 2);
            button3.Background = button4.Background = button5.Background = button6.Background = Avalonia.Media.Brushes.White;
            advancedGrid.Children.Add(tbGroup);
            advancedGrid.Children.Add(button4);
            advancedGrid.Children.Add(tbGUID);
            advancedGrid.Children.Add(button6);
            advancedGrid.Children.Add(tbCresName);
            advancedGrid.Children.Add(button5);
            var advancedExpander = new Avalonia.Controls.Expander
            {
                Header = "Advanced",
                Content = advancedGrid,
                IsExpanded = UserVerification.HaveUserId,
                Margin = new Avalonia.Thickness(0, 8, 0, 0),
            };
            this.xpAdvanced.Children.Add(advancedExpander);

            // Step 1 uses a Grid so the credits text anchors to the bottom.
            var step1Grid = new Avalonia.Controls.Grid();
            step1Grid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            step1Grid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            step1Grid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            step1Grid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            step1Grid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Star)); // spacer
            step1Grid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto)); // credits
            var step1Desc = new Avalonia.Controls.TextBlock
            {
                Text = label2.Text,
                TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                Margin = new Avalonia.Thickness(0, 8, 0, 8),
            };
            button1.Background = Avalonia.Media.Brushes.White;
            button2.Background = Avalonia.Media.Brushes.White;
            var step1Buttons = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Vertical, Spacing = 4, HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left };
            button1.MinWidth = 80;
            button2.MinWidth = 80;
            step1Buttons.Children.Add(button1);
            step1Buttons.Children.Add(button2);
            label4.FontSize = 10;
            label4.Foreground = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.Parse("#555555"));
            label4.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Bottom;
            Avalonia.Controls.Grid.SetRow(step1Desc, 0);
            Avalonia.Controls.Grid.SetRow(label1, 1);
            Avalonia.Controls.Grid.SetRow(step1Buttons, 2);
            Avalonia.Controls.Grid.SetRow(xpAdvanced, 3);
            Avalonia.Controls.Grid.SetRow(label4, 5);
            step1Grid.Children.Add(step1Desc);
            step1Grid.Children.Add(label1);
            step1Grid.Children.Add(step1Buttons);
            step1Grid.Children.Add(xpAdvanced);
            step1Grid.Children.Add(label4);
            step1Grid.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch;
            step1Grid.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
            step1Grid.Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.Parse("#DCE4EE"));
            step1Grid.Margin = new Avalonia.Thickness(4);
            wizardStepPanel1.Children.Add(step1Grid);

            // ── Step 2: Select Object (tree/list + preview) ──
            _avCatalogTree = new Avalonia.Controls.TreeView
            {
                IsVisible = false, // shown after loading completes
                FontSize = 11,
                Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.Parse("#DCE4EE")),
            };
            _avCatalogTree.SelectionChanged += _avCatalogTree_SelectionChanged;

            var step2Grid = new Avalonia.Controls.Grid();
            step2Grid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Star));
            step2Grid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            step2Grid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(new Avalonia.Controls.GridLength(200)));
            Avalonia.Controls.Grid.SetRow(_avCatalogTree, 0);
            Avalonia.Controls.Grid.SetRow(lb, 0);
            Avalonia.Controls.Grid.SetRow(splitter1, 1);
            Avalonia.Controls.Grid.SetRow(panel1, 2);
            step2Grid.Children.Add(_avCatalogTree);
            step2Grid.Children.Add(lb);
            step2Grid.Children.Add(splitter1);
            // xpTaskBoxSimple2 → "Selected Object" preview area
            var selectedObjHeader = new Avalonia.Controls.Border
            {
                Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.Parse("#CBD3E4")),
                Padding = new Avalonia.Thickness(6, 3),
                Child = new Avalonia.Controls.TextBlock
                {
                    Text = "Selected Object",
                    FontWeight = Avalonia.Media.FontWeight.Bold,
                    FontSize = 12,
                },
            };
            xpTaskBoxSimple2.Children.Add(selectedObjHeader);
            xpTaskBoxSimple2.Children.Add(op1);
            panel1.Children.Add(xpTaskBoxSimple2);
            step2Grid.Children.Add(panel1);
            wizardStepPanel2.Children.Add(step2Grid);

            // ── Step 3: Clone/Recolor Settings ──
            // Task row: "Task:" label + dropdown (full width) + Start button
            label3.Margin = new Avalonia.Thickness(0, 4, 0, 2);
            cbTask.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
            cbTask.Margin = new Avalonia.Thickness(0, 2, 0, 4);
            button3.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right;
            button3.Background = Avalonia.Media.Brushes.White;
            panel2.Children.Add(label3);
            panel2.Children.Add(cbTask);
            panel2.Children.Add(button3);
            panel2.Margin = new Avalonia.Thickness(0, 0, 0, 4);

            // Clone options
            gbClone.Children.Add(cbgid);
            gbClone.Children.Add(cbfix);
            gbClone.Children.Add(cbclean);
            gbClone.Children.Add(cbRemTxt);
            gbClone.Children.Add(cbparent);
            gbClone.Children.Add(cbdefault);
            gbClone.Children.Add(cbwallmask);
            gbClone.Children.Add(cbanim);
            gbClone.Children.Add(cbstrlink);
            gbClone.Children.Add(cbOrgGmdc);
            gbClone.Children.Add(cbDesc);
            gbClone.IsVisible = false;
            gbClone.Margin = new Avalonia.Thickness(4, 2, 4, 4);

            // Recolor options
            gbRecolor.Children.Add(cbColorExt);
            gbRecolor.IsVisible = true;
            gbRecolor.Margin = new Avalonia.Thickness(4, 2, 4, 4);

            // Section headers with gradient-style background (matching original)
            var sectionBg = Avalonia.Media.Color.Parse("#CBD3E4");
            var cloneHeader = new Avalonia.Controls.Border
            {
                Background = new Avalonia.Media.SolidColorBrush(sectionBg),
                Padding = new Avalonia.Thickness(6, 3),
                Margin = new Avalonia.Thickness(0, 4, 0, 0),
                Child = new Avalonia.Controls.TextBlock { Text = "Clone", FontWeight = Avalonia.Media.FontWeight.Bold, FontSize = 12 },
            };
            var recolorHeader = new Avalonia.Controls.Border
            {
                Background = new Avalonia.Media.SolidColorBrush(sectionBg),
                Padding = new Avalonia.Thickness(6, 3),
                Margin = new Avalonia.Thickness(0, 4, 0, 0),
                Child = new Avalonia.Controls.TextBlock { Text = "Recolour", FontWeight = Avalonia.Media.FontWeight.Bold, FontSize = 12 },
            };

            // Selected Object preview for step 3 (uses op2)
            var step3ObjHeader = new Avalonia.Controls.Border
            {
                Background = new Avalonia.Media.SolidColorBrush(sectionBg),
                Padding = new Avalonia.Thickness(6, 3),
                Margin = new Avalonia.Thickness(0, 4, 0, 0),
                Child = new Avalonia.Controls.TextBlock { Text = "Selected Object", FontWeight = Avalonia.Media.FontWeight.Bold, FontSize = 12 },
            };
            xpTaskBoxSimple1.Children.Add(step3ObjHeader);
            xpTaskBoxSimple1.Children.Add(op2);

            // Assemble step 3 in a ScrollViewer so it doesn't clip
            var step3Stack = new Avalonia.Controls.StackPanel();
            step3Stack.Children.Add(panel2);
            step3Stack.Children.Add(cloneHeader);
            step3Stack.Children.Add(gbClone);
            step3Stack.Children.Add(recolorHeader);
            step3Stack.Children.Add(gbRecolor);
            step3Stack.Children.Add(xpTaskBoxSimple1);
            var step3Scroll = new Avalonia.Controls.ScrollViewer
            {
                Content = step3Stack,
                VerticalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Auto,
            };
            wizardStepPanel3.Children.Add(step3Scroll);

            // ── Step 5: Description Edit ──
            var descGrid = new Avalonia.Controls.Grid();
            descGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition(Avalonia.Controls.GridLength.Auto));
            descGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition(Avalonia.Controls.GridLength.Star));
            descGrid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            descGrid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            descGrid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Star));
            Avalonia.Controls.Grid.SetRow(label5, 0); Avalonia.Controls.Grid.SetColumn(label5, 0);
            Avalonia.Controls.Grid.SetRow(tbName, 0);  Avalonia.Controls.Grid.SetColumn(tbName, 1);
            Avalonia.Controls.Grid.SetRow(label7, 1); Avalonia.Controls.Grid.SetColumn(label7, 0);
            Avalonia.Controls.Grid.SetRow(tbPrice, 1); Avalonia.Controls.Grid.SetColumn(tbPrice, 1);
            Avalonia.Controls.Grid.SetRow(label6, 2); Avalonia.Controls.Grid.SetColumn(label6, 0);
            Avalonia.Controls.Grid.SetRow(tbDesc, 2); Avalonia.Controls.Grid.SetColumn(tbDesc, 1);
            label5.Margin = label6.Margin = label7.Margin = new Avalonia.Thickness(0, 4, 8, 4);
            label5.VerticalAlignment = label7.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            tbName.Margin = tbPrice.Margin = new Avalonia.Thickness(0, 2);
            tbDesc.Margin = new Avalonia.Thickness(0, 2);
            tbDesc.AcceptsReturn = true;
            tbDesc.TextWrapping = Avalonia.Media.TextWrapping.Wrap;
            descGrid.Children.Add(label5);
            descGrid.Children.Add(tbName);
            descGrid.Children.Add(label7);
            descGrid.Children.Add(tbPrice);
            descGrid.Children.Add(label6);
            descGrid.Children.Add(tbDesc);
            xpTaskBoxSimple3.Children.Add(descGrid);
            wizardStepPanel5.Children.Add(xpTaskBoxSimple3);

            // ── Step 4: Wait / Progress ──
            pnWait.Margin = new Avalonia.Thickness(8);
            pnWait.Children.Add(lbwait);
            pnWait.Children.Add(lbfinload);
            pnWait.Children.Add(lberr);
            pnWait.Children.Add(lbfinished);
            wizardStepPanel4.Children.Add(pnWait);

            // ── Assemble wizard (add steps in order) ──
            wizard1.Children.Add(wizardStepPanel1);
            wizard1.Children.Add(wizardStepPanel2);
            wizard1.Children.Add(wizardStepPanel3);
            wizard1.Children.Add(wizardStepPanel5);
            wizard1.Children.Add(wizardStepPanel4);

            // ── Assemble outer container ──
            xpGradientPanel1.Background = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.Parse("#C8D0DA"));
            xpGradientPanel1.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Auto));
            xpGradientPanel1.RowDefinitions.Add(new Avalonia.Controls.RowDefinition(Avalonia.Controls.GridLength.Star));
            Avalonia.Controls.Grid.SetRow(toolbar, 0);
            Avalonia.Controls.Grid.SetRow(wizard1, 1);
            wizard1.Margin = new Avalonia.Thickness(8);
            xpGradientPanel1.Children.Add(toolbar);
            xpGradientPanel1.Children.Add(wizard1);

            // Properties formerly set by the WinForms designer via .resx
            this.AvaloniaContent = this.xpGradientPanel1;
            this.TabText = "Object Workshop";
            this.TabIconBitmap = SimPe.LoadIcon.LoadAvaloniaBitmap("OWDockForm_dcObjectWorkshop.TabImage.png");
		}
		#endregion

		private void wizard1_ChangedFinishState(SimPe.Wizards.Wizard sender)
		{
			biFinish.Visible = sender.FinishEnabled;
			if (_btnFinish != null) _btnFinish.IsVisible = sender.FinishEnabled;
		}

		private void wizard1_ChangedNextState(SimPe.Wizards.Wizard sender)
		{
			biNext.Enabled = sender.NextEnabled;
			if (_btnNext != null) _btnNext.IsEnabled = sender.NextEnabled;
		}

		private void wizard1_ChangedPrevState(SimPe.Wizards.Wizard sender)
		{
			biPrev.Enabled = sender.PrevEnabled;
			this.biAbort.Visible = biPrev.Enabled;
			if (_btnPrev != null) _btnPrev.IsEnabled = biPrev.Enabled;
			if (_btnAbort != null) _btnAbort.IsVisible = biPrev.Enabled;
		}

		private void Activate_biPrev(object sender, System.EventArgs e)
		{
			wizard1.GoPrev();
		}

		private void Activate_biNext(object sender, System.EventArgs e)
		{
			wizard1.GoNext();
		}

		private void ActivateFinish(object sender, System.EventArgs e)
		{
			if (wizard1.CurrentStep == this.wizardStepPanel3 || wizard1.CurrentStep == this.wizardStepPanel5) Activate_biNext(sender, e);
			else wizard1.Finish();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			onlybase = false;
			Activate_biNext(biNext, e);
		}

        delegate void TreeViewSetUpdateHandler(TreeView tv, bool begin);

        protected TreeNode RootNode;
		private void wizardStepPanel2_Prepare(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardStepPanel step, int target)
		{
			if (target==step.Index) 
			{
				onlybase = false;
				if (lb.Items.Count==0 && tv.Nodes.Count==0) 
				{
                    if (RootNode == null) RootNode = new SimPe.Scenegraph.Compat.TreeNode();
					tv.IsEnabled = false;
					lb.IsEnabled = false;
					lastselected = null;
					this.ilist.Images.Clear();
					this.ilist.Images.Add(SKBitmap.Decode(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.Tool.Dockable.subitems.png")));
					this.ilist.Images.Add(SKBitmap.Decode(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.Tool.Dockable.nothumb.png")));
					this.ilist.Images.Add(SKBitmap.Decode(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.Tool.Dockable.custom.png")));

					lb.Items.Clear();
                    RootNode.Nodes.Clear();
					tv.Nodes.Clear();
					tv.Sorted = true;
					tv.ImageList = ilist;
                    tv.DoBeginUpdate();
				
					ObjectLoader ol = new ObjectLoader(null);
					ol.LoadedItem += new SimPe.Plugin.Tool.Dockable.ObjectLoader.LoadItemHandler(ol_LoadedItem);
					ol.Finished += new EventHandler(ol_Finished);
					ol.LoadData();				
				}
			}
		}

		delegate SimPe.Scenegraph.Compat.TreeNode GetParentNodeHandler(TreeNodeCollection nodes, string[] names, int id, SimPe.Cache.ObjectCacheItem oci, SimPe.Data.Alias a, ImageList ilist);
		

		private void ol_LoadedItem(SimPe.Cache.ObjectCacheItem oci, SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem fii, SimPe.Data.Alias a)
		{
			if (a==null) return;

            //if (oci.Class == SimPe.Cache.ObjectClass.XObject && !Helper.XmlRegistry.OWincludewalls) return;

			string[][] cats = oci.ObjectCategory;			
			foreach (string[] ss in cats)				
			{			
				ObjectLoader.GetParentNode(RootNode.Nodes, ss, 0, oci, a, ilist);
			}

            AddItemToListBox( a , EventArgs.Empty);			
		}

        private void AddItemToListBox(object obj, EventArgs e)
        {
            lb.Items.Add(obj);
        }

        private void ol_Finished(object sender, EventArgs e)
        {
            Avalonia.Threading.Dispatcher.UIThread.Post(() => invoke_ol_Finished(sender, e));
        }

        private void invoke_ol_Finished(object sender, EventArgs e)
		{
			tv.IsEnabled = true;

            Wait.SubStart(RootNode.Nodes.Count);
            Wait.Message = "Building Object List";
            int ct = 0;
            foreach (TreeNode node in RootNode.Nodes)
            {
                Wait.Progress = ct++;
                tv.Nodes.Add(node);
            }

            tv.EndUpdate();

            // Populate the Avalonia TreeView from the compat tree data
            Wait.Message = "Building Catalog View";
            Wait.MaxProgress = tv.Nodes.Count;
            PopulateAvaloniaTree();

            Wait.SubStop();

			lb.IsEnabled = true;
            tv.DoEndUpdate((this.biCatalog.Checked == true));
		}

        /// <summary>
        /// Converts compat TreeNodes into Avalonia TreeViewItems and populates _avCatalogTree.
        /// </summary>
        private void PopulateAvaloniaTree()
        {
            var items = new System.Collections.Generic.List<Avalonia.Controls.TreeViewItem>();
            int ct = 0;
            foreach (TreeNode node in tv.Nodes)
            {
                Wait.Progress = ct++;
                items.Add(BuildTreeViewItem(node));
            }

            _avCatalogTree.ItemsSource = items;
            _avCatalogTree.IsVisible = (this.biCatalog.Checked == true);
            lb.IsVisible = !(this.biCatalog.Checked == true);
        }

        private Avalonia.Controls.TreeViewItem BuildTreeViewItem(TreeNode node)
        {
            var tvi = new Avalonia.Controls.TreeViewItem
            {
                Header = node.Text ?? "",
                Tag = node,
            };
            foreach (TreeNode child in node.Nodes)
                tvi.Items.Add(BuildTreeViewItem(child));
            return tvi;
        }

        private void _avCatalogTree_SelectionChanged(object sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            if (_avCatalogTree.SelectedItem is Avalonia.Controls.TreeViewItem tvi
                && tvi.Tag is TreeNode compatNode)
            {
                tv.SelectedNode = compatNode;
                tv_AfterSelect(tv, new SimPe.Scenegraph.Compat.TreeViewEventArgs(compatNode));
            }
        }

		private void Activate_biCatalog(object sender, System.EventArgs e)
		{
			this.tv.IsVisible = (this.biCatalog.Checked == true);
			this._avCatalogTree.IsVisible = (this.biCatalog.Checked == true);
			this.lb.IsVisible = !(this.biCatalog.Checked == true);
			
			lb_SelectedIndexChanged(lb, null);
			tv_AfterSelect(tv, null);
		}

		private void wizard1_ShowStep(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardEventArgs e)
		{
            this.biCatalog.IsVisible = (e.Step.Index == wizardStepPanel2.Index);
            if (_btnCatalog != null) _btnCatalog.IsVisible = biCatalog.IsVisible;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			onlybase = false;
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = ExtensionProvider.BuildFilterString(
				new SimPe.ExtensionType[] {
											  SimPe.ExtensionType.Package,
											  SimPe.ExtensionType.DisabledPackage,
											  SimPe.ExtensionType.AllFiles
										  }
				);

			package = null;
			if (ofd.ShowDialog()==System.Windows.Forms.DialogResult.OK) 
			{
				package = SimPe.Packages.GeneratableFile.LoadFromFile(ofd.FileName);
				wizard1.JumpToStep(2);
			}
		}

		SimPe.Packages.GeneratableFile package;
		Data.Alias lastselected;
		private void tv_AfterSelect(object sender, SimPe.Scenegraph.Compat.TreeViewEventArgs e)
		{
			if (wizard1.CurrentStepNumber==this.wizardStepPanel2.Index && (tv.Visible || _avCatalogTree.IsVisible))
			{
				if (tv.SelectedNode==null) wizard1.NextEnabled = false;
				else wizard1.NextEnabled = tv.SelectedNode.Tag!=null;
			}

			if (wizard1.NextEnabled) 
			{
				lastselected = (Data.Alias)tv.SelectedNode.Tag;
			} 
			else lastselected = null;
					
			UpdateObjectPreview(op1);
		}

		private void lb_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (wizard1.CurrentStepNumber==this.wizardStepPanel2.Index && lb.IsVisible) 
			{
				wizard1.NextEnabled = (lb.SelectedIndex>=0);
			}	
		
			if (wizard1.NextEnabled) 
			{
				lastselected = (Data.Alias)lb.SelectedItem;
			} 
			else lastselected = null;
					
			UpdateObjectPreview(op1);
		}

		private void cbTask_SelectedIndexChanged(object sender, System.EventArgs e)
		{			
			if (cbTask.SelectedIndex==1) 
			{
				gbRecolor.IsVisible = false;
				gbClone.IsVisible = true;				
			} 
			else 
			{
				gbRecolor.IsVisible = true;
				gbClone.IsVisible = false;				
			}

			if (cbTask.SelectedIndex==1 && (this.cbDesc.IsChecked == true)) 
			{
				wizard1.FinishEnabled = false;
				wizard1.NextEnabled = (lastselected!=null || package!=null) ;
			} 
			else 
			{
				wizard1.FinishEnabled = (lastselected!=null || package!=null);
				wizard1.NextEnabled = false;
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			Activate_biNext(biNext, e);
		}

		private void wizardStepPanel2_Activate(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardEventArgs e)		
		{
			

			package = null;
			if (tv.Visible) 
			{
				if (tv.SelectedNode==null) e.EnableNext = false;
				else if (tv.SelectedNode.Tag==null) e.EnableNext = false;
				else e.EnableNext = true;
			} 
			else 
			{
				e.EnableNext = lb.SelectedIndex>=0;
			}

			tv.SelectedNode = null;
			lb.SelectedIndex = -1;
		}		
		

		private void wizardStepPanel4_Activate(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardEventArgs e)
		{
			e.CanFinish = false;
            this.pbWait.Image = null;
			this.lbwait.IsVisible = true;
			this.lbfinished.IsVisible = false;
			this.lbfinload.IsVisible = false;
			this.lberr.IsVisible = false;
		}

        private async void wizardStepPanel4_Activated(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardStepPanel step)
        {
            SimPe.Packages.GeneratableFile package = null;
			if (lastselected ==null && this.package==null)
			{
				sender.FinishEnabled = false;
			}
			else
			{
				SimPe.Interfaces.IAlias a;
				Interfaces.Files.IPackedFileDescriptor pfd;
				uint localgroup;
				ObjectWorkshopHelper.PrepareForClone(this.package, this.lastselected, out a, out localgroup, out pfd);

				ObjectWorkshopSettings settings;

				//Clone an Object
                if (this.cbTask.SelectedIndex == 1)
                {
                    OWCloneSettings cs = new OWCloneSettings();
                    cs.IncludeWallmask = (this.cbwallmask.IsChecked == true);
                    cs.OnlyDefaultMmats = (this.cbdefault.IsChecked == true);
                    cs.IncludeAnimationResources = (this.cbanim.IsChecked == true);
                    cs.CustomGroup = (this.cbgid.IsChecked == true);
                    cs.FixResources = (this.cbfix.IsChecked == true);
                    cs.RemoveUselessResource = (this.cbclean.IsChecked == true);
                    cs.StandAloneObject = (this.cbparent.IsChecked == true);
                    cs.RemoveNonDefaultTextReferences = (this.cbRemTxt.IsChecked == true);
                    cs.KeepOriginalMesh = (this.cbOrgGmdc.IsChecked == true);
                    cs.PullResourcesByStr = (this.cbstrlink.IsChecked == true);

                    cs.ChangeObjectDescription = (this.cbDesc.IsChecked == true);
                    cs.Title = this.tbName.Text;
                    cs.Description = this.tbDesc.Text;
                    short price;
                    if (!short.TryParse(this.tbPrice.Text, out price))
                        price = 0;
                    cs.Price = price;

                    settings = cs;
                }
                else
                {
                    //Recolor a Object
                    settings = new OWRecolorSettings();
                    settings.RemoveNonDefaultTextReferences = false;
                }

                // Show save dialog BEFORE entering sync code (Avalonia can't nest event loops)
                var mainWindow = (Avalonia.Application.Current?.ApplicationLifetime
                    as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime)?.MainWindow;
                if (mainWindow != null)
                {
                    var result = await mainWindow.StorageProvider.SaveFilePickerAsync(
                        new Avalonia.Platform.Storage.FilePickerSaveOptions
                        {
                            Title = "Save As",
                            FileTypeChoices = FileDialogHelper.ParseFilter(
                                ExtensionProvider.BuildFilterString(
                                    new SimPe.ExtensionType[] {
                                        SimPe.ExtensionType.Package,
                                        SimPe.ExtensionType.AllFiles
                                    })),
                        });
                    if (result != null)
                        SaveFileDialog.PresetFileName = result.Path.LocalPath;
                    else
                    {
                        // User cancelled — don't proceed
                        this.lbwait.IsVisible = false;
                        this.lberr.IsVisible = true;
                        this.lbfinished.IsVisible = false;
                        return;
                    }
                }

				try
				{
                    var result = await ObjectWorkshopHelper.StartAsync(this.package, a, pfd, localgroup, settings, onlybase);
                    package = result.Package;
                    pfd = result.Pfd;
                }
				catch (Exception ex)
				{
					Helper.ExceptionMessage(ex);
				}
				if (package!=null) this.lbfinload.IsVisible = settings.RemoteResult;
				else this.lberr.IsVisible = true;

			}

			this.lbwait.IsVisible = false;
			this.lbfinished.IsVisible = !this.lbfinload.IsVisible && !lberr.IsVisible;
		}

		private void wizardStepPanel3_Activate(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardEventArgs e)
		{			
			e.CanFinish = ((lastselected!=null || package!=null) && this.cbTask.SelectedIndex==0 && !(this.cbDesc.IsChecked == true));
			e.EnableNext = ((lastselected!=null || package!=null) && !(this.cbTask.SelectedIndex==0 && !(this.cbDesc.IsChecked == true)));		
			UpdateObjectPreview(op2);
			UpdateEnabledOptions();
		}

		void UpdateEnabledOptions()
		{
			if (lastselected!=null) 
			{
				SimPe.Cache.ObjectCacheItem oci = (SimPe.Cache.ObjectCacheItem)lastselected.Tag[3];
				if (oci.Class != SimPe.Cache.ObjectClass.Object) 
				{
					cbclean.IsEnabled = false;
					cbdefault.IsEnabled = false;
					cbparent.IsEnabled = false;
					
					cbTask.SelectedIndex = 1;
#if DEBUG
#else
					cbTask.IsEnabled = false;
#endif
				} 
				else 
				{
					cbclean.IsEnabled = true && (this.cbfix.IsChecked == true);
					cbdefault.IsEnabled = true;
					cbparent.IsEnabled = true;
					cbTask.IsEnabled = true;
				}
			}
		}
		void UpdateObjectPreview(ObjectPreview op)
		{
			if (lastselected!=null) op.SetFromObjectCacheItem((SimPe.Cache.ObjectCacheItem)lastselected.Tag[3]);			
			else if (package!=null)	op.SetFromPackage(package);
			else op.SelectedObject = null;
		}

		private void biAbort_Activate(object sender, System.EventArgs e)
		{
			wizard1.JumpToStep(0);
		}

		private void cbfix_CheckedChanged(object sender, System.EventArgs e)
		{			
			cbclean.IsEnabled = (this.cbfix.IsChecked == true);
			cbRemTxt.IsEnabled = (this.cbfix.IsChecked == true);
			UpdateEnabledOptions();	
		}

		private void wizardStepPanel3_Activated(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardStepPanel step)
		{
			
		}

		private void wizardStepPanel5_Activate(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardEventArgs e)
		{
			e.CanFinish = true;
			e.EnableNext = false;

			this.tbName.Text = this.op2.Title;
			this.tbDesc.Text = this.op2.Description;
			this.tbPrice.Text = this.op2.Price.ToString();
		}

		private void wizardStepPanel5_Activated(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardStepPanel step)
		{
			
			
		}

		private void wizard1_PrepareStep(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardStepPanel step, int target)
		{			
		}

		private void wizard1_ShowedStep(SimPe.Wizards.Wizard sender, int source)
		{

			if (sender.CurrentStep == this.wizardStepPanel5 && (this.cbTask.SelectedIndex==0 || (this.cbDesc.IsChecked == true)==false)) 
			{
				if (source<sender.CurrentStep.Index) wizard1.GoNext();
				else wizard1.GoPrev();
			}
		}

		private void cbDesc_CheckedChanged(object sender, System.EventArgs e)
		{
			cbTask_SelectedIndexChanged(this.cbTask, null);
		}

		private void lberr_Click(object sender, System.EventArgs e)
		{
		
		}

		bool onlybase;
		private void button4_Click(object sender, System.EventArgs e)
		{
			lastselected = null;
			this.tv.SelectedNode = null;
			onlybase = false;			
			package = ObjectWorkshopHelper.CreatCloneByGroup(Helper.StringToUInt32(tbGroup.Text, 0x7f000000, 16));

			wizard1.JumpToStep(2);
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			lastselected = null;
			this.tv.SelectedNode = null;
			onlybase = false;
			package = ObjectWorkshopHelper.CreatCloneByCres(this.tbCresName.Text);
			
			wizard1.JumpToStep(2);
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			lastselected = null;
			this.tv.SelectedNode = null;			
			onlybase = false;
			package = ObjectWorkshopHelper.CreatCloneByGuid(Helper.StringToUInt32(this.tbGUID.Text, 0x00000000, 16));

			wizard1.JumpToStep(2);
		}

		private void xpTaskBoxSimple1_Resize(object sender, EventArgs e)
		{
		}

		private void xpTaskBoxSimple2_Resize(object sender, EventArgs e)
		{
		}

        private void SetDefaultsForClone(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registry.SetDefaults();
        }
	}
}
