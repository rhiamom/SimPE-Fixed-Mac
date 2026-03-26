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
using Avalonia.Input;
using Avalonia.Interactivity;
using SimPe.Interfaces.Plugin;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for MyPackedFileForm.
	/// </summary>
	public class RefFileForm : Avalonia.Controls.Window
	{
        internal Avalonia.Controls.Panel wrapperPanel;
        private SimPe.Windows.Forms.WrapperBaseControl panel3;
		internal Avalonia.Controls.ListBox lblist;
		private Avalonia.Controls.Border gbtypes;
		private Avalonia.Controls.Panel pntypes;
		internal Avalonia.Controls.TextBox tbsubtype;
		internal Avalonia.Controls.TextBox tbinstance;
		private Avalonia.Controls.TextBlock label11;
		internal Avalonia.Controls.TextBox tbtype;
		private Avalonia.Controls.TextBlock label8;
		private Avalonia.Controls.TextBlock label9;
		private Avalonia.Controls.TextBlock label10;
		internal Avalonia.Controls.TextBox tbgroup;
		internal Avalonia.Controls.ComboBox cbtypes;
		internal Avalonia.Controls.TextBlock llcommit;
		internal Avalonia.Controls.TextBlock lldelete;
        internal Avalonia.Controls.TextBlock lladd;
		internal Avalonia.Controls.Button btup;
		internal Avalonia.Controls.Button btdown;
		private Avalonia.Controls.Button button4;
		private Avalonia.Controls.Button button2;
        internal Avalonia.Controls.Image pb;
		private Avalonia.Controls.ContextMenu contextMenu1;
		private Avalonia.Controls.MenuItem miAdd;
		internal Avalonia.Controls.MenuItem miRem;
		private System.ComponentModel.IContainer components;
        internal System.Drawing.Image imge;

		public RefFileForm()
		{
			components = null;
			InitializeComponent();
            if (Helper.XmlRegistry.UseBigIcons && lblist?.FontSize != null)
                lblist.FontSize = 14;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
		}

		#region Avalonia AXAML layout placeholder
		private void InitializeComponent()
		{
			// TODO: Avalonia AXAML layout
			wrapperPanel = new Avalonia.Controls.Panel();
			pb = new Avalonia.Controls.Image();
			button2 = new Avalonia.Controls.Button();
			button4 = new Avalonia.Controls.Button();
			btdown = new Avalonia.Controls.Button();
			btup = new Avalonia.Controls.Button();
			gbtypes = new Avalonia.Controls.Border();
			pntypes = new Avalonia.Controls.Panel();
			lladd = new Avalonia.Controls.TextBlock();
			lldelete = new Avalonia.Controls.TextBlock();
			tbsubtype = new Avalonia.Controls.TextBox();
			tbinstance = new Avalonia.Controls.TextBox();
			label11 = new Avalonia.Controls.TextBlock();
			tbtype = new Avalonia.Controls.TextBox();
			label8 = new Avalonia.Controls.TextBlock();
			label9 = new Avalonia.Controls.TextBlock();
			label10 = new Avalonia.Controls.TextBlock();
			tbgroup = new Avalonia.Controls.TextBox();
			cbtypes = new Avalonia.Controls.ComboBox();
			llcommit = new Avalonia.Controls.TextBlock();
			lblist = new Avalonia.Controls.ListBox();
			contextMenu1 = new Avalonia.Controls.ContextMenu();
			miAdd = new Avalonia.Controls.MenuItem();
			miRem = new Avalonia.Controls.MenuItem();
			panel3 = new SimPe.Windows.Forms.WrapperBaseControl();
		}
		#endregion


		/// <summary>
		/// Stores the currently active Wrapper
		/// </summary>
		internal IFileWrapperSaveExtension wrapper = null;

		private void SelectType(object sender, System.EventArgs e)
		{
			if (cbtypes.Tag != null) return;
			tbtype.Text = "0x"+Helper.HexString(((SimPe.Data.TypeAlias)cbtypes.Items[cbtypes.SelectedIndex]).Id);
		}

		private void tbtype_TextChanged(object sender, System.EventArgs e)
		{
			cbtypes.Tag = true;
			Data.TypeAlias a = Data.MetaData.FindTypeAlias(Helper.HexStringToUInt(tbtype.Text));

			this.AutoChange(sender, e);
			int ct=0;
			foreach(Data.TypeAlias i in cbtypes.Items)
			{
				if (i==a)
				{
					cbtypes.SelectedIndex = ct;
					cbtypes.Tag = null;
					return;
				}
				ct++;
			}

			cbtypes.SelectedIndex = -1;
			cbtypes.Tag = null;
		}

		private void SelectFile(object sender, System.EventArgs e)
		{
            if (lblist.SelectedIndex < 0) { llcommit.IsVisible = lldelete.IsVisible = btup.IsEnabled = btdown.IsEnabled = miAdd.IsEnabled = miRem.IsEnabled = false; return; }
            llcommit.IsVisible = lldelete.IsVisible = true; btup.IsEnabled = btdown.IsEnabled = miAdd.IsEnabled = miRem.IsEnabled = true;

			if (tbtype.Tag!=null) return;
			try
			{
				tbtype.Tag = true;
				Interfaces.Files.IPackedFileDescriptor pfd = (Interfaces.Files.IPackedFileDescriptor)lblist.Items[lblist.SelectedIndex];
				this.tbgroup.Text = "0x"+Helper.HexString(pfd.Group);
				this.tbinstance.Text = "0x"+Helper.HexString(pfd.Instance);
				this.tbsubtype.Text = "0x"+Helper.HexString(pfd.SubType);
				this.tbtype.Text = "0x"+Helper.HexString(pfd.Type);

				//get Texture
				if (pfd.GetType()==typeof(RefFileItem))
				{
					RefFile wrp = (RefFile)wrapper;
					SkinChain sc = ((RefFileItem)pfd).Skin;
					SimPe.Plugin.GenericRcol txtr = null;
					if (sc!=null) txtr = sc.TXTR;

					//show the Image
					if (txtr==null)
					{
                        // pb.Source set from imge would need conversion — skipped for now
					}
					else
					{
						MipMap mm = ((ImageData)txtr.Blocks[0]).GetLargestTexture(new System.Drawing.Size(100, 100));
						// TODO: convert System.Drawing.Image to Avalonia bitmap for pb.Source
					}
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
			finally
			{
				tbtype.Tag = null;
			}
		}

		private void ChangeFile(object sender, EventArgs e)
		{
			try
			{
				Packages.PackedFileDescriptor pfd = null;
				if (lblist.SelectedIndex>=0) pfd = (Packages.PackedFileDescriptor)lblist.Items[lblist.SelectedIndex];
				else pfd = new Packages.PackedFileDescriptor();

				pfd.Group = Convert.ToUInt32(this.tbgroup.Text, 16);
				pfd.Instance = Convert.ToUInt32(this.tbinstance.Text, 16);
				pfd.SubType = Convert.ToUInt32(this.tbsubtype.Text, 16);
				pfd.Type = Convert.ToUInt32(this.tbtype.Text, 16);

				if (lblist.SelectedIndex>=0)
				{
					lblist.Items[lblist.SelectedIndex] = pfd;
					try
					{
						RefFileItem rfi = (RefFileItem)pfd;
						rfi.Skin = null;
					}
					catch {}
				}
				else lblist.Items.Add(pfd);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void DeleteFile(object sender, EventArgs e)
		{
			llcommit.IsVisible = false;
			lldelete.IsVisible = false;
			btup.IsEnabled = false;
			btdown.IsEnabled = false;
			miRem.IsEnabled = false;
			if (lblist.SelectedIndex<0) return;
			llcommit.IsVisible = true;
			lldelete.IsVisible = true;
			btup.IsEnabled = true;
			btdown.IsEnabled = true;
			miRem.IsEnabled = true;

			lblist.Items.Remove(lblist.Items[lblist.SelectedIndex]);
		}

		private void AddFile(object sender, EventArgs e)
		{
			lblist.SelectedIndex = -1;
			ChangeFile(null, null);
			lblist.SelectedIndex = lblist.Items.Count - 1;
		}

		private void CommitAll(object sender, System.EventArgs e)
		{
			try
			{
				RefFile wrp = (RefFile)wrapper;

				Interfaces.Files.IPackedFileDescriptor[] pfds = new Interfaces.Files.IPackedFileDescriptor[lblist.Items.Count];
				for (int i=0; i<pfds.Length; i++)
				{
					pfds[i] = (Interfaces.Files.IPackedFileDescriptor)lblist.Items[i];
				}

				wrp.Items = pfds;
				wrapper.SynchronizeUserData();
				SimPe.Message.Show(Localization.Manager.GetString("commited"));
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}
		}

		private void MoveUp(object sender, System.EventArgs e)
		{
			if (lblist.SelectedIndex<1) return;

			object pfd = lblist.Items[lblist.SelectedIndex];
			lblist.Items[lblist.SelectedIndex] = lblist.Items[lblist.SelectedIndex-1];
			lblist.Items[lblist.SelectedIndex-1] = pfd;
			lblist.SelectedIndex--;
		}

		private void MoveDown(object sender, System.EventArgs e)
		{
			if (lblist.SelectedIndex<0) return;
			if (lblist.SelectedIndex>lblist.Items.Count-2) return;

			object pfd = lblist.Items[lblist.SelectedIndex];
			lblist.Items[lblist.SelectedIndex] = lblist.Items[lblist.SelectedIndex+1];
			lblist.Items[lblist.SelectedIndex+1] = pfd;
			lblist.SelectedIndex++;
		}

		private void AutoChange(object sender, System.EventArgs e)
		{
			if (tbtype.Tag != null) return;

			tbtype.Tag = true;
			if (lblist.SelectedIndex>=0) ChangeFile(null, null);
			tbtype.Tag = null;
		}

		private void ChooseFile(object sender, System.EventArgs e)
		{
			try
			{
				RefFile wrp = (RefFile)wrapper;
                Interfaces.Files.IPackedFileDescriptor pfd = FileSelect.Execute();
				if (pfd!=null)
				{
					tbtype.Tag = true;
					this.tbgroup.Text = "0x"+Helper.HexString(pfd.Group);
					this.tbinstance.Text = "0x"+Helper.HexString(pfd.Instance);
					this.tbsubtype.Text = "0x"+Helper.HexString(pfd.SubType);
					this.tbtype.Text = "0x"+Helper.HexString(pfd.Type);
					tbtype.Tag = null;
					this.AutoChange(sender, e);
				}
			}
			catch (Exception) {}
			finally
			{
				tbtype.Tag = null;
			}
		}

		#region Package Selector
		private void ShowPackageSelector(object sender, System.EventArgs e)
		{
			SimPe.PackageSelectorForm form = new SimPe.PackageSelectorForm();
			form.Execute(((RefFile)wrapper).Package);
		}

		// Avalonia drag/drop uses string data-format keys, not .NET types.
		// The drag source must store the descriptor under this same key.
		private const string PackedFileDescriptorFormat = "SimPe.PackedFileDescriptor";

		private void PackageItemDragEnter(object sender, DragEventArgs e)
		{
			e.DragEffects = e.Data.Contains(PackedFileDescriptorFormat)
				? DragDropEffects.Copy
				: DragDropEffects.None;
		}

		private void PackageItemDrop(object sender, DragEventArgs e)
		{
			try
			{
				var pfd = (Interfaces.Files.IPackedFileDescriptor)e.Data.Get(PackedFileDescriptorFormat);
				lblist.Items.Add(pfd);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}
		#endregion

		private void pb_SizeChanged(object sender, System.EventArgs e)
		{
			// No-op: Avalonia Image doesn't have Width/Height settable the same way
		}

		private void miAdd_Click(object sender, System.EventArgs e)
		{
			AddFile(null, null);
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			DeleteFile(null, null);
		}
	}
}
