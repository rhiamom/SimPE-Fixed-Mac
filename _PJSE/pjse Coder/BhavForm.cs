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
using System.Collections;
using System.ComponentModel;
using Avalonia.Controls;
using SimPe.Scenegraph.Compat;
using MessageBoxButtons = SimPe.Scenegraph.Compat.MessageBoxButtons;
using MessageBoxIcon = SimPe.Scenegraph.Compat.MessageBoxIcon;
using SimPe.Interfaces;
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces.Scenegraph;
using SimPe.PackedFiles.Wrapper;
using pjse;

namespace SimPe.PackedFiles.UserInterface
{
	/// <summary>
	/// Summary description for BhavForm.
	/// </summary>
	public class BhavForm : UserControl, IPackedFileUI
	{
		#region Form variables

        private LabelCompat lbFilename;
		private LabelCompat lbFormat;
		private LabelCompat lbType;
		private LabelCompat lbLocalC;
		private TextBoxCompat tbFilename;
		private TextBoxCompat tbType;
		private TextBoxCompat tbArgC;
		private TextBoxCompat tbLocalC;
		private ComboBoxCompat tba1;
		private ComboBoxCompat tba2;
		private LinkLabel llopenbhav;
		private LabelCompat label9;
		private LabelCompat label10;
		private LabelCompat label11;
		private LabelCompat label12;
		private LabelCompat label13;
		private TextBoxCompat tbInst_OpCode;
		private TextBoxCompat tbInst_Op7;
		private TextBoxCompat tbInst_Op6;
		private TextBoxCompat tbInst_Op5;
		private TextBoxCompat tbInst_Op4;
		private TextBoxCompat tbInst_Op3;
		private TextBoxCompat tbInst_Op2;
		private TextBoxCompat tbInst_Op1;
		private TextBoxCompat tbInst_Op0;
		private TextBoxCompat tbInst_Unk7;
		private TextBoxCompat tbInst_Unk6;
		private TextBoxCompat tbInst_Unk5;
		private TextBoxCompat tbInst_Unk4;
		private TextBoxCompat tbInst_Unk3;
		private TextBoxCompat tbInst_Unk2;
		private TextBoxCompat tbInst_Unk1;
		private TextBoxCompat tbInst_Unk0;
		private Canvas gbInstruction;
		private DockPanel bhavPanel;
		private ButtonCompat btnCommit;
		private ButtonCompat btnOpCode;
		private ButtonCompat btnOperandWiz;
		private ButtonCompat btnSort;
		private LabelCompat lbUpDown;
		private TextBoxCompat tbLines;
		private ButtonCompat btnUp;
		private ButtonCompat btnDown;
		private ButtonCompat btnDel;
		private ButtonCompat btnAdd;
		private ButtonCompat btnCancel;
        private SimPe.PackedFiles.UserInterface.BhavInstListControl pnflowcontainer;
		private Canvas gbMove;
		private LabelCompat lbArgC;
		private Canvas gbSpecial;
		private ButtonCompat btnInsTrue;
		private ButtonCompat btnInsFalse;
		private ButtonCompat btnLinkInge;
		private ButtonCompat btnDelPescado;
		private ButtonCompat btnAppend;
		private ComboBoxCompat cbFormat;
		private ButtonCompat btnDelMerola;
		private LabelCompat lbCacheFlags;
		private TextBoxCompat tbCacheFlags;
		private LabelCompat lbTreeVersion;
		private TextBoxCompat tbTreeVersion;
		private TextBoxCompat tbHeaderFlag;
		private LabelCompat lbHeaderFlag;
		private ButtonCompat btnOperandRaw;
		private TextBoxCompat tbInst_NodeVersion;
		private ButtonCompat btnClose;
		private CheckBoxCompat2 cbSpecial;
		private TextBoxCompat tbInst_Longname;
        private ButtonCompat btnCopyListing;
        private ButtonCompat btnTPRPMaker;
        private ButtonCompat btnGUIDIndex;
        private ContextMenuStrip cmenuGUIDIndex;
        private ToolStripMenuItem createAllPackagesToolStripMenuItem;
        private ToolStripMenuItem createCurrentPackageToolStripMenuItem;
        private ToolStripMenuItem loadIndexToolStripMenuItem;
        private ToolStripMenuItem defaultFileToolStripMenuItem;
        private ToolStripMenuItem fromFileToolStripMenuItem;
        private ToolStripMenuItem saveIndexToolStripMenuItem;
        private ToolStripMenuItem defaultFileToolStripMenuItem1;
        private ToolStripMenuItem toFileToolStripMenuItem;
        private ButtonCompat btnCopyBHAV;
        private TextBoxCompat tbHidesOP;
        private LinkLabel llHidesOP;
        private LabelCompat lbHidesOP;
        private ButtonCompat btnPasteListing;
        private ButtonCompat btnZero;
        private SimPe.Scenegraph.Compat.ToolTip ttBhavForm;
        private pjse_banner pjse_banner1;
        private CompareButton cmpBHAV;
        private ButtonCompat btnInsUnlinked;
        private ButtonCompat btnImportBHAV;
        private ButtonCompat button1;
        private IContainer components;
        #endregion
       
		public BhavForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            hidesFmt = llHidesOP.Text;
			this.Tag = "Normal"; // Used by SetReadOnly
            TextBoxCompat[] iob = {
								 tbInst_Op0  ,tbInst_Op1  ,tbInst_Op2  ,tbInst_Op3
								,tbInst_Op4  ,tbInst_Op5  ,tbInst_Op6  ,tbInst_Op7
								,tbInst_Unk0 ,tbInst_Unk1 ,tbInst_Unk2 ,tbInst_Unk3
								,tbInst_Unk4 ,tbInst_Unk5 ,tbInst_Unk6 ,tbInst_Unk7
								,tbInst_NodeVersion
								,tbHeaderFlag
								,tbType
								,tbCacheFlags
								,tbArgC
								,tbLocalC
							};
			alHex8 = new ArrayList(iob);

            TextBoxCompat[] w = { tbInst_OpCode ,tbLines ,};
			alHex16 = new ArrayList(w);

			TextBoxCompat[] dw = { tbTreeVersion ,};
			alHex32 = new ArrayList(dw);

			ComboBoxCompat[] cb = { tba1 ,tba2 ,cbFormat ,};
			alHex16cb = new ArrayList(cb);

            this.button1.IsVisible = (UserVerification.HaveValidUserId && !Helper.XmlRegistry.HiddenMode);

			this.cbSpecial.IsChecked = pjse.Settings.PJSE.ShowSpecialButtons;
			this.gbSpecial.IsVisible = pjse.Settings.PJSE.ShowSpecialButtons;

			pjse.FileTable.GFT.FiletableRefresh += (s, e) => this.FiletableRefresh(s, e);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		public void Dispose()
		{
			if (setHandler && wrapper != null)
			{
                wrapper.FileDescriptor.DescriptionChanged -= new EventHandler(FileDescriptor_DescriptionChanged);
                wrapper.WrapperChanged -= new System.EventHandler(this.WrapperChanged);
				setHandler = false;
			}
			wrapper = null;
			currentInst = null;
			origInst = null;
            alHex8 = alHex16 = alHex32 = alDec8 = alHex16cb = null;
		}

		
		#region BhavForm
		private Bhav wrapper;
		private bool setHandler = false;
		private BhavWiz currentInst;
		private Instruction origInst;
		private bool internalchg;
        private ArrayList alHex8;
		private ArrayList alHex16;
		private ArrayList alHex32;
		private ArrayList alDec8;
		private ArrayList alHex16cb;
        private String hidesFmt = "{0}";

        // These should be on the ExtendedWrapper class or BhavWiz or, indeed, PackedFileDescriptor
        private static IPackedFileDescriptor newPFD(IPackedFileDescriptor oldPFD) { return newPFD(oldPFD.Type, oldPFD.Group, oldPFD.SubType, oldPFD.Instance); }
        private static IPackedFileDescriptor newPFD(uint type, uint group, uint instance) { return newPFD(type, group, 0x00000000, instance); }
        private static IPackedFileDescriptor newPFD(uint type, uint group, uint subtype, uint instance)
        {
            IPackedFileDescriptor npfd = new SimPe.Packages.PackedFileDescriptor();
            npfd.Type = type;
            npfd.Group = group;
            npfd.SubType = subtype;
            npfd.Instance = instance;
            return npfd;
        }

        private IPackageFile currentPackage = null;
        private void TakeACopy()
        {
            IPackedFileDescriptor npfd = newPFD(wrapper.FileDescriptor);
            npfd.UserData = wrapper.Package.Read(wrapper.FileDescriptor).UncompressedData;
            currentPackage.Add(npfd, true);
        }

        private delegate bool ignoreEntry(pjse.FileTable.Entry i, IPackedFileDescriptor npfd);
        private delegate bool matchItem(object o, uint inst);
        private delegate void setter(object o, ushort inst);

        private void doUpdate(string typeName
            , uint oldInst
            , IPackedFileDescriptor npfd
            , pjse.FileTable.Entry[] entries
            , ignoreEntry ieDelegate
            , matchItem[] matchDelegates
            , setter[] setDelegates
            )
        {
            if (npfd == null) return;
            if (entries == null || entries.Length == 0) return;
            if (matchDelegates == null || matchDelegates.Length == 0) return;
            if (setDelegates == null || setDelegates.Length != matchDelegates.Length) return;

            WaitingScreen.Message = "Updating current package - " + typeName + "s...";
            foreach (pjse.FileTable.Entry i in entries)
            {
                ResourceLoader.Refresh(i); // make sure it's been saved before we search it
                // Application.DoEvents() not needed in Avalonia

                AbstractWrapper wrapper = i.Wrapper;
                if (wrapper as IEnumerable == null) break;

                if (ieDelegate != null && ieDelegate(i, npfd)) continue;

                foreach (object o in (IEnumerable)wrapper)
                {
                    for (int j = 0; j < matchDelegates.Length; j++)
                    {
                        matchItem md = matchDelegates[j];
                        setter sd = setDelegates[j];
                        if (md != null && sd != null && md(o, oldInst))
                        {
                            sd(o, (ushort)npfd.Instance);
                        }
                    }
                }
                if (wrapper.Changed)
                {
                    wrapper.SynchronizeUserData();
                    ResourceLoader.Refresh(i);
                }
            }
        }
        private void ImportBHAV()
        {
            WaitingScreen.Wait();

            #region Finding available BHAV number
            WaitingScreen.Message = "Finding available BHAV number...";
            pjse.FileTable.Entry[] ai = pjse.FileTable.GFT[Bhav.Bhavtype, pjse.FileTable.Source.Local];
            ushort newInst = 0x0fff;
            foreach (pjse.FileTable.Entry i in ai) if (i.Instance >= 0x1000 && i.Instance < 0x2000 && i.Instance > newInst) newInst = (ushort)i.Instance;
            newInst++;
            #endregion

            currentPackage.BeginUpdate();

            #region Cloning BHAV
            WaitingScreen.Message = "Cloning BHAV...";
            IPackedFileDescriptor npfd = newPFD(Bhav.Bhavtype, 0xffffffff, newInst);
            npfd.UserData = wrapper.Package.Read(wrapper.FileDescriptor).UncompressedData;
            currentPackage.Add(npfd, true);
            #endregion

            #region Updating current package - BHAVs
            doUpdate("BHAV"
                , wrapper.FileDescriptor.Instance
                , npfd
                , ai
                , delegate(pjse.FileTable.Entry i, IPackedFileDescriptor pfd) { return (i.Group != pfd.Group || i.Instance < 0x1000 || i.Instance >= 0x2000); }
                , new matchItem[] { delegate(object o, uint value) {
                    return ((Instruction)o).OpCode == value; } }
                , new setter[] { delegate(object o, ushort value) { ((Instruction)o).OpCode = value; } }
                );
            #endregion

            #region Updating current package - OBJFs
            doUpdate("OBJF"
                , wrapper.FileDescriptor.Instance
                , npfd
                , pjse.FileTable.GFT[Objf.Objftype, pjse.FileTable.Source.Local]
                , null
                , new matchItem[] {
                    delegate(object o, uint value) { return ((ObjfItem)o).Action == value; },
                    delegate(object o, uint value) { return ((ObjfItem)o).Guardian == value; },
                }
                , new setter[] {
                    delegate(object o, ushort value) { ((ObjfItem)o).Action = value; },
                    delegate(object o, ushort value) { ((ObjfItem)o).Guardian = value; },
                }
                );
            #endregion

            #region Updating current package - TTABs
            doUpdate("TTAB"
                , wrapper.FileDescriptor.Instance
                , npfd
                , pjse.FileTable.GFT[Ttab.Ttabtype, pjse.FileTable.Source.Local]
                , null
                , new matchItem[] {
                    delegate(object o, uint value) { return ((TtabItem)o).Action == value; },
                    delegate(object o, uint value) { return ((TtabItem)o).Guardian == value; },
                }
                , new setter[] {
                    delegate(object o, ushort value) { ((TtabItem)o).Action = value; },
                    delegate(object o, ushort value) { ((TtabItem)o).Guardian = value; },
                }
                );
            #endregion

            currentPackage.EndUpdate();

            WaitingScreen.Message = "";
            WaitingScreen.Stop();
            SimPe.Scenegraph.Compat.MessageBox.ShowAsync(
                pjse.Localization.GetString("ml_done")
                , btnImportBHAV.Content?.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information).GetAwaiter().GetResult();
        }

