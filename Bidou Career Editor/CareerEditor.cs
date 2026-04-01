/***************************************************************************
 *   Original (C) Bidou, assumed to be licenced as part of SimPE           *
 *   Pet updates copyright (C) 2007 by Peter L Jones                       *
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
 *   59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.             *
 ***************************************************************************/

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Avalonia.Controls;
using Avalonia.Layout;
using SimPe.Scenegraph.Compat;
using System.Data;
using System.IO;
using SimPe;
using SimPe.Data;
using SimPe.Interfaces;
using SimPe.PackedFiles.Wrapper;


namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class CareerEditor : Avalonia.Controls.Window
	{
		#region Windows Form Designer generated code

		private Avalonia.Controls.TabItem tabPage4;
		private Avalonia.Controls.TabItem tabPage3;
		private Avalonia.Controls.TabItem tabPage2;
		private Avalonia.Controls.TabControl tabControl1;
		private Avalonia.Controls.TextBox CareerTitle;
		private Avalonia.Controls.TextBlock label3;
		private Avalonia.Controls.TabItem tabPage1;
		private Avalonia.Controls.TextBlock label1;
        private Avalonia.Controls.TextBox CareerLvls;
        private GroupBox gbJLDetails;
		private Avalonia.Controls.ComboBox Language;
        private Avalonia.Controls.TextBlock label10;
		private GroupBox gbJLHoursWages;
        private GroupBox gbJLPromo;
		private Avalonia.Controls.NumericUpDown PromoBody;
		private Avalonia.Controls.NumericUpDown PromoMechanical;
		private Avalonia.Controls.NumericUpDown PromoCooking;
		private Avalonia.Controls.NumericUpDown PromoCharisma;
		private Avalonia.Controls.NumericUpDown PromoFriends;
		private Avalonia.Controls.NumericUpDown PromoCleaning;
		private Avalonia.Controls.NumericUpDown PromoLogic;
		private Avalonia.Controls.NumericUpDown PromoCreativity;
		private Avalonia.Controls.TextBlock label34;
		private Avalonia.Controls.TextBlock label35;
		private Avalonia.Controls.TextBlock label36;
		private Avalonia.Controls.TextBlock label37;
		private Avalonia.Controls.TextBlock label38;
		private Avalonia.Controls.TextBlock label39;
		private Avalonia.Controls.TextBlock label40;
        private Avalonia.Controls.TextBlock label41;
		private ListView JobDetailList;
		private ColumnHeader JdLvl;
		private ColumnHeader JdJobTitle;
		private ColumnHeader JdDesc;
		private ColumnHeader JdOutfit;
		private ColumnHeader JdVehicle;
		private ListView HoursWagesList;
		private ColumnHeader HwLvl;
		private ColumnHeader HwStart;
		private ColumnHeader HwHours;
		private ColumnHeader HwEnd;
		private ColumnHeader HwWages;
        private ColumnHeader HwCatWages;
        private ColumnHeader HwDogWages;
        private ColumnHeader HwSun;
		private ColumnHeader HwMon;
		private ColumnHeader HwTue;
		private ColumnHeader HwWed;
		private ColumnHeader HwThu;
		private ColumnHeader HwFri;
		private ColumnHeader HwSat;
		private ListView PromoList;
		private ColumnHeader PrLvl;
		private ColumnHeader PrCooking;
		private ColumnHeader PrMechanical;
		private ColumnHeader PrCharisma;
		private ColumnHeader PrBody;
		private ColumnHeader PrCreativity;
		private ColumnHeader PrLogic;
		private ColumnHeader PrCleaning;
		private ColumnHeader PrFriends;
        private GroupBox gbPromo;
        private GroupBox gbJobDetails;
        private Avalonia.Controls.Menu mainMenu1;
        private Avalonia.Controls.MenuItem menuItem1;
        private Avalonia.Controls.MenuItem miEnglishOnly;
        private Avalonia.Controls.MenuItem menuItem6;
        private Avalonia.Controls.MenuItem miAddLvl;
        private Avalonia.Controls.MenuItem miRemoveLvl;
        private Avalonia.Controls.TextBlock label101;
        private ComboBox cbTrick;
        private ColumnHeader PrTrick;
        private JobDescPanel jdpMale;
        private JobDescPanel jdpFemale;
        private Avalonia.Controls.Button JobDetailsCopy;
        private LabelledNumericUpDown lnudChanceCurrentLevel;
        private LabelledNumericUpDown lnudChancePercent;
        private ChoicePanel cpChoiceA;
        private ChoicePanel cpChoiceB;
        private Avalonia.Controls.TextBlock label51;
        private TextBox ChanceTextMale;
        private Avalonia.Controls.Button ChanceCopy;
        private Avalonia.Controls.TextBlock label52;
        private TextBox ChanceTextFemale;
        private TabControl tcChanceOutcome;
        private Avalonia.Controls.TabItem tabPage5;
        private EffectPanel epPassA;
        private Avalonia.Controls.TabItem tabPage6;
        private EffectPanel epFailA;
        private Avalonia.Controls.TabItem tabPage7;
        private EffectPanel epPassB;
        private Avalonia.Controls.TabItem tabPage8;
        private EffectPanel epFailB;
        private GroupBox gbHoursWages;
        private LabelledNumericUpDown lnudWorkStart;
        private LabelledNumericUpDown lnudWorkHours;
        private LabelledNumericUpDown lnudWages;
        private LabelledNumericUpDown lnudWagesDog;
        private LabelledNumericUpDown lnudWagesCat;
        private CheckBox WorkMonday;
        private CheckBox WorkTuesday;
        private CheckBox WorkWednesday;
        private CheckBox WorkThursday;
        private CheckBox WorkFriday;
        private CheckBox WorkSaturday;
        private CheckBox WorkSunday;
        private GroupBox gbHWMotives;
        private Avalonia.Controls.TextBlock label27;
        private Avalonia.Controls.TextBlock label24;
        private NumericUpDown ComfortHours;
        private NumericUpDown HygieneTotal;
        private NumericUpDown BladderTotal;
        private Avalonia.Controls.TextBlock label21;
        private NumericUpDown WorkBladder;
        private Avalonia.Controls.TextBlock label23;
        private Avalonia.Controls.TextBlock label19;
        private NumericUpDown WorkComfort;
        private NumericUpDown HungerHours;
        private NumericUpDown EnergyHours;
        private Avalonia.Controls.TextBlock label25;
        private Avalonia.Controls.TextBlock label18;
        private NumericUpDown WorkPublic;
        private NumericUpDown WorkHunger;
        private NumericUpDown BladderHours;
        private NumericUpDown ComfortTotal;
        private Avalonia.Controls.TextBlock label22;
        private NumericUpDown HungerTotal;
        private NumericUpDown HygieneHours;
        private NumericUpDown AmorousHours;
        private NumericUpDown WorkEnergy;
        private NumericUpDown WorkFun;
        private NumericUpDown WorkAmorous;
        private NumericUpDown WorkSunshine;
        private NumericUpDown PublicHours;
        private Avalonia.Controls.TextBlock label20;
        private NumericUpDown SunshineTotal;
        private NumericUpDown EnergyTotal;
        private NumericUpDown FunTotal;
        private NumericUpDown PublicTotal;
        private Avalonia.Controls.TextBlock label33;
        private Avalonia.Controls.TextBlock label32;
        private Avalonia.Controls.TextBlock label31;
        private Avalonia.Controls.TextBlock label30;
        private Avalonia.Controls.TextBlock label28;
        private Avalonia.Controls.TextBlock label26;
        private NumericUpDown FunHours;
        private NumericUpDown WorkHygiene;
        private NumericUpDown SunshineHours;
        private NumericUpDown AmorousTotal;
        private GUIDChooser gcReward;
        private GUIDChooser gcUpgrade;
        private GUIDChooser gcOutfit;
        private GUIDChooser gcVehicle;
        private Avalonia.Controls.TextBlock lbcrap;
        private Avalonia.Controls.TextBlock lbPTO;
        private Avalonia.Controls.TextBlock lbLscore;
        private NumericUpDown numLscore;
        private NumericUpDown numPTO;
        private Avalonia.Controls.Panel pntheme;
        private PictureBox pictureBox1;
        private Avalonia.Controls.TabItem tabPage9;
        private Avalonia.Controls.Panel thmepanel1;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private GroupBox gbExtras;
        private TextBox textBox1b;
        private TextBox textBox1g;
        private Avalonia.Controls.TextBlock label2;
        private CheckBox checkBox3;
        private Avalonia.Controls.TextBlock label9;
        private Avalonia.Controls.TextBlock label8;
        private Avalonia.Controls.TextBlock label7;
        private Avalonia.Controls.TextBlock label6;
        private Avalonia.Controls.TextBlock label5;
        private Avalonia.Controls.TextBlock label4;
        private CheckBox checkBox6;
        private CheckBox checkBox5;
        private CheckBox checkBox4;
        private CheckBox checkBox7;
        private ComboBox comboBox1;
        private Avalonia.Controls.TextBlock label13;
        private Avalonia.Controls.TextBlock label11;
        private Avalonia.Controls.TextBlock label12;
        private CheckBox checkBox9;
        private CheckBox checkBox8;
        private CheckBox checkBox42;
        private CheckBox checkBox43;
        private CheckBox checkBox44;
        private CheckBox checkBox45;
        private TextBox textBox17;
        private TextBox textBox18;
        private Avalonia.Controls.TextBlock label46;
        private CheckBox checkBox38;
        private CheckBox checkBox39;
        private CheckBox checkBox40;
        private CheckBox checkBox41;
        private TextBox textBox15;
        private TextBox textBox16;
        private Avalonia.Controls.TextBlock label45;
        private CheckBox checkBox34;
        private CheckBox checkBox35;
        private CheckBox checkBox36;
        private CheckBox checkBox37;
        private TextBox textBox13;
        private TextBox textBox14;
        private Avalonia.Controls.TextBlock label44;
        private CheckBox checkBox30;
        private CheckBox checkBox31;
        private CheckBox checkBox32;
        private CheckBox checkBox33;
        private TextBox textBox11;
        private TextBox textBox12;
        private Avalonia.Controls.TextBlock label43;
        private CheckBox checkBox26;
        private CheckBox checkBox27;
        private CheckBox checkBox28;
        private CheckBox checkBox29;
        private TextBox textBox9;
        private TextBox textBox10;
        private Avalonia.Controls.TextBlock label42;
        private CheckBox checkBox22;
        private CheckBox checkBox23;
        private CheckBox checkBox24;
        private CheckBox checkBox25;
        private TextBox textBox7;
        private TextBox textBox8;
        private Avalonia.Controls.TextBlock label17;
        private CheckBox checkBox18;
        private CheckBox checkBox19;
        private CheckBox checkBox20;
        private CheckBox checkBox21;
        private TextBox textBox5;
        private TextBox textBox6;
        private Avalonia.Controls.TextBlock label16;
        private CheckBox checkBox14;
        private CheckBox checkBox15;
        private CheckBox checkBox16;
        private CheckBox checkBox17;
        private TextBox textBox3;
        private TextBox textBox4;
        private Avalonia.Controls.TextBlock label15;
        private CheckBox checkBox10;
        private CheckBox checkBox11;
        private CheckBox checkBox12;
        private CheckBox checkBox13;
        private TextBox textBox1;
        private TextBox textBox2;
        private Avalonia.Controls.TextBlock label14;
        private CheckBox checkBox67;
        private CheckBox checkBox68;
        private CheckBox checkBox69;
        private ComboBox comboBox9;
        private CheckBox checkBox64;
        private CheckBox checkBox65;
        private CheckBox checkBox66;
        private ComboBox comboBox8;
        private CheckBox checkBox61;
        private CheckBox checkBox62;
        private CheckBox checkBox63;
        private ComboBox comboBox7;
        private CheckBox checkBox58;
        private CheckBox checkBox59;
        private CheckBox checkBox60;
        private ComboBox comboBox6;
        private CheckBox checkBox55;
        private CheckBox checkBox56;
        private CheckBox checkBox57;
        private ComboBox comboBox5;
        private CheckBox checkBox52;
        private CheckBox checkBox53;
        private CheckBox checkBox54;
        private ComboBox comboBox4;
        private CheckBox checkBox49;
        private CheckBox checkBox50;
        private CheckBox checkBox51;
        private ComboBox comboBox3;
        private CheckBox checkBox46;
        private CheckBox checkBox47;
        private CheckBox checkBox48;
        private ComboBox comboBox2;
        private CheckBox checkBox70;
        private CheckBox checkBox71;
        private CheckBox checkBox72;
        private ComboBox comboBox10;
        private Avalonia.Controls.TextBlock lbrewguid;
        private Button btexApply;
        private Button btUgrade;
        private Avalonia.Controls.TabItem tabPagMajor;
        private Avalonia.Controls.Panel gpmajors;
        private GroupBox gbmajaffil;
        private GroupBox gbrequir;
        private CheckBox cbrdrama;
        private CheckBox cbrbiol;
        private CheckBox cbrArt;
        private CheckBox cbrecon;
        private CheckBox cbrhisto;
        private CheckBox cbrliter;
        private CheckBox cbrmaths;
        private CheckBox cbrphilo;
        private CheckBox cbrphysi;
        private CheckBox cbrphyco;
        private CheckBox cbrpolit;
        private CheckBox cbaphyco;
        private CheckBox cbapolit;
        private CheckBox cbaphysi;
        private CheckBox cbrahilo;
        private CheckBox cbamaths;
        private CheckBox cbaliter;
        private CheckBox cbahisto;
        private CheckBox cbaecon;
        private CheckBox cbadrama;
        private CheckBox cbabiol;
        private CheckBox cbaArt;
        private Avalonia.Controls.TextBlock label29;
        private Button btmajApply;
        private Avalonia.Controls.TextBlock label47;
        private LabelledNumericUpDown lnudPetChancePercent;
        private CheckBox cbischance;
        private LabelledNumericUpDown lnudFoods;
        private TextBox tbWorkFinish;
        private Avalonia.Controls.TextBlock label48;
        private PictureBox pictureBox2;

        #endregion

		public CareerEditor()
		{
			//
			// Required for Windows Form Designer support
			//
            BuildLayout();

			englishOnly = false;

            internalchg = true;
            languageString = new List<string>(pjse.BhavWiz.readStr(pjse.GS.BhavStr.Languages));
            if (languageString.Count > 0) languageString.RemoveAt(0);

            
            gcReward.KnownObjects = new object[] { new List<String>(rewardName), new List<UInt32>(rewardGUID) };
            gcUpgrade.KnownObjects = new object[] { new List<String>(upgradeName), new List<UInt32>(upgradeGUID) };
            gcOutfit.KnownObjects = new object[] { new List<String>(outfitName), new List<UInt32>(outfitGUID) };
            gcVehicle.KnownObjects = new object[] { new List<String>(vehicleName), new List<UInt32>(vehicleGUID) };
            this.label18.IsVisible = this.AmorousHours.IsVisible = this.WorkAmorous.IsVisible = this.AmorousTotal.IsVisible = false;
            
            internalchg = false;

            this.gcUpgrade.ComboBoxWidth = this.gcReward.ComboBoxWidth = 220;
            this.gcOutfit.ComboBoxWidth = this.gcVehicle.ComboBoxWidth = 300;
            // this.thmepanel1.BackgroundImage = GetImage.GetExpansionLogo(PathProvider.Global.Latest.Version); // Panel has no BackgroundImage in Avalonia
            // BackgroundImage omitted — expansion logo renders over checkboxes on .NET 8
            this.pictureBox2.Image = LoadIcon.load("information.png");
        }

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void BuildLayout()
		{
            this.tabPage4 = new Avalonia.Controls.TabItem();
            this.pictureBox2 = new PictureBox();
            this.lnudPetChancePercent = new SimPe.Plugin.LabelledNumericUpDown();
            this.cbischance = new Avalonia.Controls.CheckBox();
            this.lnudChanceCurrentLevel = new SimPe.Plugin.LabelledNumericUpDown();
            this.lnudChancePercent = new SimPe.Plugin.LabelledNumericUpDown();
            this.cpChoiceA = new SimPe.Plugin.ChoicePanel();
            this.cpChoiceB = new SimPe.Plugin.ChoicePanel();
            this.ChanceTextMale = new Avalonia.Controls.TextBox();
            this.ChanceCopy = new Avalonia.Controls.Button();
            this.ChanceTextFemale = new Avalonia.Controls.TextBox();
            this.tcChanceOutcome = new Avalonia.Controls.TabControl();
            this.tabPage5 = new Avalonia.Controls.TabItem();
            this.epPassA = new SimPe.Plugin.EffectPanel();
            this.tabPage6 = new Avalonia.Controls.TabItem();
            this.epFailA = new SimPe.Plugin.EffectPanel();
            this.tabPage7 = new Avalonia.Controls.TabItem();
            this.epPassB = new SimPe.Plugin.EffectPanel();
            this.tabPage8 = new Avalonia.Controls.TabItem();
            this.epFailB = new SimPe.Plugin.EffectPanel();
            this.label51 = new Avalonia.Controls.TextBlock();
            this.label52 = new Avalonia.Controls.TextBlock();
            this.tabPage3 = new Avalonia.Controls.TabItem();
            this.gbJLPromo = new GroupBox();
            this.PromoList = new ListView();
            this.PrLvl = new ColumnHeader();
            this.PrCooking = new ColumnHeader();
            this.PrMechanical = new ColumnHeader();
            this.PrBody = new ColumnHeader();
            this.PrCharisma = new ColumnHeader();
            this.PrCreativity = new ColumnHeader();
            this.PrLogic = new ColumnHeader();
            this.PrCleaning = new ColumnHeader();
            this.PrFriends = new ColumnHeader();
            this.PrTrick = new ColumnHeader();
            this.gbPromo = new GroupBox();
            this.cbTrick = new Avalonia.Controls.ComboBox();
            this.label101 = new Avalonia.Controls.TextBlock();
            this.label41 = new Avalonia.Controls.TextBlock();
            this.label40 = new Avalonia.Controls.TextBlock();
            this.label39 = new Avalonia.Controls.TextBlock();
            this.label38 = new Avalonia.Controls.TextBlock();
            this.label37 = new Avalonia.Controls.TextBlock();
            this.label36 = new Avalonia.Controls.TextBlock();
            this.label35 = new Avalonia.Controls.TextBlock();
            this.label34 = new Avalonia.Controls.TextBlock();
            this.PromoFriends = new Avalonia.Controls.NumericUpDown();
            this.PromoCleaning = new Avalonia.Controls.NumericUpDown();
            this.PromoLogic = new Avalonia.Controls.NumericUpDown();
            this.PromoCreativity = new Avalonia.Controls.NumericUpDown();
            this.PromoCharisma = new Avalonia.Controls.NumericUpDown();
            this.PromoBody = new Avalonia.Controls.NumericUpDown();
            this.PromoMechanical = new Avalonia.Controls.NumericUpDown();
            this.PromoCooking = new Avalonia.Controls.NumericUpDown();
            this.tabPage2 = new Avalonia.Controls.TabItem();
            this.gbHoursWages = new GroupBox();
            this.tbWorkFinish = new Avalonia.Controls.TextBox();
            this.label48 = new Avalonia.Controls.TextBlock();
            this.lnudFoods = new SimPe.Plugin.LabelledNumericUpDown();
            this.lnudWorkStart = new SimPe.Plugin.LabelledNumericUpDown();
            this.lnudWorkHours = new SimPe.Plugin.LabelledNumericUpDown();
            this.lnudWages = new SimPe.Plugin.LabelledNumericUpDown();
            this.lnudWagesDog = new SimPe.Plugin.LabelledNumericUpDown();
            this.lbPTO = new Avalonia.Controls.TextBlock();
            this.lnudWagesCat = new SimPe.Plugin.LabelledNumericUpDown();
            this.WorkMonday = new Avalonia.Controls.CheckBox();
            this.WorkTuesday = new Avalonia.Controls.CheckBox();
            this.WorkWednesday = new Avalonia.Controls.CheckBox();
            this.WorkThursday = new Avalonia.Controls.CheckBox();
            this.WorkFriday = new Avalonia.Controls.CheckBox();
            this.WorkSaturday = new Avalonia.Controls.CheckBox();
            this.WorkSunday = new Avalonia.Controls.CheckBox();
            this.gbHWMotives = new GroupBox();
            this.label27 = new Avalonia.Controls.TextBlock();
            this.label24 = new Avalonia.Controls.TextBlock();
            this.ComfortHours = new Avalonia.Controls.NumericUpDown();
            this.HygieneTotal = new Avalonia.Controls.NumericUpDown();
            this.BladderTotal = new Avalonia.Controls.NumericUpDown();
            this.label21 = new Avalonia.Controls.TextBlock();
            this.WorkBladder = new Avalonia.Controls.NumericUpDown();
            this.label23 = new Avalonia.Controls.TextBlock();
            this.label19 = new Avalonia.Controls.TextBlock();
            this.WorkComfort = new Avalonia.Controls.NumericUpDown();
            this.HungerHours = new Avalonia.Controls.NumericUpDown();
            this.EnergyHours = new Avalonia.Controls.NumericUpDown();
            this.label25 = new Avalonia.Controls.TextBlock();
            this.WorkPublic = new Avalonia.Controls.NumericUpDown();
            this.WorkHunger = new Avalonia.Controls.NumericUpDown();
            this.BladderHours = new Avalonia.Controls.NumericUpDown();
            this.ComfortTotal = new Avalonia.Controls.NumericUpDown();
            this.label22 = new Avalonia.Controls.TextBlock();
            this.HungerTotal = new Avalonia.Controls.NumericUpDown();
            this.HygieneHours = new Avalonia.Controls.NumericUpDown();
            this.WorkEnergy = new Avalonia.Controls.NumericUpDown();
            this.WorkFun = new Avalonia.Controls.NumericUpDown();
            this.WorkSunshine = new Avalonia.Controls.NumericUpDown();
            this.PublicHours = new Avalonia.Controls.NumericUpDown();
            this.label20 = new Avalonia.Controls.TextBlock();
            this.SunshineTotal = new Avalonia.Controls.NumericUpDown();
            this.EnergyTotal = new Avalonia.Controls.NumericUpDown();
            this.FunTotal = new Avalonia.Controls.NumericUpDown();
            this.PublicTotal = new Avalonia.Controls.NumericUpDown();
            this.label33 = new Avalonia.Controls.TextBlock();
            this.label32 = new Avalonia.Controls.TextBlock();
            this.label31 = new Avalonia.Controls.TextBlock();
            this.label30 = new Avalonia.Controls.TextBlock();
            this.label28 = new Avalonia.Controls.TextBlock();
            this.label26 = new Avalonia.Controls.TextBlock();
            this.FunHours = new Avalonia.Controls.NumericUpDown();
            this.WorkHygiene = new Avalonia.Controls.NumericUpDown();
            this.SunshineHours = new Avalonia.Controls.NumericUpDown();
            this.label18 = new Avalonia.Controls.TextBlock();
            this.AmorousHours = new Avalonia.Controls.NumericUpDown();
            this.WorkAmorous = new Avalonia.Controls.NumericUpDown();
            this.AmorousTotal = new Avalonia.Controls.NumericUpDown();
            this.lbLscore = new Avalonia.Controls.TextBlock();
            this.numLscore = new Avalonia.Controls.NumericUpDown();
            this.numPTO = new Avalonia.Controls.NumericUpDown();
            this.gbJLHoursWages = new GroupBox();
            this.HoursWagesList = new ListView();
            this.HwLvl = new ColumnHeader();
            this.HwStart = new ColumnHeader();
            this.HwHours = new ColumnHeader();
            this.HwEnd = new ColumnHeader();
            this.HwWages = new ColumnHeader();
            this.HwDogWages = new ColumnHeader();
            this.HwCatWages = new ColumnHeader();
            this.HwMon = new ColumnHeader();
            this.HwTue = new ColumnHeader();
            this.HwWed = new ColumnHeader();
            this.HwThu = new ColumnHeader();
            this.HwFri = new ColumnHeader();
            this.HwSat = new ColumnHeader();
            this.HwSun = new ColumnHeader();
            this.tabControl1 = new Avalonia.Controls.TabControl();
            this.tabPage1 = new Avalonia.Controls.TabItem();
            this.gbJobDetails = new GroupBox();
            this.gcVehicle = new SimPe.Plugin.GUIDChooser();
            this.gcOutfit = new SimPe.Plugin.GUIDChooser();
            this.JobDetailsCopy = new Avalonia.Controls.Button();
            this.jdpFemale = new SimPe.Plugin.JobDescPanel();
            this.jdpMale = new SimPe.Plugin.JobDescPanel();
            this.gbJLDetails = new GroupBox();
            this.JobDetailList = new ListView();
            this.JdLvl = new ColumnHeader();
            this.JdJobTitle = new ColumnHeader();
            this.JdDesc = new ColumnHeader();
            this.JdOutfit = new ColumnHeader();
            this.JdVehicle = new ColumnHeader();
            this.tabPagMajor = new Avalonia.Controls.TabItem();
            this.gpmajors = new Avalonia.Controls.Panel();
            this.btmajApply = new Avalonia.Controls.Button();
            this.gbmajaffil = new GroupBox();
            this.label47 = new Avalonia.Controls.TextBlock();
            this.cbaphyco = new Avalonia.Controls.CheckBox();
            this.cbapolit = new Avalonia.Controls.CheckBox();
            this.cbaphysi = new Avalonia.Controls.CheckBox();
            this.cbrahilo = new Avalonia.Controls.CheckBox();
            this.cbamaths = new Avalonia.Controls.CheckBox();
            this.cbaliter = new Avalonia.Controls.CheckBox();
            this.cbahisto = new Avalonia.Controls.CheckBox();
            this.cbaecon = new Avalonia.Controls.CheckBox();
            this.cbadrama = new Avalonia.Controls.CheckBox();
            this.cbabiol = new Avalonia.Controls.CheckBox();
            this.cbaArt = new Avalonia.Controls.CheckBox();
            this.gbrequir = new GroupBox();
            this.label29 = new Avalonia.Controls.TextBlock();
            this.cbrphyco = new Avalonia.Controls.CheckBox();
            this.cbrpolit = new Avalonia.Controls.CheckBox();
            this.cbrphysi = new Avalonia.Controls.CheckBox();
            this.cbrphilo = new Avalonia.Controls.CheckBox();
            this.cbrmaths = new Avalonia.Controls.CheckBox();
            this.cbrliter = new Avalonia.Controls.CheckBox();
            this.cbrhisto = new Avalonia.Controls.CheckBox();
            this.cbrecon = new Avalonia.Controls.CheckBox();
            this.cbrdrama = new Avalonia.Controls.CheckBox();
            this.cbrbiol = new Avalonia.Controls.CheckBox();
            this.cbrArt = new Avalonia.Controls.CheckBox();
            this.tabPage9 = new Avalonia.Controls.TabItem();
            this.thmepanel1 = new Avalonia.Controls.Panel();
            this.btexApply = new Avalonia.Controls.Button();
            this.checkBox70 = new Avalonia.Controls.CheckBox();
            this.checkBox71 = new Avalonia.Controls.CheckBox();
            this.checkBox72 = new Avalonia.Controls.CheckBox();
            this.comboBox10 = new Avalonia.Controls.ComboBox();
            this.checkBox67 = new Avalonia.Controls.CheckBox();
            this.checkBox68 = new Avalonia.Controls.CheckBox();
            this.checkBox69 = new Avalonia.Controls.CheckBox();
            this.comboBox9 = new Avalonia.Controls.ComboBox();
            this.checkBox64 = new Avalonia.Controls.CheckBox();
            this.checkBox65 = new Avalonia.Controls.CheckBox();
            this.checkBox66 = new Avalonia.Controls.CheckBox();
            this.comboBox8 = new Avalonia.Controls.ComboBox();
            this.checkBox61 = new Avalonia.Controls.CheckBox();
            this.checkBox62 = new Avalonia.Controls.CheckBox();
            this.checkBox63 = new Avalonia.Controls.CheckBox();
            this.comboBox7 = new Avalonia.Controls.ComboBox();
            this.checkBox58 = new Avalonia.Controls.CheckBox();
            this.checkBox59 = new Avalonia.Controls.CheckBox();
            this.checkBox60 = new Avalonia.Controls.CheckBox();
            this.comboBox6 = new Avalonia.Controls.ComboBox();
            this.checkBox55 = new Avalonia.Controls.CheckBox();
            this.checkBox56 = new Avalonia.Controls.CheckBox();
            this.checkBox57 = new Avalonia.Controls.CheckBox();
            this.comboBox5 = new Avalonia.Controls.ComboBox();
            this.checkBox52 = new Avalonia.Controls.CheckBox();
            this.checkBox53 = new Avalonia.Controls.CheckBox();
            this.checkBox54 = new Avalonia.Controls.CheckBox();
            this.comboBox4 = new Avalonia.Controls.ComboBox();
            this.checkBox49 = new Avalonia.Controls.CheckBox();
            this.checkBox50 = new Avalonia.Controls.CheckBox();
            this.checkBox51 = new Avalonia.Controls.CheckBox();
            this.comboBox3 = new Avalonia.Controls.ComboBox();
            this.checkBox46 = new Avalonia.Controls.CheckBox();
            this.checkBox47 = new Avalonia.Controls.CheckBox();
            this.checkBox48 = new Avalonia.Controls.CheckBox();
            this.comboBox2 = new Avalonia.Controls.ComboBox();
            this.label13 = new Avalonia.Controls.TextBlock();
            this.label11 = new Avalonia.Controls.TextBlock();
            this.label12 = new Avalonia.Controls.TextBlock();
            this.checkBox9 = new Avalonia.Controls.CheckBox();
            this.checkBox8 = new Avalonia.Controls.CheckBox();
            this.checkBox7 = new Avalonia.Controls.CheckBox();
            this.comboBox1 = new Avalonia.Controls.ComboBox();
            this.gbExtras = new GroupBox();
            this.lbrewguid = new Avalonia.Controls.TextBlock();
            this.checkBox42 = new Avalonia.Controls.CheckBox();
            this.checkBox43 = new Avalonia.Controls.CheckBox();
            this.checkBox44 = new Avalonia.Controls.CheckBox();
            this.checkBox45 = new Avalonia.Controls.CheckBox();
            this.textBox17 = new Avalonia.Controls.TextBox();
            this.textBox18 = new Avalonia.Controls.TextBox();
            this.label46 = new Avalonia.Controls.TextBlock();
            this.checkBox38 = new Avalonia.Controls.CheckBox();
            this.checkBox39 = new Avalonia.Controls.CheckBox();
            this.checkBox40 = new Avalonia.Controls.CheckBox();
            this.checkBox41 = new Avalonia.Controls.CheckBox();
            this.textBox15 = new Avalonia.Controls.TextBox();
            this.textBox16 = new Avalonia.Controls.TextBox();
            this.label45 = new Avalonia.Controls.TextBlock();
            this.checkBox34 = new Avalonia.Controls.CheckBox();
            this.checkBox35 = new Avalonia.Controls.CheckBox();
            this.checkBox36 = new Avalonia.Controls.CheckBox();
            this.checkBox37 = new Avalonia.Controls.CheckBox();
            this.textBox13 = new Avalonia.Controls.TextBox();
            this.textBox14 = new Avalonia.Controls.TextBox();
            this.label44 = new Avalonia.Controls.TextBlock();
            this.checkBox30 = new Avalonia.Controls.CheckBox();
            this.checkBox31 = new Avalonia.Controls.CheckBox();
            this.checkBox32 = new Avalonia.Controls.CheckBox();
            this.checkBox33 = new Avalonia.Controls.CheckBox();
            this.textBox11 = new Avalonia.Controls.TextBox();
            this.textBox12 = new Avalonia.Controls.TextBox();
            this.label43 = new Avalonia.Controls.TextBlock();
            this.checkBox26 = new Avalonia.Controls.CheckBox();
            this.checkBox27 = new Avalonia.Controls.CheckBox();
            this.checkBox28 = new Avalonia.Controls.CheckBox();
            this.checkBox29 = new Avalonia.Controls.CheckBox();
            this.textBox9 = new Avalonia.Controls.TextBox();
            this.textBox10 = new Avalonia.Controls.TextBox();
            this.label42 = new Avalonia.Controls.TextBlock();
            this.checkBox22 = new Avalonia.Controls.CheckBox();
            this.checkBox23 = new Avalonia.Controls.CheckBox();
            this.checkBox24 = new Avalonia.Controls.CheckBox();
            this.checkBox25 = new Avalonia.Controls.CheckBox();
            this.textBox7 = new Avalonia.Controls.TextBox();
            this.textBox8 = new Avalonia.Controls.TextBox();
            this.label17 = new Avalonia.Controls.TextBlock();
            this.checkBox18 = new Avalonia.Controls.CheckBox();
            this.checkBox19 = new Avalonia.Controls.CheckBox();
            this.checkBox20 = new Avalonia.Controls.CheckBox();
            this.checkBox21 = new Avalonia.Controls.CheckBox();
            this.textBox5 = new Avalonia.Controls.TextBox();
            this.textBox6 = new Avalonia.Controls.TextBox();
            this.label16 = new Avalonia.Controls.TextBlock();
            this.checkBox14 = new Avalonia.Controls.CheckBox();
            this.checkBox15 = new Avalonia.Controls.CheckBox();
            this.checkBox16 = new Avalonia.Controls.CheckBox();
            this.checkBox17 = new Avalonia.Controls.CheckBox();
            this.textBox3 = new Avalonia.Controls.TextBox();
            this.textBox4 = new Avalonia.Controls.TextBox();
            this.label15 = new Avalonia.Controls.TextBlock();
            this.checkBox10 = new Avalonia.Controls.CheckBox();
            this.checkBox11 = new Avalonia.Controls.CheckBox();
            this.checkBox12 = new Avalonia.Controls.CheckBox();
            this.checkBox13 = new Avalonia.Controls.CheckBox();
            this.textBox1 = new Avalonia.Controls.TextBox();
            this.textBox2 = new Avalonia.Controls.TextBox();
            this.label14 = new Avalonia.Controls.TextBlock();
            this.label9 = new Avalonia.Controls.TextBlock();
            this.label8 = new Avalonia.Controls.TextBlock();
            this.label7 = new Avalonia.Controls.TextBlock();
            this.label6 = new Avalonia.Controls.TextBlock();
            this.label5 = new Avalonia.Controls.TextBlock();
            this.label4 = new Avalonia.Controls.TextBlock();
            this.checkBox6 = new Avalonia.Controls.CheckBox();
            this.checkBox5 = new Avalonia.Controls.CheckBox();
            this.checkBox4 = new Avalonia.Controls.CheckBox();
            this.checkBox3 = new Avalonia.Controls.CheckBox();
            this.textBox1b = new Avalonia.Controls.TextBox();
            this.textBox1g = new Avalonia.Controls.TextBox();
            this.label2 = new Avalonia.Controls.TextBlock();
            this.checkBox2 = new Avalonia.Controls.CheckBox();
            this.checkBox1 = new Avalonia.Controls.CheckBox();
            this.CareerLvls = new Avalonia.Controls.TextBox();
            this.label1 = new Avalonia.Controls.TextBlock();
            this.CareerTitle = new Avalonia.Controls.TextBox();
            this.label3 = new Avalonia.Controls.TextBlock();
            this.Language = new Avalonia.Controls.ComboBox();
            this.label10 = new Avalonia.Controls.TextBlock();
            this.mainMenu1 = new Avalonia.Controls.Menu();
            this.menuItem6 = new Avalonia.Controls.MenuItem();
            this.miAddLvl = new Avalonia.Controls.MenuItem();
            this.miRemoveLvl = new Avalonia.Controls.MenuItem();
            this.menuItem1 = new Avalonia.Controls.MenuItem();
            this.miEnglishOnly = new Avalonia.Controls.MenuItem();
            this.gcUpgrade = new SimPe.Plugin.GUIDChooser();
            this.gcReward = new SimPe.Plugin.GUIDChooser();
            this.lbcrap = new Avalonia.Controls.TextBlock();
            this.pntheme = new Avalonia.Controls.Panel();
            this.btUgrade = new Avalonia.Controls.Button();
            this.pictureBox1 = new PictureBox();
            this.cbischance.IsCheckedChanged += (s,e) => this.cbischance_CheckedChanged(s, EventArgs.Empty);
            this.lnudChanceCurrentLevel.ValueChanged += (s,e) => this.lnudChanceCurrentLevel_ValueChanged(s, EventArgs.Empty);
            this.lnudChancePercent.ValueChanged += (s,e) => this.lnudChancePercent_ValueChanged(s, EventArgs.Empty);
            this.ChanceCopy.Click += (s,e) => this.ChanceCopy_LinkClicked(s, EventArgs.Empty);
            this.PromoList.SelectionChanged += (s,e) => this.PromoList_SelectedIndexChanged(s, EventArgs.Empty);
            this.cbTrick.SelectionChanged += (s,e) => this.cbTrick_SelectedIndexChanged(s, EventArgs.Empty);
            this.PromoFriends.ValueChanged += (s,e) => this.Promo_ValueChanged(s, EventArgs.Empty);
            this.PromoFriends.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.Promo_KeyUp);
            this.PromoCleaning.ValueChanged += (s,e) => this.Promo_ValueChanged(s, EventArgs.Empty);
            this.PromoCleaning.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.Promo_KeyUp);
            this.PromoLogic.ValueChanged += (s,e) => this.Promo_ValueChanged(s, EventArgs.Empty);
            this.PromoLogic.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.Promo_KeyUp);
            this.PromoCreativity.ValueChanged += (s,e) => this.Promo_ValueChanged(s, EventArgs.Empty);
            this.PromoCreativity.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.Promo_KeyUp);
            this.PromoCharisma.ValueChanged += (s,e) => this.Promo_ValueChanged(s, EventArgs.Empty);
            this.PromoCharisma.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.Promo_KeyUp);
            this.PromoBody.ValueChanged += (s,e) => this.Promo_ValueChanged(s, EventArgs.Empty);
            this.PromoBody.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.Promo_KeyUp);
            this.PromoMechanical.ValueChanged += (s,e) => this.Promo_ValueChanged(s, EventArgs.Empty);
            this.PromoMechanical.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.Promo_KeyUp);
            this.PromoCooking.ValueChanged += (s,e) => this.Promo_ValueChanged(s, EventArgs.Empty);
            this.PromoCooking.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.Promo_KeyUp);
            this.lnudFoods.ValueChanged += (s,e) => this.lnudFoods_ValueChanged(s, EventArgs.Empty);
            this.lnudWorkStart.ValueChanged += (s,e) => this.lnudWork_ValueChanged(s, EventArgs.Empty);
            this.lnudWorkStart.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.lnudWork_KeyUp);
            this.lnudWorkHours.ValueChanged += (s,e) => this.lnudWork_ValueChanged(s, EventArgs.Empty);
            this.lnudWorkHours.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.lnudWork_KeyUp);
            this.lnudWages.ValueChanged += (s,e) => this.lnudWork_ValueChanged(s, EventArgs.Empty);
            this.lnudWages.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.lnudWork_KeyUp);
            this.lnudWagesDog.ValueChanged += (s,e) => this.lnudWork_ValueChanged(s, EventArgs.Empty);
            this.lnudWagesDog.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.lnudWork_KeyUp);
            this.lnudWagesCat.ValueChanged += (s,e) => this.lnudWork_ValueChanged(s, EventArgs.Empty);
            this.lnudWagesCat.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.lnudWork_KeyUp);
            this.WorkMonday.IsCheckedChanged += (s,e) => this.Workday_CheckedChanged(s, EventArgs.Empty);
            this.WorkTuesday.IsCheckedChanged += (s,e) => this.Workday_CheckedChanged(s, EventArgs.Empty);
            this.WorkWednesday.IsCheckedChanged += (s,e) => this.Workday_CheckedChanged(s, EventArgs.Empty);
            this.WorkThursday.IsCheckedChanged += (s,e) => this.Workday_CheckedChanged(s, EventArgs.Empty);
            this.WorkFriday.IsCheckedChanged += (s,e) => this.Workday_CheckedChanged(s, EventArgs.Empty);
            this.WorkSaturday.IsCheckedChanged += (s,e) => this.Workday_CheckedChanged(s, EventArgs.Empty);
            this.WorkSunday.IsCheckedChanged += (s,e) => this.Workday_CheckedChanged(s, EventArgs.Empty);
            this.WorkBladder.ValueChanged += (s,e) => this.nudMotive_ValueChanged(s, EventArgs.Empty);
            this.WorkBladder.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.nudMotive_KeyUp);
            this.WorkComfort.ValueChanged += (s,e) => this.nudMotive_ValueChanged(s, EventArgs.Empty);
            this.WorkComfort.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.nudMotive_KeyUp);
            this.WorkPublic.ValueChanged += (s,e) => this.nudMotive_ValueChanged(s, EventArgs.Empty);
            this.WorkPublic.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.nudMotive_KeyUp);
            this.WorkHunger.ValueChanged += (s,e) => this.nudMotive_ValueChanged(s, EventArgs.Empty);
            this.WorkHunger.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.nudMotive_KeyUp);
            this.WorkEnergy.ValueChanged += (s,e) => this.nudMotive_ValueChanged(s, EventArgs.Empty);
            this.WorkEnergy.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.nudMotive_KeyUp);
            this.WorkFun.ValueChanged += (s,e) => this.nudMotive_ValueChanged(s, EventArgs.Empty);
            this.WorkFun.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.nudMotive_KeyUp);
            this.WorkSunshine.ValueChanged += (s,e) => this.nudMotive_ValueChanged(s, EventArgs.Empty);
            this.WorkSunshine.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.nudMotive_KeyUp);
            this.WorkHygiene.ValueChanged += (s,e) => this.nudMotive_ValueChanged(s, EventArgs.Empty);
            this.WorkHygiene.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.nudMotive_KeyUp);
            this.WorkAmorous.ValueChanged += (s,e) => this.nudMotive_ValueChanged(s, EventArgs.Empty);
            this.WorkAmorous.KeyUp += new System.EventHandler<Avalonia.Input.KeyEventArgs>(this.nudMotive_KeyUp);
            this.numLscore.ValueChanged += (s,e) => this.numLscore_ValueChanged(s, EventArgs.Empty);
            this.numPTO.ValueChanged += (s,e) => this.numPTO_ValueChanged(s, EventArgs.Empty);
            this.HoursWagesList.SelectionChanged += (s,e) => this.HoursWagesList_SelectedIndexChanged(s, EventArgs.Empty);
            this.gcVehicle.GUIDChooserValueChanged += (s,e) => this.gcVehicle_GUIDChooserValueChanged(s, EventArgs.Empty);
            this.gcOutfit.GUIDChooserValueChanged += (s,e) => this.gcOutfit_GUIDChooserValueChanged(s, EventArgs.Empty);
            this.JobDetailsCopy.Click += (s,e) => this.JobDetailsCopy_LinkClicked(s, EventArgs.Empty);
            this.jdpFemale.TitleValueChanged += (s,e) => this.jdpFemale_TitleValueChanged(s, EventArgs.Empty);
            this.jdpFemale.DescValueChanged += (s,e) => this.jdpFemale_DescValueChanged(s, EventArgs.Empty);
            this.jdpMale.TitleValueChanged += (s,e) => this.jdpMale_TitleValueChanged(s, EventArgs.Empty);
            this.jdpMale.DescValueChanged += (s,e) => this.jdpMale_DescValueChanged(s, EventArgs.Empty);
            this.JobDetailList.SelectionChanged += (s,e) => this.JobDetailList_SelectedIndexChanged(s, EventArgs.Empty);
            this.btmajApply.Click += (s,e) => this.btmajApply_Click(s, EventArgs.Empty);
            this.checkBox42.IsCheckedChanged += (s,e) => this.checkBchanceBox_checkup(s, EventArgs.Empty);
            this.checkBox43.IsCheckedChanged += (s,e) => this.chanceBcheckBox_checkup(s, EventArgs.Empty);
            this.checkBox44.IsCheckedChanged += (s,e) => this.boxCheckBchance_checkup(s, EventArgs.Empty);
            this.checkBox45.IsCheckedChanged += (s,e) => this.chanceAcheckBox_checkup(s, EventArgs.Empty);
            this.textBox17.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.textBox18.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.checkBox38.IsCheckedChanged += (s,e) => this.checkBchanceBox_checkup(s, EventArgs.Empty);
            this.checkBox39.IsCheckedChanged += (s,e) => this.chanceBcheckBox_checkup(s, EventArgs.Empty);
            this.checkBox40.IsCheckedChanged += (s,e) => this.boxCheckBchance_checkup(s, EventArgs.Empty);
            this.checkBox41.IsCheckedChanged += (s,e) => this.chanceAcheckBox_checkup(s, EventArgs.Empty);
            this.textBox15.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.textBox16.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.checkBox34.IsCheckedChanged += (s,e) => this.checkBchanceBox_checkup(s, EventArgs.Empty);
            this.checkBox35.IsCheckedChanged += (s,e) => this.chanceBcheckBox_checkup(s, EventArgs.Empty);
            this.checkBox36.IsCheckedChanged += (s,e) => this.boxCheckBchance_checkup(s, EventArgs.Empty);
            this.checkBox37.IsCheckedChanged += (s,e) => this.chanceAcheckBox_checkup(s, EventArgs.Empty);
            this.textBox13.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.textBox14.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.checkBox30.IsCheckedChanged += (s,e) => this.checkBchanceBox_checkup(s, EventArgs.Empty);
            this.checkBox31.IsCheckedChanged += (s,e) => this.chanceBcheckBox_checkup(s, EventArgs.Empty);
            this.checkBox32.IsCheckedChanged += (s,e) => this.boxCheckBchance_checkup(s, EventArgs.Empty);
            this.checkBox33.IsCheckedChanged += (s,e) => this.chanceAcheckBox_checkup(s, EventArgs.Empty);
            this.textBox11.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.textBox12.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.checkBox26.IsCheckedChanged += (s,e) => this.checkBchanceBox_checkup(s, EventArgs.Empty);
            this.checkBox27.IsCheckedChanged += (s,e) => this.chanceBcheckBox_checkup(s, EventArgs.Empty);
            this.checkBox28.IsCheckedChanged += (s,e) => this.boxCheckBchance_checkup(s, EventArgs.Empty);
            this.checkBox29.IsCheckedChanged += (s,e) => this.chanceAcheckBox_checkup(s, EventArgs.Empty);
            this.textBox9.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.textBox10.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.checkBox22.IsCheckedChanged += (s,e) => this.checkBchanceBox_checkup(s, EventArgs.Empty);
            this.checkBox23.IsCheckedChanged += (s,e) => this.chanceBcheckBox_checkup(s, EventArgs.Empty);
            this.checkBox24.IsCheckedChanged += (s,e) => this.boxCheckBchance_checkup(s, EventArgs.Empty);
            this.checkBox25.IsCheckedChanged += (s,e) => this.chanceAcheckBox_checkup(s, EventArgs.Empty);
            this.textBox7.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.textBox8.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.checkBox18.IsCheckedChanged += (s,e) => this.checkBchanceBox_checkup(s, EventArgs.Empty);
            this.checkBox19.IsCheckedChanged += (s,e) => this.chanceBcheckBox_checkup(s, EventArgs.Empty);
            this.checkBox20.IsCheckedChanged += (s,e) => this.boxCheckBchance_checkup(s, EventArgs.Empty);
            this.checkBox21.IsCheckedChanged += (s,e) => this.chanceAcheckBox_checkup(s, EventArgs.Empty);
            this.textBox5.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.textBox6.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.checkBox14.IsCheckedChanged += (s,e) => this.checkBchanceBox_checkup(s, EventArgs.Empty);
            this.checkBox15.IsCheckedChanged += (s,e) => this.chanceBcheckBox_checkup(s, EventArgs.Empty);
            this.checkBox16.IsCheckedChanged += (s,e) => this.boxCheckBchance_checkup(s, EventArgs.Empty);
            this.checkBox17.IsCheckedChanged += (s,e) => this.chanceAcheckBox_checkup(s, EventArgs.Empty);
            this.textBox3.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.textBox4.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.checkBox10.IsCheckedChanged += (s,e) => this.checkBchanceBox_checkup(s, EventArgs.Empty);
            this.checkBox11.IsCheckedChanged += (s,e) => this.chanceBcheckBox_checkup(s, EventArgs.Empty);
            this.checkBox12.IsCheckedChanged += (s,e) => this.boxCheckBchance_checkup(s, EventArgs.Empty);
            this.checkBox13.IsCheckedChanged += (s,e) => this.chanceAcheckBox_checkup(s, EventArgs.Empty);
            this.textBox1.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.textBox2.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.checkBox6.IsCheckedChanged += (s,e) => this.checkBchanceBox_checkup(s, EventArgs.Empty);
            this.checkBox5.IsCheckedChanged += (s,e) => this.chanceBcheckBox_checkup(s, EventArgs.Empty);
            this.checkBox4.IsCheckedChanged += (s,e) => this.boxCheckBchance_checkup(s, EventArgs.Empty);
            this.checkBox3.IsCheckedChanged += (s,e) => this.chanceAcheckBox_checkup(s, EventArgs.Empty);
            this.textBox1b.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.textBox1g.TextChanged += (s,e) => this.textBox1g_TextChanged(s, EventArgs.Empty);
            this.checkBox2.IsCheckedChanged += (s,e) => this.checkBox2_CheckedChanged(s, EventArgs.Empty);
            this.checkBox1.IsCheckedChanged += (s,e) => this.checkBox1_CheckedChanged(s, EventArgs.Empty);
            this.CareerTitle.TextChanged += (s,e) => this.CareerTitle_TextChanged(s, EventArgs.Empty);
            this.Language.SelectionChanged += (s,e) => this.Language_SelectedIndexChanged(s, EventArgs.Empty);
            this.miAddLvl.Click += (s,e) => this.miAddLvl_Click(s, EventArgs.Empty);
            this.miRemoveLvl.Click += (s,e) => this.miRemoveLvl_Click(s, EventArgs.Empty);
            this.miEnglishOnly.Click += (s,e) => this.miEnglishOnly_Click(s, EventArgs.Empty);
            this.btUgrade.Click += (s,e) => this.btUgrade_Click(s, EventArgs.Empty);
            // this.ClientSize = new System.Drawing.Size(1104, 661); // not applicable for Avalonia Window
            // this.MinimumSize = new System.Drawing.Size(1120, 700); // not applicable

            // === Layout ===

            // --- Set label/button text content ---
            label1.Text = "Career Lvls";
            label3.Text = "Career Title";
            label10.Text = "Language";
            label34.Text = "Cooking"; label35.Text = "Mechanical"; label36.Text = "Body";
            label37.Text = "Charisma"; label38.Text = "Creativity"; label39.Text = "Logic";
            label40.Text = "Cleaning"; label41.Text = "Family Friends"; label101.Text = "Trick";
            label51.Text = "Text Male"; label52.Text = "Text Female";
            label48.Text = "Finish";
            label18.Text = "Amorous";
            label19.Text = "Hunger"; label20.Text = "Comfort"; label21.Text = "Hygiene";
            label22.Text = "Bladder"; label23.Text = "PerHour"; label24.Text = "times Hours";
            label25.Text = "= Total"; label26.Text = "= Total"; label27.Text = "times Hours";
            label28.Text = "PerHour"; label30.Text = "Sunshine"; label31.Text = "Social";
            label32.Text = "Energy"; label33.Text = "Fun";
            label2.Text = "Level 1"; label14.Text = "Level 2"; label15.Text = "Level 3";
            label16.Text = "Level 4"; label17.Text = "Level 5"; label42.Text = "Level 6";
            label43.Text = "Level 7"; label44.Text = "Level 8"; label45.Text = "Level 9"; label46.Text = "Level 10";
            label4.Text = "Good GUID"; label5.Text = "Bad GUID";
            label6.Text = "Pass A"; label7.Text = "Fail A"; label8.Text = "Pass B"; label9.Text = "Fail B";
            label11.Text = "Fail B"; label12.Text = "Fail A"; label13.Text = "Outfit Override\n(female only)";
            label29.Text = "A sim needs to have graduated from any one\nof these majors to be offered this career.";
            label47.Text = "A sim that has graduated one of these majors\nwill get a level boost when starting this career.";
            lbcrap.Text = "Old Career Type!\nNot compatible with Seasons EP or above";
            lbPTO.Text = "Paid Time Off (PTO) Daily Accruement";
            lbLscore.Text = "Life Score";
            lbrewguid.Text = "none";

            // --- CheckBox content ---
            cbischance.Content = "Is Available at this Level?";
            WorkMonday.Content = "Mon"; WorkTuesday.Content = "Tue"; WorkWednesday.Content = "Wed";
            WorkThursday.Content = "Thu"; WorkFriday.Content = "Fri";
            WorkSaturday.Content = "Sat"; WorkSunday.Content = "Sun";
            checkBox1.Content = "Enable Extra Rewards"; checkBox2.Content = "Enable Outfit Overrides";
            checkBox3.Content = "use"; checkBox4.Content = "use"; checkBox5.Content = "use"; checkBox6.Content = "use";
            checkBox7.Content = "use"; checkBox8.Content = "use"; checkBox9.Content = "use";
            checkBox10.Content = "use"; checkBox11.Content = "use"; checkBox12.Content = "use"; checkBox13.Content = "use";
            checkBox14.Content = "use"; checkBox15.Content = "use"; checkBox16.Content = "use"; checkBox17.Content = "use";
            checkBox18.Content = "use"; checkBox19.Content = "use"; checkBox20.Content = "use"; checkBox21.Content = "use";
            checkBox22.Content = "use"; checkBox23.Content = "use"; checkBox24.Content = "use"; checkBox25.Content = "use";
            checkBox26.Content = "use"; checkBox27.Content = "use"; checkBox28.Content = "use"; checkBox29.Content = "use";
            checkBox30.Content = "use"; checkBox31.Content = "use"; checkBox32.Content = "use"; checkBox33.Content = "use";
            checkBox34.Content = "use"; checkBox35.Content = "use"; checkBox36.Content = "use"; checkBox37.Content = "use";
            checkBox38.Content = "use"; checkBox39.Content = "use"; checkBox40.Content = "use"; checkBox41.Content = "use";
            checkBox42.Content = "use"; checkBox43.Content = "use"; checkBox44.Content = "use"; checkBox45.Content = "use";
            checkBox46.Content = "use"; checkBox47.Content = "use"; checkBox48.Content = "use";
            checkBox49.Content = "use"; checkBox50.Content = "use"; checkBox51.Content = "use";
            checkBox52.Content = "use"; checkBox53.Content = "use"; checkBox54.Content = "use";
            checkBox55.Content = "use"; checkBox56.Content = "use"; checkBox57.Content = "use";
            checkBox58.Content = "use"; checkBox59.Content = "use"; checkBox60.Content = "use";
            checkBox61.Content = "use"; checkBox62.Content = "use"; checkBox63.Content = "use";
            checkBox64.Content = "use"; checkBox65.Content = "use"; checkBox66.Content = "use";
            checkBox67.Content = "use"; checkBox68.Content = "use"; checkBox69.Content = "use";
            checkBox70.Content = "use"; checkBox71.Content = "use"; checkBox72.Content = "use";

            // --- Majors affiliation checkboxes ---
            cbaArt.Content = "Art"; cbabiol.Content = "Biology"; cbadrama.Content = "Drama";
            cbaecon.Content = "Economics"; cbahisto.Content = "History"; cbaliter.Content = "Literature";
            cbamaths.Content = "Mathematics"; cbaphysi.Content = "Physics";
            cbaphyco.Content = "Psychology"; cbapolit.Content = "Political Science";

            // --- Majors required checkboxes ---
            cbrArt.Content = "Art"; cbrbiol.Content = "Biology"; cbrdrama.Content = "Drama";
            cbrecon.Content = "Economics"; cbrhisto.Content = "History"; cbrliter.Content = "Literature";
            cbrmaths.Content = "Mathematics"; cbrphysi.Content = "Physics";
            cbrphyco.Content = "Psychology"; cbrpolit.Content = "Political Science";

            // --- Button content ---
            btUgrade.Content = "Upgrade to Latest EP";
            btexApply.Content = "Apply";
            btmajApply.Content = "Apply";
            JobDetailsCopy.Content = "Copy >";
            ChanceCopy.Content = "Copy >";

            // --- LabelledNumericUpDown labels ---
            lnudWorkStart.Label = "Start"; lnudWorkHours.Label = "Hours";
            lnudWages.Label = "Wages"; lnudWagesDog.Label = "Dog Wages"; lnudWagesCat.Label = "Cat Wages";
            lnudFoods.Label = "Food Inc.";
            lnudChanceCurrentLevel.Label = "Career Level"; lnudChancePercent.Label = "% Chance";
            lnudPetChancePercent.Label = "% Pet Chance";

            // --- Menu hierarchy ---
            menuItem6.Header = "&Levels";
            miAddLvl.Header = "&Add Level";
            miRemoveLvl.Header = "&Remove Level";
            menuItem1.Header = "L&anguages";
            miEnglishOnly.Header = "US English only";
            miEnglishOnly.ToggleType = Avalonia.Controls.MenuItemToggleType.CheckBox;
            menuItem6.Items.Add(miAddLvl);
            menuItem6.Items.Add(miRemoveLvl);
            menuItem1.Items.Add(miEnglishOnly);
            mainMenu1.Items.Add(menuItem6);
            mainMenu1.Items.Add(menuItem1);

            // --- tabPage1: Job Details ---
            var jdRightStack = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Vertical, Spacing = 4 };
            jdRightStack.Children.Add(jdpMale);
            jdRightStack.Children.Add(jdpFemale);
            jdRightStack.Children.Add(JobDetailsCopy);
            jdRightStack.Children.Add(gcOutfit);
            jdRightStack.Children.Add(gcVehicle);
            gbJobDetails.Content = jdRightStack;

            gbJLDetails.Content = JobDetailList;

            var jdGrid = new Avalonia.Controls.Grid();
            jdGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = new Avalonia.Controls.GridLength(1, Avalonia.Controls.GridUnitType.Star) });
            jdGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = new Avalonia.Controls.GridLength(1, Avalonia.Controls.GridUnitType.Star) });
            Avalonia.Controls.Grid.SetColumn(gbJLDetails, 0);
            Avalonia.Controls.Grid.SetColumn(gbJobDetails, 1);
            jdGrid.Children.Add(gbJLDetails);
            jdGrid.Children.Add(gbJobDetails);

            tabPage1.Header = "Job Details";
            tabPage1.Content = jdGrid;

            // --- tabPage2: Hours & Wages ---
            // Motive deltas grid (9 rows, 6 columns: label | work NUD | × | hours NUD | = | total NUD)
            var motiveGrid = new Avalonia.Controls.Grid();
            for (int r = 0; r < 9; r++)
                motiveGrid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition { Height = Avalonia.Controls.GridLength.Auto });
            motiveGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = new Avalonia.Controls.GridLength(80, Avalonia.Controls.GridUnitType.Pixel) });
            motiveGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = Avalonia.Controls.GridLength.Auto });
            motiveGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = Avalonia.Controls.GridLength.Auto });
            motiveGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = Avalonia.Controls.GridLength.Auto });
            motiveGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = Avalonia.Controls.GridLength.Auto });
            motiveGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = Avalonia.Controls.GridLength.Auto });

            var motiveRows = new (Avalonia.Controls.TextBlock lbl, Avalonia.Controls.NumericUpDown work, Avalonia.Controls.TextBlock perHour, Avalonia.Controls.NumericUpDown hours, Avalonia.Controls.TextBlock eq, Avalonia.Controls.NumericUpDown total)[]
            {
                (label19, WorkHunger,   label23, HungerHours,   label25, HungerTotal),
                (label20, WorkComfort,  label23, ComfortHours,  label25, ComfortTotal),
                (label21, WorkHygiene,  label23, HygieneHours,  label25, HygieneTotal),
                (label22, WorkBladder,  label23, BladderHours,  label25, BladderTotal),
                (label32, WorkEnergy,   label28, EnergyHours,   label26, EnergyTotal),
                (label33, WorkFun,      label28, FunHours,      label26, FunTotal),
                (label31, WorkPublic,   label28, PublicHours,   label26, PublicTotal),
                (label30, WorkSunshine, label28, SunshineHours, label26, SunshineTotal),
                (label18, WorkAmorous,  label28, AmorousHours,  label26, AmorousTotal),
            };

            // We need fresh TextBlock instances for the repeated × and = labels per row
            var perHourLabels = new[] { "×", "×", "×", "×", "×", "×", "×", "×", "×" };
            var eqLabels      = new[] { "=", "=", "=", "=", "=", "=", "=", "=", "=" };

            for (int row = 0; row < motiveRows.Length; row++)
            {
                var (lbl, work, _, hours, __, total) = motiveRows[row];

                var rowLbl = new Avalonia.Controls.TextBlock { Text = lbl.Text, VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center };
                var mulLbl = new Avalonia.Controls.TextBlock { Text = perHourLabels[row], VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center, Margin = new Avalonia.Thickness(4, 0) };
                var eqLbl  = new Avalonia.Controls.TextBlock { Text = eqLabels[row],      VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center, Margin = new Avalonia.Thickness(4, 0) };

                // Transfer IsVisible from the original hidden label18 row
                bool visible = lbl != label18;
                rowLbl.IsVisible = work.IsVisible = mulLbl.IsVisible = hours.IsVisible = eqLbl.IsVisible = total.IsVisible = visible;

                Avalonia.Controls.Grid.SetRow(rowLbl, row); Avalonia.Controls.Grid.SetColumn(rowLbl, 0);
                Avalonia.Controls.Grid.SetRow(work,   row); Avalonia.Controls.Grid.SetColumn(work,   1);
                Avalonia.Controls.Grid.SetRow(mulLbl, row); Avalonia.Controls.Grid.SetColumn(mulLbl, 2);
                Avalonia.Controls.Grid.SetRow(hours,  row); Avalonia.Controls.Grid.SetColumn(hours,  3);
                Avalonia.Controls.Grid.SetRow(eqLbl,  row); Avalonia.Controls.Grid.SetColumn(eqLbl,  4);
                Avalonia.Controls.Grid.SetRow(total,  row); Avalonia.Controls.Grid.SetColumn(total,  5);

                motiveGrid.Children.Add(rowLbl);
                motiveGrid.Children.Add(work);
                motiveGrid.Children.Add(mulLbl);
                motiveGrid.Children.Add(hours);
                motiveGrid.Children.Add(eqLbl);
                motiveGrid.Children.Add(total);
            }
            gbHWMotives.Text = "Motives";
            gbHWMotives.Content = motiveGrid;

            var daysStack = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 4 };
            daysStack.Children.Add(WorkMonday); daysStack.Children.Add(WorkTuesday);
            daysStack.Children.Add(WorkWednesday); daysStack.Children.Add(WorkThursday);
            daysStack.Children.Add(WorkFriday); daysStack.Children.Add(WorkSaturday);
            daysStack.Children.Add(WorkSunday);

            var hwFinishRow = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 4 };
            hwFinishRow.Children.Add(label48); hwFinishRow.Children.Add(tbWorkFinish);

            var hwRightStack = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Vertical, Spacing = 4 };
            hwRightStack.Children.Add(lnudWorkStart);
            hwRightStack.Children.Add(lnudWorkHours);
            hwRightStack.Children.Add(hwFinishRow);
            hwRightStack.Children.Add(lnudWages);
            hwRightStack.Children.Add(lnudWagesDog);
            hwRightStack.Children.Add(lnudWagesCat);
            hwRightStack.Children.Add(lnudFoods);
            hwRightStack.Children.Add(daysStack);
            var lscoreRow = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 4 };
            lscoreRow.Children.Add(lbLscore); lscoreRow.Children.Add(numLscore);
            var ptoRow = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 4 };
            ptoRow.Children.Add(lbPTO); ptoRow.Children.Add(numPTO);
            hwRightStack.Children.Add(lscoreRow);
            hwRightStack.Children.Add(ptoRow);
            gbHoursWages.Content = hwRightStack;

            gbJLHoursWages.Content = HoursWagesList;

            var hwGrid = new Avalonia.Controls.Grid();
            hwGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = new Avalonia.Controls.GridLength(1, Avalonia.Controls.GridUnitType.Star) });
            hwGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = new Avalonia.Controls.GridLength(1, Avalonia.Controls.GridUnitType.Star) });
            Avalonia.Controls.Grid.SetColumn(gbJLHoursWages, 0);
            Avalonia.Controls.Grid.SetColumn(gbHoursWages, 1);
            hwGrid.Children.Add(gbJLHoursWages);
            hwGrid.Children.Add(gbHoursWages);

            var hwOuter = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Vertical, Spacing = 4 };
            hwOuter.Children.Add(hwGrid);
            hwOuter.Children.Add(gbHWMotives);

            tabPage2.Header = "Hours & Wages";
            tabPage2.Content = hwOuter;

            // --- tabPage3: Promotion ---
            var promoSkillGrid = new Avalonia.Controls.Grid();
            promoSkillGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = Avalonia.Controls.GridLength.Auto });
            promoSkillGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = Avalonia.Controls.GridLength.Auto });
            var promoSkillRows = new (Avalonia.Controls.TextBlock lbl, Avalonia.Controls.NumericUpDown nud)[]
            {
                (label34, PromoCooking), (label35, PromoMechanical), (label36, PromoBody),
                (label37, PromoCharisma), (label38, PromoCreativity), (label39, PromoLogic),
                (label40, PromoCleaning), (label41, PromoFriends),
            };
            for (int i = 0; i < promoSkillRows.Length; i++)
            {
                promoSkillGrid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition { Height = Avalonia.Controls.GridLength.Auto });
                Avalonia.Controls.Grid.SetRow(promoSkillRows[i].lbl, i); Avalonia.Controls.Grid.SetColumn(promoSkillRows[i].lbl, 0);
                Avalonia.Controls.Grid.SetRow(promoSkillRows[i].nud, i); Avalonia.Controls.Grid.SetColumn(promoSkillRows[i].nud, 1);
                promoSkillGrid.Children.Add(promoSkillRows[i].lbl);
                promoSkillGrid.Children.Add(promoSkillRows[i].nud);
            }
            // Trick row
            promoSkillGrid.RowDefinitions.Add(new Avalonia.Controls.RowDefinition { Height = Avalonia.Controls.GridLength.Auto });
            Avalonia.Controls.Grid.SetRow(label101, promoSkillRows.Length); Avalonia.Controls.Grid.SetColumn(label101, 0);
            Avalonia.Controls.Grid.SetRow(cbTrick,  promoSkillRows.Length); Avalonia.Controls.Grid.SetColumn(cbTrick,  1);
            promoSkillGrid.Children.Add(label101);
            promoSkillGrid.Children.Add(cbTrick);

            gbPromo.Content = promoSkillGrid;
            gbJLPromo.Content = PromoList;

            var promoGrid = new Avalonia.Controls.Grid();
            promoGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = new Avalonia.Controls.GridLength(1, Avalonia.Controls.GridUnitType.Star) });
            promoGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = new Avalonia.Controls.GridLength(1, Avalonia.Controls.GridUnitType.Star) });
            Avalonia.Controls.Grid.SetColumn(gbJLPromo, 0);
            Avalonia.Controls.Grid.SetColumn(gbPromo, 1);
            promoGrid.Children.Add(gbJLPromo);
            promoGrid.Children.Add(gbPromo);

            tabPage3.Header = "Promotion";
            tabPage3.Content = promoGrid;

            // --- tabPage4: Chance Cards ---
            tabPage5.Header = "Pass A"; tabPage5.Content = epPassA;
            tabPage6.Header = "Fail A"; tabPage6.Content = epFailA;
            tabPage7.Header = "Pass B"; tabPage7.Content = epPassB;
            tabPage8.Header = "Fail B"; tabPage8.Content = epFailB;
            tcChanceOutcome.Items.Add(tabPage5);
            tcChanceOutcome.Items.Add(tabPage6);
            tcChanceOutcome.Items.Add(tabPage7);
            tcChanceOutcome.Items.Add(tabPage8);

            var chanceMaleRow = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 4 };
            chanceMaleRow.Children.Add(label51); chanceMaleRow.Children.Add(ChanceTextMale); chanceMaleRow.Children.Add(ChanceCopy);
            var chanceFemaleRow = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 4 };
            chanceFemaleRow.Children.Add(label52); chanceFemaleRow.Children.Add(ChanceTextFemale);

            var chanceStack = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Vertical, Spacing = 4 };
            chanceStack.Children.Add(cbischance);
            chanceStack.Children.Add(lnudChanceCurrentLevel);
            chanceStack.Children.Add(lnudChancePercent);
            chanceStack.Children.Add(lnudPetChancePercent);
            chanceStack.Children.Add(cpChoiceA);
            chanceStack.Children.Add(chanceMaleRow);
            chanceStack.Children.Add(chanceFemaleRow);
            chanceStack.Children.Add(tcChanceOutcome);

            tabPage4.Header = "Chance Cards";
            tabPage4.Content = chanceStack;

            // --- tabPagMajor: Majors ---
            var majAffilStack = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Vertical, Spacing = 2 };
            majAffilStack.Children.Add(label47);
            majAffilStack.Children.Add(cbaArt); majAffilStack.Children.Add(cbabiol);
            majAffilStack.Children.Add(cbadrama); majAffilStack.Children.Add(cbaecon);
            majAffilStack.Children.Add(cbahisto); majAffilStack.Children.Add(cbaliter);
            majAffilStack.Children.Add(cbamaths); majAffilStack.Children.Add(cbaphysi);
            majAffilStack.Children.Add(cbaphyco); majAffilStack.Children.Add(cbapolit);
            gbmajaffil.Text = "Level Boost (Affiliated)";
            gbmajaffil.Content = majAffilStack;

            var majRequirStack = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Vertical, Spacing = 2 };
            majRequirStack.Children.Add(label29);
            majRequirStack.Children.Add(cbrArt); majRequirStack.Children.Add(cbrbiol);
            majRequirStack.Children.Add(cbrdrama); majRequirStack.Children.Add(cbrecon);
            majRequirStack.Children.Add(cbrhisto); majRequirStack.Children.Add(cbrliter);
            majRequirStack.Children.Add(cbrmaths); majRequirStack.Children.Add(cbrphysi);
            majRequirStack.Children.Add(cbrphyco); majRequirStack.Children.Add(cbrpolit);
            gbrequir.Text = "Required to Enter";
            gbrequir.Content = majRequirStack;

            var majorsGrid = new Avalonia.Controls.Grid();
            majorsGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = new Avalonia.Controls.GridLength(1, Avalonia.Controls.GridUnitType.Star) });
            majorsGrid.ColumnDefinitions.Add(new Avalonia.Controls.ColumnDefinition { Width = new Avalonia.Controls.GridLength(1, Avalonia.Controls.GridUnitType.Star) });
            Avalonia.Controls.Grid.SetColumn(gbmajaffil, 0);
            Avalonia.Controls.Grid.SetColumn(gbrequir, 1);
            majorsGrid.Children.Add(gbmajaffil);
            majorsGrid.Children.Add(gbrequir);

            gpmajors.Children.Add(majorsGrid);
            gpmajors.Children.Add(btmajApply);

            tabPagMajor.Header = "Majors";
            tabPagMajor.Content = gpmajors;

            // --- tabPage9: Extra Reward Items (always removed at runtime; simple layout) ---
            var extrasScroll = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Vertical, Spacing = 4 };

            // Header controls
            var extrasTopRow = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 8 };
            extrasTopRow.Children.Add(checkBox1); extrasTopRow.Children.Add(checkBox2);
            extrasScroll.Children.Add(extrasTopRow);

            var extrasHeaderRow = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 8 };
            extrasHeaderRow.Children.Add(label4); extrasHeaderRow.Children.Add(label5);
            extrasHeaderRow.Children.Add(label6); extrasHeaderRow.Children.Add(label7);
            extrasHeaderRow.Children.Add(label8); extrasHeaderRow.Children.Add(label9);
            extrasHeaderRow.Children.Add(label13);
            extrasScroll.Children.Add(extrasHeaderRow);

            // Level sections: label, combobox, 4 checkboxes, 2 textboxes per level
            var levelLabels   = new Avalonia.Controls.TextBlock[] { label2, label14, label15, label16, label17, label42, label43, label44, label45, label46 };
            var levelCombos   = new Avalonia.Controls.ComboBox[]  { comboBox1, comboBox2, comboBox3, comboBox4, comboBox5, comboBox6, comboBox7, comboBox8, comboBox9, comboBox10 };
            var levelChecksA  = new Avalonia.Controls.CheckBox[]  { checkBox3,  checkBox46, checkBox49, checkBox52, checkBox55, checkBox58, checkBox61, checkBox64, checkBox67, checkBox70 };
            var levelChecksB  = new Avalonia.Controls.CheckBox[]  { checkBox4,  checkBox47, checkBox50, checkBox53, checkBox56, checkBox59, checkBox62, checkBox65, checkBox68, checkBox71 };
            var levelChecksC  = new Avalonia.Controls.CheckBox[]  { checkBox5,  checkBox48, checkBox51, checkBox54, checkBox57, checkBox60, checkBox63, checkBox66, checkBox69, checkBox72 };
            var levelChecksD  = new Avalonia.Controls.CheckBox[]  { checkBox6,  checkBox7,  checkBox8,  checkBox9,  checkBox10, checkBox11, checkBox12, checkBox13, checkBox14, checkBox15 };
            var levelTextsG   = new Avalonia.Controls.TextBox[]   { textBox1g,  textBox1,   textBox3,   textBox5,   textBox7,   textBox9,   textBox11,  textBox13,  textBox15,  textBox17  };
            var levelTextsB   = new Avalonia.Controls.TextBox[]   { textBox1b,  textBox2,   textBox4,   textBox6,   textBox8,   textBox10,  textBox12,  textBox14,  textBox16,  textBox18  };

            for (int lvl = 0; lvl < 10; lvl++)
            {
                var lvlRow = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 4 };
                lvlRow.Children.Add(levelLabels[lvl]);
                lvlRow.Children.Add(levelCombos[lvl]);
                lvlRow.Children.Add(levelChecksA[lvl]);
                lvlRow.Children.Add(levelChecksB[lvl]);
                lvlRow.Children.Add(levelChecksC[lvl]);
                lvlRow.Children.Add(levelChecksD[lvl]);
                lvlRow.Children.Add(levelTextsG[lvl]);
                lvlRow.Children.Add(levelTextsB[lvl]);
                extrasScroll.Children.Add(lvlRow);
            }

            var extrasBottomRow = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 4 };
            extrasBottomRow.Children.Add(lbrewguid);
            extrasBottomRow.Children.Add(btexApply);
            extrasBottomRow.Children.Add(pictureBox2);
            extrasScroll.Children.Add(extrasBottomRow);

            gbExtras.Content = extrasScroll;

            tabPage9.Header = "Extra Reward Items";
            tabPage9.Content = gbExtras;

            // --- Assemble tabControl1 ---
            tabControl1.Items.Add(tabPage1);
            tabControl1.Items.Add(tabPage2);
            tabControl1.Items.Add(tabPage3);
            tabControl1.Items.Add(tabPage4);
            tabControl1.Items.Add(tabPagMajor);
            tabControl1.Items.Add(tabPage9);

            // --- Window root: DockPanel ---
            var rootDock = new Avalonia.Controls.DockPanel { LastChildFill = true };

            // Menu bar (Dock.Top)
            Avalonia.Controls.DockPanel.SetDock(mainMenu1, Avalonia.Controls.Dock.Top);
            rootDock.Children.Add(mainMenu1);

            // Top info bar (Dock.Top)
            var topBar = new Avalonia.Controls.StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 6 };
            topBar.Children.Add(label3);  topBar.Children.Add(CareerTitle);
            topBar.Children.Add(label1);  topBar.Children.Add(CareerLvls);
            topBar.Children.Add(label10); topBar.Children.Add(Language);
            topBar.Children.Add(pictureBox1);
            topBar.Children.Add(lbcrap);
            topBar.Children.Add(btUgrade);
            Avalonia.Controls.DockPanel.SetDock(topBar, Avalonia.Controls.Dock.Top);
            rootDock.Children.Add(topBar);

            // TabControl fills remaining space (LastChildFill = true)
            rootDock.Children.Add(tabControl1);

            this.Content = rootDock;
		}
		#endregion

        #region Global
        private SimPe.Packages.GeneratableFile package;
        private uint groupId;
        private StrWrapper catalogueDesc;
        private Bcon tuning;
        private Bcon lifeScore;
        private Bcon PTO;
        private Bcon goodRew;
        private Bcon badRew;
        private Bcon majors;
        private Bcon cclevels;

        private bool rewenabled = false;
        private bool preuniv = false;
        private bool isCastaway = false;
        static TextBox tbox;

        #region Reward and Upgrade

        private static String[] rewardName = new String[] {
                "Biotech Station",
                "Camera",
                "Candy Factory",
                "Cow Plant",
                "Fingerprint Kit",
                "Home Plastic Surgery Kit",
                "Hydroponic Garden",
                "Obstacle Course",
                "Polygraph",
                "Putting Green",
                "Punching Bag",
                "Resurrectonomitron",
                "Surgical Dummy",
                "Teleprompter",
                "Drafting Table",
                "Ballet Bar",
                "Books First Bookshelf",
                "Starstruck Fame Star Rug",
                "Guitar",
                "Journalism Award",
                "Carefree Koi Pond",
                "Lectern",
                "Pinball",
                "Surveillance Mic",
                "Golden Skull of Jumbok IV",
        };
        private static UInt32[] rewardGUID = new UInt32[] {
                0x0C6E194A, 0xAEB9F591, 0x8C4D2997, 0xCEA505BB,
                0xD1CD15C8, 0x4E9FBE5D, 0xCC58DF85, 0x6C2979FB,
                0xCC16D816, 0xCC20426A, 0x4C2148B0, 0xAE8D50B2,
                0x6C6CE31F, 0xAC314A3A, 0xB3FD5372, 0x33EC6E0A,
                0x524E1066, 0x33E2E4E8, 0x324D0D87, 0xD24CE39C,
                0x53EDA12F, 0x124E3138, 0xF24CFF80, 0xB3FEC1B6,
                0xD24D09FD,
        };

        private static String[] upgradeName = new String[] {
            "Adventurer", "Artist", "Athletics", "Business",
            "Construction", "Criminal", "Culinary", "Dance",
            "Economy", "Entertainment", "Education", "Gamer",
            "Intelligence", "Journalism", "Law", "Law Enforcement",
            "Medicine", "Military", "Music", "NaturalScientist",
            "Ocenography", "Paranormal", "Politics", "Science",
            "ShowBiz", "Slacker", "Owned Business",
        };
        private static UInt32[] upgradeGUID = new UInt32[] {
            0x3240CBA5, 0x4E6FFFBC, 0x2C89E95F, 0x45196555,
            0xF3E1C301, 0x6C9EBD0E, 0xEC9EBD5F, 0xD3E09422,
            0x45196555, 0xB3E09417, 0x72428B30, 0xF240C306,
            0x33E0940E, 0x7240D944, 0x12428B19, 0xAC9EBCE3,
            0x0C7761FD, 0x6C9EBD32, 0xB2428B0C, 0xEE70001C,
            0x73E09404, 0x2E6FFF87, 0x2C945B14, 0x0C9EBD47,
            0xAE6FFFB0, 0xEC77620B, 0xD08F400A,
        };

        // end crap, start goody

        private static String[] TArewardName = new String[] {
                "Biotech Station",
                "Camera",
                "Candy Factory",
                "Cow Plant",
                "Fingerprint Kit",
                "Home Plastic Surgery Kit",
                "Hydroponic Garden",
                "Obstacle Course",
                "Polygraph",
                "Putting Green",
                "Punching Bag",
                "Resurrectonomitron",
                "Surgical Dummy",
                "Teleprompter",
                "Drafting Table",
                "Ballet Bar",
                "Books First Bookshelf",
                "Starstruck Fame Star Rug",
                "Guitar",
                "Journalism Award",
                "Carefree Koi Pond",
                "Lectern",
                "Pinball",
                "Surveillance Mic",
                "Golden Skull of Jumbok IV",
                "Frankensim Maker",
        };
        private static UInt32[] TArewardGUID = new UInt32[] {
                0x0C6E194A, 0xAEB9F591, 0x8C4D2997, 0xCEA505BB,
                0xD1CD15C8, 0x4E9FBE5D, 0xCC58DF85, 0x6C2979FB,
                0xCC16D816, 0xCC20426A, 0x4C2148B0, 0xAE8D50B2,
                0x6C6CE31F, 0xAC314A3A, 0xB3FD5372, 0x33EC6E0A,
                0x524E1066, 0x33E2E4E8, 0x324D0D87, 0xD24CE39C,
                0x53EDA12F, 0x124E3138, 0xF24CFF80, 0xB3FEC1B6,
                0xD24D09FD, 0x00845D23,
        };

        private static String[] TAupgradeName = new String[] {
            "Adventurer", "Artist", "Athletics", "Business",
            "Construction", "Criminal", "Culinary", "Dance",
            "Economy", "Entertainment", "Education", "Gamer",
            "Intelligence", "Journalism", "Law", "Law Enforcement",
            "Medicine", "Military", "Music", "NaturalScientist",
            "Ocenography", "Paranormal", "Politics", "Science",
            "ShowBiz", "Slacker",
            "Owned Business", "Party Industry",
        };

        private static UInt32[] TAupgradeGUID = new UInt32[] {
            0x3240CBA5, 0x4E6FFFBC, 0x2C89E95F, 0x45196555,
            0xF3E1C301, 0x6C9EBD0E, 0xEC9EBD5F, 0xD3E09422,
            0x45196555, 0xB3E09417, 0x72428B30, 0xF240C306,
            0x33E0940E, 0x7240D944, 0x12428B19, 0xAC9EBCE3,
            0x0C7761FD, 0x6C9EBD32, 0xB2428B0C, 0xEE70001C,
            0x73E09404, 0x2E6FFF87, 0x2C945B14, 0x0C9EBD47,
            0xAE6FFFB0, 0xEC77620B,
            0xD08F400A, 0x00845D10,
        };

        // end goody, start Castaawy

        private static String[] CSrewardName = new String[] { "Shaman Station", "Electronic Crafting Station", "Obstacle Course", };
        private static UInt32[] CSrewardGUID = new UInt32[] { 0xB3ED7E27, 0xD3CF5048, 0xB3D1BACF, };
        private static String[] CSupgradeName = new String[] { "Gatherer", "Crafter", "Hunter", };
        private static UInt32[] CSupgradeGUID = new UInt32[] { 0x738D6245, 0xD38D6534, 0x93701850, };

        private Instruction AddRewardToInventory = null;
        private Instruction JobUpgrade = null;

        private void setRewardFromGUID() { setGCFromIns(gcReward, AddRewardToInventory, 5); }
        private void getGUIDFromReward() { setInsFromGC(gcReward, AddRewardToInventory, 5); }
        private void setUpgradeFromGUID() { setGCFromIns(gcUpgrade, JobUpgrade, 0); }
        private void getGUIDFromUpgrade() { setInsFromGC(gcUpgrade, JobUpgrade, 0); }

        #endregion

        private void setGCFromIns(GUIDChooser gc, Instruction ins, int op)
        {
            if (ins == null)
            {
                gc.Value = 0;
                gc.IsEnabled = false;
                return;
            }
            byte[] o = new byte[16];
            ((byte[])ins.Operands).CopyTo(o, 0);
            ((byte[])ins.Reserved1).CopyTo(o, 8);
            gc.Value = (UInt32)(o[op] | (o[op + 1] << 8) | (o[op + 2] << 16) | (o[op + 3] << 24));
        }
        private void setInsFromGC(GUIDChooser gc, Instruction ins, int op)
        {
            if (ins == null) return; // Should never happen
            if (gc.Value == 0) // Was something, now "None"
            {
                ins.Parent.FileDescriptor.MarkForDelete = true;
                ins.Parent.Changed = true;
                return;
            }
            UInt32 guid = gc.Value;
            for (int i = 0; i < 4; i++)
                if (op + i < 8) ins.Operands[op + i] = (byte)((guid >> (i * 8)) & 0xff);
                else ins.Reserved1[op + i - 8] = (byte)((guid >> (i * 8)) & 0xff);
        }

        private bool isPetCareer = false;
        private bool isTeenCareer = false;

        private ushort noLevels;
        private ushort femaleOffset;
        private void noLevelsChanged(ushort l)
        {
            ushort oldNoLevels = noLevels;
            ushort oldLevel = currentLevel;
            if (l > oldNoLevels) oldLevel++; else if (oldLevel > 1) oldLevel--;

            noLevels = l;
            femaleOffset = (ushort)(2 * noLevels);

            miAddLvl.IsEnabled = (noLevels < 100);
            miRemoveLvl.IsEnabled = (noLevels > 1);

            CareerLvls.Text = Convert.ToString(noLevels);
            //CareerLvls.Value = noLevels;

            fillJobDetails();
            fillHoursAndWages();
            fillPromotionDetails();
            lnudChanceCurrentLevel.Maximum = noLevels;

            currentLevel = 0;
            levelChanged(oldLevel > noLevels ? noLevels : oldLevel);
        }

        private ushort currentLevel;
        private bool levelChanging = false;
        private void levelChanged(ushort newLevel)
        {
            if (levelChanging || newLevel == currentLevel) return;
            internalchg = levelChanging = true;

            //lbPTO.Text = "Paid Time Off (PTO) Daily Accruement " + PTO[newLevel];
            numPTO.Value = PTO[newLevel];

            if (lifeScore != null) numLscore.Value = lifeScore[newLevel];
            else lbLscore.IsVisible = numLscore.IsVisible = false;

            #region job details
            JobDetailList.SelectedIndices.Clear();
            JobDetailList.Items[newLevel - 1].Selected = true;
            gbJobDetails.Text = "Current Level: " + newLevel;

            List<StrItem> items = jobTitles[currentLanguage];
            jdpMale.TitleValue = items[(newLevel - 1) * 2 + 1].Title;
            jdpMale.DescValue = items[(newLevel - 1) * 2 + 2].Title;
            jdpFemale.TitleValue = items[femaleOffset + (newLevel - 1) * 2 + 1].Title;
            jdpFemale.DescValue = items[femaleOffset + (newLevel - 1) * 2 + 2].Title;

            setOutfitFromGUID(newLevel);
            setVehicleFromGUID(newLevel);
            #endregion

            #region hours & wages
            HoursWagesList.SelectedIndices.Clear();
            HoursWagesList.Items[newLevel - 1].Selected = true;
            gbHoursWages.Text = "Current Level: " + newLevel;
            lnudWorkStart.Value = startHour[newLevel];
            lnudWorkHours.Value = hoursWorked[newLevel];
            tbWorkFinish.Text = Convert.ToString((startHour[newLevel] + hoursWorked[newLevel]) % 24);
            if (isCastaway)
                lnudFoods.Value = foodinc[newLevel];

            if (!isPetCareer)
            {
                if (wages[newLevel] > lnudWages.Maximum)
                    lnudWages.Value = lnudWages.Maximum;
                else
                    lnudWages.Value = wages[newLevel];

                lnudWagesDog.IsVisible = lnudWagesCat.IsVisible = false;
            }
            else
            {
                lnudWages.IsEnabled = false;
                if (wagesDog[newLevel] > lnudWagesDog.Maximum)
                    lnudWagesDog.Value = lnudWagesDog.Maximum;
                else
                    lnudWagesDog.Value = wagesDog[newLevel];

                if (wagesCat[newLevel] > lnudWagesCat.Maximum)
                    lnudWagesCat.Value = lnudWagesCat.Maximum;
                else
                    lnudWagesCat.Value = wagesCat[newLevel];
            }

            Boolset dw = getDaysArray(daysWorked[newLevel]);
            WorkMonday.IsChecked = dw[MONDAY];
            WorkTuesday.IsChecked = dw[TUESDAY];
            WorkWednesday.IsChecked = dw[WEDNESDAY];
            WorkThursday.IsChecked = dw[THURSDAY];
            WorkFriday.IsChecked = dw[FRIDAY];
            WorkSaturday.IsChecked = dw[SATURDAY];
            WorkSunday.IsChecked = dw[SUNDAY];

            WorkHunger.Value = motiveDeltas[HUNGER][newLevel];
            WorkAmorous.Value = motiveDeltas[AMOROUS][newLevel];
            WorkComfort.Value = motiveDeltas[COMFORT][newLevel];
            WorkHygiene.Value = motiveDeltas[HYGIENE][newLevel];
            WorkBladder.Value = motiveDeltas[BLADDER][newLevel];
            WorkEnergy.Value = motiveDeltas[ENERGY][newLevel];
            WorkFun.Value = motiveDeltas[FUN][newLevel];
            WorkPublic.Value = motiveDeltas[PUBLIC][newLevel];
            WorkSunshine.Value = motiveDeltas[SUNSHINE][newLevel];

            WorkChanged(newLevel);
            #endregion

            #region promotion
            PromoList.SelectedIndices.Clear();
            PromoList.Items[newLevel - 1].Selected = true;
            gbPromo.Text = "Current Level: " + newLevel;
            if (!isPetCareer) // people
            {
                PromoCooking.Value = skillReq[COOKING][newLevel] / 100;
                PromoMechanical.Value = skillReq[MECHANICAL][newLevel] / 100;
                PromoBody.Value = skillReq[BODY][newLevel] / 100;
                PromoCharisma.Value = skillReq[CHARISMA][newLevel] / 100;
                PromoCreativity.Value = skillReq[CREATIVITY][newLevel] / 100;
                PromoLogic.Value = skillReq[LOGIC][newLevel] / 100;
                PromoCleaning.Value = skillReq[CLEANING][newLevel] / 100;
                cbTrick.IsEnabled = false;
            }
            else // pets
            {
                PromoCooking.IsEnabled = PromoMechanical.IsEnabled = PromoBody.IsEnabled = PromoCharisma.IsEnabled = PromoCreativity.IsEnabled = PromoLogic.IsEnabled = PromoCleaning.IsEnabled = false;
                cbTrick.SelectedIndex = 0;
                for (int i = 0; i * 2 < trick.Count; i++)
                    if (trick[i * 2 + 1] == newLevel)
                        cbTrick.SelectedIndex = trick[i * 2];
            }

            PromoFriends.Value = friends[newLevel];
            #endregion

            //chance cards
            chanceCardsLevelChanged(newLevel);

            currentLevel = newLevel;
            internalchg = levelChanging = false;
        }

        private byte currentLanguage;
        private List<String> languageString;
        private bool englishOnly;

        #endregion

        #region Job Details
        #region Job Titles
        private StrWrapper jobTitles;
        private void fillJobDetails()
        {
            JobDetailList.Items.Clear();
            for (int i = jobTitles[currentLanguage].Count; i < noLevels * 4 + 1; i++) jobTitles.Add(currentLanguage, "", "");
            List<StrItem> items = jobTitles[currentLanguage];
            for (ushort i = 0; i < noLevels; i++)
            {
                ListViewItem item1 = new ListViewItem("" + (i + 1), 0);
                item1.SubItems.Add(items[femaleOffset + (i * 2) + 1].Title);
                item1.SubItems.Add(items[femaleOffset + (i * 2) + 2].Title);
                item1.SubItems.Add(getOutfitTextFromGUIDAt(i + 1));
                if (isCastaway)
                    item1.SubItems.Add("");
                else
                    item1.SubItems.Add(getVehicleTextFromGUID(i + 1));
                JobDetailList.Items.Add(item1);
            }
        }
        #endregion

        #region Outfits Vehicles

        private Bcon outfit;
        private Bcon vehicle;

        private string getOutfitTextFromGUIDAt(int i)
        {
            if (isCastaway)
                return StringFromGUID(GUIDfromBCON(outfit, i), CSoutfitGUID, CSoutfitName);
            else
                return StringFromGUID(GUIDfromBCON(outfit, i), outfitGUID, outfitName);        
        }

        private void setOutfitFromGUID(int i)
        {
            gcOutfit.Value = GUIDfromBCON(outfit, i);
        }

        private string getVehicleTextFromGUID(int i)
        {
            return StringFromGUID(GUIDfromBCON(vehicle, i), vehicleGUID, vehicleName);
        }

        private void setVehicleFromGUID(int i)
        {
            gcVehicle.Value = GUIDfromBCON(vehicle, i);
        }

        private static UInt32[] outfitGUID = {
            0x526A1BC5, 0x0CC8FB0A, 0x6CF36A28, 0xACCFBA59,
            0x4CC8D355, 0x6CC1394A, 0x6CDB4D89, 0xACCFB5BA,
            0x6CE5E896, 0x7260F377, 0x5260F2CD, 0x2DC106EF,
            0xD2648300, 0xB2648347, 0x926A15C0, 0x325B8C8C,
            0x8CC8D510, 0xD26477D7, 0x8EBB1D6F, 0x6EBE0635,
            0x2ED4954E, 0xCEBA9C6C, 0x8EBE06F7, 0xEEBE33C6,
            0x6ED49951, 0xEEC0D438, 0xEEC0C883, 0xEEC0E1F1,
            0xAEC0D9E1, 0x52647796, 0xACCFB61B, 0xCCC8FA1E,
            0x926475B2, 0x526A0B4A, 0x0CCFB4F4, 0x4CF368BE,
            0x526481A6, 0x8CCFA3D8, 0xD264817E, 0xF25F7774,
            0x12648223, 0x92647699, 0x325F70DC, 0xB25F60C2,
            0x8CCFA130, 0x8CCFA223, 0xCCE5E90F, 0x52647657,
            0x8CCFA318, 0x2CD4EE5D, 0x8CE5EA26, 0xCCCF9FA7,
            0xECE5EB2F, 0xB25F6141, 0x8CCFA387, 0x925F75C9,
            0x925F7806, 0x72647706, 0x92647B25, 0x6CC13C27,
            0xACD4EEE6, 0x3260F41C, 0x7260F192, 0xACC8EE11,
            0x726483E4, 0x6CC8CFBD, 0x0DCC7C4D, 0x126A202D,
            0x525F71A7, 0xD260F3D9, 0xB2647B72, 0x7260F460,
            0xACCFB97C, 0xACE5EB8C, 0x0CCFA643, 0x52647818,
            0xECCFA694, 0xECCFB386, 0x125F7704, 
            0xEDED8493, 0x6DD1D04B,
            0xECA45D6D, 0x2CA45AE2, 0x0DB8AE38, 0x8CC13127,
            0x2CD89D6B, 0x2DC0FCD7, 0x8DC3893C, 0x0CA45C86,
            0xCDC38723, 0x8CDA36DA, 0xCD65FD9F, 0x6CC13409,
            0x6DC38A6C, 0xECD0F3FC, 0xCCD0F227, 0x6CC1322B,
            0xACD0F47C, 0x6CD0F537, 0x0CA45C32, 0x6CC130A6,
            
            0xECD7C130, 0xCE029001, 0x2E02903A, 0x6CD4EDE8,
            0xEE028B7C, 0x2E028C23, 0xAE028CB9, 0x6D771A13,
        };

        private static String[] outfitName = new String[] {
            "Adventurer", "Astronaut", "Blue Scrubs", "Burglar",
            "Captain Hero", "Cheap Suit", "Chef", "Clerk",
            "Coach", "Concert Pianist", "Conductor", "Cop",
            "Senior Professor", "Dean of Students", "Diver", "Dread Pirate",
            "EMT", "Entertainment Attorney", "EP1 Bartender", "EP1 Commercial Mascot",
            "EP1 Cultleader", "EP1 Graduation", "EP1 Natural Scientist", "EP1 Paranormal",
            "EP1 Photographer", "EP1 Post Graduation", "EP1 Secret Society", "EP1 TogaParty Casual",
            "EP1 TogaParty Outgoing", "Family Attorney", "Fast Food", "Fatigues",
            "File Clerk", "Gamer", "Gas Station", "Green Scrubs",
            "Guest Lecturer", "High-Rank", "Highschool", "Hostage",
            "Highschool Principal", "Injury Attorney", "Intern", "Journalist",
            "Lab Coat1", "Lab Coat2", "Leather Jacket", "Legal Secretary",
            "Low-Rank", "Mad-Lab", "Mascot", "Mastcot Evil",
            "Mayor", "Media", "Mid-Rank", "Multi-Regional",
            "Mystery", "Paralegal", "Playground Monitor", "Power-Suit",
            "Restaurant", "Roadie", "Rockstar", "Scrubs",
            "Secretary of Education", "Security Guard", "Slick-Suit", "Space Pirate",
            "Spelunker", "Studio Musician", "Substitute Teacher", "Summercamp",
            "Super Chef", "Swat", "Sweat-Suit", "The Law",
            "Tracksuit", "Tweed Jacket", "Warhead",
            "Electrocution", "Maternity",
            "NPC Ambulance Driver (Paramedic)", "NPC Bartender", "NPC Burglar", "NPC BusDriver",
            "NPC Clerk", "NPC Cop", "NPC DeliveryPerson", "NPC Exterminator",
            "NPC FireFighter", "NPC Gardener", "NPC HandyPerson", "NPC HeadMaster", 
            "NPC Maid", "NPC Mail Delivery", "NPC Nanny", "NPC Paper Delivery",
            "NPC Pizza Delivery", "NPC Repo", "NPC Salesperson", "NPC SocialWorker",
            "PrivateSchool", "Reaper Lei", "Reaper NoLei", "SkeletonGlow",
            "SocialBunny Blue", "SocialBunny Pink", "SocialBunny Yellow", "Wedding Outfit",
        };

        private static UInt32[] vehicleGUID = new UInt32[] {
			0xAD0AB49C, 0x0D099B93, 0xAC43E058, 0x4CA1487C,
            0x6C6CDEC6, 0xCC69FDA3, 0xEC860630, 0x0CA42373,
            0x8C5A4D9E, 0x4D50E553, 0x4C03451A, 0x6CA4110A,
            0x4C413B80, 0x0C85AE14, 0xCD08624E, 0x4D09B954,
        };
        private static String[] vehicleName = new String[] {
			"Captain Hero Flyaway", "Helicopter - Executive", "Helicopter - Army", "Town Car",
			"Sports Car - Super", "Sports Car - Mid", "Sports Car - Low", "Sports Team Bus",
			"Sedan", "Taxi", "Minivan", "Limo", 
            "HMV", "Hatchback", "Police", "Ambulance",
		};

        // End Shit - Start good

        private static UInt32[] TAoutfitGUID = {
            0x526A1BC5, 0x0CC8FB0A, 0x6CF36A28, 0xACCFBA59,
            0x4CC8D355, 0x6CC1394A, 0x6CDB4D89, 0xACCFB5BA,
            0x6CE5E896, 0x7260F377, 0x5260F2CD, 0x2DC106EF,
            0xD2648300, 0xB2648347, 0x926A15C0, 0x325B8C8C,
            0x8CC8D510, 0xD26477D7, 0x8EBB1D6F, 0x6EBE0635,
            0x2ED4954E, 0xCEBA9C6C, 0x8EBE06F7, 0xEEBE33C6,
            0x6ED49951, 0xEEC0D438, 0xEEC0C883, 0xEEC0E1F1,
            0xAEC0D9E1, 0x52647796, 0xACCFB61B, 0xCCC8FA1E,
            0x926475B2, 0x526A0B4A, 0x0CCFB4F4, 0x4CF368BE,
            0x526481A6, 0x8CCFA3D8, 0xD264817E, 0xF25F7774,
            0x12648223, 0x92647699, 0x325F70DC, 0xB25F60C2,
            0x8CCFA130, 0x8CCFA223, 0xCCE5E90F, 0x52647657,
            0x8CCFA318, 0x2CD4EE5D, 0x8CE5EA26, 0xCCCF9FA7,
            0xECE5EB2F, 0xB25F6141, 0x8CCFA387, 0x925F75C9,
            0x925F7806, 0x72647706, 0x92647B25, 0x6CC13C27,
            0xACD4EEE6, 0x3260F41C, 0x7260F192, 0xACC8EE11,
            0x726483E4, 0x6CC8CFBD, 0x0DCC7C4D, 0x126A202D,
            0x525F71A7, 0xD260F3D9, 0xB2647B72, 0x7260F460,
            0xACCFB97C, 0xACE5EB8C, 0x0CCFA643, 0x52647818,
            0xECCFA694, 0xECCFB386, 0x125F7704, 0xEDED8493,
            0x6DD1D04B, 0xECA45D6D, 0x2CA45AE2, 0x0DB8AE38,
            0x8CC13127, 0x2CD89D6B, 0x2DC0FCD7, 0x8DC3893C,
            0x0CA45C86, 0xCDC38723, 0x8CDA36DA, 0xCD65FD9F,
            0x6CC13409, 0x6DC38A6C, 0xECD0F3FC, 0xCCD0F227,
            0x6CC1322B, 0xACD0F47C, 0x6CD0F537, 0x0CA45C32,
            0x6CC130A6, 0xECD7C130, 0xCE029001, 0x2E02903A,
            0x6CD4EDE8, 0xEE028B7C, 0x2E028C23, 0xAE028CB9,
            0x6D771A13, 0x00845D77, 0x8F73B607, 0xF46C3CDD,
            0x008BB233,
        };

        private static String[] TAoutfitName = new String[] {
            "Adventurer", "Astronaut", "Blue Scrubs", "Burglar",
            "Captain Hero", "Cheap Suit", "Chef", "Clerk",
            "Coach", "Concert Pianist", "Conductor", "Cop",
            "Senior Professor", "Dean of Students", "Diver", "Dread Pirate",
            "EMT", "Entertainment Attorney", "EP1 Bartender", "EP1 Commercial Mascot",
            "EP1 Cultleader", "EP1 Graduation", "EP1 Natural Scientist", "EP1 Paranormal",
            "EP1 Photographer", "EP1 Post Graduation", "EP1 Secret Society", "EP1 TogaParty Casual",
            "EP1 TogaParty Outgoing", "Family Attorney", "Fast Food", "Fatigues",
            "File Clerk", "Gamer", "Gas Station", "Green Scrubs",
            "Guest Lecturer", "High-Rank", "Highschool", "Hostage",
            "Highschool Principal", "Injury Attorney", "Intern", "Journalist",
            "Lab Coat1", "Lab Coat2", "Leather Jacket", "Legal Secretary",
            "Low-Rank", "Mad-Lab", "Mascot", "Mastcot Evil",
            "Mayor", "Media", "Mid-Rank", "Multi-Regional",
            "Mystery", "Paralegal", "Playground Monitor", "Power-Suit",
            "Restaurant", "Roadie", "Rockstar", "Scrubs",
            "Secretary of Education", "Security Guard", "Slick-Suit", "Space Pirate",
            "Spelunker", "Studio Musician", "Substitute Teacher", "Summercamp",
            "Super Chef", "Swat", "Sweat-Suit", "The Law",
            "Tracksuit", "Tweed Jacket", "Warhead",
            "Electrocution", "Maternity",
            "NPC Ambulance Driver (Paramedic)", "NPC Bartender", "NPC Burglar", "NPC BusDriver",
            "NPC Clerk", "NPC Cop", "NPC DeliveryPerson", "NPC Exterminator",
            "NPC FireFighter", "NPC Gardener", "NPC HandyPerson", "NPC HeadMaster", 
            "NPC Maid", "NPC Mail Delivery", "NPC Nanny", "NPC Paper Delivery",
            "NPC Pizza Delivery", "NPC Repo", "NPC Salesperson", "NPC SocialWorker",
            "Private School Uniform", "Reaper Lei", "Reaper NoLei", "Skeleton Glow",
            "SocialBunny Blue", "SocialBunny Pink", "SocialBunny Yellow", "Wedding Outfit",            
            "High Society Escort", "EP2 Date Diva", "EP7 Diva", "St.Trinians Uniform",
        };

        private static UInt32[] TAvehicleGUID = new UInt32[] {
			0xAD0AB49C, 0x0D099B93, 0xAC43E058, 0x4CA1487C,
            0x6C6CDEC6, 0xCC69FDA3, 0xEC860630, 0x0CA42373,
            0x8C5A4D9E, 0x4D50E553, 0x4C03451A, 0x6CA4110A,
            0x4C413B80, 0x0C85AE14, 0xCD08624E, 0x4D09B954,
            0x00845D0F, 0x00845D41,
        };
        private static String[] TAvehicleName = new String[] {
			"Captain Hero Flyaway", "Helicopter - Executive", "Helicopter - Army", "Town Car",
			"Sports Car - Super", "Sports Car - Mid", "Sports Car - Low", "Sports Team Bus",
			"Sedan", "Taxi", "Minivan", "Limo", 
            "HMV", "Hatchback", "Police", "Ambulance",
            "Princess Van", "White Limo",
		};

        private static UInt32[] CSoutfitGUID = { 0xB431D8A0, 0x9431D91A, 0x7431D945, };
        private static String[] CSoutfitName = new String[] { "Career Crafter", "Career Gatherer", "Career Hunter", };

        #endregion

        private uint GUIDfromBCON(Bcon bcon, int i)
        {
            return (uint)(((ushort)bcon[i * 2 + 1] << 16) | ((ushort)bcon[i * 2]));
        }
        private string StringFromGUID(uint guid, UInt32[] guids, String[] strings)
        {
            if (guid == 0) return "";
            int index = (new List<uint>(guids)).IndexOf(guid);
            return index >= 0 ? strings[index] : "Other";
        }
        private void insertGuid(Bcon bcon, int index, uint guid)
        {
            List<short> bconItem = new List<short>();
            foreach (short i in bcon) bconItem.Add(i);
            bconItem.Insert((index + 1) * 2, (short)(guid & 0xffff));
            bconItem.Insert((index + 1) * 2 + 1, (short)(guid >> 16 & 0xffff));
            bcon.Clear();
            foreach (short i in bconItem) bcon.Add(i);
        }
        #endregion


        #region Hours & Wages
        private Bcon startHour;
        private Bcon hoursWorked;
        private Bcon daysWorked;
        private Bcon wages;
        private Bcon wagesCat;
        private Bcon wagesDog;
        private Bcon foodinc;
        private Bcon resinc;
        private Bcon[] motiveDeltas;

        private void fillHoursAndWages()
        {
            HoursWagesList.Items.Clear();
            for (ushort i = 1; i <= noLevels; i++)
            {
                ListViewItem item1 = new ListViewItem("" + i, 0);

                item1.SubItems.Add("" + startHour[i]);
                item1.SubItems.Add("" + hoursWorked[i]);
                item1.SubItems.Add("" + (startHour[i] + hoursWorked[i]) % 24);
                if (isCastaway)
                {
                    item1.SubItems.Add("" + wages[i]);
                    item1.SubItems.Add("" + foodinc[i]);
                    item1.SubItems.Add("");
                }
                else if (!isPetCareer)
                {
                    item1.SubItems.Add("" + wages[i]);
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                }
                else
                {
                    item1.SubItems.Add("");
                    item1.SubItems.Add("" + wagesDog[i]);
                    item1.SubItems.Add("" + wagesCat[i]);
                }

                Boolset dw = getDaysArray(daysWorked[i]);
                item1.SubItems.Add("" + dw[MONDAY]);
                item1.SubItems.Add("" + dw[TUESDAY]);
                item1.SubItems.Add("" + dw[WEDNESDAY]);
                item1.SubItems.Add("" + dw[THURSDAY]);
                item1.SubItems.Add("" + dw[FRIDAY]);
                item1.SubItems.Add("" + dw[SATURDAY]);
                item1.SubItems.Add("" + dw[SUNDAY]);

                HoursWagesList.Items.Add(item1);
            }
        }

        private Boolset getDaysArray(short val)
        {
            return new Boolset((byte)((val >= 0) ? val : val + 65536));
        }
        private void WorkChanged(int level)
        {
            HungerHours.Value = hoursWorked[level];
            AmorousHours.Value = hoursWorked[level];
            ComfortHours.Value = hoursWorked[level];
            HygieneHours.Value = hoursWorked[level];
            BladderHours.Value = hoursWorked[level];
            EnergyHours.Value = hoursWorked[level];
            FunHours.Value = hoursWorked[level];
            PublicHours.Value = hoursWorked[level];
            SunshineHours.Value = hoursWorked[level];

            HungerTotal.Value = motiveDeltas[HUNGER][level] * hoursWorked[level];
            AmorousTotal.Value = motiveDeltas[AMOROUS][level] * hoursWorked[level];
            ComfortTotal.Value = motiveDeltas[COMFORT][level] * hoursWorked[level];
            HygieneTotal.Value = motiveDeltas[HYGIENE][level] * hoursWorked[level];
            BladderTotal.Value = motiveDeltas[BLADDER][level] * hoursWorked[level];
            EnergyTotal.Value = motiveDeltas[ENERGY][level] * hoursWorked[level];
            FunTotal.Value = motiveDeltas[FUN][level] * hoursWorked[level];
            PublicTotal.Value = motiveDeltas[PUBLIC][level] * hoursWorked[level];
            SunshineTotal.Value = motiveDeltas[SUNSHINE][level] * hoursWorked[level];
        }
        #endregion


        #region Promotion
        private Bcon[] skillReq;
        private Bcon friends;
        private Bcon trick;

        private void fillPromotionDetails()
        {
            PromoList.Items.Clear();
            for (ushort i = 1; i <= noLevels; i++)
            {
                ListViewItem item1 = new ListViewItem("" + i, 0);

                if (!isPetCareer) // people
                {
                    item1.SubItems.Add("" + skillReq[COOKING][i] / 100);
                    item1.SubItems.Add("" + skillReq[MECHANICAL][i] / 100);
                    item1.SubItems.Add("" + skillReq[BODY][i] / 100);
                    item1.SubItems.Add("" + skillReq[CHARISMA][i] / 100);
                    item1.SubItems.Add("" + skillReq[CREATIVITY][i] / 100);
                    item1.SubItems.Add("" + skillReq[LOGIC][i] / 100);
                    item1.SubItems.Add("" + skillReq[CLEANING][i] / 100);
                    item1.SubItems.Add("" + friends[i]);
                    item1.SubItems.Add("");
                }
                else // pets
                {
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("");
                    item1.SubItems.Add("" + friends[i]);
                    String tr = (String)cbTrick.Items[0];
                    for (int j = 0; j * 2 < trick.Count; j++)
                        if (trick[j * 2 + 1] == i)
                            tr = (String)cbTrick.Items[trick[j * 2]];
                    item1.SubItems.Add(tr);
                }

                PromoList.Items.Add(item1);
            }
        }
        #endregion


        #region Chance Cards
        private StrWrapper chanceCardsText;
        private Bcon[] chanceASkills;
        private Bcon[] chanceAGood;
        private Bcon[] chanceABad;

        private Bcon[] chanceBSkills;
        private Bcon[] chanceBGood;
        private Bcon[] chanceBBad;

        private Bcon chanceChance;
        private Bcon chanceAchance;
        private Bcon chanceBchance;

        private void chanceCardsLevelChanged(ushort newLevel)
        {
            if (currentLevel != 0 && currentLevel <= noLevels) chanceCardsToFiles();

            lnudChanceCurrentLevel.Value = newLevel;
            if (isPetCareer)
            {
                lnudPetChancePercent.Value = chanceAchance[newLevel];
                lnudPetChancePercent.IsVisible = true;
                lnudChancePercent.Value = chanceBchance[newLevel];
            }
            else
                lnudChancePercent.Value = chanceChance[newLevel];

            cpChoiceA.HaveSkills = cpChoiceB.HaveSkills = (chanceChance[newLevel] < 0 && !isPetCareer);

            if (preuniv)
                cbischance.IsVisible = false;
            else 
                cbischance.IsChecked = cclevels[newLevel] > 0;

            for (int i = chanceCardsText[currentLanguage].Count; i < newLevel * 12 + 19; i++)
                chanceCardsText.Add(currentLanguage, "", "");
            List<StrItem> items = chanceCardsText[currentLanguage];

            cpChoiceA.setValues(true, cpChoiceA.Label, items[newLevel * 12 + 7].Title, chanceASkills, newLevel);
            cpChoiceB.setValues(false, cpChoiceB.Label, items[newLevel * 12 + 8].Title, chanceBSkills, newLevel);

            ChanceTextMale.Text = items[newLevel * 12 + 9].Title;
            ChanceTextFemale.Text = items[newLevel * 12 + 10].Title;

            epPassA.setValues(noLevels, newLevel, chanceAGood, items[newLevel * 12 + 11].Title, items[newLevel * 12 + 12].Title);
            epFailA.setValues(noLevels, newLevel, chanceABad, items[newLevel * 12 + 13].Title, items[newLevel * 12 + 14].Title);
            epPassB.setValues(noLevels, newLevel, chanceBGood, items[newLevel * 12 + 15].Title, items[newLevel * 12 + 16].Title);
            epFailB.setValues(noLevels, newLevel, chanceBBad, items[newLevel * 12 + 17].Title, items[newLevel * 12 + 18].Title);
        }
        private void chanceCardsToFiles()
        {
            List<StrItem> items = chanceCardsText[currentLanguage];
            if (isPetCareer)
            {
                chanceAchance[currentLevel] = (short)lnudPetChancePercent.Value;
                chanceBchance[currentLevel] = (short)lnudChancePercent.Value;
            }
            else
                chanceChance[currentLevel] = (short)lnudChancePercent.Value;

            items[currentLevel * 12 + 7].Title = cpChoiceA.Value;
            if (!isPetCareer)
                cpChoiceA.getValues(chanceASkills, currentLevel);

            items[currentLevel * 12 + 8].Title = cpChoiceB.Value;
            if (!isPetCareer)
                cpChoiceB.getValues(chanceBSkills, currentLevel);

            items[currentLevel * 12 + 9].Title = ChanceTextMale.Text;
            items[currentLevel * 12 + 10].Title = ChanceTextFemale.Text;

            string male = "", female = "";
            epPassA.getValues(chanceAGood, currentLevel, ref male, ref female);
            items[currentLevel * 12 + 11].Title = male;
            items[currentLevel * 12 + 12].Title = female;

            epFailA.getValues(chanceABad, currentLevel, ref male, ref female);
            items[currentLevel * 12 + 13].Title = male;
            items[currentLevel * 12 + 14].Title = female;

            epPassB.getValues(chanceBGood, currentLevel, ref male, ref female);
            items[currentLevel * 12 + 15].Title = male;
            items[currentLevel * 12 + 16].Title = female;

            epFailB.getValues(chanceBBad, currentLevel, ref male, ref female);
            items[currentLevel * 12 + 17].Title = male;
            items[currentLevel * 12 + 18].Title = female;
        }
        #endregion


        #region program constants
        private const byte COOKING = 0;
        private const byte MECHANICAL = 1;
        private const byte BODY = 2;
        private const byte CHARISMA = 3;
        private const byte CREATIVITY = 4;
        private const byte LOGIC = 5;
        private const byte CLEANING = 6;

        private const byte MONEY = 7;
        private const byte JOB = 8;
        private const byte FOOD = 9;

        private const byte HUNGER = 0;
        private const byte AMOROUS = 1;
        private const byte COMFORT = 2;
        private const byte HYGIENE = 3;
        private const byte BLADDER = 4;
        private const byte ENERGY = 5;
        private const byte FUN = 6;
        private const byte PUBLIC = 7;
        private const byte SUNSHINE = 8;

        private const byte MONDAY = 0;
        private const byte TUESDAY = 1;
        private const byte WEDNESDAY = 2;
        private const byte THURSDAY = 3;
        private const byte FRIDAY = 4;
        private const byte SATURDAY = 5;
        private const byte SUNDAY = 6;
        #endregion


        private bool internalchg = false;
		public Interfaces.Plugin.IToolResult Execute(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package, Interfaces.IProviderRegistry prov) 
		{
			bool newpackage = false;

			this.package = (SimPe.Packages.GeneratableFile)package;
			
			WaitingScreen.Wait();
			try 
			{
                if (this.package == null || this.package.FileName == null)
				{
                    System.IO.Stream s = typeof(CareerEditor).Assembly.GetManifestResourceStream(CareerTool.DefaultCareerFile);
                    System.IO.BinaryReader br = new BinaryReader(s);
                    this.package = SimPe.Packages.GeneratableFile.LoadFromStream(br);
                    newpackage = true;
                    this.package = (SimPe.Packages.GeneratableFile)this.package.Clone();
				}
                if (!pjse.GUIDIndex.TheGUIDIndex.IsLoaded) pjse.GUIDIndex.TheGUIDIndex.Load(pjse.GUIDIndex.DefaultGUIDFile);

                loadFiles();
                if (isCastaway)
                {
                    gcReward.KnownObjects = new object[] { new List<String>(CSrewardName), new List<UInt32>(CSrewardGUID) };
                    gcUpgrade.KnownObjects = new object[] { new List<String>(CSupgradeName), new List<UInt32>(CSupgradeGUID) };
                    gcOutfit.KnownObjects = new object[] { new List<String>(CSoutfitName), new List<UInt32>(CSoutfitGUID) };
                    this.tabControl1.Items.Remove(this.tabPage9);
                    this.tabControl1.Items.Remove(this.tabPagMajor);
                    this.gcVehicle.IsVisible = false;
                    if (isTeenCareer)
                    {
                        if (tuning[0] == 4) this.Title = "Career Editor (by Bidou) - Castaway Orangutan Career";
                        else this.Title = "Career Editor (by Bidou) - Castaway Teen, Elder Career";
                    }
                    else this.Title = "Career Editor (by Bidou) - Castaway Stories Adult Career";
                    this.lnudWages.Label = "Resource";
                    // this.lnudWages.Location = new System.Drawing.Point(39, 71); // no .Location in Avalonia
                    this.lnudFoods.IsVisible = true;
                    this.HwWages.Text = "Resource";
                    this.HwDogWages.Text = "Food";
                }
                else
                {
                    // Chris Hatch "extras" (T&A panels, overrides) removed.
                    // Always hide the extras tab in the clean version.
                    this.tabControl1.Items.Remove(this.tabPage9);

                    // Keep the original majors logic.
                    if (preuniv || isPetCareer || isTeenCareer)
                        this.tabControl1.Items.Remove(this.tabPagMajor);
                    else
                        setmajors();

                    // Set the window title based on career type (unchanged).
                    if (isPetCareer)
                        this.Title = "Career Editor (by Bidou) - Pet Career";
                    else if (isTeenCareer)
                        this.Title = "Career Editor (by Bidou) - Teen, Elder Career";
                    else
                        this.Title = "Career Editor (by Bidou) - Adult Career";
                }

                SimPe.Interfaces.Files.IPackedFileDescriptor bfd = this.package.FindFile(0x856DDBAC, 0, groupId, 1);
                if (bfd != null)
                {
                    SimPe.PackedFiles.Wrapper.Picture pic = new SimPe.PackedFiles.Wrapper.Picture();
                    pic.ProcessData(bfd, this.package);
                    pictureBox1.Image = pic.Image;
                }
                else pictureBox1.Image = null;

                //menuItem6.IsEnabled = !isPetCareer;
                WorkAmorous.IsEnabled = !isPetCareer;
				miEnglishOnly.IsChecked = englishOnly;

                if (catalogueDesc.Languages.Length <= 1) currentLanguage = 1;
                else currentLanguage = (byte)Helper.XmlRegistry.LanguageCode; // CJH

                internalchg = true;

                setRewardFromGUID();
                setUpgradeFromGUID();

                Language.ItemsSource = languageString;
                int langIdx = currentLanguage - 1;
                if (langIdx >= 0 && langIdx < languageString.Count)
                    Language.SelectedIndex = langIdx;

                CareerTitle.Text = (((List<StrItem>)catalogueDesc[currentLanguage]).Count == 0) ? "" : catalogueDesc[currentLanguage, 0].Title;

                // englishOnly = (catalogueDesc.Languages.Length <= 1);
                englishOnly = false;
                miEnglishOnly.IsChecked = englishOnly;
                Language.IsEnabled = !englishOnly;
                
                internalchg = false;
                noLevelsChanged((ushort)tuning[0]);
			}
			catch (Exception e)
			{
				Helper.ExceptionMessage("Error Loading Career", e);
				return new Plugin.ToolResult(false, false);
			} 
			finally 
			{
                internalchg = false; // Should already be done.
				WaitingScreen.Stop(this);
			}

			ShowDialog(null).GetAwaiter().GetResult();

            getGUIDFromReward();
            getGUIDFromUpgrade();
            chanceCardsToFiles();

			if (englishOnly)
            {
                jobTitles.DefaultOnly();
                chanceCardsText.DefaultOnly();
                catalogueDesc.DefaultOnly();
            }

			saveFiles();

			if (newpackage) package = this.package;
			return new Plugin.ToolResult(true, newpackage);
		}


        private Bcon getBcon(uint instance)
		{
			Interfaces.Files.IPackedFileDescriptor pfd = package.FindFile(0x42434F4E, 0, groupId, instance);
            if (pfd == null) return null;

			Bcon bcon = new Bcon();
			bcon.ProcessData(pfd, package);
			return bcon;
		}
        private bool makeBcon(uint instance, int lvls, string flname)
        {
            package.Add(package.NewDescriptor(0x42434F4E, 0, groupId, instance));
            Interfaces.Files.IPackedFileDescriptor pfd = package.FindFile(0x42434F4E, 0, groupId, instance);
            if (pfd == null) return false;
            lvls++;
            Bcon bcon = new Bcon();
            bcon.ProcessData(pfd, package);
            bcon.FileName = flname;
            for (int i = 0; i < lvls; i++)
                bcon.Add(0);
            bcon.SynchronizeUserData();
            return true;
        }
        private void insertBcon(Bcon bcon, int index, short value)
        {
            List<short> bconItem = new List<short>();
            foreach (short i in bcon) bconItem.Add(i);
            bconItem.Insert(index, value);
            bcon.Clear();
            foreach (short i in bconItem) bcon.Add(i);
        }

        private SimPe.PackedFiles.Wrapper.StrWrapper getCtss() { return getStr(package.FindFiles(Data.MetaData.CTSS_FILE)[0]); }
        private SimPe.PackedFiles.Wrapper.StrWrapper getStr(uint instance) { return getStr(package.FindFile(0x53545223, 0, groupId, instance)); }
        private SimPe.PackedFiles.Wrapper.StrWrapper getStr(Interfaces.Files.IPackedFileDescriptor pfd)
		{
            if (pfd == null) return null;

            SimPe.PackedFiles.Wrapper.StrWrapper str = new SimPe.PackedFiles.Wrapper.StrWrapper();
			str.ProcessData(pfd, package);

            // This makes sure US English is as long the longest str[lng] array
            int count = 0;
            for (byte i = 1; i <= languageString.Count; i++) count = Math.Max(count, str[i].Count);
            while (count > 0 && str[1, count - 1] == null) str.Add(1, "", "");

            return str;
		}

        private void loadFiles()
        {
            catalogueDesc = getCtss();
            groupId = catalogueDesc.FileDescriptor.Group;

            lifeScore = getBcon(0x100D); // not pets
            PTO = getBcon(0x1054);
            goodRew = getBcon(0x105A);
            badRew = getBcon(0x105B);
            majors = getBcon(0x1056);
            cclevels = getBcon(0x1057);

            Bhav bhav;

            foreach (Interfaces.Files.IPackedFileDescriptor p in package.FindFiles(0x42484156))
            {
                if (p.MarkForDelete || p.Invalid || p.Group != groupId) continue;
                bhav = new Bhav();
                bhav.ProcessData(p, package);

                #region Find Career Reward
                if (bhav.FileName.Equals("CT - Career Reward")) // Must be adult career
                {
                    isTeenCareer = false;
                    foreach (Instruction ins in bhav)
                        if (ins.OpCode == 0x0033) // Manage Inventory
                        {
                            AddRewardToInventory = ins;
                            break;
                        }
                    continue;
                }
                #endregion

                #region Find Job Ugrade
                if (bhav.FileName.Equals("CT - Upgrade Job to Adult")) // Must be Teen - Elder career
                {
                    isTeenCareer = true;
                    foreach (Instruction ins in bhav)
                        if (ins.OpCode == 0x001f || // Set To Next
                            ins.OpCode == 0x0020) // Test Object Of Type (Owned Business uses this)
                        {
                            JobUpgrade = ins;
                            break;
                        }
                    continue;
                }
                #endregion

                #region Check if Pet career
                if (bhav.FileName.Equals("CT - Command Needed?"))
                {
                    isTeenCareer = false;
                    isPetCareer = true;
                    continue;
                }
                #endregion
            }

            tuning = getBcon(0x1019);

            // Job Details
            jobTitles = getStr(0x0093);
            outfit = getBcon(0x1055);
            vehicle = getBcon(0x100C);

            // Promotion
            if (!isPetCareer)
            {
                skillReq = new Bcon[7];
                skillReq[COOKING] = getBcon(0x1004);
                skillReq[MECHANICAL] = getBcon(0x1005);
                skillReq[BODY] = getBcon(0x1006);
                skillReq[CHARISMA] = getBcon(0x1007);
                skillReq[CREATIVITY] = getBcon(0x1008);
                skillReq[LOGIC] = getBcon(0x1009);
                skillReq[CLEANING] = getBcon(0x100B);
                trick = null;
            }
            else
            {
                trick = getBcon(0x1004);
            }
            friends = getBcon(0x1003);

            // Hours & Wages
            startHour = getBcon(0x1001);
            hoursWorked = getBcon(0x1002);
            if (!isPetCareer)
            {
                wages = getBcon(0x1000);
                wagesDog = wagesCat = null;
            }
            else
            {
                wagesDog = getBcon(0x1000);
                wagesCat = getBcon(0x1005);
                wages = null;
            }
            foodinc = getBcon(0x105E);
            resinc = getBcon(0x105F);

            daysWorked = getBcon(0x101A);

            motiveDeltas = new Bcon[9];
            motiveDeltas[HUNGER] = getBcon(0x100E);
            motiveDeltas[AMOROUS] = getBcon(0x1016);
            motiveDeltas[COMFORT] = getBcon(0x1010);
            motiveDeltas[HYGIENE] = getBcon(0x1011);
            motiveDeltas[BLADDER] = getBcon(0x1012);
            motiveDeltas[ENERGY] = getBcon(0x1013);
            motiveDeltas[FUN] = getBcon(0x1014);
            motiveDeltas[PUBLIC] = getBcon(0x1015);
            if (getBcon(0x105F) != null)
            {
                isCastaway = true;
                motiveDeltas[SUNSHINE] = getBcon(0x1059);
                menuItem6.IsEnabled = false;                
                // this.lbcrap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold); // no .Font on Avalonia controls
                // this.lbcrap.ForeColor = System.Drawing.Color.DarkMagenta; // no .ForeColor on Avalonia controls
                this.lbcrap.Text = "Castaway Stories Career";
                this.lbcrap.IsVisible = true;
            }
            else
            {
                if (getBcon(0x1059) == null)
                {
                    motiveDeltas[SUNSHINE] = getBcon(0x100F); // CJH
                    this.WorkSunshine.IsEnabled = false;
                    this.lbcrap.IsVisible = this.btUgrade.IsVisible = !isPetCareer;
                }
                else
                {
                    motiveDeltas[SUNSHINE] = getBcon(0x1059);
                    this.WorkSunshine.IsEnabled = true;
                    if (getBcon(0x105B) == null) this.lbcrap.IsVisible = false;
                    else
                    {
                        if (getBcon(0x105B).Count > 21)
                        {
                            // this.lbcrap.ForeColor = System.Drawing.Color.RoyalBlue; // no .ForeColor on Avalonia controls
                            this.lbcrap.Text = "New Career Type!\r\nSupports the extra Reward Feature";
                            this.lbcrap.IsVisible = true;
                            rewenabled = true;
                        }
                    }
                }
                if (getBcon(0x1056) == null)
                {
                    this.btUgrade.IsVisible = true;
                    this.lbcrap.Text = "Really Old Career Type!\r\nIncompatible with University EP or above";
                    preuniv = true;
                }
            }

            // Chance cards
            chanceCardsText = getStr(0x012D);
            chanceChance = getBcon(0x101B); // Seasons - not % chance of happening but % chance true is good answer. if -1 then use skill instead
            // Seasons uses current season to determine % chance of happening, set by semiglobal constant not in career
            if (!isPetCareer)
            {
                chanceASkills = new Bcon[7]; // not pets
                chanceASkills[COOKING] = getBcon(0x101C);
                chanceASkills[MECHANICAL] = getBcon(0x101D);
                chanceASkills[BODY] = getBcon(0x101E);
                chanceASkills[CHARISMA] = getBcon(0x101F);
                chanceASkills[CREATIVITY] = getBcon(0x1020);
                chanceASkills[LOGIC] = getBcon(0x1021);
                chanceASkills[CLEANING] = getBcon(0x1023);

                chanceBSkills = new Bcon[7]; // not pets
                chanceBSkills[COOKING] = getBcon(0x1024);
                chanceBSkills[MECHANICAL] = getBcon(0x1025);
                chanceBSkills[BODY] = getBcon(0x1026);
                chanceBSkills[CHARISMA] = getBcon(0x1027);
                chanceBSkills[CREATIVITY] = getBcon(0x1028);
                chanceBSkills[LOGIC] = getBcon(0x1029);
                chanceBSkills[CLEANING] = getBcon(0x102B);
            }
            else // not people
            {
                chanceAchance = getBcon(0x103A);
                chanceBchance = getBcon(0x104E);
            }

            chanceAGood = new Bcon[10];
            chanceAGood[MONEY] = getBcon(0x102C);
            chanceAGood[JOB] = getBcon(0x102D);
            chanceABad = new Bcon[10]; // not pets
            chanceABad[MONEY] = getBcon(0x1036);
            chanceABad[JOB] = getBcon(0x1037);
            chanceBGood = new Bcon[10];
            chanceBGood[MONEY] = getBcon(0x1040);
            chanceBGood[JOB] = getBcon(0x1041);
            chanceBBad = new Bcon[10];
            chanceBBad[MONEY] = getBcon(0x104A);
            chanceBBad[JOB] = getBcon(0x104B);
            if (!isPetCareer) // not pets
            {
                chanceAGood[COOKING] = getBcon(0x102E);
                chanceAGood[MECHANICAL] = getBcon(0x102F);
                chanceAGood[BODY] = getBcon(0x1030);
                chanceAGood[CHARISMA] = getBcon(0x1031);
                chanceAGood[CREATIVITY] = getBcon(0x1032);
                chanceAGood[LOGIC] = getBcon(0x1033);
                chanceAGood[CLEANING] = getBcon(0x1035);
                chanceABad[COOKING] = getBcon(0x1038);
                chanceABad[MECHANICAL] = getBcon(0x1039);
                chanceABad[BODY] = getBcon(0x103A);
                chanceABad[CHARISMA] = getBcon(0x103B);
                chanceABad[CREATIVITY] = getBcon(0x103C);
                chanceABad[LOGIC] = getBcon(0x103D);
                chanceABad[CLEANING] = getBcon(0x103F);
                chanceBGood[COOKING] = getBcon(0x1042);
                chanceBGood[MECHANICAL] = getBcon(0x1043);
                chanceBGood[BODY] = getBcon(0x1044);
                chanceBGood[CHARISMA] = getBcon(0x1045);
                chanceBGood[CREATIVITY] = getBcon(0x1046);
                chanceBGood[LOGIC] = getBcon(0x1047);
                chanceBGood[CLEANING] = getBcon(0x1049);
                chanceBBad[COOKING] = getBcon(0x104C);
                chanceBBad[MECHANICAL] = getBcon(0x104D);
                chanceBBad[BODY] = getBcon(0x104E);
                chanceBBad[CHARISMA] = getBcon(0x104F);
                chanceBBad[CREATIVITY] = getBcon(0x1050);
                chanceBBad[LOGIC] = getBcon(0x1051);
                chanceBBad[CLEANING] = getBcon(0x1053);
            }
            if (isCastaway)
            {
                chanceAGood[FOOD] = getBcon(0x105A);
                chanceABad[FOOD] = getBcon(0x105B);
                chanceBGood[FOOD] = getBcon(0x105C);
                chanceBBad[FOOD] = getBcon(0x105D);
            }
        }

		private void saveFiles()
		{
            //if (isCastaway) return;
			if (catalogueDesc.Changed) catalogueDesc.SynchronizeUserData();

            if (lifeScore != null && lifeScore.Changed)
                lifeScore.SynchronizeUserData();
            if (PTO.Changed) PTO.SynchronizeUserData();

            if (AddRewardToInventory != null && AddRewardToInventory.Parent.Changed)
                AddRewardToInventory.Parent.SynchronizeUserData();

            if (JobUpgrade != null && JobUpgrade.Parent.Changed)
                JobUpgrade.Parent.SynchronizeUserData();

            if (tuning.Changed) tuning.SynchronizeUserData();

            // Job Details
            if (jobTitles.Changed) jobTitles.SynchronizeUserData();
            if (vehicle.Changed) vehicle.SynchronizeUserData();
			if (outfit.Changed) outfit.SynchronizeUserData();

            // Hours & Wages
            if (startHour.Changed) startHour.SynchronizeUserData();
			if (hoursWorked.Changed) hoursWorked.SynchronizeUserData();
            if (!isPetCareer)
            {
                if (wages.Changed) wages.SynchronizeUserData();
            }
            else
            {
                if (wagesDog.Changed) wagesDog.SynchronizeUserData();
                if (wagesCat.Changed) wagesCat.SynchronizeUserData();
            }
            if (isCastaway)
            {
                if (foodinc.Changed) foodinc.SynchronizeUserData();
                if (resinc.Changed) resinc.SynchronizeUserData();
            }
			if (daysWorked.Changed) daysWorked.SynchronizeUserData();

            for (int i = 0; i < 9; i++)
                if (motiveDeltas[i].Changed) motiveDeltas[i].SynchronizeUserData();

            // Promotion
            if (!isPetCareer)
            {
                foreach (Bcon b in skillReq)
                    if (b.Changed) b.SynchronizeUserData();
            }
            else
            {
                if (trick.Changed)
                {
                    trick.Clear();
                    for (short i = 0; i < noLevels; i++)
                    {
                        ListViewItem item = PromoList.Items[i];
                        short tr = (short)cbTrick.Items.IndexOf(item.SubItems[9].Text);
                        if (tr > 0)
                        {
                            trick.Add(tr);
                            trick.Add((short)(i + 1));
                        }
                    }
                    trick.SynchronizeUserData();
                }
            }
			if (friends.Changed) friends.SynchronizeUserData();
            if (!preuniv)
            {
                if (majors.Changed) majors.SynchronizeUserData();
                if (cclevels.Changed) cclevels.SynchronizeUserData();
            }

            // Chance Cards
            if (chanceCardsText.Changed) chanceCardsText.SynchronizeUserData();
			if (chanceChance.Changed) chanceChance.SynchronizeUserData();
            for (int i = 7; i < 9; i++)
            {
                if (chanceAGood[i].Changed) chanceAGood[i].SynchronizeUserData();
                if (chanceABad[i].Changed) chanceABad[i].SynchronizeUserData();
                if (chanceBGood[i].Changed) chanceBGood[i].SynchronizeUserData();
                if (chanceBBad[i].Changed) chanceBBad[i].SynchronizeUserData();
            }
            if (!isPetCareer)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (skillReq[i].Changed) skillReq[i].SynchronizeUserData();
                    if (chanceASkills[i].Changed) chanceASkills[i].SynchronizeUserData();
                    if (chanceBSkills[i].Changed) chanceBSkills[i].SynchronizeUserData();
                }
                for (int i = 0; i < 7; i++)
                {
                    // if (i == 6) continue;
                    if (chanceAGood[i].Changed) chanceAGood[i].SynchronizeUserData();
                    if (chanceABad[i].Changed) chanceABad[i].SynchronizeUserData();
                    if (chanceBGood[i].Changed) chanceBGood[i].SynchronizeUserData();
                    if (chanceBBad[i].Changed) chanceBBad[i].SynchronizeUserData();
                }
            }
            else
            {
                if (chanceAchance.Changed) chanceAchance.SynchronizeUserData();
                if (chanceBchance.Changed) chanceBchance.SynchronizeUserData();
            }
            if (isCastaway)
            {
                if (chanceAGood[9].Changed) chanceAGood[9].SynchronizeUserData();
                if (chanceABad[9].Changed) chanceABad[9].SynchronizeUserData();
                if (chanceBGood[9].Changed) chanceBGood[9].SynchronizeUserData();
                if (chanceBBad[9].Changed) chanceBBad[9].SynchronizeUserData();
            }
		}

        private void miAddLvl_Click(object sender, System.EventArgs e)
        {
            if (internalchg) return;
            internalchg = true;

            byte us = 1;
            List<StrItem> usitems = null;
            ushort newNoLevels = (ushort)(noLevels + 1);
            ushort newLevel = (ushort)(currentLevel + 1);
            tuning[0] = (short)newNoLevels;

            #region Job Details
            ushort newFemaleOffset = (ushort)(newNoLevels * 2);
            for (byte l = 1; l <= languageString.Count; l++)
            {
                // Make safe for empty languages
                for (int i = jobTitles[l].Count; i < newNoLevels * 4 + 1; i++) jobTitles.Add(l, "", "");
                List<StrItem> items = jobTitles[l];
                // Shift all female entries up to free male entries
                for (int i = noLevels - 1; i > 0; i--)
                {
                    items[newFemaleOffset + (i * 2) + 1].Title = items[femaleOffset + (i * 2) + 1].Title;
                    items[newFemaleOffset + (i * 2) + 2].Title = items[femaleOffset + (i * 2) + 2].Title;
                }
                // Shift female entries up to free new level
                for (int i = newNoLevels - 1; i > currentLevel; i--)
                {
                    items[newFemaleOffset + (i * 2) + 1].Title = items[newFemaleOffset + (i - 1) * 2 + 1].Title;
                    items[newFemaleOffset + (i * 2) + 2].Title = items[newFemaleOffset + (i - 1) * 2 + 2].Title;
                }
                // Shift male entries up to free new level
                for (int i = newNoLevels - 1; i > currentLevel; i--)
                {
                    items[i * 2 + 1].Title = items[(i - 1) * 2 + 1].Title;
                    items[i * 2 + 2].Title = items[(i - 1) * 2 + 2].Title;
                }
                // Clear text out of new level fields
                // (new level is currentLevel + 1, index is that - 1, so just use currentLevel)
                items[currentLevel * 2 + 1].Title = "";
                items[currentLevel * 2 + 2].Title = "";
                items[newFemaleOffset + currentLevel * 2 + 1].Title = "";
                items[newFemaleOffset + currentLevel * 2 + 2].Title = "";
            }
            usitems = jobTitles[us];
            // (new level is currentLevel + 1, index is that - 1, so just use currentLevel)
            usitems[currentLevel * 2 + 1].Title = "New Male Job Title";
            usitems[currentLevel * 2 + 2].Title = "New Male Job Desc";
            usitems[newFemaleOffset + currentLevel * 2 + 1].Title = "New Female Job Title";
            usitems[newFemaleOffset + currentLevel * 2 + 2].Title = "New Female Job Desc";
            #endregion

            insertGuid(outfit, currentLevel, 0);
            insertGuid(vehicle, currentLevel, 0x0C85AE14);

            #region Hours & Wages
            insertBcon(PTO, newLevel, 15);
            if (lifeScore != null)
                insertBcon(lifeScore, newLevel, 0);
            insertBcon(startHour, currentLevel + 1, 0);
            insertBcon(hoursWorked, currentLevel + 1, 1);
            if (!isPetCareer) // Currently Pet careers can't ever get here
            {
                insertBcon(wages, currentLevel + 1, 0);
            }
            else
            {
                insertBcon(wagesDog, currentLevel + 1, 0);
                insertBcon(wagesCat, currentLevel + 1, 0);
            }
            insertBcon(daysWorked, currentLevel + 1, 0);

            for (int i = 0; i < motiveDeltas.Length; i++)
                insertBcon(motiveDeltas[i], currentLevel + 1, 0);
            #endregion
            
            if (rewenabled)
            {
                insertGuid(goodRew, currentLevel, 0);
                insertGuid(badRew, currentLevel, 0);
            }
            if (!preuniv) insertBcon(cclevels, currentLevel + 1, 0);

            #region Promotion
            if (!isPetCareer) // people
                for (int i = 0; i < skillReq.Length; i++)
                    insertBcon(skillReq[i], currentLevel + 1, 0);
            // nothing to do for Pets
            insertBcon(friends, currentLevel + 1, 0);
            #endregion

            #region Chance Cards
            insertBcon(chanceChance, currentLevel + 1, 0);
            if (!isPetCareer)
                for (int i = 0; i < chanceASkills.Length; i++)
                {
                    insertBcon(chanceASkills[i], currentLevel + 1, 0);
                    insertBcon(chanceBSkills[i], currentLevel + 1, 0);
                }

            if (!isPetCareer)
            {
                for (int i = 0; i < 7; i++)
                {
                    insertBcon(chanceAGood[i], currentLevel + 1, 0);
                    insertBcon(chanceABad[i], currentLevel + 1, 0);
                    insertBcon(chanceBGood[i], currentLevel + 1, 0);
                    insertBcon(chanceBBad[i], currentLevel + 1, 0);
                }
            }
            else
            {
                insertBcon(chanceAchance, currentLevel + 1, 50);
                insertBcon(chanceBchance, currentLevel + 1, 50);
            }

            for (int i = 7; i < chanceAGood.Length; i++)
            {
                insertBcon(chanceAGood[i], currentLevel + 1, 0);
                insertBcon(chanceABad[i], currentLevel + 1, 0);
                insertBcon(chanceBGood[i], currentLevel + 1, 0);
                insertBcon(chanceBBad[i], currentLevel + 1, 0);
            }
            #endregion

            #region Chance Cards Texts
            for (byte i = 1; i <= languageString.Count; i++)
            {
                for (int k = chanceCardsText[i].Count; k < newNoLevels * 12 + 19; k++)
                    chanceCardsText.Add(i, "", "");
                List<StrItem> items = chanceCardsText[i];
                for (int j = newNoLevels; j > newLevel; j--)
                    for (int k = 7; k < 19; k++)
                        items[j * 12 + k].Title = items[(j - 1) * 12 + k].Title;
                for (int k = 7; k < 19; k++)
                    items[newLevel * 12 + k].Title = "";
            }

            usitems = chanceCardsText[us];
            usitems[newLevel * 12 + 7].Title = "Choice A";
            usitems[newLevel * 12 + 8].Title = "Choice B";
            usitems[newLevel * 12 + 9].Title = "Male Text";
            usitems[newLevel * 12 + 10].Title = "Female Text";
            usitems[newLevel * 12 + 11].Title = "Pass A Male";
            usitems[newLevel * 12 + 12].Title = "Pass A Female";
            usitems[newLevel * 12 + 13].Title = "Fail A Male";
            usitems[newLevel * 12 + 14].Title = "Fail A Female";
            usitems[newLevel * 12 + 15].Title = "Pass B Male";
            usitems[newLevel * 12 + 16].Title = "Pass B Female";
            usitems[newLevel * 12 + 17].Title = "Fail B Male";
            usitems[newLevel * 12 + 18].Title = "Fail B Female";
            #endregion

            noLevelsChanged(newNoLevels);

            internalchg = false;

            stabalizecount();
        }

        private void miRemoveLvl_Click(object sender, System.EventArgs e)
        {
            if (internalchg) return;
            internalchg = true;
            this.tabControl1.IsEnabled = menuItem6.IsEnabled = false;
            // this.lbcrap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold); // no .Font on Avalonia controls
            // this.lbcrap.ForeColor = System.Drawing.Color.HotPink; // no .ForeColor on Avalonia controls
            this.lbcrap.Text = "You now need to close\r\nCareer Editor then restart it";
            this.lbcrap.IsVisible = true;

            ushort newNoLevels = (ushort)(noLevels - 1);

            tuning[0] = (short)newNoLevels;

            PTO.RemoveAt(currentLevel);
            if (lifeScore != null)
                lifeScore.RemoveAt(currentLevel);

            #region Job Details
            ushort newFemaleOffset = (ushort)(newNoLevels * 2);
            for (byte l = 1; l <= languageString.Count; l++)
            {
                // Make safe for empty languages
                //for (int i = jobTitles[l].Count; i < noLevels * 4 + 1; i++) jobTitles.Add(l, "", ""); // this does nuffin, writing an empty string does not add a string
                try
                {
                    List<StrItem> items = jobTitles[l];
                    if (items.Count > noLevels * 4) // trying to clean an empty language chucks a wobbly
                    {
                        // Shift all entries down over removed level
                        for (int i = currentLevel; i < (noLevels * 2); i++)
                        {
                            items[(i - 1) * 2 + 1].Title = items[i * 2 + 1].Title;
                            items[(i - 1) * 2 + 2].Title = items[i * 2 + 2].Title;
                        }
                        // Shift female entries down over removed level
                        for (int i = currentLevel; i < noLevels; i++)
                        {
                            items[newFemaleOffset + (i - 1) * 2 + 1].Title = items[newFemaleOffset + (i * 2) + 1].Title;
                            items[newFemaleOffset + (i - 1) * 2 + 2].Title = items[newFemaleOffset + (i * 2) + 2].Title;
                        }
                        // Remove unused entries, must start at last and work back
                        int k = items.Count - 1;
                        for (int i = k; i > k - 4; i--)
                            jobTitles.Remove(items[i]);
                    }
                }
                catch { }
            }
            
            outfit.RemoveAt(currentLevel - 1);
            outfit.RemoveAt(currentLevel - 1);
            vehicle.RemoveAt(currentLevel - 1);
            vehicle.RemoveAt(currentLevel - 1);

            if (rewenabled)
            {
                goodRew.RemoveAt(currentLevel - 1);
                goodRew.RemoveAt(currentLevel - 1);
                badRew.RemoveAt(currentLevel - 1);
                badRew.RemoveAt(currentLevel - 1);
            }
            if (!preuniv) cclevels.RemoveAt(currentLevel);
            
            #endregion

            #region Hours & Wages
            startHour.RemoveAt(currentLevel);
            hoursWorked.RemoveAt(currentLevel);
            if (!isPetCareer)
            {
                wages.RemoveAt(currentLevel);
            }
            else
            {
                wagesDog.RemoveAt(currentLevel);
                wagesCat.RemoveAt(currentLevel);
            }
            daysWorked.RemoveAt(currentLevel);

            for (int i = 0; i < 9; i++)
                motiveDeltas[i].RemoveAt(currentLevel);
            #endregion

            #region Promotion
            if (!isPetCareer)
                for (int i = 0; i < skillReq.Length; i++)
                    skillReq[i].RemoveAt(currentLevel);
            // nothing to do for Pets
            friends.RemoveAt(currentLevel);
            #endregion

            #region Chance Cards

            chanceChance.RemoveAt(currentLevel);
            if (!isPetCareer)
                for (int i = 0; i < chanceASkills.Length; i++)
                {
                    chanceASkills[i].RemoveAt(currentLevel);
                    chanceBSkills[i].RemoveAt(currentLevel);
                }

            if (!isPetCareer)
                for (int i = 0; i < 7; i++)
                {
                    chanceAGood[i].RemoveAt(currentLevel);
                    chanceABad[i].RemoveAt(currentLevel);
                    chanceBGood[i].RemoveAt(currentLevel);
                    chanceBBad[i].RemoveAt(currentLevel);
                }
            else
            {
                chanceAchance.RemoveAt(currentLevel);
                chanceBchance.RemoveAt(currentLevel);
            }

            for (int i = 7; i < chanceAGood.Length; i++)
            {
                chanceAGood[i].RemoveAt(currentLevel);
                chanceABad[i].RemoveAt(currentLevel);
                chanceBGood[i].RemoveAt(currentLevel);
                chanceBBad[i].RemoveAt(currentLevel);
            }

            for (byte i = 1; i <= languageString.Count; i++)
            {
                // Make safe for empty languages
                //for (int k = chanceCardsText[i].Count; k < noLevels * 12 + 19; k++) // this does nuffing and no point trying
                //    chanceCardsText.Add(i, "", "");
                try
                {
                    List<StrItem> items = chanceCardsText[i];
                    if (items.Count > noLevels * 12) // trying to clean an empty language chucks a wobbly
                    {
                        // Shift entries down over removed level
                        for (int j = currentLevel; j < noLevels; j++)
                            for (int k = 7; k < 19; k++)
                                items[j * 12 + k].Title = items[(j + 1) * 12 + k].Title;

                        // Remove unused entries, must start at last and work back
                        for (int k = 18; k > 6; k--)
                            chanceCardsText.Remove(items[noLevels * 12 + k]);
                    }
                }
                catch { }
            }

            #endregion

            noLevelsChanged(newNoLevels);

            internalchg = false;

            stabalizecount();
        }

        private void stabalizecount()
        {
            if (noLevels > 10) return; // leave extras
            if (friends.Count < 11)
                friends.Add(0);
            if (friends.Count > 11)
                friends.RemoveAt(11);
            if (outfit.Count < 22)
            {
                outfit.Add(0);
                outfit.Add(0);
            }
            if (outfit.Count > 23)
            {
                outfit.RemoveAt(23);
                outfit.RemoveAt(22);
            }
            if (vehicle.Count < 22)
            {
                vehicle.Add(0);
                vehicle.Add(0);
            }
            if (vehicle.Count > 23)
            {
                vehicle.RemoveAt(23);
                vehicle.RemoveAt(22);
            }
            if (PTO.Count < 11)
                PTO.Add(0);
            if (PTO.Count > 11)
                PTO.RemoveAt(11);
            if (lifeScore != null)
            {
                if (lifeScore.Count < 11)
                    lifeScore.Add(0);
                if (lifeScore.Count > 11)
                    lifeScore.RemoveAt(11);
            }
            if (hoursWorked.Count > 11)
                hoursWorked.RemoveAt(11);
            if (hoursWorked.Count < 11)
                hoursWorked.Add(0);
            if (startHour.Count > 11)
                startHour.RemoveAt(11);
            if (startHour.Count < 11)
                startHour.Add(0);
            if (daysWorked.Count < 11)
                daysWorked.Add(0);
            if (daysWorked.Count > 11)
                daysWorked.RemoveAt(11);

            if (rewenabled)
            {
                if (goodRew.Count < 22)
                {
                    goodRew.Add(0);
                    goodRew.Add(0);
                }
                if (goodRew.Count > 23)
                {
                    goodRew.RemoveAt(23);
                    goodRew.RemoveAt(22);
                }
                if (badRew.Count < 22)
                {
                    badRew.Add(0);
                    badRew.Add(0);
                }
                if (badRew.Count > 23)
                {
                    badRew.RemoveAt(23);
                    badRew.RemoveAt(22);
                }
            }
            if (!preuniv)
            {
                if (cclevels.Count < 11)
                    cclevels.Add(0);
                if (cclevels.Count > 11)
                    cclevels.RemoveAt(11);
            }

            if (!isPetCareer)
            {
                if (wages.Count < 11)
                    wages.Add(0);
                if (wages.Count > 11)
                    wages.RemoveAt(11);
                for (int i = 0; i < chanceASkills.Length; i++)
                {
                    if (chanceASkills[i].Count > 11)
                        chanceASkills[i].RemoveAt(11);
                    if (chanceASkills[i].Count < 11)
                        chanceASkills[i].Add(0);
                    if (chanceBSkills[i].Count > 11)
                        chanceBSkills[i].RemoveAt(11);
                    if (chanceBSkills[i].Count < 11)
                        chanceBSkills[i].Add(0);
                }
                for (int i = 0; i < 7; i++)
                {
                    if (chanceAGood[i].Count > 11)
                        chanceAGood[i].RemoveAt(11);
                    if (chanceAGood[i].Count < 11)
                        chanceAGood[i].Add(0);
                    if (chanceABad[i].Count > 11)
                        chanceABad[i].RemoveAt(11);
                    if (chanceABad[i].Count < 11)
                        chanceABad[i].Add(0);
                    if (chanceBGood[i].Count > 11)
                        chanceBGood[i].RemoveAt(11);
                    if (chanceBGood[i].Count < 11)
                        chanceBGood[i].Add(0);
                    if (chanceBBad[i].Count > 11)
                        chanceBBad[i].RemoveAt(11);
                    if (chanceBBad[i].Count < 11)
                        chanceBBad[i].Add(0);
                }
                for (int i = 0; i < skillReq.Length; i++)
                {
                    if (skillReq[i].Count > 11)
                        skillReq[i].RemoveAt(11);
                    if (skillReq[i].Count < 11)
                        skillReq[i].Add(0);
                }
            }
            else
            {
                if (wagesDog.Count < 11)
                    wagesDog.Add(0);
                if (wagesDog.Count > 11)
                    wagesDog.RemoveAt(11);
                if (wagesCat.Count < 11)
                    wagesCat.Add(0);
                if (wagesCat.Count > 11)
                    wagesCat.RemoveAt(11);
            }
            for (int i = 0; i < SUNSHINE; i++)
            {
                if (motiveDeltas[i].Count < 11)
                    motiveDeltas[i].Add(0);
                if (motiveDeltas[i].Count > 11)
                    motiveDeltas[i].RemoveAt(11);
            }
            if (motiveDeltas[SUNSHINE].Count < 12)
                motiveDeltas[SUNSHINE].Add(0);
            if (motiveDeltas[SUNSHINE].Count > 12)
                motiveDeltas[SUNSHINE].RemoveAt(12);
            if (chanceChance.Count > 11)
                chanceChance.RemoveAt(11);
            if (chanceChance.Count < 11)
                chanceChance.Add(0);

            for (int i = 7; i < chanceAGood.Length; i++)
            {
                if (chanceAGood[i].Count > 11)
                    chanceAGood[i].RemoveAt(11);
                if (chanceAGood[i].Count < 11)
                    chanceAGood[i].Add(0);
                if (chanceABad[i].Count > 11)
                    chanceABad[i].RemoveAt(11);
                if (chanceABad[i].Count < 11)
                    chanceABad[i].Add(0);
                if (chanceBGood[i].Count > 11)
                    chanceBGood[i].RemoveAt(11);
                if (chanceBGood[i].Count < 11)
                    chanceBGood[i].Add(0);
                if (chanceBBad[i].Count > 11)
                    chanceBBad[i].RemoveAt(11);
                if (chanceBBad[i].Count < 11)
                    chanceBBad[i].Add(0);
            }
        }

        private void miEnglishOnly_Click(object sender, System.EventArgs e)
        {
            englishOnly = !englishOnly;
            miEnglishOnly.IsChecked = englishOnly;
            if (englishOnly) Language.SelectedIndex = 0;
            Language.IsEnabled = !englishOnly;
        }

		private void CareerTitle_TextChanged(object sender, System.EventArgs e)
		{
            if (internalchg) return;
			string text = ((Avalonia.Controls.TextBox)sender).Text;
            if (((List<StrItem>)catalogueDesc[currentLanguage]).Count == 0)
                ((List<StrItem>)catalogueDesc[currentLanguage]).Add(new StrItem(catalogueDesc));
            catalogueDesc[currentLanguage][0].Title = text;
		}
        private void Language_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (internalchg) return;
            internalchg = true;

            int index = ((Avalonia.Controls.ComboBox)sender).SelectedIndex;
            currentLanguage = (byte)(index + 1);
            JobDetailList.Items.Clear();
            fillJobDetails();

            CareerTitle.Text = (((List<StrItem>)catalogueDesc[currentLanguage]).Count == 0) ? "" : catalogueDesc[currentLanguage, 0].Title;
            internalchg = false;

            ushort newLevel = currentLevel;
            currentLevel = 0;
            levelChanged(newLevel);
        }

        private void JobDetailList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (levelChanging) return;
            ListView.SelectedIndexCollection indices = ((ListView)sender).SelectedIndices;
			if ((indices.Count > 0) && (indices[0] < noLevels))
                levelChanged((ushort)(indices[0] + 1));
        }
        private void JobDetailsCopy_LinkClicked(object sender, EventArgs e)
        {
            jdpMale.DescValue = jdpFemale.DescValue;
        }
        private void jdpMale_TitleValueChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            string text = jdpMale.TitleValue;
            jobTitles[currentLanguage][currentLevel * 2 - 1].Title = text;
        }
        private void jdpMale_DescValueChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            string text = jdpMale.DescValue;
            jobTitles[currentLanguage][currentLevel * 2].Title = text;
        }
        private void jdpFemale_TitleValueChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            string text = jdpFemale.TitleValue;
            ListViewItem item = JobDetailList.Items[currentLevel - 1];
            item.SubItems[1].Text = text;
            jobTitles[currentLanguage][currentLevel * 2 - 1 + femaleOffset].Title = text;
		}
        private void jdpFemale_DescValueChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            string text = jdpFemale.DescValue;
            ListViewItem item = JobDetailList.Items[currentLevel - 1];
            item.SubItems[2].Text = text;
            jobTitles[currentLanguage][currentLevel * 2 + femaleOffset].Title = text;
		}
        private void gcOutfit_GUIDChooserValueChanged(object sender, EventArgs e)
        {
            if (internalchg) return;

            ListViewItem item = JobDetailList.Items[currentLevel - 1];

            if (isCastaway)
            {
                // Castaway Stories uses its own outfit GUID/name arrays.
                item.SubItems[3].Text = StringFromGUID(gcOutfit.Value, CSoutfitGUID, CSoutfitName);
            }
            else
            {
                // Clean SimPE: use the standard outfit list only.
                item.SubItems[3].Text = StringFromGUID(gcOutfit.Value, outfitGUID, outfitName);
            }

            // Store the selected outfit GUID split into two 16-bit shorts (unchanged logic).
            outfit[currentLevel * 2]     = (short)(gcOutfit.Value & 0xffff);
            outfit[currentLevel * 2 + 1] = (short)((gcOutfit.Value >> 16) & 0xffff);
        }

        private void gcVehicle_GUIDChooserValueChanged(object sender, EventArgs e)
        {
            if (internalchg) return;

            ListViewItem item = JobDetailList.Items[currentLevel - 1];
            item.SubItems[4].Text = StringFromGUID(gcVehicle.Value, vehicleGUID, vehicleName);

            vehicle[currentLevel * 2] = (short)(gcVehicle.Value & 0xffff);
            vehicle[currentLevel * 2 + 1] = (short)(gcVehicle.Value >> 16 & 0xffff);
        }

        private void HoursWagesList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (levelChanging) return;
            ListView.SelectedIndexCollection indices = ((ListView)sender).SelectedIndices;
			if ((indices.Count > 0) && (indices[0] < noLevels))
                levelChanged((ushort)(indices[0] + 1));
        }
        private void lnudWork_ValueChanged(object sender, System.EventArgs e)
        {
            if (levelChanging || internalchg) return;
            if (isCastaway)
                resinc[currentLevel] = (short)lnudWages.Value;
            LabelledNumericUpDown nud = (LabelledNumericUpDown)sender;
            ListViewItem item = HoursWagesList.Items[currentLevel - 1];
            int i = -1;

            #region Hours
            List<LabelledNumericUpDown> lHours = new List<LabelledNumericUpDown>(new LabelledNumericUpDown[] {
                lnudWorkStart, lnudWorkHours,
            });
            List<Bcon> lbHours = new List<Bcon>(new Bcon[] { startHour, hoursWorked, });
            i = lHours.IndexOf(nud);
            if (i >= 0)
            {
                lbHours[i][currentLevel] = (short)nud.Value;
                item.SubItems[i + 1].Text = "" + nud.Value;
                tbWorkFinish.Text = Convert.ToString((startHour[currentLevel] + hoursWorked[currentLevel]) % 24);
                item.SubItems[3].Text = tbWorkFinish.Text;
                WorkChanged(currentLevel);
                return;
            }
            #endregion

            #region Wages
            if (isCastaway)
            {
                List<LabelledNumericUpDown> lWages = new List<LabelledNumericUpDown>(new LabelledNumericUpDown[] {
                lnudWages, lnudFoods, lnudWagesCat, });
                List<Bcon> lbWages = new List<Bcon>(new Bcon[] { wages, foodinc, wagesCat, });
                i = lWages.IndexOf(nud);
                if (i >= 0)
                {
                    lbWages[i][currentLevel] = (short)nud.Value;
                    item.SubItems[i + 4].Text = "" + nud.Value;
                    return;
                }
            }
            else
            {
                List<LabelledNumericUpDown> lWages = new List<LabelledNumericUpDown>(new LabelledNumericUpDown[] {
                lnudWages, lnudWagesDog, lnudWagesCat,
            });
                List<Bcon> lbWages = new List<Bcon>(new Bcon[] { wages, wagesDog, wagesCat, });
                i = lWages.IndexOf(nud);
                if (i >= 0)
                {
                    lbWages[i][currentLevel] = (short)nud.Value;
                    item.SubItems[i + 4].Text = "" + nud.Value;
                    return;
                }
            }

            #endregion
        }
        private void lnudWork_KeyUp(object sender, Avalonia.Input.KeyEventArgs e)
        {
            lnudWork_ValueChanged(sender, new EventArgs());
        }
		private void Workday_CheckedChanged(object sender, System.EventArgs e)
		{
            if (levelChanging || internalchg) return;

            List<CheckBox> lcb = new List<CheckBox>(new CheckBox[] {
                WorkMonday, WorkTuesday, WorkWednesday, WorkThursday, WorkFriday, WorkSaturday, WorkSunday, 
            });

            int index = lcb.IndexOf((CheckBox)sender);
            if (index < 0 || index > 6) return; // crash!

            Boolset dw = new Boolset((byte)daysWorked[currentLevel]);
            dw[index] = ((CheckBox)sender).IsChecked == true;
            daysWorked[currentLevel] = (byte)dw;

            ListViewItem item = HoursWagesList.Items[currentLevel - 1];
            item.SubItems[index + 7].Text = ((CheckBox)sender).IsChecked.GetValueOrDefault().ToString();
        }
        private void numPTO_ValueChanged(object sender, System.EventArgs e)
        {
            if (levelChanging || internalchg) return;
            PTO[currentLevel] = (short)numPTO.Value;
        }
        private void numLscore_ValueChanged(object sender, System.EventArgs e)
        {
            if (levelChanging || internalchg) return;
            lifeScore[currentLevel] = (short)numLscore.Value;
        }
		private void nudMotive_ValueChanged(object sender, System.EventArgs e)
		{
            if (levelChanging || internalchg) return;
            NumericUpDown nud = (NumericUpDown)sender;
            ListViewItem item = HoursWagesList.Items[currentLevel - 1];
            int i = -1;

            #region Motives
            List<NumericUpDown> lMotive = new List<NumericUpDown>(new NumericUpDown[] {
                WorkHunger, WorkAmorous, WorkComfort, WorkHygiene, WorkBladder,
                WorkEnergy, WorkFun, WorkPublic, WorkSunshine,
            });
            List<NumericUpDown> lMotiveTotal = new List<NumericUpDown>(new NumericUpDown[] {
                HungerTotal, AmorousTotal, ComfortTotal, HygieneTotal, BladderTotal,
                EnergyTotal, FunTotal, PublicTotal, SunshineTotal,
            });
            i = lMotive.IndexOf(nud);
            if (i >= 0)
            {
                motiveDeltas[i][currentLevel] = (short)nud.Value;
                lMotiveTotal[i].Value = motiveDeltas[i][currentLevel] * hoursWorked[currentLevel];
                return;
            }
            #endregion
        }
        private void nudMotive_KeyUp(object sender, Avalonia.Input.KeyEventArgs e)
		{
            nudMotive_ValueChanged(sender, new EventArgs());
		}

        private void PromoList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (levelChanging) return;
            ListView.SelectedIndexCollection indices = ((ListView)sender).SelectedIndices;
            if ((indices.Count > 0) && (indices[0] < noLevels))
                levelChanged((ushort)(indices[0] + 1));
        }
		private void Promo_ValueChanged(object sender, System.EventArgs e)
		{
            if (levelChanging || internalchg || sender == null) return;
            if (isPetCareer)
            {
                friends[currentLevel] = (short)PromoFriends.Value;
                ListViewItem itemx = PromoList.Items[currentLevel - 1];
                itemx.SubItems[8].Text = "" + (short)PromoFriends.Value;
                return;
            }
            ArrayList alNud = new ArrayList(new NumericUpDown[] {
                PromoCooking, PromoMechanical, PromoBody, PromoCharisma,
                PromoCreativity, PromoLogic, PromoCleaning, PromoFriends,
            });
            int i = alNud.IndexOf((NumericUpDown)sender);
            if (i == -1) return; // crash!

            ListViewItem item = PromoList.Items[currentLevel - 1];
            short val = (short)((NumericUpDown)sender).Value;
            item.SubItems[i + 1].Text = "" + val;

            if (i < skillReq.Length)
                skillReq[i][currentLevel] = (short)(val * 100);
            else switch (i - skillReq.Length)
                {
                    case 0:
                        friends[currentLevel] = val;
                        break;
                }
        }
		private void Promo_KeyUp(object sender, Avalonia.Input.KeyEventArgs e)
		{
            Promo_ValueChanged(sender, e);		
		}
        private void cbTrick_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (levelChanging || internalchg) return;
            ListViewItem item = PromoList.Items[currentLevel - 1];
            item.SubItems[9].Text = (String)((ComboBox)sender).SelectedItem;

            List<short[]> lTrick = new List<short[]>();
            for (int i = 0; i < trick.Count / 2; i++)
                lTrick.Add(new short[] { trick[i * 2], trick[i * 2 + 1] });

            short[] result = new short[] { (short)((ComboBox)sender).SelectedIndex, (short)currentLevel };

            int insPtr = 0;
            while (insPtr < lTrick.Count && currentLevel > lTrick[insPtr][1])
                insPtr++;

            if (insPtr < lTrick.Count)
            {
                if (currentLevel == lTrick[insPtr][1])
                    lTrick[insPtr] = result;
                else
                    lTrick.Insert(insPtr, result);
            }
            else
                lTrick.Add(result);

            trick.Clear();
            foreach (short[] pair in lTrick)
            {
                trick.Add(pair[0]);
                trick.Add(pair[1]);
            }
        }

        private void lnudChanceCurrentLevel_ValueChanged(object sender, EventArgs e)
        {
            if (levelChanging || internalchg) return;
            levelChanged((ushort)lnudChanceCurrentLevel.Value);
        }
        private void ChanceCopy_LinkClicked(object sender, EventArgs e)
		{
            ChanceTextFemale.Text = ChanceTextMale.Text;
        }

        private void textBox1g_TextChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                if (sender.GetType() == typeof(TextBox))
                {
                    tbox = sender as TextBox;
                    lbrewguid.Text = pjse.GUIDIndex.TheGUIDIndex[SimPe.Helper.HexStringToUInt(tbox.Text)];
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // foreach disabled — extras tab is removed at runtime
            // foreach (object ct in gbExtras.Controls) { ct.IsEnabled = checkBox1.IsChecked.GetValueOrDefault(); }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            chanceBcheckBox_checkup(sender, e);
        }

        
        private void boxCheckAchance_checkup(object sender, EventArgs e)
        {
            if (checkBox8.IsChecked.GetValueOrDefault()) checkBox4.IsChecked = false;
            if (checkBox47.IsChecked.GetValueOrDefault()) checkBox12.IsChecked = false;
            if (checkBox50.IsChecked.GetValueOrDefault()) checkBox16.IsChecked = false;
            if (checkBox53.IsChecked.GetValueOrDefault()) checkBox20.IsChecked = false;
            if (checkBox56.IsChecked.GetValueOrDefault()) checkBox24.IsChecked = false;
            if (checkBox59.IsChecked.GetValueOrDefault()) checkBox28.IsChecked = false;
            if (checkBox62.IsChecked.GetValueOrDefault()) checkBox32.IsChecked = false;
            if (checkBox65.IsChecked.GetValueOrDefault()) checkBox36.IsChecked = false;
            if (checkBox68.IsChecked.GetValueOrDefault()) checkBox40.IsChecked = false;
            if (checkBox71.IsChecked.GetValueOrDefault()) checkBox44.IsChecked = false;
        }

        private void boxCheckBchance_checkup(object sender, EventArgs e)
        {
            if (checkBox4.IsChecked.GetValueOrDefault()) checkBox8.IsChecked = false;
            if (checkBox12.IsChecked.GetValueOrDefault()) checkBox47.IsChecked = false;
            if (checkBox16.IsChecked.GetValueOrDefault()) checkBox50.IsChecked = false;
            if (checkBox20.IsChecked.GetValueOrDefault()) checkBox53.IsChecked = false;
            if (checkBox24.IsChecked.GetValueOrDefault()) checkBox56.IsChecked = false;
            if (checkBox28.IsChecked.GetValueOrDefault()) checkBox59.IsChecked = false;
            if (checkBox32.IsChecked.GetValueOrDefault()) checkBox62.IsChecked = false;
            if (checkBox36.IsChecked.GetValueOrDefault()) checkBox65.IsChecked = false;
            if (checkBox40.IsChecked.GetValueOrDefault()) checkBox68.IsChecked = false;
            if (checkBox44.IsChecked.GetValueOrDefault()) checkBox71.IsChecked = false;
        }

        private void checkAchanceBox_checkup(object sender, EventArgs e)
        {
            if (checkBox9.IsChecked.GetValueOrDefault()) checkBox6.IsChecked = false;
            if (checkBox46.IsChecked.GetValueOrDefault()) checkBox10.IsChecked = false;
            if (checkBox49.IsChecked.GetValueOrDefault()) checkBox14.IsChecked = false;
            if (checkBox52.IsChecked.GetValueOrDefault()) checkBox18.IsChecked = false;
            if (checkBox55.IsChecked.GetValueOrDefault()) checkBox22.IsChecked = false;
            if (checkBox58.IsChecked.GetValueOrDefault()) checkBox26.IsChecked = false;
            if (checkBox61.IsChecked.GetValueOrDefault()) checkBox30.IsChecked = false;
            if (checkBox64.IsChecked.GetValueOrDefault()) checkBox34.IsChecked = false;
            if (checkBox67.IsChecked.GetValueOrDefault()) checkBox38.IsChecked = false;
            if (checkBox70.IsChecked.GetValueOrDefault()) checkBox42.IsChecked = false;
        }

        private void checkBchanceBox_checkup(object sender, EventArgs e)
        {
            if (checkBox6.IsChecked.GetValueOrDefault()) checkBox9.IsChecked = false;
            if (checkBox10.IsChecked.GetValueOrDefault()) checkBox46.IsChecked = false;
            if (checkBox14.IsChecked.GetValueOrDefault()) checkBox49.IsChecked = false;
            if (checkBox18.IsChecked.GetValueOrDefault()) checkBox52.IsChecked = false;
            if (checkBox22.IsChecked.GetValueOrDefault()) checkBox55.IsChecked = false;
            if (checkBox26.IsChecked.GetValueOrDefault()) checkBox58.IsChecked = false;
            if (checkBox30.IsChecked.GetValueOrDefault()) checkBox61.IsChecked = false;
            if (checkBox34.IsChecked.GetValueOrDefault()) checkBox64.IsChecked = false;
            if (checkBox38.IsChecked.GetValueOrDefault()) checkBox67.IsChecked = false;
            if (checkBox42.IsChecked.GetValueOrDefault()) checkBox70.IsChecked = false;
        }

        private void chanceBcheckBox_checkup(object sender, EventArgs e)
        {
            comboBox1.IsEnabled = (!checkBox5.IsChecked.GetValueOrDefault() && checkBox2.IsChecked.GetValueOrDefault());
            comboBox2.IsEnabled = (!checkBox11.IsChecked.GetValueOrDefault() && checkBox2.IsChecked.GetValueOrDefault());
            comboBox3.IsEnabled = (!checkBox15.IsChecked.GetValueOrDefault() && checkBox2.IsChecked.GetValueOrDefault());
            comboBox4.IsEnabled = (!checkBox19.IsChecked.GetValueOrDefault() && checkBox2.IsChecked.GetValueOrDefault());
            comboBox5.IsEnabled = (!checkBox23.IsChecked.GetValueOrDefault() && checkBox2.IsChecked.GetValueOrDefault());
            comboBox6.IsEnabled = (!checkBox27.IsChecked.GetValueOrDefault() && checkBox2.IsChecked.GetValueOrDefault());
            comboBox7.IsEnabled = (!checkBox31.IsChecked.GetValueOrDefault() && checkBox2.IsChecked.GetValueOrDefault());
            comboBox8.IsEnabled = (!checkBox35.IsChecked.GetValueOrDefault() && checkBox2.IsChecked.GetValueOrDefault());
            comboBox9.IsEnabled = (!checkBox39.IsChecked.GetValueOrDefault() && checkBox2.IsChecked.GetValueOrDefault());
            comboBox10.IsEnabled = (!checkBox43.IsChecked.GetValueOrDefault() && checkBox2.IsChecked.GetValueOrDefault());
        }

        private void woohoocheckBox_checkup(object sender, EventArgs e)
        {
            if (!rewenabled) return;
            if (checkBox7.IsChecked.GetValueOrDefault()) checkBox3.IsChecked = false;
            if (checkBox48.IsChecked.GetValueOrDefault()) checkBox13.IsChecked = false;
            if (checkBox51.IsChecked.GetValueOrDefault()) checkBox17.IsChecked = false;
            if (checkBox54.IsChecked.GetValueOrDefault()) checkBox21.IsChecked = false;
            if (checkBox57.IsChecked.GetValueOrDefault()) checkBox25.IsChecked = false;
            if (checkBox60.IsChecked.GetValueOrDefault()) checkBox29.IsChecked = false;
            if (checkBox63.IsChecked.GetValueOrDefault()) checkBox33.IsChecked = false;
            if (checkBox66.IsChecked.GetValueOrDefault()) checkBox37.IsChecked = false;
            if (checkBox69.IsChecked.GetValueOrDefault()) checkBox41.IsChecked = false;
            if (checkBox72.IsChecked.GetValueOrDefault()) checkBox45.IsChecked = false;
        }

        private void chanceAcheckBox_checkup(object sender, EventArgs e)
        {
            if (checkBox3.IsChecked.GetValueOrDefault()) checkBox7.IsChecked = false;
            if (checkBox13.IsChecked.GetValueOrDefault()) checkBox48.IsChecked = false;
            if (checkBox17.IsChecked.GetValueOrDefault()) checkBox51.IsChecked = false;
            if (checkBox21.IsChecked.GetValueOrDefault()) checkBox54.IsChecked = false;
            if (checkBox25.IsChecked.GetValueOrDefault()) checkBox57.IsChecked = false;
            if (checkBox29.IsChecked.GetValueOrDefault()) checkBox60.IsChecked = false;
            if (checkBox33.IsChecked.GetValueOrDefault()) checkBox63.IsChecked = false;
            if (checkBox37.IsChecked.GetValueOrDefault()) checkBox66.IsChecked = false;
            if (checkBox41.IsChecked.GetValueOrDefault()) checkBox69.IsChecked = false;
            if (checkBox45.IsChecked.GetValueOrDefault()) checkBox72.IsChecked = false;
        }

        private void setmajors()
        {
            int requiredmajor = getBcon(0x1056)[1];
            if (requiredmajor >= 32768) requiredmajor -= 32768;
            if (requiredmajor >= 16384) requiredmajor -= 16384;
            if (requiredmajor >= 8192) requiredmajor -= 8192;
            if (requiredmajor >= 4096) requiredmajor -= 4096;
            if (requiredmajor >= 2048) requiredmajor -= 2048;
            if (requiredmajor >= 1024) { requiredmajor -= 1024; this.cbrphyco.IsChecked = true; } else this.cbrphyco.IsChecked = false;
            if (requiredmajor >= 512) { requiredmajor -= 512; this.cbrpolit.IsChecked = true; } else this.cbrpolit.IsChecked = false;
            if (requiredmajor >= 256) { requiredmajor -= 256; this.cbrphysi.IsChecked = true; } else this.cbrphysi.IsChecked = false;
            if (requiredmajor >= 128) { requiredmajor -= 128; this.cbrphilo.IsChecked = true; } else this.cbrphilo.IsChecked = false;
            if (requiredmajor >= 64) { requiredmajor -= 64; this.cbrmaths.IsChecked = true; } else this.cbrmaths.IsChecked = false;
            if (requiredmajor >= 32) { requiredmajor -= 32; this.cbrliter.IsChecked = true; } else this.cbrliter.IsChecked = false;
            if (requiredmajor >= 16) { requiredmajor -= 16; this.cbrhisto.IsChecked = true; } else this.cbrhisto.IsChecked = false;
            if (requiredmajor >= 8) { requiredmajor -= 8; this.cbrecon.IsChecked = true; } else this.cbrecon.IsChecked = false;
            if (requiredmajor >= 4) { requiredmajor -= 4; this.cbrdrama.IsChecked = true; } else this.cbrdrama.IsChecked = false;
            if (requiredmajor >= 2) { requiredmajor -= 2; this.cbrbiol.IsChecked = true; } else this.cbrbiol.IsChecked = false;
            if (requiredmajor >= 1) this.cbrArt.IsChecked = true; else this.cbrArt.IsChecked = false;

            int allowedmajor = getBcon(0x1056)[2];
            if (allowedmajor >= 32768) allowedmajor -= 32768;
            if (allowedmajor >= 16384) allowedmajor -= 16384;
            if (allowedmajor >= 8192) allowedmajor -= 8192;
            if (allowedmajor >= 4096) allowedmajor -= 4096;
            if (allowedmajor >= 2048) allowedmajor -= 2048;
            if (allowedmajor >= 1024) { allowedmajor -= 1024; this.cbaphyco.IsChecked = true; } else this.cbaphyco.IsChecked = false;
            if (allowedmajor >= 512) { allowedmajor -= 512; this.cbapolit.IsChecked = true; } else this.cbapolit.IsChecked = false;
            if (allowedmajor >= 256) { allowedmajor -= 256; this.cbaphysi.IsChecked = true; } else this.cbaphysi.IsChecked = false;
            if (allowedmajor >= 128) { allowedmajor -= 128; this.cbrahilo.IsChecked = true; } else this.cbrahilo.IsChecked = false;
            if (allowedmajor >= 64) { allowedmajor -= 64; this.cbamaths.IsChecked = true; } else this.cbamaths.IsChecked = false;
            if (allowedmajor >= 32) { allowedmajor -= 32; this.cbaliter.IsChecked = true; } else this.cbaliter.IsChecked = false;
            if (allowedmajor >= 16) { allowedmajor -= 16; this.cbahisto.IsChecked = true; } else this.cbahisto.IsChecked = false;
            if (allowedmajor >= 8) { allowedmajor -= 8; this.cbaecon.IsChecked = true; } else this.cbaecon.IsChecked = false;
            if (allowedmajor >= 4) { allowedmajor -= 4; this.cbadrama.IsChecked = true; } else this.cbadrama.IsChecked = false;
            if (allowedmajor >= 2) { allowedmajor -= 2; this.cbabiol.IsChecked = true; } else this.cbabiol.IsChecked = false;
            if (allowedmajor >= 1) this.cbaArt.IsChecked = true; else this.cbaArt.IsChecked = false;
        }

        private void btmajApply_Click(object sender, EventArgs e)
        {
            majors[1] = majors[2] = 0;
            if (cbrArt.IsChecked.GetValueOrDefault()) majors[1] = 1;
            if (cbrbiol.IsChecked.GetValueOrDefault()) majors[1] += 2;
            if (cbrdrama.IsChecked.GetValueOrDefault()) majors[1] += 4;
            if (cbrecon.IsChecked.GetValueOrDefault()) majors[1] += 8;
            if (cbrhisto.IsChecked.GetValueOrDefault()) majors[1] += 16;
            if (cbrliter.IsChecked.GetValueOrDefault()) majors[1] += 32;
            if (cbrmaths.IsChecked.GetValueOrDefault()) majors[1] += 64;
            if (cbrhisto.IsChecked.GetValueOrDefault()) majors[1] += 128;
            if (cbrphysi.IsChecked.GetValueOrDefault()) majors[1] += 256;
            if (cbrpolit.IsChecked.GetValueOrDefault()) majors[1] += 512;
            if (cbrphyco.IsChecked.GetValueOrDefault()) majors[1] += 1024;

            if (cbaArt.IsChecked.GetValueOrDefault()) majors[2] = 1;
            if (cbabiol.IsChecked.GetValueOrDefault()) majors[2] += 2;
            if (cbadrama.IsChecked.GetValueOrDefault()) majors[2] += 4;
            if (cbaecon.IsChecked.GetValueOrDefault()) majors[2] += 8;
            if (cbahisto.IsChecked.GetValueOrDefault()) majors[2] += 16;
            if (cbaliter.IsChecked.GetValueOrDefault()) majors[2] += 32;
            if (cbamaths.IsChecked.GetValueOrDefault()) majors[2] += 64;
            if (cbrahilo.IsChecked.GetValueOrDefault()) majors[2] += 128;
            if (cbaphysi.IsChecked.GetValueOrDefault()) majors[2] += 256;
            if (cbapolit.IsChecked.GetValueOrDefault()) majors[2] += 512;
            if (cbaphyco.IsChecked.GetValueOrDefault()) majors[2] += 1024;

            majors.SynchronizeUserData();
        }

        private void btUgrade_Click(object sender, EventArgs e)
        {
            bool ok = false;

            // Core Bidou/PJSE tuning BCONs � keep these.
            if (getBcon(0x1056) == null)
                ok = makeBcon(0x1056, 2, "Tuning - Flags");
            if (getBcon(0x1057) == null)
                ok = makeBcon(0x1057, 10, "Tuning - Chance Card Levels");
            if (getBcon(0x1058) == null)
                ok = makeBcon(0x1058, 1, "Top Memory");
            if (getBcon(0x1059) == null && !isPetCareer)
                ok = makeBcon(0x1059, 11, "Motive Deltas - Plantsim Sunshine");

            // Chris Hatch PrettyGirls/Tits/Angels BCONs removed:
            // 0x105A, 0x105B ("Tuning - Chance - Good - Item GUIDs") were used only by his sex-mod items.

            if (ok)
            {
                this.btUgrade.IsVisible = false;
                this.tabControl1.IsEnabled = menuItem6.IsEnabled = false;

                // this.lbcrap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold); // no .Font on Avalonia controls
                // this.lbcrap.ForeColor = System.Drawing.Color.HotPink; // no .ForeColor on Avalonia controls
                this.lbcrap.Text = "You now need to close\r\nCareer Editor then restart it";
                this.lbcrap.IsVisible = true;
            }
        }


        private void cbischance_CheckedChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            if (cbischance.IsChecked.GetValueOrDefault()) cclevels[currentLevel] = (short)1;
            else cclevels[currentLevel] = (short)0;
        }

        private void lnudChancePercent_ValueChanged(object sender, EventArgs e)
        {
            cpChoiceA.HaveSkills = cpChoiceB.HaveSkills = (lnudChancePercent.Value < 0 && !isPetCareer);
        }

        private void lnudFoods_ValueChanged(object sender, EventArgs e)
        {
            if (internalchg) return;
            foodinc[currentLevel] = (short)lnudFoods.Value;
            lnudWork_ValueChanged(sender, e);
        }
    }
}
