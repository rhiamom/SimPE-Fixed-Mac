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
using System.Collections.Generic;
using System.IO;
using Avalonia.Controls;
using Avalonia.Layout;

namespace SimPe.Plugin
{
    /// <summary>
    /// Summary description for Form.
    /// </summary>
    public class LanguageExtrator : Avalonia.Controls.Window
    {
        private Avalonia.Controls.ComboBox Language;
        private Avalonia.Controls.StackPanel pntheme;
        private Avalonia.Controls.ProgressBar Progress;
        private Avalonia.Controls.TextBlock lbselect;
        private Avalonia.Controls.TextBlock lbdone;
        private Avalonia.Controls.Button btGo;
        private Avalonia.Controls.Button btCome;
        private Avalonia.Controls.Button btclean;

        private SimPe.Packages.GeneratableFile package;
        private List<String> languageString;
        private byte currentLanguage = 1;
        private bool okay = false;

        private void BuildLayout()
        {
            Language = new Avalonia.Controls.ComboBox();
            Language.SelectionChanged += (s, e) => Language_SelectedIndexChanged(s, EventArgs.Empty);

            pntheme = new Avalonia.Controls.StackPanel { Orientation = Orientation.Vertical };

            Progress = new Avalonia.Controls.ProgressBar { Minimum = 0, Maximum = 100, Value = 0 };

            lbdone = new Avalonia.Controls.TextBlock { Text = "All Done!", IsVisible = false };
            lbselect = new Avalonia.Controls.TextBlock { Text = "Select the Language to\nExtract from or Import to" };

            btGo = new Avalonia.Controls.Button { Content = "Extract" };
            btGo.Click += (s, e) => btGo_Click(s, EventArgs.Empty);

            btCome = new Avalonia.Controls.Button { Content = "Import" };
            btCome.Click += (s, e) => btCome_Click(s, EventArgs.Empty);

            btclean = new Avalonia.Controls.Button { Content = "Clean All" };
            btclean.Click += (s, e) => btclean_Click(s, EventArgs.Empty);

            pntheme.Children.Add(lbselect);
            pntheme.Children.Add(Language);
            pntheme.Children.Add(btGo);
            pntheme.Children.Add(btCome);
            pntheme.Children.Add(btclean);
            pntheme.Children.Add(lbdone);
            pntheme.Children.Add(Progress);

            Title = "Single Language Extractor / Importer";
            Content = pntheme;
        }

        public LanguageExtrator()
        {
            BuildLayout();

            languageString = new List<string>(pjse.BhavWiz.readStr(pjse.GS.BhavStr.Languages));
            languageString.RemoveAt(0);
        }

        public Interfaces.Plugin.IToolResult Execute(ref SimPe.Interfaces.Files.IPackedFileDescriptor pfd, ref SimPe.Interfaces.Files.IPackageFile package, Interfaces.IProviderRegistry prov)
        {
            Language.ItemsSource = languageString;
            Language.SelectedIndex = currentLanguage - 1;
            this.package = (SimPe.Packages.GeneratableFile)package;
            ShowDialog(null).GetAwaiter().GetResult();
            return new Plugin.ToolResult(okay, okay);
        }

        private void Language_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int index = ((Avalonia.Controls.ComboBox)sender).SelectedIndex;
            currentLanguage = (byte)(index + 1);
        }

        private void btGo_Click(object sender, EventArgs e)
        {
            Progress.Value = 0;
            this.lbdone.IsVisible = false;
            saveFiles();
            if (okay)
            {
                this.lbdone.Text = "All Done!";
                this.lbdone.IsVisible = true;
            }
        }

        private void btCome_Click(object sender, EventArgs e)
        {
            Progress.Value = 0;
            this.lbdone.IsVisible = false;
            getFiles();
            if (okay)
            {
                this.lbdone.Text = "All Done!";
                this.lbdone.IsVisible = true;
            }
        }

        private void btclean_Click(object sender, EventArgs e)
        {
            cleanim();
            this.lbdone.Text = "Cleaned!";
            this.lbdone.IsVisible = true;
        }

        private void saveFiles()
        {
            string parf;
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() != SimPe.DialogResult.OK) return;
            this.Language.IsEnabled = this.btGo.IsEnabled = this.btCome.IsEnabled = this.btclean.IsEnabled = false;
            string floder = fbd.SelectedPath + "\\" + languageString[currentLanguage-1];
            if (!Directory.Exists(floder)) Directory.CreateDirectory(floder);

            SimPe.Interfaces.Files.IPackedFileDescriptor[] pfdc = this.package.FindFiles(0x43545353); //CTSS
            SimPe.Interfaces.Files.IPackedFileDescriptor[] pfdm = this.package.FindFiles(0x54544173); //Pie String (TTAB)
            SimPe.Interfaces.Files.IPackedFileDescriptor[] pfdt = this.package.FindFiles(0x53545223); //STR#
            Progress.Maximum = pfdc.Length + pfdm.Length + pfdt.Length;

