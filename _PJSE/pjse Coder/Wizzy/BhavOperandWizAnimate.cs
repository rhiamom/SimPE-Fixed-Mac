/***************************************************************************
 *   Copyright (C) 2007 by Peter L Jones                                   *
 *   pljones@users.sf.net                                                  *
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
 *   59 Temple Place - Suite 330, Boston, MA  32111-1307, USA.             *
 ***************************************************************************/
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Controls;
using SimPe.Scenegraph.Compat;
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.WizAnimate
{
    internal class UI : Window, iBhavOperandWizForm
    {
        #region Form variables

        internal StackPanel pnWizAnimate;
        private FlowLayoutPanel flpnMain;
        private Panel pnObject;
        private ComboBoxCompat cbPickerObject;
        private TextBoxCompat tbValObject;
        private ComboBoxCompat cbdoObject;
        private LabelCompat label1;
        private FlowLayoutPanel flpnAnimType;
        private LabelCompat label4;
        private TextBoxCompat tbValAnimType;
        private ComboBoxCompat cbAnimType;
        private TextBoxCompat tbAnimType;
        private FlowLayoutPanel flpnAnim;
        private LabelCompat lbParam;
        private TextBoxCompat tbValAnim;
        private ButtonCompat btnAnim;
        private TextBoxCompat tbAnim;
        private FlowLayoutPanel flpnEventScope;
        private LabelCompat label2;
        private ComboBoxCompat cbEventScope;
        private FlowLayoutPanel flpnEventTree;
        private LinkLabel llEvent;
        private TextBoxCompat tbValEventTree;
        private ButtonCompat btnEventTree;
        private TextBoxCompat tbEventTree;
        private FlowLayoutPanel flpnOptions;
        private GroupBox groupBox1;
        private FlowLayoutPanel flpnOptions1;
        private CheckBoxCompat2 ckbFlipped;
        private CheckBoxCompat2 ckbAnimSpeed;
        private CheckBoxCompat2 ckbParam;
        private CheckBoxCompat2 ckbInterruptible;
        private CheckBoxCompat2 ckbStartTag;
        private CheckBoxCompat2 ckbLoopCount;
        private CheckBoxCompat2 ckbTransToIdle;
        private CheckBoxCompat2 ckbBlendOut;
        private CheckBoxCompat2 ckbBlendIn;
        private GroupBox groupBox2;
        private FlowLayoutPanel flpnOptions2;
        private CheckBoxCompat2 ckbFlipTemp3;
        private CheckBoxCompat2 ckbSync;
        private CheckBoxCompat2 ckbAlignBlend;
        private CheckBoxCompat2 ckbControllerIsSource;
        private CheckBoxCompat2 ckbNotHurryable;
        private Panel pnDoidOptions;
        private CheckBoxCompat2 ckbAttrPicker;
        private CheckBoxCompat2 ckbDecimal;
        private Panel pnIKObject;
        private ComboBoxCompat cbPickerIK;
        private TextBoxCompat tbValIK;
        private ComboBoxCompat cbdoIK;
        private LabelCompat label3;
        private GroupBox gbPriority;
        private ComboBoxCompat cbPriority;
        /// <summary>
        /// Required designer variable.
        /// </summary>
                #endregion


        /// <summary>
        /// Initialise the Wizard user interface
        /// </summary>
        /// <param name="mode">Specify whether the wizard is for Animate Object, Sim or Overlay</param>
        public UI(String mode)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            #region AnimNames
            cbAnimType.Items.Clear();
            cbAnimType.Items.AddRange(new  String[] {
                "AdultAnims",
                "ChildAnims",
                "SocialAnims",
                "LocoAnims",
                "ObjectAnims",
                "ToddlerAnims",
                "TeenAnims",
                "ElderAnims",
                "CatAnims",
                "DogAnims",
                "BabyAnims",
                "ReachAnims",
                "PuppyAnims",
                "KittenAnims",
                "SmallDogAnims",
                "ElderLargeDogAnims",
                "ElderSmallDogAnims",
                "ElderCatAnims",
            });
            // Two-byte values
            //cbAnimType.Items.AddRange(new String[] {
            //            "ObjectElderAnims",
            //            "ObjectTeenAnims",
            //            "ObjectChildAnims",
            //            "ObjectToddlerAnims",
            //            "ObjectLargeDogAnims",
            //            "ObjectCatAnims",
            //            "ObjectPuppyAnims",
            //            "ObjectKittenAnims",
            //            "ObjectSmallDogAnims",
            //        });
            #endregion

            this.mode = mode;
            switch (mode)
            {
                case "bwp_Object":
                    lckbOptions1 = new List<CheckBoxCompat2>(new CheckBoxCompat2[] {
                        ckbFlipped, ckbAnimSpeed, ckbParam, ckbInterruptible, ckbStartTag, ckbLoopCount, ckbBlendOut, ckbBlendIn
                    });
                    lckbOptions2 = new List<CheckBoxCompat2>(new CheckBoxCompat2[] {
                        ckbFlipTemp3, null, ckbSync, ckbAlignBlend, ckbNotHurryable
                    });
                    this.flpnMain.Controls.Remove(flpnAnimType);
                    this.flpnMain.Controls.Remove(pnIKObject);
                    this.flpnOptions.Controls.Remove(gbPriority);
                    break;
                case "bwp_Sim":
                    lckbOptions1 = new List<CheckBoxCompat2>(new CheckBoxCompat2[] {
                        ckbFlipped, ckbAnimSpeed, ckbParam, ckbInterruptible, ckbStartTag, ckbTransToIdle, ckbBlendOut, ckbBlendIn
                    });
                    lckbOptions2 = new List<CheckBoxCompat2>(new CheckBoxCompat2[] {
                        ckbFlipTemp3, ckbSync, null, null, ckbSync, ckbControllerIsSource, ckbNotHurryable
                    });
                    this.flpnMain.Controls.Remove(pnObject);
                    break;
                case "bwp_Overlay":
                    lckbOptions1 = new List<CheckBoxCompat2>(new CheckBoxCompat2[] {
                        ckbFlipped, ckbAnimSpeed, ckbParam, ckbInterruptible, ckbStartTag, ckbLoopCount, ckbBlendOut, ckbBlendIn
                    });
                    lckbOptions2 = new List<CheckBoxCompat2>(new CheckBoxCompat2[] {
                        ckbFlipTemp3, null, null, null, ckbSync, ckbAlignBlend
                    });
                    this.flpnMain.Controls.Remove(pnIKObject);
                    break;
                default:
                    throw new ArgumentException("Argument must match bwp_{Object,Sim,Overlay}", "mode");
            }
            lckb = new List<CheckBoxCompat2>(new CheckBoxCompat2[] {
                ckbAnimSpeed, ckbInterruptible, ckbStartTag, ckbLoopCount,
                ckbTransToIdle, ckbBlendOut, ckbBlendIn, ckbFlipTemp3,
                ckbSync, ckbAlignBlend, ckbControllerIsSource, ckbNotHurryable
            });

            pnWizAnimate.Height = flpnOptions.Bottom;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
            }


            inst = null;
        }


        private String mode = "";
        private Instruction inst = null;

        private DataOwnerControl doidObject = null;
        private DataOwnerControl doidAnim = null;
        private DataOwnerControl doidEvent = null;
        private DataOwnerControl doidAnimType = null;
        private DataOwnerControl doidIK = null;

        private bool internalchg = false;

        private List<CheckBoxCompat2> lckbOptions1;
        private List<CheckBoxCompat2> lckbOptions2;
        private List<CheckBoxCompat2> lckb;

        private void doCkbParam()
        {
            if (ckbParam.IsChecked == true)
            {
                lbParam.Content = ((String)lbParam.Tag).Split('|')[0];
            }
            else
            {
                lbParam.Content = ((String)lbParam.Tag).Split('|')[1];
                doStrValue(doidAnim.Value, tbAnim);
            }
            btnAnim.IsVisible = tbAnim.IsVisible = ckbParam.IsChecked == true != true;
        }

        private void doStrChooser(TextBoxCompat tbVal, TextBoxCompat strText)
        {
            pjse.FileTable.Entry[] items =
                pjse.FileTable.GFT[(uint)SimPe.Data.MetaData.STRING_FILE, inst.Parent.GroupForScope(AnimScope()), (uint)AnimInstance()];

            if (items == null || items.Length == 0)
            {
                MessageBox.Show(pjse.Localization.GetString("bow_noStrings")
                    + " (" + pjse.Localization.GetString(AnimScope().ToString()) + ")");
                return; // eek!
            }

            SimPe.PackedFiles.Wrapper.StrWrapper str = new StrWrapper();
            str.ProcessData(items[0].PFD, items[0].Package);

            int i = (new StrChooser(true)).Strnum(str);
            if (i >= 0)
            {
                bool savedState = internalchg;
                internalchg = true;
                tbVal.Text = "0x" + SimPe.Helper.HexString((ushort)i);
                doStrValue((ushort)i, strText);
                internalchg = savedState;
            }
        }

        private bool IsAnim(ushort i)
        {
            try { return IsAnim((GS.GlobalStr)i); }
            catch { }
            return false;
        }
        private bool IsAnim(GS.GlobalStr g) { return IsAnim(g.ToString()); }
        private bool IsAnim(String s) { return s.EndsWith("Anims"); }

        private Scope AnimScope()
        {
            if (mode.Equals("bwp_Object")) return Scope.Private;
            return (this.doidAnimType.Value == 0x80) ? Scope.Global : Scope.Private;
        }

        private GS.GlobalStr AnimInstance()
        {
            if (mode.Equals("bwp_Object")) return GS.GlobalStr.ObjectAnims;

            if (this.doidAnimType.Value == 0x80) return GS.GlobalStr.AdultAnims;
            if (IsAnim(this.doidAnimType.Value)) return (GS.GlobalStr)this.doidAnimType.Value;
            return GS.GlobalStr.ObjectAnims;
        }

        private void doStrValue(ushort strno, TextBoxCompat strText)
        {
            strText.Text = ((BhavWiz)inst).readStr(AnimScope(), AnimInstance(), strno, -1, pjse.Detail.ErrorNames);
        }

        private void doidAnimType_DataOwnerControlChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            internalchg = true;

            try
            {
                cbAnimType.SelectedIndex = cbAnimType.Items.IndexOf(((GS.GlobalStr)doidAnimType.Value).ToString());
                tbAnimType.Text = (cbAnimType.SelectedIndex >= 0) ? this.cbAnimType.SelectedItem.ToString() : "---";
            }
            finally
            {
                internalchg = false;
                doStrValue(doidAnim.Value, tbAnim);
            }
        }

        private void doidAnim_DataOwnerControlChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            doStrValue(doidAnim.Value, tbAnim);
        }

        private void doidEvent_DataOwnerControlChanged(object sender, EventArgs e)
        {
            bool found = false;
            tbEventTree.Text = pjse.BhavWiz.bhavName(inst.Parent, doidEvent.Value, ref found);
            if (!found) tbEventTree.Text = "---";
            llEvent.IsEnabled = found;
        }

        private byte getScope(byte scope)
        {
            return (byte)((cbEventScope.SelectedIndex >= 0) ? cbEventScope.SelectedIndex : scope);
        }

        private byte getPriority(byte priority)
        {
            return (byte)((cbPriority.SelectedIndex >= 0) ? cbPriority.SelectedIndex : priority);
        }

        private byte getOptions(List<CheckBoxCompat2> lckbOptions, Boolset options)
        {
            for (int i = 0; i < lckbOptions.Count; i++)
                if (lckbOptions[i] != null) options[i] = lckbOptions[i].Checked;
            return options;
        }

        #region iBhavOperandWizForm
        public StackPanel WizPanel { get { return this.pnWizAnimate; } }

        public void Execute(Instruction inst)
        {
            this.inst = inst;

            wrappedByteArray ops1 = inst.Operands;
            wrappedByteArray ops2 = inst.Reserved1;
            Boolset options1 = null;
            Boolset options2 = null;
            int scope = 0;
            int priority = -1;

            internalchg = true;

            foreach (CheckBoxCompat2 c in lckb) c.IsVisible = false;

            doidAnim = new DataOwnerControl(inst, null, null, tbValAnim,
                ckbDecimal, ckbAttrPicker, null, 0x07, BhavWiz.ToShort(ops1[0], ops1[1]));
            doidAnim.DataOwnerControlChanged += new EventHandler(doidAnim_DataOwnerControlChanged);

            options1 = ops1[2];

            doidEvent = new DataOwnerControl(inst, null, null, tbValEventTree,
                ckbDecimal, ckbAttrPicker, null, 0x07, BhavWiz.ToShort(ops1[4], ops1[5]));
            doidEvent.DataOwnerControlChanged += new EventHandler(doidEvent_DataOwnerControlChanged);

            switch (mode)
            {
                case "bwp_Object":
                    doidObject = new DataOwnerControl(inst, cbdoObject, cbPickerObject, tbValObject,
                        ckbDecimal, ckbAttrPicker, null, ops1[6], BhavWiz.ToShort(ops1[7], ops2[0]));
                    scope = ops2[1];
                    options2 = ops2[2];
                    break;

                case "bwp_Sim":
                    doidAnimType = new DataOwnerControl(inst, null, null, tbValAnimType,
                        ckbDecimal, ckbAttrPicker, null, 0x07, (byte)ops1[6]);
                    doidAnimType.DataOwnerControlChanged += new EventHandler(doidAnimType_DataOwnerControlChanged);
                    scope = ops1[7];
                    options2 = ops2[0];
                    doidIK = new DataOwnerControl(inst, cbdoIK, cbPickerIK, tbValIK,
                        ckbDecimal, ckbAttrPicker, null, ops2[1], BhavWiz.ToShort(ops2[2], ops2[3]));
                    priority = ops2[4];
                    break;

                case "bwp_Overlay":
                    doidObject = new DataOwnerControl(inst, cbdoObject, cbPickerObject, tbValObject,
                        ckbDecimal, ckbAttrPicker, null, ops1[6], BhavWiz.ToShort(ops1[7], ops2[0]));
                    doidAnimType = new DataOwnerControl(inst, null, null, tbValAnimType,
                        ckbDecimal, ckbAttrPicker, null, 0x07, (byte)ops2[1]);
                    doidAnimType.DataOwnerControlChanged += new EventHandler(doidAnimType_DataOwnerControlChanged);
                    if (inst.NodeVersion != 0)
                    {
                        priority = ops2[3];
                        ckbNotHurryable.IsChecked = (ops2[4] & 0x01) != 0;
                        ckbNotHurryable.IsVisible = true;
                    }
                    else
                        priority = ops2[4];
                    scope = ops2[6];
                    options2 = ops2[7];
                    break;
            }

            for (int i = 0; i < lckbOptions1.Count; i++)
                if (lckbOptions1[i] != null)
                {
                    lckbOptions1[i].Visible = true;
                    lckbOptions1[i].Checked = options1[i];
                }

            for (int i = 0; i < lckbOptions2.Count; i++)
                if (lckbOptions2[i] != null)
                {
                    lckbOptions2[i].Visible = true;
                    lckbOptions2[i].Checked = options2[i];
                }

            switch (scope)
            {
                case 0: cbEventScope.SelectedIndex = 0; break;
                case 1: cbEventScope.SelectedIndex = 1; break;
                default: cbEventScope.SelectedIndex = 2; break;
            }

            internalchg = false;

            if (!mode.Equals("bwp_Object"))
                doidAnimType_DataOwnerControlChanged(null, null);
            else
                doidAnim_DataOwnerControlChanged(null, null);
            doidEvent_DataOwnerControlChanged(null, null);
            ckbParam_CheckedChanged(null, null);
            ckbFlipTemp3_CheckedChanged(null, null);
            if (priority < cbPriority.Items.Count)
                cbPriority.SelectedIndex = priority;
        }

        public Instruction Write(Instruction inst)
        {
            if (inst != null)
            {
                wrappedByteArray ops1 = inst.Operands;
                wrappedByteArray ops2 = inst.Reserved1;

                BhavWiz.FromShort(ref ops1, 0, doidAnim.Value);

                ops1[2] = getOptions(lckbOptions1, ops1[2]);

                BhavWiz.FromShort(ref ops1, 4, doidEvent.Value);
                byte[] lohi = { 0, 0 };

                switch (mode)
                {
                    case "bwp_Object":
                        ops1[6] = doidObject.DataOwner;
                        BhavWiz.FromShort(ref lohi, 0, doidObject.Value);
                        ops1[7] = lohi[0];
                        ops2[0] = lohi[1];
                        ops2[1] = getScope(ops2[1]);
                        ops2[2] = getOptions(lckbOptions2, ops2[2]);
                        break;

                    case "bwp_Sim":
                        ops1[6] = (byte)(doidAnimType.Value & 0xff);
                        ops1[7] = getScope(ops1[7]);
                        ops2[0] = getOptions(lckbOptions2, ops2[0]);
                        ops2[1] = doidIK.DataOwner;
                        BhavWiz.FromShort(ref ops2, 2, doidIK.Value);
                        ops2[4] = getPriority(ops2[4]);
                        break;

                    case "bwp_Overlay":
                        ops1[6] = doidObject.DataOwner;
                        BhavWiz.FromShort(ref lohi, 0, doidObject.Value);
                        ops1[7] = lohi[0];
                        ops2[0] = lohi[1];
                        ops2[1] = (byte)(doidAnimType.Value & 0xff);

                        if (inst.NodeVersion != 0)
                        {
                            ops2[3] = getPriority(ops2[3]);
                            Boolset options3 = ops2[4];
                            options3[0] = ckbNotHurryable.IsChecked == true;
                            ops2[4] = options3;
                        }
                        else
                            ops2[4] = getPriority(ops2[4]);

                        ops2[6] = getScope(ops2[6]);
                        ops2[7] = getOptions(lckbOptions2, ops2[7]);
                        break;
                }
            }
            return inst;
        }

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {            this.pnWizAnimate = new StackPanel();
            this.flpnMain = new FlowLayoutPanel();
            this.pnObject = new StackPanel();
            this.cbPickerObject = new ComboBoxCompat();
            this.tbValObject = new TextBoxCompat();
            this.cbdoObject = new ComboBoxCompat();
            this.label1 = new LabelCompat();
            this.pnIKObject = new StackPanel();
            this.cbPickerIK = new ComboBoxCompat();
            this.tbValIK = new TextBoxCompat();
            this.cbdoIK = new ComboBoxCompat();
            this.label3 = new LabelCompat();
            this.pnDoidOptions = new StackPanel();
            this.ckbAttrPicker = new CheckBoxCompat2();
            this.ckbDecimal = new CheckBoxCompat2();
            this.flpnAnimType = new FlowLayoutPanel();
            this.label4 = new LabelCompat();
            this.tbValAnimType = new TextBoxCompat();
            this.cbAnimType = new ComboBoxCompat();
            this.tbAnimType = new TextBoxCompat();
            this.flpnAnim = new FlowLayoutPanel();
            this.lbParam = new LabelCompat();
            this.tbValAnim = new TextBoxCompat();
            this.btnAnim = new ButtonCompat();
            this.tbAnim = new TextBoxCompat();
            this.flpnEventScope = new FlowLayoutPanel();
            this.label2 = new LabelCompat();
            this.cbEventScope = new ComboBoxCompat();
            this.flpnEventTree = new FlowLayoutPanel();
            this.llEvent = new SimPe.Scenegraph.Compat.LinkLabel();
            this.tbValEventTree = new TextBoxCompat();
            this.btnEventTree = new ButtonCompat();
            this.tbEventTree = new TextBoxCompat();
            this.flpnOptions = new FlowLayoutPanel();
            this.groupBox1 = new SimPe.Scenegraph.Compat.GroupBox();
            this.flpnOptions1 = new FlowLayoutPanel();
            this.ckbFlipped = new CheckBoxCompat2();
            this.ckbAnimSpeed = new CheckBoxCompat2();
            this.ckbParam = new CheckBoxCompat2();
            this.ckbInterruptible = new CheckBoxCompat2();
            this.ckbStartTag = new CheckBoxCompat2();
            this.ckbLoopCount = new CheckBoxCompat2();
            this.ckbTransToIdle = new CheckBoxCompat2();
            this.ckbBlendOut = new CheckBoxCompat2();
            this.ckbBlendIn = new CheckBoxCompat2();
            this.groupBox2 = new SimPe.Scenegraph.Compat.GroupBox();
            this.flpnOptions2 = new FlowLayoutPanel();
            this.ckbFlipTemp3 = new CheckBoxCompat2();
            this.ckbSync = new CheckBoxCompat2();
            this.ckbAlignBlend = new CheckBoxCompat2();
            this.ckbControllerIsSource = new CheckBoxCompat2();
            this.ckbNotHurryable = new CheckBoxCompat2();
            this.gbPriority = new SimPe.Scenegraph.Compat.GroupBox();
            this.cbPriority = new ComboBoxCompat();            //
            // pnWizAnimate
            //            this.pnWizAnimate.Children.Add(this.flpnMain);
            this.pnWizAnimate.Name = "pnWizAnimate";
            //
            // flpnMain
            //            this.flpnMain.Children.Add(this.pnObject);
            this.flpnMain.Children.Add(this.pnIKObject);
            this.flpnMain.Children.Add(this.pnDoidOptions);
            this.flpnMain.Children.Add(this.flpnAnimType);
            this.flpnMain.Children.Add(this.flpnAnim);
            this.flpnMain.Children.Add(this.flpnEventScope);
            this.flpnMain.Children.Add(this.flpnEventTree);
            this.flpnMain.Children.Add(this.flpnOptions);
            this.flpnMain.Name = "flpnMain";
            //
            // pnObject
            //
            this.pnObject.Children.Add(this.cbPickerObject);
            this.pnObject.Children.Add(this.tbValObject);
            this.pnObject.Children.Add(this.cbdoObject);
            this.pnObject.Children.Add(this.label1);            this.pnObject.Name = "pnObject";
            //
            // cbPickerObject
            //
            this.cbPickerObject.Name = "cbPickerObject";
            // tbValObject
            //            this.tbValObject.Name = "tbValObject";
            // cbdoObject
            //
            this.cbdoObject.Name = "cbdoObject";
            //
            // label1
            //            this.label1.Name = "label1";
            //
            // pnIKObject
            //
            this.pnIKObject.Children.Add(this.cbPickerIK);
            this.pnIKObject.Children.Add(this.tbValIK);
            this.pnIKObject.Children.Add(this.cbdoIK);
            this.pnIKObject.Children.Add(this.label3);            this.pnIKObject.Name = "pnIKObject";
            //
            // cbPickerIK
            //
            this.cbPickerIK.Name = "cbPickerIK";
            // tbValIK
            //            this.tbValIK.Name = "tbValIK";
            // cbdoIK
            //
            this.cbdoIK.Name = "cbdoIK";
            //
            // label3
            //            this.label3.Name = "label3";
            //
            // pnDoidOptions
            //
            this.pnDoidOptions.Children.Add(this.ckbAttrPicker);
            this.pnDoidOptions.Children.Add(this.ckbDecimal);            this.pnDoidOptions.Name = "pnDoidOptions";
            //
            // ckbAttrPicker
            //            this.ckbAttrPicker.Name = "ckbAttrPicker";
            //
            // ckbDecimal
            //            this.ckbDecimal.Name = "ckbDecimal";
            //
            // flpnAnimType
            //            this.flpnAnimType.Children.Add(this.label4);
            this.flpnAnimType.Children.Add(this.tbValAnimType);
            this.flpnAnimType.Children.Add(this.cbAnimType);
            this.flpnAnimType.Children.Add(this.tbAnimType);
            this.flpnAnimType.Name = "flpnAnimType";
            //
            // label4
            //            this.label4.Name = "label4";
            //
            // tbValAnimType
            //            this.tbValAnimType.Name = "tbValAnimType";
            //
            // cbAnimType
            //
            this.cbAnimType.SelectionChanged += (s, e) => this.cbAnimType_SelectedIndexChanged(s, e);
            //
            // tbAnimType
            //            this.tbAnimType.Name = "tbAnimType";
            this.tbAnimType.IsReadOnly = true;
            // flpnAnim
            //            this.flpnAnim.Children.Add(this.lbParam);
            this.flpnAnim.Children.Add(this.tbValAnim);
            this.flpnAnim.Children.Add(this.btnAnim);
            this.flpnAnim.Children.Add(this.tbAnim);
            this.flpnAnim.Name = "flpnAnim";
            //
            // lbParam
            //            this.lbParam.Name = "lbParam";
            this.lbParam.Tag = "Param|Animation String";
            //
            // tbValAnim
            //            this.tbValAnim.Name = "tbValAnim";
            //
            // btnAnim
            //            this.btnAnim.Name = "btnAnim";
            this.btnAnim.Click += (s, e) => this.btnAnim_Click(s, e);
            //
            // tbAnim
            //            this.tbAnim.Name = "tbAnim";
            this.tbAnim.IsReadOnly = true;
            // flpnEventScope
            //            this.flpnEventScope.Children.Add(this.label2);
            this.flpnEventScope.Children.Add(this.cbEventScope);
            this.flpnEventScope.Name = "flpnEventScope";
            //
            // label2
            //            this.label2.Name = "label2";
            this.label2.Tag = "";
            //
            // cbEventScope
            //
            //
            // flpnEventTree
            //            this.flpnEventTree.Children.Add(this.llEvent);
            this.flpnEventTree.Children.Add(this.tbValEventTree);
            this.flpnEventTree.Children.Add(this.btnEventTree);
            this.flpnEventTree.Children.Add(this.tbEventTree);
            this.flpnEventTree.Name = "flpnEventTree";
            //
            // llEvent
            //            this.llEvent.Name = "llEvent";
            this.llEvent.LinkClicked += new SimPe.Scenegraph.Compat.LinkLabelLinkClickedEventHandler(this.llEvent_LinkClicked);
            //
            // tbValEventTree
            //            this.tbValEventTree.Name = "tbValEventTree";
            //
            // btnEventTree
            //            this.btnEventTree.Name = "btnEventTree";
            this.btnEventTree.Click += (s, e) => this.btnEventTree_Click(s, e);
            //
            // tbEventTree
            //            this.tbEventTree.Name = "tbEventTree";
            this.tbEventTree.IsReadOnly = true;
            // flpnOptions
            //            this.flpnOptions.Children.Add(this.groupBox1);
            this.flpnOptions.Children.Add(this.groupBox2);
            this.flpnOptions.Children.Add(this.gbPriority);
            this.flpnOptions.Name = "flpnOptions";
            //
            // groupBox1
            //            this.groupBox1.Children.Add(this.flpnOptions1);
            this.groupBox1.Name = "groupBox1";
            // flpnOptions1
            //            this.flpnOptions1.Children.Add(this.ckbFlipped);
            this.flpnOptions1.Children.Add(this.ckbAnimSpeed);
            this.flpnOptions1.Children.Add(this.ckbParam);
            this.flpnOptions1.Children.Add(this.ckbInterruptible);
            this.flpnOptions1.Children.Add(this.ckbStartTag);
            this.flpnOptions1.Children.Add(this.ckbLoopCount);
            this.flpnOptions1.Children.Add(this.ckbTransToIdle);
            this.flpnOptions1.Children.Add(this.ckbBlendOut);
            this.flpnOptions1.Children.Add(this.ckbBlendIn);
            this.flpnOptions1.Name = "flpnOptions1";
            //
            // ckbFlipped
            //            this.ckbFlipped.Name = "ckbFlipped";
            // ckbAnimSpeed
            //            this.ckbAnimSpeed.Name = "ckbAnimSpeed";
            // ckbParam
            //            this.ckbParam.Name = "ckbParam";
            this.ckbParam.IsCheckedChanged += (s, e) => this.ckbParam_CheckedChanged(s, e);
            //
            // ckbInterruptible
            //            this.ckbInterruptible.Name = "ckbInterruptible";
            // ckbStartTag
            //            this.ckbStartTag.Name = "ckbStartTag";
            // ckbLoopCount
            //            this.ckbLoopCount.Name = "ckbLoopCount";
            // ckbTransToIdle
            //            this.ckbTransToIdle.Name = "ckbTransToIdle";
            // ckbBlendOut
            //            this.ckbBlendOut.Name = "ckbBlendOut";
            // ckbBlendIn
            //            this.ckbBlendIn.Name = "ckbBlendIn";
            // groupBox2
            //            this.groupBox2.Children.Add(this.flpnOptions2);
            this.groupBox2.Name = "groupBox2";
            // flpnOptions2
            //            this.flpnOptions2.Children.Add(this.ckbFlipTemp3);
            this.flpnOptions2.Children.Add(this.ckbSync);
            this.flpnOptions2.Children.Add(this.ckbAlignBlend);
            this.flpnOptions2.Children.Add(this.ckbControllerIsSource);
            this.flpnOptions2.Children.Add(this.ckbNotHurryable);
            this.flpnOptions2.Name = "flpnOptions2";
            //
            // ckbFlipTemp3
            //            this.ckbFlipTemp3.Name = "ckbFlipTemp3";
            this.ckbFlipTemp3.IsCheckedChanged += (s, e) => this.ckbFlipTemp3_CheckedChanged(s, e);
            //
            // ckbSync
            //            this.ckbSync.Name = "ckbSync";
            // ckbAlignBlend
            //            this.ckbAlignBlend.Name = "ckbAlignBlend";
            // ckbControllerIsSource
            //            this.ckbControllerIsSource.Name = "ckbControllerIsSource";
            // ckbNotHurryable
            //            this.ckbNotHurryable.Name = "ckbNotHurryable";
            // gbPriority
            //            this.gbPriority.Children.Add(this.cbPriority);
            this.gbPriority.Name = "gbPriority";
            // cbPriority
            //
            //
            // UI
            //            this.Controls.Add(this.pnWizAnimate);

        }
        #endregion

        private void llEvent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pjse.FileTable.Entry item = inst.Parent.ResourceByInstance(SimPe.Data.MetaData.BHAV_FILE, doidEvent.Value);
            Bhav b = new Bhav();
            b.ProcessData(item.PFD, item.Package);

            SimPe.PackedFiles.UserInterface.BhavForm ui = (SimPe.PackedFiles.UserInterface.BhavForm)b.UIHandler;
            ui.Tag = "Popup"; // tells the SetReadOnly function it's in a popup - so everything locked down
            string bhavTitle = pjse.Localization.GetString("viewbhav")
                + ": " + b.FileName + " [" + b.Package.SaveFileName + "]";
            b.RefreshUI();
            new Avalonia.Controls.Window { Title = bhavTitle, Content = ui }.Show();
        }

        private void btnEventTree_Click(object sender, EventArgs e)
        {
            pjse.FileTable.Entry item = new pjse.ResourceChooser().Execute(SimPe.Data.MetaData.BHAV_FILE, inst.Parent.FileDescriptor.Group, this, false);
            if (item != null)
                tbValEventTree.Text = "0x" + SimPe.Helper.HexString((ushort)item.Instance);
        }

        private void btnAnim_Click(object sender, EventArgs e)
        {
            this.doStrChooser(this.tbValAnim, this.tbAnim);
        }

        private void ckbParam_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            doCkbParam();
        }

        private void ckbFlipTemp3_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            ckbFlipped.IsEnabled = ckbFlipTemp3.IsChecked == true != true;
        }

        private void cbAnimType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            internalchg = true;

            try
            {
                if (this.cbAnimType.SelectedIndex >= 0)
                {
                    GS.GlobalStr gs = (GS.GlobalStr)Enum.Parse(typeof(GS.GlobalStr), this.cbAnimType.SelectedItem.ToString());
                    tbValAnimType.Text = "0x" + ((ushort)gs).ToString("X");
                }
            }
            finally
            {
                tbAnimType.Text = (this.cbAnimType.SelectedIndex >= 0) ? this.cbAnimType.SelectedItem.ToString() : "---";
            }
            doStrValue(doidAnim.Value, tbAnim);
            tbValAnimType.Focus();

            internalchg = false;
        }

    }

}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWizAnimate : pjse.ABhavOperandWiz
	{
        public BhavOperandWizAnimate(Instruction i, String mode) : base(i) { myForm = new WizAnimate.UI(mode); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
