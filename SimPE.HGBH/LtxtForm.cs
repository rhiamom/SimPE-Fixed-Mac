/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   Copyright (C) 2008 Peter L Jones                                      *
 *   pljones@users.sf.net                                                  *
 *                                                                         *
 *   Copyright (C) 2008 by GramzeSweatShop                                 *
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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using SimPe.Scenegraph.Compat;
using Avalonia.Controls;


namespace SimPe.Plugin
{
    /// <summary>
    /// Summary description for LtxtForm.
    /// </summary>
    public class LtxtForm
    {
        private bool loading;

        #region Form controls
        internal Avalonia.Controls.StackPanel ltxtPanel;
        private Avalonia.Controls.StackPanel panel2;
        private Avalonia.Controls.TextBlock label1;
        private Avalonia.Controls.TextBlock label2;
        private Avalonia.Controls.TextBlock label3;
        private Avalonia.Controls.TextBlock label4;
        private Avalonia.Controls.TextBlock label5;
        private Avalonia.Controls.TextBlock label6;
        private Avalonia.Controls.TextBlock label7;
        private Avalonia.Controls.TextBlock label8;
        private Avalonia.Controls.TextBlock label9;
        private Avalonia.Controls.TextBlock label10;
        private Avalonia.Controls.TextBlock label11;
        private Avalonia.Controls.TextBlock label12;
        private Avalonia.Controls.TextBlock label13;
        private Avalonia.Controls.TextBlock label14;
        private Avalonia.Controls.TextBlock label15;
        private Avalonia.Controls.TextBlock label16;
        private Avalonia.Controls.TextBlock label17;
        private Avalonia.Controls.TextBlock label18;
        private Avalonia.Controls.TextBlock label19;
        private Avalonia.Controls.TextBlock label20;
        private Avalonia.Controls.TextBlock label21;
        private Avalonia.Controls.TextBlock label22;
        private Avalonia.Controls.TextBlock label23;
        private Avalonia.Controls.TextBlock label24;
        internal Avalonia.Controls.TextBlock label25;
        private Avalonia.Controls.TextBlock label30;
        private Avalonia.Controls.TextBlock label31;
        private Avalonia.Controls.TextBlock label32;
        internal Avalonia.Controls.Button llFamily;
        internal Avalonia.Controls.Button llSubLot;
        internal Avalonia.Controls.Button llAptBase;
        internal Avalonia.Controls.Button llunknone;
        internal GroupBox gbApartment;
        internal GroupBox gbclarse;
        internal GroupBox gbunown;
        private GroupBox gbFlagg;
        internal GroupBox gbApart;
        internal GroupBox gbtravel;
        internal GroupBox gbhobby;
        internal Avalonia.Controls.TextBox tblotname;
        internal Avalonia.Controls.TextBox tbdesc;
        internal Avalonia.Controls.TextBox tbRoads;
        internal Avalonia.Controls.TextBox tbrotation;
        internal Avalonia.Controls.TextBox tbtype;
        internal Avalonia.Controls.TextBox tbver;
        internal Avalonia.Controls.TextBox tbsubver;
        internal Avalonia.Controls.TextBox tbhg;
        internal Avalonia.Controls.TextBox tbwd;
        internal Avalonia.Controls.TextBox tbtop;
        internal Avalonia.Controls.TextBox tbleft;
        internal Avalonia.Controls.TextBox tbinst;
        internal Avalonia.Controls.TextBox tbu2;
        internal Avalonia.Controls.ListBox lb;
        internal Avalonia.Controls.TextBox tbz;
        internal Avalonia.Controls.TextBox tbData;
        internal Avalonia.Controls.TextBox tbu0;
        internal Avalonia.Controls.TextBox tbu4;
        internal Avalonia.Controls.TextBox tbu3;
        internal Avalonia.Controls.TextBox tbTexture;
        internal Avalonia.Controls.TextBox tbowner;
        internal Avalonia.Controls.TextBox tbu5;
        internal Avalonia.Controls.TextBox tbApBase;
        internal Avalonia.Controls.TextBox tbu6;
        internal Avalonia.Controls.ListBox lbApts;
        internal Avalonia.Controls.TextBox tbElevationAt;
        internal Avalonia.Controls.TextBox tbApartment;
        internal Avalonia.Controls.TextBox tbSAu3;
        internal Avalonia.Controls.TextBox tbSAu2;
        internal Avalonia.Controls.TextBox tbSAFamily;
        internal Avalonia.Controls.ListBox lbu7;
        internal Avalonia.Controls.TextBox tbu7;
        internal Avalonia.Controls.TextBox tblotclass;
        internal Avalonia.Controls.TextBox tbcset;
        internal Avalonia.Controls.ComboBox cbtype;
        internal Avalonia.Controls.ComboBox cbLotClas;
        internal Avalonia.Controls.ComboBox cborient;
        internal Avalonia.Controls.CheckBox cbhidim;
        internal Avalonia.Controls.CheckBox cbhbmusic;
        internal Avalonia.Controls.CheckBox cbhbsport;
        internal Avalonia.Controls.CheckBox cbhbscience;
        internal Avalonia.Controls.CheckBox cbhbfitness;
        internal Avalonia.Controls.CheckBox cbhbtinker;
        internal Avalonia.Controls.CheckBox cbhbnature;
        internal Avalonia.Controls.CheckBox cbhbgames;
        internal Avalonia.Controls.CheckBox cbhbfilm;
        internal Avalonia.Controls.CheckBox cbhbart;
        internal Avalonia.Controls.CheckBox cbhbcook;
        internal Avalonia.Controls.CheckBox cbtrjflag5;
        internal Avalonia.Controls.CheckBox cbtrjflag4;
        internal Avalonia.Controls.CheckBox cbtrjflag3;
        internal Avalonia.Controls.CheckBox cbtrjflag2;
        internal Avalonia.Controls.CheckBox cbtrjflag1;
        internal Avalonia.Controls.CheckBox cbtrjungle;
        internal Avalonia.Controls.CheckBox cbtrhidec;
        internal Avalonia.Controls.CheckBox cbtrpool;
        internal Avalonia.Controls.CheckBox cbtrmale;
        internal Avalonia.Controls.CheckBox cbtrfem;
        internal Avalonia.Controls.CheckBox cbtrbeach;
        internal Avalonia.Controls.CheckBox cbtrformal;
        internal Avalonia.Controls.CheckBox cbtrteen;
        internal Avalonia.Controls.CheckBox cbtrnude;
        internal Avalonia.Controls.CheckBox cbtrpern;
        internal Avalonia.Controls.CheckBox cgtrwhite;
        internal Avalonia.Controls.CheckBox cbtrblue;
        internal Avalonia.Controls.CheckBox cbtrredred;
        internal Avalonia.Controls.CheckBox cbtradult;
        internal Avalonia.Controls.CheckBox cbtrclub;
        internal Avalonia.Controls.CheckBox cbBeachy;
        internal PictureBox pb;
        internal Avalonia.Controls.Button btnDelApt;
        internal Avalonia.Controls.Button btnAddApt;
        internal Avalonia.Controls.Button bthbytrvl;
        internal Avalonia.Controls.TextBlock lbPlayim;
        #endregion


