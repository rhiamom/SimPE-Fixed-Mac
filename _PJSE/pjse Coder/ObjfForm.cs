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
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;

namespace SimPe.PackedFiles.UserInterface
{
    /// <summary>
    /// Summary description for ObjfForm.
    /// </summary>
    public class ObjfForm : Window, IPackedFileUI
    {
        #region Form variables

        private LabelCompat label19;
        private StackPanel objfPanel;
        private LabelCompat lbFilename;
        private TextBoxCompat tbFilename;
        private LinkLabel llGuardian;
        private LinkLabel llAction;
        private ButtonCompat btnAction;
        private ButtonCompat btnGuardian;
        private TextBoxCompat tbGuardian;
        private TextBoxCompat tbAction;
        private ButtonCompat btnCommit;
        private LabelCompat lbAction;
        private LabelCompat lbGuardian;
        private ListView lvObjfItem;
        private pjse.pjse_banner pjse_banner1;
        private LabelCompat lbFunction;
        #endregion

        public ObjfForm()
        {
            InitializeComponent();
            lbFunction.Text = "";

            TextBoxCompat[] tbua = { tbAction, tbGuardian };
            alHex16 = new ArrayList(tbua);

            pjse.FileTable.GFT.FiletableRefresh += new EventHandler(GFT_FiletableRefresh);
            if (SimPe.Helper.XmlRegistry.UseBigIcons)
            {
                this.chAction.Width = 350;
                this.chGuardian.Width = 350;
            }
            ThemeManager.Global.AddControl(this.objfPanel);
        }

        void GFT_FiletableRefresh(object sender, EventArgs e)
        {
            if (wrapper.FileDescriptor == null) return;

            bool savedchg = internalchg;
            internalchg = true;

            bool parm = false;

            funcDescs = new pjse.Str(pjse.GS.BhavStr.OBJFDescs);
            if (wrapper.Count == 0)
            {
                int max = pjse.BhavWiz.readStr(pjse.GS.BhavStr.OBJFDescs).Count;
                for (int i = 0; i < max; i++) wrapper.Add(new ObjfItem(wrapper));
                lvObjfItem.Items[0].Selected = true;
            }
            for (ushort i = 0; i < lvObjfItem.Items.Count; i++)
            {
                lvObjfItem.Items[i].SubItems[0].Text = pjse.BhavWiz.readStr(pjse.GS.BhavStr.OBJFDescs, i);
                lvObjfItem.Items[i].SubItems[1].Text = pjse.BhavWiz.bhavName(wrapper, wrapper[i].Action, ref parm);
                lvObjfItem.Items[i].SubItems[2].Text = pjse.BhavWiz.bhavName(wrapper, wrapper[i].Guardian, ref parm);
            }

            if (lvObjfItem.SelectedIndices.Count > 0)
                setLabel(lvObjfItem.SelectedIndices[0]);

            if (currentItem != null)
            {
                setBHAV(0, currentItem.Action, false);
                setBHAV(1, currentItem.Guardian, false);
            }

            internalchg = savedchg;
        }

        public void Dispose() { }


        #region ObjfForm
        /// <summary>
        /// Stores the currently active Wrapper
        /// </summary>
        private Objf wrapper = null;
        private bool internalchg;
        private bool setHandler = false;
        private ArrayList alHex16;
        private ObjfItem origItem;
        private ObjfItem currentItem;

        private ColumnHeader chFunction;
        private ColumnHeader chGuardian;
        private ColumnHeader chAction;

        private static pjse.Str funcDescs = new pjse.Str(pjse.GS.BhavStr.OBJFDescs);
        private void setLabel(int index)
        {
            lbFunction.Text = "";
            if (funcDescs == null || index < 0 || ((pjse.FallbackStrItem)funcDescs[index]) == null) return;
            StrItem s = ((pjse.FallbackStrItem)funcDescs[index]).strItem;
            if (s != null) lbFunction.Text = s.Description;
        }

        private bool hex16_IsValid(object sender)
        {
            if (alHex16.IndexOf(sender) < 0)
                throw new Exception("hex16_IsValid not applicable to control " + sender.ToString());
            try { Convert.ToUInt16(((TextBoxCompat)sender).Text, 16); }
            catch (Exception) { return false; }
            return true;
        }


        private void setBHAV(int which, ushort target, bool notxt)
        {
            TextBoxCompat[] tbaAG = { tbAction, tbGuardian };
            if (!notxt) tbaAG[which].Text = "0x"+Helper.HexString(target);

            LabelCompat[] lbaAG = { lbAction, lbGuardian };
            LinkLabel[] llaAG = { llAction, llGuardian };
            bool found = false;
            this.lvObjfItem.SelectedItems[0].SubItems[1 + which].Text = (string)(lbaAG[which].Text = pjse.BhavWiz.bhavName(wrapper, target, ref found));
            llaAG[which].IsEnabled = found;
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
                return objfPanel;
            }
        }

