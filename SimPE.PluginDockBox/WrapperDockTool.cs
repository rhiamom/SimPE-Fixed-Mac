/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSweatshop                                 *
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

namespace SimPe.Plugin.Tool.Dockable
{
	/// <summary>
	/// Dockable Tool that displays Wrapper specific Informations
	/// </summary>
	public class WrapperDockTool : SimPe.Interfaces.IDockableTool
	{
		ResourceDock rd;
		public WrapperDockTool(ResourceDock rd)
		{
			this.rd = rd;
		}

		#region IDockableTool Member

        public Ambertation.Windows.Forms.DockPanel GetDockableControl()
		{
			return rd.dcWrapper;
		}

		public event SimPe.Events.ChangedResourceEvent ShowNewResource;

		public void RefreshDock(object sender, SimPe.Events.ResourceEventArgs es)
		{
			if (!es.Empty)
				if (es.HasFileDescriptor)
					{
						SimPe.Interfaces.IWrapper wrp = FileTable.WrapperRegistry.FindHandler(es[0].Resource.FileDescriptor.Type);

						if (wrp!=null) 
						{
                            rd.lbName.IsVisible = rd.lbDesc.IsVisible = rd.lbVersion.IsVisible = rd.lbAuthor.IsVisible = rd.label5.IsVisible = rd.label2.IsVisible = rd.label1.IsVisible = rd.label3.IsVisible = true;
							rd.lbName.Text = wrp.WrapperDescription.Name;
							rd.lbAuthor.Text = wrp.WrapperDescription.Author;
							rd.lbVersion.Text = wrp.WrapperDescription.Version.ToString();
							rd.lbDesc.Text = wrp.WrapperDescription.Description;
							rd.pb.Image = null; // wrp.WrapperDescription.Icon is System.Drawing.Image; pb.Image is SKBitmap — skip
							// if (rd.pb.Image!=null) rd.lbName.Left = rd.pb.Right+4; // layout n/a in Avalonia
							// else rd.lbName.Left = rd.pb.Left; // layout n/a in Avalonia
							return;
						}
					}
            rd.lbName.Text = rd.lbAuthor.Text = rd.lbVersion.Text = rd.lbDesc.Text = "";
            rd.lbName.IsVisible = rd.lbDesc.IsVisible = rd.lbVersion.IsVisible = rd.lbAuthor.IsVisible = rd.label5.IsVisible = rd.label2.IsVisible = rd.label1.IsVisible = rd.label3.IsVisible = false;
            rd.pb.Image = null;
			// rd.lbName.Left = rd.pb.Left;
		}

		#endregion

		#region IToolPlugin Member

		public override string ToString()
		{
			return rd.dcWrapper.Text;
		}

		#endregion

		#region IToolExt Member

		public int Shortcut
		{
			get
			{
				return 0;
			}
		}

		public object Icon
		{
			get
			{
				return rd.dcWrapper.TabImage;
			}
		}	

		public virtual bool Visible 
		{
			get { return GetDockableControl().IsDocked ||  GetDockableControl().IsFloating; }
		}

		#endregion
	}
}
