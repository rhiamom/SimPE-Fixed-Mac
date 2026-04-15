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
using SkiaSharp;
using SimPe.Scenegraph.Compat;
using System.Collections;
using System.ComponentModel;
using TreeNodeCollection = SimPe.Scenegraph.Compat.ITreeNodeCollection;
// Disambiguate DockStyle — defined identically in both simpe.wizardbase and simpe.workspace.plugin

namespace SimPe.Plugin.Tool.Dockable
{
	/// <summary>
	/// Zusammenfassung f�r ObjectWorkshopDock.
	/// </summary>
	public class ObjectWorkshopDock
	{
		private Avalonia.Controls.StackPanel rightSandDock;
		internal Avalonia.Controls.StackPanel dcObjectWorkshop;
		private Avalonia.Controls.StackPanel xpGradientPanel1;
        private System.Windows.Forms.ToolStrip toolBar1;
        private SimPe.Wizards.Wizard wizard1;
		private SimPe.Wizards.WizardStepPanel wizardStepPanel1;
		private Avalonia.Controls.Button button2;
		private Avalonia.Controls.TextBlock label1;
		private Avalonia.Controls.TextBlock label2;
		private Avalonia.Controls.Button button1;
		private SimPe.Wizards.WizardStepPanel wizardStepPanel2;
        private System.Windows.Forms.ToolStripButton biPrev;
        private System.Windows.Forms.ToolStripButton biNext;
        private System.Windows.Forms.ToolStripButton biFinish;
        private Avalonia.Controls.ListBox lb;
        private System.Windows.Forms.ToolStripButton biCatalog;
        private Avalonia.Controls.StackPanel panel1;
		private SimPe.Scenegraph.Compat.TreeView tv;
		private SimPe.Scenegraph.Compat.ImageList ilist;
		private SimPe.Wizards.WizardStepPanel wizardStepPanel3;
		private Avalonia.Controls.StackPanel panel2;
        private Avalonia.Controls.ComboBox cbTask;
        private Avalonia.Controls.TextBlock label3;
		private Ambertation.Windows.Forms.XPTaskBoxSimple gbClone;
		private Ambertation.Windows.Forms.XPTaskBoxSimple gbRecolor;
		private Ambertation.Windows.Forms.TransparentCheckBox cbColorExt;
		private Ambertation.Windows.Forms.TransparentCheckBox cbanim;
		private Ambertation.Windows.Forms.TransparentCheckBox cbwallmask;
		private Ambertation.Windows.Forms.TransparentCheckBox cbparent;
		private Ambertation.Windows.Forms.TransparentCheckBox cbclean;
		private Ambertation.Windows.Forms.TransparentCheckBox cbfix;
		private Ambertation.Windows.Forms.TransparentCheckBox cbdefault;
		private Ambertation.Windows.Forms.TransparentCheckBox cbgid;
		private Avalonia.Controls.Button button3;
		private SimPe.Wizards.WizardStepPanel wizardStepPanel4;
		private System.Windows.Forms.SaveFileDialog sfd;
			private Avalonia.Controls.StackPanel pnWait;
		private Ambertation.Windows.Forms.AnimatedImagelist animatedImagelist1;
		private Avalonia.Controls.TextBlock lbwait;
		private Avalonia.Controls.TextBlock lbfinished;
		private Avalonia.Controls.TextBlock lbfinload;
        private System.Windows.Forms.ToolStripButton biAbort;
        private Avalonia.Controls.TextBlock lberr;
		private Ambertation.Windows.Forms.XPTaskBoxSimple xpTaskBoxSimple1;
		private Ambertation.Windows.Forms.XPTaskBoxSimple xpTaskBoxSimple2;
		private Avalonia.Controls.GridSplitter splitter1;
		private System.ComponentModel.IContainer components;

		public ObjectWorkshopDock()
		{
			//
			// Erforderlich f�r die Windows Form-Designerunterst�tzung
			//
			InitializeComponent();

			wizard1.Start();
			SimPe.ThemeManager tm = SimPe.ThemeManager.Global.CreateChild();
			tm.AddControl(this.xpGradientPanel1);
			tm.AddControl(this.toolBar1);
			tm.AddControl(this.splitter1);

			biFinish.IsVisible = wizard1.FinishEnabled;
			this.biAbort.IsVisible = wizard1.PrevEnabled;
			biNext.Enabled = wizard1.NextEnabled;
			biPrev.Enabled = wizard1.PrevEnabled;

			this.cbTask.SelectedIndex = 0;
		}

		/// <summary>
		/// Die verwendeten Ressourcen bereinigen.
		/// </summary>
		protected virtual void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
		}

