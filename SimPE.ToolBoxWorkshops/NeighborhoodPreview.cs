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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using SkiaSharp;
using Avalonia.Controls;
using SimPe.Scenegraph.Compat;
using Image = System.Drawing.Image;

namespace SimPe.Plugin.Tool.Dockable
{
	/// <summary>
	/// Summary description for ObjectPreview.
	/// </summary>
	public class NeighborhoodPreview : Avalonia.Controls.UserControl
	{
		private TextBlock label1;
		private TextBlock label3;
		private TextBlock lbName;
		private PictureBox pb;
		private TextBlock lbAbout;
		private TextBlock label2;
        private TextBlock lbPop;
		private TextBlock label4;
		private TextBlock lbUni;
		private TextBlock label5;
		private TextBlock lbVer;
		private TextBlock lbType;
        private TextBlock label6;
        private TextBlock label7;
        private TextBlock lbholi;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NeighborhoodPreview()
		{
			loaded = false;			

			// Required designer variable.
			InitializeComponent();

			BuildDefaultImage();
			ClearScreen();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected virtual void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
		}

		#region Windows Form Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pb = new SimPe.Scenegraph.Compat.PictureBox();
            this.label1 = new Avalonia.Controls.TextBlock();
            this.label3 = new Avalonia.Controls.TextBlock();
            this.lbName = new Avalonia.Controls.TextBlock();
            this.lbAbout = new Avalonia.Controls.TextBlock();
            this.label2 = new Avalonia.Controls.TextBlock();
            this.lbPop = new Avalonia.Controls.TextBlock();
            this.label4 = new Avalonia.Controls.TextBlock();
            this.lbUni = new Avalonia.Controls.TextBlock();
            this.label5 = new Avalonia.Controls.TextBlock();
            this.lbVer = new Avalonia.Controls.TextBlock();
            this.lbType = new Avalonia.Controls.TextBlock();
            this.label6 = new Avalonia.Controls.TextBlock();
            this.label7 = new Avalonia.Controls.TextBlock();
            this.lbholi = new Avalonia.Controls.TextBlock();
		}
		#endregion

		#region Public Properties		

		bool loaded;
		[Browsable(false)]
		public new bool Loaded
		{
			get { return loaded; }
		}

		SimPe.Interfaces.Files.IPackageFile pkg;
		[Browsable(false)]
		public SimPe.Interfaces.Files.IPackageFile Package
		{
			get { return pkg; }
		}
		#endregion

		
		

		protected void ClearScreen()
		{
			label5.IsVisible = Helper.XmlRegistry.HiddenMode;
            lbVer.IsVisible = Helper.XmlRegistry.HiddenMode;

			if (this.CatalogDescription!=null) 
			{
				ctss.ChangedData -= new SimPe.Events.PackedFileChanged(ctss_ChangedUserData);
				ctss = null;
			}
			pb.Image = defimg;
			this.lbAbout.Text = "";
			this.lbName.Text = "";
			this.lbPop.Text = "???";
            this.lbUni.Text = "???";
            this.lbholi.Text = "???";
		}

		public void SetFromPackage(SimPe.Interfaces.Files.IPackageFile pkg)
		{			
			loaded = false;
			ClearScreen();
			this.pkg = pkg;
			if (pkg==null) return;
			if (!Helper.IsNeighborhoodFile(pkg.FileName)) return;
			loaded = true;
			
			try 
			{
				SimPe.PackedFiles.Wrapper.StrItemList strs = GetCtssItems();
				if (strs!=null) 
				{
					if (strs.Count>0) this.lbName.Text = strs[0].Title;
					if (strs.Count>1) this.lbAbout.Text = strs[1].Title;
				}			

				string tname = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(pkg.FileName), System.IO.Path.GetFileNameWithoutExtension(pkg.FileName)+".png");
				pb.Image = null;
				if (System.IO.File.Exists(tname)) 
				{
					try 
					{
						pb.Image = ObjectPreview.GenerateImage(pb.Size, Image.FromFile(tname), false);
					} 
					catch {}
				}

				if (pb.Image == null) pb.Image = defimg;

                SimPe.Plugin.Idno idno = SimPe.Plugin.Idno.FromPackage(pkg);
                if (idno != null)
                {
                    if (idno.Type == SimPe.Plugin.NeighborhoodType.Normal)
                    {
                        this.label2.IsVisible = true; this.lbPop.IsVisible = true;
                        lbPop.Text = pkg.FindFiles(Data.MetaData.SIM_DESCRIPTION_FILE).Length.ToString();
                    }
                    else
                    {
                        this.label2.IsVisible = false; this.lbPop.IsVisible = false;
                    }

                    if (idno.Type == SimPe.Plugin.NeighborhoodType.Normal || (idno.Type == SimPe.Plugin.NeighborhoodType.Suburb && (idno.Subep == Data.MetaData.NeighbourhoodEP.Business || idno.Subep == Data.MetaData.NeighbourhoodEP.MansionGarden)))
                    {
                        this.label4.IsVisible = true; 
                        this.label7.IsVisible = true; 
                        this.lbUni.IsVisible = true; 
                        this.lbholi.IsVisible = true;
                        lbUni.Text = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(pkg.FileName), "*_University*.package").Length.ToString();
                        lbholi.Text = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(pkg.FileName), "*_Vacation*.package").Length.ToString();
                    }

