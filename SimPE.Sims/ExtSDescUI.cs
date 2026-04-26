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

using Ambertation.Windows.Forms;
using Avalonia.Controls;
using SimPe.Data;
using SimPe.Interfaces;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;
using SimPe.PackedFiles.Wrapper.Supporting;
using SimPe.Windows.Forms;
using System;
using System.Collections;
using System.Windows.Forms;
using Button = Avalonia.Controls.Button;
using ComboBox = Avalonia.Controls.ComboBox;
using ListBox = Avalonia.Controls.ListBox;
using Panel = Avalonia.Controls.Panel;
using System.ComponentModel;
using System.Drawing;

namespace SimPe.PackedFiles.UserInterface
{
    /// <summary>
    /// Clean 0.73-style ExtSDesc UI
    /// </summary>
    public partial class ExtSDesc : WrapperBaseControl, IPackedFileUI
    {
        private System.Resources.ResourceManager strresources;
        private ThemeManager themeManager;

        public ExtSDesc()
        {
            strresources = new System.Resources.ResourceManager(typeof(ExtSDesc));
            // Text (WinForms form title) not applicable in Avalonia plugin wrapper

            // Windows Form Designer initialization
            InitializeComponent();
            Initialize();

            // toolBar1.Renderer: MediaPlayerRenderer is WinForms-only; no-op in Avalonia

            // Create a THEMING context (per-control)
            themeManager = ThemeManager.Global.CreateChild();

            themeManager.AddControl(this.toolBar1);
            themeManager.AddControl(this.srcTb);
            themeManager.AddControl(this.dstTb);
            themeManager.AddControl(this.xpTaskBoxSimple1);
            themeManager.AddControl(this.xpTaskBoxSimple2);
            themeManager.AddControl(this.xpTaskBoxSimple3);
            themeManager.AddControl(this.miRel);
            themeManager.AddControl(this.mbiLink);

            // Sidebar button tags
            this.biId.Tag = pnId;
            this.biSkill.Tag = pnSkill;
            this.biChar.Tag = pnChar;
            this.biCareer.Tag = pnCareer;
            this.biEP1.Tag = pnEP1;
            this.biEP2.Tag = pnEP2;
            this.biEP3.Tag = pnEP3;
            this.biEP6.Tag = pnVoyage;
            this.biEP7.Tag = pnEP7;
            this.biInt.Tag = pnInt;
            this.biRel.Tag = pnRel;
            this.biMisc.Tag = pnMisc;

            // Hidden-mode options
            this.tbsim.IsReadOnly = !Helper.XmlRegistry.HiddenMode;
            this.miRelink.IsEnabled = Helper.XmlRegistry.HiddenMode;
            this.tbBugColl.IsReadOnly = !Helper.XmlRegistry.HiddenMode;
            this.tbHobbyPre.IsVisible = Helper.XmlRegistry.HiddenMode;

            InitDropDowns();
            SelectButton(biId);

            intern = true;
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName == "en")
            { /* pbLastGrade.DisplayOffset: WinForms-only custom property; no-op in Avalonia */ }
            else
            { /* pbLastGrade.DisplayOffset: WinForms-only custom property; no-op in Avalonia */ }
            intern = false;

            lv.SimDetails = true;
        }
    