		#region Vom Windows Form-Designer generierter Code
		/// <summary>
		/// Erforderliche Methode f�r die Designerunterst�tzung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor ge�ndert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.rightSandDock = new Avalonia.Controls.StackPanel();
			this.dcObjectWorkshop = new Avalonia.Controls.StackPanel();
			this.xpGradientPanel1 = new Avalonia.Controls.StackPanel();
			this.wizard1 = new SimPe.Wizards.Wizard();
			this.wizardStepPanel1 = new SimPe.Wizards.WizardStepPanel();
			this.button2 = new Avalonia.Controls.Button();
			this.label1 = new Avalonia.Controls.TextBlock();
			this.label2 = new Avalonia.Controls.TextBlock();
			this.button1 = new Avalonia.Controls.Button();
			this.wizardStepPanel2 = new SimPe.Wizards.WizardStepPanel();
			this.tv = new SimPe.Scenegraph.Compat.TreeView();
			this.lb = new Avalonia.Controls.ListBox();
			this.splitter1 = new Avalonia.Controls.GridSplitter();
			this.panel1 = new Avalonia.Controls.StackPanel();
			this.wizardStepPanel3 = new SimPe.Wizards.WizardStepPanel();
			this.gbClone = new Ambertation.Windows.Forms.XPTaskBoxSimple();
			this.gbRecolor = new Ambertation.Windows.Forms.XPTaskBoxSimple();
			this.cbColorExt = new Ambertation.Windows.Forms.TransparentCheckBox();
			this.cbgid = new Ambertation.Windows.Forms.TransparentCheckBox();
			this.cbfix = new Ambertation.Windows.Forms.TransparentCheckBox();
			this.cbdefault = new Ambertation.Windows.Forms.TransparentCheckBox();
			this.cbclean = new Ambertation.Windows.Forms.TransparentCheckBox();
			this.cbparent = new Ambertation.Windows.Forms.TransparentCheckBox();
			this.cbwallmask = new Ambertation.Windows.Forms.TransparentCheckBox();
			this.cbanim = new Ambertation.Windows.Forms.TransparentCheckBox();
			this.panel2 = new Avalonia.Controls.StackPanel();
			this.cbTask = new Avalonia.Controls.ComboBox();
			this.label3 = new Avalonia.Controls.TextBlock();
			this.wizardStepPanel4 = new SimPe.Wizards.WizardStepPanel();
			this.pnWait = new Avalonia.Controls.StackPanel();
			this.lberr = new Avalonia.Controls.TextBlock();
			this.lbfinload = new Avalonia.Controls.TextBlock();
			this.lbwait = new Avalonia.Controls.TextBlock();
			this.lbfinished = new Avalonia.Controls.TextBlock();
            this.toolBar1 = new System.Windows.Forms.ToolStrip();
            this.biPrev = new System.Windows.Forms.ToolStripButton();
            this.biNext = new System.Windows.Forms.ToolStripButton();
            this.biFinish = new System.Windows.Forms.ToolStripButton();
            this.biAbort = new System.Windows.Forms.ToolStripButton();
            this.biCatalog = new System.Windows.Forms.ToolStripButton();
			this.ilist = new SimPe.Scenegraph.Compat.ImageList();
			this.sfd = new System.Windows.Forms.SaveFileDialog();

			// Wizard events
			this.wizard1.FinishEnabled = false;
			this.wizard1.NextEnabled = false;
			this.wizard1.PrevEnabled = false;
			this.wizard1.ChangedPrevState += new SimPe.Wizards.WizardHandle(this.wizard1_ChangedPrevState);
			this.wizard1.ChangedFinishState += new SimPe.Wizards.WizardHandle(this.wizard1_ChangedFinishState);
			this.wizard1.ChangedNextState += new SimPe.Wizards.WizardHandle(this.wizard1_ChangedNextState);
			this.wizard1.ShowStep += new SimPe.Wizards.WizardChangeHandle(this.wizard1_ShowStep);

			// WizardStepPanel events
			this.wizardStepPanel2.Activate += new SimPe.Wizards.WizardChangeHandle(this.wizardStepPanel2_Activate);
			this.wizardStepPanel3.Activate += new SimPe.Wizards.WizardChangeHandle(this.wizardStepPanel3_Activate);
			this.wizardStepPanel4.Last = true;
			this.wizardStepPanel4.Activate += new SimPe.Wizards.WizardChangeHandle(this.wizardStepPanel4_Activate);
			this.wizardStepPanel4.Activated += new SimPe.Wizards.WizardStepHandle(this.wizardStepPanel4_Activated);

