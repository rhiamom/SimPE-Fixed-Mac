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
using SimPe.Interfaces.Plugin;
using System.Collections.Generic;

namespace SimPe.Plugin
{
	/// <summary>
	/// This class is used to fill the UI for this FileType with Data
	/// </summary>
    public class StringMapPackedFileUI : SimPe.Windows.Forms.WrapperBaseControl, IPackedFileUI
    {
        private Avalonia.Controls.TextBlock label1 = new Avalonia.Controls.TextBlock();
        internal Avalonia.Controls.TextBox tbfilenm = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock lbfilenm = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock lbdatas = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBox rtbStrings = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBox rtbDatas = new Avalonia.Controls.TextBox();
        private Avalonia.Controls.TextBlock lbsrins = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.TextBlock lbType = new Avalonia.Controls.TextBlock();
        private Avalonia.Controls.Button btshowim = new Avalonia.Controls.Button();
        private Avalonia.Controls.TextBox rtbnames = new Avalonia.Controls.TextBox();

        private void InitializeComponent() { }

        protected new StringMapPackedFileWrapper Wrapper
        {
            get { return base.Wrapper as StringMapPackedFileWrapper; }
        }
        public StringMapPackedFileWrapper TPFW
        {
            get { return (StringMapPackedFileWrapper)Wrapper; }
        }

        #region WrapperBaseControl Member

        bool holde = true;
        Dictionary<uint, string> wallsandfloors = new Dictionary<uint, string>();

        public StringMapPackedFileUI()
        {
            InitializeComponent();
        }

        public override void RefreshGUI()
        {
            holde = true;
            base.RefreshGUI();
            this.CanCommit = false;
            this.rtbnames.IsVisible = false;
            this.btshowim.Content = "Show Names";
//             this.BackgroundImageLocation = new System.Drawing.Point(730, 0);  // no BackgroundImageLocation in Avalonia
            
            this.tbfilenm.Text = Wrapper.FileName;
            if (Wrapper.FileDescriptor.Instance == 13) this.lbType.Text = "Type: Walls";
            else if (Wrapper.FileDescriptor.Instance == 14) this.lbType.Text = "Type: Floor Coverings";
            else this.lbType.Text = "";
            fillimup();
            holde = false;
        }

        public override void OnCommit()
        {
            base.OnCommit();
            TPFW.SynchronizeUserData(true, false);
        }
        #endregion

        #region IPackedFileUI Member
        Avalonia.Controls.Control IPackedFileUI.GUIHandle
        {
            get { return this; }
        }
        #endregion

        #region IDisposable Member

        void IDisposable.Dispose()
        {
            this.TPFW.Dispose();
        }
        #endregion

        private void tbfilenm_TextChanged(object sender, EventArgs e)
        {
            if (holde) return;
            Wrapper.FileName = tbfilenm.Text;
            this.CanCommit = true;
            Wrapper.Changed = true;
        }

        private void rtbStrings_TextChanged(object sender, EventArgs e)
        {
            if (holde) return;
            int i = 0;
            foreach (string clit in rtbStrings.Text.Split(new char[]{'\n'}))
            {
                if (i == Wrapper.Strings.Length) break;
                Wrapper.Strings[i] = clit;
                i++;
            }
            this.CanCommit = true;
            Wrapper.Changed = true;
        }

        private void btshowim_Click(object sender, EventArgs e)
        {
            if (holde) return;
            if (this.rtbnames.IsVisible)
            {
                this.rtbnames.IsVisible = false;
//                 this.BackgroundImageLocation = new System.Drawing.Point(730, 0);  // no BackgroundImageLocation in Avalonia
                this.btshowim.Content = "Show Names";
            }
            else
            {
                holde = true;
                this.rtbnames.Text = "";
                uint tmpy = 0;
                if (wallsandfloors.Count < 1) fildictionary();
                foreach (string clit in rtbStrings.Text.Split(new char[]{'\n'}))
                {
                    if (clit.Length < 9)
                    {
                        try
                        {
                            tmpy = Helper.HexStringToUInt(clit);
                            if (wallsandfloors.ContainsKey(tmpy) && tmpy > 0)
                                rtbnames.Text += wallsandfloors[tmpy] + "\r\n";
                            else
                                rtbnames.Text += clit + "\r\n";
                        }
                        catch { rtbnames.Text += clit + "\r\n"; }
                    }
                    else
                        rtbnames.Text += clit + "\r\n";
                }
//                 this.BackgroundImageLocation = new System.Drawing.Point(930, 0);  // no BackgroundImageLocation in Avalonia
                this.rtbnames.IsVisible = true;
                this.btshowim.Content = "Hide Names";
                holde = false;
            }
        }

        private void fillimup()
        {
            rtbStrings.Text = rtbDatas.Text = "";
            for (int i = 0; i < Wrapper.Strings.Length; i++)
            {
                rtbStrings.Text += Wrapper.Strings[i] + "\r\n";
                rtbDatas.Text += "index " + Convert.ToString(Wrapper.Datas[i]) + " - data 0x" + Helper.HexString(Wrapper.TyPes[i]) + "\r\n";
            }
        }

        private void fildictionary()
        {
            wallsandfloors.Add(0x00000000, "none"); //ensure dictionary is no longer empty even if none of the catpatterns are available
            FileTable.FileIndex.Load();
            Interfaces.Scenegraph.IScenegraphFileIndexItem[] items = FileTable.FileIndex.FindFile(0xCCA8E925, true);
            Wait.SubStart(items.Length);
            Wait.Message = "Loading Wall & Floor Names";
            foreach (Interfaces.Scenegraph.IScenegraphFileIndexItem item in items)
            {
                try
                {
                    SimPe.PackedFiles.Wrapper.Cpf colour = new SimPe.PackedFiles.Wrapper.Cpf();
                    colour.ProcessData(item);                    
                        if (!wallsandfloors.ContainsKey(colour.GetSaveItem("guid").UIntegerValue))
                    wallsandfloors.Add(colour.GetSaveItem("guid").UIntegerValue, colour.GetSaveItem("name").StringValue);
                }
                catch { }
                Wait.Progress +=1;
            }
            Wait.SubStop();
        }
    }
}
