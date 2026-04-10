/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   pljones@users.sf.net                                                  *
 *                                                                         *
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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using SimPe.Scenegraph.Compat;
using MessageBoxButtons = SimPe.Scenegraph.Compat.MessageBoxButtons;
using MessageBoxIcon = SimPe.Scenegraph.Compat.MessageBoxIcon;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for BconForm.
	/// </summary>
	public class BconForm : UserControl, IPackedFileUI
	{
		#region Form variables
		private TextBoxCompat tbFilename;
		private StackPanel bconPanel;
		private ButtonCompat btnCommit;
        private ListView lvConstants;
		private LabelCompat label5;
		private LabelCompat label6;
		private TextBoxCompat tbValueHex;
		private TextBoxCompat tbValueDec;
		private ColumnHeader chID;
		private ColumnHeader chValue;
		private ColumnHeader chLabel;
		private ButtonCompat btnStrDelete;
		private ButtonCompat btnStrAdd;
		private LabelCompat lbFilename;
        private GroupBox gbValue;
		private CheckBoxCompat cbFlag;
		private ButtonCompat btnStrPrev;
        private ButtonCompat btnStrNext;
        private ButtonCompat btnTRCNMaker;
        private ButtonCompat btnCancel;
        private pjse.pjse_banner pjse_banner1;
        private ButtonCompat btnUpdateBCON;
        private LinkLabel llIsOverride;
        private pjse.CompareButton cmpBCON;
        private ButtonCompat btnClose;
		#endregion

		public BconForm()
		{
			InitializeComponent();
            pjse.FileTable.GFT.FiletableRefresh += (s, e) => this.FiletableRefresh(s, e);
        }

        public void Dispose()
        {
            if (setHandler && wrapper != null)
            {
                wrapper.WrapperChanged -= new System.EventHandler(this.WrapperChanged);
                setHandler = false;
            }
            wrapper = null;
            trcnres = null;
        }

		#region Controller
		private Bcon wrapper = null;
        private Trcn trcnres = null;
		private bool setHandler = false;
		private bool internalchg = false;

		private int index = -1;
		private short origItem = -1;
		private short currentItem = -1;

		private bool hex16_IsValid(object sender)
		{
			try { Convert.ToUInt16(((TextBoxCompat)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool dec16_IsValid(object sender)
		{
			try { Convert.ToInt16(((TextBoxCompat)sender).Text, 10); }
			catch (Exception) { return false; }
			return true;
		}


		private void UpdateBconItem_Value(short val, bool doHex, bool doDec)
		{
			internalchg = true;
			wrapper[index] = currentItem = val;
			lvConstants.SelectedItems[0].SubItems[1].Text = "0x" + SimPe.Helper.HexString(currentItem);
			if (doHex)
				tbValueHex.Text = lvConstants.SelectedItems[0].SubItems[1].Text;
			if (doDec)
				tbValueDec.Text = currentItem.ToString();
			internalchg = false;
		}

		private ListViewItem lvItem(int i)
		{
			string cID = "0x" + i.ToString("X") + " (" + i + ")";
			string cValue = "0x" + SimPe.Helper.HexString(wrapper[i]);
            string cLabel = (trcnres != null && !trcnres.TextOnly && i < trcnres.Count) ? trcnres[i].ConstName : "";
			string[] v = { cID, cValue, cLabel };
			return new ListViewItem(v);
		}

		private void updateLists()
		{
			index = -1;
            trcnres = (Trcn)(wrapper == null ? null : wrapper.SiblingResource(Trcn.Trcntype));

			this.lvConstants.Items.Clear();
			int nItems = wrapper == null ? 0 : wrapper.Count;
			for(int i = 0; i < nItems; i++)
				this.lvConstants.Items.Add(lvItem(i));
		}

		private void setIndex(int i)
		{
			internalchg = true;
			if (i >= 0) this.lvConstants.Items[i].Selected = true;
			else if (index >= 0) this.lvConstants.Items[index].Selected = false;
			internalchg = false;

			if (this.lvConstants.SelectedItems.Count > 0)
			{
				if (this.lvConstants.Focused) this.lvConstants.SelectedItems[0].Focused = true;
				this.lvConstants.SelectedItems[0].EnsureVisible();
			}

			if (index == i) return;
			index = i;
			displayBconItem();
		}


		private void displayBconItem()
		{
			internalchg = true;
			if (index >= 0 && index < wrapper.Count)
			{
				origItem = currentItem = wrapper[index];

				this.tbValueHex.Text = "0x" + SimPe.Helper.HexString(currentItem);
				this.tbValueDec.Text = currentItem.ToString();

				this.tbValueHex.IsEnabled = this.tbValueDec.IsEnabled = true;
			}
			else
			{
				origItem = currentItem = -1;
				this.tbValueHex.Text = this.tbValueDec.Text = "";
				this.tbValueHex.IsEnabled = this.tbValueDec.IsEnabled = false;
			}
			this.btnStrPrev.IsEnabled = (index > 0);
			this.btnStrNext.IsEnabled = (index < this.lvConstants.Items.Count - 1);
			internalchg = false;

			this.btnCancel.IsEnabled = false;
		}



        private bool isPopup { get { return this.Tag == null ? false : ((string)(this.Tag)).StartsWith("Popup"); } }
        private bool isNoOverride { get { return this.Tag == null ? false : ((string)(this.Tag)).Contains(";noOverride"); } }
        private string expName
        {
            get
            {
                if (this.Tag != null)
                {
                    string s = (string)this.Tag;
                    int i = s.IndexOf(";expName=+");
                    if (i >= 0) return s.Substring(i + 10).TrimEnd('+');
                }
                foreach (pjse.FileTable.Entry item in pjse.FileTable.GFT[wrapper.Package, wrapper.FileDescriptor])
                    if (item.PFD == wrapper.FileDescriptor)
                    {
                        if (item.IsMaxis) return pjse.Localization.GetString("expCurrent");
                        else break;
                    }
                return pjse.Localization.GetString("expCustom");
            }
        }

        private bool isOverride
        {
            get
            {
                llIsOverride.Tag = null;
                pjse.FileTable.Entry[] items = pjse.FileTable.GFT[wrapper.Package, wrapper.FileDescriptor];
                if (items.Length <= 1) return false;

                pjse.FileTable.Entry item = items[items.Length - 1];
                if (item.PFD == wrapper.FileDescriptor) return false;
                if (!item.IsMaxis) return false;

                llIsOverride.Tag = item;
                return true;
            }
        }

        private void common_Popup(pjse.FileTable.Entry item, SimPe.ExpansionItem exp, bool noOverride)
        {
            if (item == null) return;
            Bcon bcon = new Bcon();
            bcon.ProcessData(item.PFD, item.Package);

            BconForm ui = (BconForm)bcon.UIHandler;
            string tag = "Popup";
            if (noOverride) tag += ";noOverride";
            if (exp != null) tag += ";expName=+" + exp.NameShort + "+";
            ui.Tag = tag;

            bcon.RefreshUI();
            var win = new Window { Content = ui, Title = "BCON Viewer" };
            win.Show();
        }

        private String formTitle
        {
            get
            {
                return pjse.Localization.GetString("pjseWindowTitle"
                    , expName
                    , System.IO.Path.GetFileName(wrapper.Package.SaveFileName)
                    , wrapper.FileDescriptor.TypeName.shortname
                    , "0x" + SimPe.Helper.HexString(wrapper.FileDescriptor.Group)
                    , "0x" + SimPe.Helper.HexString((ushort)wrapper.FileDescriptor.Instance)
                    , wrapper.FileName
                    , pjse.Localization.GetString(isPopup ? "pjseWindowTitleView" : "pjseWindowTitleEdit")
                    );
            }
        }


        private void doUpdateBCON()
        {
            if (!isOverride) return;
            pjse.FileTable.Entry item = (pjse.FileTable.Entry)llIsOverride.Tag;
            Bcon bcon = new Bcon();
            bcon.ProcessData(item.PFD, item.Package);
            internalchg = true;
            while (wrapper.Count < bcon.Count)
                wrapper.Add(new BconItem(bcon[wrapper.Count]));
            internalchg = false;
            updateLists();
        }



		private void BconItemAdd()
		{
			bool savedstate = internalchg;
			internalchg = true;

            try
            {
                wrapper.Add(0);
                this.lvConstants.Items.Add(lvItem(wrapper.Count - 1));
            }
            catch { }

			internalchg = savedstate;

			setIndex(lvConstants.Items.Count - 1);
		}

		private void BconItemDelete()
		{
			bool savedstate = internalchg;
			internalchg = true;

			int i = index;
			wrapper.RemoveAt(i);
			updateLists();

			internalchg = savedstate;

			setIndex((i >= lvConstants.Items.Count) ? lvConstants.Items.Count - 1 : i);
		}

		private void Commit()
		{
			bool savedstate = internalchg;
			internalchg = true;

			try
			{
				wrapper.SynchronizeUserData();
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(pjse.Localization.GetString("errwritingfile"), ex);
			}

			btnCommit.IsEnabled = wrapper.Changed;

			int i = index;
			updateLists();

			internalchg = savedstate;

			setIndex((i >= lvConstants.Items.Count) ? lvConstants.Items.Count - 1 : i);
		}

		private void Cancel()
		{
			bool savedstate = internalchg;
			internalchg = true;

			UpdateBconItem_Value(origItem, true, true);

			internalchg = savedstate;

			displayBconItem();
		}

        private async Task TRCNMaker()
        {
            try
            {
                int minArgc = 0;
                Trcn trcn = (Trcn)wrapper.SiblingResource(Trcn.Trcntype);

                wrapper.Package.BeginUpdate();

                if (trcn != null && trcn.TextOnly)
                {
                    SimPe.DialogResult dr = await MessageBox.ShowAsync(
                        pjse.Localization.GetString("ml_overwriteduff")
                        , btnTRCNMaker.Content?.ToString()
                        , MessageBoxButtons.OKCancel
                        , MessageBoxIcon.Warning);
                    if (dr != SimPe.DialogResult.OK)
                        return;
                    wrapper.Package.Remove(trcn.FileDescriptor);
                    trcn = null;
                }
                if (trcn != null)
                {
                    uint vers = trcn.Version;
                    SimPe.DialogResult dr = await MessageBox.ShowAsync(
                        pjse.Localization.GetString("ml_keeplabels")
                        , btnTRCNMaker.Content?.ToString()
                        , MessageBoxButtons.YesNoCancel
                        , MessageBoxIcon.Warning);
                    if (dr == SimPe.DialogResult.Cancel)
                        return;

                    if (!trcn.Package.Equals(wrapper.Package))
                    {
                        if (dr == SimPe.DialogResult.Yes) Wait.MaxProgress = trcn.Count;
                        SimPe.Interfaces.Files.IPackedFileDescriptor npfd = trcn.FileDescriptor.Clone();
                        Trcn ntrcn = new Trcn();
                        ntrcn.FileDescriptor = npfd;
                        wrapper.Package.Add(npfd, true);
                        ntrcn.ProcessData(npfd, wrapper.Package);
                        if (dr == SimPe.DialogResult.Yes) foreach (TrcnItem item in trcn) { ntrcn.Add(item); Wait.Progress++; }
                        trcn = ntrcn;
                        trcn.SynchronizeUserData();
                        trcn.Version = vers;
                        Wait.MaxProgress = 0;
                    }

                    if (dr == SimPe.DialogResult.Yes)
                        minArgc = trcn.Count;
                    else
                        trcn.Clear();
                }
                else
                {
                    SimPe.Interfaces.Files.IPackedFileDescriptor npfd = wrapper.FileDescriptor.Clone();
                    trcn = new Trcn();
                    npfd.Type = Trcn.Trcntype;
                    trcn.FileDescriptor = npfd;
                    wrapper.Package.Add(npfd, true);
                    trcn.SynchronizeUserData();
                }

                Wait.MaxProgress = wrapper.Count - minArgc;
                trcn.FileName = wrapper.FileName;

                for (int arg = minArgc; arg < wrapper.Count; arg++)
                {
                    trcn.Add(new TrcnItem(trcn));
                    trcn[arg].ConstId = (uint)arg;
                    trcn[arg].ConstName = "LabelCompat " + arg.ToString();
                    trcn[arg].DefValue = trcn[arg].MaxValue = trcn[arg].MinValue = 0;
                    Wait.Progress++;
                }
                trcn.SynchronizeUserData();
                wrapper.Package.EndUpdate();
            }
            finally
            {
                Wait.SubStop();
            }
            await MessageBox.ShowAsync(
                    pjse.Localization.GetString("ml_done")
                    , btnTRCNMaker.Content?.ToString()
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Information);
        }

        private void FiletableRefresh(object sender, System.EventArgs e)
        {
            pjse_banner1.SiblingEnabled = wrapper != null && wrapper.SiblingResource(Trcn.Trcntype) != null;
            updateLists();
        }
        #endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Control that will be displayed within SimPe
		/// </summary>
		public Avalonia.Controls.Control GUIHandle
		{
			get { return this; }
		}

		/// <summary>
		/// Called by the AbstractWrapper when the file should be displayed to the user.
		/// </summary>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Bcon)wrp;
			WrapperChanged(wrapper, null);
            pjse_banner1.SiblingEnabled = wrapper.SiblingResource(Trcn.Trcntype) != null;

			internalchg = true;
			updateLists();
			internalchg = false;

			setIndex(lvConstants.Items.Count > 0 ? 0 : -1);

            btnClose.IsVisible = isPopup;

			if (!setHandler)
			{
				wrapper.WrapperChanged += (s, e) => this.WrapperChanged(s, e);
				setHandler = true;
			}
		}

		private void WrapperChanged(object sender, System.EventArgs e)
		{
            if (isPopup) wrapper.Changed = false;

            this.btnCommit.IsEnabled = wrapper.Changed;
            if (index >= 0 && sender is BconItem && wrapper.IndexOf((BconItem)sender) == index)
            {
                this.btnCancel.IsEnabled = true;
                return;
            }

			if (internalchg) return;

            if (sender.Equals(wrapper))
            {
                internalchg = true;
                this.cbFlag.Checked = wrapper.Flag;
                this.llIsOverride.IsVisible = !isNoOverride && isOverride;
                tbFilename.Text = wrapper.FileName;
                cmpBCON.Wrapper = wrapper;
                cmpBCON.WrapperName = wrapper.FileName;
                internalchg = false;
			}
            else
				updateLists();
		}
		#endregion

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
            // Create controls
            this.lbFilename = new LabelCompat();
            this.tbFilename = new TextBoxCompat();
            this.tbValueDec = new TextBoxCompat();
            this.tbValueHex = new TextBoxCompat();
            this.label5 = new LabelCompat();
            this.label6 = new LabelCompat();
            this.gbValue = new GroupBox();
            this.btnCancel = new ButtonCompat();
            this.bconPanel = new StackPanel();
            this.btnClose = new ButtonCompat();
            this.cmpBCON = new pjse.CompareButton();
            this.llIsOverride = new LinkLabel();
            this.btnUpdateBCON = new ButtonCompat();
            this.pjse_banner1 = new pjse.pjse_banner();
            this.btnStrPrev = new ButtonCompat();
            this.btnStrNext = new ButtonCompat();
            this.cbFlag = new CheckBoxCompat();
            this.btnStrDelete = new ButtonCompat();
            this.btnStrAdd = new ButtonCompat();
            this.lvConstants = new ListView();
            this.chID = new ColumnHeader();
            this.chValue = new ColumnHeader();
            this.chLabel = new ColumnHeader();
            this.btnCommit = new ButtonCompat();
            this.btnTRCNMaker = new ButtonCompat();

            // -- pjse_banner1 --
            this.pjse_banner1.TitleText = "Behaviour Constant";
            this.pjse_banner1.SiblingText = "TRCN";
            this.pjse_banner1.SiblingVisible = true;

            // -- Filename row --
            this.lbFilename.Content = "Filename";
            this.lbFilename.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;

            this.tbFilename.MaxLength = 64;
            this.tbFilename.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
            this.tbFilename.Padding = new Avalonia.Thickness(4, 1);
            this.tbFilename.MinHeight = 0;

            this.cbFlag.Content = "Flag";
            this.cbFlag.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            this.cbFlag.Padding = new Avalonia.Thickness(4, 0);

            this.btnCommit.Content = "Commit File";
            this.btnCommit.MinWidth = 87;
            this.btnCommit.Padding = new Avalonia.Thickness(8, 2);

            var filenameRow = new Avalonia.Controls.DockPanel { Margin = new Avalonia.Thickness(4, 2) };
            Avalonia.Controls.DockPanel.SetDock(this.lbFilename, Avalonia.Controls.Dock.Left);
            Avalonia.Controls.DockPanel.SetDock(this.btnCommit, Avalonia.Controls.Dock.Right);
            Avalonia.Controls.DockPanel.SetDock(this.cbFlag, Avalonia.Controls.Dock.Right);
            filenameRow.Children.Add(this.lbFilename);
            filenameRow.Children.Add(this.btnCommit);
            filenameRow.Children.Add(this.cbFlag);
            filenameRow.Children.Add(this.tbFilename);
            this.tbFilename.Margin = new Avalonia.Thickness(4, 0);
            this.cbFlag.Margin = new Avalonia.Thickness(4, 0);

            // -- Add/Delete buttons row --
            this.btnStrAdd.Content = "Add Value";
            this.btnStrAdd.MinWidth = 79;
            this.btnStrDelete.Content = "Delete Value";
            this.btnStrDelete.MinWidth = 79;

            var addDeleteRow = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 4,
                Margin = new Avalonia.Thickness(4, 2)
            };
            addDeleteRow.Children.Add(this.btnStrAdd);
            addDeleteRow.Children.Add(this.btnStrDelete);

            // -- Value edit area (Hex/Dec/Cancel) --
            this.label5.Content = "Hex";
            this.label5.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            this.label5.MinWidth = 30;
            this.label6.Content = "Dec";
            this.label6.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            this.label6.MinWidth = 30;
            this.tbValueHex.MinWidth = 70;
            this.tbValueHex.MaxWidth = 80;
            this.tbValueDec.MinWidth = 70;
            this.tbValueDec.MaxWidth = 80;
            this.btnCancel.Content = "Cancel";
            this.btnCancel.MinWidth = 79;

            var hexRow = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 4
            };
            hexRow.Children.Add(this.label5);
            hexRow.Children.Add(this.tbValueHex);

            var decRow = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 4
            };
            decRow.Children.Add(this.label6);
            decRow.Children.Add(this.tbValueDec);

            var valuePanel = new Avalonia.Controls.StackPanel { Spacing = 4 };
            valuePanel.Children.Add(hexRow);
            valuePanel.Children.Add(decRow);

            // -- Prev/Next arrows --
            this.btnStrPrev.Content = "\u25B2";
            this.btnStrPrev.MinWidth = 20;
            this.btnStrPrev.MaxWidth = 20;
            this.btnStrPrev.MinHeight = 20;
            this.btnStrPrev.Padding = new Avalonia.Thickness(0);
            this.btnStrNext.Content = "\u25BC";
            this.btnStrNext.MinWidth = 20;
            this.btnStrNext.MaxWidth = 20;
            this.btnStrNext.MinHeight = 20;
            this.btnStrNext.Padding = new Avalonia.Thickness(0);

            var navAndValueRow = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 4
            };
            var navArrows = new Avalonia.Controls.StackPanel { Spacing = 2 };
            navArrows.Children.Add(this.btnStrPrev);
            navArrows.Children.Add(this.btnStrNext);

            navAndValueRow.Children.Add(valuePanel);
            navAndValueRow.Children.Add(navArrows);

            // -- 2x2 button grid: Cancel | Make Labels / Compare | Update --
            this.btnTRCNMaker.Content = "Make Labels";
            this.btnTRCNMaker.MinWidth = 79;
            this.cmpBCON.Content = "Compare";
            this.cmpBCON.MinWidth = 79;
            this.btnUpdateBCON.Content = "Update";
            this.btnUpdateBCON.MinWidth = 79;

            var cancelMakeLabelsRow = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 4
            };
            cancelMakeLabelsRow.Children.Add(this.btnCancel);
            cancelMakeLabelsRow.Children.Add(this.btnTRCNMaker);

            var compareUpdateRow = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 4
            };
            compareUpdateRow.Children.Add(this.cmpBCON);
            compareUpdateRow.Children.Add(this.btnUpdateBCON);

            // -- Override link --
            this.llIsOverride.Text = "This is an override.\nView original.";
            this.llIsOverride.IsVisible = false;

            // -- Close button (popup only) --
            this.btnClose.Content = "Close";
            this.btnClose.MinWidth = 87;
            this.btnClose.IsVisible = false;
            this.btnClose.Margin = new Avalonia.Thickness(0, 4, 0, 0);

            // -- Left panel assembly --
            var leftPanel = new Avalonia.Controls.StackPanel
            {
                Spacing = 4,
                Margin = new Avalonia.Thickness(4, 0),
                MinWidth = 175,
                MaxWidth = 175,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top
            };
            leftPanel.Children.Add(addDeleteRow);
            leftPanel.Children.Add(navAndValueRow);
            leftPanel.Children.Add(cancelMakeLabelsRow);
            leftPanel.Children.Add(compareUpdateRow);
            leftPanel.Children.Add(this.llIsOverride);
            leftPanel.Children.Add(this.btnClose);

            // -- ListView --
            this.lvConstants.Columns.AddRange(new ColumnHeader[] { this.chID, this.chValue, this.chLabel });
            this.chID.Text = "Line";
            this.chID.Width = 89;
            this.chValue.Text = "Value";
            this.chValue.Width = 66;
            this.chLabel.Text = "Label";
            this.chLabel.Width = 374;
            this.lvConstants.FullRowSelect = true;
            this.lvConstants.GridLines = true;
            this.lvConstants.HideSelection = false;
            this.lvConstants.MultiSelect = false;
            this.lvConstants.View = SimPe.Scenegraph.Compat.View.Details;
            this.lvConstants.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
            this.lvConstants.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch;

            // -- Main content: left panel + ListView side by side --
            var mainContent = new Avalonia.Controls.DockPanel { Margin = new Avalonia.Thickness(0, 2) };
            Avalonia.Controls.DockPanel.SetDock(leftPanel, Avalonia.Controls.Dock.Left);
            mainContent.Children.Add(leftPanel);
            mainContent.Children.Add(this.lvConstants);

            // -- Root layout --
            var rootPanel = new Avalonia.Controls.DockPanel();
            Avalonia.Controls.DockPanel.SetDock(this.pjse_banner1, Avalonia.Controls.Dock.Top);
            Avalonia.Controls.DockPanel.SetDock(filenameRow, Avalonia.Controls.Dock.Top);
            rootPanel.Children.Add(this.pjse_banner1);
            rootPanel.Children.Add(filenameRow);
            rootPanel.Children.Add(mainContent);

            // Event wiring
            this.tbFilename.TextChanged += (s, e) => this.tbFilename_TextChanged(s, e);
            this.tbFilename.GotFocus += (s, e) => this.tbText_Enter(s, e);
            this.tbValueDec.TextChanged += (s, e) => this.dec16_TextChanged(s, e);
            this.tbValueDec.LostFocus += (s, e) => this.dec16_Validated(s, null);
            this.tbValueDec.GotFocus += (s, e) => this.tbText_Enter(s, e);
            this.tbValueHex.TextChanged += (s, e) => this.hex16_TextChanged(s, e);
            this.tbValueHex.LostFocus += (s, e) => this.hex16_Validated(s, null);
            this.tbValueHex.GotFocus += (s, e) => this.tbText_Enter(s, e);
            this.btnCancel.Click += (s, e) => this.btnCancel_Click(s, e);
            this.llIsOverride.LinkClicked += (s, e) => this.llIsOverride_LinkClicked(s, e);
            this.btnUpdateBCON.Click += (s, e) => this.btnUpdateBCON_Click(s, e);
            this.pjse_banner1.SiblingClick += (s, e) => this.pjse_banner1_SiblingClick(s, e);
            this.btnStrPrev.Click += (s, e) => this.btnStrPrev_Click(s, e);
            this.btnStrNext.Click += (s, e) => this.btnStrNext_Click(s, e);
            this.cbFlag.IsCheckedChanged += (s, e) => this.cbFlag_CheckedChanged(s, e);
            this.btnStrDelete.Click += (s, e) => this.btnStrDelete_Click(s, e);
            this.btnStrAdd.Click += (s, e) => this.btnStrAdd_Click(s, e);
            this.lvConstants.SelectedIndexChanged += (s, e) => this.lvConstants_SelectedIndexChanged(s, e);
            this.btnCommit.Click += (s, e) => this.btnCommit_Clicked(s, e);
            this.btnClose.Click += (s, e) => this.btnClose_Click(s, e);
            this.btnTRCNMaker.Click += async (s, e) => await this.TRCNMaker();
            this.cmpBCON.CompareWith += (s, e) => this.cmpBCON_CompareWith(s, e);

            Content = rootPanel;
		}
		#endregion

		private void lvConstants_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			setIndex((this.lvConstants.SelectedIndices.Count > 0) ? this.lvConstants.SelectedIndices[0] : -1);
		}


		private void btnCommit_Clicked(object sender, System.EventArgs e)
		{
			this.Commit();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Cancel();
			this.tbValueHex.SelectAll();
			this.tbValueHex.Focus();
		}


        private async void pjse_banner1_SiblingClick(object sender, EventArgs e)
        {
            Trcn trcn = (Trcn)wrapper.SiblingResource(Trcn.Trcntype);
            if (trcn == null) return;
            if (trcn.Package != wrapper.Package)
            {
                SimPe.DialogResult dr = await MessageBox.ShowAsync(Localization.GetString("OpenOtherPkg"), pjse_banner1.TitleText, MessageBoxButtons.YesNo);
                if (dr != SimPe.DialogResult.Yes) return;
            }
            SimPe.RemoteControl.OpenPackedFile(trcn.FileDescriptor, trcn.Package);
        }


		private void btnStrPrev_Click(object sender, System.EventArgs e)
		{
			this.setIndex(index - 1);
		}

		private void btnStrNext_Click(object sender, System.EventArgs e)
		{
			this.setIndex(index + 1);
		}

		private void btnStrAdd_Click(object sender, System.EventArgs e)
		{
			this.BconItemAdd();
			this.tbValueHex.SelectAll();
			this.tbValueHex.Focus();
		}

		private void btnStrDelete_Click(object sender, System.EventArgs e)
		{
			this.BconItemDelete();
		}


        private void cmpBCON_CompareWith(object sender, pjse.CompareButton.CompareWithEventArgs e)
        {
            common_Popup(e.Item, e.ExpansionItem, true);
        }

        private void llIsOverride_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            common_Popup((pjse.FileTable.Entry)((LinkLabel)sender).Tag, null, false);
        }


        private void btnUpdateBCON_Click(object sender, EventArgs e)
        {
            doUpdateBCON();
        }


		private void cbFlag_CheckedChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			internalchg = true;
			wrapper.Flag = ((CheckBoxCompat)sender).Checked;
			internalchg = false;
		}


		private void tbText_Enter(object sender, System.EventArgs e)
		{
			((TextBoxCompat)sender).SelectAll();
		}

		private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			internalchg = true;
			wrapper.FileName = tbFilename.Text;
			internalchg = false;
		}


		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;
			UpdateBconItem_Value(Convert.ToInt16(((TextBoxCompat)sender).Text, 16), false, true);
		}

		private void hex16_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBoxCompat)sender).Text = "0x" + Helper.HexString(currentItem);
			internalchg = origstate;
		}


		private void dec16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!dec16_IsValid(sender)) return;
			UpdateBconItem_Value(Convert.ToInt16(((TextBoxCompat)sender).Text, 10), true, false);
		}

		private void dec16_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBoxCompat)sender).Text = currentItem.ToString();
			internalchg = origstate;
		}

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.isPopup)
                (this.VisualRoot as Window)?.Close();
        }
	}
}