		void Initialize()
		{			
			this.tbEp3Flag.IsReadOnly = !Helper.XmlRegistry.HiddenMode;
			this.tbEp3Lot.IsReadOnly = !Helper.XmlRegistry.HiddenMode;

			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExtSDesc));
			this.Commited += new System.EventHandler(this.ExtSDesc_Commited);

			this.srcRel = new SimPe.PackedFiles.UserInterface.CommonSrel();
			this.dstRel = new SimPe.PackedFiles.UserInterface.CommonSrel();

			// 
			// srcRel
			// 
			// srcRel.Dock: DockStyle is WinForms-only; no-op in Avalonia
			this.srcRel.IsEnabled = false;
			this.srcRel.Name = "srcRel";
			this.srcRel.Srel = null;
			this.srcRel.IsVisible = true;

			//
			// dstRel
			//
			// dstRel.Dock: DockStyle is WinForms-only; no-op in Avalonia
			this.dstRel.IsEnabled = false;
			this.dstRel.Name = "dstRel";
			this.dstRel.Srel = null;
			this.dstRel.IsVisible = true;

            // srcTb.Controls.Add / dstTb.Controls.Add: Avalonia Border has a single Child;
            // child insertion is handled in the AXAML layout — no-op here
            // this.dstTb.Top = this.srcTb.Bottom: WinForms geometry; no-op in Avalonia

            // cbEp3Asgn.ResourceManager / .Enum: custom WinForms ComboBox properties; no-op            
		}		


		public void SelectButton(Button b)
		{
			foreach (var child in this.toolBar1.Children)
			{
				if (child is Button item)
				{
					
					if (item.Tag!=null) 
					{
						Panel pn = (Panel)item.Tag;
                        if (pn == pnChar)
                        {
                            SetCharacterAttributesVisibility();                            
                        }
                        
                        pn.IsVisible = (item == b);
					}
				}
			}

			mbiMax.IsEnabled = pnSkill.IsVisible || pnChar.IsVisible || pnInt.IsVisible || pnRel.IsVisible;
			this.miRand.IsEnabled = mbiMax.IsEnabled;
            this.miOpenSCOR.IsEnabled = (int)PathProvider.Global.Latest.Expansion >= (int)Expansions.Business;
		}

        
		
		private void ChoosePage(object sender, System.EventArgs e)
		{
			SelectButton((Button)sender);
		}

        void AddAspiration(ComboBox cb, Data.MetaData.AspirationTypes exclude, Data.MetaData.AspirationTypes asp)
        {
            if ((ushort)exclude == 0xFFFF || asp != exclude)
                cb.Items.Add(new LocalizedAspirationTypes(asp));
        }

        void SetAspirations(ComboBox cb)
        {
            SetAspirations(cb, (Data.MetaData.AspirationTypes)0xffff);
        }

        void SetAspirations(ComboBox cb, Data.MetaData.AspirationTypes exclude)
        {
            
            cb.Items.Clear();
            
            AddAspiration(cb, exclude, Data.MetaData.AspirationTypes.Nothing);
            AddAspiration(cb, exclude, MetaData.AspirationTypes.Fortune);
            AddAspiration(cb, exclude, Data.MetaData.AspirationTypes.Family);
            AddAspiration(cb, exclude, Data.MetaData.AspirationTypes.Knowledge);
            AddAspiration(cb, exclude, Data.MetaData.AspirationTypes.Reputation);
            AddAspiration(cb, exclude, Data.MetaData.AspirationTypes.Romance);
            AddAspiration(cb, exclude, Data.MetaData.AspirationTypes.Growup);
            AddAspiration(cb, exclude, Data.MetaData.AspirationTypes.Pleasure);
            AddAspiration(cb, exclude, Data.MetaData.AspirationTypes.Chees);
        }

        void SelectAspiration(ComboBox cb, Data.MetaData.AspirationTypes val)
        {
            if (cb.Items.Count == 0) return;
            cb.SelectedIndex = 0;
            for (int i = 0; i < cb.Items.Count; i++)
            {
                Data.MetaData.AspirationTypes at = (LocalizedAspirationTypes)cb.Items[i];
                if (at == val)
                {
                    cb.SelectedIndex = i;
                    break;
                }
            }	
        }

		void InitDropDowns()
		{
            SetAspirations(cbaspiration);
            SetAspirations(cbaspiration2);

			
			this.cblifesection.Items.Clear();
			this.cblifesection.Items.Add(new LocalizedLifeSections(Data.MetaData.LifeSections.Unknown));
			this.cblifesection.Items.Add(new LocalizedLifeSections(Data.MetaData.LifeSections.Baby));
			this.cblifesection.Items.Add(new LocalizedLifeSections(Data.MetaData.LifeSections.Toddler));
			this.cblifesection.Items.Add(new LocalizedLifeSections(Data.MetaData.LifeSections.Child));
			this.cblifesection.Items.Add(new LocalizedLifeSections(Data.MetaData.LifeSections.Teen));
			//this.cblifesection.Items.Add(new LocalizedLifeSections(Data.MetaData.LifeSections.YoungAdult));
			this.cblifesection.Items.Add(new LocalizedLifeSections(Data.MetaData.LifeSections.Adult));
			this.cblifesection.Items.Add(new LocalizedLifeSections(Data.MetaData.LifeSections.Elder));

			this.cbcareer.Items.Clear();
			foreach (SimPe.Interfaces.IAlias a in SimPe.PackedFiles.Wrapper.SDesc.AddonCarrers) this.cbcareer.Items.Add(a);
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Unknown));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Unemployed));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Science));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Medical));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Politics));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Athletic));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.LawEnforcement));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Culinary));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Economy));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Slacker));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Criminal));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Military));

            if ((SimPe.PathProvider.Global.EPInstalled >= 1) || (Helper.XmlRegistry.HiddenMode))
            {
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Paranormal));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.NaturalScientist));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.ShowBiz));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Artist));
            }

            if ((SimPe.PathProvider.Global.EPInstalled >= 8) || (Helper.XmlRegistry.HiddenMode))
            {
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Adventurer));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Education));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Gamer));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Journalism));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Law));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Music));
            }
			
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderAthletic));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderBusiness));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderCriminal));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderCulinary));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderLawEnforcement));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderMedical));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderMilitary));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderPolitics));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderScience));
			this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderSlacker));

            if ((SimPe.PathProvider.Global.EPInstalled >= 8) || (Helper.XmlRegistry.HiddenMode))
            {
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderAdventurer));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderEducation));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderGamer));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderJournalism));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderLaw));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderMusic));
            }

            if ((SimPe.PathProvider.Global.EPInstalled >= 6) || (Helper.XmlRegistry.HiddenMode))
            {
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.PetSecurity));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.PetService));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.PetShowBiz));
            }

            if ((SimPe.PathProvider.Global.EPInstalled >= 13) || (Helper.XmlRegistry.HiddenMode))
            {
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Construction));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Dance));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Entertainment));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Intelligence));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.Ocenography));

                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderConstruction));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderDance));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderEntertainment));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderIntelligence));
                this.cbcareer.Items.Add(new LocalizedCareers(Data.MetaData.Careers.TeenElderOcenography));
                
            }
			

			this.cbgrade.Items.Clear();
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.Unknown));
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.APlus));
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.A));
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.AMinus));
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.BPlus));
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.B));
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.BMinus));
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.CPlus));
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.C));
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.CMinus));
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.DPlus));
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.D));
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.DMinus));
			this.cbgrade.Items.Add(new LocalizedGrades(Data.MetaData.Grades.F));			

			this.cbmajor.Items.Clear();
			
			foreach (SimPe.Interfaces.IAlias a in SimPe.PackedFiles.Wrapper.SDesc.AddonMajors) this.cbmajor.Items.Add(a);
			System.Array majors = System.Enum.GetValues(typeof(Data.Majors));
			foreach (Data.Majors c in majors) this.cbmajor.Items.Add(c);

			this.cbschooltype.Items.Clear();
			System.Array schools = System.Enum.GetValues(typeof(Data.MetaData.SchoolTypes));
			foreach (Data.MetaData.SchoolTypes c in schools) this.cbschooltype.Items.Add(new LocalizedSchoolType(c));
			foreach (SimPe.Interfaces.IAlias a in SimPe.PackedFiles.Wrapper.SDesc.AddonSchools) this.cbschooltype.Items.Add(a);

			this.cbzodiac.Items.Clear();
			for (ushort i=0x01; i<=0x0C; i++) this.cbzodiac.Items.Add(new LocalizedZodiacSignes((Data.MetaData.ZodiacSignes)i));

            // cbSpecies.ResourceManager / .Enum: custom WinForms ComboBox properties; no-op in Avalonia

            for (int i = 0; i < cbHobbyEnth.Items.Count; i++)
            {
                SimPe.PackedFiles.Wrapper.Hobbies hb = SimPe.PackedFiles.Wrapper.SdscFreetime.IndexToHobbies((ushort)i);
                Type type = typeof(SimPe.PackedFiles.Wrapper.Hobbies);
                cbHobbyEnth.Items[i] = SimPe.Localization.GetString(type.Namespace + "." + type.Name + "." + hb.ToString());
            }

            // cbHobbyPre.ResourceManager / .Enum: custom WinForms ComboBox properties; no-op in Avalonia
		}

        #region IPackedFileUI Member


        public Wrapper.ExtSDesc Sdesc
		{
			get { return (SimPe.PackedFiles.Wrapper.ExtSDesc)Wrapper; }
		}
		

		public override void RefreshGUI()
		{
			loadedRel = false;
			this.intern = true;
			try 
			{
				base.RefreshGUI ();
		
				miOpenChar.IsEnabled = System.IO.File.Exists(Sdesc.CharacterFileName) && !Sdesc.IsNPC;
				miOpenCloth.IsEnabled = miOpenChar.IsEnabled;
				miRelink.IsEnabled = /*miOpenChar.IsEnabled &&*/ Helper.XmlRegistry.HiddenMode;

				if (System.IO.File.Exists(Sdesc.CharacterFileName))
					miOpenChar.Header = strresources.GetString("miOpenChar.Text")+" ("+System.IO.Path.GetFileNameWithoutExtension(Sdesc.CharacterFileName)+")";
				else
					miOpenChar.Header = strresources.GetString("miOpenChar.Text");

				this.tbsimdescname.IsReadOnly = Sdesc.IsNPC;
				this.tbsimdescfamname.IsReadOnly = this.tbsimdescname.IsReadOnly;

				RefreshSkills(Sdesc);
				RefreshId(Sdesc);
				RefreshCareer(Sdesc);
				RefreshCharcter(Sdesc);
				RefreshInterests(Sdesc);
				RefreshMisc(Sdesc);

				pnRel_VisibleChanged(null, null);

               

				this.biEP1.IsEnabled = (int)Sdesc.Version >= (int)SimPe.PackedFiles.Wrapper.SDescVersions.University;
				this.biEP2.IsEnabled = (int)Sdesc.Version >= (int)SimPe.PackedFiles.Wrapper.SDescVersions.Nightlife;
                this.biEP3.IsEnabled = (int)Sdesc.Version >= (int)SimPe.PackedFiles.Wrapper.SDescVersions.Business;
                this.biEP6.IsEnabled = (int)Sdesc.Version >= (int)SimPe.PackedFiles.Wrapper.SDescVersions.Voyage;
                this.biEP7.IsEnabled = (int)Sdesc.Version >= (int)SimPe.PackedFiles.Wrapper.SDescVersions.Freetime; 
				if (pnEP1.IsVisible && !biEP1.IsEnabled) this.SelectButton(biId);
				if (pnEP2.IsVisible && !biEP2.IsEnabled) this.SelectButton(biId);
                if (pnEP3.IsVisible && !biEP3.IsEnabled) this.SelectButton(biId);
                if (pnVoyage.IsVisible && !biEP6.IsEnabled) this.SelectButton(biId);
                if (pnEP7.IsVisible && !biEP7.IsEnabled) this.SelectButton(biId);

                
				if (biEP1.IsEnabled) RefreshEP1(Sdesc);
                if (biEP2.IsEnabled) RefreshEP2(Sdesc);
                else cbSpecies.SelectedValue = SimPe.PackedFiles.Wrapper.SdscNightlife.SpeciesType.Human;
				if (biEP3.IsEnabled) RefreshEP3(Sdesc);
                RefreshEP4(Sdesc);
                if (biEP6.IsEnabled) RefreshEP6(Sdesc);
                if (biEP7.IsEnabled) RefreshEP7(Sdesc);

                this.cbSpecies.IsEnabled = biEP2.IsEnabled;

                
			} 
			finally 
			{
                SetCharacterAttributesVisibility();
				this.intern = false;
			}
		}

		

		void RefreshEP1(Wrapper.ExtSDesc sdesc)
		{
			this.cbmajor.SelectedIndex = 0;
			this.tbmajor.Text = "0x"+Helper.HexString((uint)sdesc.University.Major);		
			this.tbmajor.IsVisible = Helper.XmlRegistry.HiddenMode;
			this.cbmajor.SelectedIndex = this.cbmajor.Items.Count -1;
			for (int i=0;i<this.cbmajor.Items.Count;i++)
			{					 
				object o = this.cbmajor.Items[i];
				Data.Majors at;
				if (o.GetType()==typeof(Alias)) at = (Data.Majors)((uint)((Alias)o).Id);
				else at = (Data.Majors)o;
					
				if (at==sdesc.University.Major) 
				{
					this.cbmajor.SelectedIndex = i;
					break;
				}
			}

			this.cboncampus.IsChecked = (sdesc.University.OnCampus==0x1);
			this.pbEffort.Value = sdesc.University.Effort;
			this.pbLastGrade.Value = sdesc.University.Grade;

			this.pbUniTime.Value = sdesc.University.Time;
			this.tbinfluence.Text = sdesc.University.Influence.ToString();
			this.tbsemester.Text = sdesc.University.Semester.ToString();
		}

		void RefreshSkills(Wrapper.ExtSDesc sdesc)
		{
			this.pbBody.Value = sdesc.Skills.Body;
			this.pbCharisma.Value = sdesc.Skills.Charisma;
			this.pbClean.Value = sdesc.Skills.Cleaning;
			this.pbCooking.Value = sdesc.Skills.Cooking;
			this.pbCreative.Value = sdesc.Skills.Creativity;
			this.pbFat.Value = sdesc.Skills.Fatness;
			this.pbLogic.Value = sdesc.Skills.Logic;
			this.pbMech.Value = sdesc.Skills.Mechanical;
			this.pbRomance.Value = sdesc.Skills.Romance;
		}

		void RefreshMisc(Wrapper.ExtSDesc sdesc)
		{
			//ghostflags
			this.cbisghost.IsChecked = sdesc.CharacterDescription.GhostFlag.IsGhost;
			this.cbpassobject.IsChecked = sdesc.CharacterDescription.GhostFlag.CanPassThroughObjects;
			this.cbpasswalls.IsChecked = sdesc.CharacterDescription.GhostFlag.CanPassThroughWalls;
			this.cbpasspeople.IsChecked = sdesc.CharacterDescription.GhostFlag.CanPassThroughPeople;
			this.cbignoretraversal.IsChecked = sdesc.CharacterDescription.GhostFlag.IgnoreTraversalCosts;

			//body flags
			this.cbfit.IsChecked = sdesc.CharacterDescription.BodyFlag.Fit;
			this.cbfat.IsChecked = sdesc.CharacterDescription.BodyFlag.Fat;
			this.cbpregfull.IsChecked = sdesc.CharacterDescription.BodyFlag.PregnantFull;
			this.cbpreghalf.IsChecked = sdesc.CharacterDescription.BodyFlag.PregnantHalf;
			this.cbpreginv.IsChecked = sdesc.CharacterDescription.BodyFlag.PregnantHidden;

			//misc
			this.tbprevdays.Text = sdesc.CharacterDescription.PrevAgeDays.ToString();
			this.tbagedur.Text = sdesc.CharacterDescription.AgeDuration.ToString();
			this.tbunlinked.Text = "0x"+Helper.HexString(sdesc.Unlinked);
			this.tbvoice.Text = "0x"+Helper.HexString(sdesc.CharacterDescription.VoiceType);
			this.tbautonomy.Text = "0x"+Helper.HexString(sdesc.CharacterDescription.AutonomyLevel);
			this.tbnpc.Text = "0x"+Helper.HexString(sdesc.CharacterDescription.NPCType);
			tbstatmot.Text = "0x"+Helper.HexString(sdesc.CharacterDescription.MotivesStatic);

            
		}

		void RefreshId(Wrapper.ExtSDesc sdesc)
		{
			this.tbage.Text = sdesc.CharacterDescription.Age.ToString();
			this.tbsimdescname.Text = sdesc.SimName;
			this.tbsimdescfamname.Text = sdesc.SimFamilyName;
			this.tbsim.Text = "0x"+Helper.HexString(sdesc.SimId);
			this.tbsim.IsReadOnly = !Helper.XmlRegistry.HiddenMode;
			this.tbfaminst.Text = "0x"+Helper.HexString(sdesc.FamilyInstance);
			
			System.Drawing.Image img = null;
			
			if (sdesc.Image is System.Drawing.Image sdescImg)
				if (sdescImg.Width>5)
					img = Ambertation.Drawing.GraphicRoutines.KnockoutImage(sdescImg, new Point(0,0), Color.Magenta);

			if (img == null)
                img = Ambertation.Drawing.GraphicRoutines.SKBitmapToGdiImage(
                    Helper.LoadImage(typeof(SimPe.Helper).Assembly.GetManifestResourceStream("SimPe.IconXmlResources.noone.png")));

            var iconSize = new System.Drawing.Size((int)pbImage.Bounds.Width, (int)pbImage.Bounds.Height);
            img = Ambertation.Windows.Forms.Graph.ImagePanel.CreateThumbnail(img, iconSize, 12, Color.FromArgb(90, Color.Black), SimPe.PackedFiles.Wrapper.SimPoolControl.GetImagePanelColor(Sdesc), Color.White, Color.FromArgb(80, Color.White), true, 4, 0);
			this.pbImage.Source = Helper.ToAvaloniaBitmap(Ambertation.Drawing.GraphicRoutines.GdiImageToSKBitmap(img));

			//Lifesection
			this.cblifesection.SelectedIndex = 0;
			for (int i=0;i<this.cblifesection.Items.Count;i++)
			{
				Data.MetaData.LifeSections at = (LocalizedLifeSections)this.cblifesection.Items[i];
				if (at==sdesc.CharacterDescription.LifeSection) 
				{
					this.cblifesection.SelectedIndex = i;
					break;
				}
			}

			this.rbfemale.IsChecked = (sdesc.CharacterDescription.Gender == Data.MetaData.Gender.Female);
			this.rbmale.IsChecked = (sdesc.CharacterDescription.Gender == Data.MetaData.Gender.Male);
		}

		void RefreshCareer(Wrapper.ExtSDesc sdesc)
		{
			this.pbCareerLevel.Value = sdesc.CharacterDescription.CareerLevel;
			this.pbCareerPerformance.Value = sdesc.CharacterDescription.CareerPerformance;

			//Career
			this.tbcareervalue.Text = "0x"+Helper.HexString((uint)sdesc.CharacterDescription.Career);
			this.cbcareer.SelectedIndex = 0;
			for (int i=0;i<this.cbcareer.Items.Count;i++)
			{
				object o = this.cbcareer.Items[i];
				Data.MetaData.Careers at;
				if (o.GetType()==typeof(Alias)) at = (Data.LocalizedCareers)((uint)((Alias)o).Id); 
				else at = (Data.LocalizedCareers)o;
				
				if (at==sdesc.CharacterDescription.Career) 
				{
					this.cbcareer.SelectedIndex = i;
					break;
				}
			}

			//school
			this.cbschooltype.SelectedIndex = 0;
			this.tbschooltype.IsVisible = true;
			this.tbschooltype.IsReadOnly = !Helper.XmlRegistry.HiddenMode;
			for(int i=0; i<this.cbschooltype.Items.Count; i++)
			{
				Data.LocalizedSchoolType type;
				object o = this.cbschooltype.Items[i];
				if (o.GetType()==typeof(Alias)) type = (Data.LocalizedSchoolType)((uint)((Alias)o).Id); 
				else type = (Data.LocalizedSchoolType)o;
				
				if (sdesc.CharacterDescription.SchoolType == (Data.MetaData.SchoolTypes)type) 
				{
					this.cbschooltype.SelectedIndex = i;
					break;
				}
			}

			this.tbschooltype.Text = "0x"+Helper.HexString((uint)sdesc.CharacterDescription.SchoolType);

			//grades and school
			this.cbgrade.SelectedIndex = 0;
			for(int i=0; i<this.cbgrade.Items.Count; i++)
			{
				Data.MetaData.Grades grade;
				object o = this.cbgrade.Items[i];
				if (o.GetType()==typeof(Alias)) grade = (Data.LocalizedGrades)((uint)((Alias)o).Id); 
				else grade = (Data.LocalizedGrades)o;
				if (sdesc.CharacterDescription.Grade == (Data.MetaData.Grades)grade) 
				{
					this.cbgrade.SelectedIndex = i;
					break;
				}
			}

			//Aspiration
			this.pbAspBliz.Value = sdesc.CharacterDescription.BlizLifelinePoints;
			this.pbAspCur.Value = sdesc.CharacterDescription.LifelinePoints;

            SelectAspiration(cbaspiration, sdesc.Freetime.PrimaryAspiration);
					
			this.tblifelinescore.Text = sdesc.CharacterDescription.LifelineScore.ToString();
		}

		void RefreshInterests(Wrapper.ExtSDesc sdesc)
		{
			this.pbAnimals.Value = sdesc.Interests.Animals;
			this.pbCrime.Value = sdesc.Interests.Crime;
			this.pbCulture.Value = sdesc.Interests.Culture;
			this.pbEntertainment.Value = sdesc.Interests.Entertainment;
			this.pbEnvironment.Value = sdesc.Interests.Environment; 
			this.pbFashion.Value = sdesc.Interests.Fashion;
			this.pbFood.Value = sdesc.Interests.Food;
			this.pbHealth.Value = sdesc.Interests.Health;
			this.pbMoney.Value = sdesc.Interests.Money;
			this.pbParanormal.Value = sdesc.Interests.Paranormal;
			this.pbPolitics.Value = sdesc.Interests.Politics;
			this.pbSchool.Value = sdesc.Interests.School;
			this.pbSciFi.Value = sdesc.Interests.Scifi;
			this.pbSports.Value = sdesc.Interests.Sports ;
			this.pbToys.Value = sdesc.Interests.Toys;
			this.pbTravel.Value = sdesc.Interests.Travel;
			this.pbWeather.Value = sdesc.Interests.Weather;
			this.pbWork.Value = sdesc.Interests.Work;

            this.pbPetEating.Value = sdesc.Interests.Environment;
            this.pbPetWeather.Value = sdesc.Interests.Food;
            this.pbPetPlaying.Value = sdesc.Interests.Culture;
            this.pbPetSpooky.Value = sdesc.Interests.Money;
            this.pbPetSleep.Value = sdesc.Interests.Entertainment;
            this.pbPetToy.Value = sdesc.Interests.Health;
            this.pbPetPets.Value = sdesc.Interests.Politics;
            this.pbPetOutside.Value = sdesc.Interests.Crime;
            this.pbPetAnimals.Value = sdesc.Interests.Fashion;
		}

		void RefreshCharcter(Wrapper.ExtSDesc sdesc)
		{
			
			this.cbzodiac.SelectedIndex = ((ushort)sdesc.CharacterDescription.ZodiacSign-1);

			//Character
			this.pbNeat.Value = sdesc.Character.Neat;
			this.pbOutgoing.Value = sdesc.Character.Outgoing;
			this.pbActive.Value = sdesc.Character.Active;
			this.pbPlayful.Value = sdesc.Character.Playful;
			this.pbNice.Value = sdesc.Character.Nice;

			//Genetic Character
			this.pbGNeat.Value = sdesc.GeneticCharacter.Neat;
			this.pbGOutgoing.Value = sdesc.GeneticCharacter.Outgoing;
			this.pbGActive.Value = sdesc.GeneticCharacter.Active;
			this.pbGPlayful.Value = sdesc.GeneticCharacter.Playful;
			this.pbGNice.Value = sdesc.GeneticCharacter.Nice;

            pbWoman.Value = Math.Max(0, Math.Min(2000, sdesc.Interests.FemalePreference + 1000));
            pbMan.Value   = Math.Max(0, Math.Min(2000, sdesc.Interests.MalePreference   + 1000));
        }

		#endregion

		private void Activate_biMax(object sender, System.EventArgs e)
		{			
			if (this.pnSkill.IsVisible) 
			{
				intern = true;
				foreach (var c in pnSkill.Children)
				{
					if (c == this.pbFat) 
					{
						((LabeledProgressBar)c).Value = 0;
					} 
					else if (c is LabeledProgressBar)
					{
						((LabeledProgressBar)c).Value = (int)((LabeledProgressBar)c).Maximum-1;
					}
				}
				intern = false;	this.ChangedSkill(null, null);
			} 
			else if(this.pnChar.IsVisible) 
			{
				intern = true;
				foreach (var c in this.pnHumanChar.Children)
					if (c is LabeledProgressBar)
						((LabeledProgressBar)c).Value = (int)((LabeledProgressBar)c).Maximum;
				intern = false;	this.ChangedChar(null, null);
			}
			else if(this.pnInt.IsVisible) 
			{
				intern = true;
				foreach (var c in this.pnPetInt.Children)
					if (c is LabeledProgressBar)
						((LabeledProgressBar)c).Value = (int)((LabeledProgressBar)c).Maximum;
                foreach (var c in this.pnSimInt.Children)
                    if (c is LabeledProgressBar)
                        ((LabeledProgressBar)c).Value = (int)((LabeledProgressBar)c).Maximum;
				intern = false;	this.ChangedInt(null, null);
			} 
			else if (this.pnRel.IsVisible)
			{
				int index = -1;
				if (lv.SelectedIndices.Count>0)
					index = (int)lv.SelectedIndices[0];
				foreach (SimPe.PackedFiles.Wrapper.XPListViewItem lvi in lv.Items)
				{
					
					if (lvi.GroupIndex!=1) 
					{
						lvi.Selected = true;
                        lv_SelectedSimChanged(lv, null, null);
						if (this.srcRel.Srel!=null) 
						{
							srcRel.Srel.Longterm = 100;
							srcRel.Srel.Shortterm = 100;
							srcRel.Srel.Changed = true;
						}

						if (this.dstRel.Srel!=null) 
						{
							dstRel.Srel.Longterm = 100;
							dstRel.Srel.Shortterm = 100;
							dstRel.Srel.Changed = true;
						}
					}
				}
				if (index>=0) ((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.Items[index]).Selected = true;
				else if (lv.Items.Count>0) ((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.Items[0]).Selected= true;
			}
		}

		private void Activate_biRand(object sender, System.EventArgs e)
		{			
			Random rnd = new Random();
			if (this.pnSkill.IsVisible) 
			{
				intern = true;
				foreach (var c in pnSkill.Children)				
					if (c is LabeledProgressBar)					
						((LabeledProgressBar)c).Value = rnd.Next((int)((LabeledProgressBar)c).Maximum);					
				
				intern = false;	this.ChangedSkill(null, null);
			} 
			else if(this.pnChar.IsVisible) 
			{
				intern = true;
                foreach (var c in pnHumanChar.Children)
					if (c is LabeledProgressBar)
						((LabeledProgressBar)c).Value = rnd.Next((int)((LabeledProgressBar)c).Maximum);
				intern = false;	this.ChangedSkill(null, null);
			}
			else if(this.pnInt.IsVisible) 
			{
				intern = true;
                foreach (var c in pnPetInt.Children)
					if (c is LabeledProgressBar)
						((LabeledProgressBar)c).Value = rnd.Next((int)((LabeledProgressBar)c).Maximum);
                foreach (var c in pnSimInt.Children)
                    if (c is LabeledProgressBar)
                        ((LabeledProgressBar)c).Value = rnd.Next((int)((LabeledProgressBar)c).Maximum);
				intern = false;	this.ChangedSkill(null, null);
			}
			else if (this.pnRel.IsVisible)
			{
				foreach (SimPe.PackedFiles.Wrapper.XPListViewItem lvi in lv.Items)
				{
					
					if (lvi.GroupIndex!=1) 
					{
						lvi.Selected = true;
						int baseval = rnd.Next(200)-100;
						if (this.srcRel.Srel!=null) 
						{
							srcRel.Srel.Longterm = Math.Max(-100, Math.Min(100, baseval + rnd.Next(40)-20));
							srcRel.Srel.Shortterm = Math.Max(-100, Math.Min(100, baseval + rnd.Next(40)-20));
							srcRel.Srel.Changed = true;
						}

						if (this.dstRel.Srel!=null) 
						{
							dstRel.Srel.Longterm = Math.Max(-100, Math.Min(100, baseval + rnd.Next(40)-20));
							dstRel.Srel.Shortterm = Math.Max(-100, Math.Min(100, baseval + rnd.Next(40)-20));
							dstRel.Srel.Changed = true;
						}
					}
				}
				if (lv.Items.Count>0) ((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.Items[0]).Selected= true;
			}
		}		

		private void ExtSDesc_Commited(object sender, System.EventArgs e)
		{
			Sdesc.SynchronizeUserData();
		}

		private void cbmajor_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cbmajor.SelectedIndex<0) return;
			object o = cbmajor.Items[cbmajor.SelectedIndex];
			Data.Majors v;
			if (o.GetType()==typeof(Data.Alias)) v = (Data.Majors)((Data.Alias)o).Id; 
			else v = (Data.Majors)o;
			
			if ( v == Data.Majors.Unknown) return;

			tbmajor.Text = "0x"+Helper.HexString((uint)v);
		}

		private void cbcareer_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cbcareer.SelectedIndex<0) return;
			object o = cbcareer.Items[cbcareer.SelectedIndex];
			if (o.GetType()!=typeof(Data.Alias)) 
			{
				Data.MetaData.Careers career = (Data.LocalizedCareers)o;
				if (career != Data.MetaData.Careers.Unknown) 
				{
					tbcareervalue.Text = "0x"+Helper.HexString((uint)career);
				}
			} 
			else 
			{
				Data.Alias a = (Data.Alias)o;
				tbcareervalue.Text = "0x"+Helper.HexString((uint)a.Id);
			}
		}

		private void cbschooltype_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cbschooltype.SelectedIndex<0) return;
			object o = cbschooltype.Items[cbschooltype.SelectedIndex];
			if (o.GetType()!=typeof(Data.Alias)) 
			{
				Data.MetaData.SchoolTypes st = (Data.LocalizedSchoolType)o;
				if (st != Data.MetaData.SchoolTypes.Unknown) 
				{
					tbschooltype.Text = "0x"+Helper.HexString((uint)st);
				}
			} 
			else 
			{
				Data.Alias a = (Data.Alias)o;
				tbschooltype.Text = "0x"+Helper.HexString((uint)a.Id);
			}
		}

		#region Changing Data
		bool intern;
		protected bool InternalChange
		{
			get {return intern;}
			set {intern = value;}
		}

		private void ChangedId(object sender, System.EventArgs e)
		{
			if (intern) return;
			intern = true;
			try 
			{
				Sdesc.SimId = Helper.StringToUInt32(this.tbsim.Text, Sdesc.SimId, 16);
				Sdesc.FamilyInstance = Helper.StringToUInt16(this.tbfaminst.Text, Sdesc.FamilyInstance, 16);

				Sdesc.CharacterDescription.Age = Helper.StringToUInt16(this.tbage.Text, Sdesc.CharacterDescription.Age, 10);
				if (Sdesc.SimName!=tbsimdescname.Text) Sdesc.SimName = this.tbsimdescname.Text;
				if (Sdesc.SimFamilyName!=tbsimdescfamname.Text) Sdesc.SimFamilyName = this.tbsimdescfamname.Text;
				
				this.tbsim.IsReadOnly = !Helper.XmlRegistry.HiddenMode;
				
			
				
				//Lifesection
				Sdesc.CharacterDescription.LifeSection = (Data.LocalizedLifeSections)this.cblifesection.SelectedItem;
				

				if (this.rbfemale.IsChecked == true) Sdesc.CharacterDescription.Gender = Data.MetaData.Gender.Female;
				else Sdesc.CharacterDescription.Gender = Data.MetaData.Gender.Male;

				Sdesc.Changed = true;
			} 
			finally 
			{
				intern = false;
			}
		}

		private void ChangedRel(object sender, System.EventArgs e)
		{
			if (intern) return;
			intern = true;
			try 
			{
			} 
			finally 
			{
				intern = false;
			}
		}

		private void ChangedInt(object sender, System.EventArgs e)
		{
			if (intern) return;
			intern = true;
			try 
			{
                if (IsHumanoid())
                {
                    Sdesc.Interests.Animals = (ushort)this.pbAnimals.Value;
                    Sdesc.Interests.Crime = (ushort)this.pbCrime.Value;
                    Sdesc.Interests.Culture = (ushort)this.pbCulture.Value;
                    Sdesc.Interests.Entertainment = (ushort)this.pbEntertainment.Value;
                    Sdesc.Interests.Environment = (ushort)this.pbEnvironment.Value;
                    Sdesc.Interests.Fashion = (ushort)this.pbFashion.Value;
                    Sdesc.Interests.Food = (ushort)this.pbFood.Value;
                    Sdesc.Interests.Health = (ushort)this.pbHealth.Value;
                    Sdesc.Interests.Money = (ushort)this.pbMoney.Value;
                    Sdesc.Interests.Paranormal = (ushort)this.pbParanormal.Value;
                    Sdesc.Interests.Politics = (ushort)this.pbPolitics.Value;
                    Sdesc.Interests.School = (ushort)this.pbSchool.Value;
                    Sdesc.Interests.Scifi = (ushort)this.pbSciFi.Value;
                    Sdesc.Interests.Sports = (ushort)this.pbSports.Value;
                    Sdesc.Interests.Toys = (ushort)this.pbToys.Value;
                    Sdesc.Interests.Travel = (ushort)this.pbTravel.Value;
                    Sdesc.Interests.Weather = (ushort)this.pbWeather.Value;
                    Sdesc.Interests.Work = (ushort)this.pbWork.Value;
                }
                else
                {
                    Sdesc.Interests.Environment = (ushort)this.pbPetEating.Value;
                    Sdesc.Interests.Food = (ushort)this.pbPetWeather.Value;
                    Sdesc.Interests.Culture = (ushort)this.pbPetPlaying.Value;
                    Sdesc.Interests.Money = (ushort)this.pbPetSpooky.Value;
                    Sdesc.Interests.Entertainment = (ushort)this.pbPetSleep.Value;
                    Sdesc.Interests.Health = (ushort)this.pbPetToy.Value;
                    Sdesc.Interests.Politics = (ushort)this.pbPetPets.Value;
                    Sdesc.Interests.Crime = (ushort)this.pbPetOutside.Value;
                    Sdesc.Interests.Fashion = (ushort)this.pbPetAnimals.Value;
                }

				Sdesc.Changed = true;
			} 
			finally 
			{
				intern = false;
			}
		}

		private void ChangedCareer(object sender, System.EventArgs e)
		{
			if (intern) return;
			intern = true;
			try 
			{
				Sdesc.CharacterDescription.CareerLevel = (ushort)this.pbCareerLevel.Value;
				Sdesc.CharacterDescription.CareerPerformance = (short)this.pbCareerPerformance.Value;

				//Career
				Sdesc.CharacterDescription.Career = (SimPe.Data.MetaData.Careers)Helper.StringToUInt32(this.tbcareervalue.Text, (uint)Sdesc.CharacterDescription.Career, 16);
				
				//school
				Sdesc.CharacterDescription.SchoolType = (SimPe.Data.MetaData.SchoolTypes)Helper.StringToUInt32(this.tbschooltype.Text, (uint)Sdesc.CharacterDescription.SchoolType, 16);

				//grades and school
				Sdesc.CharacterDescription.Grade = (Data.LocalizedGrades)cbgrade.SelectedItem;
				

				//Aspiration
				Sdesc.CharacterDescription.BlizLifelinePoints = (ushort)this.pbAspBliz.Value;
				Sdesc.CharacterDescription.LifelinePoints = (short)this.pbAspCur.Value;

			
				Sdesc.Freetime.PrimaryAspiration = (LocalizedAspirationTypes)this.cbaspiration.SelectedItem;				
				Sdesc.CharacterDescription.LifelineScore = Helper.StringToUInt32(this.tblifelinescore.Text, (uint)Sdesc.CharacterDescription.LifelineScore, 10);

				Sdesc.Changed = true;
			} 
			finally 
			{
				intern = false;
			}
		}

		private void ChangedChar(object sender, System.EventArgs e)
		{
			if (intern) return;
			intern = true;
			try 
			{
				Sdesc.CharacterDescription.ZodiacSign = (Data.MetaData.ZodiacSignes)(this.cbzodiac.SelectedIndex+1);

				//Character
				Sdesc.Character.Neat = (ushort)this.pbNeat.Value;
				Sdesc.Character.Outgoing = (ushort)this.pbOutgoing.Value;
				Sdesc.Character.Active = (ushort)this.pbActive.Value;
				Sdesc.Character.Playful = (ushort)this.pbPlayful.Value;
				Sdesc.Character.Nice = (ushort)this.pbNice.Value;

				//Genetic Character
				Sdesc.GeneticCharacter.Neat = (ushort)this.pbGNeat.Value;
				Sdesc.GeneticCharacter.Outgoing = (ushort)this.pbGOutgoing.Value;
				Sdesc.GeneticCharacter.Active = (ushort)this.pbGActive.Value;
				Sdesc.GeneticCharacter.Playful = (ushort)this.pbGPlayful.Value;
				Sdesc.GeneticCharacter.Nice = (ushort)this.pbGNice.Value;

                Sdesc.Interests.FemalePreference = (short)(pbWoman.Value - 1000);
                Sdesc.Interests.MalePreference   = (short)(pbMan.Value   - 1000);

                Sdesc.Changed = true;
			} 
			finally 
			{
				intern = false;
			}
		}

		private void ChangedSkill(object sender, System.EventArgs e)
		{
			if (intern) return;
			intern = true;
			try 
			{
				Sdesc.Skills.Body = (ushort)this.pbBody.Value;
				Sdesc.Skills.Charisma = (ushort)this.pbCharisma.Value;
				Sdesc.Skills.Cleaning = (ushort)this.pbClean.Value;
				Sdesc.Skills.Cooking = (ushort)this.pbCooking.Value;
				Sdesc.Skills.Creativity = (ushort)this.pbCreative.Value;
				Sdesc.Skills.Fatness = (ushort)this.pbFat.Value;
				Sdesc.Skills.Logic = (ushort)this.pbLogic.Value;
				Sdesc.Skills.Mechanical = (ushort)this.pbMech.Value;
				Sdesc.Skills.Romance = (ushort)this.pbRomance.Value;
				Sdesc.Changed = true;
			} 
			finally 
			{
				intern = false;
			}
		}

		private void ChangedOther(object sender, System.EventArgs e)
		{
			if (intern) return;
			intern = true;
			try 
			{
				//ghostflags
				Sdesc.CharacterDescription.GhostFlag.IsGhost = this.cbisghost.IsChecked == true;
				Sdesc.CharacterDescription.GhostFlag.CanPassThroughObjects = this.cbpassobject.IsChecked == true;
				Sdesc.CharacterDescription.GhostFlag.CanPassThroughWalls = this.cbpasswalls.IsChecked == true;
				Sdesc.CharacterDescription.GhostFlag.CanPassThroughPeople = this.cbpasspeople.IsChecked == true;
				Sdesc.CharacterDescription.GhostFlag.IgnoreTraversalCosts = this.cbignoretraversal.IsChecked == true;

				//body flags
				Sdesc.CharacterDescription.BodyFlag.Fit = this.cbfit.IsChecked == true;
				Sdesc.CharacterDescription.BodyFlag.Fat = this.cbfat.IsChecked == true;
				Sdesc.CharacterDescription.BodyFlag.PregnantFull = this.cbpregfull.IsChecked == true;
				Sdesc.CharacterDescription.BodyFlag.PregnantHalf = this.cbpreghalf.IsChecked == true;
				Sdesc.CharacterDescription.BodyFlag.PregnantHidden = this.cbpreginv.IsChecked == true;

				//misc
				Sdesc.CharacterDescription.PrevAgeDays = Helper.StringToUInt16(this.tbprevdays.Text, Sdesc.CharacterDescription.PrevAgeDays, 10);
				Sdesc.CharacterDescription.AgeDuration = Helper.StringToUInt16(this.tbagedur.Text, Sdesc.CharacterDescription.AgeDuration, 10);
				Sdesc.Unlinked = Helper.StringToUInt16(this.tbunlinked.Text, Sdesc.Unlinked, 16);
				Sdesc.CharacterDescription.VoiceType = Helper.StringToUInt16(this.tbvoice.Text, Sdesc.CharacterDescription.VoiceType, 16);
				Sdesc.CharacterDescription.AutonomyLevel = Helper.StringToUInt16(this.tbautonomy.Text, Sdesc.CharacterDescription.AutonomyLevel, 16);
				Sdesc.CharacterDescription.NPCType = Helper.StringToUInt16(this.tbnpc.Text, Sdesc.CharacterDescription.NPCType, 16);
				Sdesc.CharacterDescription.MotivesStatic = Helper.StringToUInt16(this.tbstatmot.Text, Sdesc.CharacterDescription.MotivesStatic, 16);

                
				Sdesc.Changed = true;
			} 
			finally 
			{
				intern = false;
			}
		}

		private void ChangedEP1(object sender, System.EventArgs e)
		{
			if (intern) return;
			intern = true;
			try 
			{				
				Sdesc.University.Major = (Data.Majors)Helper.StringToUInt32(this.tbmajor.Text, (uint)Sdesc.University.Major, 16);						
				
				if (this.cboncampus.IsChecked == true) Sdesc.University.OnCampus=0x1;
				else Sdesc.University.OnCampus=0x0;

				Sdesc.University.Effort = (ushort)this.pbEffort.Value;
				Sdesc.University.Grade = (ushort)this.pbLastGrade.Value;

				Sdesc.University.Time = (ushort)this.pbUniTime.Value;
				Sdesc.University.Influence = Helper.StringToUInt16(this.tbinfluence.Text, Sdesc.University.Influence, 10);
				Sdesc.University.Semester = Helper.StringToUInt16(this.tbsemester.Text, Sdesc.University.Semester, 10);

				Sdesc.Changed = true;
			} 
			finally 
			{
				intern = false;
			}
		}

		#endregion
	

		#region More Menu
		private void Activate_miMore(object sender, System.EventArgs e)
		{
			SdscExtendedForm.Execute(this.Sdesc);
		}

		private void Activate_biMore(object sender, System.EventArgs e)
		{
			// biMore.Content / mbiLink.Show not available in Avalonia; no-op
		}

		private void Activate_miRelink(object sender, System.EventArgs e)
		{
			this.tbsim.Text = "0x"+Helper.HexString(SimRelinkForm.Execute(Sdesc));
		}

		private void Activate_miOpenCHar(object sender, System.EventArgs e)
		{
			try 
			{
				SimPe.RemoteControl.OpenPackage(Sdesc.CharacterFileName);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(ex);
			}
		}

		private void Activate_miOpenCloth(object sender, System.EventArgs e)
		{
			try 
			{				
				if (System.IO.File.Exists(Sdesc.CharacterFileName)) 
				{
					uint inst = Convert.ToUInt32(this.tbfaminst.Text, 16);					
					SimPe.Packages.GeneratableFile fl = SimPe.Packages.GeneratableFile.LoadFromFile(Sdesc.CharacterFileName);

					Interfaces.Files.IPackedFileDescriptor[] pfds = fl.FindFile(0xAC506764, 0, 0x1);
					if (pfds.Length>0) 
					{
						SimPe.RemoteControl.OpenPackage(Sdesc.CharacterFileName);						
						SimPe.RemoteControl.OpenPackedFile(pfds[0], fl);
					}
				}
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(ex);
			}
		}

		private void Activate_miFamily(object sender, System.EventArgs e)
		{
			try 
			{
				uint inst = Convert.ToUInt32(this.tbfaminst.Text, 16);
				

				Interfaces.Files.IPackedFileDescriptor pfd = Sdesc.Package.NewDescriptor(0x46414D49, Sdesc.FileDescriptor.SubType, Sdesc.FileDescriptor.Group, inst); //try a Fami File
				pfd = Sdesc.Package.FindFile(pfd);
				SimPe.RemoteControl.OpenPackedFile(pfd, Sdesc.Package);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(ex);
			}
		}

		private void Activate_miOpenWf(object sender, System.EventArgs e)
		{
			try 
			{
				//Open File
				Interfaces.Files.IPackedFileDescriptor pfd = Sdesc.Package.NewDescriptor(0xCD95548E, Sdesc.FileDescriptor.SubType, Sdesc.FileDescriptor.Group, Sdesc.FileDescriptor.Instance); //try a W&f File
				pfd = Sdesc.Package.FindFile(pfd);
				SimPe.RemoteControl.OpenPackedFile(pfd, Sdesc.Package);
			}
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(ex);
			}
		}

        private void Activate_miOpenMem(object sender, System.EventArgs e)
        {
            try
            {
               
                // Open the NGBH (Memories/Badges) resource in the SAME package as the selected SDesc.
                // This avoids reloading the whole neighborhood package and blowing away the Sim Description UI.
                SimPe.Interfaces.Files.IPackedFileDescriptor pfd =
                    Sdesc.Package.NewDescriptor(0x4E474248, 0, Data.MetaData.LOCAL_GROUP, 1);

                pfd = Sdesc.Package.FindFile(pfd);
                if (pfd == null)
                {
                    SimPe.Message.Show(
                        "Memories (NGBH) not found in the current neighborhood package.",
                        "Memories",
                        MessageBoxButtons.OK);
                    return;
                }
               
                SimPe.RemoteControl.OpenPackedFile(pfd, Sdesc.Package);
                
                // Tell the NGBH UI which Sim (instance) to select, and that we want the Sims slot (memories).
                object[] data = new object[] { Sdesc.FileDescriptor.Instance, Data.NeighborhoodSlots.Sims };
                SimPe.RemoteControl.AddMessage(this, new SimPe.RemoteControl.ControlEventArgs(0x4E474248, data));
                
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage(ex);
            }
        }
        private void Activate_miOpenBadge(object sender, System.EventArgs e)
		{
			try 
			{
				//Open File
				Interfaces.Files.IPackedFileDescriptor pfd = Sdesc.Package.NewDescriptor(0x4E474248, 0, Data.MetaData.LOCAL_GROUP, 1); //try the memory Resource
				pfd = Sdesc.Package.FindFile(pfd);				
				SimPe.RemoteControl.OpenPackedFile(pfd, Sdesc.Package);				

				object[] data = new object[] {Sdesc.FileDescriptor.Instance, Data.NeighborhoodSlots.SimsIntern}; 
				SimPe.RemoteControl.AddMessage(this, new SimPe.RemoteControl.ControlEventArgs(0x4E474248, data));
			}
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(ex);
			}
		}

		private void Activate_miOpenDNA(object sender, System.EventArgs e)
		{
			try 
			{
				Interfaces.Files.IPackedFileDescriptor pfd = Sdesc.Package.NewDescriptor(0xEBFEE33F, Sdesc.FileDescriptor.SubType, Sdesc.FileDescriptor.Group, Sdesc.FileDescriptor.Instance); //try a DNA File
				pfd = Sdesc.Package.FindFile(pfd);
				SimPe.RemoteControl.OpenPackedFile(pfd, Sdesc.Package);
			} 
			catch (Exception ex) 
			{
				Helper.ExceptionMessage(ex);
			}
		}
		#endregion

		#region Relations
		bool loadedRel;
		SimPe.Interfaces.Files.IPackageFile lastpkg;
        private PackedFiles.Wrapper.ExtSDesc pinnedSubject = null;
        private PackedFiles.Wrapper.ExtSDesc pinnedTarget = null;

        private void pnRel_VisibleChanged(object sender, System.EventArgs e)
        {
            if (!pnRel.IsVisible) return;
            if (Sdesc == null) return;

            // If the neighborhood package changed, reload the relation control�s content
            if (lastpkg == null || !lastpkg.Equals(Sdesc.Package))
            {
                LoadRelList(); // (your existing method)
            }
            else
            {
                // Pin the subject when the Relations panel is shown.
                // If the subject changed, clear the target and blank the panels.
                var newSubject = (PackedFiles.Wrapper.ExtSDesc)Sdesc;
                if (pinnedSubject == null || !pinnedSubject.Equals(newSubject))
                {
                    pinnedSubject = newSubject;
                    pinnedTarget = null;

                    lv.Sim = pinnedSubject;   // bind list to pinned subject
                    ResetLabel();             // clears srcRel/dstRel + labels/icons
                }

            }

            // Force the household dropdown to the currently selected sim�s household
            // (otherwise SimPoolControl defaults to the provider's "current"/first household, e.g. Burb)
            lv.SelectHousehold(Sdesc.HouseholdName);

            // Now build the list immediately (don�t wait for dropdown events)
            lv.UpdateSimList();

            lastpkg = Sdesc.Package;
            loadedRel = true;
        }

        void LoadRelList()
        {
            if (Sdesc == null)
            {
                lv.Package = null;
                lv.Sim = null;
                return;
            }

            lv.Package = Sdesc.Package;
            lv.Sim = Sdesc;

            // Force initial population (otherwise it waits for the dropdown event)
            lv.UpdateSimList();

            // Optional: default the dropdown to the current sim�s household
            // lv.SelectHousehold(Sdesc.HouseholdName);

            ResetLabel();
            loadedRel = true;

            /*lv.BeginUpdate();
			Wait.SubStart(FileTable.ProviderRegistry.SimDescriptionProvider.SimGuidMap.Count);
			Wait.Message = "Loading Relations";
			lv.Clear();	
			int ct = 0;
			ArrayList inst = new ArrayList();
			foreach (PackedFiles.Wrapper.ExtSDesc sdesc in FileTable.ProviderRegistry.SimDescriptionProvider.SimGuidMap.Values)
			{				
				inst.Add((ushort)sdesc.FileDescriptor.Instance);
				SimPe.PackedFiles.Wrapper.XPListViewItem lvi = lv.Add(sdesc);
				if (Sdesc.HasRelationWith(sdesc)) lvi.GroupIndex=0;
				else if (sdesc.IsNPC) lvi.GroupIndex=3;
				else if (sdesc.IsTownie) lvi.GroupIndex=4;
				else lvi.GroupIndex=1;

				lvi.Tag = sdesc;
				Wait.Progress = ct++;					
			}

			AddUnknownToRelList(inst);

			
								
			
			lv.EndUpdate();
			Wait.SubStop();	

			ResetLabel();
			loadedRel = true;
			lv.Sort();*/
        }

		void UpdateRelList()
		{
            lv.Sim = Sdesc;
			/*ArrayList inst = new ArrayList();
			for (int i=lv.Items.Count-1; i>=0; i--)
			{
				SimPe.PackedFiles.Wrapper.XPListViewItem lvi = lv.Items[i];
				PackedFiles.Wrapper.ExtSDesc sdesc = (PackedFiles.Wrapper.ExtSDesc)lvi.Tag;
				if (lvi.GroupIndex==2) 
				{
					lv.Items.Remove(lvi);
					continue;
				}
				
				inst.Add((ushort)sdesc.FileDescriptor.Instance);
				if (Sdesc.HasRelationWith(sdesc)) lvi.GroupIndex=0;
				else if (sdesc.IsNPC) lvi.GroupIndex=3;
				else if (sdesc.IsTownie) lvi.GroupIndex=4;
				else lvi.GroupIndex=1;
			}

			AddUnknownToRelList(inst);
			ResetLabel();
			loadedRel = true;
			lv.Sort();*/
		}
		
		void AddUnknownToRelList(ArrayList insts)
		{
			foreach (ushort inst in Sdesc.Relations.SimInstances)
			{
				if (!insts.Contains(inst))
				{
					PackedFiles.Wrapper.ExtSDesc sdesc = new SimPe.PackedFiles.Wrapper.ExtSDesc();
					sdesc.FileDescriptor = Sdesc.Package.NewDescriptor(Data.MetaData.SIM_DESCRIPTION_FILE, 0, Sdesc.FileDescriptor.Group, inst);
					sdesc.Package = Sdesc.Package;
					SimPe.PackedFiles.Wrapper.XPListViewItem lvi = lv.Add(sdesc);
					lvi.GroupIndex=2;					

					lvi.Tag = sdesc;					
				}
			}
		}

        void ResetLabel()
        {
            this.srcRel.Srel = null;
            this.dstRel.Srel = null;
            UpdateLabel();
        }



        void UpdateLabel()
		{
			System.Drawing.Image img = null;
			//srcTb.HeaderText = srcRel.SourceSimName + " " + SimPe.Localization.GetString("towards") +" " +srcRel.TargetSimName; // no HeaderText in Avalonia Border
			if (srcRel.TargetSim==null)img  = null;
			else  img = (System.Drawing.Image)srcRel.Image;
			if (img!=null) 
			{
				//img = Ambertation.Drawing.GraphicRoutines.KnockoutImage(img, new Point(0,0), Color.Magenta);
				//img = Ambertation.Drawing.GraphicRoutines.ScaleImage(img, srcTb.IconSize.Width, srcTb.IconSize.Height, true); // Border has no IconSize in Avalonia
			}
			//srcTb.Icon = img; // Border has no Icon in Avalonia
			

			//dstTb.HeaderText = dstRel.SourceSimName + " " + SimPe.Localization.GetString("towards") +" " +dstRel.TargetSimName; // no HeaderText in Avalonia Border
			if (dstRel.TargetSim==null) img = null;
			else img = (System.Drawing.Image)dstRel.Image.Clone();
			if (img!=null) 
			{
				//img = Ambertation.Drawing.GraphicRoutines.KnockoutImage(img, new Point(0,0), Color.Magenta);
				//img = Ambertation.Drawing.GraphicRoutines.ScaleImage(img, srcTb.IconSize.Width, srcTb.IconSize.Height, true); // Border has no IconSize in Avalonia
			}
			//dstTb.Icon = img; // Border has no Icon in Avalonia
		}

		SimPe.PackedFiles.Wrapper.ExtSrel FindRelation(PackedFiles.Wrapper.ExtSDesc src, PackedFiles.Wrapper.ExtSDesc dst)
		{
            return SimPe.PackedFiles.Wrapper.ExtSDesc.FindRelation(pinnedSubject, src, dst);
        }

		void DiplayRelation(PackedFiles.Wrapper.ExtSDesc src, PackedFiles.Wrapper.ExtSDesc dst, CommonSrel c)
		{
			if (src.Equals(dst) && (c==dstRel || !Helper.XmlRegistry.HiddenMode)) 
			{
				c.Srel = null;
			} 
			else 
			{
				SimPe.PackedFiles.Wrapper.ExtSrel srel = FindRelation(src, dst);			
				c.Srel = srel;
				Sdesc.AddRelationToCache(srel);
			}
		}

        void lv_SelectedSimChanged(object sender, System.Drawing.Image thumb, SimPe.PackedFiles.Wrapper.SDesc sdesc)
        {
            SelectedSimRelationChanged(sender, null);
        }

        private void SelectedSimRelationChanged(object sender, System.EventArgs e)
        {
            if (lv.SelectedItems.Count != 1) return;
            if (pinnedSubject == null) return;

            pinnedTarget = (PackedFiles.Wrapper.ExtSDesc)((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).Tag;

            DiplayRelation(pinnedSubject, pinnedTarget, srcRel);
            DiplayRelation(pinnedTarget, pinnedSubject, dstRel);

            UpdateLabel();
        }

        private void miRel_BeforePopup(object sender, System.EventArgs e)
		{
			if (lv.SelectedItems.Count==1) 
			{
				if (Helper.XmlRegistry.HiddenMode)
					this.miAddRelation.IsEnabled = ((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).GroupIndex==1;
				else
					this.miAddRelation.IsEnabled = ((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).GroupIndex==1 && !Sdesc.Equals(((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).Tag);

				this.miRemRelation.IsEnabled = ((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).GroupIndex!=1;
			
				string name = SimPe.Localization.GetString("AddRelationCaption").Replace("{name}", ((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).Text);
				this.miAddRelation.Header = name;

				name = SimPe.Localization.GetString("RemoveRelationCaption").Replace("{name}", ((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).Text);
				this.miRemRelation.Header = name;

				name = SimPe.Localization.GetString("Max Relation to this Sim").Replace("{name}", ((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).Text);
				this.mbiMaxThisRel.Header = name;
				this.mbiMaxThisRel.IsEnabled = this.miRemRelation.IsEnabled;

				this.mbiMaxKnownRel.IsEnabled = true;
			} 
			else 
			{
				this.miAddRelation.IsEnabled = false;
				this.miRemRelation.IsEnabled = false;
				this.mbiMaxThisRel.IsEnabled = false;
				this.mbiMaxKnownRel.IsEnabled = true;

				string name = SimPe.Localization.GetString("AddRelationCaption").Replace("{name}", SimPe.Localization.GetString("Unknown"));
				this.miAddRelation.Header = name;

				name = SimPe.Localization.GetString("RemoveRelationCaption").Replace("{name}", SimPe.Localization.GetString("Unknown"));
				this.miRemRelation.Header = name;
			}
		}

		private void Activate_miAddRelation(object sender, System.EventArgs e)
		{
			if (lv.SelectedItems.Count!=1) return;
			PackedFiles.Wrapper.ExtSDesc sdesc = (PackedFiles.Wrapper.ExtSDesc)((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).Tag;

			SimPe.PackedFiles.Wrapper.ExtSrel srel = FindRelation(Sdesc, sdesc);
			if (srel==null) srel = Sdesc.CreateRelation(sdesc);
			Sdesc.AddRelationToCache(srel);
			Sdesc.AddRelation(sdesc);

			srel = FindRelation(sdesc, Sdesc);
			if (srel==null) srel = sdesc.CreateRelation(Sdesc);
			Sdesc.AddRelationToCache(srel);
			sdesc.AddRelation(Sdesc);

			((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).GroupIndex=0;
			lv.EnsureVisible(((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).Index);
            lv.UpdateIcon();
			SelectedSimRelationChanged(lv, null);
		}

		private void Activate_miRemRelation(object sender, System.EventArgs e)
		{
			if (lv.SelectedItems.Count!=1) return;
			PackedFiles.Wrapper.ExtSDesc sdesc = (PackedFiles.Wrapper.ExtSDesc)((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).Tag;

			SimPe.PackedFiles.Wrapper.ExtSrel srel = FindRelation(Sdesc, sdesc);
			if (srel!=null) Sdesc.RemoveRelationFromCache(srel);				
			Sdesc.RemoveRelation(sdesc);
			

			srel = FindRelation(sdesc, Sdesc);
			if (srel!=null) Sdesc.RemoveRelationFromCache(srel);
			sdesc.RemoveRelation(Sdesc);
			

			if (((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).GroupIndex==2)
				lv.Items.Remove((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]);
			else 			
				((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).GroupIndex=1;
			
			lv.EnsureVisible(((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.SelectedItems[0]).Index);
            lv.UpdateIcon();
			SelectedSimRelationChanged(lv, null);
		}

		private void Activate_mbiMaxThisRel(object sender, System.EventArgs e)
		{
			foreach (SimPe.PackedFiles.Wrapper.XPListViewItem lvi in lv.SelectedItems)
			{					
				if (lvi.GroupIndex!=1) 
				{
					if (this.srcRel.Srel!=null) 
					{
						srcRel.Srel.Longterm = 100;
						srcRel.Srel.Shortterm = 100;
						srcRel.Srel.Changed = true;						
					}

					if (this.dstRel.Srel!=null) 
					{						
						dstRel.Srel.Longterm = 100;
						dstRel.Srel.Shortterm = 100;
						dstRel.Srel.Changed = true;					
					}
				}
			}	
		
			this.SelectedSimRelationChanged(lv, null);
		}

		private void Activate_mbiMaxKnownRel(object sender, System.EventArgs e)
		{
			int index = -1;
			if (lv.SelectedIndices.Count>0)
				index = (int)lv.SelectedIndices[0];
			foreach (SimPe.PackedFiles.Wrapper.XPListViewItem lvi in lv.Items)
			{					
				if (lvi.GroupIndex!=1) 
				{
					lvi.Selected = true;
                    this.lv_SelectedSimChanged(lv, null, null);
					if (this.srcRel.Srel!=null) 
					{
						if (srcRel.Srel.RelationState.IsKnown) 
						{
							srcRel.Srel.Longterm = 100;
							srcRel.Srel.Shortterm = 100;
							srcRel.Srel.Changed = true;
						}
					}

					if (this.dstRel.Srel!=null) 
					{
						if (dstRel.Srel.RelationState.IsKnown) 
						{
							dstRel.Srel.Longterm = 100;
							dstRel.Srel.Shortterm = 100;
							dstRel.Srel.Changed = true;
						}
					}
				}
			}

			if (index>=0) ((SimPe.PackedFiles.Wrapper.XPListViewItem)lv.Items[index]).Selected = true;
		}
		#endregion

		#region Nightlife
		void FillNightlifeListBox(ListBox clb)
		{
			if (clb.ItemCount > 0) return;

			SimPe.Providers.TraitAlias[] al = FileTable.ProviderRegistry.SimDescriptionProvider.GetAllTurnOns();
			foreach (SimPe.Providers.TraitAlias a in al)
				clb.Items.Add(a);
		}

		void SelectNightlifeItems(Avalonia.Controls.ListBox clb, ushort v1, ushort v2, ushort v3)
		{
			FillNightlifeListBox(clb);

			ulong cur = FileTable.ProviderRegistry.SimDescriptionProvider.BuildTurnOnIndex(v1, v2, v3);
			for (int i=0; i<clb.Items.Count; i++)
			{
				ulong val = ((SimPe.Providers.TraitAlias)clb.Items[i]).Id;
				// clb.SetItemChecked: Avalonia ListBox has no checked items; selection used as proxy
			}
		}

		void RefreshEP2(Wrapper.ExtSDesc sdesc)
		{
			SelectNightlifeItems(this.lbTraits, sdesc.Nightlife.AttractionTraits1, sdesc.Nightlife.AttractionTraits2, sdesc.Nightlife.AttractionTraits3);
			SelectNightlifeItems(this.lbTurnOn, sdesc.Nightlife.AttractionTurnOns1, sdesc.Nightlife.AttractionTurnOns2, sdesc.Nightlife.AttractionTurnOns3);
			SelectNightlifeItems(this.lbTurnOff, sdesc.Nightlife.AttractionTurnOffs1, sdesc.Nightlife.AttractionTurnOffs2, sdesc.Nightlife.AttractionTurnOffs3);

			this.tbNTPerfume.Text = sdesc.Nightlife.PerfumeDuration.ToString();
			this.tbNTLove.Text = sdesc.Nightlife.LovePotionDuration.ToString();
            cbSpecies.SelectedValue = sdesc.Nightlife.Species;

        }

        ulong SumSelection(Avalonia.Controls.ListBox clb, System.EventArgs e)
		{
			ulong val = 0;
            var selectedItems = clb.SelectedItems;
            foreach (object item in (selectedItems != null ? selectedItems : (System.Collections.IList)new object[0]))
                val += ((SimPe.Providers.TraitAlias)item).Id;

			return val;
		}

        void cklb_ItemCheck(object sender, System.EventArgs e)
        {
            if (intern) return;

            int which = (new System.Collections.Generic.List<Avalonia.Controls.ListBox>(new Avalonia.Controls.ListBox[] { lbTraits, lbTurnOn, lbTurnOff })).IndexOf((Avalonia.Controls.ListBox)sender);

            ushort[] v = FileTable.ProviderRegistry.SimDescriptionProvider.GetFromTurnOnIndex(SumSelection((Avalonia.Controls.ListBox)sender, e));
            switch (which)
            {
                case 0:
                    Sdesc.Nightlife.AttractionTraits1 = v[0];
                    Sdesc.Nightlife.AttractionTraits2 = v[1];
                    Sdesc.Nightlife.AttractionTraits3 = v[2];
                    break;
                case 1:
                    Sdesc.Nightlife.AttractionTurnOns1 = v[0];
                    Sdesc.Nightlife.AttractionTurnOns2 = v[1];
                    Sdesc.Nightlife.AttractionTurnOns3 = v[2];
                    break;
                case 2:
                    Sdesc.Nightlife.AttractionTurnOffs1 = v[0];
                    Sdesc.Nightlife.AttractionTurnOffs2 = v[1];
                    Sdesc.Nightlife.AttractionTurnOffs3 = v[2];
                    break;
            }
        }

        private void ChangedEP2(object sender, System.EventArgs e)
        {
            if (intern) return;
            intern = true;
            try
            {
                Sdesc.Nightlife.PerfumeDuration = Helper.StringToUInt16(this.tbNTPerfume.Text, Sdesc.Nightlife.PerfumeDuration, 10);
                Sdesc.Nightlife.LovePotionDuration = Helper.StringToUInt16(this.tbNTLove.Text, Sdesc.Nightlife.LovePotionDuration, 10);
                Sdesc.Nightlife.Species = (SimPe.PackedFiles.Wrapper.SdscNightlife.SpeciesType)cbSpecies.SelectedValue;

                Sdesc.Changed = true;
            }
            finally
            {
                intern = false;
            }
        }

        #endregion

        #region Bon Voyage
        void ShowAllCollectibles()
        {
            if (lvCollectibles.Items.Count > 0) return;
            SimPe.Providers.CollectibleAlias[] al = FileTable.ProviderRegistry.SimDescriptionProvider.GetAllCollectibles();
            foreach (SimPe.Providers.CollectibleAlias a in al)
            {
                //ilCollectibles.Images.Add(a.Image); // ImageList not available in Avalonia
                SimPe.PackedFiles.Wrapper.XPListViewItem lvi = new SimPe.PackedFiles.Wrapper.XPListViewItem();
                lvi.Text = a.ToString();
                lvi.Tag = a;
                lvCollectibles.Items.Add(lvi);
            }
        }


        void RefreshEP6(Wrapper.ExtSDesc sdesc)
        {
            ShowAllCollectibles();
            tbhdaysleft.Text = sdesc.Voyage.DaysLeft.ToString();

            foreach (var item in lvCollectibles.Items){
                SimPe.PackedFiles.Wrapper.XPListViewItem lvi = item as SimPe.PackedFiles.Wrapper.XPListViewItem;
                if (lvi == null) continue;
                SimPe.Providers.CollectibleAlias a = (SimPe.Providers.CollectibleAlias)lvi.Tag;
                lvi.IsChecked = (a.Id & sdesc.Voyage.CollectiblesPlain) == a.Id;
            }
        }

        private void ChangedEP6(object sender, System.EventArgs e)
        {
            if (intern) return;
            intern = true;
            try
            {
                if ((int)Sdesc.Version >= (int)SimPe.PackedFiles.Wrapper.SDescVersions.Voyage)
                {
                    Sdesc.Voyage.CollectiblesPlain = 0;
                    Sdesc.Voyage.DaysLeft = Helper.StringToUInt16(tbhdaysleft.Text, Sdesc.Voyage.DaysLeft, 10);
                    foreach (var item in lvCollectibles.Items)
                    {
                        SimPe.PackedFiles.Wrapper.XPListViewItem lvi = item as SimPe.PackedFiles.Wrapper.XPListViewItem;
                        if (lvi == null) continue;
                        SimPe.Providers.CollectibleAlias a = (SimPe.Providers.CollectibleAlias)lvi.Tag;
                        if (lvi.IsChecked) Sdesc.Voyage.CollectiblesPlain = Sdesc.Voyage.CollectiblesPlain | a.Id;
                    }

                    Sdesc.Changed = true;
                }
            }
            finally
            {
                intern = false;
            }
        }
        #endregion

        void RefreshEP3(Wrapper.ExtSDesc sdesc)
		{
			this.tbEp3Flag.Text = Helper.MinStrLength(Convert.ToString(sdesc.Business.Flags, 2), 16);
			this.tbEp3Lot.Text = Helper.HexString(sdesc.Business.LotID);
			this.tbEp3Salery.Text = sdesc.Business.Salary.ToString();

			this.cbEp3Asgn.SelectedValue = sdesc.Business.Assignment;
			this.sblb.SimDescription = sdesc;
		}

        void RefreshEP4(Wrapper.ExtSDesc sdesc)
        {
            this.ptGifted.SetTraitLevel(0, 1, sdesc.Pets.PetTraits);
            this.ptHyper.SetTraitLevel(2, 3, sdesc.Pets.PetTraits);
            this.ptIndep.SetTraitLevel(4, 5, sdesc.Pets.PetTraits);
            this.ptAggres.SetTraitLevel(6, 7, sdesc.Pets.PetTraits);
            this.ptPigpen.SetTraitLevel(8, 9, sdesc.Pets.PetTraits);        
        }

        
		

		
		private void ChangedEP3(object sender, System.EventArgs e)
		{
			if (intern) return;
			intern = true;
			try 
			{								
				Sdesc.Business.Salary = Helper.StringToUInt16(this.tbEp3Salery.Text, Sdesc.Business.Salary, 10);
				Sdesc.Business.LotID = Helper.StringToUInt16(this.tbEp3Lot.Text, Sdesc.Business.LotID, 16);
				Sdesc.Business.Flags = Helper.StringToUInt16(this.tbEp3Flag.Text, Sdesc.Business.Flags, 2);
				Sdesc.Business.Assignment = (Wrapper.JobAssignment)this.cbEp3Asgn.SelectedValue;

				Sdesc.Changed = true;
			} 
			finally 
			{
				intern = false;
			}
		}

        private void ChangedEP4(object sender, System.EventArgs e)
        {
            if (intern) return;
            intern = true;
            try
            {
                if ((int)Sdesc.Version >= (int)SimPe.PackedFiles.Wrapper.SDescVersions.Pets)
                {
                    this.ptGifted.UpdateTraits(0, 1, Sdesc.Pets.PetTraits);
                    this.ptHyper.UpdateTraits(2, 3, Sdesc.Pets.PetTraits);
                    this.ptIndep.UpdateTraits(4, 5, Sdesc.Pets.PetTraits);
                    this.ptAggres.UpdateTraits(6, 7, Sdesc.Pets.PetTraits);
                    this.ptPigpen.UpdateTraits(8, 9, Sdesc.Pets.PetTraits);
                    //Sdesc.Changed = true;
                }
            }
            finally
            {
                intern = false;
            }
        }


        #region Freetime
        void RefreshEP7(Wrapper.ExtSDesc sdesc)
        {
            intern = true;
            /*if ((int)sdesc.Version < (int)SimPe.PackedFiles.Wrapper.SDescVersions.Freetime) cbaspiration.IsEnabled = true;
            else cbaspiration.IsEnabled = Helper.XmlRegistry.AllowChangeOfSecondaryAspiration;*/
            cbaspiration2.IsEnabled = Helper.XmlRegistry.AllowChangeOfSecondaryAspiration;
            
            if (cbHobbyEnth.SelectedIndex<0) cbHobbyEnth.SelectedIndex = 0;
            else this.EnthusiasmIndexChanged(cbHobbyEnth, null);

            cbHobbyPre.SelectedValue = sdesc.Freetime.HobbyPredistined;

            this.tbHobbyPre.Text = "0x"+Helper.HexString((ushort)sdesc.Freetime.HobbyPredistined);
            this.tbBugColl.Text = "0x" + Helper.HexString(sdesc.Freetime.BugCollection);
            this.tbLtAsp.Text = "0x" + Helper.HexString(sdesc.Freetime.LongtermAspiration);
            this.tbUnlockPts.Text = sdesc.Freetime.LongtermAspirationUnlockPoints.ToString();
            this.tbUnlocksUsed.Text = sdesc.Freetime.LongtermAspirationUnlocksSpent.ToString();

            this.tb7hunger.Text = sdesc.Freetime.HungerDecayModifier.ToString();
            this.tb7comfort.Text = sdesc.Freetime.ComfortDecayModifier.ToString();
            this.tb7bladder.Text = sdesc.Freetime.BladderDecayModifier.ToString();
            this.tb7energy.Text = sdesc.Freetime.EnergyDecayModifier.ToString();
            this.tb7hygiene.Text = sdesc.Freetime.HygieneDecayModifier.ToString();
            this.tb7fun.Text = sdesc.Freetime.FunDecayModifier.ToString();
            this.tb7social.Text = sdesc.Freetime.SocialPublicDecayModifier.ToString();
           

            
            SelectAspiration(cbaspiration2, sdesc.Freetime.SecondaryAspiration);
            
            intern = false;
        }

        void UpdateSecAspDropDown()
        {
            SetAspirations(cbaspiration2, Sdesc.Freetime.PrimaryAspiration);
        }

        void ChangedAspiration(object sender, EventArgs e){
            ChangedCareer(sender, e);
            UpdateSecAspDropDown();
            SelectAspiration(cbaspiration2, Sdesc.Freetime.SecondaryAspiration);
        }

        private void ChangedHobbyEnthProgress(object sender, EventArgs e)
        {
            ChangedEP7(sender, e);
        }

        private void ChangedEP7(object sender, System.EventArgs e)
        {
            if (intern) return;
            intern = true;
            try
            {
                if ((int)Sdesc.Version >= (int)SimPe.PackedFiles.Wrapper.SDescVersions.Freetime)
                {
                    if (cbHobbyEnth.SelectedIndex >= 0 && cbHobbyEnth.SelectedIndex < Sdesc.Freetime.HobbyEnthusiasm.Count)
                        Sdesc.Freetime.HobbyEnthusiasm[cbHobbyEnth.SelectedIndex] = (ushort)pbhbenth.Value;
                     
                    Sdesc.Freetime.BugCollection = Helper.StringToUInt32(this.tbBugColl.Text, Sdesc.Freetime.BugCollection, 16);
                    Sdesc.Freetime.LongtermAspiration = Helper.StringToUInt16(this.tbLtAsp.Text, Sdesc.Freetime.LongtermAspiration, 16);
                    Sdesc.Freetime.LongtermAspirationUnlockPoints = Helper.StringToUInt16(this.tbUnlockPts.Text, Sdesc.Freetime.LongtermAspirationUnlockPoints, 10);
                    Sdesc.Freetime.LongtermAspirationUnlocksSpent = Helper.StringToUInt16(this.tbUnlocksUsed.Text, Sdesc.Freetime.LongtermAspirationUnlocksSpent, 10);

                    Sdesc.Freetime.HungerDecayModifier = Helper.StringToUInt16(this.tb7hunger.Text, Sdesc.Freetime.HungerDecayModifier, 10);
                    Sdesc.Freetime.ComfortDecayModifier = Helper.StringToUInt16(this.tb7comfort.Text, Sdesc.Freetime.ComfortDecayModifier, 10);
                    Sdesc.Freetime.BladderDecayModifier = Helper.StringToUInt16(this.tb7bladder.Text, Sdesc.Freetime.BladderDecayModifier, 10);
                    Sdesc.Freetime.EnergyDecayModifier = Helper.StringToUInt16(this.tb7energy.Text, Sdesc.Freetime.EnergyDecayModifier, 10);
                    Sdesc.Freetime.HygieneDecayModifier = Helper.StringToUInt16(this.tb7hygiene.Text, Sdesc.Freetime.HygieneDecayModifier, 10);
                    Sdesc.Freetime.FunDecayModifier = Helper.StringToUInt16(this.tb7fun.Text, Sdesc.Freetime.FunDecayModifier, 10);
                    Sdesc.Freetime.SocialPublicDecayModifier = Helper.StringToUInt16(this.tb7social.Text, Sdesc.Freetime.SocialPublicDecayModifier, 10);

                    Sdesc.Freetime.HobbyPredistined = SimPe.PackedFiles.Wrapper.SdscFreetime.IndexToHobbies(cbHobbyPre.SelectedIndex);
                    Sdesc.Freetime.SecondaryAspiration = (LocalizedAspirationTypes)this.cbaspiration2.SelectedItem;				
				

                    Sdesc.Changed = true;
                }
            }
            finally
            {
                intern = false;
            }
        }

        private void PredistinedHobbyIndexChanged(object sender, EventArgs e)
        {
            SimPe.PackedFiles.Wrapper.Hobbies hb = SimPe.PackedFiles.Wrapper.SdscFreetime.IndexToHobbies(cbHobbyPre.SelectedIndex);
            tbHobbyPre.Text = "0x" + Helper.HexString((ushort)hb);

            ChangedEP7(sender, e);
        }

        private void EnthusiasmIndexChanged(object sender, EventArgs e)
        {
            if (cbHobbyEnth.SelectedIndex >= 0 && cbHobbyEnth.SelectedIndex < Sdesc.Freetime.HobbyEnthusiasm.Count)
            {

                this.pbhbenth.Value = Sdesc.Freetime.HobbyEnthusiasm[cbHobbyEnth.SelectedIndex];
                this.pbhbenth.IsEnabled = true;
            }
            else
            {
                this.pbhbenth.Value = 0;
                this.pbhbenth.IsEnabled = false;
            }
        }
        #endregion



        private void sblb_SelectedBusinessChanged(object sender, System.EventArgs e)
		{
			this.llep3openinfo.IsEnabled = (sblb.SelectedBusiness!=null);
			if (sblb.SelectedBusiness!=null)
			{
				if (sblb.SelectedBusiness.BnfoFileIndexItem==null) llep3openinfo.IsEnabled = false;
			}
		}


		private void llep3openinfo_LinkClicked(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if (sblb.SelectedBusiness==null) return;
			
			SimPe.RemoteControl.OpenPackedFile(sblb.SelectedBusiness.BnfoFileIndexItem);
		}

        private void cbSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool showsim = IsHumanoid();
            pnSimInt.IsVisible = showsim;
            pnHumanChar.IsVisible = showsim;
            pnPetChar.IsVisible = !showsim;
            pnPetInt.IsVisible = !showsim;            
        }

        private bool IsHumanoid()
        {
            SimPe.PackedFiles.Wrapper.SdscNightlife.SpeciesType sp = (SimPe.PackedFiles.Wrapper.SdscNightlife.SpeciesType)cbSpecies.SelectedValue;
            bool showsim = sp == SimPe.PackedFiles.Wrapper.SdscNightlife.SpeciesType.Human;
            return showsim;
        }

        private void SetCharacterAttributesVisibility()
        {
            return;
#if UNREACHABLE
            bool showsim = true;
            if (Sdesc == null)
            {
                showsim = true;

            }
            else
            {

                if ((int)Sdesc.Version >= (int)SimPe.PackedFiles.Wrapper.SDescVersions.Pets)
                    showsim = Sdesc.Nightlife.IsHuman;
                else showsim = true;
            }

            pnHumanChar.IsVisible = showsim;
            pnPetChar.IsVisible = !showsim;

            this.pnSimInt.IsVisible = showsim;
            this.pnPetInt.IsVisible = !showsim;
#endif
        }

        private void pnInt_VisibleChanged(object sender, EventArgs e)
        {
            cbSpecies_SelectedIndexChanged(null, null);
        }

        private void pnSimInt_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void activate_miOpenScore(object sender, EventArgs e)
        {
			try
			{
				SimPe.Interfaces.Files.IPackedFileDescriptor scorPfd = null;
				foreach (SimPe.Interfaces.Files.IPackedFileDescriptor p in Sdesc.Package.Index)
				{
					if (p.Type == 0x3053CF74 && p.Instance == Sdesc.FileDescriptor.Instance)
					{
						scorPfd = p;
						break;
					}
				}
				if (scorPfd == null)
					Console.WriteLine("SCOR not found"); // MessageBox replaced: no WinForms in Avalonia port
				else
				{
                    SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem fii =
    FileTable.FileIndex.CreateFileIndexItem(scorPfd, Sdesc.Package);

                    SimPe.RemoteControl.OpenPackedFile(scorPfd, Sdesc.Package);
			}
            }
            catch (Exception ex)
            {
                Helper.ExceptionMessage(ex);
            }
        }

        private void lbcollectibles_Click(object sender, EventArgs e)
        {

        }

        private void lvCollectibles_ItemChecked(object sender, System.EventArgs e)
        {
            ChangedEP6(sender, e);
        }

        #region Avalonia layout (ported from WinForms Designer) — Chunk 1 of 6: skeleton only
        // Form size 696x344 from resx; toolbar at (0,24) size 696x56;
        // page area at (0,80) size 696x264 hosts 12 mode panels stacked at the same coord,
        // mutually exclusive via IsVisible (toggled by SelectButton in this file).
        // Future chunks will add children to each pn* mode panel (see CHUNK markers below).
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ── Toolbar: 14 Avalonia Buttons inside a horizontal StackPanel named toolBar1.
            //    SelectButton iterates toolBar1.Children with `child is Button` (Button = Avalonia.Controls.Button);
            //    do not change to ToolStrip. ButtonCompat would be ideal but lives in SimPE.Scenegraph
            //    which SimPE.Sims does not reference; plain Avalonia.Controls.Button works because
            //    the file's `using Button = Avalonia.Controls.Button;` already aliases it.
            this.toolBar1 = new Avalonia.Controls.StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 2,
                Height = 56,
            };

            this.biId     = new Avalonia.Controls.Button { Content = "Overview" };
            this.biCareer = new Avalonia.Controls.Button { Content = "Career" };
            this.biRel    = new Avalonia.Controls.Button { Content = "Relations" };
            this.biInt    = new Avalonia.Controls.Button { Content = "Interests" };
            this.biChar   = new Avalonia.Controls.Button { Content = "Character" };
            this.biSkill  = new Avalonia.Controls.Button { Content = "Skills" };
            this.biMisc   = new Avalonia.Controls.Button { Content = "Other" };
            this.biEP1    = new Avalonia.Controls.Button { Content = "University" };
            this.biEP2    = new Avalonia.Controls.Button { Content = "Nightlife" };
            this.biEP3    = new Avalonia.Controls.Button { Content = "Business" };
            this.biEP6    = new Avalonia.Controls.Button { Content = "Voyage" };
            this.biEP7    = new Avalonia.Controls.Button { Content = "Freetime" };
            this.biMore   = new Avalonia.Controls.Button { Content = "More" };
            this.biMax    = new Avalonia.Controls.Button
            {
                Content = "Max ALL",
                Margin = new Avalonia.Thickness(52, 1, 0, 2),
            };

            // Click handlers — all 12 mode buttons go through ChoosePage; biMore/biMax have own.
            this.biId.Click     += this.ChoosePage;
            this.biCareer.Click += this.ChoosePage;
            this.biRel.Click    += this.ChoosePage;
            this.biInt.Click    += this.ChoosePage;
            this.biChar.Click   += this.ChoosePage;
            this.biSkill.Click  += this.ChoosePage;
            this.biMisc.Click   += this.ChoosePage;
            this.biEP1.Click    += this.ChoosePage;
            this.biEP2.Click    += this.ChoosePage;
            this.biEP3.Click    += this.ChoosePage;
            this.biEP6.Click    += this.ChoosePage;
            this.biEP7.Click    += this.ChoosePage;
            this.biMore.Click   += this.Activate_biMore;
            this.biMax.Click    += this.Activate_biMax;

            this.toolBar1.Children.Add(this.biId);
            this.toolBar1.Children.Add(this.biCareer);
            this.toolBar1.Children.Add(this.biRel);
            this.toolBar1.Children.Add(this.biInt);
            this.toolBar1.Children.Add(this.biChar);
            this.toolBar1.Children.Add(this.biSkill);
            this.toolBar1.Children.Add(this.biMisc);
            this.toolBar1.Children.Add(this.biEP1);
            this.toolBar1.Children.Add(this.biEP2);
            this.toolBar1.Children.Add(this.biEP3);
            this.toolBar1.Children.Add(this.biEP6);
            this.toolBar1.Children.Add(this.biEP7);
            this.toolBar1.Children.Add(this.biMore);
            this.toolBar1.Children.Add(this.biMax);

            // Tags: each toolbar button references the mode panel it switches to.
            // (Also set in the constructor today; setting here too is harmless and
            // documents the intent for future chunks.)
            // (Done in constructor — see the Tag assignments after InitializeComponent.)

            // ── 12 mode panels — all at (0,80) size 696x264. Mutual exclusion via IsVisible.
            // Use Canvas because most mode panel children use absolute positioning in WinForms.
            // CHUNK 2-6: panel children populated panel-by-panel in later invocations.
            var pageArea = new Avalonia.Controls.Canvas
            {
                Width = 696,
                Height = 264,
                ClipToBounds = true,
            };

            // CHUNK 2: pnId children go here — 17 controls from Designer.cs:626-642 / resx 1340-1819.
            // pnId hosts the identity page: portrait, name/family/instance fields, gender RBs,
            // species combo, life-section combo, age textbox, and a row of bold Tahoma labels.
            // Coordinates and sizes are pnId-relative (taken straight from the resx — no offset).
            this.pnId = new Avalonia.Controls.Canvas { Width = 696, Height = 264, IsVisible = true };
            Avalonia.Controls.Canvas.SetLeft(this.pnId, 0);
            Avalonia.Controls.Canvas.SetTop(this.pnId, 0);
            pageArea.Children.Add(this.pnId);

            // ── Labels (TextBlocks; theme handles padding & line-height; do NOT set Padding).
            // All resx Text values were "TopRight"-aligned in WinForms → TextAlignment.Right.

            // label13 — "Name:" (120,48 / 96x17)
            this.label13.Text = "Name:";
            this.label13.Width = 96;
            this.label13.Height = 17;
            this.label13.TextAlignment = Avalonia.Media.TextAlignment.Right;
            Avalonia.Controls.Canvas.SetLeft(this.label13, 120);
            Avalonia.Controls.Canvas.SetTop(this.label13, 48);
            this.pnId.Children.Add(this.label13);

            // label2 — "Sim ID:" (120,24 / 96x17)
            this.label2.Text = "Sim ID:";
            this.label2.Width = 96;
            this.label2.Height = 17;
            this.label2.TextAlignment = Avalonia.Media.TextAlignment.Right;
            Avalonia.Controls.Canvas.SetLeft(this.label2, 120);
            Avalonia.Controls.Canvas.SetTop(this.label2, 24);
            this.pnId.Children.Add(this.label2);

            // label1 — "Family Instance:" (112,72 / 104x17)
            this.label1.Text = "Family Instance:";
            this.label1.Width = 104;
            this.label1.Height = 17;
            this.label1.TextAlignment = Avalonia.Media.TextAlignment.Right;
            Avalonia.Controls.Canvas.SetLeft(this.label1, 112);
            Avalonia.Controls.Canvas.SetTop(this.label1, 72);
            this.pnId.Children.Add(this.label1);

            // label49 — "Treat as:" (144,96 / 72x17)
            this.label49.Text = "Treat as:";
            this.label49.Width = 72;
            this.label49.Height = 17;
            this.label49.TextAlignment = Avalonia.Media.TextAlignment.Right;
            Avalonia.Controls.Canvas.SetLeft(this.label49, 144);
            Avalonia.Controls.Canvas.SetTop(this.label49, 96);
            this.pnId.Children.Add(this.label49);

            // label48 — "Life Section:" (8,144 / 112x17)
            this.label48.Text = "Life Section:";
            this.label48.Width = 112;
            this.label48.Height = 17;
            this.label48.TextAlignment = Avalonia.Media.TextAlignment.Right;
            Avalonia.Controls.Canvas.SetLeft(this.label48, 8);
            Avalonia.Controls.Canvas.SetTop(this.label48, 144);
            this.pnId.Children.Add(this.label48);

            // label10 — "Remaining Days:" (8,168 / 112x17)
            this.label10.Text = "Remaining Days:";
            this.label10.Width = 112;
            this.label10.Height = 17;
            this.label10.TextAlignment = Avalonia.Media.TextAlignment.Right;
            Avalonia.Controls.Canvas.SetLeft(this.label10, 8);
            Avalonia.Controls.Canvas.SetTop(this.label10, 168);
            this.pnId.Children.Add(this.label10);

            // label16 — "Species:" (8,192 / 112x17)
            this.label16.Text = "Species:";
            this.label16.Width = 112;
            this.label16.Height = 17;
            this.label16.TextAlignment = Avalonia.Media.TextAlignment.Right;
            Avalonia.Controls.Canvas.SetLeft(this.label16, 8);
            Avalonia.Controls.Canvas.SetTop(this.label16, 192);
            this.pnId.Children.Add(this.label16);

            // ── TextBoxes — all wired to ChangedId; existed as `new TextBox()` at field decl.

            // tbsim — Sim ID (224,16 / 104x24)
            this.tbsim.Width = 104;
            this.tbsim.Height = 24;
            Avalonia.Controls.Canvas.SetLeft(this.tbsim, 224);
            Avalonia.Controls.Canvas.SetTop(this.tbsim, 16);
            this.tbsim.TextChanged += this.ChangedId;
            this.pnId.Children.Add(this.tbsim);

            // tbsimdescname — Name (224,40 / 216x24)
            this.tbsimdescname.Width = 216;
            this.tbsimdescname.Height = 24;
            Avalonia.Controls.Canvas.SetLeft(this.tbsimdescname, 224);
            Avalonia.Controls.Canvas.SetTop(this.tbsimdescname, 40);
            this.tbsimdescname.TextChanged += this.ChangedId;
            this.pnId.Children.Add(this.tbsimdescname);

            // tbsimdescfamname — Family Name (448,40 / 216x24)
            this.tbsimdescfamname.Width = 216;
            this.tbsimdescfamname.Height = 24;
            Avalonia.Controls.Canvas.SetLeft(this.tbsimdescfamname, 448);
            Avalonia.Controls.Canvas.SetTop(this.tbsimdescfamname, 40);
            this.tbsimdescfamname.TextChanged += this.ChangedId;
            this.pnId.Children.Add(this.tbsimdescfamname);

            // tbfaminst — Family Instance (224,64 / 56x24)
            this.tbfaminst.Width = 56;
            this.tbfaminst.Height = 24;
            Avalonia.Controls.Canvas.SetLeft(this.tbfaminst, 224);
            Avalonia.Controls.Canvas.SetTop(this.tbfaminst, 64);
            this.tbfaminst.TextChanged += this.ChangedId;
            this.pnId.Children.Add(this.tbfaminst);

            // tbage — Remaining Days (128,160 / 56x24)
            this.tbage.Width = 56;
            this.tbage.Height = 24;
            Avalonia.Controls.Canvas.SetLeft(this.tbage, 128);
            Avalonia.Controls.Canvas.SetTop(this.tbage, 160);
            this.tbage.TextChanged += this.ChangedId;
            this.pnId.Children.Add(this.tbage);

            // ── Gender RadioButtons — share pnId as parent (auto-exclusive).
            // DO NOT set GroupName: it is window-scoped in Avalonia and would couple
            // unrelated RBs across the form. Per feedback_radiobutton_groupname.md.

            // rbfemale (224,96 / 64x16)
            this.rbfemale.Content = "Female";
            this.rbfemale.Width = 64;
            this.rbfemale.Height = 16;
            Avalonia.Controls.Canvas.SetLeft(this.rbfemale, 224);
            Avalonia.Controls.Canvas.SetTop(this.rbfemale, 96);
            this.rbfemale.IsCheckedChanged += this.ChangedId;
            this.pnId.Children.Add(this.rbfemale);

            // rbmale (296,96 / 48x16)
            this.rbmale.Content = "Male";
            this.rbmale.Width = 48;
            this.rbmale.Height = 16;
            Avalonia.Controls.Canvas.SetLeft(this.rbmale, 296);
            Avalonia.Controls.Canvas.SetTop(this.rbmale, 96);
            this.rbmale.IsCheckedChanged += this.ChangedId;
            this.pnId.Children.Add(this.rbmale);

            // ── ComboBoxes (both DropDownList in Designer → non-editable Avalonia ComboBox).

            // cblifesection (128,136 / 160x25) — wired to ChangedId via SelectionChanged.
            this.cblifesection.Width = 160;
            this.cblifesection.Height = 25;
            Avalonia.Controls.Canvas.SetLeft(this.cblifesection, 128);
            Avalonia.Controls.Canvas.SetTop(this.cblifesection, 136);
            this.cblifesection.SelectionChanged += this.ChangedId;
            this.pnId.Children.Add(this.cblifesection);

            // cbSpecies (128,184 / 160x25) — EnumComboBox; two events from Designer.
            // NOTE: cbSpecies is the WinForms-derived Ambertation.Windows.Forms.EnumComboBox.
            // Its WinForms event names (SelectionChangeCommitted, SelectedIndexChanged) do NOT
            // exist on Avalonia.Controls.ComboBox; we route both through Avalonia's
            // SelectionChanged so refresh logic still fires when the selected species changes.
            this.cbSpecies.Width = 160;
            this.cbSpecies.Height = 25;
            Avalonia.Controls.Canvas.SetLeft(this.cbSpecies, 128);
            Avalonia.Controls.Canvas.SetTop(this.cbSpecies, 184);
            this.cbSpecies.SelectionChanged += this.ChangedEP2;
            this.cbSpecies.SelectionChanged += this.cbSpecies_SelectedIndexChanged;
            this.pnId.Children.Add(this.cbSpecies);

            // ── Portrait — PictureBox → Image.
            // SizeMode=StretchImage → Stretch.Fill (stretches without preserving aspect).
            // BackColor=Transparent in Designer is the Avalonia default for Image, no setter needed.
            // pbImage (8,8 / 104x96)
            this.pbImage.Width = 104;
            this.pbImage.Height = 96;
            this.pbImage.Stretch = Avalonia.Media.Stretch.Fill;
            Avalonia.Controls.Canvas.SetLeft(this.pbImage, 8);
            Avalonia.Controls.Canvas.SetTop(this.pbImage, 8);
            this.pnId.Children.Add(this.pbImage);

            // CHUNK 3: pnCareer children go here
            this.pnCareer = new Avalonia.Controls.Canvas { Width = 696, Height = 264, IsVisible = false };
            Avalonia.Controls.Canvas.SetLeft(this.pnCareer, 0);
            Avalonia.Controls.Canvas.SetTop(this.pnCareer, 0);
            pageArea.Children.Add(this.pnCareer);

            // CHUNK 3: pnSkill children go here
            this.pnSkill = new Avalonia.Controls.Canvas { Width = 696, Height = 264, IsVisible = false };
            Avalonia.Controls.Canvas.SetLeft(this.pnSkill, 0);
            Avalonia.Controls.Canvas.SetTop(this.pnSkill, 0);
            pageArea.Children.Add(this.pnSkill);

            // CHUNK 4: pnChar children go here (also hosts pnHumanChar + pnPetChar internally)
            this.pnChar = new Avalonia.Controls.Canvas { Width = 696, Height = 264, IsVisible = false };
            Avalonia.Controls.Canvas.SetLeft(this.pnChar, 0);
            Avalonia.Controls.Canvas.SetTop(this.pnChar, 0);
            pageArea.Children.Add(this.pnChar);

            // CHUNK 4: pnInt children go here (also hosts pnSimInt + pnPetInt internally)
            this.pnInt = new Avalonia.Controls.Canvas { Width = 696, Height = 264, IsVisible = false };
            Avalonia.Controls.Canvas.SetLeft(this.pnInt, 0);
            Avalonia.Controls.Canvas.SetTop(this.pnInt, 0);
            pageArea.Children.Add(this.pnInt);

            // CHUNK 4: pnRel children go here
            this.pnRel = new Avalonia.Controls.Canvas { Width = 696, Height = 264, IsVisible = false };
            Avalonia.Controls.Canvas.SetLeft(this.pnRel, 0);
            Avalonia.Controls.Canvas.SetTop(this.pnRel, 0);
            pageArea.Children.Add(this.pnRel);

            // CHUNK 5: pnMisc children go here
            this.pnMisc = new Avalonia.Controls.Canvas { Width = 696, Height = 264, IsVisible = false };
            Avalonia.Controls.Canvas.SetLeft(this.pnMisc, 0);
            Avalonia.Controls.Canvas.SetTop(this.pnMisc, 0);
            pageArea.Children.Add(this.pnMisc);

            // CHUNK 5: pnEP1 (University) children go here
            this.pnEP1 = new Avalonia.Controls.Canvas { Width = 696, Height = 264, IsVisible = false };
            Avalonia.Controls.Canvas.SetLeft(this.pnEP1, 0);
            Avalonia.Controls.Canvas.SetTop(this.pnEP1, 0);
            pageArea.Children.Add(this.pnEP1);

            // CHUNK 5: pnEP2 (Nightlife) children go here
            this.pnEP2 = new Avalonia.Controls.Canvas { Width = 696, Height = 264, IsVisible = false };
            Avalonia.Controls.Canvas.SetLeft(this.pnEP2, 0);
            Avalonia.Controls.Canvas.SetTop(this.pnEP2, 0);
            pageArea.Children.Add(this.pnEP2);

            // CHUNK 6: pnEP3 (Business) children go here
            this.pnEP3 = new Avalonia.Controls.Canvas { Width = 696, Height = 264, IsVisible = false };
            Avalonia.Controls.Canvas.SetLeft(this.pnEP3, 0);
            Avalonia.Controls.Canvas.SetTop(this.pnEP3, 0);
            pageArea.Children.Add(this.pnEP3);

            // CHUNK 6: pnEP7 (Freetime) children go here
            this.pnEP7 = new Avalonia.Controls.Canvas { Width = 696, Height = 264, IsVisible = false };
            Avalonia.Controls.Canvas.SetLeft(this.pnEP7, 0);
            Avalonia.Controls.Canvas.SetTop(this.pnEP7, 0);
            pageArea.Children.Add(this.pnEP7);

            // CHUNK 6: pnVoyage (EP6) children go here
            this.pnVoyage = new Avalonia.Controls.Canvas { Width = 696, Height = 264, IsVisible = false };
            Avalonia.Controls.Canvas.SetLeft(this.pnVoyage, 0);
            Avalonia.Controls.Canvas.SetTop(this.pnVoyage, 0);
            pageArea.Children.Add(this.pnVoyage);

            // ── ContextMenu 1 (mbiLink): the popup attached to biMore.
            // Items per Designer.cs:1294-1308: mbiMax, miRand, sep, miOpenChar, miOpenWf,
            // miOpenMem, miOpenBadge, miOpenDNA, miOpenSCOR, miOpenFamily, miOpenCloth,
            // sep, miMore, miRelink.
            this.mbiMax           = new Avalonia.Controls.MenuItem { Header = "Maximize" };
            this.miRand           = new Avalonia.Controls.MenuItem { Header = "Randomize" };
            this.miOpenChar       = new Avalonia.Controls.MenuItem { Header = "Open Character File" };
            this.miOpenWf         = new Avalonia.Controls.MenuItem { Header = "Open Wants && Fears" };
            this.miOpenMem        = new Avalonia.Controls.MenuItem { Header = "Open Memories" };
            this.miOpenBadge      = new Avalonia.Controls.MenuItem { Header = "Open hidden Skills && Badges" };
            this.miOpenDNA        = new Avalonia.Controls.MenuItem { Header = "Open Sim DNA" };
            this.miOpenSCOR       = new Avalonia.Controls.MenuItem { Header = "Open Sim Score" };
            this.miOpenFamily     = new Avalonia.Controls.MenuItem { Header = "Open Family" };
            this.miOpenCloth      = new Avalonia.Controls.MenuItem { Header = "Open Clothing" };
            this.miMore           = new Avalonia.Controls.MenuItem { Header = "Extended Browser..." };
            this.miRelink         = new Avalonia.Controls.MenuItem { Header = "Relink Character" };

            this.mbiMax.Click       += this.Activate_biMax;
            this.miRand.Click       += this.Activate_biRand;
            this.miOpenChar.Click   += this.Activate_miOpenCHar;
            this.miOpenWf.Click     += this.Activate_miOpenWf;
            this.miOpenMem.Click    += this.Activate_miOpenMem;
            this.miOpenBadge.Click  += this.Activate_miOpenBadge;
            this.miOpenDNA.Click    += this.Activate_miOpenDNA;
            this.miOpenSCOR.Click   += this.activate_miOpenScore;
            this.miOpenFamily.Click += this.Activate_miFamily;
            this.miOpenCloth.Click  += this.Activate_miOpenCloth;
            this.miMore.Click       += this.Activate_miMore;
            this.miRelink.Click     += this.Activate_miRelink;

            this.mbiLink = new Avalonia.Controls.ContextMenu();
            this.mbiLink.Items.Add(this.mbiMax);
            this.mbiLink.Items.Add(this.miRand);
            this.mbiLink.Items.Add(new Avalonia.Controls.Separator());
            this.mbiLink.Items.Add(this.miOpenChar);
            this.mbiLink.Items.Add(this.miOpenWf);
            this.mbiLink.Items.Add(this.miOpenMem);
            this.mbiLink.Items.Add(this.miOpenBadge);
            this.mbiLink.Items.Add(this.miOpenDNA);
            this.mbiLink.Items.Add(this.miOpenSCOR);
            this.mbiLink.Items.Add(this.miOpenFamily);
            this.mbiLink.Items.Add(this.miOpenCloth);
            this.mbiLink.Items.Add(new Avalonia.Controls.Separator());
            this.mbiLink.Items.Add(this.miMore);
            this.mbiLink.Items.Add(this.miRelink);

            // ── ContextMenu 2 (miRel): the popup for the relations list.
            // Items per Designer.cs:1391-1396: miAddRelation, miRemRelation, sep,
            // mbiMaxThisRel, mbiMaxKnownRel.
            this.miAddRelation  = new Avalonia.Controls.MenuItem { Header = "Add" };
            this.miRemRelation  = new Avalonia.Controls.MenuItem { Header = "Remove" };
            this.mbiMaxThisRel  = new Avalonia.Controls.MenuItem { Header = "Max this Relation" };
            this.mbiMaxKnownRel = new Avalonia.Controls.MenuItem { Header = "Max Relations to known Sims" };

            this.miAddRelation.Click  += this.Activate_miAddRelation;
            this.miRemRelation.Click  += this.Activate_miRemRelation;
            this.mbiMaxThisRel.Click  += this.Activate_mbiMaxThisRel;
            this.mbiMaxKnownRel.Click += this.Activate_mbiMaxKnownRel;

            this.miRel = new Avalonia.Controls.ContextMenu();
            this.miRel.Items.Add(this.miAddRelation);
            this.miRel.Items.Add(this.miRemRelation);
            this.miRel.Items.Add(new Avalonia.Controls.Separator());
            this.miRel.Items.Add(this.mbiMaxThisRel);
            this.miRel.Items.Add(this.mbiMaxKnownRel);
            // miRel_BeforePopup is hooked to the miRel (ContextMenu) Open event so it
            // can configure Add/Remove enabled state before the popup is shown.
            this.miRel.Opened += this.miRel_BeforePopup;

            // ── Root layout: DockPanel with toolbar docked Top, page area filling rest.
            // WrapperBaseControl provides a header band y=0..24 painted by its own render code;
            // the toolbar sits at y=24..80 (size 696x56), pages at y=80..344 (size 696x264).
            // We use DockPanel responsively rather than fixed-positioned Canvas at form root.
            var root = new Avalonia.Controls.DockPanel { LastChildFill = true };

            // Spacer to reserve the 24px header band that WrapperBaseControl paints over.
            var headerSpacer = new Avalonia.Controls.Border { Height = 24, Background = Avalonia.Media.Brushes.Transparent };
            Avalonia.Controls.DockPanel.SetDock(headerSpacer, Avalonia.Controls.Dock.Top);
            root.Children.Add(headerSpacer);

            Avalonia.Controls.DockPanel.SetDock(this.toolBar1, Avalonia.Controls.Dock.Top);
            root.Children.Add(this.toolBar1);

            // pageArea fills remaining space.
            root.Children.Add(pageArea);

            this.Content = root;

            // Responsive sizing per memory: MinWidth/MinHeight, no fixed Width/Height on root.
            this.MinWidth = 696;
            this.MinHeight = 344;
        }

        // ──────────────────────────────────────────────────────────────────
        // Field declarations — moved from SimPE.Sims.Stubs.cs partial-class block.
        // Real types (XPTaskBoxSimple, LabeledProgressBar, EnumComboBox, TransparentCheckBox)
        // restored from the prior Border/ProgressBar/ComboBox/CheckBox shims so Chunks 2-6
        // can populate them with correct type-specific properties.
        // ──────────────────────────────────────────────────────────────────
        private System.ComponentModel.IContainer components;

        // Toolbar StackPanel (was Panel shim) — SelectButton iterates its Children.
        private Avalonia.Controls.StackPanel toolBar1;

        // Toolbar buttons (14) — plain Avalonia.Controls.Button (SimPE.Sims doesn't ref Scenegraph).
        private Avalonia.Controls.Button biId;
        private Avalonia.Controls.Button biCareer;
        private Avalonia.Controls.Button biRel;
        private Avalonia.Controls.Button biInt;
        private Avalonia.Controls.Button biChar;
        private Avalonia.Controls.Button biSkill;
        private Avalonia.Controls.Button biMisc;
        private Avalonia.Controls.Button biEP1;
        private Avalonia.Controls.Button biEP2;
        private Avalonia.Controls.Button biEP3;
        private Avalonia.Controls.Button biEP6;
        private Avalonia.Controls.Button biEP7;
        private Avalonia.Controls.Button biMore;
        private Avalonia.Controls.Button biMax;

        // 12 mode panels (Canvas because children use absolute positioning).
        private Avalonia.Controls.Canvas pnId;
        private Avalonia.Controls.Canvas pnSkill;
        private Avalonia.Controls.Canvas pnChar;
        private Avalonia.Controls.Canvas pnCareer;
        private Avalonia.Controls.Canvas pnRel;
        private Avalonia.Controls.Canvas pnInt;
        private Avalonia.Controls.Canvas pnMisc;
        private Avalonia.Controls.Canvas pnEP1;
        private Avalonia.Controls.Canvas pnEP2;
        private Avalonia.Controls.Canvas pnEP3;
        private Avalonia.Controls.Canvas pnEP7;
        private Avalonia.Controls.Canvas pnVoyage;

        // Sub-panels inside pnChar/pnInt — Designer.cs has these as nested Panels.
        // Declared but not yet placed; CHUNK 4 will add them inside their parents.
        private Avalonia.Controls.Canvas pnHumanChar = new Avalonia.Controls.Canvas();
        private Avalonia.Controls.Canvas pnPetChar  = new Avalonia.Controls.Canvas();
        private Avalonia.Controls.Canvas pnSimInt   = new Avalonia.Controls.Canvas();
        private Avalonia.Controls.Canvas pnPetInt   = new Avalonia.Controls.Canvas();
        // Generic sub-panels used inside misc/relations panels — placement in later chunks.
        private Avalonia.Controls.Canvas panel1 = new Avalonia.Controls.Canvas();
        private Avalonia.Controls.Canvas panel2 = new Avalonia.Controls.Canvas();
        private Avalonia.Controls.Canvas panel3 = new Avalonia.Controls.Canvas();

        // Context menus and their items (ContextMenu1 = mbiLink, ContextMenu2 = miRel).
        private Avalonia.Controls.ContextMenu mbiLink;
        private Avalonia.Controls.ContextMenu miRel;
        private Avalonia.Controls.MenuItem mbiMax;
        private Avalonia.Controls.MenuItem miRand;
        private Avalonia.Controls.MenuItem miOpenChar;
        private Avalonia.Controls.MenuItem miOpenWf;
        private Avalonia.Controls.MenuItem miOpenMem;
        private Avalonia.Controls.MenuItem miOpenBadge;
        private Avalonia.Controls.MenuItem miOpenDNA;
        private Avalonia.Controls.MenuItem miOpenSCOR;
        private Avalonia.Controls.MenuItem miOpenFamily;
        private Avalonia.Controls.MenuItem miOpenCloth;
        private Avalonia.Controls.MenuItem miMore;
        private Avalonia.Controls.MenuItem miRelink;
        private Avalonia.Controls.MenuItem miAddRelation;
        private Avalonia.Controls.MenuItem miRemRelation;
        private Avalonia.Controls.MenuItem mbiMaxThisRel;
        private Avalonia.Controls.MenuItem mbiMaxKnownRel;

        // ── XPTaskBoxSimple — REAL TYPE restored from Border shim.
        // Theming code in the constructor calls themeManager.AddControl(srcTb/dstTb/xpTaskBoxSimpleN).
        private Ambertation.Windows.Forms.XPTaskBoxSimple srcTb            = new Ambertation.Windows.Forms.XPTaskBoxSimple();
        private Ambertation.Windows.Forms.XPTaskBoxSimple dstTb            = new Ambertation.Windows.Forms.XPTaskBoxSimple();
        private Ambertation.Windows.Forms.XPTaskBoxSimple xpTaskBoxSimple1 = new Ambertation.Windows.Forms.XPTaskBoxSimple();
        private Ambertation.Windows.Forms.XPTaskBoxSimple xpTaskBoxSimple2 = new Ambertation.Windows.Forms.XPTaskBoxSimple();
        private Ambertation.Windows.Forms.XPTaskBoxSimple xpTaskBoxSimple3 = new Ambertation.Windows.Forms.XPTaskBoxSimple();
        private Ambertation.Windows.Forms.XPTaskBoxSimple xpTaskBoxSimple4 = new Ambertation.Windows.Forms.XPTaskBoxSimple();

        // ── LabeledProgressBar — REAL TYPE restored from ProgressBar shim.
        // Activate_biMax/Activate_biRand cast pn*.Children to LabeledProgressBar.
        private Ambertation.Windows.Forms.LabeledProgressBar pbActive             = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbAnimals            = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbAspBliz            = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbAspCur             = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbBody               = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbCareerLevel        = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbCareerPerformance  = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbCharisma           = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbClean              = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbCooking            = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbCreative           = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbCrime              = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbCulture            = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbEffort             = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbEntertainment      = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbEnvironment        = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbFashion            = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbFat                = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbFood               = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbGActive            = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbGNeat              = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbGNice              = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbGOutgoing          = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbGPlayful           = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbHealth             = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbLastGrade          = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbLogic              = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbMan                = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbMech               = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbMoney              = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbNeat               = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbNice               = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbOutgoing           = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbParanormal         = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbPlayful            = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbPolitics           = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbRomance            = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbSchool             = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbSciFi              = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbSports             = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbToys               = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbTravel             = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbUniTime            = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbWeather            = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbWoman              = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbWork               = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbPetAnimals         = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbPetEating          = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbPetOutside         = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbPetPets            = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbPetPlaying         = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbPetSleep           = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbPetSpooky          = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbPetToy             = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbPetWeather         = new Ambertation.Windows.Forms.LabeledProgressBar();
        private Ambertation.Windows.Forms.LabeledProgressBar pbhbenth             = new Ambertation.Windows.Forms.LabeledProgressBar();

        // ── EnumComboBox — REAL TYPE restored from ComboBox shim.
        private Ambertation.Windows.Forms.EnumComboBox cbEp3Asgn = new Ambertation.Windows.Forms.EnumComboBox();
        private Ambertation.Windows.Forms.EnumComboBox cbHobbyPre = new Ambertation.Windows.Forms.EnumComboBox();
        internal Ambertation.Windows.Forms.EnumComboBox cbSpecies = new Ambertation.Windows.Forms.EnumComboBox();

        // ── TransparentCheckBox — REAL TYPE restored from CheckBox shim.
        private Ambertation.Windows.Forms.TransparentCheckBox cbfat              = new Ambertation.Windows.Forms.TransparentCheckBox();
        private Ambertation.Windows.Forms.TransparentCheckBox cbfit              = new Ambertation.Windows.Forms.TransparentCheckBox();
        private Ambertation.Windows.Forms.TransparentCheckBox cbignoretraversal  = new Ambertation.Windows.Forms.TransparentCheckBox();
        private Ambertation.Windows.Forms.TransparentCheckBox cbisghost          = new Ambertation.Windows.Forms.TransparentCheckBox();
        private Ambertation.Windows.Forms.TransparentCheckBox cbpassobject       = new Ambertation.Windows.Forms.TransparentCheckBox();
        private Ambertation.Windows.Forms.TransparentCheckBox cbpasspeople       = new Ambertation.Windows.Forms.TransparentCheckBox();
        private Ambertation.Windows.Forms.TransparentCheckBox cbpasswalls        = new Ambertation.Windows.Forms.TransparentCheckBox();
        private Ambertation.Windows.Forms.TransparentCheckBox cbpregfull         = new Ambertation.Windows.Forms.TransparentCheckBox();
        private Ambertation.Windows.Forms.TransparentCheckBox cbpreghalf         = new Ambertation.Windows.Forms.TransparentCheckBox();
        private Ambertation.Windows.Forms.TransparentCheckBox cbpreginv          = new Ambertation.Windows.Forms.TransparentCheckBox();
        internal Ambertation.Windows.Forms.TransparentCheckBox cboncampus        = new Ambertation.Windows.Forms.TransparentCheckBox();

        // Standard ComboBoxes
        private Avalonia.Controls.ComboBox cbHobbyEnth = new Avalonia.Controls.ComboBox();

        // Labels — TextBlock for non-interactive text.
        private Avalonia.Controls.TextBlock label1 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label2 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label3 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label4 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label5 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label6 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label7 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label8 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label9 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label10 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label11 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label12 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label13 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label14 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label15 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label16 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label17 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label18 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label19 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label20 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label21 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label22 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label23 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label24 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label25 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label26 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label27 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label28 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label29 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label30 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label31 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label32 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label33 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label34 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label35 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label36 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label37 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label38 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label39 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label40 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label41 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock labelcol = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label46 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label47 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label48 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label49 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label50 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label60 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label69 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label70 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label77 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label78 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label86 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label87 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label90 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label94 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label95 = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock label96 = new Avalonia.Controls.TextBlock();
        internal Avalonia.Controls.TextBlock label98 = new Avalonia.Controls.TextBlock();
        internal Avalonia.Controls.TextBlock label101 = new Avalonia.Controls.TextBlock();
        internal Avalonia.Controls.TextBlock label103 = new Avalonia.Controls.TextBlock();

        // ListView -> ListBox stand-in (CHUNK 6 will refine if proper ListView is needed).
        private Avalonia.Controls.ListBox lvCollectibles = new Avalonia.Controls.ListBox();

        // PictureBox -> Image
        private Avalonia.Controls.Image pictureBox1 = new Avalonia.Controls.Image();

        // CheckedListBox -> ListBox (Avalonia has none).
        private Avalonia.Controls.ListBox lbTraits  = new Avalonia.Controls.ListBox();
        private Avalonia.Controls.ListBox lbTurnOff = new Avalonia.Controls.ListBox();
        private Avalonia.Controls.ListBox lbTurnOn  = new Avalonia.Controls.ListBox();

        // LinkLabel -> Button styled as link (CHUNK 5 may swap to LinkLabel if available).
        private Avalonia.Controls.Button llep3openinfo = new Avalonia.Controls.Button();

        // TextBoxes — non-internal (private).
        private Avalonia.Controls.TextBox tbEp3Flag      = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox tbEp3Lot       = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox tbEp3Salery    = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox tbNTLove       = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox tbNTPerfume    = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox tbBugColl      = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox tbHobbyPre     = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox tbLtAsp        = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox tbUnlockPts    = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox tbUnlocksUsed  = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox tbhdaysleft    = new Avalonia.Controls.TextBox();

        // SimPe.PackedFiles.UserInterface types (same project).
        private SimPe.PackedFiles.UserInterface.CommonSrel dstRel;
        private SimPe.PackedFiles.UserInterface.CommonSrel srcRel;

        // SimPe.PackedFiles.Wrapper types — real classes from the project.
        private SimPe.PackedFiles.Wrapper.PetTraitSelect ptAggres = new SimPe.PackedFiles.Wrapper.PetTraitSelect();
        private SimPe.PackedFiles.Wrapper.PetTraitSelect ptGifted = new SimPe.PackedFiles.Wrapper.PetTraitSelect();
        private SimPe.PackedFiles.Wrapper.PetTraitSelect ptHyper  = new SimPe.PackedFiles.Wrapper.PetTraitSelect();
        private SimPe.PackedFiles.Wrapper.PetTraitSelect ptIndep  = new SimPe.PackedFiles.Wrapper.PetTraitSelect();
        private SimPe.PackedFiles.Wrapper.PetTraitSelect ptPigpen = new SimPe.PackedFiles.Wrapper.PetTraitSelect();
        private SimPe.PackedFiles.Wrapper.SimBusinessList sblb;
        private SimPe.PackedFiles.Wrapper.SimRelationPoolControl lv = new SimPe.PackedFiles.Wrapper.SimRelationPoolControl();

        // Internal fields accessed from the constructor / refresh code — preserved verbatim.
        internal Avalonia.Controls.TextBox    tbsimdescfamname = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tbfaminst        = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.RadioButton rbmale          = new Avalonia.Controls.RadioButton();
        internal Avalonia.Controls.RadioButton rbfemale        = new Avalonia.Controls.RadioButton();
        internal Avalonia.Controls.ComboBox    cblifesection   = new Avalonia.Controls.ComboBox();
        internal Avalonia.Controls.Image       pbImage         = new Avalonia.Controls.Image();
        internal Avalonia.Controls.TextBox    tbsimdescname    = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tbsim            = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tbage            = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.ComboBox    cbzodiac        = new Avalonia.Controls.ComboBox();
        internal Avalonia.Controls.TextBox    tbschooltype     = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.ComboBox    cbgrade         = new Avalonia.Controls.ComboBox();
        internal Avalonia.Controls.ComboBox    cbschooltype    = new Avalonia.Controls.ComboBox();
        internal Avalonia.Controls.ComboBox    cbcareer        = new Avalonia.Controls.ComboBox();
        internal Avalonia.Controls.TextBox    tbcareervalue    = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.ComboBox    cbaspiration    = new Avalonia.Controls.ComboBox();
        internal Avalonia.Controls.TextBox    tblifelinescore  = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tbunlinked       = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tbagedur         = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tbprevdays       = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tbvoice          = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tbnpc            = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tbautonomy       = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tbinfluence      = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tbsemester       = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.ComboBox    cbmajor         = new Avalonia.Controls.ComboBox();
        internal Avalonia.Controls.TextBox    tbmajor          = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tbstatmot        = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tb7social        = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tb7fun           = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tb7hygiene       = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tb7energy        = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tb7bladder       = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tb7comfort       = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.TextBox    tb7hunger        = new Avalonia.Controls.TextBox();
        internal Avalonia.Controls.ComboBox    cbaspiration2   = new Avalonia.Controls.ComboBox();
        #endregion
	}
}