        private void cmpBHAV_CompareWith(object sender, CompareButton.CompareWithEventArgs e) { common_LinkClicked(e.Item, e.ExpansionItem, true); }
        private void common_LinkClicked(pjse.FileTable.Entry item) { common_LinkClicked(item, null, false); }
        private void common_LinkClicked(pjse.FileTable.Entry item, SimPe.ExpansionItem exp, bool noOverride)
        {
            if (item == null) return; // this should never happen
            Bhav bhav = new Bhav();
            bhav.ProcessData(item.PFD, item.Package);

            BhavForm ui = (BhavForm)bhav.UIHandler;
            string tag = "Popup"; // tells the SetReadOnly function it's in a popup - so everything locked down
            if (noOverride) tag += ";noOverride"; // prevents handleOverride displaying anything
            tag += ";callerID=+" + wrapper.FileDescriptor.ExportFileName +"+";
            if (exp != null) tag += ";expName=+" + exp.NameShort + "+";
            ui.Tag = tag;

            bhav.RefreshUI();
            var win = new Window { Content = ui, Title = "BHAV Viewer" };
            win.Show();
        }

        private string getValueFromTag(string key)
        {
            string s = this.Tag as string;
            if (s == null) return null;

            key = ";" + key + "=+";
            int i = s.IndexOf(key);
            if (i < 0) return null;

            s = s.Substring(i + key.Length);
            i = s.IndexOf("+");
            return (i >= 0) ? s.Substring(0, i) : null;
        }
        private bool isPopup { get { return (this.Tag == null || this.Tag as string == null) ? false : ((string)(this.Tag)).StartsWith("Popup"); } }
        private bool isNoOverride { get { return (this.Tag == null || this.Tag as string == null) ? false : ((string)(this.Tag)).Contains(";noOverride"); } }
        private string callerID { get { return getValueFromTag("callerID"); } }
        private string expName
        {
            get
            {
                string s = getValueFromTag("expName");
                if (s != null) return s;

                foreach(pjse.FileTable.Entry item in pjse.FileTable.GFT[wrapper.Package, wrapper.FileDescriptor])
                    if (item.PFD == wrapper.FileDescriptor)
                    {
                        if (item.IsMaxis) return pjse.Localization.GetString("expCurrent");
                        else break;
                    }
                return pjse.Localization.GetString("expCustom");
            }
        }

        private String formTitle
        {
            get
            {
                return pjse.Localization.GetString("pjseWindowTitle"
                    , expName // EP Name or Custom
                    , System.IO.Path.GetFileName(wrapper.Package.SaveFileName) // package Filename without path
                    , wrapper.FileDescriptor.TypeName.shortname // Type (short name)
                    , "0x" + SimPe.Helper.HexString(wrapper.FileDescriptor.Group) // Group Number
                    , "0x" + SimPe.Helper.HexString((ushort)wrapper.FileDescriptor.Instance) // Instance Number
                    , wrapper.FileName
                    ,  pjse.Localization.GetString(isPopup ? "pjseWindowTitleView" : "pjseWindowTitleEdit") // View or Edit
                    );
            }
        }

        private void handleOverride()
        {
            lbHidesOP.IsVisible = tbHidesOP.IsVisible = llHidesOP.IsVisible = false;
            llHidesOP.Tag = null;
            if (this.isNoOverride) return;

            pjse.FileTable.Entry[] items = pjse.FileTable.GFT[wrapper.Package, wrapper.FileDescriptor];
            
            if (items.Length > 1) // currentpkg, other, fixed, maxis
            {
                pjse.FileTable.Entry item = items[items.Length - 1];
                if (item.PFD == wrapper.FileDescriptor) return;
                if (!item.IsMaxis && !item.IsFixed) return;

                this.lbHidesOP.IsVisible = this.tbHidesOP.IsVisible = this.llHidesOP.IsVisible = true;
                llHidesOP.Links[0].Start -= llHidesOP.Text.Length;
                llHidesOP.Content = hidesFmt.Replace("{0}", System.IO.Path.GetFileName(item.Package.SaveFileName));
                llHidesOP.Links[0].Start += llHidesOP.Text.Length;
                this.tbHidesOP.Text = wrapper.Package.FileName;
                llHidesOP.Tag = item.IsMaxis ? pjse.FileTable.Source.Maxis : pjse.FileTable.Source.Fixed;
            }
        }

        private void SetReadOnly(bool state) 
		{
            //if (this.isPopup) state = true;

            this.tbInst_OpCode.IsReadOnly = state;
			this.btnOpCode.IsEnabled = !state;
			this.tbInst_NodeVersion.IsReadOnly = state || wrapper.Header.Format < 0x8005;
			this.tba1.IsEnabled = !state;
			this.tba2.IsEnabled = !state;

			/*this.tbInst_Op01_dec.IsReadOnly = state;
			this.tbInst_Op23_dec.IsReadOnly = state;*/

			this.tbInst_Op0.IsReadOnly = state;
			this.tbInst_Op1.IsReadOnly = state;
			this.tbInst_Op2.IsReadOnly = state;
			this.tbInst_Op3.IsReadOnly = state;
			this.tbInst_Op4.IsReadOnly = state;
			this.tbInst_Op5.IsReadOnly = state;
			this.tbInst_Op6.IsReadOnly = state;
			this.tbInst_Op7.IsReadOnly = state;

			this.btnOperandWiz.IsEnabled = !state;
			/*this.btnOperandRaw.IsEnabled = !state;*/
            this.btnZero.IsEnabled = !state;
			
			this.tbInst_Unk0.IsReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk1.IsReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk2.IsReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk3.IsReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk4.IsReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk5.IsReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk6.IsReadOnly = state || wrapper.Header.Format < 0x8003;
			this.tbInst_Unk7.IsReadOnly = state || wrapper.Header.Format < 0x8003;

			this.btnUp.IsEnabled = !state;
			this.btnDown.IsEnabled = !state;
			this.tbLines.IsReadOnly = state;
			this.btnDelPescado.IsEnabled = this.btnDel.IsEnabled = !state;
			this.btnInsTrue.IsEnabled = this.btnInsFalse.IsEnabled = this.btnAdd.IsEnabled = !state;
		}

        private bool instIsBhav()
        {
            return wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, currentInst.Instruction.OpCode) != null;
        }

        private void OperandWiz(int type)
        {
            internalchg = true;
            bool changed = false;
            Instruction inst = currentInst.Instruction;
            currentInst = null;
            try
            {
                changed = ((new BhavOperandWiz()).Execute(btnCommit.IsVisible ? inst : inst.Clone(), type) != null);
            }
            finally
            {
                currentInst = inst;
                if (btnCommit.IsVisible)
                {
                    if (changed) UpdateInstPanel();
                    this.btnCancel.IsEnabled = true;
                }
                internalchg = false;
            }
        }

        private void UpdateInstPanel()
		{
			internalchg = true;
			if (currentInst == null || wrapper.IndexOf(currentInst.Instruction) < 0)
			{
				SetReadOnly(true);
				this.llopenbhav.IsEnabled = false;
				this.btnInsTrue.IsEnabled = this.btnInsFalse.IsEnabled = this.btnAdd.IsEnabled = true;

				this.tbInst_OpCode.Text = "";
				this.tbInst_NodeVersion.Text = "";
				this.tba1.SelectedIndex = 0;
				this.tba2.SelectedIndex = 0;
				this.tbInst_Op0.Text = "";
				this.tbInst_Op1.Text = "";
				this.tbInst_Op2.Text = "";
				this.tbInst_Op3.Text = "";
				this.tbInst_Op4.Text = "";
				this.tbInst_Op5.Text = "";
				this.tbInst_Op6.Text = "";
				this.tbInst_Op7.Text = "";
				this.tbInst_Unk0.Text = "";
				this.tbInst_Unk1.Text = "";
				this.tbInst_Unk2.Text = "";
				this.tbInst_Unk3.Text = "";
				this.tbInst_Unk4.Text = "";
				this.tbInst_Unk5.Text = "";
				this.tbInst_Unk6.Text = "";
				this.tbInst_Unk7.Text = "";
			}
			else
			{
				Instruction inst = currentInst.Instruction; // saves typing

				SetReadOnly(false);

				this.tbInst_OpCode.Text = "0x"+Helper.HexString(inst.OpCode);
				this.tbInst_NodeVersion.Text = "0x"+Helper.HexString(inst.NodeVersion);
				if (inst.Target1 >= 0xFFFC && inst.Target1 < 0xFFFF)
				{
					this.tba1.SelectedIndex = inst.Target1 - 0xFFFC;
				}
				else
				{
					this.tba1.SelectedIndex = -1;
					this.tba1.Text = "0x"+Helper.HexString(inst.Target1);
				}
				if (inst.Target2 >= 0xFFFC && inst.Target2 < 0xFFFF)
				{
					this.tba2.SelectedIndex = inst.Target2 - 0xFFFC;
				}
				else
				{
					this.tba2.SelectedIndex = -1;
					this.tba2.Text = "0x"+Helper.HexString(inst.Target2);
				}

				this.tbInst_Op0.Text = Helper.HexString(inst.Operands[0]);
				this.tbInst_Op1.Text = Helper.HexString(inst.Operands[1]);
				this.tbInst_Op2.Text = Helper.HexString(inst.Operands[2]);
				this.tbInst_Op3.Text = Helper.HexString(inst.Operands[3]);
				this.tbInst_Op4.Text = Helper.HexString(inst.Operands[4]);
				this.tbInst_Op5.Text = Helper.HexString(inst.Operands[5]);
				this.tbInst_Op6.Text = Helper.HexString(inst.Operands[6]);
				this.tbInst_Op7.Text = Helper.HexString(inst.Operands[7]);
				this.tbInst_Unk0.Text = Helper.HexString(inst.Reserved1[0]);
				this.tbInst_Unk1.Text = Helper.HexString(inst.Reserved1[1]);
				this.tbInst_Unk2.Text = Helper.HexString(inst.Reserved1[2]);
				this.tbInst_Unk3.Text = Helper.HexString(inst.Reserved1[3]);
				this.tbInst_Unk4.Text = Helper.HexString(inst.Reserved1[4]);
				this.tbInst_Unk5.Text = Helper.HexString(inst.Reserved1[5]);
				this.tbInst_Unk6.Text = Helper.HexString(inst.Reserved1[6]);
				this.tbInst_Unk7.Text = Helper.HexString(inst.Reserved1[7]);
				this.btnUp.IsEnabled = pnflowcontainer.SelectedIndex > 0;
				this.btnDown.IsEnabled = pnflowcontainer.SelectedIndex < wrapper.Count - 1;

				this.btnDelPescado.IsEnabled = this.btnDel.IsEnabled = wrapper.Count > 1;

                this.llopenbhav.IsEnabled = instIsBhav();
				this.btnOperandWiz.IsEnabled = currentInst.Wizard() != null;
			}
            setLongname();
            internalchg = false;
		}