			// Button click events
			this.button2.Click += (s, e) => this.button2_Click(s, e);
			this.button1.Click += (s, e) => this.button1_Click(s, e);
			this.biPrev.Click += (s, e) => this.Activate_biPrev(s, e);
			this.biNext.Click += (s, e) => this.Activate_biNext(s, e);
			this.biFinish.Click += (s, e) => this.ActivateFinish(s, e);
			this.biAbort.Click += (s, e) => this.biAbort_Activate(s, e);
			this.biCatalog.Click += (s, e) => this.Activate_biCatalog(s, e);

			// List/tree events
			this.lb.SelectionChanged += (s, e) => this.lb_SelectedIndexChanged(s, EventArgs.Empty);
			this.tv.AfterSelect += (s, e) => this.tv_AfterSelect(s, e);

			// ComboBox
			this.cbTask.SelectionChanged += (s, e) => this.cbTask_SelectedIndexChanged(s, EventArgs.Empty);

			// CheckBox initial state
			this.cbColorExt.Checked = true;
			this.cbgid.Checked = true;
			this.cbfix.Checked = true;
			this.cbdefault.Checked = true;
			this.cbclean.Checked = true;
			this.cbwallmask.Checked = true;
			this.biCatalog.Checked = true;

			// ToolStrip items
			this.toolBar1.Items.Add(this.biPrev);
			this.toolBar1.Items.Add(this.biNext);
			this.toolBar1.Items.Add(this.biFinish);
			this.toolBar1.Items.Add(this.biAbort);
			this.toolBar1.Items.Add(this.biCatalog);

