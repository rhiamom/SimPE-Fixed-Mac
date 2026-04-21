/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   pljones@users.sf.net                                                  *
 *                                                                         *
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSweatShop                                *
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
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using Avalonia.Controls;
using SimPe.Scenegraph.Compat;
using MessageBoxButtons = SimPe.Scenegraph.Compat.MessageBoxButtons;
using MessageBoxIcon = SimPe.Scenegraph.Compat.MessageBoxIcon;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;
using pjse;
using Str = pjse.Str;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for BconForm.
	/// </summary>
	public class TtabForm : Window, IPackedFileUI
	{
		#region Form variables

        private StackPanel ttabPanel;
		private TabControlCompat tabControl1;
		private Avalonia.Controls.TabItem tpSettings;
		private LabelCompat lbaction;
		private LabelCompat lbguard;
		private LabelCompat label40;
		private LabelCompat label33;
		private LabelCompat label34;
		private LabelCompat label35;
		private LabelCompat label29;
		private LabelCompat label30;
		private LabelCompat label31;
        private LabelCompat label32;
		private TextBoxCompat tbGuardian;
		private CheckBoxCompat2 cbBitE;
		private CheckBoxCompat2 cbBitF;
		private CheckBoxCompat2 cbBitC;
		private CheckBoxCompat2 cbBitD;
		private CheckBoxCompat2 cbBitB;
		private CheckBoxCompat2 cbBitA;
		private CheckBoxCompat2 cbBit9;
		private CheckBoxCompat2 cbBit8;
		private CheckBoxCompat2 cbBit7;
		private CheckBoxCompat2 cbBit6;
		private CheckBoxCompat2 cbBit5;
		private CheckBoxCompat2 cbBit4;
		private CheckBoxCompat2 cbBit3;
		private CheckBoxCompat2 cbBit2;
		private CheckBoxCompat2 cbBit1;
		private Avalonia.Controls.TabItem tpHumanMotives;
		private CheckBoxCompat2 cbBit0;
		private LabelCompat label24;
        private TextBoxCompat tbAction;
		private TextBoxCompat tbStringIndex;
		private GroupBox gbFlags;
		private TextBoxCompat tbFlags;
		private TextBoxCompat tbAttenuationValue;
		private TextBoxCompat tbAutonomy;
		private LabelCompat label1;
		private TextBoxCompat tbJoinIndex;
		private LabelCompat label2;
		private ButtonCompat btnGuardian;
        private ButtonCompat btnAction;
		private ComboBoxCompat cbAttenuationCode;
		private Avalonia.Controls.ListBox lbttab;
		private ButtonCompat btnAdd;
		private LabelCompat label26;
		private ButtonCompat btnDelete;
		private LabelCompat lbFilename;
		private TextBoxCompat tbFilename;
		private TextBoxCompat tbFormat;
		private LabelCompat label41;
		private ButtonCompat btnCommit;
		private ButtonCompat btnAppend;
		private TextBoxCompat tbUIDispType;
		private TextBoxCompat tbFaceAnimID;
		private TextBoxCompat tbMemIterMult;
		private TextBoxCompat tbObjType;
		private TextBoxCompat tbModelTabID;
		private ComboBoxCompat cbStringIndex;
		private LinkLabel llAction;
		private LinkLabel llGuardian;
        private ButtonCompat btnNoFlags;
        private ButtonCompat btnStrPrev;
        private ButtonCompat btnStrNext;
        private TabPage tpAnimalMotives;
        private TtabItemMotiveTableUI timtuiHuman;
        private TtabItemMotiveTableUI timtuiAnimal;
        private GroupBox gbFlags2;
        private TextBoxCompat tbFlags2;
        private ButtonCompat btnNoFlags2;
        private LabelCompat label3;
        private CheckBoxCompat2 cb2Bit0;
        private CheckBoxCompat2 cb2BitE;
        private CheckBoxCompat2 cb2BitF;
        private CheckBoxCompat2 cb2BitC;
        private CheckBoxCompat2 cb2BitD;
        private CheckBoxCompat2 cb2BitB;
        private CheckBoxCompat2 cb2BitA;
        private CheckBoxCompat2 cb2Bit9;
        private CheckBoxCompat2 cb2Bit8;
        private CheckBoxCompat2 cb2Bit7;
        private CheckBoxCompat2 cb2Bit6;
        private CheckBoxCompat2 cb2Bit5;
        private CheckBoxCompat2 cb2Bit4;
        private CheckBoxCompat2 cb2Bit3;
        private CheckBoxCompat2 cb2Bit2;
        private CheckBoxCompat2 cb2Bit1;
        private LabelCompat lbPieString;
        private pjse_banner pjse_banner1;
        private ButtonCompat btnMoveDown;
        private ButtonCompat btnMoveUp;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tlpSettingsHead;
        private LabelCompat label4;
        private LabelCompat lbTTABEntry;
        private FlowLayoutPanel flpPieStringID;
        private FlowLayoutPanel flpAction;
        private FlowLayoutPanel flpGuard;
        private FlowLayoutPanel flpFileCtrl;
        private TableLayoutPanel tableLayoutPanel1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
				#endregion

		public TtabForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
            TextBoxCompat[] tbua = { tbAction, tbGuardian, tbFlags, tbFlags2, tbUIDispType };
			alHex16 = new ArrayList(tbua);

			TextBoxCompat[] tbia = { tbFormat, tbStringIndex, tbAutonomy, tbFaceAnimID, tbObjType, tbModelTabID, tbJoinIndex };
			alHex32 = new ArrayList(tbia);

			TextBoxCompat[] tbfa = { tbAttenuationValue, tbMemIterMult };
			alFloats = new ArrayList(tbfa);

			CheckBoxCompat2[] cba = {
							    cbBit0 ,cbBit1 ,cbBit2 ,cbBit3 ,cbBit4 ,cbBit5 ,cbBit6 ,cbBit7
							   ,cbBit8 ,cbBit9 ,cbBitA ,cbBitB ,cbBitC ,cbBitD ,cbBitE ,cbBitF
							   ,cb2Bit0 ,cb2Bit1 ,cb2Bit2 ,cb2Bit3 ,cb2Bit4 ,cb2Bit5 ,cb2Bit6 ,cb2Bit7
							   ,cb2Bit8 ,cb2Bit9 ,cb2BitA ,cb2BitB ,cb2BitC ,cb2BitD ,cb2BitE ,cb2BitF
						   };
			alFlags = new ArrayList(cba);

			ComboBoxCompat[] cbb = { cbStringIndex ,cbAttenuationCode };
			alHex32cb = new ArrayList(cbb);

            this.label40.Left = this.tbStringIndex.Left - this.label40.Width - 6;
            this.llAction.Left = this.tbStringIndex.Left - this.llAction.Width - 6;
            this.llGuardian.Left = this.tbStringIndex.Left - this.llGuardian.Width - 6;

            LabelCompat[] al = { label32, label31, label1, label35, label30, label2, label29, label34, label33 };
            //foreach (Label l in al)
            //    l.Left = cbAttenuationCode.Left - l.Width - 6;
            
            if (SimPe.Helper.XmlRegistry.UseBigIcons)
            {
                // splitContainer removed: SplitterDistance = 400;
            }
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		public void Dispose()
		{
            if (setHandler)
            {
                wrapper.WrapperChanged -= new System.EventHandler(this.WrapperChanged);
                pjse.FileTable.GFT.FiletableRefresh -= new EventHandler(GFT_FiletableRefresh);
                setHandler = false;
            }
		}

		#region TtabForm
		/// <summary>
		/// Stores the currently active Wrapper
		/// </summary>
		private Ttab wrapper = null;
		private bool internalchg;
		private bool setHandler = false;
		private ArrayList alHex16;
		private ArrayList alHex32;
		private ArrayList alFloats;
		private ArrayList alFlags;
		private ArrayList alHex32cb;
		private TtabItem origItem;
		private TtabItem currentItem;

		private bool cbHex32_IsValid(object sender)
		{
			if (alHex32cb.IndexOf(sender) < 0)
				throw new Exception("cbHex32_IsValid not applicable to control " + sender.ToString());
			if (((ComboBoxCompat)sender).FindStringExact(((ComboBoxCompat)sender).Text) >= 0) return true;

			try { Convert.ToUInt32(((ComboBoxCompat)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex16_IsValid(object sender)
		{
			if (alHex16.IndexOf(sender) < 0)
				throw new Exception("hex16_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt16(((TextBoxCompat)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex32_IsValid(object sender)
		{
			if (alHex32.IndexOf(sender) < 0)
				throw new Exception("hex32_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToUInt32(((TextBoxCompat)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool float_IsValid(object sender)
		{
			if (alFloats.IndexOf(sender) < 0)
				throw new Exception("float_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToSingle(((TextBoxCompat)sender).Text); }
			catch (Exception) { return false; }
			return true;
		}

		public void Append(pjse.FileTable.Entry e)
		{
			if (e == null || !(e.Wrapper is Ttab)) return;

            uint offset = getTTAsCount();
            uint maxtti = getMaxTtabItemStringIndex() + 1;
            //if (maxtti != wrapper.Count)
            offset = getUserChoice(offset, maxtti, (uint)wrapper.Count);
            if (offset >= 0x8000) return;

            bool savedstate = internalchg;
			internalchg = true;


			Ttab b = (Ttab)e.Wrapper;

			for (int bi = 0; bi < b.Count; bi++)
			{
                wrapper.Add(b[bi]);
                wrapper[wrapper.Count - 1].StringIndex += offset;
                addItem(wrapper.Count - 1);
			}

			internalchg = savedstate;
		}

        private Str str = null;
        private Str StrRes
        {
            get
            {
                if (str == null)
                    str = new Str(wrapper, wrapper.FileDescriptor.Instance, 0x54544173);
                return str;
            }
        }

        private uint getTTAsCount()
		{
            Str w = StrRes;
            if (w == null) return 0;

            uint max = 0;
            for (byte lid = 1; lid < 44; lid++) max = (uint)Math.Max(max, w[lid].Count);
            return max;
        }

        private uint getMaxTtabItemStringIndex()
        {
            uint m = 0;
            foreach(TtabItem ti in wrapper) if (ti.StringIndex > m) m = ti.StringIndex;
            return m;
        }

        private uint getUserChoice(uint offset, uint maxtti, uint nr)
        {
            PickANumber pan = new PickANumber(
                    new ushort[] { (ushort)(maxtti & 0x7fff) },
                    new String[] { "Increase new Pie String IDs by" }
                );
            pan.FormTitle = "\"Pie String ID\" increment";
            pan.Prompt = "";
            pan.ShowDialog(null).GetAwaiter().GetResult();
            if (pan.DialogAccepted)
                return pan.Value;
            return 0xffffffff;
        }

        private void populateCbStringIndex()
        {
            bool prev = internalchg;
            internalchg = true;

            int cbStringIndexSelectedIndex = this.cbStringIndex.SelectedIndex;

            this.cbStringIndex.Items.Clear();

            uint c = getTTAsCount();
            Str w = StrRes;
            for (int i = 0; i < c; i++)
            {
                FallbackStrItem si = w[1, i];
                this.cbStringIndex.Items.Add("0x" + i.ToString("X") + " (" + i + "): "
                    + ((si == null)
                    ? "*!no default string!*"
                    : si.strItem.Title + (si.lidFallback ? " [LID=1]" : "") + (si.fallback.Count > 0 ? " [*]" : "")
                    ));
            }

            if (cbStringIndexSelectedIndex >= 0 && cbStringIndexSelectedIndex < this.cbStringIndex.Items.Count)
                this.cbStringIndex.SelectedIndex = cbStringIndexSelectedIndex;
            else
                this.cbStringIndex.SelectedIndex = -1;

            internalchg = prev;
        }

        private void populateLbttab()
        {
            bool prev = internalchg;
            internalchg = true;
            int lbttabSelectedIndex = this.lbttab.SelectedIndex;

            lbttab.Items.Clear();
            for (int i = 0; i < wrapper.Count; i++) addItem(i);

            if (lbttabSelectedIndex >= 0)
            {
                if (lbttabSelectedIndex < lbttab.Items.Count)
                    this.lbttab.SelectedIndex = lbttabSelectedIndex;
                else
                    this.lbttab.SelectedIndex = lbttab.Items.Count - 1;
            }

            this.            internalchg = false;
            TtabSelect(null, null);

            internalchg = prev;
        }

        private void doFlags()
        {
            internalchg = true;
            Boolset flags = new Boolset(currentItem.Flags);
            if (wrapper.Format < 0x54) flags.flip(new int[] { 4, 5, 6 });
            for (int i = 0; i < flags.Length; i++)
                ((CheckBoxCompat2)alFlags[i]).IsChecked = flags[i];
            internalchg = false;
        }

        private void doFlags2()
        {
            internalchg = true;
            Boolset flags = new Boolset(currentItem.Flags2);
            for (int i = 0; i < flags.Length; i++)
                ((CheckBoxCompat2)alFlags[i + 16]).IsChecked = flags[i];
            internalchg = false;
        }

        private uint previousFormat;
        private void resetFormat()
        {
            bool saved = internalchg;
            internalchg = true;

            currentItem = null;
            lbttab.SelectedIndex = -1;

            for (int i = 0; i < wrapper.Count; i++)
                wrapper[i] = wrapper[i].Clone();

            // Flip those flags
            if (previousFormat < 0x54 && wrapper.Format >= 0x54 || previousFormat >= 0x54 && wrapper.Format < 0x54)
            {
                Boolset flags;
                foreach (TtabItem ti in wrapper)
                {
                    flags = new Boolset(ti.Flags);
                    flags.flip(new int[] { 4, 5, 6 });
                    ti.Flags = flags;
                }
            }

            previousFormat = wrapper.Format;

            internalchg = saved;
        }
        private void setFormat()
        {
            int siWas = lbttab.SelectedIndex;

            if (wrapper.Format < 0x44 && previousFormat >= 0x44)
            {
                SimPe.DialogResult dr = SimPe.Scenegraph.Compat.MessageBox.ShowAsync(pjse.Localization.GetString("ttabForm_Sure"), pjse.Localization.GetString("ttabForm_Single"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation).GetAwaiter().GetResult();
                if (!DialogResult.OK.Equals(dr))
                    wrapper.Format = previousFormat;
                else
                    resetFormat();
            }
            else if (wrapper.Format >= 0x44 && wrapper.Format < 0x54 && (previousFormat < 0x44 || previousFormat >= 0x54))
            {
                SimPe.DialogResult dr = SimPe.Scenegraph.Compat.MessageBox.ShowAsync(pjse.Localization.GetString("ttabForm_Sure"), pjse.Localization.GetString("ttabForm_MultipleFixed"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation).GetAwaiter().GetResult();
                if (!DialogResult.OK.Equals(dr))
                    wrapper.Format = previousFormat;
                else
                    resetFormat();
            }
            else if (wrapper.Format >= 0x54 && previousFormat < 0x54)
            {
                SimPe.DialogResult dr = SimPe.Scenegraph.Compat.MessageBox.ShowAsync(pjse.Localization.GetString("ttabForm_Sure"), pjse.Localization.GetString("ttabForm_MultipleVaries"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation).GetAwaiter().GetResult();
                if (!DialogResult.OK.Equals(dr))
                    wrapper.Format = previousFormat;
                else
                    resetFormat();
            }

            this.tbUIDispType.IsEnabled = this.tbFaceAnimID.IsEnabled =
                this.tbModelTabID.IsEnabled = this.tbMemIterMult.IsEnabled = this.tbObjType.IsEnabled = false;

            this.tabControl1.TabPages.Remove(this.tpAnimalMotives);

            int index = 0;

            if (wrapper.Format >= 0x45)
            {
                this.tbUIDispType.IsEnabled = true;
                if (wrapper.Format >= 0x46)
                {
                    this.tbModelTabID.IsEnabled = true;
                    if (wrapper.Format >= 0x4a)
                    {
                        this.tbFaceAnimID.IsEnabled = true;
                        if (wrapper.Format >= 0x4c)
                        {
                            this.tbMemIterMult.IsEnabled = this.tbObjType.IsEnabled = true;
                            if (wrapper.Format >= 0x54)
                            {
                                this.tabControl1.TabPages.Add(this.tpAnimalMotives);
                                index = 1;
                            }
                        }
                    }
                }
            }
            this.tpHumanMotives.Content = ((String)this.tpHumanMotives.Tag).Split('/')[index];
            for (int i = 0; i < this.alFlags.Count; i++)
            {
                CheckBoxCompat2 lcb = (CheckBoxCompat2)alFlags[i];
                if (lcb.Tag != null && lcb.Tag.ToString().Length > 0)
                    lcb.Content = ((String)lcb.Tag).Split('/')[index];
            }

            if (wrapper.Count > 0 && lbttab.Items.Count > siWas)
                lbttab.SelectedIndex = siWas;
        }

        /// <summary>
        /// Add the ith TtabItem to the lbttab listbox
        /// </summary>
        /// <param name="i">index of TtabItem to add</param>
        private void addItem(int i)
        {
            lbttab.Items.Add(lbttabItem(i));
        }

        private String lbttabItem(int i)
        {
            if (wrapper[i] != null && wrapper[i].StringIndex < cbStringIndex.Items.Count)
                return (String)cbStringIndex.Items[(int)wrapper[i].StringIndex];
            else
                return "[0x" + i.ToString("X") + " (" + i + "): " + pjse.Localization.GetString("unk") + ": 0x" + SimPe.Helper.HexString(wrapper[i].StringIndex) + "]";
        }

		private void setBHAV(int which, ushort target, bool notxt)
		{
			TextBoxCompat[] tbaGA = { tbAction, tbGuardian };
			if (!notxt) tbaGA[which].Text = "0x"+Helper.HexString(target);
			bool found = false;
            LabelCompat[] lbaGA = { lbaction, lbguard };
            lbaGA[which].Content = pjse.BhavWiz.bhavName(wrapper, target, ref found);
            LinkLabel[] llaGA = { llAction, llGuardian };
            llaGA[which].IsEnabled = found;
		}

		private void setStringIndex(uint si, bool doText, bool doCB)
		{
            currentItem.StringIndex = si;
            lbttab.Items[lbttab.SelectedIndex] = lbPieString.Content = lbttabItem(lbttab.SelectedIndex);
            if (doText) tbStringIndex.Text = "0x" + Helper.HexString(si);
			if (doCB)
			{
                if (si >= 0 && si < cbStringIndex.Items.Count)
					this.cbStringIndex.SelectedIndex = (int)si;
				else
				{
					this.cbStringIndex.SelectedIndex = -1;
					this.cbStringIndex.SelectedItem = tbStringIndex.Text;
				}
            }
        }

		#endregion

		#region IPackedFileUI Member
		/// <summary>
		/// Returns the Control that will be displayed within SimPe
		/// </summary>
		public Avalonia.Controls.Control GUIHandle
		{
			get
			{
				return ttabPanel;
			}
		}

		/// <summary>
		/// Called by the AbstractWrapper when the file should be displayed to the user.
		/// </summary>
		/// <param name="wrp">Reference to the wrapper to be displayed.</param>
		public void UpdateGUI(IFileWrapper wrp)
		{
			wrapper = (Ttab) wrp;

            // We don't repopulate cbStringIndex on WrapperChanged
            this.cbStringIndex.SelectedIndex = -1;
            populateCbStringIndex();

            // Avoid warning popups from setFormat()!
            previousFormat = wrapper.Format;
            // WrapperChanged() calls populateLbttab(), so set lbttab.SelectedIndex to -1
            this.lbttab.SelectedIndex = -1;
            WrapperChanged(wrapper, null);

            internalchg = true;
            populateLbttab();
            internalchg = false;

            // Now call TtabSelect (one way or another)
            if (this.lbttab.Items.Count > 0) this.lbttab.SelectedIndex = 0;
            else TtabSelect(null, null);

			if (!setHandler)
			{
				wrapper.WrapperChanged += (s, e) => this.WrapperChanged(s, e);
                pjse.FileTable.GFT.FiletableRefresh += new EventHandler(GFT_FiletableRefresh);
				setHandler = true;
			}
		}

        private void GFT_FiletableRefresh(object sender, EventArgs e)
        {
            str = null;
            if (wrapper == null || wrapper.FileDescriptor == null) return;

            populateCbStringIndex();
            populateLbttab();
        }

		private void WrapperChanged(object sender, System.EventArgs e)
		{
			this.btnCommit.IsEnabled = wrapper.Changed;

            if (internalchg) return;
            internalchg = true;

            if (sender == wrapper)
            {
                this.Title = tbFilename.Text = wrapper.FileName;
                tbFormat.Text = "0x" + Helper.HexString(wrapper.Format);
                setFormat();
            }
            else if (sender is List<TtabItem>)
                populateLbttab();
            else if (sender == wrapper[(lbttab.SelectedIndex)])
                TtabSelect(null, null);

            internalchg = false;
        }

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TtabForm));
            this.ttabPanel = new StackPanel();
            this.splitContainer1 = new SplitContainer();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.lbttab = new Avalonia.Controls.ListBox();
            this.flpFileCtrl = new FlowLayoutPanel();
            this.lbFilename = new LabelCompat();
            this.tbFilename = new TextBoxCompat();
            this.label41 = new LabelCompat();
            this.tbFormat = new TextBoxCompat();
            this.btnCommit = new ButtonCompat();
            this.label26 = new LabelCompat();
            this.btnStrPrev = new ButtonCompat();
            this.btnMoveUp = new ButtonCompat();
            this.btnAdd = new ButtonCompat();
            this.btnStrNext = new ButtonCompat();
            this.btnMoveDown = new ButtonCompat();
            this.btnDelete = new ButtonCompat();
            this.btnAppend = new ButtonCompat();
            this.tabControl1 = new TabControlCompat();
            this.tpSettings = new Avalonia.Controls.TabItem();
            this.tlpSettingsHead = new TableLayoutPanel();
            this.label4 = new LabelCompat();
            this.lbTTABEntry = new LabelCompat();
            this.llGuardian = new LinkLabel();
            this.label40 = new LabelCompat();
            this.llAction = new LinkLabel();
            this.flpPieStringID = new FlowLayoutPanel();
            this.tbStringIndex = new TextBoxCompat();
            this.cbStringIndex = new ComboBoxCompat();
            this.lbPieString = new LabelCompat();
            this.flpAction = new FlowLayoutPanel();
            this.tbAction = new TextBoxCompat();
            this.btnAction = new ButtonCompat();
            this.lbaction = new LabelCompat();
            this.flpGuard = new FlowLayoutPanel();
            this.tbGuardian = new TextBoxCompat();
            this.btnGuardian = new ButtonCompat();
            this.lbguard = new LabelCompat();
            this.gbFlags2 = new GroupBox();
            this.tbFlags2 = new TextBoxCompat();
            this.btnNoFlags2 = new ButtonCompat();
            this.label3 = new LabelCompat();
            this.cb2Bit0 = new CheckBoxCompat2();
            this.cb2BitE = new CheckBoxCompat2();
            this.cb2BitF = new CheckBoxCompat2();
            this.cb2BitC = new CheckBoxCompat2();
            this.cb2BitD = new CheckBoxCompat2();
            this.cb2BitB = new CheckBoxCompat2();
            this.cb2BitA = new CheckBoxCompat2();
            this.cb2Bit9 = new CheckBoxCompat2();
            this.cb2Bit8 = new CheckBoxCompat2();
            this.cb2Bit7 = new CheckBoxCompat2();
            this.cb2Bit6 = new CheckBoxCompat2();
            this.cb2Bit5 = new CheckBoxCompat2();
            this.cb2Bit4 = new CheckBoxCompat2();
            this.cb2Bit3 = new CheckBoxCompat2();
            this.cb2Bit2 = new CheckBoxCompat2();
            this.cb2Bit1 = new CheckBoxCompat2();
            this.cbAttenuationCode = new ComboBoxCompat();
            this.tbModelTabID = new TextBoxCompat();
            this.label33 = new LabelCompat();
            this.tbObjType = new TextBoxCompat();
            this.label34 = new LabelCompat();
            this.tbUIDispType = new TextBoxCompat();
            this.label35 = new LabelCompat();
            this.tbAutonomy = new TextBoxCompat();
            this.tbMemIterMult = new TextBoxCompat();
            this.label29 = new LabelCompat();
            this.tbFaceAnimID = new TextBoxCompat();
            this.label30 = new LabelCompat();
            this.tbAttenuationValue = new TextBoxCompat();
            this.label31 = new LabelCompat();
            this.label32 = new LabelCompat();
            this.gbFlags = new GroupBox();
            this.btnNoFlags = new ButtonCompat();
            this.tbFlags = new TextBoxCompat();
            this.label24 = new LabelCompat();
            this.cbBit0 = new CheckBoxCompat2();
            this.cbBitE = new CheckBoxCompat2();
            this.cbBitF = new CheckBoxCompat2();
            this.cbBitC = new CheckBoxCompat2();
            this.cbBitD = new CheckBoxCompat2();
            this.cbBitB = new CheckBoxCompat2();
            this.cbBitA = new CheckBoxCompat2();
            this.cbBit9 = new CheckBoxCompat2();
            this.cbBit8 = new CheckBoxCompat2();
            this.cbBit7 = new CheckBoxCompat2();
            this.cbBit6 = new CheckBoxCompat2();
            this.cbBit5 = new CheckBoxCompat2();
            this.cbBit4 = new CheckBoxCompat2();
            this.cbBit3 = new CheckBoxCompat2();
            this.cbBit2 = new CheckBoxCompat2();
            this.cbBit1 = new CheckBoxCompat2();
            this.label1 = new LabelCompat();
            this.tbJoinIndex = new TextBoxCompat();
            this.label2 = new LabelCompat();
            this.tpHumanMotives = new Avalonia.Controls.TabItem();
            this.timtuiHuman = new SimPe.PackedFiles.UserInterface.TtabItemMotiveTableUI();
            this.tpAnimalMotives = new SimPe.Scenegraph.Compat.TabPage();
            this.timtuiAnimal = new SimPe.PackedFiles.UserInterface.TtabItemMotiveTableUI();
            this.pjse_banner1 = new pjse.pjse_banner();
            //
            // ttabPanel
            //            this.ttabPanel.Children.Add(this.splitContainer1);
            this.ttabPanel.Children.Add(this.pjse_banner1);
            this.ttabPanel.Name = "ttabPanel";
            // 
            // splitContainer1
            //            // splitContainer removed: FixedPanel = 
            // splitContainer removed: Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            // splitContainer removed: Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            // splitContainer removed: Panel2.Controls.Add(this.tabControl1);
            // 
            // tableLayoutPanel1
            //            this.tableLayoutPanel1.Children.Add(this.lbttab);
            this.tableLayoutPanel1.Children.Add(this.flpFileCtrl);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lbttab
            //            this.lbttab.Name = "lbttab";
            this.lbttab.SelectionChanged += (s, e) => this.TtabSelect(s, e);
            // 
            // flpFileCtrl
            //            this.flpFileCtrl.Children.Add(this.lbFilename);
            this.flpFileCtrl.Children.Add(this.tbFilename);
            this.flpFileCtrl.Children.Add(this.label41);
            this.flpFileCtrl.Children.Add(this.tbFormat);
            this.flpFileCtrl.Children.Add(this.btnCommit);
            this.flpFileCtrl.Children.Add(this.label26);
            this.flpFileCtrl.Children.Add(this.btnStrPrev);
            this.flpFileCtrl.Children.Add(this.btnMoveUp);
            this.flpFileCtrl.Children.Add(this.btnAdd);
            this.flpFileCtrl.Children.Add(this.btnStrNext);
            this.flpFileCtrl.Children.Add(this.btnMoveDown);
            this.flpFileCtrl.Children.Add(this.btnDelete);
            this.flpFileCtrl.Children.Add(this.btnAppend);
            this.flpFileCtrl.Name = "flpFileCtrl";
            // 
            // lbFilename
            //            this.flpFileCtrl.SetFlowBreak(this.lbFilename, true);
            this.lbFilename.Name = "lbFilename";
            // 
            // tbFilename
            //            this.flpFileCtrl.SetFlowBreak(this.tbFilename, true);
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.TextChanged += (s, e) => this.tbFilename_TextChanged(s, e);
            this.tbFilename.LostFocus += (s, e) => this.tbFilename_Validated(s, e);
            // 
            // label41
            //            this.label41.Name = "label41";
            // 
            // tbFormat
            //            this.tbFormat.Name = "tbFormat";
            this.tbFormat.TextChanged += (s, e) => this.hex32_TextChanged(s, e);
            this.tbFormat.LostFocus += (s, e) => this.hex32_Validated(s, e);
            // btnCommit
            //            this.flpFileCtrl.SetFlowBreak(this.btnCommit, true);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Click += (s, e) => this.btnCommit_Click(s, e);
            // 
            // label26
            //            this.flpFileCtrl.SetFlowBreak(this.label26, true);
            this.label26.Name = "label26";
            // 
            // btnStrPrev
            //            this.btnStrPrev.Name = "btnStrPrev";
            this.btnStrPrev.Click += (s, e) => this.btnStrPrev_Click(s, e);
            // 
            // btnMoveUp
            //            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Click += (s, e) => this.btnMoveUp_Click(s, e);
            // 
            // btnAdd
            //            this.flpFileCtrl.SetFlowBreak(this.btnAdd, true);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += (s, e) => this.btnAdd_Click(s, e);
            // 
            // btnStrNext
            //            this.btnStrNext.Name = "btnStrNext";
            this.btnStrNext.Click += (s, e) => this.btnStrNext_Click(s, e);
            // 
            // btnMoveDown
            //            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Click += (s, e) => this.btnMoveDown_Click(s, e);
            // 
            // btnDelete
            //            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Click += (s, e) => this.btnDelete_Click(s, e);
            // 
            // btnAppend
            //            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Click += (s, e) => this.btnAppend_Click(s, e);
            // 
            // tabControl1
            //            this.tabControl1.Items.Add(this.tpSettings);
            this.tabControl1.Items.Add(this.tpHumanMotives);
            this.tabControl1.Items.Add(this.tpAnimalMotives);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tpSettings
            //            this.// tab content: this.tlpSettingsHead;
            this.tpSettings.Name = "tpSettings";
            // tlpSettingsHead
            //            this.tlpSettingsHead.Children.Add(this.label4);
            this.tlpSettingsHead.Children.Add(this.lbTTABEntry);
            this.tlpSettingsHead.Children.Add(this.llGuardian);
            this.tlpSettingsHead.Children.Add(this.label40);
            this.tlpSettingsHead.Children.Add(this.llAction);
            this.tlpSettingsHead.Children.Add(this.flpPieStringID);
            this.tlpSettingsHead.Children.Add(this.flpAction);
            this.tlpSettingsHead.Children.Add(this.flpGuard);
            this.tlpSettingsHead.Name = "tlpSettingsHead";
            // 
            // label4
            //            this.label4.Name = "label4";
            // 
            // lbTTABEntry
            //            this.lbTTABEntry.Name = "lbTTABEntry";
            // 
            // llGuardian
            //            this.llGuardian.Name = "llGuardian";
            this.llGuardian.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);
            // 
            // label40
            //            this.label40.Name = "label40";
            // 
            // llAction
            //            this.llAction.Name = "llAction";
            this.llAction.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);
            // 
            // flpPieStringID
            //            this.flpPieStringID.Children.Add(this.tbStringIndex);
            this.flpPieStringID.Children.Add(this.cbStringIndex);
            this.flpPieStringID.Children.Add(this.lbPieString);
            this.flpPieStringID.Name = "flpPieStringID";
            // 
            // tbStringIndex
            //            this.tbStringIndex.Name = "tbStringIndex";
            this.tbStringIndex.TextChanged += (s, e) => this.hex32_TextChanged(s, e);
            this.tbStringIndex.LostFocus += (s, e) => this.hex32_Validated(s, e);
            // cbStringIndex
            //            this.cbStringIndex.DisplayMember = "Display";
            this.cbStringIndex.Name = "cbStringIndex";
            this.cbStringIndex.ValueMember = "Value";
            this.cbStringIndex.SelectionChanged += (s, e) => this.cbHex32_SelectedIndexChanged(s, e);
            this.cbStringIndex.GotFocus += (s, e) => this.cbHex32_Enter(s, e);
            this.cbStringIndex.LostFocus += (s, e) => this.cbHex32_Validated(s, e);
            this.cbStringIndex.TextChanged += (s, e) => this.cbHex32_TextChanged(s, e);
            // 
            // lbPieString
            //            this.lbPieString.Name = "lbPieString";
            // flpAction
            //            this.flpAction.Children.Add(this.tbAction);
            this.flpAction.Children.Add(this.btnAction);
            this.flpAction.Children.Add(this.lbaction);
            this.flpAction.Name = "flpAction";
            // 
            // tbAction
            //            this.tbAction.Name = "tbAction";
            this.tbAction.TextChanged += (s, e) => this.hex16_TextChanged(s, e);
            this.tbAction.LostFocus += (s, e) => this.hex16_Validated(s, e);
            // btnAction
            //            this.btnAction.Name = "btnAction";
            this.btnAction.Click += (s, e) => this.GetTTABAction(s, e);
            // 
            // lbaction
            //            this.lbaction.Name = "lbaction";
            // flpGuard
            //            this.flpGuard.Children.Add(this.tbGuardian);
            this.flpGuard.Children.Add(this.btnGuardian);
            this.flpGuard.Children.Add(this.lbguard);
            this.flpGuard.Name = "flpGuard";
            // 
            // tbGuardian
            //            this.tbGuardian.Name = "tbGuardian";
            this.tbGuardian.TextChanged += (s, e) => this.hex16_TextChanged(s, e);
            this.tbGuardian.LostFocus += (s, e) => this.hex16_Validated(s, e);
            // btnGuardian
            //            this.btnGuardian.Name = "btnGuardian";
            this.btnGuardian.Click += (s, e) => this.GetTTABGuard(s, e);
            // 
            // lbguard
            //            this.lbguard.Name = "lbguard";
            // gbFlags2
            // 
            this.gbFlags2.Children.Add(this.tbFlags2);
            this.gbFlags2.Children.Add(this.btnNoFlags2);
            this.gbFlags2.Children.Add(this.label3);
            this.gbFlags2.Children.Add(this.cb2Bit0);
            this.gbFlags2.Children.Add(this.cb2BitE);
            this.gbFlags2.Children.Add(this.cb2BitF);
            this.gbFlags2.Children.Add(this.cb2BitC);
            this.gbFlags2.Children.Add(this.cb2BitD);
            this.gbFlags2.Children.Add(this.cb2BitB);
            this.gbFlags2.Children.Add(this.cb2BitA);
            this.gbFlags2.Children.Add(this.cb2Bit9);
            this.gbFlags2.Children.Add(this.cb2Bit8);
            this.gbFlags2.Children.Add(this.cb2Bit7);
            this.gbFlags2.Children.Add(this.cb2Bit6);
            this.gbFlags2.Children.Add(this.cb2Bit5);
            this.gbFlags2.Children.Add(this.cb2Bit4);
            this.gbFlags2.Children.Add(this.cb2Bit3);
            this.gbFlags2.Children.Add(this.cb2Bit2);
            this.gbFlags2.Children.Add(this.cb2Bit1);            this.gbFlags2.Name = "gbFlags2";
            // tbFlags2
            //            this.tbFlags2.Name = "tbFlags2";
            this.tbFlags2.TextChanged += (s, e) => this.hex16_TextChanged(s, e);
            this.tbFlags2.LostFocus += (s, e) => this.hex16_Validated(s, e);
            // btnNoFlags2
            //            this.btnNoFlags2.Name = "btnNoFlags2";
            this.btnNoFlags2.Click += (s, e) => this.btnNoFlags2_Click(s, e);
            // 
            // label3
            //            this.label3.Name = "label3";
            // 
            // cb2Bit0
            //            this.cb2Bit0.Name = "cb2Bit0";
            this.cb2Bit0.Tag = "";
            this.cb2Bit0.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2BitE
            //            this.cb2BitE.Name = "cb2BitE";
            this.cb2BitE.Tag = "";
            this.cb2BitE.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2BitF
            //            this.cb2BitF.Name = "cb2BitF";
            this.cb2BitF.Tag = "";
            this.cb2BitF.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2BitC
            //            this.cb2BitC.Name = "cb2BitC";
            this.cb2BitC.Tag = "?/adult small dogs";
            this.cb2BitC.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2BitD
            //            this.cb2BitD.Name = "cb2BitD";
            this.cb2BitD.Tag = "?/elder small dogs";
            this.cb2BitD.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2BitB
            //            this.cb2BitB.Name = "cb2BitB";
            this.cb2BitB.Tag = "?/elder cats";
            this.cb2BitB.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2BitA
            //            this.cb2BitA.Name = "cb2BitA";
            this.cb2BitA.Tag = "?/elder big dogs";
            this.cb2BitA.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2Bit9
            //            this.cb2Bit9.Name = "cb2Bit9";
            this.cb2Bit9.Tag = "?/kittens";
            this.cb2Bit9.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2Bit8
            //            this.cb2Bit8.Name = "cb2Bit8";
            this.cb2Bit8.Tag = "?/puppies";
            this.cb2Bit8.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2Bit7
            //            this.cb2Bit7.Name = "cb2Bit7";
            this.cb2Bit7.Tag = "";
            this.cb2Bit7.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2Bit6
            //            this.cb2Bit6.Name = "cb2Bit6";
            this.cb2Bit6.Tag = "";
            this.cb2Bit6.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2Bit5
            //            this.cb2Bit5.Name = "cb2Bit5";
            this.cb2Bit5.Tag = "";
            this.cb2Bit5.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2Bit4
            //            this.cb2Bit4.Name = "cb2Bit4";
            this.cb2Bit4.Tag = "";
            this.cb2Bit4.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2Bit3
            //            this.cb2Bit3.Name = "cb2Bit3";
            this.cb2Bit3.Tag = "";
            this.cb2Bit3.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2Bit2
            //            this.cb2Bit2.Name = "cb2Bit2";
            this.cb2Bit2.Tag = "";
            this.cb2Bit2.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cb2Bit1
            //            this.cb2Bit1.Name = "cb2Bit1";
            this.cb2Bit1.Tag = "";
            this.cb2Bit1.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbAttenuationCode
            this.cbAttenuationCode.Name = "cbAttenuationCode";
            this.cbAttenuationCode.SelectionChanged += (s, e) => this.cbHex32_SelectedIndexChanged(s, e);
            this.cbAttenuationCode.GotFocus += (s, e) => this.cbHex32_Enter(s, e);
            this.cbAttenuationCode.LostFocus += (s, e) => this.cbHex32_Validated(s, e);
            this.cbAttenuationCode.TextChanged += (s, e) => this.cbHex32_TextChanged(s, e);
            // 
            // tbModelTabID
            //            this.tbModelTabID.Name = "tbModelTabID";
            this.tbModelTabID.TextChanged += (s, e) => this.hex32_TextChanged(s, e);
            this.tbModelTabID.LostFocus += (s, e) => this.hex32_Validated(s, e);
            // label33
            //            this.label33.Name = "label33";
            // 
            // tbObjType
            //            this.tbObjType.Name = "tbObjType";
            this.tbObjType.TextChanged += (s, e) => this.hex32_TextChanged(s, e);
            this.tbObjType.LostFocus += (s, e) => this.hex32_Validated(s, e);
            // label34
            //            this.label34.Name = "label34";
            // 
            // tbUIDispType
            //            this.tbUIDispType.Name = "tbUIDispType";
            this.tbUIDispType.TextChanged += (s, e) => this.hex16_TextChanged(s, e);
            this.tbUIDispType.LostFocus += (s, e) => this.hex16_Validated(s, e);
            // label35
            //            this.label35.Name = "label35";
            // 
            // tbAutonomy
            //            this.tbAutonomy.Name = "tbAutonomy";
            this.tbAutonomy.TextChanged += (s, e) => this.hex32_TextChanged(s, e);
            this.tbAutonomy.LostFocus += (s, e) => this.hex32_Validated(s, e);
            // tbMemIterMult
            //            this.tbMemIterMult.Name = "tbMemIterMult";
            this.tbMemIterMult.TextChanged += (s, e) => this.float_TextChanged(s, e);
            this.tbMemIterMult.LostFocus += (s, e) => this.float_Validated(s, e);
            // label29
            //            this.label29.Name = "label29";
            // 
            // tbFaceAnimID
            //            this.tbFaceAnimID.Name = "tbFaceAnimID";
            this.tbFaceAnimID.TextChanged += (s, e) => this.hex32_TextChanged(s, e);
            this.tbFaceAnimID.LostFocus += (s, e) => this.hex32_Validated(s, e);
            // label30
            //            this.label30.Name = "label30";
            // 
            // tbAttenuationValue
            //            this.tbAttenuationValue.Name = "tbAttenuationValue";
            this.tbAttenuationValue.TextChanged += (s, e) => this.float_TextChanged(s, e);
            this.tbAttenuationValue.LostFocus += (s, e) => this.float_Validated(s, e);
            // label31
            //            this.label31.Name = "label31";
            // 
            // label32
            //            this.label32.Name = "label32";
            // 
            // gbFlags
            // 
            this.gbFlags.Children.Add(this.btnNoFlags);
            this.gbFlags.Children.Add(this.tbFlags);
            this.gbFlags.Children.Add(this.label24);
            this.gbFlags.Children.Add(this.cbBit0);
            this.gbFlags.Children.Add(this.cbBitE);
            this.gbFlags.Children.Add(this.cbBitF);
            this.gbFlags.Children.Add(this.cbBitC);
            this.gbFlags.Children.Add(this.cbBitD);
            this.gbFlags.Children.Add(this.cbBitB);
            this.gbFlags.Children.Add(this.cbBitA);
            this.gbFlags.Children.Add(this.cbBit9);
            this.gbFlags.Children.Add(this.cbBit8);
            this.gbFlags.Children.Add(this.cbBit7);
            this.gbFlags.Children.Add(this.cbBit6);
            this.gbFlags.Children.Add(this.cbBit5);
            this.gbFlags.Children.Add(this.cbBit4);
            this.gbFlags.Children.Add(this.cbBit3);
            this.gbFlags.Children.Add(this.cbBit2);
            this.gbFlags.Children.Add(this.cbBit1);            this.gbFlags.Name = "gbFlags";
            // btnNoFlags
            //            this.btnNoFlags.Name = "btnNoFlags";
            this.btnNoFlags.Click += (s, e) => this.btnNoFlags_Click(s, e);
            // 
            // tbFlags
            //            this.tbFlags.Name = "tbFlags";
            this.tbFlags.TextChanged += (s, e) => this.hex16_TextChanged(s, e);
            this.tbFlags.LostFocus += (s, e) => this.hex16_Validated(s, e);
            // label24
            //            this.label24.Name = "label24";
            // 
            // cbBit0
            //            this.cbBit0.Name = "cbBit0";
            this.cbBit0.Tag = "";
            this.cbBit0.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBitE
            //            this.cbBitE.Name = "cbBitE";
            this.cbBitE.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBitF
            //            this.cbBitF.Name = "cbBitF";
            this.cbBitF.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBitC
            //            this.cbBitC.Name = "cbBitC";
            this.cbBitC.Tag = "dogs/adult big dogs";
            this.cbBitC.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBitD
            //            this.cbBitD.Name = "cbBitD";
            this.cbBitD.Tag = "cats/adult cats";
            this.cbBitD.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBitB
            //            this.cbBitB.Name = "cbBitB";
            this.cbBitB.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBitA
            //            this.cbBitA.Name = "cbBitA";
            this.cbBitA.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBit9
            //            this.cbBit9.Name = "cbBit9";
            this.cbBit9.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBit8
            //            this.cbBit8.Name = "cbBit8";
            this.cbBit8.Tag = "auto first/auto first?";
            this.cbBit8.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBit7
            //            this.cbBit7.Name = "cbBit7";
            this.cbBit7.Tag = "debug menu/debug menu?";
            this.cbBit7.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBit6
            //            this.cbBit6.Name = "cbBit6";
            this.cbBit6.Tag = "";
            this.cbBit6.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBit5
            //            this.cbBit5.Name = "cbBit5";
            this.cbBit5.Tag = "demo child/2-way?";
            this.cbBit5.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBit4
            //            this.cbBit4.Name = "cbBit4";
            this.cbBit4.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBit3
            //            this.cbBit3.Name = "cbBit3";
            this.cbBit3.Tag = "consecutive/consecutive?";
            this.cbBit3.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBit2
            //            this.cbBit2.Name = "cbBit2";
            this.cbBit2.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // cbBit1
            //            this.cbBit1.Name = "cbBit1";
            this.cbBit1.Tag = "joinable/joinable?";
            this.cbBit1.IsCheckedChanged += (s, e) => this.checkbox_CheckedChanged(s, e);
            // 
            // label1
            //            this.label1.Name = "label1";
            // 
            // tbJoinIndex
            //            this.tbJoinIndex.Name = "tbJoinIndex";
            this.tbJoinIndex.TextChanged += (s, e) => this.hex32_TextChanged(s, e);
            this.tbJoinIndex.LostFocus += (s, e) => this.hex32_Validated(s, e);
            // label2
            //            this.label2.Name = "label2";
            // 
            // tpHumanMotives
            //            this.// tab content: this.timtuiHuman;
            this.tpHumanMotives.Name = "tpHumanMotives";
            this.tpHumanMotives.Tag = "Motives/Human Motives";
            // timtuiHuman
            //            this.timtuiHuman.MotiveTable = null;
            this.timtuiHuman.Name = "timtuiHuman";
            // 
            // tpAnimalMotives
            //            this.// tab content: this.timtuiAnimal;
            this.tpAnimalMotives.Name = "tpAnimalMotives";
            // timtuiAnimal
            //            this.timtuiAnimal.MotiveTable = null;
            this.timtuiAnimal.Name = "timtuiAnimal";
            // 
            // pjse_banner1
            //            this.pjse_banner1.Name = "pjse_banner1";
            // 
            // TtabForm
            // 
            this.Name = "TtabForm";
        }

		#endregion

        // -------------- wrapper
        //
        // wrapper
        //
        // --------------

        private void btnCommit_Click(object sender, System.EventArgs e)
        {
            try
            {
                wrapper.SynchronizeUserData();
                btnCommit.IsEnabled = wrapper.Changed;
                //TtabSelect(null, null);
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage(pjse.Localization.GetString("errwritingfile"), ex);
            }
        }

        private void tbFilename_TextChanged(object sender, System.EventArgs e)
        {
            internalchg = true;
            wrapper.FileName = tbFilename.Text;
            internalchg = false;
        }

        private void tbFilename_Validated(object sender, System.EventArgs e)
        {
            tbFilename.SelectAll();
        }

        // Format is a hex32 field, currently handled with ttabItem
        private void doFormat() { }

        // -------------- wrapper[]
        //
        // wrapper[]
        //
        // --------------

        private void btnStrPrev_Click(object sender, EventArgs e)
        {
            lbttab.SelectedIndex = (lbttab.SelectedIndex) - 1;
        }

        private void btnStrNext_Click(object sender, EventArgs e)
        {
            lbttab.SelectedIndex = (lbttab.SelectedIndex) + 1;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int i = lbttab.SelectedIndex;
            object a, b;

            internalchg = true;
            a = lbttab.Items[i];
            b = lbttab.Items[i - 1];
            wrapper.Move(i, i - 1);
            lbttab.Items[i] = b;
            lbttab.Items[i - 1] = a;
            internalchg = false;

            lbttab.SelectedIndex = (lbttab.SelectedIndex) - 1;
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int i = lbttab.SelectedIndex;
            object a, b;

            internalchg = true;
            a = lbttab.Items[i];
            b = lbttab.Items[i + 1];
            wrapper.Move(i, i + 1);
            lbttab.Items[i] = b;
            lbttab.Items[i + 1] = a;
            internalchg = false;

            lbttab.SelectedIndex = (lbttab.SelectedIndex) + 1;
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            internalchg = true;
            wrapper.Add(wrapper[(lbttab.SelectedIndex)].Clone());
            addItem(wrapper.Count - 1);
            internalchg = false;
            lbttab.SelectedIndex = wrapper.Count - 1;
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            wrapper.RemoveAt(lbttab.SelectedIndex);
        }

        private void btnAppend_Click(object sender, System.EventArgs e)
        {
            this.Append((new pjse.ResourceChooser()).Execute(wrapper.FileDescriptor.Type, wrapper.FileDescriptor.Group, ttabPanel, true));
        }

        // -------------- ttabItem
        //
        // ttabItem
        //
        // --------------

        private void TtabSelect(object sender, System.EventArgs e)
		{
			if (internalchg) return;

            internalchg = true;


            this.btnMoveUp.IsEnabled = this.btnStrPrev.IsEnabled = (lbttab.SelectedIndex > 0);
            this.btnMoveDown.IsEnabled = this.btnStrNext.IsEnabled = (lbttab.SelectedIndex < lbttab.Items.Count - 1);

            if (lbttab.SelectedIndex >= 0)
			{
                lbTTABEntry.Content = "0x" + lbttab.SelectedIndex.ToString("X");
                tabControl1.IsEnabled = btnDelete.IsEnabled = true;

                currentItem = wrapper[(lbttab.SelectedIndex)];
				origItem = currentItem.Clone();

				setStringIndex(currentItem.StringIndex, true, true);

				setBHAV(0, currentItem.Action, false);
				setBHAV(1, currentItem.Guardian, false);

				this.tbFlags.Text = "0x"+Helper.HexString(currentItem.Flags);
				this.tbFlags2.Text = "0x"+Helper.HexString(currentItem.Flags2);
				if (currentItem.AttenuationCode < this.cbAttenuationCode.Items.Count)
				{
					cbAttenuationCode.SelectedIndex = (int)currentItem.AttenuationCode;
				}
				else
				{
					cbAttenuationCode.SelectedIndex = -1;
					// cbAttenuationCode.Content = "0x"+Helper.HexString(currentItem.AttenuationCode); // TODO: set selected item
				}
				tbAttenuationValue.Text = currentItem.AttenuationValue.ToString("N8");
				tbAutonomy.Text = "0x"+Helper.HexString(currentItem.Autonomy);
				tbJoinIndex.Text = "0x"+Helper.HexString(currentItem.JoinIndex);
				tbUIDispType.Text = "0x"+Helper.HexString(currentItem.UIDisplayType);
				tbFaceAnimID.Text = "0x"+Helper.HexString(currentItem.FacialAnimationID);
				tbMemIterMult.Text = currentItem.MemoryIterativeMultiplier.ToString("N8");
				tbObjType.Text = "0x"+Helper.HexString(currentItem.ObjectType);
				tbModelTabID.Text = "0x"+Helper.HexString(currentItem.ModelTableID);
                doFlags();
                doFlags2();

                timtuiHuman.MotiveTable = wrapper[(lbttab.SelectedIndex)].HumanMotives;
                timtuiAnimal.MotiveTable = wrapper[(lbttab.SelectedIndex)].AnimalMotives;
            }
			else
			{
                lbTTABEntry.Content = "---";
                tabControl1.IsEnabled = this.btnDelete.IsEnabled = false;

				cbAttenuationCode.SelectedIndex = -1;
				tbGuardian.Text = tbAction.Text = lbguard.Text = lbaction.Text = tbFlags.Text = tbFlags2.Text =
					tbStringIndex.Text = tbAttenuationValue.Text = tbAutonomy.Text = tbJoinIndex.Text =
					tbUIDispType.Text = tbFaceAnimID.Text = tbMemIterMult.Text = tbObjType.Text = tbModelTabID.Text =
					"";
				for (int i = 0; i < alFlags.Count; i++) ((CheckBoxCompat2)alFlags[i]).IsChecked = false;
			}


            internalchg = false;
        }

        /*
         * By way of reminder:
         * action           - ushort - 4 hex digits (BHAV number)
         * guard            - ushort - 4 hex digits (BHAV number)
         * flags            - ushort - 4 hex digits
         * flags2           - ushort - 4 hex digits
         * strindex         - uint   - 8 hex digits
         * attenuationcode  - uint   - 8 hex digits
         * attenuationvalue - uint   - 8 hex digits
         * autonomy         - uint   - 8 hex digits
         * joinindex        - uint   - 8 hex digits
         * uidisplaytype    - ushort - 4 hex digits
         * facialanimation  - uint   - 8 hex digits
         * memoryitermult   - float  - decimal digits and "."
         * objecttype       - uint   - 8 hex digits
         * modeltableid     - uint   - 8 hex digits
         */

        private void GetTTABGuard(object sender, System.EventArgs e)
		{
			pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, ttabPanel.Parent as Avalonia.Controls.Control, false);
			if (item != null)
				setBHAV(1, (ushort)item.Instance, false);
		}

		private void GetTTABAction(object sender, System.EventArgs e)
		{
			pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, ttabPanel.Parent as Avalonia.Controls.Control, false);
			if (item != null)
				setBHAV(0, (ushort)item.Instance, false);
		}

        private void llBhav_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pjse.FileTable.Entry item = wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, (sender == llAction) ? currentItem.Action : currentItem.Guardian);
            Bhav b = new Bhav();
            b.ProcessData(item.PFD, item.Package);

            BhavForm ui = (BhavForm)b.UIHandler;
            ui.Tag = "Popup" // tells the SetReadOnly function it's in a popup - so everything locked down
                + ";callerID=+" + wrapper.FileDescriptor.ExportFileName + "+";
            string bhavTitle = pjse.Localization.GetString("viewbhav")
                + ": " + b.FileName + " [" + b.Package.SaveFileName + "]";
            b.RefreshUI();
            new Avalonia.Controls.Window { Title = bhavTitle, Content = ui }.Show();
        }

        private void btnNoFlags_Click(object sender, System.EventArgs e)
        {
            internalchg = true;
            currentItem.Flags = (ushort)(wrapper.Format < 0x54 ? 0x0070 : 0x0000);
            this.tbFlags.Text = "0x" + Helper.HexString(currentItem.Flags);
            doFlags();
            internalchg = false;
        }

        private void btnNoFlags2_Click(object sender, EventArgs e)
        {
            internalchg = true;
            currentItem.Flags2 = (ushort)0x0000;
            this.tbFlags2.Text = "0x" + Helper.HexString(currentItem.Flags2);
            doFlags2();
            internalchg = false;
        }

        private void checkbox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (internalchg) return;

            if (!(sender is CheckBoxCompat2)) return;

            int i = alFlags.IndexOf(sender);
            if (i < 0)
                throw new Exception("checkbox_CheckedChanged not applicable to control " + sender.ToString());

            internalchg = true;
            if (i < 16)
            {
                Boolset flags = new Boolset(currentItem.Flags);
                flags.flip(i);
                currentItem.Flags = flags;
                this.tbFlags.Text = "0x" + Helper.HexString(currentItem.Flags);
            }
            else if (i < 32)
            {
                Boolset flags = new Boolset(currentItem.Flags2);
                flags.flip(i - 16);
                currentItem.Flags2 = flags;
                this.tbFlags2.Text = "0x" + Helper.HexString(currentItem.Flags2);
            }
            internalchg = false;
        }

        private void cbHex32_Enter(object sender, System.EventArgs e)
		{
			((ComboBoxCompat)sender).SelectAll();
		}

		private void cbHex32_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!cbHex32_IsValid(sender)) return;
			if (((ComboBoxCompat)sender).FindStringExact(((ComboBoxCompat)sender).Text) >= 0) return;

			uint val = Convert.ToUInt32(((ComboBoxCompat)sender).Text, 16);
			internalchg = true;
			switch (alHex32cb.IndexOf(sender))
			{
				case 0:
					currentItem.StringIndex = val;
					setStringIndex(val, true, false);
					lbttab.Items[lbttab.SelectedIndex] = currentItem;
					break;
				case 1: currentItem.AttenuationCode = val; break;
			}
			internalchg = false;
		}

		private void cbHex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (cbHex32_IsValid(sender)) return;

			e.Cancel = true;

			int i = alHex32cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex32_Validating not applicable to control " + sender.ToString());

			uint val = 0;
			switch (i)
			{
				case 0: val = origItem.StringIndex; currentItem.StringIndex = val; break;
				case 1: val = origItem.AttenuationCode; currentItem.AttenuationCode = val; break;
			}

			bool origstate = internalchg;
			internalchg = true;
			if (i == 0)
			{
				setStringIndex(val, true, true);
				lbttab.Items[lbttab.SelectedIndex] = currentItem;
			}
			else if (i == 1)
			{
				if (val < ((ComboBoxCompat)sender).Items.Count)
				{
					((ComboBoxCompat)sender).SelectedIndex = (int)val;
				}
				else
				{
					((ComboBoxCompat)sender).SelectedIndex = -1;
					((ComboBoxCompat)sender).Content = "0x"+Helper.HexString(val);
				}
			}
			internalchg = origstate;
			((ComboBoxCompat)sender).SelectAll();
		}

		private void cbHex32_Validated(object sender, System.EventArgs e)
		{
			int i = alHex32cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex32_Validated not applicable to control " + sender.ToString());
			if (((ComboBoxCompat)sender).FindStringExact(((ComboBoxCompat)sender).Text) >= 0) return;

			uint val = Convert.ToUInt32(((ComboBoxCompat)sender).Text, 16);

			bool origstate = internalchg;
			internalchg = true;
			if (i == 0)
			{
				setStringIndex(val, true, true);
			}
			else if (i == 1)
			{
				if (val < ((ComboBoxCompat)sender).Items.Count)
				{
					((ComboBoxCompat)sender).SelectedIndex = (int)val;
				}
				else
				{
					((ComboBoxCompat)sender).SelectedIndex = -1;
					((ComboBoxCompat)sender).Content = "0x"+Helper.HexString(val);
				}
			}
			internalchg = origstate;
			((ComboBoxCompat)sender).Select(0, 0);
		}

		private void cbHex32_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			int i = alHex32cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex32_SelectedIndexChanged not applicable to control " + sender.ToString());
			if (((ComboBoxCompat)sender).SelectedIndex == -1) return;

            int val = ((ComboBoxCompat)sender).SelectedIndex;

			internalchg = true;
			if (i == 0)
			{
                setStringIndex((uint)val, true, false);
                tbStringIndex.Focus();
            }
			else if (i == 1)
			{
				currentItem.AttenuationCode = (uint)val;
			}
			internalchg = false;

			((ComboBoxCompat)sender).SelectAll();
		}

		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;

			ushort val = Convert.ToUInt16(((TextBoxCompat)sender).Text, 16);
			internalchg = true;
			switch (alHex16.IndexOf(sender))
			{
				case 0:
					currentItem.Action = val;
					setBHAV(0, val, true);
					break;
				case 1:
					currentItem.Guardian = val;
					setBHAV(1, val, true);
					break;
				case 2:
					currentItem.Flags = val;
					doFlags();
					break;
				case 3:
                    currentItem.Flags2 = val;
                    doFlags2();
                    break;
				case 4: currentItem.UIDisplayType = val; break;
			}
			internalchg = false;
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex16_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			ushort val = 0;
			switch (alHex16.IndexOf(sender))
			{
				case 0:
					currentItem.Action = val = origItem.Action;
					setBHAV(0, val, true);
					break;
				case 1:
					currentItem.Guardian = val = origItem.Guardian;
					setBHAV(1, val, true);
					break;
				case 2:
					currentItem.Flags = val = origItem.Flags;
					doFlags();
					break;
				case 3: currentItem.Flags2 = val = origItem.Flags2; break;
				case 4: currentItem.UIDisplayType = val = origItem.UIDisplayType; break;
			}
			((TextBoxCompat)sender).Text = "0x" + Helper.HexString(val);
			((TextBoxCompat)sender).SelectAll();
			internalchg = false;
		}

		private void hex16_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBoxCompat)sender).Text = "0x" + Helper.HexString(Convert.ToUInt16(((TextBoxCompat)sender).Text, 16));
			((TextBoxCompat)sender).SelectAll();
			internalchg = origstate;
		}

		private void hex32_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex32_IsValid(sender)) return;

			uint val = Convert.ToUInt32(((TextBoxCompat)sender).Text, 16);
			internalchg = true;
			switch (alHex32.IndexOf(sender))
			{
				case 0: wrapper.Format = val; break;
				case 1:
                    setStringIndex(val, false, true);
                    break;
				case 2: currentItem.Autonomy = val; break;
				case 3: currentItem.FacialAnimationID = val; break;
				case 4: currentItem.ObjectType = val; break;
				case 5: currentItem.ModelTableID = val; break;
				case 6: currentItem.JoinIndex = val; break;
			}
			internalchg = false;
		}

		private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex32_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			uint val = 0;
			switch (alHex32.IndexOf(sender))
			{
				case 0: val = wrapper.Format; break;
				case 1:
					currentItem.StringIndex = val = origItem.StringIndex;
					lbttab.Items[lbttab.SelectedIndex] = currentItem;
					break;
				case 2: currentItem.Autonomy = val = origItem.Autonomy; break;
				case 3: currentItem.FacialAnimationID = val = origItem.FacialAnimationID; break;
				case 4: currentItem.ObjectType = val = origItem.ObjectType; break;
				case 5: currentItem.ModelTableID = val = origItem.ModelTableID; break;
				case 6: currentItem.JoinIndex = val = origItem.JoinIndex; break;
			}

			((TextBoxCompat)sender).Text = "0x" + Helper.HexString(val);
			((TextBoxCompat)sender).SelectAll();
			internalchg = false;
		}

		private void hex32_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBoxCompat)sender).Text = "0x" + Helper.HexString(Convert.ToUInt32(((TextBoxCompat)sender).Text, 16));
			((TextBoxCompat)sender).SelectAll();
			internalchg = origstate;
            if (alHex32.IndexOf(sender) == 0) setFormat();
		}

		private void float_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!float_IsValid(sender)) return;

			float val = Convert.ToSingle(((TextBoxCompat)sender).Text);
			internalchg = true;
			switch (alFloats.IndexOf(sender))
			{
				case 0: currentItem.AttenuationValue = val; break;
				case 1: currentItem.MemoryIterativeMultiplier = val; break;
			}
			internalchg = false;
		}

		private void float_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (float_IsValid(sender)) return;

			e.Cancel = true;

			internalchg = true;
			float val = 0.0f;
			switch (alFloats.IndexOf(sender))
			{
				case 0: currentItem.AttenuationValue = val = origItem.AttenuationValue; break;
				case 1: currentItem.MemoryIterativeMultiplier = val = origItem.MemoryIterativeMultiplier; break;
			}

			((TextBoxCompat)sender).Text = val.ToString("N8");
			((TextBoxCompat)sender).SelectAll();
			internalchg = false;
		}

		private void float_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBoxCompat)sender).Text = Convert.ToSingle(((TextBoxCompat)sender).Text).ToString("N8");
			((TextBoxCompat)sender).SelectAll();
			internalchg = origstate;
		}

	}
}