        /// <summary>
        /// Called by the AbstractWrapper when the file should be displayed to the user.
        /// </summary>
        public void UpdateGUI(IFileWrapper wrp)
        {
            wrapper = (Objf)wrp;
            WrapperChanged(wrapper, null);

            internalchg = true;

            this.lvObjfItem.Items.Clear();
            bool parm = false;

            if (wrapper.Count == 0)
            {
                int max = pjse.BhavWiz.readStr(pjse.GS.BhavStr.OBJFDescs).Count;
                for (int i = 0; i < max; i++) wrapper.Add(new ObjfItem(wrapper));
            }
            for (ushort i = 0; i < wrapper.Count; i++)
                this.lvObjfItem.Items.Add(new ListViewItem(
                    new string[] {
                                     pjse.BhavWiz.readStr(pjse.GS.BhavStr.OBJFDescs, i)
                                     , pjse.BhavWiz.bhavName(wrapper, wrapper[i].Action, ref parm)
                                     , pjse.BhavWiz.bhavName(wrapper, wrapper[i].Guardian, ref parm)
                                 }
                    ));

            internalchg = false;

            lvObjfItem.Items[0].Selected = true;

            if (!setHandler)
            {
                wrapper.WrapperChanged += (s, e) => this.WrapperChanged(s, e);
                setHandler = true;
            }
        }

        private void WrapperChanged(object sender, System.EventArgs e)
        {
            this.btnCommit.IsEnabled = wrapper.Changed;

            if (internalchg) return;
            internalchg = true;
            this.Title = tbFilename.Text = wrapper.FileName;
            internalchg = false;
        }
        #endregion

        #region InitializeComponent
        private void InitializeComponent()
        {
            this.objfPanel = new StackPanel();
            this.llAction = new LinkLabel();
            this.llGuardian = new LinkLabel();
            this.tbAction = new TextBoxCompat();
            this.btnAction = new ButtonCompat { Content = "..." };
            this.tbGuardian = new TextBoxCompat();
            this.btnGuardian = new ButtonCompat { Content = "..." };
            this.lbAction = new LabelCompat();
            this.lbGuardian = new LabelCompat();
            this.pjse_banner1 = new pjse.pjse_banner();
            this.lbFunction = new LabelCompat();
            this.lvObjfItem = new ListView();
            this.chFunction = new ColumnHeader { Text = "Function" };
            this.chAction = new ColumnHeader { Text = "Action" };
            this.chGuardian = new ColumnHeader { Text = "Guardian" };
            this.btnCommit = new ButtonCompat { Content = "Commit" };
            this.lbFilename = new LabelCompat { Content = "Filename" };
            this.tbFilename = new TextBoxCompat();
            this.label19 = new LabelCompat();

            this.lvObjfItem.Columns.Add(this.chFunction);
            this.lvObjfItem.Columns.Add(this.chAction);
            this.lvObjfItem.Columns.Add(this.chGuardian);
            this.lvObjfItem.Name = "lvObjfItem";
            this.lvObjfItem.SelectedIndexChanged += (s, e) => this.lvObjfItem_SelectedIndexChanged(s, e);

            this.llAction.Name = "llAction";
            this.llAction.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);
            this.llGuardian.Name = "llGuardian";
            this.llGuardian.LinkClicked += new LinkLabelLinkClickedEventHandler(this.llBhav_LinkClicked);

            this.tbAction.TextChanged += (s, e) => this.hex16_TextChanged(s, e);
            this.tbAction.LostFocus += (s, e) => this.hex16_Validated(s, e);
            this.tbGuardian.TextChanged += (s, e) => this.hex16_TextChanged(s, e);
            this.tbGuardian.LostFocus += (s, e) => this.hex16_Validated(s, e);

            this.btnAction.Click += (s, e) => this.GetObjfAction(s, e);
            this.btnGuardian.Click += (s, e) => this.GetObjfGuard(s, e);
            this.btnCommit.Click += (s, e) => this.btnCommit_Click(s, e);
            this.tbFilename.TextChanged += (s, e) => this.tbFilename_TextChanged(s, e);
            this.tbFilename.LostFocus += (s, e) => this.tbFilename_Validated(s, e);

            var actionRow = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
            actionRow.Children.Add(this.tbAction);
            actionRow.Children.Add(this.btnAction);
            var guardianRow = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
            guardianRow.Children.Add(this.tbGuardian);
            guardianRow.Children.Add(this.btnGuardian);

