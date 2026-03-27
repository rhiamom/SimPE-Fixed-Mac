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

namespace SimPe.Wizards
{
	/// <summary>
	/// This class can be used to interface the StatusBar of the Main GUI, which will display
	/// something like the WaitingScreen
	/// </summary>
	internal class WaitBarControl : IWaitingBarControl
	{
		Form1 f;
		public WaitBarControl(Form1 mf)
		{
			f = mf;
		}

		delegate void SetStuff(object o);
		delegate void ShowStuff(bool visible);

		#region Visible Control
		protected void ShowMain(bool visible)
		{
			f.pnP.IsVisible = visible;
		}


		protected void DoShowProgress(bool visible)
		{
			f.pbP.IsVisible = visible;
		}


		protected void ShowDescription(bool visible)
		{
			f.lbPmsg.IsVisible = visible;
		}
		#endregion

		#region Setters
		protected void SetMessage(object text)
		{
			f.lbPmsg.Text = text.ToString();
		}


		protected void SetProgress(object val)
		{
			int i = (int)val;
			f.pbP.Value = i;
		}



		protected void SetMaxProgress(object val)
		{
			int i = (int)val;
			f.pbP.Maximum = i;
		}


		#endregion

        public bool ShowProgress
        {
            get { return f.pbP.IsVisible;  }
            set { DoShowProgress(value); }
        }

		public bool Running
		{
			get { return f.pnP.IsVisible; }
		}

		public string Message
		{
			get { return f.lbPmsg.Text; }
			set
			{
				if (value!=f.lbPmsg.Text)
				{
					f.lbPmsg.Text = " "+value;
				}
			}
		}

		public Avalonia.Media.Imaging.Bitmap Image
		{
			get { return null; }
			set
			{

			}
		}

		public int Progress
		{
			get { return (int)f.pbP.Value; }
			set
			{
				if (value!=(int)f.pbP.Value)
				{
					SetProgress(value);
				}
			}
		}

		public int MaxProgress
		{
			get { return (int)f.pbP.Maximum; }
			set
			{
				if (value!=(int)f.pbP.Maximum)
				{
					DoShowProgress(true);
					f.pbP.Maximum = value;
				}
			}
		}

		protected void StartWait()
		{
			ShowDescription(true);
			Message = SimPe.Localization.GetString("Please Wait");
			Image = null;
			ShowMain(true);
		}

		public void Wait()
		{
			StartWait();
		}

		public void Wait(int max)
		{
			Progress=0;
			StartWait();
			MaxProgress = max;
		}

		public void Stop()
		{
			try
			{
				ShowMain(false);
				DoShowProgress(false);
			}
			catch {}
		}
	}
}