                    string typeText = idno.Type.ToString().Replace("_", " ");

                    if (idno.Type == SimPe.Plugin.NeighborhoodType.Suburb &&
                        idno.Subep != Data.MetaData.NeighbourhoodEP.Business)
                    {
                        this.lbType.Text = "Hidden " + typeText;
                    }
                    else
                    {
                        this.lbType.Text = typeText;
                    }
                   
                }
                else
                {
                    this.label2.IsVisible = false; this.label4.IsVisible = false; this.label7.IsVisible = false; this.lbPop.IsVisible = false; this.lbUni.IsVisible = false; this.lbholi.IsVisible = false;
                    if (pkg.FileName.Contains ("Tutorial"))
                    {
                        this.lbType.Text = "Tutorial Neighbourhood";
                        this.lbVer.Text = "Every EP";
                        this.lbName.Text = "Tutorial";
                        this.lbAbout.Text = "This neighbourhood is for you to learn with, don't make changes here but in game you may do whatever you like.";
                    }
                    else
                    {
                        this.lbType.Text = SimPe.Plugin.NeighborhoodType.Unknown.ToString();
                        this.lbVer.Text = SimPe.Plugin.NeighborhoodVersion.Unknown.ToString();
                    }
                }
			}
			catch (Exception ex)
			{
				this.lbAbout.Text = ex.Message;
			}
		}

		Interfaces.Files.IPackedFileDescriptor ctss;
		protected Interfaces.Files.IPackedFileDescriptor CatalogDescription
		{
			get 
			{
				if (pkg==null) return null;
				if (ctss==null) ctss = pkg.FindFile(Data.MetaData.CTSS_FILE, 0, Data.MetaData.LOCAL_GROUP, 1);
				return ctss;
			}
		}

		protected void ShowVersion()
		{
			SimPe.Plugin.Idno idno = SimPe.Plugin.Idno.FromPackage(pkg);
			if (idno!=null) 
			{
				this.lbVer.Text = idno.Version.ToString().Replace("_", " ");
			} 
			else 
			{
				this.lbVer.Text = SimPe.Plugin.NeighborhoodVersion.Unknown.ToString();
			}
		}

		protected SimPe.PackedFiles.Wrapper.StrItemList GetCtssItems()
		{
			//Get the Name of the Object
			Interfaces.Files.IPackedFileDescriptor ctss = CatalogDescription;
			if (ctss!= null) 
			{
				ctss.ChangedData += new SimPe.Events.PackedFileChanged(ctss_ChangedUserData);
				SimPe.PackedFiles.Wrapper.Str str = new SimPe.PackedFiles.Wrapper.Str();
				str.ProcessData(ctss, pkg);

               return str.LanguageItems((SimPe.PackedFiles.Wrapper.StrLanguage)(int)Helper.XmlRegistry.LanguageCode);
				
			} 
			return null;
		}

		object defimg;
		protected void BuildDefaultImage()
		{
            defimg = new SKBitmap(1, 1);
		}


		private void ctss_ChangedUserData(SimPe.Interfaces.Files.IPackedFileDescriptor sender)
		{
			SetFromPackage(this.pkg);
		}
	}
}