        private void OpcodeChanged(ushort value)
        {
            currentInst.Instruction.OpCode = value; 
            this.currentInst = currentInst.Instruction;
            this.llopenbhav.IsEnabled = instIsBhav();
            this.btnOperandWiz.IsEnabled = currentInst.Wizard() != null;
            setLongname();
        }

        private void ChangeLongname(byte oldval, byte newval) { if (oldval != newval) setLongname(); }

        private static string onearg = pjse.Localization.GetString("oneArg");
        private static string manyargs = pjse.Localization.GetString("manyArgs");
        private void setLongname()
        {
            if (currentInst == null || wrapper.IndexOf(currentInst.Instruction) < 0)
                this.tbInst_Longname.Text = "";
            else
            {
                try
                {
                    this.tbInst_Longname.Text = currentInst.LongName.Replace(", ", ",\r\n  ")
                    .Replace(onearg + ": ", onearg + ":\r\n  ")
                    .Replace(manyargs + ": ", manyargs + ":\r\n  ")
                    ;
                }
                finally { }
            }
        }

		private void CopyListing()
		{
			string listing = "";

			int lines = wrapper.Count;
			for (short i = 0; i < lines; i++)
			{
				Instruction inst = wrapper[i];
				BhavWiz w = inst;

				string operands = "";
				for(int j = 0; j < 8; j++) operands += SimPe.Helper.HexString(inst.Operands[j]);
				for(int j = 0; j < 8; j++) operands += SimPe.Helper.HexString(inst.Reserved1[j]);

				listing += ("     "
					+ SimPe.Helper.HexString(i)
					+ " : " + SimPe.Helper.HexString(inst.OpCode)
                    + " : " + SimPe.Helper.HexString(inst.NodeVersion)
                    + " : " + SimPe.Helper.HexString(inst.Target1)
                    + " : " + SimPe.Helper.HexString(inst.Target2)
                    + " : " + operands
					+ "\r\n" + w.LongName + "\r\n\r\n");
			}

			_ = Avalonia.Controls.TopLevel.GetTopLevel(this)?.Clipboard?.SetTextAsync(listing);
		}

        private void PasteListing()
        {
            int i = 0;
            int origlen = wrapper.Count;

            string listing = Avalonia.Controls.TopLevel.GetTopLevel(this)?.Clipboard?.GetTextAsync().GetAwaiter().GetResult() ?? "";
            foreach (string line in listing.Split('\r', '\n'))
            {
                if (line.Length == 0) continue;
                string[] args = line.Split(':');
                if (args.Length != 6) continue;

                try
                {
                    if (Convert.ToUInt32(args[0].Trim(), 16) != i)
                        throw new Exception("Foo");

                    Instruction inst = new Instruction(wrapper);

                    inst.OpCode = Convert.ToUInt16(args[1].Trim(), 16);
                    inst.NodeVersion = Convert.ToByte(args[2].Trim(), 16);
                    inst.Target1 = Convert.ToUInt16(args[3].Trim(), 16);
                    inst.Target2 = Convert.ToUInt16(args[4].Trim(), 16);
                    for (int j = 0; j < 8; j++)
                        inst.Operands[j] = Convert.ToByte(args[5].Trim().Substring(j * 2, 2), 16);
                    for (int j = 0; j < 8; j++)
                        inst.Reserved1[j] = Convert.ToByte(args[5].Trim().Substring(16 + j * 2, 2), 16);

                    if (inst.Target1 < 0xfffc) inst.Target1 = (ushort)(inst.Target1 + origlen);
                    if (inst.Target2 < 0xfffc) inst.Target2 = (ushort)(inst.Target2 + origlen);

                    wrapper.Add(inst);
                }
                finally
                {
                    i++;
                }
            }
        }

        private void TPRPMaker()
        {
            try
            {
                int minArgc = 0;
                int minLocalC = 0;
                TPRP tprp = (TPRP)wrapper.SiblingResource(TPRP.TPRPtype); // find TPRP for this BHAV

                wrapper.Package.BeginUpdate();

                if (tprp != null && tprp.TextOnly)
                {
                    // if it exists but is unreadable, as if user wants to overwrite
                    SimPe.DialogResult dr = SimPe.Scenegraph.Compat.MessageBox.ShowAsync(
                        pjse.Localization.GetString("ml_overwriteduff")
                        , btnTPRPMaker.Content?.ToString()
                        , MessageBoxButtons.OKCancel
                        , MessageBoxIcon.Warning).GetAwaiter().GetResult();
                    if (dr != SimPe.DialogResult.OK)
                        return;
                    wrapper.Package.Remove(tprp.FileDescriptor);
                    tprp = null;
                }
                if (tprp != null)
                {
                    // if it exists ask if user wants to preserve content
                    SimPe.DialogResult dr = SimPe.Scenegraph.Compat.MessageBox.ShowAsync(
                        pjse.Localization.GetString("ml_keeplabels")
                        , btnTPRPMaker.Content?.ToString()
                        , MessageBoxButtons.YesNoCancel
                        , MessageBoxIcon.Warning).GetAwaiter().GetResult();
                    if (dr == SimPe.DialogResult.Cancel)
                        return;

                    if (!tprp.Package.Equals(wrapper.Package))
                    {
                        // Clone the original into this package
                        if (dr == SimPe.DialogResult.Yes) Wait.MaxProgress = tprp.Count;
                        SimPe.Interfaces.Files.IPackedFileDescriptor npfd = newPFD(tprp.FileDescriptor);
                        TPRP ntprp = new TPRP();
                        ntprp.FileDescriptor = npfd;
                        wrapper.Package.Add(npfd, true);
                        if (dr == SimPe.DialogResult.Yes) foreach (TPRPItem item in tprp) { ntprp.Add(item.Clone()); Wait.Progress++; }
                        tprp = ntprp;
                        tprp.SynchronizeUserData();
                        Wait.MaxProgress = 0;
                    }

                    if (dr == SimPe.DialogResult.Yes)
                    {
                        minArgc = tprp.ParamCount;
                        minLocalC = tprp.LocalCount;
                    }
                    else
                        tprp.Clear();
                }
                else
                {
                    // create a new TPRP file
                    tprp = new TPRP();
                    tprp.FileDescriptor =
                        newPFD(TPRP.TPRPtype, wrapper.FileDescriptor.Group, wrapper.FileDescriptor.SubType, wrapper.FileDescriptor.Instance);
                    wrapper.Package.Add(tprp.FileDescriptor, true);
                    tprp.SynchronizeUserData();
                }

                Wait.MaxProgress = wrapper.Header.ArgumentCount - minArgc + wrapper.Header.LocalVarCount - minLocalC;
                tprp.FileName = wrapper.FileName;

                for (int arg = minArgc; arg < wrapper.Header.ArgumentCount; arg++)
                {
                    tprp.Add(new TPRPParamLabel(tprp));
                    tprp[false, tprp.ParamCount - 1].LabelCompat = BhavWiz.dnParam() + " " + arg.ToString();
                    Wait.Progress++;
                }
                for (int local = minLocalC; local < wrapper.Header.LocalVarCount; local++)
                {
                    tprp.Add(new TPRPLocalLabel(tprp));
                    tprp[true, tprp.LocalCount - 1].LabelCompat = BhavWiz.dnLocal() + " " + local.ToString();
                    Wait.Progress++;
                }
                tprp.SynchronizeUserData();
                wrapper.Package.EndUpdate();
            }
            finally
            {
                Wait.SubStop();
            }
            SimPe.Scenegraph.Compat.MessageBox.ShowAsync( pjse.Localization.GetString("ml_done"), btnTPRPMaker.Content?.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information).GetAwaiter().GetResult();
        }

        private void TPFWMaker() // Fuck
        {
            try
            {
                SimPe.Plugin.TreesPackedFileWrapper tpfw = (SimPe.Plugin.TreesPackedFileWrapper)wrapper.SiblingResource(0x54524545); // find TPFW for this BHAV
                if (tpfw != null) return;
                tpfw = new SimPe.Plugin.TreesPackedFileWrapper();
                tpfw.FileDescriptor = newPFD(0x54524545, wrapper.FileDescriptor.Group, wrapper.FileDescriptor.SubType, wrapper.FileDescriptor.Instance);
                tpfw.Count = 0;
                tpfw.FileNam = wrapper.FileName;
                for (int i = 0; i < wrapper.Count; i++)
                {
                    tpfw.AddBlock();
                }
                wrapper.Package.Add(tpfw.FileDescriptor, true);
                tpfw.SynchronizeUserData();
                pjse_banner1.TreeVisible = true;
                button1.IsEnabled = false;
                SimPe.Scenegraph.Compat.MessageBox.ShowAsync(pjse.Localization.GetString("ml_done"), "comments", MessageBoxButtons.OK, MessageBoxIcon.Information).GetAwaiter().GetResult();
            }
            catch { }
        }

        private void SetComments()
        {
            if (!UserVerification.HaveValidUserId || Helper.XmlRegistry.HiddenMode) return;
            SimPe.Plugin.TreesPackedFileWrapper tpfw = (SimPe.Plugin.TreesPackedFileWrapper)wrapper.SiblingResource(0x54524545);
            if (tpfw == null) return;
            int indx = 0;
            BhavInstListItemUI cc;
            foreach (Control LI in this.pnflowcontainer.Controls)
            {
                if (LI.GetType() == typeof(BhavInstListItemUI))
                {
                    cc = LI as BhavInstListItemUI;
                    cc.SetComment(tpfw.ReadComment(indx));
                    indx++;
                }
            }
        }

		private short OpsToShort(byte lo, byte hi)
		{
			ushort uval = (ushort)(lo + (hi << 8));
			if (uval > 32767) return (short)(uval - 65536);
			else return (short)uval;
		}

		private byte[] ShortToOps(short val)
		{
			byte[] ops = new byte[2];
			ushort uval;
			if (val < 0)
				uval = (ushort)(65536 + val);
			else
				uval = (ushort)val;
			ops[0] = (byte)(uval & 0xFF);
			ops[1] = (byte)((uval >> 8) & 0xFF);
			return ops;
		}

		private bool cbHex16_IsValid(object sender)
		{
			if (alHex16cb.IndexOf(sender) < 0)
				throw new Exception("cbHex16_IsValid not applicable to control " + sender.ToString());
			if (((ComboBoxCompat)sender).Items.IndexOf(((ComboBoxCompat)sender).Text) != -1) return true;

			try { Convert.ToUInt16(((ComboBoxCompat)sender).Text, 16); }
			catch (Exception) { return false; }
			return true;
		}

