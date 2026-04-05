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
using System.Collections;
using Avalonia.Controls;
using SimPe.Interfaces.Files;
using SimPe.Interfaces.Wrapper;
using SimPe.Data;
using SimPe.PackedFiles.Wrapper;
using SimPe.Interfaces.Plugin;

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// Avalonia port of the WinForms Elements2 form.
	/// Acts as a data-container/panel-host for Cpf and Nref UIs.
	/// </summary>
	public class Elements2 : UserControl
	{
		internal Button   btprev      = new Button { Content = "Preview", IsVisible = false };
		internal ComboBox cbtype      = new ComboBox();
		internal TextBox  rtbcpfname  = new TextBox { Background = Avalonia.Media.Brushes.White, MinHeight = 0, Padding = new Avalonia.Thickness(4, 2) };
		internal TextBox  rtbcpf      = new TextBox { Background = Avalonia.Media.Brushes.White, AcceptsReturn = true, MinHeight = 0, Padding = new Avalonia.Thickness(4, 2), Height = 120 };
		private  Button   btcpfcommit = new Button { Content = "Commit", Padding = new Avalonia.Thickness(6, 2) };
		internal ListBox  lbcpf       = new ListBox();
		internal TextBox  tbNref      = new TextBox { Background = Avalonia.Media.Brushes.White };
		internal TextBox  tbnrefhash  = new TextBox { IsReadOnly = true, Background = Avalonia.Media.Brushes.White };
		private  Button   deleteButton = new Button { Content = "delete", Padding = new Avalonia.Thickness(0), Background = Avalonia.Media.Brushes.Transparent, BorderThickness = new Avalonia.Thickness(0), Foreground = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromRgb(0, 80, 180)), Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Hand) };
		internal Panel    NrefPanel   = new Panel();
		internal Panel    CpfPanel    = new Panel();
		internal Button   llcpfadd    = new Button { Content = "add", Padding = new Avalonia.Thickness(0), Background = Avalonia.Media.Brushes.Transparent, BorderThickness = new Avalonia.Thickness(0), Foreground = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromRgb(0, 80, 180)), Cursor = new Avalonia.Input.Cursor(Avalonia.Input.StandardCursorType.Hand) };
		internal Button   llcpfchange = new Button { Content = "Change", IsVisible = false };
		internal Button   linkLabel1  = new Button { Content = "Commit" };

		internal IFileWrapperSaveExtension wrapper;

		public Elements2()
		{
			btprev.IsEnabled = true;
			btprev.Padding   = new Avalonia.Thickness(6, 2);

			// Compact ListBox items
			var compactTheme = new Avalonia.Styling.ControlTheme(typeof(ListBoxItem));
			compactTheme.Setters.Add(new Avalonia.Styling.Setter(ListBoxItem.PaddingProperty, new Avalonia.Thickness(4, 1)));
			lbcpf.ItemContainerTheme = compactTheme;

			cbtype.MinHeight = 0;
			cbtype.Padding   = new Avalonia.Thickness(6, 2);
			cbtype.Background = Avalonia.Media.Brushes.White;

			// Right-side editor: Name, Type, Value fields + add/delete + buttons
			var nameLabel  = new TextBlock { Text = "Name:",  VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center, Margin = new Avalonia.Thickness(0, 0, 0, 2) };
			var typeLabel  = new TextBlock { Text = "Type:",  VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center, Margin = new Avalonia.Thickness(0, 4, 0, 2) };
			var valueLabel = new TextBlock { Text = "Value:", VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center, Margin = new Avalonia.Thickness(0, 4, 0, 2) };

			var addDelRow = new Avalonia.Controls.StackPanel {
				Orientation = Avalonia.Layout.Orientation.Horizontal,
				Spacing = 8,
				HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
				Margin = new Avalonia.Thickness(0, 4, 0, 0)
			};
			addDelRow.Children.Add(llcpfadd);
			addDelRow.Children.Add(deleteButton);

			var buttonRow = new Avalonia.Controls.StackPanel {
				Orientation = Avalonia.Layout.Orientation.Horizontal,
				Spacing = 4,
				HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
				Margin = new Avalonia.Thickness(0, 6, 0, 0)
			};
			buttonRow.Children.Add(btprev);
			buttonRow.Children.Add(btcpfcommit);

			var editorInner = new Avalonia.Controls.StackPanel {
				Margin = new Avalonia.Thickness(6),
				Spacing = 0,
				Children = { nameLabel, rtbcpfname, typeLabel, cbtype, valueLabel, rtbcpf, addDelRow, buttonRow }
			};

			var editorHeader = new Avalonia.Controls.Border {
				Background = new Avalonia.Media.LinearGradientBrush {
					StartPoint = new Avalonia.RelativePoint(0, 0.5, Avalonia.RelativeUnit.Relative),
					EndPoint   = new Avalonia.RelativePoint(1, 0.5, Avalonia.RelativeUnit.Relative),
					GradientStops = { new Avalonia.Media.GradientStop(Avalonia.Media.Color.FromArgb(220, 60, 60, 80), 0.0), new Avalonia.Media.GradientStop(Avalonia.Media.Color.FromArgb(200, 80, 80, 110), 1.0) }
				},
				Child = new TextBlock { Text = "Property", Foreground = Avalonia.Media.Brushes.White, FontSize = 11, FontWeight = Avalonia.Media.FontWeight.SemiBold, Margin = new Avalonia.Thickness(6, 3) }
			};

			var editor = new Avalonia.Controls.Border {
				VerticalAlignment   = Avalonia.Layout.VerticalAlignment.Top,
				Background          = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromRgb(220, 228, 238)),
				BorderBrush         = new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.FromRgb(170, 185, 205)),
				BorderThickness     = new Avalonia.Thickness(1),
				CornerRadius        = new Avalonia.CornerRadius(3),
				Margin              = new Avalonia.Thickness(6, 0, 0, 0),
				MinWidth            = 240,
				ClipToBounds        = true,
				Child = new Avalonia.Controls.StackPanel { Children = { editorHeader, editorInner } }
			};

			// 2-column grid: listbox fills left, editor auto-sizes right
			var grid = new Avalonia.Controls.Grid();
			grid.ColumnDefinitions.Add(new ColumnDefinition(new Avalonia.Controls.GridLength(1, Avalonia.Controls.GridUnitType.Star)));
			grid.ColumnDefinitions.Add(new ColumnDefinition(Avalonia.Controls.GridLength.Auto));
			Grid.SetColumn(lbcpf,  0);
			Grid.SetColumn(editor, 1);
			grid.Children.Add(lbcpf);
			grid.Children.Add(editor);

			// Dark gradient header bar — "CPF Editor" left, Commit button right
			var headerLabel = new TextBlock {
				Text = "CPF Editor",
				Foreground = Avalonia.Media.Brushes.White,
				FontWeight = Avalonia.Media.FontWeight.SemiBold,
				FontSize = 12,
				VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
				Margin = new Avalonia.Thickness(8, 0)
			};
			var headerContent = new Avalonia.Controls.DockPanel { LastChildFill = true };
			headerContent.Children.Add(headerLabel);

			var headerBar = new Avalonia.Controls.Border {
				Background = new Avalonia.Media.LinearGradientBrush {
					StartPoint = new Avalonia.RelativePoint(0, 0, Avalonia.RelativeUnit.Relative),
					EndPoint   = new Avalonia.RelativePoint(1, 0, Avalonia.RelativeUnit.Relative),
					GradientStops = {
						new Avalonia.Media.GradientStop(Avalonia.Media.Color.FromRgb(100, 116, 140), 0.0),
						new Avalonia.Media.GradientStop(Avalonia.Media.Color.FromRgb( 80,  96, 120), 1.0)
					}
				},
				MinHeight = 28,
				Child = headerContent
			};

			var dock = new Avalonia.Controls.DockPanel();
			Avalonia.Controls.DockPanel.SetDock(headerBar, Avalonia.Controls.Dock.Top);
			dock.Children.Add(headerBar);
			dock.Children.Add(grid);

			CpfPanel.Children.Add(dock);

			// Wire events
			btcpfcommit.Click       += CpfCommit;
			lbcpf.SelectionChanged  += CpfItemSelect;
			llcpfadd.Click          += AddCpf;
			llcpfchange.Click       += CpfChange;
			deleteButton.Click      += DeleteCpf;
			tbNref.TextChanged      += tbnref_TextChanged;
			linkLabel1.Click        += NrefCommit;
			btprev.Click            += btprev_Click;
			rtbcpfname.TextChanged  += CpfAutoChange;
			rtbcpf.TextChanged      += CpfAutoChange;
			cbtype.SelectionChanged += CpfAutoChange;
		}

		public void Dispose() { }

		#region CPF
		private void CpfItemSelect(object sender, SelectionChangedEventArgs e)
		{
            if (rtbcpfname.Tag != null) return;
            this.llcpfchange.IsEnabled = false;
			if (this.lbcpf.SelectedIndex < 0) return;
			this.llcpfchange.IsEnabled = true;

			rtbcpfname.Tag = true;
			try
			{
				CpfItem item = (CpfItem)lbcpf.Items[lbcpf.SelectedIndex];
				this.rtbcpfname.Text = item.Name;
				for (int i = 0; i < cbtype.ItemCount; i++)
				{
					cbtype.SelectedIndex = -1;
					Data.MetaData.DataTypes type = (Data.MetaData.DataTypes)cbtype.Items[i];
					if (type == item.Datatype)
					{
						cbtype.SelectedIndex = i;
						break;
					}
				}

				switch (item.Datatype)
				{
					case Data.MetaData.DataTypes.dtSingle:
					{
						rtbcpf.Text = item.SingleValue.ToString();
						break;
					}
					case Data.MetaData.DataTypes.dtInteger:
					{
						rtbcpf.Text = "0x" + Helper.HexString((uint)item.IntegerValue);
						break;
					}
					case Data.MetaData.DataTypes.dtUInteger:
					{
						rtbcpf.Text = "0x" + Helper.HexString((uint)item.UIntegerValue);
						break;
					}
					case Data.MetaData.DataTypes.dtBoolean:
					{
						rtbcpf.Text = item.BooleanValue ? "1" : "0";
						break;
					}
					default:
					{
						rtbcpf.Text = item.StringValue;
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errconvert"), ex);
			}
			finally
			{
				rtbcpfname.Tag = null;
			}
		}

		private void CpfChange(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if (cbtype.SelectedIndex < 0) cbtype.SelectedIndex = cbtype.ItemCount - 1;
			CpfItem item;
			if (lbcpf.SelectedIndex < 0) item = new CpfItem();
			else item = (CpfItem)lbcpf.Items[lbcpf.SelectedIndex];

			item.Name = rtbcpfname.Text;
			item.Datatype = (Data.MetaData.DataTypes)cbtype.Items[cbtype.SelectedIndex];

			switch (item.Datatype)
			{
				case Data.MetaData.DataTypes.dtInteger:
				{
					try { item.IntegerValue = Convert.ToInt32(rtbcpf.Text, 16); }
					catch { item.IntegerValue = 0; }
					break;
				}
				case Data.MetaData.DataTypes.dtUInteger:
				{
					try { item.UIntegerValue = Convert.ToUInt32(rtbcpf.Text, 16); }
					catch { item.UIntegerValue = 0; }
					break;
				}
				case Data.MetaData.DataTypes.dtSingle:
				{
					try { item.SingleValue = Convert.ToSingle(rtbcpf.Text); }
					catch { item.SingleValue = 0; }
					break;
				}
				case Data.MetaData.DataTypes.dtBoolean:
				{
					try { item.BooleanValue = (Convert.ToByte(rtbcpf.Text) != 0); }
					catch { item.BooleanValue = false; }
					break;
				}
				default:
				{
					item.StringValue = rtbcpf.Text;
					break;
				}
			}

			if (lbcpf.SelectedIndex < 0) lbcpf.Items.Add(item);
			else lbcpf.Items[lbcpf.SelectedIndex] = item;

			if (wrapper != null) wrapper.Changed = true;
		}

		private void AddCpf(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			lbcpf.SelectedIndex = -1;
			CpfChange(null, null);
			lbcpf.SelectedIndex = lbcpf.ItemCount - 1;
			CpfUpdate();
		}

		private void CpfUpdate()
		{
			Cpf wrp = (Cpf)wrapper;
			CpfItem[] items = new CpfItem[lbcpf.ItemCount];
			for (int i = 0; i < items.Length; i++) items[i] = (CpfItem)lbcpf.Items[i];
			wrp.Items = items;
		}

		private void CpfCommit(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			try
			{
				if (this.lbcpf.SelectedIndex >= 0) CpfChange(null, null);
				CpfUpdate();
				Cpf wrp = (Cpf)wrapper;
				wrp.SynchronizeUserData();
				SimPe.Message.Show(Localization.Manager.GetString("commited"), null, MessageBoxButtons.OK);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}
		}

		#endregion

		private void tbnref_TextChanged(object sender, Avalonia.Controls.TextChangedEventArgs e)
		{
			try
			{
				Nref wrp = (Nref)wrapper;
				tbnrefhash.Text = "0x" + Helper.HexString(wrp.Group);
				if (tbNref.Tag == null)
				{
					wrp.FileName = tbNref.Text;
					wrp.Changed = true;
				}
				tbnrefhash.Text = "0x" + Helper.HexString(wrp.Group);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		private void NrefCommit(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			try
			{
				wrapper.SynchronizeUserData();
				SimPe.Message.Show(Localization.Manager.GetString("commited"), null, MessageBoxButtons.OK);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage(Localization.Manager.GetString("errwritingfile"), ex);
			}
		}

		private void CpfAutoChange()
		{
            if (rtbcpfname.Tag != null) return;
			if (lbcpf.SelectedIndex < 0) return;
			rtbcpfname.Tag = true;
			try
			{
				CpfChange(null, null);
			}
			finally
			{
				rtbcpfname.Tag = null;
			}
		}

		internal SimPe.PackedFiles.UserInterface.CpfUI.ExecutePreview fkt;
		private void btprev_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if (fkt == null) return;
			try
			{
				Cpf cpf = (Cpf)wrapper;
				fkt(cpf, cpf.Package);
			}
			catch (Exception ex)
			{
				Helper.ExceptionMessage("", ex);
			}
		}

		private void DeleteCpf(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if (lbcpf.SelectedIndex < 0) return;
			CpfItem item = (CpfItem)lbcpf.Items[lbcpf.SelectedIndex];
			lbcpf.Items.Remove(item);
			CpfUpdate();
			wrapper.Changed = true;
		}

		private void CpfAutoChange(object sender, Avalonia.Controls.TextChangedEventArgs e)
		{
			CpfAutoChange();
		}

		private void CpfAutoChange(object sender, SelectionChangedEventArgs e)
		{
			CpfAutoChange();
		}
	}
}
