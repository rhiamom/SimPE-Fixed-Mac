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
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using SimPe.Interfaces.Plugin;
using SimPe.PackedFiles.Wrapper;
using SimPe.Scenegraph.Compat;
using Avalonia.Controls;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for NgbhForm.
	/// </summary>
	public class NgbhForm : IDisposable
	{

		public NgbhForm()
		{
			InitializeComponent();

			this.cbtype.SelectedIndex = cbtype.Items.Count-1;

			SimPe.RemoteControl.HookToMessageQueue(0x4E474248, new SimPe.RemoteControl.ControlEvent(ControlEvent));
		}

		protected void ControlEvent(object sender, SimPe.RemoteControl.ControlEventArgs e)
		{
			object[] os = e.Items as object[];
			if (os!=null)
			{
				this.cbtype.SelectedIndex = (int)((Data.NeighborhoodSlots)os[1]);
				uint inst = (uint)os[0];
				foreach (ListViewItem lvi in this.lv.Items)
				{

					PackedFiles.Wrapper.SDesc sdesc = lvi.Tag as PackedFiles.Wrapper.SDesc;
					if (sdesc.FileDescriptor.Instance == inst)
					{
						lvi.Selected = true;
						lvi.EnsureVisible();
					} else lvi.Selected = false;
				}

				lv.Refresh();
			}
		}

		public void Dispose()
		{
			SimPe.RemoteControl.UnhookFromMessageQueue(0x4E474248, new SimPe.RemoteControl.ControlEvent(ControlEvent));
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.ngbhPanel  = new StackPanel();
            this.cbtype     = new ComboBox();
            this.lbname     = new TextBlock();
            this.button1    = new Button { Content = "Commit" };
            this.gbmem      = new GroupBox();
            this.cbown      = new SortableComboBox();
            this.tbval      = new TextBox();
            this.label6     = new TextBlock();
            this.tbUnk      = new TextBox();
            this.label5     = new TextBlock();
            this.btdown     = new Button { Content = "Down" };
            this.btup       = new Button { Content = "Up" };
            this.linkLabel2 = new Button { Content = "I Own" };
            this.lbmem      = new ListView();
            this.memilist   = new ImageList();
            this.tbown      = new TextBox();
            this.label4     = new TextBlock();
            this.lladd      = new Button { Content = "Add" };
            this.linkLabel1 = new Button { Content = "Delete" };
            this.tbsubid    = new TextBox();
            this.cbsub      = new SortableComboBox();
            this.tbsub      = new TextBox();
            this.label3     = new TextBlock();
            this.cbguid     = new SortableComboBox();
            this.tbguid     = new TextBox();
            this.label2     = new TextBlock();
            this.cbaction   = new CheckBox();
            this.cbvis      = new CheckBox();
            this.tbFlag     = new TextBox();
            this.label1     = new TextBlock();
            this.pb         = new PictureBox();
            this.lbdata     = new TextBox();
            this.lv         = new ListView();
            this.ilist      = new ImageList();
            this.panel2     = new StackPanel();
            this.label27    = new TextBlock();

            // cbtype items
            this.cbtype.Items.Add("Sims");
            this.cbtype.Items.Add("Sims Intern");
            this.cbtype.Items.Add("Families");
            this.cbtype.Items.Add("Families Intern");
            this.cbtype.Items.Add("Lots");
            this.cbtype.Items.Add("Lots Intern");
            this.cbtype.SelectionChanged += (s, e) => this.SelectSim(s, EventArgs.Empty);

            // button1
            this.button1.Click += (s, e) => this.Commit(s, EventArgs.Empty);

            // linkLabel2
            this.linkLabel2.Click += (s, e) => IOwn(s, System.EventArgs.Empty);

            // lbmem
            this.lbmem.HideSelection = false;
            this.lbmem.MultiSelect = false;
            this.lbmem.UseCompatibleStateImageBehavior = false;
            this.lbmem.View = View.List;
            this.lbmem.SelectedIndexChanged += new System.EventHandler(this.SelectMemory);

            // memilist
            this.memilist.ColorDepth = ColorDepth.Depth32Bit;

            // lladd / linkLabel1
            this.lladd.Click      += (s, e) => AddItem(s, System.EventArgs.Empty);
            this.linkLabel1.Click += (s, e) => DeleteItem(s, System.EventArgs.Empty);

            // cbown / cbsub / cbguid
            this.cbown.SelectionChanged  += (s, e) => this.ChgOwnerItem(s, EventArgs.Empty);
            this.cbsub.SelectionChanged  += (s, e) => this.ChgSubjectItem(s, EventArgs.Empty);
            this.cbguid.SelectionChanged += (s, e) => this.ChgGuidItem(s, EventArgs.Empty);

            // textbox events
            this.tbval.TextChanged   += (s, e) => this.tbval_TextChanged(s, EventArgs.Empty);
            this.tbUnk.TextChanged   += (s, e) => this.tbUnk_TextChanged(s, EventArgs.Empty);
            this.tbown.TextChanged   += (s, e) => this.ChgOwner(s, EventArgs.Empty);
            this.tbsub.TextChanged   += (s, e) => this.ChgSubject(s, EventArgs.Empty);
            this.tbsubid.TextChanged += (s, e) => this.ChgSubjectID(s, EventArgs.Empty);
            this.tbguid.TextChanged  += (s, e) => this.ChgGuid(s, EventArgs.Empty);
            this.tbFlag.TextChanged  += (s, e) => this.ChgFlag(s, EventArgs.Empty);
            this.lbdata.TextChanged  += (s, e) => this.ChgData(s, EventArgs.Empty);

            // checkbox events
            this.cbaction.IsCheckedChanged += (s, e) => this.ChgFlags(s, EventArgs.Empty);
            this.cbvis.IsCheckedChanged    += (s, e) => this.ChgFlags(s, EventArgs.Empty);

            // btdown / btup
            this.btdown.Click += (s, e) => this.ItemDown(s, EventArgs.Empty);
            this.btup.Click   += (s, e) => this.ItemUp(s, EventArgs.Empty);

            // lv
            this.lv.HideSelection = false;
            this.lv.LargeImageList = this.ilist;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.SelectedIndexChanged += new System.EventHandler(this.SelectSim);

            // ilist
            this.ilist.ColorDepth = ColorDepth.Depth32Bit;
		}
		#endregion

		private StackPanel panel2;
		private TextBlock label27;
		internal StackPanel ngbhPanel;
		internal ListView lv;
		internal ImageList ilist;
		private TextBlock label1;
		private TextBox tbFlag;
		private CheckBox cbvis;
		private CheckBox cbaction;
		private TextBox tbguid;
		private TextBlock label2;
		internal SortableComboBox cbguid;
		internal SortableComboBox cbsub;
		private TextBox tbsub;
		private TextBlock label3;
		private TextBox lbdata;
		private Button button1;
		private TextBox tbsubid;
		internal GroupBox gbmem;
		private Button linkLabel1;
		private Button lladd;
		private PictureBox pb;
		internal SortableComboBox cbown;
		private TextBox tbown;
		private TextBlock label4;
		private TextBlock lbname;
		private ImageList memilist;
		internal ListView lbmem;
		private Button linkLabel2;
		internal Button btdown;
		internal Button btup;
		internal ComboBox cbtype;
		private TextBlock label5;
		private TextBox tbUnk;
		private TextBox tbval;
		private TextBlock label6;

		internal IFileWrapperSaveExtension wrapper;

		protected void AddItem(NgbhItem item)
		{
			if (item==null) return;
			ListViewItem lvi = new ListViewItem();
			lvi.Text = item.ToString();
			lvi.Tag = item;

			if (item.MemoryCacheItem.Icon!=null)
			{
				lvi.ImageIndex = memilist.Images.Count;

				memilist.Images.Add(item.MemoryCacheItem.Icon);
			}

			lbmem.Items.Add(lvi);
		}

		private void tbval_TextChanged(object sender, System.EventArgs e)
		{
			if (tbFlag.Tag!=null) return;
			try
			{
				if (Helper.XmlRegistry.HiddenMode)
					GetSelectedItem().Value = Helper.StringToUInt16(tbval.Text, GetSelectedItem().Value, 16);
				else
					GetSelectedItem().Value = Helper.StringToUInt16(tbval.Text, GetSelectedItem().Value, 10);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void tbUnk_TextChanged(object sender, System.EventArgs e)
		{
			if (tbFlag.Tag!=null) return;
			try
			{
				GetSelectedItem().InventoryNumber = Helper.StringToUInt32(tbUnk.Text, GetSelectedItem().InventoryNumber, 16);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void ItemUp(object sender, System.EventArgs e)
		{
			if (lbmem.SelectedItems.Count==0) return;
			int SelectedIndex = lbmem.SelectedItems[0].Index;
			if (SelectedIndex<1) return;

			ListViewItem lvi = (ListViewItem)lbmem.Items[SelectedIndex];

			lbmem.Items[SelectedIndex] = (ListViewItem)lbmem.Items[SelectedIndex-1].Clone();
			lbmem.Items[SelectedIndex-1] = (ListViewItem)lvi.Clone();
			lbmem.Items[SelectedIndex-1].Selected = true;


			try
			{
				//change also in the Items List
				Ngbh wrp = (Ngbh)wrapper;
				PackedFiles.Wrapper.SDesc sdesc = (PackedFiles.Wrapper.SDesc)lv.SelectedItems[0].Tag;
				NgbhSlot slot = wrp.Sims.GetInstanceSlot(sdesc.Instance);
				NgbhItem i = slot.ItemsB[SelectedIndex- 1];
				slot.ItemsB[SelectedIndex-1] = slot.ItemsB[SelectedIndex];
				slot.ItemsB[SelectedIndex] = i;
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void ItemDown(object sender, System.EventArgs e)
		{
			if (lbmem.SelectedItems.Count==0) return;
			int SelectedIndex = lbmem.SelectedItems[0].Index;
			if (SelectedIndex<0) return;
			if (SelectedIndex>lbmem.Items.Count-2) return;

			ListViewItem lvi = (ListViewItem)lbmem.Items[SelectedIndex];
			lbmem.Items[SelectedIndex] = (ListViewItem)lbmem.Items[SelectedIndex+1].Clone();
			lbmem.Items[SelectedIndex+1] = (ListViewItem)lvi.Clone();
			lbmem.Items[SelectedIndex+1].Selected = true;

			try
			{
				//change also in the Items List
				Ngbh wrp = (Ngbh)wrapper;
				PackedFiles.Wrapper.SDesc sdesc = (PackedFiles.Wrapper.SDesc)lv.SelectedItems[0].Tag;
				NgbhSlot slot = wrp.Sims.GetInstanceSlot(sdesc.Instance);
				NgbhItem i = slot.ItemsB[SelectedIndex + 1];
				slot.ItemsB[SelectedIndex + 1] = slot.ItemsB[SelectedIndex];
				slot.ItemsB[SelectedIndex] = i;
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		protected void UpdateMemItem(NgbhItem item)
		{
			if (lbmem.SelectedItems.Count>0)
			{
				lbmem.SelectedItems[0].Text = item.ToString();

				if ((item.MemoryCacheItem.Icon!=null) && (lbmem.SelectedItems[0].ImageIndex>=0))
				{
					int id = lbmem.SelectedItems[0].ImageIndex;
					lbmem.SelectedItems[0].ImageIndex = -1;
					System.Drawing.Image simg = item.MemoryCacheItem.Icon;
					Bitmap img = new Bitmap(memilist.ImageSize.Width, memilist.ImageSize.Height);
					Graphics gr = Graphics.FromImage(img);
					gr.DrawImage(
						simg,
						0,
						0,
						memilist.ImageSize.Width,
						memilist.ImageSize.Height
						);


					memilist.Images[id] = img;
					pb.Image = simg;
					lbmem.SelectedItems[0].ImageIndex = id;
				}
			}
		}

		private void IOwn(object sender, System.EventArgs e)
		{
			if (lv.SelectedItems.Count==0) return;
			try
			{
				PackedFiles.Wrapper.SDesc sdesc = (PackedFiles.Wrapper.SDesc)lv.SelectedItems[0].Tag;

				cbown.SelectedIndex = 0;
				for(int i=0; i<cbown.Items.Count; i++)
				{
					Interfaces.IAlias a = (Interfaces.IAlias)cbown.Items[i];
					if (a.Tag==null) continue;
					ushort inst = (ushort)a.Tag[0];
					if (inst == sdesc.Instance)
					{
						cbown.SelectedIndex = i;
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void SelectSim(object sender, System.EventArgs e)
		{
			gbmem.Enabled = false;
			memilist.Images.Clear();
			if (lv.SelectedItems.Count < 1) return;
			gbmem.Enabled = true;

			try
			{
				lbname.Text = lv.SelectedItems[0].Text;
				PackedFiles.Wrapper.SDesc sdesc = (PackedFiles.Wrapper.SDesc)lv.SelectedItems[0].Tag;
				lbmem.Items.Clear();

				Ngbh wrp = (Ngbh)wrapper;
				Collections.NgbhItems items = wrp.GetItems((Data.NeighborhoodSlots)cbtype.SelectedIndex, sdesc.Instance);

				if (items!=null)
					foreach (NgbhItem item in items) this.AddItem(item);

				if (lbmem.Items.Count>0) lbmem.Items[0].Selected = true;
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		protected NgbhItem GetSelectedItem()
		{
			if (this.lbmem.SelectedItems.Count==0) return new NgbhItem(new NgbhSlot((Ngbh)wrapper, (Data.NeighborhoodSlots)this.cbtype.SelectedValue));
			return (NgbhItem)lbmem.SelectedItems[0].Tag;
		}



		private void SelectMemory(object sender, System.EventArgs e)
		{
			tbFlag.Tag = true;
			this.cbvis.IsChecked = GetSelectedItem().Flags.IsVisible;
			this.cbaction.IsChecked = GetSelectedItem().Flags.IsControler;
			this.tbFlag.Text = "0x"+Helper.HexString(GetSelectedItem().Flags.Value);

			this.tbUnk.IsEnabled = (uint)GetSelectedItem().ParentSlot.Version >= (uint)NgbhVersion.Nightlife;
			this.tbUnk.Text = "0x"+Helper.HexString(GetSelectedItem().InventoryNumber);
			if (Helper.XmlRegistry.HiddenMode)
				this.tbval.Text = "0x"+Helper.HexString(GetSelectedItem().Value);
			else
				this.tbval.Text = GetSelectedItem().Value.ToString();

			tbFlag.Tag = null;

			tbguid.Tag = true;
			tbguid.Text = "0x"+Helper.HexString(GetSelectedItem().Guid);
			cbguid.SelectedIndex = 0;
			for(int i=0; i<cbguid.Items.Count; i++)
			{
				Interfaces.IAlias a = (Interfaces.IAlias)cbguid.Items[i];
				if (a.Id == GetSelectedItem().Guid)
				{
					cbguid.SelectedIndex = i;
					break;
				}
			}
			tbguid.Tag = null;

			tbsub.Tag = true;
			tbsub.Text = "0x"+Helper.HexString(GetSelectedItem().SimInstance);
			tbsubid.Text = "0x"+Helper.HexString(GetSelectedItem().SimID);
			cbsub.SelectedIndex = 0;
			for(int i=0; i<cbsub.Items.Count; i++)
			{
				Interfaces.IAlias a = (Interfaces.IAlias)cbsub.Items[i];
				if (a.Id == GetSelectedItem().SimID)
				{
					cbsub.SelectedIndex = i;
					break;
				}
			}
			tbsub.Tag = null;

			tbown.Tag = true;
			tbown.Text = "0x"+Helper.HexString(GetSelectedItem().OwnerInstance);
			cbown.SelectedIndex = 0;
			for(int i=0; i<cbown.Items.Count; i++)
			{
				Interfaces.IAlias a = (Interfaces.IAlias)cbown.Items[i];
				if (a.Tag==null) continue;
				ushort inst = (ushort)a.Tag[0];
				if (inst == GetSelectedItem().OwnerInstance)
				{
					cbown.SelectedIndex = i;
					break;
				}
			}
			tbown.Tag = null;

			lbdata.Tag = true;
			lbdata.Text = "";
			foreach (ushort s in GetSelectedItem().Data) lbdata.Text += Helper.HexString(s) + " ";
			lbdata.Tag = null;

			pb.Image = GetSelectedItem().MemoryCacheItem.Icon;
		}

		private void ChgFlags(object sender, System.EventArgs e)
		{
			if (tbFlag.Tag!=null) return;
			tbFlag.Tag = true;
			GetSelectedItem().Flags.IsVisible = this.cbvis.IsChecked == true;
			GetSelectedItem().Flags.IsControler = this.cbaction.IsChecked == true;
			this.tbFlag.Text = "0x"+Helper.HexString(GetSelectedItem().Flags.Value);
			this.UpdateMemItem(GetSelectedItem());
			tbFlag.Tag = null;
		}

		private void ChgFlag(object sender, System.EventArgs e)
		{
			if (tbFlag.Tag!=null) return;
			try
			{
				GetSelectedItem().Flags.Value = Convert.ToUInt16(tbFlag.Text, 16);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void ChgGuidItem(object sender, System.EventArgs e)
		{
			if (tbguid.Tag != null) return;

			if (cbguid.SelectedIndex<1) return;
			Interfaces.IAlias a = (Interfaces.IAlias)cbguid.Items[cbguid.SelectedIndex];
			tbguid.Text = "0x"+Helper.HexString(a.Id);
		}

		private void ChgGuid(object sender, System.EventArgs e)
		{
			if (tbguid.Tag!=null) return;

			try
			{
				GetSelectedItem().Guid = Convert.ToUInt32(tbguid.Text, 16);
				this.UpdateMemItem(GetSelectedItem());
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void ChgSubjectItem(object sender, System.EventArgs e)
		{
			if (tbsub.Tag != null) return;

			if (cbsub.SelectedIndex<1) return;
			Interfaces.IAlias a = (Interfaces.IAlias)cbsub.Items[cbsub.SelectedIndex];
			tbsubid.Text = "0x"+Helper.HexString(a.Id);
			if (a.Tag!=null)
				tbsub.Text = "0x"+Helper.HexString((ushort)a.Tag[0]);
			else
				tbsub.Text = "0x0000";
		}

		private void ChgSubject(object sender, System.EventArgs e)
		{
			if (tbsub.Tag!=null) return;

			try
			{
				GetSelectedItem().SimInstance = Convert.ToUInt16(tbsub.Text, 16);
				this.UpdateMemItem(GetSelectedItem());
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void ChgSubjectID(object sender, System.EventArgs e)
		{
			if (tbsub.Tag!=null) return;

			try
			{
				GetSelectedItem().SimID = Convert.ToUInt32(tbsubid.Text, 16);
				this.UpdateMemItem(GetSelectedItem());
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void ChgOwnerItem(object sender, System.EventArgs e)
		{
			if (tbown.Tag != null) return;

			if (cbown.SelectedIndex<1) return;
			Interfaces.IAlias a = (Interfaces.IAlias)cbown.Items[cbown.SelectedIndex];
			if (a.Tag!=null)
				tbown.Text = "0x"+Helper.HexString((ushort)a.Tag[0]);
			else
				tbown.Text = "0x0000";
		}

		private void ChgOwner(object sender, System.EventArgs e)
		{
			if (tbown.Tag!=null) return;

			try
			{
				GetSelectedItem().OwnerInstance = Convert.ToUInt16(tbown.Text, 16);
				this.UpdateMemItem(GetSelectedItem());
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}


		private void ChgData(object sender, System.EventArgs e)
		{
			if (lbdata.Tag != null) return;

			string[] tokens = lbdata.Text.Split(" ".ToCharArray());
			ushort[] data = new ushort[tokens.Length];

			try
			{
				for(int i=0; i<tokens.Length; i++)
				{
					if (tokens[i].Trim()!="")
						data[i] = Convert.ToUInt16(tokens[i], 16);
					else
						data[i] = 0;
				}

				this.GetSelectedItem().Data = data;
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}

		private void Commit(object sender, System.EventArgs e)
		{
			try
			{
				Ngbh wrp = (Ngbh)wrapper;
				wrp.SynchronizeUserData();
				SimPe.Message.Show(Localization.Manager.GetString("commited"), "Info", MessageBoxButtons.OK);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}
		}

		private void DeleteItem(object sender, System.EventArgs e)
		{
			if (lbmem.SelectedItems.Count==0) return;
			if (cbtype.SelectedIndex%2==1)
				GetSelectedItem().RemoveFromParentB();
			else
				GetSelectedItem().RemoveFromParentA();

			lbmem.Items.Remove(lbmem.SelectedItems[0]);
		}

		private void AddItem(object sender, System.EventArgs e)
		{
			if (lv.SelectedItems.Count<=0) return;

			try
			{
				PackedFiles.Wrapper.SDesc sdesc = (PackedFiles.Wrapper.SDesc)lv.SelectedItems[0].Tag;

				Ngbh wrp = (Ngbh)wrapper;
				NgbhSlot slot = wrp.GetSlots((Data.NeighborhoodSlots)cbtype.SelectedIndex).GetInstanceSlot(sdesc.Instance, true);
				if (slot!=null)
				{
					NgbhItem item = slot.GetItems((Data.NeighborhoodSlots)cbtype.SelectedIndex).AddNew();

					item.PutValue(0x01, 0x07CD);
					item.PutValue(0x02, 0x0007);
					item.PutValue(0x0B, 0);
					item.Flags.IsVisible = true;
					item.Flags.IsControler = false;
					this.AddItem(item);
					lbmem.Items[lbmem.Items.Count-1].Selected = true;
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
		}


	}
}