		private bool dec8_IsValid(object sender)
		{
			if (alDec8.IndexOf(sender) < 0)
				throw new Exception("dec8_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToByte(((TextBoxCompat)sender).Text); }
			catch (Exception) { return false; }
			return true;
		}

		private bool hex8_IsValid(object sender)
		{
			if (alHex8.IndexOf(sender) < 0)
				throw new Exception("hex8_IsValid not applicable to control " + sender.ToString());
			try { Convert.ToByte(((TextBoxCompat)sender).Text, 16); }
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

		private void FiletableRefresh(object sender, System.EventArgs e)
		{
            pjse_banner1.SiblingEnabled = wrapper != null && wrapper.SiblingResource(TPRP.TPRPtype) != null;
            UpdateInstPanel();
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
				return this;
			}
		}

		/// <summary>
		/// Called by the AbstractWrapper when the file should be displayed to the user.
		/// </summary>
		/// <param name="wrp">Reference to the wrapper to be displayed.</param>
		public void UpdateGUI(IFileWrapper wrp) // Fuck
		{
			wrapper = (Bhav) wrp;

			internalchg = true;
            this.tbLines.Text = "0x0001";
			internalchg = false;

			this.WrapperChanged(wrapper, null);
            pjse_banner1.SiblingEnabled = wrapper.SiblingResource(TPRP.TPRPtype) != null;

			currentInst = null;
			origInst = null;
			UpdateInstPanel();
			this.pnflowcontainer.UpdateGUI(wrapper);
			// pnflowcontainer to install its handler before us.
			if (!setHandler)
			{
				wrapper.WrapperChanged += (s, e) => this.WrapperChanged(s, e);
                wrapper.FileDescriptor.DescriptionChanged += new EventHandler(FileDescriptor_DescriptionChanged);
				setHandler = true;
			}

            if (this.isPopup)
            {
                currentPackage = pjse.FileTable.GFT.CurrentPackage;
                pjse_banner1.TreeVisible = pjse_banner1.ViewVisible = pjse_banner1.FloatVisible = false;
                btnClose.IsVisible = gbSpecial.IsVisible = true;
                button1.IsEnabled = cbSpecial.IsEnabled = false;
                btnCopyBHAV.IsVisible = (currentPackage != wrapper.Package);
                btnImportBHAV.IsVisible = (currentPackage != wrapper.Package)
                    && (callerID != null && callerID.IndexOf("-FFFFFFFF-") == 17); //42484156-00000000-FFFFFFFF-00001003
                btnCopyBHAV.IsEnabled = currentPackage != null;
                btnImportBHAV.IsEnabled = (currentPackage != null) &&
                    ((wrapper.FileDescriptor.Instance >= 0x100 && wrapper.FileDescriptor.Instance < 0x1000)
                    || (wrapper.FileDescriptor.Instance >= 0x2000 && wrapper.FileDescriptor.Instance < 0x3000));

                handleOverride();

                if (this.VisualRoot is Window __win0) __win0.Title = formTitle;
                ttBhavForm.SetToolTip(tbFilename, null);
            }
            else
            {
                this.lbHidesOP.IsVisible = this.tbHidesOP.IsVisible = this.llHidesOP.IsVisible = false;
                this.llHidesOP.Tag = null;
                if (wrapper.SiblingResource(0x54524545) != null && UserVerification.HaveValidUserId && !Helper.XmlRegistry.HiddenMode)
                {
                    pjse_banner1.TreeVisible = true;
                    pjse_banner1.TreeEnabled = wrapper.SiblingResource(0x54524545).Package == wrapper.Package;
                    button1.IsEnabled = false;
                }
                else
                {
                    pjse_banner1.TreeVisible = false;
                    button1.IsEnabled = true;
                }
                currentPackage = wrapper.Package;
                ttBhavForm.SetToolTip(tbFilename, expName + ": 0x" + SimPe.Helper.HexString((ushort)wrapper.FileDescriptor.Instance));
            }
            SetComments();
        }

        void FileDescriptor_DescriptionChanged(object sender, EventArgs e)
        {
            pjse_banner1.SiblingEnabled = wrapper.SiblingResource(TPRP.TPRPtype) != null;
            if (isPopup)
            {
                if (this.VisualRoot is Window __win1) __win1.Title = formTitle;
            }
            else
            {
                ttBhavForm.SetToolTip(tbFilename, expName + ": 0x" + SimPe.Helper.HexString((ushort)wrapper.FileDescriptor.Instance));
                pjse_banner1.TreeVisible = (wrapper.SiblingResource(0x54524545) != null && UserVerification.HaveValidUserId && !Helper.XmlRegistry.HiddenMode);
            }
            SetComments();
        }

        private void WrapperChanged(object sender, System.EventArgs e)
        {
            if (isPopup) wrapper.Changed = false;

            this.btnCommit.IsEnabled = wrapper.Changed;

            // Handler for header
            if (sender == wrapper && !internalchg)
            {
                internalchg = true;
                /*this.Text = */
                tbFilename.Text = wrapper.FileName;
                // cbFormat.Content = "0x" + Helper.HexString(wrapper.Header.Format); // TODO: set selected item
                tbType.Text = "0x" + Helper.HexString(wrapper.Header.Type);
                tbArgC.Text = "0x" + Helper.HexString(wrapper.Header.ArgumentCount);
                tbLocalC.Text = "0x" + Helper.HexString(wrapper.Header.LocalVarCount);
                tbHeaderFlag.Text = "0x" + Helper.HexString(wrapper.Header.HeaderFlag);
                tbTreeVersion.Text = "0x" + Helper.HexString(wrapper.Header.TreeVersion);
                tbCacheFlags.Text = "0x" + Helper.HexString(wrapper.Header.CacheFlags);
                tbCacheFlags.IsEnabled = (wrapper.Header.Format > 0x8008);
                cmpBHAV.Wrapper = wrapper;
                cmpBHAV.WrapperName = wrapper.FileName;
                internalchg = false;
            }

            // Handler for current instruction
            if (currentInst != null && sender == currentInst.Instruction)
            {
                if (internalchg)
                    this.btnCancel.IsEnabled = true;
                else
                    pnflowcontainer_SelectedInstChanged(null, null);
            }
            SetComments();
        }

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();

			// ── Create controls ──
			this.gbInstruction = new Canvas();
			this.btnZero = new ButtonCompat();
			this.tbInst_Longname = new TextBoxCompat();
			this.btnOperandRaw = new ButtonCompat();
			this.btnCancel = new ButtonCompat();
			this.btnOperandWiz = new ButtonCompat();
			this.llopenbhav = new LinkLabel();
			this.tba2 = new ComboBoxCompat();
			this.tba1 = new ComboBoxCompat();
			this.label13 = new LabelCompat();
			this.tbInst_Unk7 = new TextBoxCompat();
			this.tbInst_Unk6 = new TextBoxCompat();
			this.tbInst_Unk5 = new TextBoxCompat();
			this.tbInst_Unk4 = new TextBoxCompat();
			this.tbInst_Unk3 = new TextBoxCompat();
			this.tbInst_Unk2 = new TextBoxCompat();
			this.tbInst_Unk1 = new TextBoxCompat();
			this.tbInst_Unk0 = new TextBoxCompat();
			this.tbInst_Op7 = new TextBoxCompat();
			this.tbInst_Op6 = new TextBoxCompat();
			this.tbInst_Op5 = new TextBoxCompat();
			this.tbInst_Op4 = new TextBoxCompat();
			this.tbInst_Op3 = new TextBoxCompat();
			this.tbInst_Op2 = new TextBoxCompat();
			this.tbInst_Op1 = new TextBoxCompat();
			this.tbInst_Op0 = new TextBoxCompat();
			this.tbInst_NodeVersion = new TextBoxCompat();
			this.tbInst_OpCode = new TextBoxCompat();
			this.label10 = new LabelCompat();
			this.label9 = new LabelCompat();
			this.label12 = new LabelCompat();
			this.label11 = new LabelCompat();
			this.btnOpCode = new ButtonCompat();
			this.tbFilename = new TextBoxCompat();
			this.lbFilename = new LabelCompat();
			this.tbLocalC = new TextBoxCompat();
			this.tbArgC = new TextBoxCompat();
			this.tbType = new TextBoxCompat();
			this.lbTreeVersion = new LabelCompat();
			this.lbType = new LabelCompat();
			this.lbLocalC = new LabelCompat();
			this.lbArgC = new LabelCompat();
			this.lbFormat = new LabelCompat();
			this.bhavPanel = new DockPanel { LastChildFill = true };
			this.pjse_banner1 = new pjse.pjse_banner();
			this.lbHidesOP = new LabelCompat();
			this.gbSpecial = new Canvas();
			this.button1 = new ButtonCompat();
			this.cmpBHAV = new pjse.CompareButton();
			this.btnPasteListing = new ButtonCompat();
			this.btnAppend = new ButtonCompat();
			this.btnInsTrue = new ButtonCompat();
			this.btnInsFalse = new ButtonCompat();
			this.btnDelPescado = new ButtonCompat();
			this.btnLinkInge = new ButtonCompat();
			this.btnGUIDIndex = new ButtonCompat();
			this.btnInsUnlinked = new ButtonCompat();
			this.btnDelMerola = new ButtonCompat();
			this.btnCopyListing = new ButtonCompat();
			this.btnTPRPMaker = new ButtonCompat();
			this.llHidesOP = new LinkLabel();
			this.tbHidesOP = new TextBoxCompat();
			this.cbSpecial = new CheckBoxCompat2();
			this.btnImportBHAV = new ButtonCompat();
			this.btnCopyBHAV = new ButtonCompat();
			this.btnClose = new ButtonCompat();
			this.tbHeaderFlag = new TextBoxCompat();
			this.lbHeaderFlag = new LabelCompat();
			this.tbCacheFlags = new TextBoxCompat();
			this.cbFormat = new ComboBoxCompat();
			this.pnflowcontainer = new SimPe.PackedFiles.UserInterface.BhavInstListControl();
			this.btnDel = new ButtonCompat();
			this.gbMove = new Canvas();
			this.btnUp = new ButtonCompat();
			this.btnDown = new ButtonCompat();
			this.lbUpDown = new LabelCompat();
			this.tbLines = new TextBoxCompat();
			this.btnSort = new ButtonCompat();
			this.btnCommit = new ButtonCompat();
			this.tbTreeVersion = new TextBoxCompat();
			this.btnAdd = new ButtonCompat();
			this.lbCacheFlags = new LabelCompat();
			this.cmenuGUIDIndex = new ContextMenuStrip();
			this.createAllPackagesToolStripMenuItem = new ToolStripMenuItem();
			this.createCurrentPackageToolStripMenuItem = new ToolStripMenuItem();
			this.loadIndexToolStripMenuItem = new ToolStripMenuItem();
			this.defaultFileToolStripMenuItem = new ToolStripMenuItem();
			this.fromFileToolStripMenuItem = new ToolStripMenuItem();
			this.saveIndexToolStripMenuItem = new ToolStripMenuItem();
			this.defaultFileToolStripMenuItem1 = new ToolStripMenuItem();
			this.toFileToolStripMenuItem = new ToolStripMenuItem();
			this.ttBhavForm = new SimPe.Scenegraph.Compat.ToolTip();

			// ── Set text, items, read-only flags ──
			this.lbFilename.Content = "Filename";
			this.lbFormat.Content = "Format";
			this.lbType.Content = "Tree Type";
			this.lbArgC.Content = "Arg Count";
			this.lbLocalC.Content = "Local Var Count";
			this.lbTreeVersion.Content = "Tree Version";
			this.lbHeaderFlag.Content = "Header Flag";
			this.lbCacheFlags.Content = "Cache flags";
			this.lbHidesOP.Content = "Displayed BHAV is from:";
			this.label9.Content = "OpCode:";
			this.label10.Content = "Node Version:";
			this.label11.Content = "True Target:";
			this.label12.Content = "False Target:";
			this.label13.Content = "Operands:";
			this.lbUpDown.Content = "lines";

			this.btnCommit.Content = "Commit File";
			this.btnSort.Content = "Sort";
			this.btnAdd.Content = "Add";
			this.btnDel.Content = "Delete";
			// '^' is shorter than 'v'; wrap in a TextBlock with explicit FontSize/LineHeight
			// so just this button gets bigger text to visually balance the two arrows.
			this.btnUp.Content = new Avalonia.Controls.TextBlock
			{
				Text = "^",
				FontSize = 16,
				LineHeight = 18,
				VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
				HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
			};
			this.btnDown.Content = "v";
			this.btnCancel.Content = "Cancel";
			this.btnClose.Content = "Close";
			this.btnImportBHAV.Content = "Import as Private";
			this.btnCopyBHAV.Content = "Import unchanged";
			this.btnOpCode.Content = "▶";
			this.btnOperandWiz.Content = "Wiz";
			this.btnOperandRaw.Content = "Raw";
			this.btnZero.Content = "Zero";

			this.button1.Content = "Comments";
			this.cmpBHAV.Content = "Compare";
			this.btnPasteListing.Content = "Paste";
			this.btnAppend.Content = "Append BHAV";
			this.btnInsTrue.Content = "Ins/true";
			this.btnInsFalse.Content = "Ins/false";
			this.btnDelPescado.Content = "Pescado's Delete";
			this.btnLinkInge.Content = "Inge's InitLinker";
			this.btnGUIDIndex.Content = "GUIDs";
			this.btnInsUnlinked.Content = "Insert unlinked";
			this.btnDelMerola.Content = "Delete to end";
			this.btnCopyListing.Content = "Copy";
			this.btnTPRPMaker.Content = "Labels";

			this.cbSpecial.Content = "Special buttons";
			this.llopenbhav.Content = "view BHAV";
			this.llHidesOP.Content = "BHAV from {0} overridden. View original";

			this.tba1.Items.Add("Error");
			this.tba1.Items.Add("Return True");
			this.tba1.Items.Add("Return False");
			this.tba2.Items.Add("Error");
			this.tba2.Items.Add("Return True");
			this.tba2.Items.Add("Return False");
			this.cbFormat.Items.Add("0x8000");
			this.cbFormat.Items.Add("0x8001");
			this.cbFormat.Items.Add("0x8002");
			this.cbFormat.Items.Add("0x8003");
			this.cbFormat.Items.Add("0x8004");
			this.cbFormat.Items.Add("0x8005");
			this.cbFormat.Items.Add("0x8006");
			this.cbFormat.Items.Add("0x8007");
			this.cbFormat.Items.Add("0x8008");
			this.cbFormat.Items.Add("0x8009");

			this.tbInst_Longname.AcceptsReturn = true;
			this.tbInst_Longname.TextWrapping = Avalonia.Media.TextWrapping.Wrap;
			this.tbInst_Longname.IsReadOnly = true;
			this.tbHidesOP.IsReadOnly = true;

			this.ttBhavForm.SetToolTip(this.btnZero, "Set all operands to zero");
			this.ttBhavForm.SetToolTip(this.btnOperandWiz, "Pop-up Wizard");
			this.ttBhavForm.SetToolTip(this.btnOperandRaw, "Pop-up raw entry box");
			this.ttBhavForm.SetToolTip(this.tbInst_Longname, "Click and drag to select text for copying");

			this.pjse_banner1.ExtractVisible = true;
			this.pjse_banner1.FloatVisible = true;
			this.pjse_banner1.SiblingVisible = true;
			this.pjse_banner1.ViewVisible = true;
			this.pjse_banner1.SiblingText = "TPRP";
			this.pjse_banner1.TitleText = "Behaviour Function";

			this.cmpBHAV.Wrapper = null;
			this.cmpBHAV.WrapperName = null;
			this.pnflowcontainer.SelectedIndex = -1;

			// ── Helpers ──
			static void Place(Avalonia.Controls.Canvas parent, Avalonia.Controls.Control c, double x, double y, double w, double h)
			{
				Avalonia.Controls.Canvas.SetLeft(c, x);
				Avalonia.Controls.Canvas.SetTop(c, y);
				c.Width = w;
				c.Height = h;
				parent.Children.Add(c);
			}

			static void SetSize(Avalonia.Controls.Control c, double w, double h)
			{
				c.Width = w;
				c.Height = h;
			}

			static Avalonia.Controls.Canvas BuildGroupBox(Avalonia.Controls.Canvas wrapper, string header, double w, double h)
			{
				wrapper.Width = w;
				wrapper.Height = h;

				var inner = new Avalonia.Controls.Canvas { Width = w, Height = h };
				var border = new Avalonia.Controls.Border
				{
					BorderBrush = Avalonia.Media.Brushes.Gray,
					BorderThickness = new Avalonia.Thickness(1),
					CornerRadius = new Avalonia.CornerRadius(3),
					Child = inner,
					Width = w,
					Height = h - 6,
				};
				Avalonia.Controls.Canvas.SetLeft(border, 0);
				Avalonia.Controls.Canvas.SetTop(border, 6);

				var hdr = new Avalonia.Controls.Border
				{
					Background = Avalonia.Media.Brushes.White,
					Padding = new Avalonia.Thickness(4, 0),
					Child = new Avalonia.Controls.TextBlock { Text = header, FontSize = 11 },
				};
				Avalonia.Controls.Canvas.SetLeft(hdr, 8);
				Avalonia.Controls.Canvas.SetTop(hdr, 0);

				wrapper.Children.Add(border);
				wrapper.Children.Add(hdr);
				return inner;
			}

			// ── gbInstruction contents (420×190 — widened to fit 24-wide operand textboxes) ──
			var gbInstCanvas = BuildGroupBox(this.gbInstruction, "Instruction Settings", 420, 190);
			Place(gbInstCanvas, this.label9, 27, 25, 56, 13);
			Place(gbInstCanvas, this.tbInst_OpCode, 83, 23, 48, 20);
			Place(gbInstCanvas, this.btnOpCode, 130, 24, 17, 16);
			Place(gbInstCanvas, this.llopenbhav, 149, 25, 75, 18);
			Place(gbInstCanvas, this.label10, 232, 25, 74, 13);
			Place(gbInstCanvas, this.tbInst_NodeVersion, 315, 23, 40, 20);
			Place(gbInstCanvas, this.label11, 9, 48, 66, 13);
			Place(gbInstCanvas, this.tba1, 83, 45, 84, 21);
			Place(gbInstCanvas, this.label12, 174, 48, 69, 13);
			Place(gbInstCanvas, this.tba2, 252, 45, 84, 21);
			Place(gbInstCanvas, this.btnCancel, 276, 68, 54, 19);
			Place(gbInstCanvas, this.label13, 19, 72, 56, 13);
			Place(gbInstCanvas, this.tbInst_Op0,  83, 69, 24, 20);
			Place(gbInstCanvas, this.tbInst_Op1, 107, 69, 24, 20);
			Place(gbInstCanvas, this.tbInst_Op2, 131, 69, 24, 20);
			Place(gbInstCanvas, this.tbInst_Op3, 155, 69, 24, 20);
			Place(gbInstCanvas, this.tbInst_Op4, 179, 69, 24, 20);
			Place(gbInstCanvas, this.tbInst_Op5, 203, 69, 24, 20);
			Place(gbInstCanvas, this.tbInst_Op6, 227, 69, 24, 20);
			Place(gbInstCanvas, this.tbInst_Op7, 251, 69, 24, 20);
			Place(gbInstCanvas, this.tbInst_Unk0,  83, 88, 24, 20);
			Place(gbInstCanvas, this.tbInst_Unk1, 107, 88, 24, 20);
			Place(gbInstCanvas, this.tbInst_Unk2, 131, 88, 24, 20);
			Place(gbInstCanvas, this.tbInst_Unk3, 155, 88, 24, 20);
			Place(gbInstCanvas, this.tbInst_Unk4, 179, 88, 24, 20);
			Place(gbInstCanvas, this.tbInst_Unk5, 203, 88, 24, 20);
			Place(gbInstCanvas, this.tbInst_Unk6, 227, 88, 24, 20);
			Place(gbInstCanvas, this.tbInst_Unk7, 251, 88, 24, 20);
			Place(gbInstCanvas, this.btnOperandWiz, 278, 87, 40, 22);
			Place(gbInstCanvas, this.btnOperandRaw, 322, 87, 40, 22);
			Place(gbInstCanvas, this.btnZero,       366, 87, 40, 22);
			Place(gbInstCanvas, this.tbInst_Longname, 8, 110, 348, 76);

			// ── gbMove contents (widened to fit readable arrows) ──
			var gbMoveCanvas = BuildGroupBox(this.gbMove, "Move", 125, 55);
			Place(gbMoveCanvas, this.btnUp,    8, 11, 22, 20);
			Place(gbMoveCanvas, this.btnDown,  8, 32, 22, 20);
			Place(gbMoveCanvas, this.tbLines, 34, 21, 54, 20);
			Place(gbMoveCanvas, this.lbUpDown, 92, 24, 28, 13);

			// ── gbSpecial contents ──
			var gbSpecialCanvas = BuildGroupBox(this.gbSpecial, "Special buttons", 341, 86);
			Place(gbSpecialCanvas, this.btnCopyListing, 4, 17, 48, 18);
			Place(gbSpecialCanvas, this.btnPasteListing, 56, 17, 48, 18);
			Place(gbSpecialCanvas, this.btnInsTrue, 108, 17, 60, 18);
			Place(gbSpecialCanvas, this.btnInsFalse, 172, 17, 60, 18);
			Place(gbSpecialCanvas, this.btnTPRPMaker, 236, 17, 48, 18);
			Place(gbSpecialCanvas, this.btnGUIDIndex, 288, 17, 48, 18);
			Place(gbSpecialCanvas, this.btnLinkInge, 4, 39, 100, 18);
			Place(gbSpecialCanvas, this.btnInsUnlinked, 108, 39, 124, 18);
			Place(gbSpecialCanvas, this.btnAppend, 236, 39, 100, 18);
			Place(gbSpecialCanvas, this.btnDelPescado, 4, 61, 100, 18);
			Place(gbSpecialCanvas, this.btnDelMerola, 107, 61, 89, 18);
			Place(gbSpecialCanvas, this.cmpBHAV, 199, 61, 73, 18);
			Place(gbSpecialCanvas, this.button1, 275, 61, 60, 18);

			// ── Right-side control strip: a vertical stack of self-sizing rows.
			//    Each row flows naturally, so Avalonia's layout engine can compress/scroll
			//    gracefully when vertical space is tight — no Canvas-overflow hazard. ──

			// Row 1: Commit File | Arg Count | Local Var Count — all left-aligned
			SetSize(this.btnCommit, 99, 22);
			SetSize(this.tbArgC, 40, 20);
			SetSize(this.tbLocalC, 40, 20);
			this.lbArgC.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
			this.lbLocalC.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
			this.lbArgC.Margin = new Avalonia.Thickness(12, 0, 4, 0);
			this.lbLocalC.Margin = new Avalonia.Thickness(8, 0, 4, 0);
			var commitRow = new Avalonia.Controls.StackPanel
			{
				Orientation = Avalonia.Layout.Orientation.Horizontal,
				VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
				Margin = new Avalonia.Thickness(0, 0, 0, 6),
			};
			commitRow.Children.Add(this.btnCommit);
			commitRow.Children.Add(this.lbArgC);
			commitRow.Children.Add(this.tbArgC);
			commitRow.Children.Add(this.lbLocalC);
			commitRow.Children.Add(this.tbLocalC);

			// Row 3: Sort | Move groupbox | Add/Del stacked | Special-buttons checkbox
			// Use MinWidth so Avalonia's button chrome doesn't truncate the text
			this.btnSort.MinWidth = 52;
			this.btnAdd.MinWidth = 60;
			this.btnDel.MinWidth = 60;
			this.btnAdd.MinHeight = 22;
			this.btnDel.MinHeight = 22;
			this.btnSort.Margin = new Avalonia.Thickness(0, 14, 8, 0);
			this.btnAdd.Margin = new Avalonia.Thickness(0, 0, 0, 2);
			this.cbSpecial.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
			this.cbSpecial.Margin = new Avalonia.Thickness(8, 0, 0, 0);
			var addDel = new Avalonia.Controls.StackPanel
			{
				Orientation = Avalonia.Layout.Orientation.Vertical,
				VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
				Margin = new Avalonia.Thickness(8, 0, 0, 0),
			};
			addDel.Children.Add(this.btnAdd);
			addDel.Children.Add(this.btnDel);
			var moveRow = new Avalonia.Controls.StackPanel
			{
				Orientation = Avalonia.Layout.Orientation.Horizontal,
				VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top,
				Margin = new Avalonia.Thickness(0, 4, 0, 6),
			};
			moveRow.Children.Add(this.btnSort);
			moveRow.Children.Add(this.gbMove);
			moveRow.Children.Add(addDel);
			moveRow.Children.Add(this.cbSpecial);

			// Row 5: override display (llHidesOP / lbHidesOP / tbHidesOP stacked vertically)
			var overrideBlock = new Avalonia.Controls.StackPanel
			{
				Orientation = Avalonia.Layout.Orientation.Vertical,
				Margin = new Avalonia.Thickness(0, 6, 0, 0),
			};
			this.tbHidesOP.Height = 60;
			overrideBlock.Children.Add(this.llHidesOP);
			overrideBlock.Children.Add(this.lbHidesOP);
			overrideBlock.Children.Add(this.tbHidesOP);

			// Compose the stack
			var rightStack = new Avalonia.Controls.StackPanel
			{
				Orientation = Avalonia.Layout.Orientation.Vertical,
				Margin = new Avalonia.Thickness(4, 0, 4, 4),
			};
			rightStack.Children.Add(commitRow);
			rightStack.Children.Add(this.gbInstruction);
			rightStack.Children.Add(moveRow);
			rightStack.Children.Add(this.gbSpecial);
			rightStack.Children.Add(overrideBlock);

			// Wrap in ScrollViewer so short tab heights scroll rather than overflow
			var rightPanel = new Avalonia.Controls.ScrollViewer
			{
				Content = rightStack,
				Width = 430,
				HorizontalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Disabled,
				VerticalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Auto,
			};

			// ── Popup-button row (docked bottom on bhavPanel, right-aligned) ──
			SetSize(this.btnImportBHAV, 112, 21);
			SetSize(this.btnCopyBHAV, 112, 21);
			SetSize(this.btnClose, 71, 21);
			var popupButtonRow = new Avalonia.Controls.StackPanel
			{
				Orientation = Avalonia.Layout.Orientation.Horizontal,
				HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
				Spacing = 4,
				Margin = new Avalonia.Thickness(4, 2, 4, 4),
			};
			popupButtonRow.Children.Add(this.btnImportBHAV);
			popupButtonRow.Children.Add(this.btnCopyBHAV);
			popupButtonRow.Children.Add(this.btnClose);

			// ── Banner (docked top, natural height) ──
			DockPanel.SetDock(this.pjse_banner1, Dock.Top);
			this.bhavPanel.Children.Add(this.pjse_banner1);

			// ── Header row (docked top): Filename on left, rest right-aligned ──
			this.lbFilename.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
			this.lbFormat.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
			this.lbType.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
			this.lbHeaderFlag.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
			this.lbTreeVersion.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
			this.lbCacheFlags.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;

			SetSize(this.cbFormat, 66, 21);
			SetSize(this.tbType, 40, 20);
			SetSize(this.tbHeaderFlag, 40, 20);
			SetSize(this.tbTreeVersion, 80, 20);
			SetSize(this.tbCacheFlags, 40, 20);
			this.tbFilename.MinWidth = 100;

			this.lbFormat.Margin = new Avalonia.Thickness(8, 0, 2, 0);
			this.lbType.Margin = new Avalonia.Thickness(8, 0, 2, 0);
			this.lbHeaderFlag.Margin = new Avalonia.Thickness(8, 0, 2, 0);
			this.lbTreeVersion.Margin = new Avalonia.Thickness(8, 0, 2, 0);
			this.lbCacheFlags.Margin = new Avalonia.Thickness(8, 0, 2, 0);

			var headerRight = new Avalonia.Controls.StackPanel
			{
				Orientation = Avalonia.Layout.Orientation.Horizontal,
				VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
			};
			headerRight.Children.Add(this.lbFormat);
			headerRight.Children.Add(this.cbFormat);
			headerRight.Children.Add(this.lbType);
			headerRight.Children.Add(this.tbType);
			headerRight.Children.Add(this.lbHeaderFlag);
			headerRight.Children.Add(this.tbHeaderFlag);
			headerRight.Children.Add(this.lbTreeVersion);
			headerRight.Children.Add(this.tbTreeVersion);
			headerRight.Children.Add(this.lbCacheFlags);
			headerRight.Children.Add(this.tbCacheFlags);

			var headerRow = new DockPanel { Margin = new Avalonia.Thickness(4, 4, 4, 2) };
			DockPanel.SetDock(this.lbFilename, Dock.Left);
			headerRow.Children.Add(this.lbFilename);
			DockPanel.SetDock(headerRight, Dock.Right);
			headerRow.Children.Add(headerRight);
			this.tbFilename.Margin = new Avalonia.Thickness(4, 0);
			headerRow.Children.Add(this.tbFilename);

			DockPanel.SetDock(headerRow, Dock.Top);
			this.bhavPanel.Children.Add(headerRow);

			// ── Popup-button row docked bottom (before right panel so it spans full width at bottom) ──
			DockPanel.SetDock(popupButtonRow, Dock.Bottom);
			this.bhavPanel.Children.Add(popupButtonRow);

			// ── Right control panel docked right ──
			DockPanel.SetDock(rightPanel, Dock.Right);
			this.bhavPanel.Children.Add(rightPanel);

			// ── Instruction list fills remaining area ──
			this.pnflowcontainer.Margin = new Avalonia.Thickness(4, 0, 4, 4);
			this.bhavPanel.Children.Add(this.pnflowcontainer);

			// ── Context menu wiring ──
			this.loadIndexToolStripMenuItem.DropDownItems.AddRange(new object[]
			{
				this.defaultFileToolStripMenuItem,
				this.fromFileToolStripMenuItem,
			});
			this.saveIndexToolStripMenuItem.DropDownItems.AddRange(new object[]
			{
				this.defaultFileToolStripMenuItem1,
				this.toFileToolStripMenuItem,
			});

			// ── Event wiring ──
			this.btnZero.Click += (s, e) => this.btnZero_Click(s, e);
			this.btnOperandRaw.Click += (s, e) => this.btnOperandRaw_Click(s, e);
			this.btnCancel.Click += (s, e) => this.btnCancel_Clicked(s, e);
			this.btnOperandWiz.Click += (s, e) => this.btnOperandWiz_Clicked(s, e);
			this.llopenbhav.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llopenbhav_LinkClicked);

