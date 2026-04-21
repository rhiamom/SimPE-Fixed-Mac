/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
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
using System.ComponentModel;
using Avalonia.Controls;
using SimPe.Scenegraph.Compat;
using SimPe.PackedFiles.Wrapper;

namespace pjse.BhavOperandWizards.WizRaw
{
	/// <summary>
	/// Summary description for BhavInstruction.
	/// </summary>
    internal class UI : Window, iBhavOperandWizForm
	{
		#region Form variables
		internal StackPanel pnWizRaw;
		private TextBoxCompat tbRaw;
		#endregion

		public UI()
		{
			InitializeComponent();
		}

        #region iBhavOperandWizForm
        public StackPanel WizPanel { get { return this.pnWizRaw; } }

		public void Execute(Instruction inst)
		{
			string s = "";
			for (int i = 0; i < 8; i++)
				s += SimPe.Helper.HexString(inst.Operands[i]);
			for (int i = 0; i < 8; i++)
				s += SimPe.Helper.HexString(inst.Reserved1[i]);
			tbRaw.Text = s;
		}

        public Instruction Write(Instruction inst)
        {
            try
            {
                string s = tbRaw.Text + "00000000000000000000000000000000";
                for (int i = 0; i < 8; i++)
                    inst.Operands[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
                for (int i = 0; i < 8; i++)
                    inst.Reserved1[i] = Convert.ToByte(s.Substring((i + 8) * 2, 2), 16);

                return inst;
            }
            catch (Exception ex)
            {
                SimPe.Helper.ExceptionMessage(pjse.Localization.GetString("errconvert"), ex);
                return null;
            }
        }

        #endregion

		private void InitializeComponent()
		{
            this.pnWizRaw = new StackPanel { Name = "pnWizRaw", Margin = new Avalonia.Thickness(12, 8) };
            this.tbRaw = new TextBoxCompat
            {
                Name = "tbRaw",
                FontFamily = new Avalonia.Media.FontFamily("Consolas,Courier New,monospace"),
                MinWidth = 380,
                MinHeight = 24,
            };
            this.pnWizRaw.Children.Add(this.tbRaw);

            // NOTE: deliberately NOT setting `this.Content = pnWizRaw`.
            // The outer BhavOperandWiz dialog reparents pnWizRaw into its own layout,
            // and Avalonia throws if a control already has a parent.
		}

	}

}

namespace pjse.BhavOperandWizards
{
	public class BhavOperandWizRaw : pjse.ABhavOperandWiz
	{
		public BhavOperandWizRaw(Instruction i) : base(i) { myForm = new WizRaw.UI(); }

		#region IDisposable Members
		public override void Dispose()
		{
			if (myForm != null) myForm = null;
		}
		#endregion

	}
}

