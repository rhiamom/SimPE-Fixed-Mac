/***************************************************************************
 *   Copyright (C) 2005-2008 by Peter L Jones                              *
 *   pljones@users.sf.net                                                  *
 *                                                                         *
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSwaeatShop                                *
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
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Input;
using SimPe.Scenegraph.Compat;
using SimPe.PackedFiles.Wrapper;

namespace pjse
{
	/// <summary>
	/// Container for bhavPrimWizPanel from BhavOperandWizProvider
	/// </summary>
	public class BhavOperandWiz : Window
	{
		#region Form variables
		private DockPanel rootPanel;
		private ContentControl wizHost;
		private ButtonCompat OK;
		private ButtonCompat Cancel;
		#endregion

		public BhavOperandWiz()
		{
			InitializeComponent();
        }

		public void Dispose()
		{
		}


		private bool _dialogResult = false;

		public Instruction Execute(BhavWiz i, int wizType)
		{
			return ExecuteAsync(i, wizType).GetAwaiter().GetResult();
		}

		public async System.Threading.Tasks.Task<Instruction> ExecuteAsync(BhavWiz i, int wizType)
		{
			pjse.ABhavOperandWiz wiz = null;
			string title = "Operand Wizard";
			switch(wizType)
			{
				case 0: wiz = new pjse.BhavOperandWizards.BhavOperandWizRaw(i); title = "Raw Operand Entry"; break;
				case 1: wiz = i.Wizard(); title = "Operand Wizard"; break;
				default: wiz = new pjse.BhavOperandWizards.BhavOperandWizDefault(i); break;
			}
			if (wiz == null) return null;

			StackPanel pn = wiz.bhavPrimWizPanel;
			this.wizHost.Content = pn;   // attach the wizard's own UI panel above the OK/Cancel row

			this.Title = title;
			wiz.Execute();

			// Avalonia 11 requires a non-null owner for ShowDialog; use the main window.
			var owner = (Avalonia.Application.Current?.ApplicationLifetime
				as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime)?.MainWindow;
			if (owner != null)
				await this.ShowDialog(owner);
			else
				this.Show();

			if (_dialogResult)
				return wiz.Write();
			return null;
		}

		private void InitializeComponent()
		{
			this.Title = "Operand Wizard";
			this.SizeToContent = SizeToContent.WidthAndHeight;

			this.wizHost = new ContentControl();

			this.OK     = new ButtonCompat { Content = "OK",     MinWidth = 72, MinHeight = 24 };
			this.Cancel = new ButtonCompat { Content = "Cancel", MinWidth = 72, MinHeight = 24, Margin = new Avalonia.Thickness(8, 0, 0, 0) };
			this.OK.Click     += (s, e) => { _dialogResult = true;  Close(); };
			this.Cancel.Click += (s, e) => { _dialogResult = false; Close(); };

			var buttonRow = new StackPanel
			{
				Orientation = Avalonia.Layout.Orientation.Horizontal,
				HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
				Margin = new Avalonia.Thickness(12, 4, 12, 10),
			};
			buttonRow.Children.Add(this.OK);
			buttonRow.Children.Add(this.Cancel);

			this.rootPanel = new DockPanel();
			DockPanel.SetDock(buttonRow, Dock.Bottom);
			this.rootPanel.Children.Add(buttonRow);
			this.rootPanel.Children.Add(this.wizHost); // fills remaining

			this.Content = this.rootPanel;
		}

	}

    public interface iBhavOperandWizForm
    {
        StackPanel WizPanel { get; }
        void Execute(Instruction inst);
        Instruction Write(Instruction inst);
    }

	/// <summary>
	/// Provides the operand wizard for a given Bhav Instruction.
	/// </summary>
	/// <summary>
	/// Abstract class for BHAV Operand Wizards to extend
	/// </summary>
	public abstract class ABhavOperandWiz : IDisposable
	{
		protected Instruction instruction = null;
        protected iBhavOperandWizForm myForm = null;
		protected ABhavOperandWiz(Instruction instruction) { this.instruction = instruction; }

        public virtual StackPanel bhavPrimWizPanel { get { return myForm.WizPanel; } }
        public virtual void Execute() { myForm.Execute(instruction); }
        public virtual Instruction Write()
        {
            //for (int i = 0; i < 8; i++) instruction.Operands[i] = 0;
            //for (int i = 0; i < 8; i++) instruction.Reserved1[i] = 0;
            return myForm.Write(instruction);
        }

		#region IDisposable Members
		public abstract void Dispose();
		#endregion
	}

}
namespace pjse.BhavOperandWizards
{
	public class DataOwnerControl : IDisposable, IDataOwner
	{
		#region Form variables
		private ComboBoxCompat cbDataOwner;
		private ComboBoxCompat cbPicker;
		private TextBoxCompat tbValue;
        private CheckBoxCompat2 ckbDecimal;
        private CheckBoxCompat2 ckbUseInstancePicker;
        private LabelCompat lbInstance;
		#endregion

		#region Form event handlers
		private void cbDataOwner_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
			if (cbDataOwner.SelectedIndex >= 0)
				SetDataOwner();
		}

		private void cbPicker_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (internalchg) return;
            if (cbPicker.SelectedIndex >= 0)
            {
                SetValue((ushort)(cbPicker.SelectedIndex));
                tbValue.Text = tbValueConverter(instance);
            }
		}


		private void tbValue_Enter(object sender, System.EventArgs e)
		{
			((TextBoxCompat)sender).SelectAll();
		}

		private void tbValue_TextChanged(object sender, System.EventArgs ev)
		{
			if (internalchg) return;
			if (!tbValue_IsValid((TextBoxCompat)sender)) return;
			SetValue(tbValueConverter((TextBoxCompat)sender));
		}

		private void tbValue_Validated(object sender, System.EventArgs e)
		{
			bool origstate = internalchg;
			internalchg = true;
			((TextBoxCompat)sender).Text = tbValueConverter(instance);
			internalchg = origstate;
		}

        private void ckbDecimal_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.ckbDecimal.IsVisible)
                pjse.Settings.PJSE.DecimalDOValue = this.ckbDecimal.IsChecked == true;
        }

        private void ckbUseAttrPicker_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.ckbUseInstancePicker.IsVisible)
                pjse.Settings.PJSE.InstancePickerAsText = this.ckbUseInstancePicker.IsChecked == true;
        }

		#endregion

		#region Form validation
        private bool tbValue_IsValid(TextBoxCompat tb)
		{
			try
			{
				ushort v = tbValueConverter(tb);
                return (v < 1 << bitsInValue);
			}
			catch
			{
				return false;
			}
		}

		private string tbValueConverter(ushort v)
		{
			if      (dataOwner == 0x1a) return pjse.BhavWiz.ExpandBCONtoString(v, false);
			else if (dataOwner == 0x2f) return pjse.BhavWiz.ExpandBCONtoString(v, true);
			else if (isDecimal) return ((short)v).ToString();

            String s = SimPe.Helper.HexString(v);
            int i = (bitsInValue + 3) / 4;
            s = s.Substring(s.Length - i);

			return (use0xPrefix ? "0x" : "") + s;
		}

		private ushort tbValueConverter(TextBoxCompat sender)
		{
            if      (dataOwner == 0x1a) return pjse.BhavWiz.StringtoExpandBCON(sender.Text, false);
            else if (dataOwner == 0x2f) return pjse.BhavWiz.StringtoExpandBCON(sender.Text, true);
            else if (isDecimal)         return (ushort)Convert.ToInt16(sender.Text, 10);
            else                        return Convert.ToUInt16(sender.Text, 16);
		}

		#endregion

        public DataOwnerControl(BhavWiz inst, ComboBoxCompat cbDataOwner, ComboBoxCompat cbPicker, TextBoxCompat tbValue,
            CheckBoxCompat2 ckbDecimal, CheckBoxCompat2 ckbUseInstancePicker, LabelCompat lbInstance, byte dataOwner, ushort instance)
        {
            bitsInValue = 16;
            SetDataOwnerControl(inst, cbDataOwner, cbPicker, tbValue, ckbDecimal, ckbUseInstancePicker, lbInstance, dataOwner, instance);
        }

        public DataOwnerControl(BhavWiz inst, ComboBoxCompat cbDataOwner, ComboBoxCompat cbPicker, TextBoxCompat tbValue,
            CheckBoxCompat2 ckbDecimal, CheckBoxCompat2 ckbUseInstancePicker, LabelCompat lbInstance, byte dataOwner, byte instance)
        {
            bitsInValue = 8;
            SetDataOwnerControl(inst, cbDataOwner, cbPicker, tbValue, ckbDecimal, ckbUseInstancePicker, lbInstance, dataOwner, instance);
        }

        public void SetDataOwnerControl(BhavWiz inst, ComboBoxCompat cbDataOwner, ComboBoxCompat cbPicker, TextBoxCompat tbValue,
            CheckBoxCompat2 ckbDecimal, CheckBoxCompat2 ckbUseInstancePicker, LabelCompat lbInstance, byte dataOwner, ushort instance)
        {
            this.Dispose();
            this.inst = inst;
            this.cbDataOwner = cbDataOwner;
            this.cbPicker = cbPicker;
            this.tbValue = tbValue;
            this.ckbDecimal = ckbDecimal;
            this.ckbUseInstancePicker = ckbUseInstancePicker;
            this.lbInstance = lbInstance;
            this.dataOwner = dataOwner;
            this.instance = instance;

            this.flagsFor = null;

            internalchg = true;
            if (this.cbDataOwner != null)
            {
                this.cbDataOwner.Items.Clear();
                foreach (var item in BhavWiz.readStr(GS.BhavStr.DataOwners))
                    this.cbDataOwner.Items.Add(item);
                if (cbDataOwner.Items.Count > dataOwner)
                    cbDataOwner.SelectedIndex = dataOwner;
                this.cbDataOwner.SelectionChanged += (s, e) => this.cbDataOwner_SelectedIndexChanged(s, e);
            }

            if (this.cbPicker != null)
                this.cbPicker.SelectionChanged += (s, e) => this.cbPicker_SelectedIndexChanged(s, e);

            if (this.tbValue != null)
            {
                this.tbValue.Text = this.tbValueConverter(instance);
                this.tbValue.LostFocus += (s, e) => this.tbValue_Validated(s, e);
                this.tbValue.TextChanged += (s, e) => this.tbValue_TextChanged(s, e);
                this.tbValue.GotFocus += (s, e) => this.tbValue_Enter(s, e);
            }

            pjse.Settings.PJSE.DecimalDOValueChanged += new EventHandler(PJSE_DecimalDOValueChanged);
            Decimal = pjse.Settings.PJSE.DecimalDOValue;
            if (this.ckbDecimal != null)
            {
                this.ckbDecimal.IsChecked = Decimal;
                this.ckbDecimal.IsCheckedChanged += (s, e) => this.ckbDecimal_CheckedChanged(s, e);
            }

            pjse.Settings.PJSE.InstancePickerAsTextChanged += new EventHandler(PJSE_InstancePickerAsTextChanged);
            UseInstancePicker = pjse.Settings.PJSE.InstancePickerAsText;
            if (this.ckbUseInstancePicker != null)
            {
                this.ckbUseInstancePicker.IsChecked = UseInstancePicker;
                this.ckbUseInstancePicker.IsCheckedChanged += (s, e) => this.ckbUseAttrPicker_CheckedChanged(s, e);
            }

            internalchg = false;

            SetDataOwner();

            setTextBoxLength();
            setInstanceLabel();
        }

        void PJSE_DecimalDOValueChanged(object sender, EventArgs e)
        {
            Decimal = pjse.Settings.PJSE.DecimalDOValue;
            if (ckbDecimal != null && (this.ckbDecimal.IsChecked == true) != Decimal)
                this.ckbDecimal.IsChecked = Decimal;
        }

        void PJSE_InstancePickerAsTextChanged(object sender, EventArgs e)
        {
            UseInstancePicker = pjse.Settings.PJSE.InstancePickerAsText;
            if (ckbUseInstancePicker != null && (this.ckbUseInstancePicker.IsChecked == true) != UseInstancePicker)
                this.ckbUseInstancePicker.IsChecked = UseInstancePicker;
        }


		#region IDisposable Members

		public void Dispose()
		{
            // Event unsubscription skipped — controls will be nulled below; lambda refs can't be unsubscribed anyway
            // IsCheckedChanged unsubscription skipped - ckb controls will be nulled below

            this.inst = null;
            this.cbDataOwner = null;
            this.cbPicker = null;
            this.tbValue = null;
            this.ckbDecimal = null;
            this.ckbUseInstancePicker = null;
            this.lbInstance = null;
            this.flagsFor = null;
        }

		#endregion

		#region IDataOwner Members

		private byte dataOwner = 0;
		private ushort instance = 0;
		public byte DataOwner { get { return dataOwner; } }

		public ushort Value { get { return instance; } }

        public event EventHandler DataOwnerControlChanged;
        internal virtual void OnDataOwnerControlChanged(object sender, EventArgs e)
        {
            if (DataOwnerControlChanged != null)
            {
                DataOwnerControlChanged(sender, e);
            }
        }

		#endregion

		private void SetDataOwner()
		{
			if (internalchg)
				return;

			internalchg = true;

            if (cbDataOwner != null && cbDataOwner.SelectedIndex != (int?)dataOwner)
            {
                dataOwner = (byte)(cbDataOwner.SelectedIndex);
                setTextBoxLength();
                tbValue.Text = tbValueConverter(instance);
                OnDataOwnerControlChanged(this, new EventArgs());
            }

			#region pickerNames
            List<String> pickerNames = null;
            if (useInstancePicker && cbPicker != null)
            {
                if (useFlagNames && dataOwner == 0x07 && flagsFor != null)
                {
                    pickerNames = BhavWiz.flagNames(flagsFor.DataOwner, flagsFor.Value);
                    if (pickerNames != null)
                    {
                        pickerNames = new List<string>(pickerNames);
                        pickerNames.Insert(0, "[0: " + pjse.Localization.GetString("invalid") + "]");
                    }
                }
                else if (inst != null && useInstancePicker && (dataOwner == 0x00 || dataOwner == 0x01))
                {
                    pickerNames = inst.GetAttrNames(Scope.Private);
                }
                else if (inst != null && useInstancePicker && (dataOwner == 0x02 || dataOwner == 0x05))
                {
                    pickerNames = inst.GetAttrNames(Scope.SemiGlobal);
                }
                else if (inst != null && dataOwner == 0x09 || dataOwner == 0x16 || dataOwner == 0x32) // Param
                {
                    pickerNames = inst.GetTPRPnames(false);
                }
                else if (inst != null && dataOwner == 0x19) // Local
                {
                    pickerNames = inst.GetTPRPnames(true);
                }
                else if (inst != null && useInstancePicker && (dataOwner >= 0x29 && dataOwner <= 0x2F))
                {
                    pickerNames = inst.GetArrayNames();
                }
                else if (BhavWiz.doidGStr[dataOwner] != null)
                {
                    pickerNames = BhavWiz.readStr((GS.BhavStr)BhavWiz.doidGStr[dataOwner]);
                }
            }
			#endregion


            if (pickerNames != null && pickerNames.Count > 0)
            {
                if (tbValue != null)
                    tbValue.IsVisible = false;
                cbPicker.IsVisible = true;
                cbPicker.Items.Clear();
                foreach (var item in pickerNames) cbPicker.Items.Add(item);
                cbPicker.SelectedIndex = (cbPicker.Items.Count > instance) ? instance : -1;
            }
            else
            {
                if (cbPicker != null)
                    cbPicker.IsVisible = false;
                if (tbValue != null)
                    tbValue.IsVisible = true;
            }

            setInstanceLabel();

			internalchg = false;
		}

        private void setInstanceLabel()
        {
            if (lbInstance != null)
            {
                string s = "";
                if (inst != null)
                {
                    List<string> labels = null;
                    if (useFlagNames && dataOwner == 0x07 && flagsFor != null)
                    {
                        labels = BhavWiz.flagNames(flagsFor.DataOwner, flagsFor.Value);
                        if (labels != null)
                        {
                            labels = new List<string>(labels);
                            labels.Insert(0, "[0: " + pjse.Localization.GetString("invalid") + "]");
                        }
                    }
                    else if (dataOwner == 0x00 || dataOwner == 0x01)
                    {
                        labels = inst.GetAttrNames(Scope.Private);
                    }
                    else if (dataOwner == 0x02 || dataOwner == 0x05)
                    {
                        labels = inst.GetAttrNames(Scope.SemiGlobal);
                    }
                    else if (dataOwner == 0x09 || dataOwner == 0x16 || dataOwner == 0x32) // Param
                    {
                        labels = inst.GetTPRPnames(false);
                    }
                    else if (dataOwner == 0x19) // Local
                    {
                        labels = inst.GetTPRPnames(true);
                    }
                    else if (dataOwner >= 0x29 && dataOwner <= 0x2F)
                    {
                        labels = inst.GetArrayNames();
                    }
                    else if (BhavWiz.doidGStr[dataOwner] != null)
                    {
                        labels = BhavWiz.readStr((GS.BhavStr)BhavWiz.doidGStr[dataOwner]);
                    }

                    if (labels != null)
                    {
                        if (instance < labels.Count)
                            s = cbDataOwner.SelectedItem?.ToString() + ": " + labels[instance];
                    }
                    else if (dataOwner == 0x1a)
                    {
                        ushort[] bcon = BhavWiz.ExpandBCON(instance, false);
                        s = ((BhavWiz)inst).readBcon(bcon[0], bcon[1], false, true);
                    }
                }
                lbInstance.Content = s;
            }
        }

        private static int[] decBitToDigits = new int[] { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5 };
        private void setTextBoxLength()
        {
            if (this.tbValue != null)
                tbValue.MaxLength = Convert.ToInt32(
                    (this.dataOwner == 0x1a) ? 13 : (this.dataOwner == 0x2f) ? 15 :
                    isDecimal ? 1 + decBitToDigits[bitsInValue - 1] : (use0xPrefix ? 2 : 0) + ((bitsInValue - 1) / 4) + 1
                    );
        }


        public BhavWiz Instruction
        {
            get { return this.inst; }
            set
            {
                if (this.inst != value)
                {
                    this.inst = value;
                    SetDataOwner();
                }
            }
        }


        private int bitsInValue = 16;
        public bool ValueIsByte
        {
            get { return bitsInValue == 8; }
            set
            {
                if ((bitsInValue == 8) != value)
                {
                    bitsInValue = value ? 8 : 16;
                    setTextBoxLength();
                    //setConstLabel();
                    internalchg = true;
                    if (tbValue != null)
                    {
                        tbValue.Text = tbValueConverter(instance);
                    }
                    internalchg = false;
                }
            }
        }

		private void SetValue(ushort i)
		{
			if (instance != i)
			{
				instance = i;
                setInstanceLabel();
                OnDataOwnerControlChanged(this, new EventArgs());
			}
		}

		private bool internalchg = false;
		private BhavWiz inst;

        private bool use0xPrefix = true;
        public bool Use0xPrefix
        {
            get { return use0xPrefix; }
            set
            {
                if (use0xPrefix != value)
                {
                    use0xPrefix = value;
                    setTextBoxLength();
                    //setConstLabel();
                    internalchg = true;
                    if (tbValue != null)
                    {
                        tbValue.Text = tbValueConverter(instance);
                    }
                    internalchg = false;
                }
            }
        }

        private bool isDecimal = false;
		public bool Decimal
		{
			get { return this.isDecimal; }

			set
			{
				if (isDecimal != value)
				{
					isDecimal = value;
                    setTextBoxLength();
                    //setConstLabel();
                    internalchg = true;
                    if (tbValue != null)
                    {
                        tbValue.Text = tbValueConverter(instance);
                    }
					internalchg = false;
				}
			}

		}

		private bool useInstancePicker = true;
		public bool UseInstancePicker
		{
			get { return this.useInstancePicker; }

			set
			{
				if (useInstancePicker != value)
				{
					useInstancePicker = value;
					SetDataOwner();
				}
			}

		}

		private bool useFlagNames = false;
		public bool UseFlagNames
		{
			get { return this.useFlagNames; }

			set
			{
				if (useFlagNames != value)
				{
					useFlagNames = value;
					SetDataOwner();
				}
			}

		}

		private IDataOwner flagsFor = null;
		public IDataOwner FlagsFor
		{
            get { return flagsFor; }
			set
			{
                if (flagsFor != value)
                {
                    if (flagsFor != null)
                        flagsFor.DataOwnerControlChanged -= new EventHandler(value_DataOwnerControlChanged);
                    flagsFor = value;
                    flagsFor.DataOwnerControlChanged += new EventHandler(value_DataOwnerControlChanged);
                }
			}
		}
        void value_DataOwnerControlChanged(object sender, EventArgs e) { SetDataOwner(); }

    }

}