			this.tba2.QueryContinueDrag += (s, e) => this.ItemQueryContinueDragTarget(s, e as SimPe.Scenegraph.Compat.QueryContinueDragEventArgs ?? new SimPe.Scenegraph.Compat.QueryContinueDragEventArgs());
			this.tba2.DragOver += (s, e) => this.ItemDragEnter(s, e as SimPe.Scenegraph.Compat.DragEventArgs ?? new SimPe.Scenegraph.Compat.DragEventArgs());
			this.tba2.SelectionChanged += (s, e) => this.cbHex16_SelectedIndexChanged(s, e);
			this.tba2.GotFocus += (s, e) => this.cbHex16_Enter(s, e);
			this.tba2.DragDrop += (s, e) => this.ItemDrop(s, e as SimPe.Scenegraph.Compat.DragEventArgs ?? new SimPe.Scenegraph.Compat.DragEventArgs());
			this.tba2.DragEnter += (s, e) => this.ItemDragEnter(s, e as SimPe.Scenegraph.Compat.DragEventArgs ?? new SimPe.Scenegraph.Compat.DragEventArgs());
			this.tba2.LostFocus += (s, e) => this.cbHex16_Validated(s, e);
			this.tba2.TextChanged += (s, e) => this.cbHex16_TextChanged(s, e);

