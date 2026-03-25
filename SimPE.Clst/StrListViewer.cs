/***************************************************************************
 *   Copyright (C) 2005 by Peter L Jones                                   *
 *   pljones@users.sf.net                                                  *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSweatShop                                 *
 *   Rhiamom@mac.com                                                       *
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

using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using SimPe.PackedFiles.Wrapper;
using SimPe.Data;

namespace SimPe.PackedFiles.UserInterface
{
    /// <summary>
    /// Displays the languages and string items of a Str packed file.
    /// Left pane: TreeView of languages.  Right pane: list of line/title/description rows.
    /// Ported from WinForms UserControl to Avalonia UserControl.
    /// </summary>
    public class StrListViewer : Avalonia.Controls.UserControl
    {
        // ── row model ─────────────────────────────────────────────────────────
        sealed class StrRow
        {
            public string Line        { get; }
            public string Title       { get; }
            public string Description { get; }
            public StrRow(string line, string title, string desc)
            { Line = line; Title = title; Description = desc; }
        }

        // ── controls & state ─────────────────────────────────────────────────
        readonly TreeView  _tree;
        readonly ListBox   _list;
        readonly ObservableCollection<StrLanguage> _languages = new();
        readonly ObservableCollection<StrRow>      _rows      = new();
        Str _wrapper;

        // ── constructor ───────────────────────────────────────────────────────
        public StrListViewer()
        {
            // ── language tree (left pane) ─────────────────────────────────────
            _tree = new TreeView
            {
                Width       = 216,
                ItemsSource = _languages,
                ItemTemplate = new FuncDataTemplate<StrLanguage>(
                    (lang, _) => new TextBlock { Text = lang?.ToString() ?? "" }, true),
                ContextMenu = BuildLangContextMenu(),
            };

            // ── splitter ──────────────────────────────────────────────────────
            var splitter = new GridSplitter
            {
                Width      = 3,
                Background = new SolidColorBrush(Colors.LightGray),
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };

            // ── string list (right pane) ──────────────────────────────────────
            _list = new ListBox
            {
                ItemsSource = _rows,
                ItemTemplate = new FuncDataTemplate<StrRow>(BuildRowTemplate, true),
                ContextMenu  = BuildStrContextMenu(),
            };

            // ── layout ────────────────────────────────────────────────────────
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition(216,            GridUnitType.Pixel));
            grid.ColumnDefinitions.Add(new ColumnDefinition(3,              GridUnitType.Pixel));
            grid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

            Grid.SetColumn(_tree,    0);
            Grid.SetColumn(splitter, 1);
            Grid.SetColumn(_list,    2);
            grid.Children.Add(_tree);
            grid.Children.Add(splitter);
            grid.Children.Add(_list);

            Content = grid;

            _tree.SelectionChanged += TreeSelectionChanged;
        }

        // ── public API ────────────────────────────────────────────────────────

        internal void UpdateGUI(Str wrp)
        {
            _wrapper = wrp;
            _languages.Clear();
            _rows.Clear();
            foreach (StrLanguage l in wrp.Languages)
                _languages.Add(l);
        }

        // ── event handler ─────────────────────────────────────────────────────

        void TreeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _rows.Clear();
            if (_tree.SelectedItem is not StrLanguage lang) return;

            StrItemList items = _wrapper?.LanguageItems(lang);
            if (items == null) return;

            for (int i = 0; i < items.Length; i++)
            {
                StrToken s = items[i];
                _rows.Add(new StrRow(i.ToString(), s.Title, s.Description));
            }
        }

        // ── item template ─────────────────────────────────────────────────────

        static Control BuildRowTemplate(StrRow row, Avalonia.Controls.INameScope _)
        {
            var g = new Grid();
            g.ColumnDefinitions.Add(new ColumnDefinition(36,              GridUnitType.Pixel));
            g.ColumnDefinitions.Add(new ColumnDefinition(246,             GridUnitType.Pixel));
            g.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

            var tbLine  = new TextBlock { Text = row?.Line        ?? "" };
            var tbTitle = new TextBlock { Text = row?.Title       ?? "" };
            var tbDesc  = new TextBlock { Text = row?.Description ?? "" };

            Grid.SetColumn(tbLine,  0);
            Grid.SetColumn(tbTitle, 1);
            Grid.SetColumn(tbDesc,  2);
            g.Children.Add(tbLine);
            g.Children.Add(tbTitle);
            g.Children.Add(tbDesc);
            return g;
        }

        // ── context menus ─────────────────────────────────────────────────────

        static ContextMenu BuildLangContextMenu() => new ContextMenu
        {
            ItemsSource = new MenuItem[]
            {
                new MenuItem { Header = "&Copy",             InputGesture = new KeyGesture(Key.C, KeyModifiers.Control) },
                new MenuItem { Header = "&Paste",            InputGesture = new KeyGesture(Key.V, KeyModifiers.Control) },
                new MenuItem { Header = "Pas&te As\u2026" },
                new MenuItem { Header = "&Set all to these" },
                new MenuItem { Header = "&Add",              InputGesture = new KeyGesture(Key.Insert) },
                new MenuItem { Header = "&Delete",           InputGesture = new KeyGesture(Key.Delete) },
            },
        };

        static ContextMenu BuildStrContextMenu() => new ContextMenu
        {
            ItemsSource = new MenuItem[]
            {
                new MenuItem { Header = "&Edit" },
                new MenuItem { Header = "&Copy",              InputGesture = new KeyGesture(Key.C, KeyModifiers.Control) },
                new MenuItem { Header = "&Paste",             InputGesture = new KeyGesture(Key.V, KeyModifiers.Control) },
                new MenuItem { Header = "&Set in all languages" },
                new MenuItem { Header = "&Add",               InputGesture = new KeyGesture(Key.Insert) },
                new MenuItem { Header = "&Delete",            InputGesture = new KeyGesture(Key.Delete) },
            },
        };
    }
}