            foreach (Interfaces.Files.IPackedFileDescriptor pfd in pfdc)
            {
                parf = "catalogue-" + Helper.HexString(pfd.Group) + "-" + Helper.HexString(pfd.Instance);
                SimPe.PackedFiles.Wrapper.StrWrapper str = new SimPe.PackedFiles.Wrapper.StrWrapper();
                str.ProcessData(pfd, this.package);
                if (str.HasLanguage(currentLanguage))
                    str.ExportLanguage(currentLanguage, floder + "\\" + parf + ".txt");
                Progress.Value += 1;
            }

            foreach (Interfaces.Files.IPackedFileDescriptor pfd in pfdm)
            {
                parf = "menu-" + Helper.HexString(pfd.Group) + "-" + Helper.HexString(pfd.Instance);
                SimPe.PackedFiles.Wrapper.StrWrapper str = new SimPe.PackedFiles.Wrapper.StrWrapper();
                str.ProcessData(pfd, this.package);
                if (str.HasLanguage(currentLanguage))
                    str.ExportLanguage(currentLanguage, floder + "\\" + parf + ".txt");
                Progress.Value += 1;
            }

            foreach (Interfaces.Files.IPackedFileDescriptor pfd in pfdt)
            {
                parf = "text-" + Helper.HexString(pfd.Group) + "-" + Helper.HexString(pfd.Instance);
                SimPe.PackedFiles.Wrapper.StrWrapper str = new SimPe.PackedFiles.Wrapper.StrWrapper();
                str.ProcessData(pfd, this.package);
                if (str.HasLanguage(currentLanguage))
                    str.ExportLanguage(currentLanguage, floder + "\\" + parf + ".txt");
                Progress.Value += 1;
            }
            okay = true;
        }

        private void getFiles()
        {
            uint tipe;
            uint groop;
            uint insta;
            string twine;
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() != SimPe.DialogResult.OK) return;
            string[] textfiles = Directory.GetFiles(fbd.SelectedPath, "*.txt", SearchOption.TopDirectoryOnly);
            if (textfiles.Length < 1)
            {
                this.lbdone.Text = "None Found!";
                this.lbdone.IsVisible = true;
                return;
            }
            this.Language.IsEnabled = this.btGo.IsEnabled = this.btCome.IsEnabled = this.btclean.IsEnabled = false;
            Progress.Maximum = textfiles.Length;

            foreach (string file in textfiles)
            {
                Progress.Value += 1;
                twine = Path.GetFileNameWithoutExtension(file);
                if (twine.StartsWith("catalogue-")) tipe = 0x43545353;
                else if (twine.StartsWith("menu-")) tipe = 0x54544173;
                else if (twine.StartsWith("text-")) tipe = 0x53545223;
                else break;
                string[] bits = twine.Split(new char[] { '-' });
                groop = Helper.HexStringToUInt(bits[1]);
                insta = Helper.HexStringToUInt(bits[2]);
                SimPe.Interfaces.Files.IPackedFileDescriptor pfd = this.package.FindFile(tipe, 0, groop, insta);
                if (pfd != null)
                {
                    SimPe.PackedFiles.Wrapper.StrWrapper str = new SimPe.PackedFiles.Wrapper.StrWrapper();
                    str.ProcessData(pfd, this.package);
                    str.ImportLanguage(currentLanguage, file);
                    str.SynchronizeUserData();
                }
            }
            okay = true;
        }

        private void cleanim()
        {
            this.Language.IsEnabled = this.btGo.IsEnabled = this.btCome.IsEnabled = this.btclean.IsEnabled = false;
            SimPe.Interfaces.Files.IPackedFileDescriptor[] pfdc = this.package.FindFiles(0x43545353); //CTSS
            SimPe.Interfaces.Files.IPackedFileDescriptor[] pfdm = this.package.FindFiles(0x54544173); //Pie String (TTAB)
            SimPe.Interfaces.Files.IPackedFileDescriptor[] pfdt = this.package.FindFiles(0x53545223); //STR#
            Progress.Maximum = pfdc.Length + pfdm.Length + pfdt.Length;

            foreach (Interfaces.Files.IPackedFileDescriptor pfd in pfdc)
            {
                SimPe.PackedFiles.Wrapper.StrWrapper str = new SimPe.PackedFiles.Wrapper.StrWrapper();
                str.ProcessData(pfd, this.package);
                str.CleanHim();
                str.SynchronizeUserData();
                Progress.Value += 1;
            }

            foreach (Interfaces.Files.IPackedFileDescriptor pfd in pfdm)
            {
                SimPe.PackedFiles.Wrapper.StrWrapper str = new SimPe.PackedFiles.Wrapper.StrWrapper();
                str.ProcessData(pfd, this.package);
                str.CleanHim();
                str.SynchronizeUserData();
                Progress.Value += 1;
            }

            foreach (Interfaces.Files.IPackedFileDescriptor pfd in pfdt)
            {
                SimPe.PackedFiles.Wrapper.StrWrapper str = new SimPe.PackedFiles.Wrapper.StrWrapper();
                str.ProcessData(pfd, this.package);
                str.CleanHim();
                str.SynchronizeUserData();
                Progress.Value += 1;
            }
            this.Language.IsEnabled = this.btGo.IsEnabled = this.btCome.IsEnabled = true;
        }
    }
}
