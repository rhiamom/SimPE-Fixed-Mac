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
using System.Collections;
using System.ComponentModel;
using System.Data;
using SimPe;
using Avalonia.Controls;

namespace SimPe.Wizards
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1
	{
		private SimPe.Scenegraph.Compat.PictureBox pbtop;
		private SimPe.Scenegraph.Compat.PictureBox pbbottom;
		internal Canvas pndrop;
		private SimPe.Scenegraph.Compat.PictureBox pbstretch;
		private TextBlock lbstep;
		private Button llnext;
        private Button llback;
		private Button llopt;
		internal Canvas pnP;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private TextBlock lbmsg;
		internal TextBlock lbPmsg;
        internal ProgressBar pbP;
        private TextBlock line1;

		// WinForms-compat stubs
		public object Invoke(Delegate method, object[] args) { try { method.DynamicInvoke(args); } catch { } return null; }

        FormStep1 step1;
		public Form1()
		{
			//
			// Required designer variable.
			//
			InitializeComponent();

			step1 = new FormStep1();
			prevsteps.Push(step1);
			ShowStep(step1, true);
			if ((!Option.HaveObjects) || (!Option.HaveSavefolder))
			{
				SimPe.Message.Show("Your Path settings are invalid. Wizards of SimPe will direct you to the Options Page.\n\nYou can just click on the 'Suggest' Buttons there, to get the default Paths. If the 'Suggest' Button disapears, your Path is set correct.", "Warning", MessageBoxButtons.OK);
				this.ShowOptions(null, null);
			}

			Wait.Bar = new SimPe.Wizards.WaitBarControl(this);
			if (SimPe.FileTable.FileIndex==null) SimPe.FileTable.FileIndex = new SimPe.Plugin.FileIndex();
			SimPe.Packages.PackageMaintainer.Maintainer.FileIndex = SimPe.FileTable.FileIndex;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		public void Dispose()
		{
			if (components != null)
			{
				components.Dispose();
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pbtop     = new SimPe.Scenegraph.Compat.PictureBox();
            this.pbbottom  = new SimPe.Scenegraph.Compat.PictureBox();
            this.pndrop    = new Canvas();
            this.line1     = new TextBlock();
            this.llopt     = new Button();
            this.llback    = new Button();
            this.llnext    = new Button();
            this.pbstretch = new SimPe.Scenegraph.Compat.PictureBox();
            this.lbstep    = new TextBlock();
            this.pnP       = new Canvas();
            this.pbP       = new ProgressBar();
            this.lbPmsg    = new TextBlock();
            this.lbmsg     = new TextBlock();
            //
            // llopt
            //
            this.llopt.Content = "Options";
            this.llopt.Click += (s, e) => ShowOptions(s, EventArgs.Empty);
            //
            // llback
            //
            this.llback.IsEnabled = false;
            this.llback.Content = "< Back";
            this.llback.Click += (s, e) => Back(s, EventArgs.Empty);
            //
            // llnext
            //
            this.llnext.IsEnabled = false;
            this.llnext.Content = "Next >";
            this.llnext.Click += (s, e) => Next(s, EventArgs.Empty);
            //
            // lbstep
            //
            this.lbstep.Text = "0";
            //
            // pbP
            //
            this.pbP.Maximum = 100;
            this.pbP.Minimum = 0;
            this.pbP.Value = 50;
            //
            // lbPmsg
            //
            this.lbPmsg.Text = "Please Wait";
            //
            // lbmsg
            //
            this.lbmsg.Text = "Description";
            //
            // pnP
            //
            this.pnP.IsVisible = false;
		}
		#endregion

		internal static Form1 form1;

		/// <summary>
		/// Der Haupteinstiegspunkt f�r die Anwendung.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				bool adv = SimPe.Helper.XmlRegistry.HiddenMode;
				bool asy = SimPe.Helper.XmlRegistry.AsynchronLoad;

				SimPe.Helper.XmlRegistry.HiddenMode = false;
				SimPe.Helper.XmlRegistry.AsynchronLoad = false;
				SimPe.Plugin.ScenegraphWrapperFactory.InitRcolBlocks();
                form1 = new Form1();
                System.Windows.Forms.Application.EnableVisualStyles();
				System.Windows.Forms.Application.Run(form1);

				SimPe.Helper.XmlRegistry.HiddenMode = adv;
				SimPe.Helper.XmlRegistry.AsynchronLoad = asy;

				SimPe.Helper.XmlRegistry.Flush();
			}
			catch (Exception ex)
			{
				SimPe.Message.Show("WOS will Shutdown due to an unhandled Exception. \n\nMessage:"+ex.Message);
			}
		}

		private void ExitClick(object sender, EventArgs e)
		{
			Dispose();
		}

		#region Step Management

		Stack prevsteps = new Stack();

		/// <summary>
		/// Called upon changes in a Step Form
		/// </summary>
		/// <param name="sender">the current Wizard Step</param>
		/// <param name="autonext">true if the page wanted to go to the next Wizard Step NOW</param>
		void ContentChanged(IWizardForm sender, bool autonext)
		{
			llnext.IsEnabled = sender.CanContinue;
			if (autonext) this.Next();
		}

		/// <summary>
		/// Show the Prev Step
		/// </summary>
		internal void Prev()
		{
			IWizardForm now = prevsteps.Pop();
			if (now==null) return;
			{ var __wp = now.WizardWindow; if (__wp != null) __wp.IsVisible = false; }

			now = prevsteps.Tail();
			if (now==null) return;

			ShowStep(now, false);
		}

		/// <summary>
		/// Show the Next Step
		/// </summary>
		internal void Next()
		{
			IWizardForm now = prevsteps.Tail();
			if (now==null) return;

			if (now.GetType().GetInterface("IWizardFinish", false) == typeof(IWizardFinish))
			{
				IWizardFinish wf = (IWizardFinish)now;
				wf.Finit();

				prevsteps = new Stack();
				prevsteps.Push(step1);
				ShowStep(step1, true);
			}
			else
			{
				{ var __wp = now.WizardWindow; if (__wp != null) __wp.IsVisible = false; }

				now = now.Next;
				if (now==null) return;

				prevsteps.Push(now);
				ShowStep(now, true);
			}
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{

		}

		Option op = new Option();
		private void ShowOptions(object sender, EventArgs e)
		{
			op.form1 = this;
			if (!pndrop.Children.Contains(op.pnopt)) pndrop.Children.Add(op.pnopt);
            op.tbsims.Text = PathProvider.Global[Expansions.BaseGame].InstallFolder;
            op.tbsave.Text = PathProvider.SimSavegameFolder;
            op.tbdds.Text = PathProvider.Global.NvidiaDDSPath;
			op.pnopt.IsVisible = true;
			pndrop.IsVisible = false;
		}

		internal void HideOptions(object sender, EventArgs e)
		{
			pndrop.IsVisible = true;
			op.pnopt.IsVisible = false;
		}

		private void Close(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if ((prevsteps.Count>1))
			{
				e.Cancel = (SimPe.Message.Show("This Wizard is not finished yet.\n\nDo you want to quit anyway?", "Information", MessageBoxButtons.YesNo)!=SimPe.DialogResult.Yes);
			}

			if (!e.Cancel) Helper.XmlRegistry.Flush();
		}

		/// <summary>
		/// Display a new Step
		/// </summary>
		/// <param name="step">The Step you want to Show</param>
		void ShowStep(IWizardForm step, bool init)
		{
			Avalonia.Controls.Panel pn = step.WizardWindow;
			if (pn == null) return;

			pn.IsVisible = false;
			if (!pndrop.Children.Contains(pn)) pndrop.Children.Add(pn);
			pn.Background = Avalonia.Media.Brushes.White;

			lbmsg.Text = step.WizardMessage;
			lbstep.Text = step.WizardStep.ToString();

			llback.IsEnabled = (prevsteps.Count>1);
			if (step.GetType().GetInterface("IWizardFinish", false) == typeof(IWizardFinish))
			{
				llnext.Content = "Finish";
				llnext.IsEnabled = true;
			}
			else
			{
				llnext.Content = "Next >";
				llnext.IsEnabled = (step.Next!=null);
			}

			llnext.IsEnabled = llnext.IsEnabled & step.CanContinue;
			llopt.IsVisible = (prevsteps.Count<=1);

			bool show = true;
			if (init) show = step.Init(new ChangedContent(this.ContentChanged));
			pn.IsVisible = show;
		}
		#endregion

		private void Back(object sender, EventArgs e)
		{
			Prev();
		}

		private void Next(object sender, EventArgs e)
		{
			Next();
		}

	}
}