			// ImageList
			this.ilist.ColorDepth = SimPe.Scenegraph.Compat.ColorDepth.Depth32Bit;
		}
		#endregion

		private void wizard1_ChangedFinishState(SimPe.Wizards.Wizard sender)
		{
			biFinish.Visible = sender.FinishEnabled;
		}

		private void wizard1_ChangedNextState(SimPe.Wizards.Wizard sender)
		{
			biNext.Enabled = sender.NextEnabled;
		}

		private void wizard1_ChangedPrevState(SimPe.Wizards.Wizard sender)
		{			
			biPrev.Enabled = sender.PrevEnabled;
			this.biAbort.Visible = biPrev.Enabled;
		}

		private void Activate_biPrev(object sender, System.EventArgs e)
		{
			wizard1.GoPrev();
		}

		private void Activate_biNext(object sender, System.EventArgs e)
		{
			wizard1.GoNext();
		}

		private void ActivateFinish(object sender, System.EventArgs e)
		{
			if (wizard1.CurrentStep == this.wizardStepPanel3) Activate_biNext(sender, e);
			else wizard1.Finish();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			Activate_biNext(biNext, e);
		}

		private void wizardStepPanel2_Prepare(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardStepPanel step, int target)
		{
			if (target==step.Index) 
			{
				if (lb.Items.Count==0) 
				{			
					lastselected = null;
					this.ilist.Images.Clear();
					this.ilist.Images.Add(SKBitmap.Decode(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.Tool.Dockable.subitems.png")));
					this.ilist.Images.Add(SKBitmap.Decode(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.Tool.Dockable.nothumb.png")));
					this.ilist.Images.Add(SKBitmap.Decode(this.GetType().Assembly.GetManifestResourceStream("SimPe.Plugin.Tool.Dockable.custom.png")));

					lb.Items.Clear();
					tv.Nodes.Clear();
					tv.Sorted = true;
					tv.ImageList = ilist;
				
					ObjectLoader ol = new ObjectLoader(null);
					ol.LoadedItem += new SimPe.Plugin.Tool.Dockable.ObjectLoader.LoadItemHandler(ol_LoadedItem);
					ol.Finished += new EventHandler(ol_Finished);
					ol.LoadData();				
				}
			}
		}

		delegate SimPe.Scenegraph.Compat.TreeNode GetParentNodeHandler(TreeNodeCollection nodes, string[] names, int id, SimPe.Cache.ObjectCacheItem oci, SimPe.Data.Alias a);
		TreeNode GetParentNode(TreeNodeCollection nodes, string[] names, int id, SimPe.Cache.ObjectCacheItem oci, SimPe.Data.Alias a)
		{	
			TreeNode ret = null;
			if (id<names.Length) 
			{	
				string name = names[id];
				foreach (TreeNode tn in nodes) 
				{
					if (tn.Text.Trim().ToLower() == name.Trim().ToLower())
					{
						ret = tn;
						if (id<names.Length-1) 
							ret = GetParentNode(tn.Nodes, names, id+1, oci, a);

						break;
					}
				}
			}

			if (ret==null) 
			{
				if (id<names.Length) ret = new SimPe.Scenegraph.Compat.TreeNode(names[id]);
				else ret = new SimPe.Scenegraph.Compat.TreeNode(SimPe.Localization.GetString("Unknown"));

				nodes.Add(ret);
				ret.SelectedImageIndex = 0;
				ret.ImageIndex = 0;
			}

			if (id==0) 
			{
				TreeNode tn = new SimPe.Scenegraph.Compat.TreeNode(a.ToString());
				tn.Tag = a;

				SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem fii = (SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem)oci.Tag;
				string flname = "";
				if (fii.Package!=null)
					if (fii.Package.FileName!=null)
						flname = fii.Package.FileName.Trim().ToLower();

				if (flname.StartsWith(PathProvider.SimSavegameFolder.Trim().ToLower())) 
				{
					tn.ImageIndex = 2;
				}
				else if (oci.Thumbnail!=null)
				{
					// Thumbnail may be SKBitmap (from cache) or System.Drawing.Image
					if (oci.Thumbnail is SKBitmap skThumb)
					{
						var resized = skThumb.Resize(new SkiaSharp.SKImageInfo(ilist.ImageSize.Width, ilist.ImageSize.Height), SkiaSharp.SKFilterQuality.Medium);
						ilist.Images.Add(resized ?? skThumb);
					}
					else if (oci.Thumbnail is Image img)
					{
						img = Ambertation.Drawing.GraphicRoutines.ScaleImage(img, ilist.ImageSize.Width, ilist.ImageSize.Height, Helper.XmlRegistry.GraphQuality);
						ilist.Images.Add(img);
					}
					tn.ImageIndex = ilist.Images.Count-1;
				}
				else
					tn.ImageIndex = 1;
				tn.SelectedImageIndex = tn.ImageIndex;
				ret.Nodes.Add(tn);
			}
			return ret;
		}

		private void ol_LoadedItem(SimPe.Cache.ObjectCacheItem oci, SimPe.Interfaces.Scenegraph.IScenegraphFileIndexItem fii, SimPe.Data.Alias a)
		{
			if (a==null) return;

			
			string[][] cats = oci.ObjectCategory;
			foreach (string[] ss in cats)				
			{
				Avalonia.Threading.Dispatcher.UIThread.Post(() => GetParentNode(tv.Nodes, ss, 0, oci, a));
			}

			//if (oci.Thumbnail!=null) a.Name = "* "+a.Name;				
			lb.Items.Add(a);			
		}

		private void ol_Finished(object sender, EventArgs e)
		{
		}

		private void Activate_biCatalog(object sender, System.EventArgs e)
		{
			biCatalog.Checked = !biCatalog.Checked;
			this.tv.Visible = biCatalog.Checked;
			this.lb.IsVisible = !biCatalog.Checked;
			
			lb_SelectedIndexChanged(lb, null);
			tv_AfterSelect(tv, null);
		}

		private void wizard1_ShowStep(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardEventArgs e)
		{
			this.biCatalog.IsVisible = (e.Step.Index==wizardStepPanel2.Index);			
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
			ofd.Filter = ExtensionProvider.BuildFilterString(
				new SimPe.ExtensionType[] {
											  SimPe.ExtensionType.Package,
											  SimPe.ExtensionType.DisabledPackage,
											  SimPe.ExtensionType.AllFiles
										  }
				);

			package = null;
			if (ofd.ShowDialog()==System.Windows.Forms.DialogResult.OK) 
			{
				package = SimPe.Packages.GeneratableFile.LoadFromFile(ofd.FileName);
				wizard1.JumpToStep(2);
			}
		}

		SimPe.Packages.GeneratableFile package;
		Data.Alias lastselected;
		private void tv_AfterSelect(object sender, SimPe.Scenegraph.Compat.TreeViewEventArgs e)
		{
			if (wizard1.CurrentStepNumber==this.wizardStepPanel2.Index && tv.Visible) 
			{
				if (tv.SelectedNode==null) wizard1.NextEnabled = false;
				else wizard1.NextEnabled = tv.SelectedNode.Tag!=null;
			}

			if (wizard1.NextEnabled) 
			{
				lastselected = (Data.Alias)tv.SelectedNode.Tag;
			} else lastselected = null;
					
			//UpdateObjectPreview(op1);
		}

		private void lb_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (wizard1.CurrentStepNumber==this.wizardStepPanel2.Index && lb.IsVisible)
			{
				wizard1.NextEnabled = (lb.SelectedIndex>=0);
			}	
		
			if (wizard1.NextEnabled) 
			{
				lastselected = (Data.Alias)lb.SelectedItem;
			} 
			else lastselected = null;
					
			//UpdateObjectPreview(op2);
		}

		private void cbTask_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cbTask.SelectedIndex==1) 
			{
				gbRecolor.IsVisible = false;
				gbClone.IsVisible = true;
			} 
			else 
			{
				gbRecolor.IsVisible = true;
				gbClone.IsVisible = false;
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			Activate_biNext(biNext, e);
		}

		private void wizardStepPanel2_Activate(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardEventArgs e)
		{
			package = null;
			if (tv.Visible) 
			{
				if (tv.SelectedNode==null) e.EnableNext = false;
				else if (tv.SelectedNode.Tag==null) e.EnableNext = false;
				else e.EnableNext = true;
			} 
			else 
			{
				e.EnableNext = lb.SelectedIndex>=0;
			}

			tv.SelectedNode = null;
			lb.SelectedIndex = -1;
		}		
		

		private void wizardStepPanel4_Activate(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardEventArgs e)
		{
			e.CanFinish = false;			 
			
			this.animatedImagelist1.Stop();
			this.lbwait.IsVisible = true;
			this.lbfinished.IsVisible = false;
			this.lbfinload.IsVisible = false;
			this.lberr.IsVisible = false;
		}

		private void wizardStepPanel4_Activated(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardStepPanel step)
		{
			this.animatedImagelist1.Start();
			SimPe.Packages.GeneratableFile package = null;
			if (lastselected ==null && this.package==null) 
			{
				sender.FinishEnabled = false;
			} 
			else 
			{				
				FileTable.FileIndex.Load();
				SimPe.Interfaces.IAlias a = null;
				Interfaces.Files.IPackedFileDescriptor pfd = null;
				uint localgroup = Data.MetaData.LOCAL_GROUP;
				if (this.package!=null) 
				{
					if (this.package.FileName!=null) 
					{
						SimPe.Interfaces.Wrapper.IGroupCacheItem gci = SimPe.FileTable.GroupCache.GetItem(this.package.FileName);
						if (gci!=null) localgroup = gci.LocalGroup;
					}
				} 
				else 
				{
					a = this.lastselected;
					pfd =(Interfaces.Files.IPackedFileDescriptor)a.Tag[0];			
					localgroup = (uint)a.Tag[1];
				}
				
				ObjectWorkshopSettings settings;

				//Clone an Object
				if (this.cbTask.SelectedIndex==1) 
				{
					OWCloneSettings cs = new OWCloneSettings();
					cs.IncludeWallmask = this.cbwallmask.Checked;
					cs.OnlyDefaultMmats = this.cbdefault.Checked;
					cs.IncludeAnimationResources = this.cbanim.Checked;
					cs.CustomGroup = this.cbgid.Checked;
					cs.FixResources = this.cbfix.Checked;
					cs.RemoveUselessResource = this.cbclean.Checked;
					cs.StandAloneObject = this.cbparent.Checked;

					settings = cs;
				} 					
				else  //Recolor a Object
				
					settings = new OWRecolorSettings();
				
				try 
				{
					package = ObjectWorkshopHelper.Start(this.package, a, ref pfd, localgroup, settings);
				} 
				catch (Exception ex) 
				{
					Helper.ExceptionMessage(ex);
				}

				this.animatedImagelist1.Stop();
				if (package!=null) this.lbfinload.IsVisible = settings.RemoteResult;
				else this.lberr.IsVisible = true;

			}

			this.lbwait.IsVisible = false;
			this.lbfinished.IsVisible = !this.lbfinload.IsVisible && !lberr.IsVisible;
		}

		private void wizardStepPanel3_Activate(SimPe.Wizards.Wizard sender, SimPe.Wizards.WizardEventArgs e)
		{
			e.CanFinish = lastselected!=null;
			e.EnableNext = false;		
			//UpdateObjectPreview(op2);
		}

		void UpdateObjectPreview(ObjectPreview op)
		{
			if (lastselected!=null) op.SetFromObjectCacheItem((SimPe.Cache.ObjectCacheItem)lastselected.Tag[3]);			
			else if (package!=null)	op.SetFromPackage(package);
			else op.SelectedObject = null;
		}

		private void biAbort_Activate(object sender, System.EventArgs e)
		{
			wizard1.JumpToStep(0);
		}

		private void cbfix_CheckedChanged(object sender, System.EventArgs e)
		{
			cbclean.Enabled = cbfix.Checked;
		}

		
	}
}
