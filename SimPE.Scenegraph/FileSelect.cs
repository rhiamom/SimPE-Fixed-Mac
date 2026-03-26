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
using Avalonia.Interactivity;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for FileSelect.
	/// </summary>
	public class FileSelect : Avalonia.Controls.Window
	{
		private Avalonia.Controls.Button button1;
		private Avalonia.Controls.TabControl tc;
		private Avalonia.Controls.Image pb;
		private Avalonia.Controls.TextBlock lbname;
		private Avalonia.Controls.TabItem tabPage1;
		private Avalonia.Controls.TabItem tabPage2;
		private Avalonia.Controls.TreeView tvfemale;
		private Avalonia.Controls.TreeView tvmale;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;


		public FileSelect()
		{
			InitializeComponent();

			Hashtable map = new Hashtable();

			CreateCategoryNodes(ref map, tvfemale, 1);
			CreateCategoryNodes(ref map, tvmale, 2);

			FillCategoryNodes(map);
		}

		/// <summary>
		/// Add the category Nodes to the Treeview
		/// </summary>
		/// <param name="map">a map that can be used to fill thenodes</param>
		/// <param name="tv">the TreeView to fill</param>
		void CreateCategoryNodes(ref Hashtable map, Avalonia.Controls.TreeView tv, uint gender)
		{
			Array cats = System.Enum.GetValues(typeof(Data.SkinCategories));
			Array ages = System.Enum.GetValues(typeof(Data.Ages));

			foreach (Data.Ages a in ages)
			{
				TreeNode node = new TreeNode(a.ToString());
				Hashtable catmap = (Hashtable)map[(uint)a];
				if (catmap == null)
				{
					catmap = new Hashtable();
					map[(uint)a] = catmap;
				}

				node.Nodes.Add(node); // placeholder — TV wiring done in AXAML layout

				foreach (Data.SkinCategories c in cats)
				{
					TreeNode catnode = new TreeNode(c.ToString());
					Hashtable list = (Hashtable)catmap[(uint)c];
					if (list==null)
					{
						list = new Hashtable();
						catmap[(uint)c] = list;
					}
					list[gender] = catnode;

					node.Nodes.Add(catnode);
				}
			}
		}

		void FillCategoryNodes(Hashtable mmap)
		{
			WaitingScreen.Wait();
            WaitingScreen.UpdateMessage("Loading File Table");
			try
			{
				FileTable.FileIndex.Load();
                Interfaces.Scenegraph.IScenegraphFileIndexItem[] items = FileTable.FileIndex.FindFile(Data.MetaData.GZPS, true);
                WaitingScreen.UpdateMessage("Loading Clothing..");
				foreach (Interfaces.Scenegraph.IScenegraphFileIndexItem item in items)
				{
					SimPe.PackedFiles.Wrapper.Cpf skin = new SimPe.PackedFiles.Wrapper.Cpf();
					skin.ProcessData(item);

                    if (skin.GetSaveItem("type").StringValue == "skin" && skin.GetSaveItem("species").UIntegerValue == 1 && skin.GetSaveItem("name").StringValue != "")
					{
						// bool added = false;
						uint skinage = skin.GetSaveItem("age").UIntegerValue;
						uint skincat = skin.GetSaveItem("category").UIntegerValue;
                        if ((skincat & (uint)Data.SkinCategories.Skin) == (uint)Data.SkinCategories.Skin) skincat = (uint)Data.SkinCategories.Skin;
                        if (skincat != 128 && (skin.GetSaveItem("outfit").UIntegerValue == 1 || skin.GetSaveItem("parts").UIntegerValue == 1)) skincat = (uint)Data.SkinCategories.Hair;
						uint skinsex = skin.GetSaveItem("gender").UIntegerValue;
						string name = skin.GetSaveItem("name").StringValue;
						foreach (uint age in mmap.Keys)
						{
							if ((age&skinage)==age)
							{
								Hashtable cats = (Hashtable)mmap[age];
								foreach (uint cat in cats.Keys)
								{
									if ((cat&skincat)==cat)
									{
										Hashtable sex = (Hashtable)cats[cat];
										foreach (uint g in sex.Keys)
										{
											if ((g&skinsex)==g)
											{
												TreeNode parent = (TreeNode)sex[g];
												TreeNode node = new TreeNode(name);
												node.Tag = skin;
												parent.Nodes.Add(node);
												// added = true;
											}
										}
									}
								}
							}
						} //foreach age
					}
				}
			}
			finally
			{
				WaitingScreen.Stop();
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected new void Dispose(bool disposing)
		{
			// base.Dispose(disposing); // Avalonia Window does not have Dispose(bool)
		}

		#region Avalonia AXAML layout placeholder
		private void InitializeComponent()
		{
			// TODO: Avalonia AXAML layout
		}
		#endregion

		bool ok;

        static FileSelect form = null;
		public static SimPe.Interfaces.Files.IPackedFileDescriptor Execute()
		{
			if (form==null) form = new FileSelect();
			return form.DoExecute();
		}

		TreeNode last;
		protected SimPe.Interfaces.Files.IPackedFileDescriptor DoExecute()
		{
			if (lbname != null) lbname.Text = "";
			ok = false;
			last = null;
			if (button1 != null) button1.IsEnabled = false;
			_ = this.ShowDialog(null);

			if ((ok)  && (last!=null))
			{
				SimPe.PackedFiles.Wrapper.Cpf cpf = (SimPe.PackedFiles.Wrapper.Cpf)last.Tag;
				return cpf.FileDescriptor;
			}

			return null;
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			ok = true;
			Close();
		}

		private void Select(object sender, TreeViewEventArgs e)
		{
			if (pb != null) pb.Source = null;
			if (button1 != null) button1.IsEnabled = false;
			if (lbname != null) lbname.Text = "";
			last = null;
			if (e==null) return;
			if (e.Node==null) return;
			if (e.Node.Tag==null) return;
			if (button1 != null) button1.IsEnabled = true;
			last = e.Node;

			SkinChain sc = new SkinChain((SimPe.PackedFiles.Wrapper.Cpf)e.Node.Tag);
			GenericRcol rcol = sc.TXTR;

			if (rcol!=null)
			{
				ImageData id = (ImageData)rcol.Blocks[0];
				System.Drawing.Size pbSize = pb != null ? new System.Drawing.Size(100, 100) : new System.Drawing.Size(100, 100);
				MipMap mm = id.GetLargestTexture(pbSize);
				// TODO: convert System.Drawing.Image to Avalonia bitmap for pb.Source
			}

			if (lbname != null)
			{
				lbname.Text = "Name: "+Helper.lbr+sc.Name+Helper.lbr+Helper.lbr;
				lbname.Text += "Category: "+Helper.lbr+sc.CategoryNames+Helper.lbr+Helper.lbr;
				lbname.Text += "Age: "+Helper.lbr+sc.AgeNames+Helper.lbr+Helper.lbr;
				lbname.Text += "Override: "+Helper.lbr+sc.Cpf.GetSaveItem("override0subset").StringValue+Helper.lbr+Helper.lbr;
				lbname.Text += "Group: "+Helper.lbr+Helper.HexString(sc.Cpf.FileDescriptor.Group)+Helper.lbr+Helper.lbr;
			}
		}
	}
}
