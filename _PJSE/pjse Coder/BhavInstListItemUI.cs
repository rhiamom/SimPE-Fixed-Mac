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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using Avalonia.Controls;
using Avalonia.Input;
using SimPe.Scenegraph.Compat;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for BhavInstListItemUI.
	/// </summary>
	public class BhavInstListItemUI : Avalonia.Controls.UserControl
    {
        // WinForms layout compat (no-ops in Avalonia)
        public int Left { get; set; }
        public int Top { get; set; }
        public new int Width { get; set; }
        public new int Height { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        #region Control variables
        private LabelCompat instrText;
		private LinkLabel trueTarget;
		private LinkLabel falseTarget;
        private TextBoxCompat bhavInstListItem;
        private Avalonia.Controls.Border rowBorder;

        // 0.75 highlighted the selected row with a light gray; pick a soft gray that
        // reads as "selected" without fighting the rest of the editor chrome.
        private static readonly Avalonia.Media.IBrush SelectedBrush   = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromRgb(0xD8, 0xD8, 0xD8));
        private static readonly Avalonia.Media.IBrush UnselectedBrush = Avalonia.Media.Brushes.Transparent;
        #endregion

        public BhavInstListItemUI()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			this.Height = rowHeight;
			MakeUnselected();
			pjse.FileTable.GFT.FiletableRefresh += new EventHandler(FiletableRefresh);

            if (strTrue == null) strTrue = this.trueTarget.Content?.ToString() ?? "true";
            if (strFalse == null) strFalse = this.falseTarget.Content?.ToString() ?? "false";
            if (SimPe.Helper.XmlRegistry.UseBigIcons && 1920 > 1600)
            {
                // Location not applicable in Avalonia
            }
        }

		public void Dispose()
		{
			pjse.FileTable.GFT.FiletableRefresh -= new EventHandler(FiletableRefresh);
			Index = -1;
			Wrapper = null;
		}


		#region BhavInstListItemUI
		public const int rowHeight = 32;
		public event EventHandler Selected;
		public event EventHandler Unselected;
		public event LinkLabelLinkClickedEventHandler TargetClick;
		public event EventHandler MoveUp;
		public event EventHandler MoveDown;
		protected virtual void OnSelected(EventArgs e) { if (Selected != null) { Selected(this, e); } }
		protected virtual void OnUnselected(EventArgs e) { if (Unselected != null) { Unselected(this, e); } }
		protected virtual void OnTargetClick(LinkLabelLinkClickedEventArgs e) { if (TargetClick != null) { TargetClick(this, e); } }
		protected virtual void OnMoveUp(EventArgs e) { if (MoveUp != null) { MoveUp(this, e); } }
		protected virtual void OnMoveDown(EventArgs e) { if (MoveDown != null) { MoveDown(this, e); } }


		private Bhav wrapper = null;
		private int index = -1;

        private static String strTrue  = null;
        private static String strFalse = null;

		public Bhav Wrapper
		{
			set
			{
				if (wrapper != value)
				{
					if (wrapper != null)
						wrapper.WrapperChanged -= new EventHandler(WrapperChanged);
					wrapper = value;
					if (wrapper != null)
					{
						if (index != -1)
							this.WrapperChanged(wrapper[index], null);
						wrapper.WrapperChanged += new EventHandler(WrapperChanged);
					}
				}
			}
		}

		public int Index
		{
			set
			{
				if (index != value)
				{
					index = value;
					if (wrapper != null && index != -1)
						this.WrapperChanged(wrapper[index], null);
				}
			}
            get
            {
                return index;
            }
		}

        public void SetComment(string tip)
        {
            // ToolTip not available in Avalonia port
        }

		public void MakeSelected()
        {
            if (rowBorder != null) rowBorder.Background = SelectedBrush;
		}

		public void MakeUnselected()
		{
            if (rowBorder != null) rowBorder.Background = UnselectedBrush;
		}

        private static string fmt = "0x{0} ({1}): {2}";
        private static new string Content(int index, pjse.BhavWiz inst)
        {
            return Format(fmt, index.ToString("X"), index.ToString(), cleanup(inst.ShortName));
        }
        private static string Format(string res, params string[] args)
        {
            for (int i = 0; i < args.Length; i++)
                res = res.Replace("{" + i.ToString() + "}", args[i]);
            return res;
        }
        private static string cleanup(string str)
        {
            for (char c = System.Convert.ToChar(1); c < ' '; c++) str = str.Replace(c, ' ');
            return str;
        }

        private void WrapperChanged(object sender, System.EventArgs e)
		{
			if (wrapper == null || index == -1) return;

			if (!(sender is Instruction) || wrapper.IndexOf((Instruction)sender) != index) return;
			Instruction inst = (Instruction)sender;

			bhavInstListItem.Text = "";
			instrText.Content = Content(index, inst);//LongName;

			trueTarget.Content = strTrue + ": "+inst.Target1.ToString("X");
			if (inst.Target1 < wrapper.Count)
				trueTarget.Tag = inst.Target1;

            falseTarget.Content = strFalse + ": " + inst.Target2.ToString("X");
			if (inst.Target2 < wrapper.Count)
				falseTarget.Tag = inst.Target2;
		}

		private void FiletableRefresh(object sender, System.EventArgs e)
		{
			if (wrapper == null || index == -1) return;
            instrText.Content = Content(index, wrapper[index]);//LongName;
        }
		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.instrText = new LabelCompat();
            this.trueTarget = new LinkLabel();
            this.falseTarget = new LinkLabel();
            this.bhavInstListItem = new TextBoxCompat();

            this.instrText.Name = "instrText";
            this.instrText.Content = "instrText";
            this.instrText.Click += (s, e) => Control_Click(s, e);

            this.trueTarget.Name = "trueTarget";
            this.trueTarget.Content = "true";
            this.trueTarget.LinkClicked += (s, e) => Target_LinkClicked(s, e);
            this.trueTarget.Click += (s, e) => Control_Click(s, e);

            this.falseTarget.Name = "falseTarget";
            this.falseTarget.Content = "false";
            this.falseTarget.LinkClicked += (s, e) => Target_LinkClicked(s, e);
            this.falseTarget.Click += (s, e) => Control_Click(s, e);

            this.bhavInstListItem.Name = "bhavInstListItem";
            this.bhavInstListItem.Text = "bhavInstListItem";
            this.bhavInstListItem.KeyDown += (s, e) => bhavInstListItem_KeyDown(s, e);

            this.GotFocus += (s, e) => bhavInstListItemUI_Enter(s, e);
            this.LostFocus += (s, e) => bhavInstListItemUI_Leave(s, e);

            // ── Compose the visible row layout and attach as Content.
            //    Without this the UserControl is empty and nothing renders. ──
            this.instrText.VerticalAlignment   = Avalonia.Layout.VerticalAlignment.Center;
            this.trueTarget.VerticalAlignment  = Avalonia.Layout.VerticalAlignment.Center;
            this.falseTarget.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            this.trueTarget.Margin  = new Avalonia.Thickness(8, 0, 4, 0);
            this.falseTarget.Margin = new Avalonia.Thickness(0, 0, 8, 0);

            var targetsPanel = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            };
            targetsPanel.Children.Add(this.trueTarget);
            targetsPanel.Children.Add(this.falseTarget);

            var rowContent = new Avalonia.Controls.DockPanel
            {
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                Margin = new Avalonia.Thickness(4, 0),
            };
            Avalonia.Controls.DockPanel.SetDock(targetsPanel, Avalonia.Controls.Dock.Right);
            rowContent.Children.Add(targetsPanel);
            rowContent.Children.Add(this.instrText); // fills remaining space

            this.rowBorder = new Avalonia.Controls.Border
            {
                BorderBrush = Avalonia.Media.Brushes.LightGray,
                BorderThickness = new Avalonia.Thickness(1),
                Padding = new Avalonia.Thickness(2),
                Background = UnselectedBrush,
                Child = rowContent,
            };
            // 'this.Content' resolves to the local static method Content(int, BhavWiz)
            // because of the `new` shadow below. Use base.Content to reach Avalonia's property.
            base.Content = this.rowBorder;
            this.Focusable = true;

            // Focus on any click anywhere on the row — without this, only clicks that
            // land directly on instrText or the link labels (which have their own Click
            // handlers calling this.Focus()) select the row, so most of the row surface
            // is dead to mouse input and selection feels unresponsive.
            this.AddHandler(Avalonia.Input.InputElement.PointerPressedEvent, (s, e) => this.Focus(),
                Avalonia.Interactivity.RoutingStrategies.Tunnel | Avalonia.Interactivity.RoutingStrategies.Bubble);
		}
		#endregion

		private void bhavInstListItemUI_Enter(object sender, System.EventArgs e)
		{
			OnSelected(e);
		}

		private void bhavInstListItemUI_Leave(object sender, System.EventArgs e)
        {
			OnUnselected(e);
		}

		private void bhavInstListItem_KeyDown(object sender, Avalonia.Input.KeyEventArgs e)
		{
			this.OnKeyDown(e);
		}

		private void Control_Click(object sender, System.EventArgs e)
		{
			this.Focus();
		}

		private void Target_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OnTargetClick(e);
		}

		private void moveUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OnMoveUp(e);
		}

		private void moveDown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OnMoveDown(e);
		}

	}
}
