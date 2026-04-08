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
using SimPe.Scenegraph.Compat;

namespace SimPe.Plugin
{
	/// <summary>
	/// Summary description for RenameForm.
	/// </summary>
	public class RenameForm : Avalonia.Controls.Window
	{
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader3;
		private ListView lv;
		private Avalonia.Controls.TextBlock label1;
		private Avalonia.Controls.TextBox tbname;
		private Avalonia.Controls.TextBlock llname;
		private Avalonia.Controls.Button button1;
		private Avalonia.Controls.CheckBox cbv2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public RenameForm()
		{
			InitializeComponent();

			cbv2.IsVisible = Helper.XmlRegistry.HiddenMode;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected void Dispose(bool disposing)
		{
			// base.Dispose(disposing); // Avalonia Window does not have Dispose(bool)
		}

		#region Avalonia layout
		private void InitializeComponent()
		{
			this.Title = "Rename";
			this.Width = 500;
			this.Height = 400;
			this.WindowStartupLocation = Avalonia.Controls.WindowStartupLocation.CenterOwner;

			lv = new ListView();
			columnHeader1 = new ColumnHeader();
			columnHeader2 = new ColumnHeader();
			columnHeader3 = new ColumnHeader();

			label1 = new Avalonia.Controls.TextBlock { Text = "Enter a new unique name for the cloned object:", TextWrapping = Avalonia.Media.TextWrapping.Wrap, Margin = new Avalonia.Thickness(0, 0, 0, 4) };
			tbname = new Avalonia.Controls.TextBox { Margin = new Avalonia.Thickness(0, 0, 0, 4) };
			llname = new Avalonia.Controls.TextBlock { FontSize = 10, Foreground = Avalonia.Media.Brushes.Gray, Margin = new Avalonia.Thickness(0, 0, 0, 8) };
			tbname.TextChanged += (s, e) => { llname.Text = tbname.Text; UpdateNames(s, e); };

			button1 = new Avalonia.Controls.Button { Content = "OK", HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right, MinWidth = 80, Background = Avalonia.Media.Brushes.White, Margin = new Avalonia.Thickness(0, 4, 0, 0) };
			button1.Click += button1_Click;

			cbv2 = new Avalonia.Controls.CheckBox { Content = "University Ready v2", Margin = new Avalonia.Thickness(0, 4, 0, 0) };

			// Build a simple list showing existing resource names
			var lvList = new Avalonia.Controls.ListBox
			{
				MinHeight = 120,
				FontSize = 11,
				Margin = new Avalonia.Thickness(0, 4, 0, 4),
			};
			lv.Tag = lvList; // store reference for populating later

			var root = new Avalonia.Controls.DockPanel { Margin = new Avalonia.Thickness(8) };
			var bottomBar = new Avalonia.Controls.StackPanel { Margin = new Avalonia.Thickness(0, 4, 0, 0) };
			bottomBar.Children.Add(cbv2);
			bottomBar.Children.Add(button1);
			Avalonia.Controls.DockPanel.SetDock(bottomBar, Avalonia.Controls.Dock.Bottom);
			Avalonia.Controls.DockPanel.SetDock(label1, Avalonia.Controls.Dock.Top);
			Avalonia.Controls.DockPanel.SetDock(tbname, Avalonia.Controls.Dock.Top);
			Avalonia.Controls.DockPanel.SetDock(llname, Avalonia.Controls.Dock.Top);

			root.Children.Add(bottomBar);
			root.Children.Add(label1);
			root.Children.Add(tbname);
			root.Children.Add(llname);
			root.Children.Add(lvList);

			this.Content = root;
		}
		#endregion

		/// <summary>
		/// Find the Modelname of the Original Object
		/// </summary>
		/// <param name="package">The Package containing the Data</param>
		/// <returns>The Modelname</returns>
		public static string FindMainOldName(SimPe.Interfaces.Files.IPackageFile package)
		{
			Interfaces.Files.IPackedFileDescriptor[] pfds = package.FindFiles(Data.MetaData.STRING_FILE);
			foreach(Interfaces.Files.IPackedFileDescriptor pfd in pfds)
			{
				if (pfd.Instance == 0x85)
				{
					SimPe.PackedFiles.Wrapper.Str str = new SimPe.PackedFiles.Wrapper.Str();
					str.ProcessData(pfd, package);

					SimPe.PackedFiles.Wrapper.StrItemList sil = str.LanguageItems(1);
					if (sil.Length>1) return sil[1].Title;
					else if (str.Items.Length>1) return str.Items[1].Title;
				}
			}

			pfds = package.FindFiles(0x4C697E5A);
			foreach(Interfaces.Files.IPackedFileDescriptor pfd in pfds)
			{
				SimPe.PackedFiles.Wrapper.Cpf cpf = new SimPe.PackedFiles.Wrapper.Cpf();
				cpf.ProcessData(pfd, package);

				if (cpf.GetSaveItem("modelName").StringValue.Trim()!="") return cpf.GetSaveItem("modelName").StringValue.Trim();

			}

			return "SimPe";
		}

		/// <summary>
		/// Replaces an old unique portion with a new Name
		/// </summary>
		/// <param name="name"></param>
		/// <param name="newunique"></param>
		/// <param name="force">if true, the unique name will be added, even if no unique item was found</param>
		/// <returns></returns>
		public static string ReplaceOldUnique(string name, string newunique, bool force)
		{
			newunique = newunique.Replace("_", ".");
			string[] parts = name.Split("[".ToCharArray(), 2);
			if (parts.Length>1)
			{
				string[] ends = parts[1].Split("]".ToCharArray(), 2);
				if (ends.Length>1) return parts[0]+newunique+ends[1];
			}

			//make sure the uniqe part is added to the ModelName
			if (force)
			{
				parts = name.Split("_".ToCharArray(), 2);

				name = "";
				bool first = true;
				foreach (string s in parts)
				{
					if (!first) name += "_";
					name += s;
					if (first)
					{
						first = false;
						name += "-"+newunique;
					}
				}
			}

			return name;
		}

		/// <summary>
		/// Creates a Name Map
		/// </summary>
		/// <param name="auto"></param>
		/// <param name="package"></param>
		/// <param name="lv"></param>
		/// <param name="username"></param>
		/// <returns></returns>
		public static Hashtable GetNames(bool auto, SimPe.Interfaces.Files.IPackageFile package, ListView lv, string username)
		{
			username = username.Replace("_", ".");

			if (lv!=null) lv.Items.Clear();
            Hashtable ht = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            string old = Hashes.StripHashFromName(FindMainOldName(package).ToLower().Trim());
			if (old.EndsWith("_cres")) old = old.Substring(0, old.Length-5);

			//load all Rcol Files
			foreach (uint type in Data.MetaData.RcolList)
			{
				Interfaces.Files.IPackedFileDescriptor[] pfds = package.FindFiles(type);
				foreach(Interfaces.Files.IPackedFileDescriptor pfd in pfds)
				{
					Rcol rcol = new GenericRcol(null, false);
					rcol.ProcessData(pfd, package);
					string newname = Hashes.StripHashFromName(rcol.FileName.Trim().ToLower());
					if (newname=="") newname="SimPE_dummy_"+username;
					if (old==null) old = "";
					if (old=="") old = " ";
					if (auto)
					{
						string secname = "";
						if (newname.EndsWith("_anim"))
						{
							string mun = username;
							secname = newname.Replace(old, "");
							int pos = secname.IndexOf("-");
							if (pos>=0 && pos<secname.Length-1) pos = secname.IndexOf("-", pos+1);

							if (pos>=0 && pos<secname.Length-1)
								secname = secname.Substring(0, pos+1) + mun + "-" + secname.Substring(pos+1);
							else
								secname = "";
						}
						if (secname=="")
							secname = newname.Replace(old, username);
						if ((secname==newname) && (old!=username.Trim().ToLower())) secname = username+"-"+secname;
						newname = secname;
					}

					if (lv!=null)
					{
						ListViewItem lvi = new ListViewItem(Hashes.StripHashFromName(newname));
						lvi.SubItems.Add(Data.MetaData.FindTypeAlias(type).shortname);
						lvi.SubItems.Add(Hashes.StripHashFromName(rcol.FileName));

						lv.Items.Add(lvi);
					}

					ht[Hashes.StripHashFromName(rcol.FileName.Trim().ToLower())] = Hashes.StripHashFromName(newname);
				}
			}

			return ht;
		}


		protected Hashtable GetReplacementMap()
		{
            Hashtable ht = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            foreach (object o in (System.Collections.IEnumerable)lv.Items)
			{
				ListViewItem lvi = (ListViewItem)o;
				string oldname = lvi.SubItems[2].Text.Trim().ToLower();
				string newname = lvi.Text.Trim();

				string ext = "_"+lvi.SubItems[1].Text.Trim().ToLower();
				if (!newname.ToLower().EndsWith(ext)) newname = newname + ext;

				try
				{
					ht.Add(Hashes.StripHashFromName(oldname), Hashes.StripHashFromName(newname));
				}
				catch (System.ArgumentException ex)
				{
					throw new Warning(ex.Message, "Two or more Resources in the package have the same name, which is not allowed! See http://ambertation.de/simpeforum/viewtopic.php?t=1078 for Details.", ex);
				}
			}

			return ht;
		}

		/// <summary>
		/// Creates a unique Name
		/// </summary>
		/// <returns>a Unique String</returns>
		public static string GetUniqueName()
		{
			return GetUniqueName(false);
		}

		/// <summary>
		/// Creates a unique Name
		/// </summary>
		/// <param name="retnull">Return null, if no GUID-DB-username was available</param>
		/// <returns>a Unique String or null</returns>
		public static string GetUniqueName(bool retnull)
			{
			string uname = Helper.XmlRegistry.Username.Trim();
			if (uname=="")
			{
				if (retnull) return null;
				uname = System.Guid.NewGuid().ToString();
			}
			else
			{
				uname = uname.Replace(" ", "-").Replace("_", "-").Replace("~", "-").Replace("!", ".").Replace("#", ".").Replace("[", "(");
				DateTime now = DateTime.Now;
				string time = now.Day.ToString()+"."+now.Month.ToString()+"."+now.Year.ToString()+"-"+now.Hour.ToString("x")+now.Minute.ToString("x")+now.Second.ToString("x");
				uname += "-"+time;
			}

			return "["+uname.Trim()+"]";
		}

		SimPe.Interfaces.Files.IPackageFile package;
		static string current_unique;
		public static Hashtable Execute(SimPe.Interfaces.Files.IPackageFile package, bool uniquename, ref FixVersion ver)
		{
			var result = ExecuteAsync(package, uniquename, ver).GetAwaiter().GetResult();
			ver = result.Ver;
			return result.Map;
		}

		public class ExecuteResult
		{
			public Hashtable Map;
			public FixVersion Ver;
		}

		public static async System.Threading.Tasks.Task<ExecuteResult> ExecuteAsync(SimPe.Interfaces.Files.IPackageFile package, bool uniquename, FixVersion ver)
		{
			RenameForm rf = new RenameForm();
			rf.ok = false;
			rf.package = package;
			rf.cbv2.IsChecked = (ver==FixVersion.UniversityReady2);

			string old = Hashes.StripHashFromName(FindMainOldName(package).ToLower().Trim());
			current_unique = GetUniqueName();
			if (old.EndsWith("_cres")) old = old.Substring(0, old.Length-5);
			if (uniquename)
			{
				string name = RenameForm.ReplaceOldUnique(old, current_unique, true);
				if (name==old) name = old+current_unique;
				rf.tbname.Text = name;
			}
			else rf.tbname.Text = old;

			GetNames(uniquename, package, rf.lv, rf.tbname.Text);

			// Only show the dialog in advanced/hidden mode; otherwise auto-accept
			if (Helper.XmlRegistry.HiddenMode)
			{
				var owner = (Avalonia.Application.Current?.ApplicationLifetime
					as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime)?.MainWindow;
				await rf.ShowDialog(owner);
			}
			else
			{
				rf.ok = true;
			}

			if (rf.ok)
			{
				if (rf.cbv2.IsChecked == true) ver = FixVersion.UniversityReady2;
				else ver = FixVersion.UniversityReady;
				return new ExecuteResult { Map = rf.GetReplacementMap(), Ver = ver };
			}
			else
				return new ExecuteResult { Map = null, Ver = ver };
		}

		private void UpdateNames(object sender, EventArgs e)
		{
			GetNames(true, package, lv, tbname.Text);
		}

		bool ok;
		private void button1_Click(object sender, RoutedEventArgs e)
		{
			ok = true;
			Close();
		}
	}
}