			this.tba1.QueryContinueDrag += (s, e) => this.ItemQueryContinueDragTarget(s, e as SimPe.Scenegraph.Compat.QueryContinueDragEventArgs ?? new SimPe.Scenegraph.Compat.QueryContinueDragEventArgs());
			this.tba1.DragOver += (s, e) => this.ItemDragEnter(s, e as SimPe.Scenegraph.Compat.DragEventArgs ?? new SimPe.Scenegraph.Compat.DragEventArgs());
			this.tba1.SelectionChanged += (s, e) => this.cbHex16_SelectedIndexChanged(s, e);
			this.tba1.GotFocus += (s, e) => this.cbHex16_Enter(s, e);
			this.tba1.DragDrop += (s, e) => this.ItemDrop(s, e as SimPe.Scenegraph.Compat.DragEventArgs ?? new SimPe.Scenegraph.Compat.DragEventArgs());
			this.tba1.DragEnter += (s, e) => this.ItemDragEnter(s, e as SimPe.Scenegraph.Compat.DragEventArgs ?? new SimPe.Scenegraph.Compat.DragEventArgs());
			this.tba1.LostFocus += (s, e) => this.cbHex16_Validated(s, e);
			this.tba1.TextChanged += (s, e) => this.cbHex16_TextChanged(s, e);

			TextBoxCompat[] hex8Controls =
			{
				this.tbInst_Unk0, this.tbInst_Unk1, this.tbInst_Unk2, this.tbInst_Unk3,
				this.tbInst_Unk4, this.tbInst_Unk5, this.tbInst_Unk6, this.tbInst_Unk7,
				this.tbInst_Op0, this.tbInst_Op1, this.tbInst_Op2, this.tbInst_Op3,
				this.tbInst_Op4, this.tbInst_Op5, this.tbInst_Op6, this.tbInst_Op7,
				this.tbInst_NodeVersion, this.tbHeaderFlag, this.tbCacheFlags,
				this.tbType, this.tbArgC, this.tbLocalC,
			};
			foreach (var tb in hex8Controls)
			{
				tb.TextChanged += (s, e) => this.hex8_TextChanged(s, e);
				tb.LostFocus += (s, e) => this.hex8_Validated(s, e);
			}

			this.tbInst_OpCode.TextChanged += (s, e) => this.hex16_TextChanged(s, e);
			this.tbInst_OpCode.LostFocus += (s, e) => this.hex16_Validated(s, e);
			this.tbLines.TextChanged += (s, e) => this.hex16_TextChanged(s, e);
			this.tbLines.LostFocus += (s, e) => this.hex16_Validated(s, e);
			this.tbTreeVersion.TextChanged += (s, e) => this.hex32_TextChanged(s, e);
			this.tbTreeVersion.LostFocus += (s, e) => this.hex32_Validated(s, e);

			this.btnOpCode.Click += (s, e) => this.btnOpCode_Clicked(s, e);
			this.tbFilename.TextChanged += (s, e) => this.tbFilename_TextChanged(s, e);
			this.tbFilename.LostFocus += (s, e) => this.tbFilename_Validated(s, e);

			this.pjse_banner1.ExtractClick += (s, e) => this.pjse_banner1_ExtractClick(s, e);
			this.pjse_banner1.TreeClick += (s, e) => this.pjse_banner1_TreeClick(s, e);
			this.pjse_banner1.SiblingClick += (s, e) => this.pjse_banner1_SiblingClick(s, e);
			this.pjse_banner1.ViewClick += (s, e) => this.pjse_banner1_ViewClick(s, e);
			this.pjse_banner1.FloatClick += (s, e) => this.btnFloat_Click(s, e);

			this.button1.Click += (s, e) => this.button1_Click(s, e);
			this.cmpBHAV.CompareWith += new pjse.CompareButton.CompareWithEventHandler(this.cmpBHAV_CompareWith);

			this.btnPasteListing.Click += (s, e) => this.btnPasteListing_Click(s, e);
			this.btnAppend.Click += (s, e) => this.btnAppend_Click(s, e);
			this.btnInsTrue.Click += (s, e) => this.btnInsVia_Click(s, e);
			this.btnInsFalse.Click += (s, e) => this.btnInsVia_Click(s, e);
			this.btnDelPescado.Click += (s, e) => this.btnDelPescado_Click(s, e);
			this.btnLinkInge.Click += (s, e) => this.btnLinkInge_Click(s, e);
			this.btnGUIDIndex.Click += (s, e) => this.btnGUIDIndex_Click(s, e);
			this.btnInsUnlinked.Click += (s, e) => this.btnInsUnlinked_Click(s, e);
			this.btnDelMerola.Click += (s, e) => this.btnDelMerola_Click(s, e);
			this.btnCopyListing.Click += (s, e) => this.btnCopyListing_Click(s, e);
			this.btnTPRPMaker.Click += (s, e) => this.btnTPRPMaker_Click(s, e);

