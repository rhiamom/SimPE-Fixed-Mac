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

namespace SimPe.Plugin.Tool.Dockable
{
	/// <summary>
	/// Summary description for DockableWindow1.
	/// </summary>
	public class dcPackageDetails : Ambertation.Windows.Forms.DockPanel
	{
        private Avalonia.Controls.StackPanel xpGradientPanel1;
		protected SimPe.Plugin.Tool.Dockable.NeighborhoodPreview np;
		private SimPe.Plugin.Tool.Dockable.ObjectPreview op;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public dcPackageDetails()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            ThemeManager.Global.AddControl(this.xpGradientPanel1);
        }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.xpGradientPanel1 = new Avalonia.Controls.StackPanel();
            this.np = new SimPe.Plugin.Tool.Dockable.NeighborhoodPreview();
            this.op = new SimPe.Plugin.Tool.Dockable.ObjectPreview();
            //
            // xpGradientPanel1
            //

            this.xpGradientPanel1.Children.Add(this.np);
            this.xpGradientPanel1.Children.Add(this.op);
            //
            // np
            //
            //
            // op
            //
            this.op.LoadCustomImage = true;
            this.op.SelectedObject = null;
            this.op.SelectedXObject = null;
            //
            // dcPackageDetails
            //
            this.AvaloniaContent = this.xpGradientPanel1;
            this.Controls.Add(this.xpGradientPanel1);
            this.FloatingSize = new System.Drawing.Size(592, 376);
            this.Image = null;
            this.TabImage = null;
            this.TabText = "Details";
            this.TabIconBitmap = SimPe.LoadIcon.LoadAvaloniaBitmap("PackageDetailDock_$this.TabImage.png");
            this.VisibleChanged += new System.EventHandler(this.dcPackageDetails_VisibleChanged);
		}
		#endregion

		private void dcPackageDetails_VisibleChanged(object sender, System.EventArgs e)
		{
			this.op.LoadCustomImage = this.IsVisible;
		}

        internal void SetPackage(SimPe.Interfaces.Files.IPackageFile pkg) // CJH
		{
			this.op.SetFromPackage(pkg);
            this.np.SetFromPackage(pkg);
			op.IsVisible = op.Loaded;
			np.IsVisible = np.Loaded;
		}
	}
}
