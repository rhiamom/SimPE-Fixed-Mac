/***************************************************************************
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
using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
    public class FunctionPackedFileUI : SimPe.Windows.Forms.WrapperBaseControl, IPackedFileUI
    {
        private Avalonia.Controls.TextBlock label2 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.StackPanel panel2 = new Avalonia.Controls.StackPanel();
        private Avalonia.Controls.TextBlock label1 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt1 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox1 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock commnt3 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox3 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label3 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt7 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox7 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label7 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt6 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox6 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label6 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt5 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox5 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label5 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt4 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox4 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label4 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt8 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox8 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label8 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt9 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox9 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label9 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt10 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox10 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label10 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt11 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox11 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label11 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt12 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox12 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label12 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt13 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox13 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label13 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt14 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox14 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label14 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt15 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox15 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label15 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt16 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox16 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label16 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt17 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox17 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label17 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt18 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox18 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label18 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt19 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox19 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label19 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt20 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox20 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label20 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt21 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox21 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label21 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt22 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox22 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label22 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt23 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox23 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label23 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt24 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox24 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label24 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt25 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox25 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label25 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt26 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox26 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label26 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt27 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox27 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label27 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt28 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox28 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label28 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt29 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox29 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label29 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt30 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox30 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label30 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt31 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox31 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label31 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt32 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox32 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label32 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt33 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox33 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label33 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt34 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox34 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label34 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt35 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox35 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label35 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt36 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox36 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label36 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt37 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox37 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label37 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt38 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox38 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label38 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt39 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox39 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label39 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt40 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox40 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label40 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt41 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox41 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label41 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt42 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox42 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label42 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt43 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox43 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label43 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt44 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox44 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label44 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt45 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox45 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label45 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt46 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox46 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label46 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt47 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox47 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label47 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt48 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox48 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label48 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt49 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox49 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label49 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt50 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox50 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label50 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt51 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox51 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label51 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt52 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox52 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label52 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt53 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox53 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label53 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt54 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox54 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label54 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt55 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox55 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label55 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock commnt56 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox textBox56 = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock label56 = new Avalonia.Controls.TextBlock();

        private void InitializeComponent() { }

        protected new FunctionPackedFileWrapper Wrapper
        {
            get { return base.Wrapper as FunctionPackedFileWrapper; }
        }
        public FunctionPackedFileWrapper TPFW
        {
            get { return (FunctionPackedFileWrapper)Wrapper; }
        }

        #region WrapperBaseControl Member

        public FunctionPackedFileUI()
        {
            InitializeComponent();
        }

        public override void RefreshGUI()
        {
            base.RefreshGUI();

            ushort numper = Wrapper.Quanty;
            if (numper > 0)
            {
                panel2.IsVisible = true;
                {
                    label1.Text = Wrapper.strung[0]; textBox1.Text = Wrapper.valwe[0].ToString(); commnt1.Text = Wrapper.comnt[0];
                    label1.IsVisible = textBox1.IsVisible = commnt1.IsVisible = true;
                }
                if (numper > 1)
                {
                    label3.Text = Wrapper.strung[1]; textBox3.Text = Wrapper.valwe[1].ToString(); commnt3.Text = Wrapper.comnt[1];
                    label3.IsVisible = textBox3.IsVisible = commnt3.IsVisible = true;
                }
                else { label3.IsVisible = textBox3.IsVisible = commnt3.IsVisible = false; }
                if (numper > 2)
                {
                    label4.Text = Wrapper.strung[2]; textBox4.Text = Wrapper.valwe[2].ToString(); commnt4.Text = Wrapper.comnt[2];
                    label4.IsVisible = textBox4.IsVisible = commnt4.IsVisible = true;
                }
                else { label4.IsVisible = textBox4.IsVisible = commnt4.IsVisible = false; }
                if (numper > 3)
                {
                    label5.Text = Wrapper.strung[3]; textBox5.Text = Wrapper.valwe[3].ToString(); commnt5.Text = Wrapper.comnt[3];
                    label5.IsVisible = textBox5.IsVisible = commnt5.IsVisible = true;
                }
                else { label5.IsVisible = textBox5.IsVisible = commnt5.IsVisible = false; }
                if (numper > 4)
                {
                    label6.Text = Wrapper.strung[4]; textBox6.Text = Wrapper.valwe[4].ToString(); commnt6.Text = Wrapper.comnt[4];
                    label6.IsVisible = textBox6.IsVisible = commnt6.IsVisible = true;
                }
                else { label6.IsVisible = textBox6.IsVisible = commnt6.IsVisible = false; }
                if (numper > 5)
                {
                    label7.Text = Wrapper.strung[5]; textBox7.Text = Wrapper.valwe[5].ToString(); commnt7.Text = Wrapper.comnt[5];
                    label7.IsVisible = textBox7.IsVisible = commnt7.IsVisible = true;
                }
                else { label7.IsVisible = textBox7.IsVisible = commnt7.IsVisible = false; }
                if (numper > 6)
                {
                    label8.Text = Wrapper.strung[6]; textBox8.Text = Wrapper.valwe[6].ToString(); commnt8.Text = Wrapper.comnt[6];
                    label8.IsVisible = textBox8.IsVisible = commnt8.IsVisible = true;
                }
                else { label8.IsVisible = textBox8.IsVisible = commnt8.IsVisible = false; }
                if (numper > 7)
                {
                    label9.Text = Wrapper.strung[7]; textBox9.Text = Wrapper.valwe[7].ToString(); commnt9.Text = Wrapper.comnt[7];
                    label9.IsVisible = textBox9.IsVisible = commnt9.IsVisible = true;
                }
                else { label9.IsVisible = textBox9.IsVisible = commnt9.IsVisible = false; }
                if (numper > 8)
                {
                    label10.Text = Wrapper.strung[8]; textBox10.Text = Wrapper.valwe[8].ToString(); commnt10.Text = Wrapper.comnt[8];
                    label10.IsVisible = textBox10.IsVisible = commnt10.IsVisible = true;
                }
                else { label10.IsVisible = textBox10.IsVisible = commnt10.IsVisible = false; }
                if (numper > 9)
                {
                    label11.Text = Wrapper.strung[9]; textBox11.Text = Wrapper.valwe[9].ToString(); commnt11.Text = Wrapper.comnt[9];
                    label11.IsVisible = textBox11.IsVisible = commnt11.IsVisible = true;
                }
                else { label11.IsVisible = textBox11.IsVisible = commnt11.IsVisible = false; }
                if (numper > 10)
                {
                    label12.Text = Wrapper.strung[10]; textBox12.Text = Wrapper.valwe[10].ToString(); commnt12.Text = Wrapper.comnt[10];
                    label12.IsVisible = textBox12.IsVisible = commnt12.IsVisible = true;
                }
                else { label12.IsVisible = textBox12.IsVisible = commnt12.IsVisible = false; }
                if (numper > 11)
                {
                    label13.Text = Wrapper.strung[11]; textBox13.Text = Wrapper.valwe[11].ToString(); commnt13.Text = Wrapper.comnt[11];
                    label13.IsVisible = textBox13.IsVisible = commnt13.IsVisible = true;
                }
                else { label13.IsVisible = textBox13.IsVisible = commnt13.IsVisible = false; }
                if (numper > 12)
                {
                    label14.Text = Wrapper.strung[12]; textBox14.Text = Wrapper.valwe[12].ToString(); commnt14.Text = Wrapper.comnt[12];
                    label14.IsVisible = textBox14.IsVisible = commnt14.IsVisible = true;
                }
                else { label14.IsVisible = textBox14.IsVisible = commnt14.IsVisible = false; }
                if (numper > 13)
                {
                    label15.Text = Wrapper.strung[13]; textBox15.Text = Wrapper.valwe[13].ToString(); commnt15.Text = Wrapper.comnt[13];
                    label15.IsVisible = textBox15.IsVisible = commnt15.IsVisible = true;
                }
                else { label15.IsVisible = textBox15.IsVisible = commnt15.IsVisible = false; }
                if (numper > 14)
                {
                    label16.Text = Wrapper.strung[14]; textBox16.Text = Wrapper.valwe[14].ToString(); commnt16.Text = Wrapper.comnt[14];
                    label16.IsVisible = textBox16.IsVisible = commnt16.IsVisible = true;
                }
                else { label16.IsVisible = textBox16.IsVisible = commnt16.IsVisible = false; }
                if (numper > 15)
                {
                    label17.Text = Wrapper.strung[15]; textBox17.Text = Wrapper.valwe[15].ToString(); commnt17.Text = Wrapper.comnt[15];
                    label17.IsVisible = textBox17.IsVisible = commnt17.IsVisible = true;
                }
                else { label17.IsVisible = textBox17.IsVisible = commnt17.IsVisible = false; }
                if (numper > 16)
                {
                    label18.Text = Wrapper.strung[16]; textBox18.Text = Wrapper.valwe[16].ToString(); commnt18.Text = Wrapper.comnt[16];
                    label18.IsVisible = textBox18.IsVisible = commnt18.IsVisible = true;
                }
                else { label18.IsVisible = textBox18.IsVisible = commnt18.IsVisible = false; }
                if (numper > 17)
                {
                    label19.Text = Wrapper.strung[17]; textBox19.Text = Wrapper.valwe[17].ToString(); commnt19.Text = Wrapper.comnt[17];
                    label19.IsVisible = textBox19.IsVisible = commnt19.IsVisible = true;
                }
                else { label19.IsVisible = textBox19.IsVisible = commnt19.IsVisible = false; }
                if (numper > 18)
                {
                    label20.Text = Wrapper.strung[18]; textBox20.Text = Wrapper.valwe[18].ToString(); commnt20.Text = Wrapper.comnt[18];
                    label20.IsVisible = textBox20.IsVisible = commnt20.IsVisible = true;
                }
                else { label20.IsVisible = textBox20.IsVisible = commnt20.IsVisible = false; }
                if (numper > 19)
                {
                    label21.Text = Wrapper.strung[19]; textBox21.Text = Wrapper.valwe[19].ToString(); commnt21.Text = Wrapper.comnt[19];
                    label21.IsVisible = textBox21.IsVisible = commnt21.IsVisible = true;
                }
                else { label21.IsVisible = textBox21.IsVisible = commnt21.IsVisible = false; }
                if (numper > 20)
                {
                    label22.Text = Wrapper.strung[20]; textBox22.Text = Wrapper.valwe[20].ToString(); commnt22.Text = Wrapper.comnt[20];
                    label22.IsVisible = textBox22.IsVisible = commnt22.IsVisible = true;
                }
                else { label22.IsVisible = textBox22.IsVisible = commnt22.IsVisible = false; }
                if (numper > 21)
                {
                    label23.Text = Wrapper.strung[21]; textBox23.Text = Wrapper.valwe[21].ToString(); commnt23.Text = Wrapper.comnt[21];
                    label23.IsVisible = textBox23.IsVisible = commnt23.IsVisible = true;
                }
                else { label23.IsVisible = textBox23.IsVisible = commnt23.IsVisible = false; }
                if (numper > 22)
                {
                    label24.Text = Wrapper.strung[22]; textBox24.Text = Wrapper.valwe[22].ToString(); commnt24.Text = Wrapper.comnt[22];
                    label24.IsVisible = textBox24.IsVisible = commnt24.IsVisible = true;
                }
                else { label24.IsVisible = textBox24.IsVisible = commnt24.IsVisible = false; }
                if (numper > 23)
                {
                    label25.Text = Wrapper.strung[23]; textBox25.Text = Wrapper.valwe[23].ToString(); commnt25.Text = Wrapper.comnt[23];
                    label25.IsVisible = textBox25.IsVisible = commnt25.IsVisible = true;
                }
                else { label25.IsVisible = textBox25.IsVisible = commnt25.IsVisible = false; }
                if (numper > 24)
                {
                    label26.Text = Wrapper.strung[24]; textBox26.Text = Wrapper.valwe[24].ToString(); commnt26.Text = Wrapper.comnt[24];
                    label26.IsVisible = textBox26.IsVisible = commnt26.IsVisible = true;
                }
                else { label26.IsVisible = textBox26.IsVisible = commnt26.IsVisible = false; }
                if (numper > 25)
                {
                    label27.Text = Wrapper.strung[25]; textBox27.Text = Wrapper.valwe[25].ToString(); commnt27.Text = Wrapper.comnt[25];
                    label27.IsVisible = textBox27.IsVisible = commnt27.IsVisible = true;
                }
                else { label27.IsVisible = textBox27.IsVisible = commnt27.IsVisible = false; }
                if (numper > 26)
                {
                    label28.Text = Wrapper.strung[26]; textBox28.Text = Wrapper.valwe[26].ToString(); commnt28.Text = Wrapper.comnt[26];
                    label28.IsVisible = textBox28.IsVisible = commnt28.IsVisible = true;
                }
                else { label28.IsVisible = textBox28.IsVisible = commnt28.IsVisible = false; }
                if (numper > 27)
                {
                    label29.Text = Wrapper.strung[27]; textBox29.Text = Wrapper.valwe[27].ToString(); commnt29.Text = Wrapper.comnt[27];
                    label29.IsVisible = textBox29.IsVisible = commnt29.IsVisible = true;
                }
                else { label29.IsVisible = textBox29.IsVisible = commnt29.IsVisible = false; }
                if (numper > 28)
                {
                    label30.Text = Wrapper.strung[28]; textBox30.Text = Wrapper.valwe[28].ToString(); commnt30.Text = Wrapper.comnt[28];
                    label30.IsVisible = textBox30.IsVisible = commnt30.IsVisible = true;
                }
                else { label30.IsVisible = textBox30.IsVisible = commnt30.IsVisible = false; }
                if (numper > 29)
                {
                    label31.Text = Wrapper.strung[29]; textBox31.Text = Wrapper.valwe[29].ToString(); commnt31.Text = Wrapper.comnt[29];
                    label31.IsVisible = textBox31.IsVisible = commnt31.IsVisible = true;
                }
                else { label31.IsVisible = textBox31.IsVisible = commnt31.IsVisible = false; }
                if (numper > 30)
                {
                    label32.Text = Wrapper.strung[30]; textBox32.Text = Wrapper.valwe[30].ToString(); commnt32.Text = Wrapper.comnt[30];
                    label32.IsVisible = textBox32.IsVisible = commnt32.IsVisible = true;
                }
                else { label32.IsVisible = textBox32.IsVisible = commnt32.IsVisible = false; }
                if (numper > 31)
                {
                    label33.Text = Wrapper.strung[31]; textBox33.Text = Wrapper.valwe[31].ToString(); commnt33.Text = Wrapper.comnt[31];
                    label33.IsVisible = textBox33.IsVisible = commnt33.IsVisible = true;
                }
                else { label33.IsVisible = textBox33.IsVisible = commnt33.IsVisible = false; }
                if (numper > 32)
                {
                    label34.Text = Wrapper.strung[32]; textBox34.Text = Wrapper.valwe[32].ToString(); commnt34.Text = Wrapper.comnt[32];
                    label34.IsVisible = textBox34.IsVisible = commnt34.IsVisible = true;
                }
                else { label34.IsVisible = textBox34.IsVisible = commnt34.IsVisible = false; }
                if (numper > 33)
                {
                    label35.Text = Wrapper.strung[33]; textBox35.Text = Wrapper.valwe[33].ToString(); commnt35.Text = Wrapper.comnt[33];
                    label35.IsVisible = textBox35.IsVisible = commnt35.IsVisible = true;
                }
                else { label35.IsVisible = textBox35.IsVisible = commnt35.IsVisible = false; }
                if (numper > 34)
                {
                    label36.Text = Wrapper.strung[34]; textBox36.Text = Wrapper.valwe[34].ToString(); commnt36.Text = Wrapper.comnt[34];
                    label36.IsVisible = textBox36.IsVisible = commnt36.IsVisible = true;
                }
                else { label36.IsVisible = textBox36.IsVisible = commnt36.IsVisible = false; }
                if (numper > 35)
                {
                    label37.Text = Wrapper.strung[35]; textBox37.Text = Wrapper.valwe[35].ToString(); commnt37.Text = Wrapper.comnt[35];
                    label37.IsVisible = textBox37.IsVisible = commnt37.IsVisible = true;
                }
                else { label37.IsVisible = textBox37.IsVisible = commnt37.IsVisible = false; }
                if (numper > 36)
                {
                    label38.Text = Wrapper.strung[36]; textBox38.Text = Wrapper.valwe[36].ToString(); commnt38.Text = Wrapper.comnt[36];
                    label38.IsVisible = textBox38.IsVisible = commnt38.IsVisible = true;
                }
                else { label38.IsVisible = textBox38.IsVisible = commnt38.IsVisible = false; }
                if (numper > 37)
                {
                    label39.Text = Wrapper.strung[37]; textBox39.Text = Wrapper.valwe[37].ToString(); commnt39.Text = Wrapper.comnt[37];
                    label39.IsVisible = textBox39.IsVisible = commnt39.IsVisible = true;
                }
                else { label39.IsVisible = textBox39.IsVisible = commnt39.IsVisible = false; }
                if (numper > 38)
                {
                    label40.Text = Wrapper.strung[38]; textBox40.Text = Wrapper.valwe[38].ToString(); commnt40.Text = Wrapper.comnt[38];
                    label40.IsVisible = textBox40.IsVisible = commnt40.IsVisible = true;
                }
                else { label40.IsVisible = textBox40.IsVisible = commnt40.IsVisible = false; }
                if (numper > 39)
                {
                    label41.Text = Wrapper.strung[39]; textBox41.Text = Wrapper.valwe[39].ToString(); commnt41.Text = Wrapper.comnt[39];
                    label41.IsVisible = textBox41.IsVisible = commnt41.IsVisible = true;
                }
                else { label41.IsVisible = textBox41.IsVisible = commnt41.IsVisible = false; }
                if (numper > 40)
                {
                    label42.Text = Wrapper.strung[40]; textBox42.Text = Wrapper.valwe[40].ToString(); commnt42.Text = Wrapper.comnt[40];
                    label42.IsVisible = textBox42.IsVisible = commnt42.IsVisible = true;
                }
                else { label42.IsVisible = textBox42.IsVisible = commnt42.IsVisible = false; }
                if (numper > 41)
                {
                    label43.Text = Wrapper.strung[41]; textBox43.Text = Wrapper.valwe[41].ToString(); commnt43.Text = Wrapper.comnt[41];
                    label43.IsVisible = textBox43.IsVisible = commnt43.IsVisible = true;
                }
                else { label43.IsVisible = textBox43.IsVisible = commnt43.IsVisible = false; }
                if (numper > 42)
                {
                    label44.Text = Wrapper.strung[42]; textBox44.Text = Wrapper.valwe[42].ToString(); commnt44.Text = Wrapper.comnt[42];
                    label44.IsVisible = textBox44.IsVisible = commnt44.IsVisible = true;
                }
                else { label44.IsVisible = textBox44.IsVisible = commnt44.IsVisible = false; }
                if (numper > 43)
                {
                    label45.Text = Wrapper.strung[43]; textBox45.Text = Wrapper.valwe[43].ToString(); commnt45.Text = Wrapper.comnt[43];
                    label45.IsVisible = textBox45.IsVisible = commnt45.IsVisible = true;
                }
                else { label45.IsVisible = textBox45.IsVisible = commnt45.IsVisible = false; }
                if (numper > 44)
                {
                    label46.Text = Wrapper.strung[44]; textBox46.Text = Wrapper.valwe[44].ToString(); commnt46.Text = Wrapper.comnt[44];
                    label46.IsVisible = textBox46.IsVisible = commnt46.IsVisible = true;
                }
                else { label46.IsVisible = textBox46.IsVisible = commnt46.IsVisible = false; }
                if (numper > 45)
                {
                    label47.Text = Wrapper.strung[45]; textBox47.Text = Wrapper.valwe[45].ToString(); commnt47.Text = Wrapper.comnt[45];
                    label47.IsVisible = textBox47.IsVisible = commnt47.IsVisible = true;
                }
                else { label47.IsVisible = textBox47.IsVisible = commnt47.IsVisible = false; }
                if (numper > 46)
                {
                    label48.Text = Wrapper.strung[46]; textBox48.Text = Wrapper.valwe[46].ToString(); commnt48.Text = Wrapper.comnt[46];
                    label48.IsVisible = textBox48.IsVisible = commnt48.IsVisible = true;
                }
                else { label48.IsVisible = textBox48.IsVisible = commnt48.IsVisible = false; }
                if (numper > 47)
                {
                    label49.Text = Wrapper.strung[47]; textBox49.Text = Wrapper.valwe[47].ToString(); commnt49.Text = Wrapper.comnt[47];
                    label49.IsVisible = textBox49.IsVisible = commnt49.IsVisible = true;
                }
                else { label49.IsVisible = textBox49.IsVisible = commnt49.IsVisible = false; }
                if (numper > 48)
                {
                    label50.Text = Wrapper.strung[48]; textBox50.Text = Wrapper.valwe[48].ToString(); commnt50.Text = Wrapper.comnt[48];
                    label50.IsVisible = textBox50.IsVisible = commnt50.IsVisible = true;
                }
                else { label50.IsVisible = textBox50.IsVisible = commnt50.IsVisible = false; }
                if (numper > 49)
                {
                    label51.Text = Wrapper.strung[49]; textBox51.Text = Wrapper.valwe[49].ToString(); commnt51.Text = Wrapper.comnt[49];
                    label51.IsVisible = textBox51.IsVisible = commnt51.IsVisible = true;
                }
                else { label51.IsVisible = textBox51.IsVisible = commnt51.IsVisible = false; }
                if (numper > 50)
                {
                    label52.Text = Wrapper.strung[50]; textBox52.Text = Wrapper.valwe[50].ToString(); commnt52.Text = Wrapper.comnt[50];
                    label52.IsVisible = textBox52.IsVisible = commnt52.IsVisible = true;
                }
                else { label52.IsVisible = textBox52.IsVisible = commnt52.IsVisible = false; }
                if (numper > 51)
                {
                    label53.Text = Wrapper.strung[51]; textBox53.Text = Wrapper.valwe[51].ToString(); commnt53.Text = Wrapper.comnt[51];
                    label53.IsVisible = textBox53.IsVisible = commnt53.IsVisible = true;
                }
                else { label53.IsVisible = textBox53.IsVisible = commnt53.IsVisible = false; }
                if (numper > 52)
                {
                    label54.Text = Wrapper.strung[52]; textBox54.Text = Wrapper.valwe[52].ToString(); commnt54.Text = Wrapper.comnt[52];
                    label54.IsVisible = textBox54.IsVisible = commnt54.IsVisible = true;
                }
                else { label54.IsVisible = textBox54.IsVisible = commnt54.IsVisible = false; }
                if (numper > 53)
                {
                    label55.Text = Wrapper.strung[53]; textBox55.Text = Wrapper.valwe[53].ToString(); commnt55.Text = Wrapper.comnt[53];
                    label55.IsVisible = textBox55.IsVisible = commnt55.IsVisible = true;
                }
                else { label55.IsVisible = textBox55.IsVisible = commnt55.IsVisible = false; }
                if (numper > 54)
                {
                    label56.Text = Wrapper.strung[54]; textBox56.Text = Wrapper.valwe[54].ToString(); commnt56.Text = Wrapper.comnt[54];
                    label56.IsVisible = textBox56.IsVisible = commnt56.IsVisible = true;
                }
                else { label56.IsVisible = textBox56.IsVisible = commnt56.IsVisible = false; }
            }
            else {panel2.IsVisible = false;}
        }

        public override void OnCommit()
        {
            base.OnCommit();
            TPFW.SynchronizeUserData(true, false);
        }
        #endregion

        #region IPackedFileUI Member
        Avalonia.Controls.Control IPackedFileUI.GUIHandle
        {
            get { return this; }
        }
        #endregion

        #region IDisposable Member

        void IDisposable.Dispose()
        {
            this.TPFW.Dispose();
        }
        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[0] = Convert.ToSingle(textBox1.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[1] = Convert.ToSingle(textBox3.Text);

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[2] = Convert.ToSingle(textBox4.Text);

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[3] = Convert.ToSingle(textBox5.Text);

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[4] = Convert.ToSingle(textBox6.Text);

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[5] = Convert.ToSingle(textBox7.Text);

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[6] = Convert.ToSingle(textBox8.Text);

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[7] = Convert.ToSingle(textBox9.Text);

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[8] = Convert.ToSingle(textBox10.Text);

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[9] = Convert.ToSingle(textBox11.Text);

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[10] = Convert.ToSingle(textBox12.Text);

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[11] = Convert.ToSingle(textBox13.Text);

        }
        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[12] = Convert.ToSingle(textBox14.Text);

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[13] = Convert.ToSingle(textBox15.Text);

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[14] = Convert.ToSingle(textBox16.Text);

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[15] = Convert.ToSingle(textBox17.Text);

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[16] = Convert.ToSingle(textBox18.Text);

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[17] = Convert.ToSingle(textBox19.Text);

        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[18] = Convert.ToSingle(textBox20.Text);

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[19] = Convert.ToSingle(textBox21.Text);

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[20] = Convert.ToSingle(textBox22.Text);

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[21] = Convert.ToSingle(textBox23.Text);

        }
        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[22] = Convert.ToSingle(textBox24.Text);

        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[23] = Convert.ToSingle(textBox25.Text);

        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[24] = Convert.ToSingle(textBox26.Text);

        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[25] = Convert.ToSingle(textBox27.Text);

        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[26] = Convert.ToSingle(textBox28.Text);

        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[27] = Convert.ToSingle(textBox29.Text);

        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[28] = Convert.ToSingle(textBox30.Text);

        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[29] = Convert.ToSingle(textBox31.Text);

        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[30] = Convert.ToSingle(textBox32.Text);

        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[31] = Convert.ToSingle(textBox33.Text);

        }

        private void textBox34_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[32] = Convert.ToSingle(textBox34.Text);

        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[33] = Convert.ToSingle(textBox35.Text);

        }

        private void textBox36_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[34] = Convert.ToSingle(textBox36.Text);

        }

        private void textBox37_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[35] = Convert.ToSingle(textBox37.Text);

        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[36] = Convert.ToSingle(textBox38.Text);

        }

        private void textBox39_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[37] = Convert.ToSingle(textBox39.Text);

        }

        private void textBox40_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[38] = Convert.ToSingle(textBox40.Text);

        }

        private void textBox41_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[39] = Convert.ToSingle(textBox41.Text);

        }

        private void textBox42_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[40] = Convert.ToSingle(textBox42.Text);

        }

        private void textBox43_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[41] = Convert.ToSingle(textBox43.Text);

        }

        private void textBox44_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[42] = Convert.ToSingle(textBox44.Text);

        }

        private void textBox45_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[43] = Convert.ToSingle(textBox45.Text);

        }

        private void textBox46_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[44] = Convert.ToSingle(textBox46.Text);

        }

        private void textBox47_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[45] = Convert.ToSingle(textBox47.Text);

        }

        private void textBox48_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[46] = Convert.ToSingle(textBox48.Text);

        }

        private void textBox49_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[47] = Convert.ToSingle(textBox49.Text);

        }

        private void textBox50_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[48] = Convert.ToSingle(textBox50.Text);

        }

        private void textBox51_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[49] = Convert.ToSingle(textBox51.Text);

        }

        private void textBox52_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[50] = Convert.ToSingle(textBox52.Text);

        }

        private void textBox53_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[51] = Convert.ToSingle(textBox53.Text);

        }

        private void textBox54_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[52] = Convert.ToSingle(textBox54.Text);

        }

        private void textBox55_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[53] = Convert.ToSingle(textBox55.Text);

        }

        private void textBox56_TextChanged(object sender, EventArgs e)
        {
            Wrapper.valwe[54] = Convert.ToSingle(textBox56.Text);

        }
    }
}