            var filenameRow = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal };
            filenameRow.Children.Add(this.lbFilename);
            filenameRow.Children.Add(this.tbFilename);

            this.objfPanel.Children.Add(this.pjse_banner1);
            this.objfPanel.Children.Add(filenameRow);
            this.objfPanel.Children.Add(this.label19);
            this.objfPanel.Children.Add(this.lvObjfItem);
            this.objfPanel.Children.Add(this.lbFunction);
            this.objfPanel.Children.Add(this.llAction);
            this.objfPanel.Children.Add(actionRow);
            this.objfPanel.Children.Add(this.lbAction);
            this.objfPanel.Children.Add(this.llGuardian);
            this.objfPanel.Children.Add(guardianRow);
            this.objfPanel.Children.Add(this.lbGuardian);
            this.objfPanel.Children.Add(this.btnCommit);
            this.objfPanel.Name = "objfPanel";

            this.Content = this.objfPanel;
            this.Name = "ObjfForm";
        }
        #endregion

        private void lvObjfItem_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.internalchg) return;

            if (lvObjfItem.SelectedIndices.Count > 0 && lvObjfItem.SelectedIndices[0] >= 0)
            {
                currentItem = wrapper[lvObjfItem.SelectedIndices[0]];
                setLabel(lvObjfItem.SelectedIndices[0]);
                origItem = currentItem.Clone();

                internalchg = true;

                setBHAV(0, currentItem.Action, false);
                setBHAV(1, currentItem.Guardian, false);
                tbGuardian.IsEnabled = tbAction.IsEnabled = true;

                internalchg = false;
            }
            else
            {
                internalchg = true;

                tbGuardian.Text = tbAction.Text = "";
                lbGuardian.Text = lbAction.Text = "";
                tbGuardian.IsEnabled = tbAction.IsEnabled = false;

                internalchg = false;
            }
        }


        private void llBhav_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pjse.FileTable.Entry item = wrapper.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, (sender == llAction) ? currentItem.Action : currentItem.Guardian);
            Bhav b = new Bhav();
            b.ProcessData(item.PFD, item.Package);

            BhavForm ui = (BhavForm)b.UIHandler;
            ui.Tag = "Popup"
                + ";callerID=+" + wrapper.FileDescriptor.ExportFileName + "+";
            string bhavTitle = pjse.Localization.GetString("viewbhav") + ": " + b.FileName + " [" + b.Package.SaveFileName + "]";
            b.RefreshUI();
            new Avalonia.Controls.Window { Title = bhavTitle, Content = ui }.Show();
        }

        private void btnCommit_Click(object sender, System.EventArgs e)
        {
            try
            {
                wrapper.SynchronizeUserData();
                btnCommit.IsEnabled = wrapper.Changed;
                lvObjfItem_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage(pjse.Localization.GetString("errwritingfile"), ex);
            }
        }


        private void GetObjfAction(object sender, System.EventArgs e)
        {
            pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, objfPanel, false);
            if (item != null)
                setBHAV(0, (ushort)item.Instance, false);
        }

        private void GetObjfGuard(object sender, System.EventArgs e)
        {
            pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, wrapper.FileDescriptor.Group, objfPanel, false);
            if (item != null)
                setBHAV(1, (ushort)item.Instance, false);
        }


        private void tbFilename_TextChanged(object sender, System.EventArgs e)
        {
            wrapper.FileName = tbFilename.Text;
        }

        private void tbFilename_Validated(object sender, System.EventArgs e)
        {
        }


        private void hex16_TextChanged(object sender, System.EventArgs ev)
        {
            if (internalchg) return;
            if (!hex16_IsValid(sender)) return;

            ushort val = Convert.ToUInt16(((TextBoxCompat)sender).Text, 16);
            internalchg = true;
            switch (alHex16.IndexOf(sender))
            {
                case 0: currentItem.Action = val; setBHAV(0, val, true); break;
                case 1: currentItem.Guardian = val; setBHAV(1, val, true); break;
            }
            internalchg = false;
        }

        private void hex16_Validating(object sender, EventArgs e)
        {
            if (hex16_IsValid(sender)) return;

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
            }
            ((TextBoxCompat)sender).Text = "0x" + Helper.HexString(val);
            internalchg = false;
        }

        private void hex16_Validated(object sender, System.EventArgs e)
        {
            bool origstate = internalchg;
            internalchg = true;
            if (hex16_IsValid(sender))
                ((TextBoxCompat)sender).Text = "0x" + Helper.HexString(Convert.ToUInt16(((TextBoxCompat)sender).Text, 16));
            internalchg = origstate;
        }
    }
}
