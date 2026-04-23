/***************************************************************************
 *   Copyright (C) 2005-2008 by Peter L Jones                              *
 *   pljones@users.sf.net                                                  *
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
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
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.Wiz0x0002
{
	/// <summary>
	/// Summary description for BhavInstruction.
	/// </summary>
    internal class UI : Window, iBhavOperandWizForm
    {
        #region Form variables

        internal StackPanel pnWiz0x0002;
        private ComboBoxCompat cbOperator;
        private LabelledDataOwner labelledDataOwner1;
        private LabelledDataOwner labelledDataOwner2;
        private FlowLayoutPanel flowLayoutPanel1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
                #endregion

        public UI()
        {
            //
            // Required designer variable.
            //
            InitializeComponent();

            cbOperator.Items.Clear();
            cbOperator.Items.AddRange(BhavWiz.readStr(GS.BhavStr.Operators).ToArray());

            labelledDataOwner2.Decimal = labelledDataOwner1.Decimal = pjse.Settings.PJSE.DecimalDOValue;
            labelledDataOwner2.UseInstancePicker = labelledDataOwner1.UseInstancePicker = pjse.Settings.PJSE.InstancePickerAsText;

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


        private Instruction inst = null;

        #region iBhavOperandWizForm
        public StackPanel WizPanel { get { return this.pnWiz0x0002; } }

        public void Execute(Instruction inst)
        {
            this.inst = labelledDataOwner1.Instruction = labelledDataOwner2.Instruction = inst;

            wrappedByteArray ops = inst.Operands;

            labelledDataOwner1.Value = BhavWiz.ToShort(ops[0x00], ops[0x01]);
            labelledDataOwner1.DataOwner = ops[0x06];

            cbOperator.SelectedIndex = (cbOperator.Items.Count > ops[0x05]) ? ops[0x05] : -1;

            labelledDataOwner2.Value = BhavWiz.ToShort(ops[0x02], ops[0x03]);
            labelledDataOwner2.DataOwner = ops[0x07];
        }

        public Instruction Write(Instruction inst)
        {
            if (inst != null)
            {
                wrappedByteArray ops = inst.Operands;

                ops[0x06] = labelledDataOwner1.DataOwner;
                BhavWiz.FromShort(ref ops, 0, labelledDataOwner1.Value);

                ops[0x05] = (byte)(cbOperator.SelectedIndex);

                ops[0x07] = labelledDataOwner2.DataOwner;
                BhavWiz.FromShort(ref ops, 2, labelledDataOwner2.Value);
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
        {
            this.pnWiz0x0002 = new StackPanel { Name = "pnWiz0x0002", Margin = new Avalonia.Thickness(6) };
            this.labelledDataOwner2 = new pjse.LabelledDataOwner();
            this.labelledDataOwner1 = new pjse.LabelledDataOwner();
            this.cbOperator = new ComboBoxCompat { Name = "cbOperator", MinWidth = 70, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Top, Margin = new Avalonia.Thickness(6, 0) };
            this.flowLayoutPanel1 = new FlowLayoutPanel
            {
                Name = "flowLayoutPanel1",
                Orientation = Avalonia.Layout.Orientation.Vertical,
            };

            // labelledDataOwner2
            this.labelledDataOwner2.DataOwner = ((byte)(255));
            this.labelledDataOwner2.DataOwnerEnabled = true;
            this.labelledDataOwner2.FlagsFor = this.labelledDataOwner1;
            this.labelledDataOwner2.Instruction = null;
            this.labelledDataOwner2.LabelVisible = false;
            this.labelledDataOwner2.Name = "labelledDataOwner2";
            this.labelledDataOwner2.UseFlagNames = false;
            this.labelledDataOwner2.Value = ((ushort)(0));

            // labelledDataOwner1
            this.labelledDataOwner1.DataOwner = ((byte)(255));
            this.labelledDataOwner1.DataOwnerEnabled = true;
            this.labelledDataOwner1.DecimalVisible = false;
            this.labelledDataOwner1.Instruction = null;
            this.labelledDataOwner1.LabelVisible = false;
            this.labelledDataOwner1.Name = "labelledDataOwner1";
            this.labelledDataOwner1.UseFlagNames = false;
            this.labelledDataOwner1.UseInstancePickerVisible = false;
            this.labelledDataOwner1.Value = ((ushort)(0));

            this.cbOperator.SelectionChanged += (s, e) => this.cbOperator_SelectedIndexChanged(s, e);

            this.flowLayoutPanel1.Children.Add(this.labelledDataOwner1);
            this.flowLayoutPanel1.Children.Add(this.cbOperator);
            this.flowLayoutPanel1.Children.Add(this.labelledDataOwner2);

            // pnWiz0x0002 holds flowLayoutPanel1 (outer BhavOperandWiz adopts pnWiz0x0002)
            this.pnWiz0x0002.Children.Add(this.flowLayoutPanel1);
        }
        #endregion

        private void cbOperator_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            labelledDataOwner2.UseFlagNames = ((cbOperator.SelectedIndex) >= 8 && (cbOperator.SelectedIndex) <= 10);
        }
    }
}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWiz0x0002 : pjse.ABhavOperandWiz
	{
		public BhavOperandWiz0x0002(Instruction i) : base(i) { myForm = new Wiz0x0002.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}

}