			this.llHidesOP.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llHidesOP_LinkClicked);

			this.cbSpecial.CheckedChanged += (s, e) => this.cbSpecial_CheckStateChanged(s, e);
			this.btnImportBHAV.Click += (s, e) => this.btnImportBHAV_Click(s, e);
			this.btnCopyBHAV.Click += (s, e) => this.btnCopyBHAV_Click(s, e);
			this.btnClose.Click += (s, e) => this.btnClose_Click(s, e);

			this.cbFormat.SelectionChanged += (s, e) => this.cbHex16_SelectedIndexChanged(s, e);
			this.cbFormat.GotFocus += (s, e) => this.cbHex16_Enter(s, e);
			this.cbFormat.LostFocus += (s, e) => this.cbHex16_Validated(s, e);
			this.cbFormat.TextChanged += (s, e) => this.cbHex16_TextChanged(s, e);

			this.pnflowcontainer.SelectedInstChanged += (s, e) => this.pnflowcontainer_SelectedInstChanged(s, e);

			this.btnDel.Click += (s, e) => this.btnDel_Clicked(s, e);
			this.btnUp.Click += (s, e) => this.btnMove_Clicked(s, e);
			this.btnDown.Click += (s, e) => this.btnMove_Clicked(s, e);
			this.btnSort.Click += (s, e) => this.btnSort_Clicked(s, e);
			this.btnCommit.Click += (s, e) => this.btnCommit_Clicked(s, e);
			this.btnAdd.Click += (s, e) => this.btnAdd_Clicked(s, e);

			this.cmenuGUIDIndex.Opening += new System.EventHandler(this.cmenuGUIDIndex_Opening);
			this.createAllPackagesToolStripMenuItem.Click += (s, e) => this.createToolStripMenuItem_Click(s, e);
			this.createCurrentPackageToolStripMenuItem.Click += (s, e) => this.createToolStripMenuItem_Click(s, e);
			this.defaultFileToolStripMenuItem.Click += (s, e) => this.defaultFileToolStripMenuItem_Click(s, e);
			this.fromFileToolStripMenuItem.Click += (s, e) => this.fileToolStripMenuItem_Click(s, e);
			this.defaultFileToolStripMenuItem1.Click += (s, e) => this.defaultFileToolStripMenuItem_Click(s, e);
			this.toFileToolStripMenuItem.Click += (s, e) => this.fileToolStripMenuItem_Click(s, e);

			this.ttBhavForm.ShowAlways = true;

			this.Name = "BhavForm";
			this.Content = this.bhavPanel;
		}
		#endregion

		private void pnflowcontainer_SelectedInstChanged(object sender, System.EventArgs e)
		{
			int index = pnflowcontainer.SelectedIndex;
			if (index < 0 || index >= wrapper.Count)
			{
				currentInst = null;
				origInst = null;
			}
			else
			{
				currentInst = wrapper[index];
				origInst = wrapper[index].Clone();
			}
			UpdateInstPanel();
			this.btnCancel.IsEnabled = false;
		}

		private void ItemQueryContinueDragTarget(object sender, QueryContinueDragEventArgs e)
		{
			if (e.KeyState==0) e.Action = DragAction.Drop;
			else e.Action = DragAction.Continue;
		}

		private void ItemDragEnter(object sender, DragEventArgs e)
		{
			// Drag-and-drop: Avalonia IDataObject has different API
			// if (e.Data.Contains(DataFormats.Text)) e.Effect = DragDropEffects.Link;
		}

		private void ItemDrop(object sender, System.EventArgs e)
		{
			int sel = 0;
			// Drag-and-drop data retrieval not available in this Avalonia port
			// sel = (int)e.Data.GetData(sel.GetType());
			ComboBoxCompat cb = ((ComboBoxCompat)sender);
			cb.SelectedIndex = -1;
			cb.Content = "0x"+Helper.HexString((ushort)sel);
		}

		private void btnCommit_Clicked(object sender, System.EventArgs e)
		{
			try 
			{
				wrapper.SynchronizeUserData();
				btnCommit.IsEnabled = wrapper.Changed;
				pnflowcontainer_SelectedInstChanged(null, null);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(pjse.Localization.GetString("errwritingfile"), ex);
			}			
		}

		private void btnCancel_Clicked(object sender, System.EventArgs e)
		{
			wrapper[pnflowcontainer.SelectedIndex] = origInst.Clone();
			pnflowcontainer_SelectedInstChanged(null, null);
		}

        private void pjse_banner1_SiblingClick(object sender, EventArgs e)
        {
            TPRP tprp = (TPRP)wrapper.SiblingResource(TPRP.TPRPtype);
            if (tprp == null) return;
            if (tprp.Package != wrapper.Package)
            {
                if (SimPe.Scenegraph.Compat.MessageBox.ShowAsync(Localization.GetString("OpenOtherPkg"), pjse_banner1.TitleText, MessageBoxButtons.YesNo).GetAwaiter().GetResult() != SimPe.DialogResult.Yes) return;
            }
            SimPe.RemoteControl.OpenPackedFile(tprp.FileDescriptor, tprp.Package);
        }

        private void pjse_banner1_TreeClick(object sender, EventArgs e) // Fuck
        {
            SimPe.Plugin.TreesPackedFileWrapper tpfw = (SimPe.Plugin.TreesPackedFileWrapper)wrapper.SiblingResource(0x54524545);
            if (tpfw == null) return;

            if (tpfw.Package != wrapper.Package)
            {
                if (SimPe.Scenegraph.Compat.MessageBox.ShowAsync(Localization.GetString("OpenOtherPkg"), pjse_banner1.TitleText, MessageBoxButtons.YesNo).GetAwaiter().GetResult() != SimPe.DialogResult.Yes) return;
            }
            SimPe.RemoteControl.OpenPackedFile(tpfw.FileDescriptor, tpfw.Package);
        }

        private void btnFloat_Click(object sender, EventArgs e)
        {
            Avalonia.Controls.Control old = this.bhavPanel.Parent as Avalonia.Controls.Control;
            string oldFloatText = this.pjse_banner1.FloatText;

            Window f = new Window();
            f.Title = formTitle;
            

            f.Content = this.bhavPanel;
            this.pjse_banner1.FloatText = pjse.Localization.GetString("bhavForm.Unfloat");
            this.pjse_banner1.FloatClick -= new System.EventHandler(this.btnFloat_Click);
            this.pjse_banner1.SetFormCancelButton(f);

            this.gbSpecial.IsVisible = true;
            this.cbSpecial.IsEnabled = false;
            this.btnCopyBHAV.IsVisible = false;

            handleOverride();

            f.ShowDialog(null).GetAwaiter().GetResult();

            // restore bhavPanel to old parent - not directly supported in Avalonia;
            this.pjse_banner1.FloatText = oldFloatText;
            this.pjse_banner1.FloatClick += (s, e) => this.btnFloat_Click(s, e);

            this.gbSpecial.IsVisible = this.cbSpecial.IsChecked == true;
            this.cbSpecial.IsEnabled = true;

            this.lbHidesOP.IsVisible = this.tbHidesOP.IsVisible = this.llHidesOP.IsVisible = false;
            this.llHidesOP.Tag = null;

            // f.Dispose() - no-op in Avalonia

            wrapper.RefreshUI();
        }

        private void pjse_banner1_ViewClick(object sender, EventArgs e)
        {
            common_LinkClicked(pjse.FileTable.GFT[wrapper.Package, wrapper.FileDescriptor][0]);
        }

		private void llopenbhav_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
            common_LinkClicked(wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, currentInst.Instruction.OpCode));
		}

        private void llHidesOP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            common_LinkClicked(wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Instance, (pjse.FileTable.Source)llHidesOP.Tag));
        }

		private void btnClose_Click(object sender, System.EventArgs e)
		{
            if (this.isPopup)
                (this.VisualRoot as Window)?.Close();
		}

        private void btnCopyBHAV_Click(object sender, EventArgs e)
        {
            btnCopyBHAV.IsEnabled = false;
            TakeACopy();
            btnCopyBHAV.Content = pjse.Localization.GetString("ml_done");
        }

        private void btnImportBHAV_Click(object sender, EventArgs e)
        {
            btnImportBHAV.IsEnabled = false;
            ImportBHAV();
            btnImportBHAV.Content = pjse.Localization.GetString("ml_done");
        }

        private void pjse_banner1_ExtractClick(object sender, EventArgs e) { pjse.ExtractCurrent.Execute(wrapper, pjse_banner1.TitleText); }

		private void btnOpCode_Clicked(object sender, System.EventArgs e)
		{
            pjse.FileTable.Entry item = new ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, bhavPanel.Parent as Avalonia.Controls.Control, false);

			if (item != null && item.Instance != currentInst.Instruction.OpCode)
				this.tbInst_OpCode.Text = "0x" + SimPe.Helper.HexString((ushort)item.Instance);
		}

        private void btnOperandWiz_Clicked(object sender, System.EventArgs e) { OperandWiz(1); }
		
		private void btnOperandRaw_Click(object sender, System.EventArgs e) { OperandWiz(0); }

        private void btnZero_Click(object sender, EventArgs e)
        {
            internalchg = true;
            Instruction inst = currentInst.Instruction;
            currentInst = null;
            try
            {
                for (int i = 0; i < 8; i++) inst.Operands[i] = 0;
                for (int i = 0; i < 8; i++) inst.Reserved1[i] = 0;
            }
            finally
            {
                currentInst = inst;
                UpdateInstPanel();
                this.btnCancel.IsEnabled = true;
                internalchg = false;
            }
        }

        private void tbFilename_TextChanged(object sender, System.EventArgs e)
		{
			wrapper.FileName = tbFilename.Text;
		}

		private void tbFilename_Validated(object sender, System.EventArgs e)
		{
			tbFilename.SelectAll();
		}

		private void cbHex16_Enter(object sender, System.EventArgs e)
		{
			((ComboBoxCompat)sender).SelectAll();
		}

		private void cbHex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!cbHex16_IsValid(sender)) return;
			if (((ComboBoxCompat)sender).Items.IndexOf(((ComboBoxCompat)sender).Text) != -1) return;

			ushort val = Convert.ToUInt16(((ComboBoxCompat)sender).Text, 16);
			internalchg = true;
			switch (alHex16cb.IndexOf(sender))
			{
				case 0: currentInst.Instruction.Target1 = val; break;
				case 1: currentInst.Instruction.Target2 = val; break;
				case 2: wrapper.Header.Format = val; break;
			}
			internalchg = false;
		}

		private void cbHex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (cbHex16_IsValid(sender)) return;

			int i = alHex16cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex16_Validating not applicable to control " + sender.ToString());

			e.Cancel = true;

			ushort val = 0;
			switch (i)
			{
				case 0: val = origInst.Target1; break;
				case 1: val = origInst.Target2; break;
				case 2: val = wrapper.Header.Format; break;
			}

			if (i < 2 && val >= 0xfffc && val <= 0xfffe)
			{
				((ComboBoxCompat)sender).SelectedIndex = val - 0xfffc;
			}
			else if (i == 2 && val >= 0x8000 && val <= 0x8007)
			{
				((ComboBoxCompat)sender).SelectedIndex = val - 0x8000;
			}
			else
			{
				((ComboBoxCompat)sender).SelectedIndex = -1;
				((ComboBoxCompat)sender).Content = "0x" + Helper.HexString(val);
			}
			((ComboBoxCompat)sender).SelectAll();
		}

		private void cbHex16_Validated(object sender, System.EventArgs e)
		{
			int i = alHex16cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex16_Validated not applicable to control " + sender.ToString());
			if (((ComboBoxCompat)sender).Items.IndexOf(((ComboBoxCompat)sender).Text) != -1) return;

			ushort val = Convert.ToUInt16(((ComboBoxCompat)sender).Text, 16);

			bool origstate = internalchg;
			internalchg = true;
			if (i < 2 && val >= 0xfffc && val <= 0xfffe)
			{
				((ComboBoxCompat)sender).SelectedIndex = val - 0xfffc;
			}
			else if (i == 2 && val >= 0x8000 && val <= 0x8007)
			{
				((ComboBoxCompat)sender).SelectedIndex = val - 0x8000;
			}
			else
			{
				((ComboBoxCompat)sender).SelectedIndex = -1;
				((ComboBoxCompat)sender).Content = "0x" + Helper.HexString(val);
			}
			internalchg = origstate;
			((ComboBoxCompat)sender).Select(0, 0);
		}

		private void cbHex16_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;

			int i = alHex16cb.IndexOf(sender);
			if (i < 0)
				throw new Exception("cbHex16_SelectedIndexChanged not applicable to control " + sender.ToString());
			if (((ComboBoxCompat)sender).SelectedIndex == -1) return;

			ushort val = (ushort)((ComboBoxCompat)alHex16cb[i]).SelectedIndex;
			((ComboBoxCompat)sender).SelectAll();

			internalchg = true;
			if (i < 2)
			{
				val += 0xFFFC;
				if (i == 0) currentInst.Instruction.Target1 = val;
				else        currentInst.Instruction.Target2 = val;
			}
			else
			{
				val += 0x8000;
				wrapper.Header.Format = val;
			}
			internalchg = false;
		}

		private void dec8_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!dec8_IsValid(sender)) return;

			byte val = Convert.ToByte(((TextBoxCompat)sender).Text);
			internalchg = true;
			switch (alDec8.IndexOf(sender))
			{
				default: break;
			}
			internalchg = false;
		}

		private void dec8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (dec8_IsValid(sender)) return;

			e.Cancel = true;

			byte val = 0;
			switch (alDec8.IndexOf(sender))
			{
				default: break;
			}

			((TextBoxCompat)sender).Text = val.ToString();
			((TextBoxCompat)sender).SelectAll();
        }

        private void hex8_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex8_IsValid(sender)) return;

			byte val = Convert.ToByte(((TextBoxCompat)sender).Text, 16);
			int i = alHex8.IndexOf(sender);

			internalchg = true;

            byte oldval = val;
            if (i < 8) { oldval = currentInst.Instruction.Operands[i]; currentInst.Instruction.Operands[i] = val; ChangeLongname(oldval, val); }
            else if (i < 16) { oldval = currentInst.Instruction.Reserved1[i - 8]; currentInst.Instruction.Reserved1[i - 8] = val; ChangeLongname(oldval, val); }
            else
                switch (i)
                {
                    case 16: oldval = currentInst.Instruction.NodeVersion; currentInst.Instruction.NodeVersion = val; ChangeLongname(oldval, val); break;
                    case 17: wrapper.Header.HeaderFlag = val; break;
                    case 18: wrapper.Header.Type = val; break;
                    case 19: wrapper.Header.CacheFlags = val; break;
                    case 20: oldval = wrapper.Header.ArgumentCount; wrapper.Header.ArgumentCount = val; ChangeLongname(oldval, val); break;
                    case 21: wrapper.Header.LocalVarCount = val; break;
                }

			internalchg = false;
		}

		private void hex8_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex8_IsValid(sender)) return;

			e.Cancel = true;

			byte val = 0;
			int i = alHex8.IndexOf(sender);

			if (i < 8) val = origInst.Operands[i];
			else if (i < 16) val = origInst.Reserved1[i-8];
			else switch(i)
				 {
					 case 16: val = origInst.NodeVersion; break;
					 case 17: val = wrapper.Header.HeaderFlag; break;
					 case 18: val = wrapper.Header.Type; break;
					 case 19: val = wrapper.Header.CacheFlags; break;
					 case 20: val = wrapper.Header.ArgumentCount; break;
					 case 21: val = wrapper.Header.LocalVarCount; break;
				 }

			((TextBoxCompat)sender).Text = ((i >= 16) ? "0x" : "") + Helper.HexString(val);
			((TextBoxCompat)sender).SelectAll();
		}

		private void hex8_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBoxCompat)sender).Text = ((alHex8.IndexOf(sender) >= 16) ? "0x" : "") + Helper.HexString(Convert.ToByte(((TextBoxCompat)sender).Text, 16));
			((TextBoxCompat)sender).SelectAll();
			internalchg = origstate;
		}

		private void hex16_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!hex16_IsValid(sender)) return;

			ushort val = Convert.ToUInt16(((TextBoxCompat)sender).Text, 16);
			internalchg = true;
			switch (alHex16.IndexOf(sender))
			{
                case 0: OpcodeChanged(val); break;
			}
			internalchg = false;
		}

		private void hex16_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex16_IsValid(sender)) return;

			e.Cancel = true;

			ushort val = 0;
			switch (alHex16.IndexOf(sender))
			{
				case 0: val = origInst.OpCode; break;
                case 1: val = 1; break;
			}

			((TextBoxCompat)sender).Text = "0x" + Helper.HexString(val);
			((TextBoxCompat)sender).SelectAll();
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
				case 0: wrapper.Header.TreeVersion = val; break;
			}
			internalchg = false;
		}

		private void hex32_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (hex32_IsValid(sender)) return;

			e.Cancel = true;

			uint val = 0;
			switch (alHex32.IndexOf(sender))
			{
				case 0: val = wrapper.Header.TreeVersion; break;
			}

			((TextBoxCompat)sender).Text = "0x" + Helper.HexString(val);
			((TextBoxCompat)sender).SelectAll();
		}

		private void hex32_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBoxCompat)sender).Text = "0x" + Helper.HexString(Convert.ToUInt32(((TextBoxCompat)sender).Text, 16));
			((TextBoxCompat)sender).SelectAll();
			internalchg = origstate;
		}

		private void btnSort_Clicked(object sender, System.EventArgs e)
		{
            this.pnflowcontainer.Sort();
            SetComments();
		}

		private void btnMove_Clicked(object sender, System.EventArgs e)
		{
			int mv;
			try { mv = Convert.ToInt32(tbLines.Text, 16); }
			catch (Exception) { return; }
            try
            {
                this.gbMove.IsEnabled = false;
                if (sender == this.btnUp)
                    this.pnflowcontainer.MoveInst(mv * -1);
                else
                    this.pnflowcontainer.MoveInst(mv);
                SetComments();
            }
            finally
            {
                this.gbMove.IsEnabled = true;
            }
        }

		private void btnAdd_Clicked(object sender, EventArgs e)
		{
            this.pnflowcontainer.Add(BhavUIAddType.Default);
            SetComments();
		}

		private void btnDel_Clicked(object sender, EventArgs e)
		{
            this.pnflowcontainer.Delete(BhavUIDeleteType.Default);
            SetComments();
		}

		private void cbSpecial_CheckStateChanged(object sender, EventArgs e)
		{
			gbSpecial.IsVisible =
                pjse.Settings.PJSE.ShowSpecialButtons = ((CheckBoxCompat2)sender).IsChecked == true;
		}

		private void btnInsVia_Click(object sender, EventArgs e)
		{
			this.pnflowcontainer.Add( (sender == this.btnInsTrue) ? BhavUIAddType.ViaTrue : BhavUIAddType.ViaFalse );
		}

		private void btnDelPescado_Click(object sender, EventArgs e)
		{
			this.pnflowcontainer.Delete(BhavUIDeleteType.Pescado);
		}

		private void btnLinkInge_Click(object sender, EventArgs e)
		{
			this.pnflowcontainer.Relink();
		}

		private void btnAppend_Click(object sender, EventArgs e)
		{
            this.pnflowcontainer.Append(new ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, bhavPanel.Parent as Avalonia.Controls.Control, true, 0x10));
		}

		private void btnDelMerola_Click(object sender, EventArgs e)
		{
			this.pnflowcontainer.DeleteUnlinked();
		}

		private void btnCopyListing_Click(object sender, EventArgs e)
		{
			this.CopyListing();
		}

        private void btnPasteListing_Click(object sender, EventArgs e)
        {
            this.PasteListing();
        }

		private void btnTPRPMaker_Click(object sender, EventArgs e)
		{
			this.TPRPMaker();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.TPFWMaker();
        }

        private void btnInsUnlinked_Click(object sender, EventArgs e)
        {
            this.pnflowcontainer.Add(BhavUIAddType.Unlinked);
        }

        private void btnGUIDIndex_Click(object sender, EventArgs e)
        {
            this.cmenuGUIDIndex.Show((Control)sender, new Point(3 ,3));
        }

        private void cmenuGUIDIndex_Opening(object sender, EventArgs e)
        {
            createCurrentPackageToolStripMenuItem.IsEnabled =
                (pjse.FileTable.GFT.CurrentPackage != null
                && pjse.FileTable.GFT.CurrentPackage.FileName != null
                && !pjse.FileTable.GFT.CurrentPackage.FileName.ToLower().EndsWith("objects.package"));
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Application.DoEvents() not needed in Avalonia
            pjse.GUIDIndex.TheGUIDIndex.Create(sender.Equals(this.createCurrentPackageToolStripMenuItem));
            // Application.DoEvents() not needed in Avalonia
            
            // DialogResult dr = pjseMsgBox.Show(RemoteControl.ApplicationForm, pjse.Localization.GetString("guidAskMessage"), pjse.Localization.GetString("guidAskTitle"),
            SimPe.DialogResult dr = pjseMsgBox.ShowAsync(RemoteControl.ApplicationForm, "Do you want to save the GUID Index now? \r\n [Default] - save in the default location \r\n [Specify...] - let me specify where to save \r\n [No] - don't save, just let me get back to SimPe", pjse.Localization.GetString("guidAskTitle"),
                new Boolset("111"), new Boolset("111"), new string[] {
                    pjse.Localization.GetString("guidAskDefault"),
                    pjse.Localization.GetString("guidAskSpecify"),
                    pjse.Localization.GetString("guidAskNoSave"),
                },
                new SimPe.DialogResult[] { SimPe.DialogResult.OK, SimPe.DialogResult.Retry, SimPe.DialogResult.Cancel, }).GetAwaiter().GetResult();
            //SimPe.DialogResult dr = SimPe.Scenegraph.Compat.MessageBox.ShowAsync(pjse.Localization.GetString("guidAskMessage"), pjse.Localization.GetString("guidAskTitle"), "\r\n" + 
            //    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == SimPe.DialogResult.OK) defaultFileToolStripMenuItem_Click(this.defaultFileToolStripMenuItem1, null);
            else if (dr == SimPe.DialogResult.Retry) fileToolStripMenuItem_Click(this.toFileToolStripMenuItem, null);
        }

        private void defaultFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.Equals(this.defaultFileToolStripMenuItem))
                pjse.GUIDIndex.TheGUIDIndex.Load();
            else
                pjse.GUIDIndex.TheGUIDIndex.Save();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool load = sender.Equals(this.fromFileToolStripMenuItem);
            FileDialogCompat fd;
            if (load)
                fd = new OpenFileDialogCompat();
            else
                fd = new SaveFileDialogCompat();
            fd.AddExtension = true;
            fd.CheckFileExists = load;
            fd.CheckPathExists = true;
            fd.DefaultExt = "txt";
            fd.DereferenceLinks = true;
            //fd.FileName = pjse.GUIDIndex.DefaultGUIDFile;
            fd.FileName = "guidindex.txt";
            fd.Filter = pjse.Localization.GetString("guidFilter");
            fd.FilterIndex = 1;
            fd.RestoreDirectory = false;
            fd.ShowHelp = false;
            // fd.SupportMultiDottedExtensions = false; // Methods missing from Mono
            fd.Title = load
                ? pjse.Localization.GetString("guidLoadIndex")
                : pjse.Localization.GetString("guidSaveIndex");
            fd.ValidateNames = true;
            SimPe.DialogResult dr = fd.ShowDialog();
            if (dr == SimPe.DialogResult.OK)
            {
                if (load)
                    pjse.GUIDIndex.TheGUIDIndex.Load(fd.FileName);
                else
                    pjse.GUIDIndex.TheGUIDIndex.Save(fd.FileName);
            }
        }
	}
}
