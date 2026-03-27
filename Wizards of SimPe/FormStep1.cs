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
using Avalonia.Controls;

namespace SimPe.Wizards
{
	/// <summary>
	/// Summary description for FormStep1.
	/// </summary>
	public class FormStep1 : IWizardForm
	{
		private Canvas pnwizard;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormStep1()
		{
			//
			// Required designer variable.
			//
			InitializeComponent();

			WizardSelector ws = new WizardSelector();

			double top = 16;
			foreach (IWizardEntry we in ws.Wizards)
			{
				Canvas pn = BuildWizardPanel(we);
				pn.IsVisible = true;
				Canvas.SetTop(pn, top);
				pnwizard.Children.Add(pn);

				top += pn.Height + 8;
			}

			pnwizard.Height = top;
		}

		Canvas BuildWizardPanel(IWizardEntry we)
		{
			Canvas pn = new Canvas();
			pn.Width = pnwizard.Width - 148;
			Canvas.SetLeft(pn, 24);
			pn.Height = 64;

			SimPe.Scenegraph.Compat.PictureBox pb = new SimPe.Scenegraph.Compat.PictureBox();
			pb.Width = 64;
			pb.Height = 64;
			Canvas.SetLeft(pb, 0);
			Canvas.SetTop(pb, 0);
			pb.Image = (System.Drawing.Image)(object)we.WizardImage;
			pb.IsVisible = true;
			pn.Children.Add(pb);

			Button lb1 = new Button();
			Canvas.SetLeft(lb1, pb.Width + 8);
			Canvas.SetTop(lb1, 0);
			lb1.Content = we.WizardCaption;
			lb1.DataContext = we;
			lb1.Click += (s, e) => StartWizard(s, EventArgs.Empty);
			lb1.IsVisible = true;
			pn.Children.Add(lb1);

			TextBlock lb2 = new TextBlock();
			Canvas.SetLeft(lb2, pb.Width + 8);
			Canvas.SetTop(lb2, lb1.Height);
			lb2.Width = pn.Width - (pb.Width + 8) - 16;
			lb2.Height = pn.Height - lb1.Height;
			lb2.Foreground = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Colors.DarkGray);
			lb2.Text = we.WizardDescription;
			lb2.IsVisible = true;
			pn.Children.Add(lb2);

			return pn;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		public void Dispose()
		{
			if(components != null)
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
            this.pnwizard = new Canvas();
            // pnwizard
            this.pnwizard.Background = Avalonia.Media.Brushes.White;
            this.pnwizard.Name = "pnwizard";
            this.pnwizard.Width = 1022;
            this.pnwizard.Height = 626;
		}
		#endregion

		#region IWizardWindow Members
		public Avalonia.Controls.Panel WizardWindow
		{
			get { return null; /* TODO: replace with Avalonia panel */ }
		}

		public string WizardMessage
		{
			get { return "Please select the Task you want to perform.";}
		}

		public int WizardStep
		{
			get { return 1; }
		}

		public bool Init(ChangedContent fkt)
		{
			return true;
		}

		IWizardForm wizard;
		public IWizardForm Next
		{
			get
			{
				return wizard;
			}
		}

		public bool CanContinue
		{
			get
			{
				return false;
			}
		}
		#endregion

		private void StartWizard(object sender, EventArgs e)
		{
			Button ll = (Button)sender;
			wizard = (IWizardForm)ll.DataContext;

			Form1.form1.Next();
		}
	}
}