        public LtxtForm()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                SimPe.Message.Show(ex.ToString(), "LtxtForm designer crash", MessageBoxButtons.OK);
                throw;
            }

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
                return;
        }
        //public LtxtForm()
        //{

        //loading = true;

        //InitializeComponent();

        //if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
        //return;

        //wrapper stays null until the caller assigns it
        //this.cborient.ResourceManager = SimPe.Localization.Manager;
        //this.cborient.Enum = typeof(Plugin.LotOrientation);

        //if (!Helper.XmlRegistry.UseBigIcons)
        //{
        //this.pb.Size = new System.Drawing.Size(124, 108);
        //this.pb.Location = new System.Drawing.Point(25, 56);
        //}

        //loading = false;
        //}



        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ltxtPanel = new StackPanel();
            this.lbPlayim = new Avalonia.Controls.TextBlock();
            this.gbApart = new GroupBox();
            this.label22 = new Avalonia.Controls.TextBlock();
            this.gbApartment = new GroupBox();
            this.llFamily = new Avalonia.Controls.Button();
            this.tbApartment = new Avalonia.Controls.TextBox();
            this.tbSAu2 = new Avalonia.Controls.TextBox();
            this.llSubLot = new Avalonia.Controls.Button();
            this.label31 = new Avalonia.Controls.TextBlock();
            this.tbSAu3 = new Avalonia.Controls.TextBox();
            this.label30 = new Avalonia.Controls.TextBlock();
            this.tbSAFamily = new Avalonia.Controls.TextBox();
            this.lbApts = new Avalonia.Controls.ListBox();
            this.tbApBase = new Avalonia.Controls.TextBox();
            this.llAptBase = new Avalonia.Controls.Button();
            this.btnDelApt = new Avalonia.Controls.Button();
            this.btnAddApt = new Avalonia.Controls.Button();
            this.tbdesc = new Avalonia.Controls.TextBox();
            this.gbtravel = new GroupBox();
            this.cbtrjflag5 = new Avalonia.Controls.CheckBox();
            this.cbtrjflag4 = new Avalonia.Controls.CheckBox();
            this.cbtrjflag3 = new Avalonia.Controls.CheckBox();
            this.cbtrjflag2 = new Avalonia.Controls.CheckBox();
            this.cbtrjflag1 = new Avalonia.Controls.CheckBox();
            this.cbtrjungle = new Avalonia.Controls.CheckBox();
            this.cbtrhidec = new Avalonia.Controls.CheckBox();
            this.cbtrpool = new Avalonia.Controls.CheckBox();
            this.cbtrmale = new Avalonia.Controls.CheckBox();
            this.cbtrfem = new Avalonia.Controls.CheckBox();
            this.cbtrbeach = new Avalonia.Controls.CheckBox();
            this.cbtrformal = new Avalonia.Controls.CheckBox();
            this.cbtrteen = new Avalonia.Controls.CheckBox();
            this.cbtrnude = new Avalonia.Controls.CheckBox();
            this.cbtrpern = new Avalonia.Controls.CheckBox();
            this.cgtrwhite = new Avalonia.Controls.CheckBox();
            this.cbtrblue = new Avalonia.Controls.CheckBox();
            this.cbtrredred = new Avalonia.Controls.CheckBox();
            this.cbtradult = new Avalonia.Controls.CheckBox();
            this.cbtrclub = new Avalonia.Controls.CheckBox();
            this.label5 = new Avalonia.Controls.TextBlock();
            this.tblotname = new Avalonia.Controls.TextBox();
            this.gbhobby = new GroupBox();
            this.cbhbmusic = new Avalonia.Controls.CheckBox();
            this.cbhbsport = new Avalonia.Controls.CheckBox();
            this.cbhbscience = new Avalonia.Controls.CheckBox();
            this.cbhbfitness = new Avalonia.Controls.CheckBox();
            this.cbhbtinker = new Avalonia.Controls.CheckBox();
            this.cbhbnature = new Avalonia.Controls.CheckBox();
            this.cbhbgames = new Avalonia.Controls.CheckBox();
            this.cbhbfilm = new Avalonia.Controls.CheckBox();
            this.cbhbart = new Avalonia.Controls.CheckBox();
            this.cbhbcook = new Avalonia.Controls.CheckBox();
            this.label4 = new Avalonia.Controls.TextBlock();
            this.llunknone = new Avalonia.Controls.Button();
            this.gbFlagg = new GroupBox();
            this.tbu0 = new Avalonia.Controls.TextBox();
            this.label21 = new Avalonia.Controls.TextBlock();
            this.cbBeachy = new Avalonia.Controls.CheckBox();
            this.cbhidim = new Avalonia.Controls.CheckBox();
            this.gbunown = new GroupBox();
            this.tbu2 = new Avalonia.Controls.TextBox();
            this.label18 = new Avalonia.Controls.TextBlock();
            this.label32 = new Avalonia.Controls.TextBlock();
            this.label19 = new Avalonia.Controls.TextBlock();
            this.lbu7 = new Avalonia.Controls.ListBox();
            this.tbu3 = new Avalonia.Controls.TextBox();
            this.label16 = new Avalonia.Controls.TextBlock();
            this.tbData = new Avalonia.Controls.TextBox();
            this.tbu7 = new Avalonia.Controls.TextBox();
            this.tbu5 = new Avalonia.Controls.TextBox();
            this.label24 = new Avalonia.Controls.TextBlock();
            this.tbu6 = new Avalonia.Controls.TextBox();
            this.label23 = new Avalonia.Controls.TextBlock();
            this.gbclarse = new GroupBox();
            this.label11 = new Avalonia.Controls.TextBlock();
            this.cbLotClas = new Avalonia.Controls.ComboBox();
            this.tbcset = new Avalonia.Controls.TextBox();
            this.tblotclass = new Avalonia.Controls.TextBox();
            this.label17 = new Avalonia.Controls.TextBlock();
            this.label7 = new Avalonia.Controls.TextBlock();
            this.lb = new Avalonia.Controls.ListBox();
            this.tbElevationAt = new Avalonia.Controls.TextBox();
            this.label25 = new Avalonia.Controls.TextBlock();
            this.tbowner = new Avalonia.Controls.TextBox();
            this.label15 = new Avalonia.Controls.TextBlock();
            this.label8 = new Avalonia.Controls.TextBlock();
            this.bthbytrvl = new Avalonia.Controls.Button();
            this.tbinst = new Avalonia.Controls.TextBox();
            this.label14 = new Avalonia.Controls.TextBlock();
            this.tbu4 = new Avalonia.Controls.TextBox();
            this.cborient = new Avalonia.Controls.ComboBox();
            this.tbTexture = new Avalonia.Controls.TextBox();
            this.label2 = new Avalonia.Controls.TextBlock();
            this.label6 = new Avalonia.Controls.TextBlock();
            this.label3 = new Avalonia.Controls.TextBlock();
            this.tbwd = new Avalonia.Controls.TextBox();
            this.tbrotation = new Avalonia.Controls.TextBox();
            this.label9 = new Avalonia.Controls.TextBlock();
            this.label10 = new Avalonia.Controls.TextBlock();
            this.tbhg = new Avalonia.Controls.TextBox();
            this.tbRoads = new Avalonia.Controls.TextBox();
            this.label12 = new Avalonia.Controls.TextBlock();
            this.tbver = new Avalonia.Controls.TextBox();
            this.tbtop = new Avalonia.Controls.TextBox();
            this.label13 = new Avalonia.Controls.TextBlock();
            this.tbsubver = new Avalonia.Controls.TextBox();
            this.tbleft = new Avalonia.Controls.TextBox();
            this.label20 = new Avalonia.Controls.TextBlock();
            this.label1 = new Avalonia.Controls.TextBlock();
            this.tbz = new Avalonia.Controls.TextBox();
            this.cbtype = new Avalonia.Controls.ComboBox();
            this.tbtype = new Avalonia.Controls.TextBox();
            this.panel2 = new StackPanel();
            this.pb = new PictureBox();
            //
            // ltxtPanel
            //
            this.ltxtPanel.Name = "ltxtPanel";
            //
            // lbPlayim
            //
            this.lbPlayim.Text = "Close SimPe and Play this Lot";
            this.lbPlayim.Name = "lbPlayim";
            this.lbPlayim.PointerReleased += (s, e) => lbPlayim_DoubleClick(s, EventArgs.Empty);
            //
            // gbApart
            //
            this.gbApart.Text = "Apartments";
            this.gbApart.Name = "gbApart";
            //
            // label22
            //
            this.label22.Text = "Sub Apartments:";
            this.label22.Name = "label22";
            //
            // gbApartment
            //
            this.gbApartment.Text = "Selected Apartment";
            this.gbApartment.Name = "gbApartment";
            //
            // llFamily
            //
            this.llFamily.Content = "Family:";
            this.llFamily.Name = "llFamily";
            this.llFamily.Click += (s, e) => ll_Click(s, EventArgs.Empty);
            //
            // tbApartment
            //
            this.tbApartment.Text = "0x00000000";
            this.tbApartment.Name = "tbApartment";
            this.tbApartment.TextChanged += (s, e) => this.SAChange(s, EventArgs.Empty);
            //
            // tbSAu2
            //
            this.tbSAu2.Text = "0x00000000";
            this.tbSAu2.Name = "tbSAu2";
            this.tbSAu2.TextChanged += (s, e) => this.SAChange(s, EventArgs.Empty);
            //
            // llSubLot
            //
            this.llSubLot.Content = "SubLot:";
            this.llSubLot.Name = "llSubLot";
            this.llSubLot.Click += (s, e) => ll_Click(s, EventArgs.Empty);
            //
            // label31
            //
            this.label31.Text = "SLu3:";
            this.label31.Name = "label31";
            //
            // tbSAu3
            //
            this.tbSAu3.Text = "0x00000000";
            this.tbSAu3.Name = "tbSAu3";
            this.tbSAu3.TextChanged += (s, e) => this.SAChange(s, EventArgs.Empty);
            //
            // label30
            //
            this.label30.Text = "SLu2:";
            this.label30.Name = "label30";
            //
            // tbSAFamily
            //
            this.tbSAFamily.Text = "0x00000000";
            this.tbSAFamily.Name = "tbSAFamily";
            this.tbSAFamily.TextChanged += (s, e) => this.SAChange(s, EventArgs.Empty);
            //
            // lbApts
            //
            this.lbApts.Name = "lbApts";
            this.lbApts.SelectionChanged += new System.EventHandler<Avalonia.Controls.SelectionChangedEventArgs>(this.lbApts_SelectedIndexChanged);
            //
            // tbApBase
            //
            this.tbApBase.Text = "0x00000000";
            this.tbApBase.Name = "tbApBase";
            this.tbApBase.TextChanged += (s, e) => this.tbApBase_TextChanged(s, EventArgs.Empty);
            //
            // llAptBase
            //
            this.llAptBase.Content = "Apartment base:";
            this.llAptBase.Name = "llAptBase";
            this.llAptBase.Click += (s, e) => ll_Click(s, EventArgs.Empty);
            //
            // btnDelApt
            //
            this.btnDelApt.Content = "Del";
            this.btnDelApt.Name = "btnDelApt";
            this.btnDelApt.Click += (s, e) => this.btnDelApt_Click(s, EventArgs.Empty);
            //
            // btnAddApt
            //
            this.btnAddApt.Content = "Add";
            this.btnAddApt.Name = "btnAddApt";
            this.btnAddApt.Click += (s, e) => this.btnAddApt_Click(s, EventArgs.Empty);
            //
            // tbdesc
            //
            this.tbdesc.Name = "tbdesc";
            this.tbdesc.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // gbtravel
            //
            this.gbtravel.Text = "Travel Flags";
            this.gbtravel.Name = "gbtravel";
            //
            // cbtrjflag5
            //
            this.cbtrjflag5.Content = "Flag 30";
            this.cbtrjflag5.Name = "cbtrjflag5";
            this.cbtrjflag5.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrjflag4
            //
            this.cbtrjflag4.Content = "Jungle Flag 4";
            this.cbtrjflag4.Name = "cbtrjflag4";
            this.cbtrjflag4.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrjflag3
            //
            this.cbtrjflag3.Content = "Jungle Flag 3";
            this.cbtrjflag3.Name = "cbtrjflag3";
            this.cbtrjflag3.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrjflag2
            //
            this.cbtrjflag2.Content = "Jungle Flag 2";
            this.cbtrjflag2.Name = "cbtrjflag2";
            this.cbtrjflag2.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrjflag1
            //
            this.cbtrjflag1.Content = "Jungle Flag 1";
            this.cbtrjflag1.Name = "cbtrjflag1";
            this.cbtrjflag1.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrjungle
            //
            this.cbtrjungle.Content = "Jungle Lot";
            this.cbtrjungle.Name = "cbtrjungle";
            this.cbtrjungle.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrhidec
            //
            this.cbtrhidec.Content = "Hidden Com Lot";
            this.cbtrhidec.Name = "cbtrhidec";
            this.cbtrhidec.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrpool
            //
            this.cbtrpool.Content = "Lot has a Pool";
            this.cbtrpool.Name = "cbtrpool";
            this.cbtrpool.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrmale
            //
            this.cbtrmale.Content = "Males Only";
            this.cbtrmale.Name = "cbtrmale";
            this.cbtrmale.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrfem
            //
            this.cbtrfem.Content = "Females Only";
            this.cbtrfem.Name = "cbtrfem";
            this.cbtrfem.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrbeach
            //
            this.cbtrbeach.Content = "Beach Lot";
            this.cbtrbeach.Name = "cbtrbeach";
            this.cbtrbeach.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrformal
            //
            this.cbtrformal.Content = "Wear Formal";
            this.cbtrformal.Name = "cbtrformal";
            this.cbtrformal.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrteen
            //
            this.cbtrteen.Content = "Teen Phone";
            this.cbtrteen.Name = "cbtrteen";
            this.cbtrteen.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrnude
            //
            this.cbtrnude.Content = "Nudist Lot";
            this.cbtrnude.Name = "cbtrnude";
            this.cbtrnude.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrpern
            //
            this.cbtrpern.Content = "Porn Cinema";
            this.cbtrpern.Name = "cbtrpern";
            this.cbtrpern.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cgtrwhite
            //
            this.cgtrwhite.Content = "White Red light";
            this.cgtrwhite.Name = "cgtrwhite";
            this.cgtrwhite.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrblue
            //
            this.cbtrblue.Content = "Blue Red light";
            this.cbtrblue.Name = "cbtrblue";
            this.cbtrblue.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrredred
            //
            this.cbtrredred.Content = "Red Red light";
            this.cbtrredred.Name = "cbtrredred";
            this.cbtrredred.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtradult
            //
            this.cbtradult.Content = "Adults Only";
            this.cbtradult.Name = "cbtradult";
            this.cbtradult.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbtrclub
            //
            this.cbtrclub.Content = "Woohoo Club";
            this.cbtrclub.Name = "cbtrclub";
            this.cbtrclub.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // label5
            //
            this.label5.Text = "Description:";
            this.label5.Name = "label5";
            //
            // tblotname
            //
            this.tblotname.Name = "tblotname";
            this.tblotname.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // gbhobby
            //
            this.gbhobby.Text = "Hobby Flags";
            this.gbhobby.Name = "gbhobby";
            //
            // cbhbmusic
            //
            this.cbhbmusic.Content = "Music and Dance";
            this.cbhbmusic.Name = "cbhbmusic";
            this.cbhbmusic.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbhbsport
            //
            this.cbhbsport.Content = "Sports";
            this.cbhbsport.Name = "cbhbsport";
            this.cbhbsport.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbhbscience
            //
            this.cbhbscience.Content = "Science";
            this.cbhbscience.Name = "cbhbscience";
            this.cbhbscience.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbhbfitness
            //
            this.cbhbfitness.Content = "Fitness";
            this.cbhbfitness.Name = "cbhbfitness";
            this.cbhbfitness.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbhbtinker
            //
            this.cbhbtinker.Content = "Tinker";
            this.cbhbtinker.Name = "cbhbtinker";
            this.cbhbtinker.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbhbnature
            //
            this.cbhbnature.Content = "Nature";
            this.cbhbnature.Name = "cbhbnature";
            this.cbhbnature.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbhbgames
            //
            this.cbhbgames.Content = "Games";
            this.cbhbgames.Name = "cbhbgames";
            this.cbhbgames.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbhbfilm
            //
            this.cbhbfilm.Content = "Film and Lit";
            this.cbhbfilm.Name = "cbhbfilm";
            this.cbhbfilm.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbhbart
            //
            this.cbhbart.Content = "Art and Craft";
            this.cbhbart.Name = "cbhbart";
            this.cbhbart.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // cbhbcook
            //
            this.cbhbcook.Content = "Cooking";
            this.cbhbcook.Name = "cbhbcook";
            this.cbhbcook.IsCheckedChanged += (s, e) => this.hobbytravel_CheckedChanged(s, EventArgs.Empty);
            //
            // label4
            //
            this.label4.Text = "Lot Name:";
            this.label4.Name = "label4";
            //
            // llunknone
            //
            this.llunknone.Content = "Unknown:";
            this.llunknone.Name = "llunknone";
            this.llunknone.Click += (s, e) => llunknone_LinkClicked(s, EventArgs.Empty);
            //
            // gbFlagg
            //
            this.gbFlagg.Text = "Lot Flags";
            this.gbFlagg.Name = "gbFlagg";
            //
            // tbu0
            //
            this.tbu0.Text = "0x00000000";
            this.tbu0.Name = "tbu0";
            this.tbu0.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // label21
            //
            this.label21.Text = "Value:";
            this.label21.Name = "label21";
            //
            // cbBeachy
            //
            this.cbBeachy.Content = "Has Beach";
            this.cbBeachy.Name = "cbBeachy";
            this.cbBeachy.IsCheckedChanged += (s, e) => this.cbhidim_CheckedChanged(s, EventArgs.Empty);
            //
            // cbhidim
            //
            this.cbhidim.Content = "Hidden";
            this.cbhidim.Name = "cbhidim";
            this.cbhidim.IsCheckedChanged += (s, e) => this.cbhidim_CheckedChanged(s, EventArgs.Empty);
            //
            // gbunown
            //
            this.gbunown.Name = "gbunown";
            this.gbunown.IsVisible = false;
            //
            // tbu2
            //
            this.tbu2.Text = "0x00";
            this.tbu2.Name = "tbu2";
            this.tbu2.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // label18
            //
            this.label18.Text = "U2:";
            this.label18.Name = "label18";
            //
            // label32
            //
            this.label32.Text = "U7:";
            this.label32.Name = "label32";
            //
            // label19
            //
            this.label19.Text = "Unknown Data:";
            this.label19.Name = "label19";
            //
            // lbu7
            //
            this.lbu7.Items.Add("0x00000000");
            this.lbu7.Items.Add("0x00000000");
            this.lbu7.Items.Add("0x00000000");
            this.lbu7.Items.Add("0x00000000");
            this.lbu7.Items.Add("0x00000000");
            this.lbu7.Items.Add("0x00000000");
            this.lbu7.Items.Add("0x00000000");
            this.lbu7.Items.Add("0x00000000");
            this.lbu7.Name = "lbu7";
            this.lbu7.SelectionChanged += new System.EventHandler<Avalonia.Controls.SelectionChangedEventArgs>(this.lbu7_SelectedIndexChanged);
            //
            // tbu3
            //
            this.tbu3.Text = "0.0";
            this.tbu3.Name = "tbu3";
            this.tbu3.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // label16
            //
            this.label16.Text = "U3:";
            this.label16.Name = "label16";
            //
            // tbData
            //
            this.tbData.Name = "tbData";
            this.tbData.TextChanged += (s, e) => this.ChangeData(s, EventArgs.Empty);
            //
            // tbu7
            //
            this.tbu7.Text = "0x00000000";
            this.tbu7.Name = "tbu7";
            this.tbu7.TextChanged += (s, e) => this.tbu7_TextChanged(s, EventArgs.Empty);
            //
            // tbu5
            //
            this.tbu5.Text = "00 00 00 00  00 00 00 00  00";
            this.tbu5.Name = "tbu5";
            this.tbu5.TextChanged += (s, e) => this.ChangeData(s, EventArgs.Empty);
            //
            // label24
            //
            this.label24.Text = "U5:";
            this.label24.Name = "label24";
            //
            // tbu6
            //
            this.tbu6.Text = "00 00 00 00  00 00 00 00  00";
            this.tbu6.Name = "tbu6";
            this.tbu6.TextChanged += (s, e) => this.ChangeData(s, EventArgs.Empty);
            //
            // label23
            //
            this.label23.Text = "U6:";
            this.label23.Name = "label23";
            //
            // gbclarse
            //
            this.gbclarse.Text = "Lot Class";
            this.gbclarse.Name = "gbclarse";
            //
            // label11
            //
            this.label11.Text = "Is Set:";
            this.label11.Name = "label11";
            //
            // cbLotClas
            //
            this.cbLotClas.Items.Add("Not Set");
            this.cbLotClas.Items.Add("Low");
            this.cbLotClas.Items.Add("Medium");
            this.cbLotClas.Items.Add("High");
            this.cbLotClas.Name = "cbLotClas";
            this.cbLotClas.SelectionChanged += new System.EventHandler<Avalonia.Controls.SelectionChangedEventArgs>(this.cbhidim_CheckedChanged);
            //
            // tbcset
            //
            this.tbcset.Name = "tbcset";
            //
            // tblotclass
            //
            this.tblotclass.Name = "tblotclass";
            //
            // label17
            //
            this.label17.Text = "Value :";
            this.label17.Name = "label17";
            //
            // label7
            //
            this.label7.Text = "Elevation offsets:";
            this.label7.Name = "label7";
            //
            // lb
            //
            this.lb.Items.Add("0,0");
            this.lb.Items.Add("0,1");
            this.lb.Items.Add("0,2");
            this.lb.Items.Add("0,3");
            this.lb.Items.Add("0,4");
            this.lb.Items.Add("0,5");
            this.lb.Items.Add("0,6");
            this.lb.Items.Add("0,0");
            this.lb.Items.Add("0,1");
            this.lb.Items.Add("0,2");
            this.lb.Items.Add("0,3");
            this.lb.Items.Add("0,4");
            this.lb.Items.Add("0,5");
            this.lb.Items.Add("1,6");
            this.lb.Items.Add("0,0");
            this.lb.Items.Add("0,1");
            this.lb.Items.Add("0,2");
            this.lb.Items.Add("0,3");
            this.lb.Items.Add("0,4");
            this.lb.Items.Add("0,5");
            this.lb.Items.Add("2,6");
            this.lb.Items.Add("0,0");
            this.lb.Items.Add("0,1");
            this.lb.Items.Add("0,2");
            this.lb.Items.Add("0,3");
            this.lb.Items.Add("0,4");
            this.lb.Items.Add("0,5");
            this.lb.Items.Add("3,6");
            this.lb.Items.Add("0,0");
            this.lb.Items.Add("0,1");
            this.lb.Items.Add("0,2");
            this.lb.Items.Add("0,3");
            this.lb.Items.Add("0,4");
            this.lb.Items.Add("0,5");
            this.lb.Items.Add("4,6");
            this.lb.Items.Add("0,0");
            this.lb.Items.Add("0,1");
            this.lb.Items.Add("0,2");
            this.lb.Items.Add("0,3");
            this.lb.Items.Add("0,4");
            this.lb.Items.Add("0,5");
            this.lb.Items.Add("5,6");
            this.lb.Items.Add("0,0");
            this.lb.Items.Add("0,1");
            this.lb.Items.Add("0,2");
            this.lb.Name = "lb";
            this.lb.SelectionChanged += new System.EventHandler<Avalonia.Controls.SelectionChangedEventArgs>(this.lb_SelectedIndexChanged);
            //
            // tbElevationAt
            //
            this.tbElevationAt.Text = "0.0";
            this.tbElevationAt.Name = "tbElevationAt";
            this.tbElevationAt.TextChanged += (s, e) => this.tbElevationAt_TextChanged(s, EventArgs.Empty);
            //
            // label25
            //
            this.label25.Text = "Lot Owner:";
            this.label25.Name = "label25";
            this.label25.PointerReleased += (s, e) => label25_Click(s, EventArgs.Empty);
            //
            // tbowner
            //
            this.tbowner.Text = "0x00000000";
            this.tbowner.Name = "tbowner";
            this.tbowner.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // label15
            //
            this.label15.Text = "Instance:";
            this.label15.Name = "label15";
            //
            // label8
            //
            this.label8.Text = "Width:";
            this.label8.Name = "label8";
            //
            // bthbytrvl
            //
            this.bthbytrvl.Content = "Hobby+Travel Flags:";
            this.bthbytrvl.Name = "bthbytrvl";
            this.bthbytrvl.Click += (s, e) => this.Openpntravel(s, EventArgs.Empty);
            //
            // tbinst
            //
            this.tbinst.Text = "0x00000000";
            this.tbinst.Name = "tbinst";
            this.tbinst.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // label14
            //
            this.label14.Text = "Orientation:";
            this.label14.Name = "label14";
            //
            // tbu4
            //
            this.tbu4.Text = "0x00000000";
            this.tbu4.Name = "tbu4";
            this.tbu4.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // cborient
            //
            this.cborient.Name = "cborient";
            this.cborient.SelectionChanged += new System.EventHandler<Avalonia.Controls.SelectionChangedEventArgs>(this.CommonChange);
            //
            // tbTexture
            //
            this.tbTexture.Text = "0x00000000";
            this.tbTexture.Name = "tbTexture";
            this.tbTexture.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // label2
            //
            this.label2.Text = "Roads:";
            this.label2.Name = "label2";
            //
            // label6
            //
            this.label6.Text = "Texture:";
            this.label6.Name = "label6";
            //
            // label3
            //
            this.label3.Text = "Rotation:";
            this.label3.Name = "label3";
            //
            // tbwd
            //
            this.tbwd.Text = "0x00";
            this.tbwd.Name = "tbwd";
            this.tbwd.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // tbrotation
            //
            this.tbrotation.Text = "0x00";
            this.tbrotation.Name = "tbrotation";
            this.tbrotation.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // label9
            //
            this.label9.Text = "Height:";
            this.label9.Name = "label9";
            //
            // label10
            //
            this.label10.Text = "Version:";
            this.label10.Name = "label10";
            //
            // tbhg
            //
            this.tbhg.Text = "0x00";
            this.tbhg.Name = "tbhg";
            this.tbhg.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // tbRoads
            //
            this.tbRoads.Text = "0x00";
            this.tbRoads.Name = "tbRoads";
            this.tbRoads.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // label12
            //
            this.label12.Text = "Top:";
            this.label12.Name = "label12";
            //
            // tbver
            //
            this.tbver.Text = "0x0000";
            this.tbver.Name = "tbver";
            this.tbver.IsReadOnly = true;
            //
            // tbtop
            //
            this.tbtop.Text = "0x00";
            this.tbtop.Name = "tbtop";
            this.tbtop.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // label13
            //
            this.label13.Text = "Left:";
            this.label13.Name = "label13";
            //
            // tbsubver
            //
            this.tbsubver.Text = "0x0000";
            this.tbsubver.Name = "tbsubver";
            this.tbsubver.IsReadOnly = true;
            //
            // tbleft
            //
            this.tbleft.Text = "0x00";
            this.tbleft.Name = "tbleft";
            this.tbleft.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // label20
            //
            this.label20.Text = "Z:";
            this.label20.Name = "label20";
            //
            // label1
            //
            this.label1.Text = "Lot Type:";
            this.label1.Name = "label1";
            //
            // tbz
            //
            this.tbz.Text = "0.0";
            this.tbz.Name = "tbz";
            this.tbz.TextChanged += (s, e) => this.CommonChange(s, EventArgs.Empty);
            //
            // cbtype
            //
            this.cbtype.Name = "cbtype";
            this.cbtype.SelectionChanged += new System.EventHandler<Avalonia.Controls.SelectionChangedEventArgs>(this.SelectType);
            //
            // tbtype
            //
            this.tbtype.Text = "0x00";
            this.tbtype.Name = "tbtype";
            this.tbtype.IsReadOnly = true;
            //
            // panel2
            //
            this.panel2.Name = "panel2";
            //
            // pb
            //
            this.pb.Name = "pb";

        }
        #endregion

        internal Ltxt wrapper;

        private void SelectType(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            if (Enum.IsDefined(typeof(Ltxt.LotType), cbtype.SelectedItem))
                wrapper.Type = (Ltxt.LotType)cbtype.SelectedItem;
            else
                wrapper.Type = Ltxt.LotType.Unknown;
            tbtype.Text = "0x" + Helper.HexString((byte)wrapper.Type);
            btnAddApt.IsEnabled = btnDelApt.IsEnabled = (wrapper.Type == Ltxt.LotType.ApartmentBase);
            cbtrclub.IsEnabled = cbtrhidec.IsEnabled = gbhobby.Enabled = (wrapper.Type == Ltxt.LotType.Hobby);
            if (wrapper.SubVersion >= LtxtSubVersion.Freetime)bthbytrvl.IsEnabled = (wrapper.Type == Ltxt.LotType.Hobby);
            if (wrapper.Type == Ltxt.LotType.ApartmentBase || wrapper.Type == Ltxt.LotType.ApartmentSublot)
            {
                gbApart.IsVisible = true;
            }
            else
            {
                gbApart.IsVisible = false;
            }

            wrapper.Changed = true;
        }

        private void Commit(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            try
            {
                wrapper.SynchronizeUserData();
                SimPe.Message.Show(Localization.Manager.GetString("commited"), null, MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
            }
        }

        private void CommonChange(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            try
            {
                wrapper.LotRoads = Convert.ToByte(this.tbRoads.Text, 16);

                wrapper.LotSize = new Size(
                    Helper.StringToInt32(tbwd.Text, wrapper.LotSize.Width, 10),
                    Helper.StringToInt32(tbhg.Text, wrapper.LotSize.Height, 10));
                wrapper.LotPosition = new Point(
                    Helper.StringToInt32(tbleft.Text, wrapper.LotPosition.X, 10),
                    Helper.StringToInt32(tbtop.Text, wrapper.LotPosition.Y, 10));
                wrapper.LotElevation = Helper.StringToFloat(tbz.Text, wrapper.LotElevation);

                wrapper.Orientation = (LotOrientation)cborient.SelectedItem;
                wrapper.LotRotation = Convert.ToByte(this.tbrotation.Text, 16);
                wrapper.Unknown0 = Helper.StringToUInt32(tbu0.Text, wrapper.Unknown0, 16);
                Boolset bby = wrapper.Unknown0;
                this.cbhidim.IsChecked = bby[4];
                this.cbBeachy.IsChecked = bby[7];
                if (wrapper.Version >= LtxtVersion.Apartment || wrapper.SubVersion >= LtxtSubVersion.Apartment)
                {
                    this.cbLotClas.IsEnabled = true;
                    if (bby[12]) this.cbLotClas.SelectedIndex = 1;
                    else if (bby[13]) this.cbLotClas.SelectedIndex = 2;
                    else if (bby[14]) this.cbLotClas.SelectedIndex = 3;
                    else this.cbLotClas.SelectedIndex = 0;
                }
                else
                {
                    this.cbLotClas.SelectedIndex = 0;
                    this.cbLotClas.IsEnabled = false;
                }

                wrapper.LotName = tblotname.Text;
                wrapper.Texture = tbTexture.Text;
                wrapper.LotDesc = tbdesc.Text;

                wrapper.LotInstance = Helper.StringToUInt32(tbinst.Text, wrapper.LotInstance, 16);
                wrapper.Unknown3 = Helper.StringToFloat(tbu3.Text, wrapper.Unknown3);
                wrapper.Unknown4 = Helper.StringToUInt32(tbu4.Text, wrapper.Unknown4, 16);
                wrapper.LotClass = Helper.StringToUInt32(tblotclass.Text, wrapper.LotClass, 16);
                Boolset tty = wrapper.Unknown4;

                this.cbtrjflag5.IsChecked = tty[30];
                this.cbtrjflag4.IsChecked = tty[28];
                this.cbtrjflag3.IsChecked = tty[27];
                this.cbtrjflag2.IsChecked = tty[26];
                this.cbtrjflag1.IsChecked = tty[25];
                this.cbtrjungle.IsChecked = tty[24];
                this.cbtrhidec.IsChecked = tty[23];
                this.cbtrpool.IsChecked = tty[22];
                this.cbtrmale.IsChecked = tty[21];
                this.cbtrfem.IsChecked = tty[20];
                this.cbtrbeach.IsChecked = tty[19];
                this.cbtrformal.IsChecked = tty[18];
                this.cbtrteen.IsChecked = tty[17];
                this.cbtrnude.IsChecked = tty[16];
                this.cbtrpern.IsChecked = tty[15];
                this.cgtrwhite.IsChecked = tty[14];
                this.cbtrblue.IsChecked = tty[13];
                this.cbtrredred.IsChecked = tty[12];
                this.cbtradult.IsChecked = tty[11];
                this.cbtrclub.IsChecked = tty[10];
                this.cbhbmusic.IsChecked = tty[9];
                this.cbhbscience.IsChecked = tty[8];
                this.cbhbfitness.IsChecked = tty[7];
                this.cbhbtinker.IsChecked = tty[6];
                this.cbhbnature.IsChecked = tty[5];
                this.cbhbgames.IsChecked = tty[4];
                this.cbhbsport.IsChecked = tty[3];
                this.cbhbfilm.IsChecked = tty[2];
                this.cbhbart.IsChecked = tty[1];
                this.cbhbcook.IsChecked = tty[0];

                wrapper.Unknown2 = (byte)Helper.StringToUInt16(tbu2.Text, wrapper.Unknown2, 16);
                wrapper.OwnerInstance = Helper.StringToUInt32(tbowner.Text, wrapper.OwnerInstance, 16);

                wrapper.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
        }

        private void cbhidim_CheckedChanged(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            try
            {
                Boolset bby = wrapper.Unknown0;
                bby[4] = this.cbhidim.IsChecked == true;
                bby[7] = this.cbBeachy.IsChecked == true;
                if (wrapper.Version >= LtxtVersion.Apartment || wrapper.SubVersion >= LtxtSubVersion.Apartment)
                {
                    bby[12] = (this.cbLotClas.SelectedIndex == 1);
                    bby[13] = (this.cbLotClas.SelectedIndex == 2);
                    bby[14] = (this.cbLotClas.SelectedIndex == 3);
                }
                wrapper.Unknown0 = bby;
                this.tbu0.Text = "0x" + Helper.HexString(wrapper.Unknown0);
                wrapper.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
        }

        private void hobbytravel_CheckedChanged(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            try
            {
                uint tty = 0;
                if (this.cbhbcook.IsChecked == true) tty += 1;
                if (this.cbhbart.IsChecked == true) tty += 2;
                if (this.cbhbfilm.IsChecked == true) tty += 4;
                if (this.cbhbsport.IsChecked == true) tty += 8;
                if (this.cbhbgames.IsChecked == true) tty += 16;
                if (this.cbhbnature.IsChecked == true) tty += 32;
                if (this.cbhbtinker.IsChecked == true) tty += 64;
                if (this.cbhbfitness.IsChecked == true) tty += 128;
                if (this.cbhbscience.IsChecked == true) tty += 256;
                if (this.cbhbmusic.IsChecked == true) tty += 512;
                if (this.cbtrclub.IsChecked == true) tty += 1024;
                if (this.cbtradult.IsChecked == true) tty += 2048;
                if (this.cbtrredred.IsChecked == true) tty += 4096;
                if (this.cbtrblue.IsChecked == true) tty += 8192;
                if (this.cgtrwhite.IsChecked == true) tty += 16384;
                if (this.cbtrpern.IsChecked == true) tty += 32768;
                if (this.cbtrnude.IsChecked == true) tty += 65536;
                if (this.cbtrteen.IsChecked == true) tty += 131072;
                if (this.cbtrformal.IsChecked == true) tty += 262144;
                if (this.cbtrbeach.IsChecked == true) tty += 524288;
                if (this.cbtrfem.IsChecked == true) tty += 1048576;
                if (this.cbtrmale.IsChecked == true) tty += 2097152;
                if (this.cbtrpool.IsChecked == true) tty += 4194304;
                if (this.cbtrhidec.IsChecked == true) tty += 8388608;
                if (this.cbtrjungle.IsChecked == true) tty += 16777216;
                if (this.cbtrjflag1.IsChecked == true) tty += 33554432;
                if (this.cbtrjflag2.IsChecked == true) tty += 67108864;
                if (this.cbtrjflag3.IsChecked == true) tty += 134217728;
                if (this.cbtrjflag4.IsChecked == true) tty += 268435456;
                if (this.cbtrjflag5.IsChecked == true) tty += 536870912;
                this.cbtrmale.IsEnabled = !this.cbtrfem.IsChecked == true;
                this.cbtrfem.IsEnabled = !this.cbtrmale.IsChecked == true;
                wrapper.Unknown4 = tty;
                this.tbu4.Text = "0x" + Helper.HexString(wrapper.Unknown4);

                wrapper.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
        }

        private void Openpntravel(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            try
            {
                this.gbunown.IsVisible = false;
                this.gbhobby.IsVisible = !this.gbhobby.IsVisible;
                this.gbtravel.IsVisible = this.gbhobby.IsVisible && (wrapper.Type == Ltxt.LotType.Hobby);
                wrapper.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
        }

        private void ChangeData(object sender, System.EventArgs e)
        {
            if (wrapper == null) return;
            try
            {
                wrapper.Unknown5 = Helper.SetLength(Helper.HexListToBytes(this.tbu5.Text), 9);
                wrapper.Unknown6 = Helper.SetLength(Helper.HexListToBytes(this.tbu6.Text), 9);
                wrapper.Followup = Helper.SetLength(Helper.HexListToBytes(this.tbData.Text), 0);

                wrapper.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
        }

        private void lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            Ltxt wrp = wrapper;
            wrapper = null;

            if (lb.SelectedIndex < 0)
                tbElevationAt.Text = "";
            else
                tbElevationAt.Text = wrp.Unknown1[lb.SelectedIndex].ToString();

            wrapper = wrp;
        }

        private void tbElevationAt_TextChanged(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            if (lb.SelectedIndex < 0) return;

            Ltxt wrp = wrapper;
            wrapper = null;

            try
            {
                wrp.Unknown1[lb.SelectedIndex] = Helper.StringToFloat(tbElevationAt.Text, wrp.Unknown1[lb.SelectedIndex]);
                int x, y;
                y = Convert.ToInt32(lb.SelectedIndex / wrp.LotSize.Height);
                x = lb.SelectedIndex - y * wrp.LotSize.Height;
                lb.Items[lb.SelectedIndex] = "(" + x + "," + y + ") " + wrp.Unknown1[lb.SelectedIndex];

                wrp.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
            finally
            {
                wrapper = wrp;
            }
        }

        private void lbApts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            Ltxt wrp = wrapper;
            wrapper = null;

            if (lbApts.SelectedIndex < 0)
            {
                tbApartment.Text = tbSAFamily.Text = tbSAu2.Text = tbSAu3.Text = "";
                btnDelApt.IsEnabled = llFamily.IsEnabled = llSubLot.IsEnabled = false;
            }
            else
            {
                Ltxt.SubLot sl = wrp.SubLots[lbApts.SelectedIndex];
                tbApartment.Text = (string)lbApts.SelectedItem;
                tbSAFamily.Text = "0x" + Helper.HexString(sl.Family);
                tbSAu2.Text = "0x" + Helper.HexString(sl.Unknown2);
                tbSAu3.Text = "0x" + Helper.HexString(sl.Unknown3);
                btnDelApt.IsEnabled = llFamily.IsEnabled = llSubLot.IsEnabled = true;
            }

            wrapper = wrp;
        }

        private void SAChange(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            if (lbApts.SelectedIndex < 0) return;

            Ltxt wrp = wrapper;
            wrapper = null;

            try
            {
                Ltxt.SubLot sl = wrp.SubLots[lbApts.SelectedIndex];
                sl.ApartmentSublot = Helper.StringToUInt32(tbApartment.Text, sl.ApartmentSublot, 16);
                sl.Family = Helper.StringToUInt32(tbSAFamily.Text, sl.Family, 16);
                sl.Unknown2 = Helper.StringToUInt32(tbSAu2.Text, sl.Unknown2, 16);
                sl.Unknown3 = Helper.StringToUInt32(tbSAu3.Text, sl.Unknown3, 16);
                lbApts.Items[lbApts.SelectedIndex] = "0x" + Helper.HexString(sl.ApartmentSublot);

                wrp.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
            finally
            {
                wrapper = wrp;
            }
        }

        private void lbu7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            Ltxt wrp = wrapper;
            wrapper = null;

            if (lbu7.SelectedIndex < 0)
                tbu7.Text = "";
            else
                tbu7.Text = (string)lbu7.SelectedItem;

            wrapper = wrp;
        }

        private void tbu7_TextChanged(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            if (lbu7.SelectedIndex < 0) return;

            Ltxt wrp = wrapper;
            wrapper = null;

            try
            {
                wrp.Unknown7[lbu7.SelectedIndex] = Helper.StringToUInt32(tbu7.Text, wrp.Unknown7[lbu7.SelectedIndex], 16);
                lbu7.Items[lb.SelectedIndex] = "0x" + Helper.HexString(wrp.Unknown7[lb.SelectedIndex]);

                wrp.Changed = true;
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage("", ex);
            }
            finally
            {
                wrapper = wrp;
            }
        }


        private void ll_Click(object sender, EventArgs e)
        {
            System.Collections.Generic.List<Avalonia.Controls.Button> lll =
                new System.Collections.Generic.List<Avalonia.Controls.Button>(new Avalonia.Controls.Button[] { llAptBase, llSubLot, llFamily, });

            uint type, inst;
            switch (lll.IndexOf((Avalonia.Controls.Button)sender))
            {
                case 0:
                    type = (uint)0x0BF999E7;
                    inst = wrapper.ApartmentBase;
                    break;
                case 1:
                    type = (uint)0x0BF999E7;
                    inst = wrapper.SubLots[lbApts.SelectedIndex].ApartmentSublot;
                    break;
                case 2:
                    type = (uint)0x46414D49;
                    inst = wrapper.SubLots[lbApts.SelectedIndex].Family;
                    break;
                default:
                    return;
            }

            Interfaces.Files.IPackedFileDescriptor pfd =
                wrapper.Package.NewDescriptor(type, wrapper.FileDescriptor.SubType, wrapper.FileDescriptor.Group, inst);
            pfd = wrapper.Package.FindFile(pfd);
            if (pfd == null) return;

            SimPe.RemoteControl.OpenPackedFile(pfd, wrapper.Package);
        }

        private void btnAddApt_Click(object sender, EventArgs e)
        {
            wrapper.SubLots.Add(new Ltxt.SubLot());
            lbApts.Items.Add("0x" + Helper.HexString(wrapper.SubLots[wrapper.SubLots.Count - 1].ApartmentSublot));
            lbApts.SelectedIndex = wrapper.SubLots.Count - 1;

            wrapper.Changed = true;
        }

        private void btnDelApt_Click(object sender, EventArgs e)
        {
            int i = lbApts.SelectedIndex;

            lbApts.SelectedIndex = -1;

            wrapper.SubLots.RemoveAt(i);
            lbApts.Items.RemoveAt(i);

            if (i > 0) i--;
            else if (lbApts.Items.Count == 0) i = -1;

            lbApts.SelectedIndex = i;

            wrapper.Changed = true;
        }

        private void tbApBase_TextChanged(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            wrapper.ApartmentBase = Helper.StringToUInt32(tbApBase.Text, wrapper.ApartmentBase, 16);
            llAptBase.IsEnabled = (wrapper.ApartmentBase != 0);
        }

        private void label25_Click(object sender, EventArgs e)
        {
            uint simmy = Helper.StringToUInt32(tbowner.Text, wrapper.OwnerInstance, 16);
            if (simmy == 0) return;

            SimPe.PackedFiles.Wrapper.SDesc sdsc =
                FileTable.ProviderRegistry.SimDescriptionProvider.SimInstance[(ushort)simmy]
                    as SimPe.PackedFiles.Wrapper.SDesc;

            if (sdsc != null)
            {
                Interfaces.Files.IPackedFileDescriptor pfd =
                    sdsc.Package.NewDescriptor(0xAACE2EFB, sdsc.FileDescriptor.SubType, sdsc.FileDescriptor.Group,
                                               sdsc.FileDescriptor.Instance);

                pfd = sdsc.Package.FindFile(pfd);
                SimPe.RemoteControl.OpenPackedFile(pfd, sdsc.Package);
            }
        }


        private void llunknone_LinkClicked(object sender, EventArgs e)
        {
            this.gbunown.IsVisible = !this.gbunown.IsVisible;
        }

        private void lbPlayim_DoubleClick(object sender, EventArgs e)
        {
            if (wrapper == null) return;
            if (!System.IO.File.Exists(SimPe.PathProvider.Global.SimsApplication) || wrapper.appendage == null) return;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = SimPe.PathProvider.Global.SimsApplication;
            p.StartInfo.Arguments = wrapper.appendage;
            p.Start();
            System.Threading.Thread.CurrentThread.Abort();
        }
    }
}
