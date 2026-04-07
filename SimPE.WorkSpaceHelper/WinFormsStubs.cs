/***************************************************************************
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

// Minimal WinForms stubs required to compile LoadFileWrappers.cs (Mac/Avalonia port).
// Only the members actually used by ToolMenuItem are provided.

using System;

namespace System.Windows.Forms
{
    [Flags]
    public enum AnchorStyles { None = 0, Top = 1, Bottom = 2, Left = 4, Right = 8 }

    public enum DockStyle { None, Top, Bottom, Left, Right, Fill }

    public enum ToolStripItemOverflow { Never, Always, AsNeeded }

    /// <summary>Minimal stub for System.Windows.Forms.Control.</summary>
    public class Control
    {
        public bool Visible { get; set; } = true;
        public bool Enabled { get; set; } = true;
        public string Name { get; set; } = "";
        public object Tag { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public System.Drawing.Point Location { get; set; }
        public System.Drawing.Size Size { get; set; }
    }

    public enum DialogResult { None = 0, OK = 1, Cancel = 2, Abort = 3, Retry = 4, Ignore = 5, Yes = 6, No = 7 }

    public static class DataFormats { public const string FileDrop = "Files"; }

    public struct LinkArea
    {
        public int Start, Length;
        public LinkArea(int start, int length) { Start = start; Length = length; }
    }

    // MessageBoxButtons, MessageBoxIcon, MessageBoxDefaultButton — defined in SimPe namespace (Message.cs).

    /// <summary>Minimal stub for System.Windows.Forms.Padding.</summary>
    public struct Padding
    {
        public int Left, Top, Right, Bottom;
        public Padding(int all) { Left = Top = Right = Bottom = all; }
        public Padding(int left, int top, int right, int bottom)
        { Left = left; Top = top; Right = right; Bottom = bottom; }
    }

    /// <summary>
    /// Minimal stub for System.Windows.Forms.ToolStripItem.
    /// </summary>
    public class ToolStripItem
    {
        public string Text    { get; set; } = string.Empty;
        public bool   Enabled { get => IsEnabled; set => IsEnabled = value; }
        public bool   IsEnabled { get; set; } = true;
        public bool   Visible { get => IsVisible; set => IsVisible = value; }
        public bool   IsVisible { get; set; } = true;
        public object Tag     { get; set; }
        public object Image   { get; set; }
        public string Name    { get; set; } = "";
        public string ToolTipText { get; set; } = "";
        public System.Drawing.Size Size { get; set; }
        public event EventHandler Click;
        public event EventHandler EnabledChanged;
        protected virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);
    }

    /// <summary>Minimal stub for System.Windows.Forms.ToolStrip.</summary>
    public class ToolStrip
    {
        public string Text { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;
        public bool Visible { get; set; } = true;
        public bool ShowItemToolTips { get; set; } = true;
        public int ImageScalingSize_Width { get; set; } = 16;
        public System.Drawing.Size ImageScalingSize { get; set; } = new System.Drawing.Size(16, 16);
        public System.Drawing.Point Location { get; set; }
        public System.Drawing.Size Size { get; set; }
        public int TabIndex { get; set; }
        public string Name { get; set; } = "";
        public ToolStripItemCollection Items { get; } = new ToolStripItemCollection();
        public void SuspendLayout() { }
        public void ResumeLayout(bool performLayout = false) { }
        public void PerformLayout() { }
    }

    /// <summary>Minimal stub for System.Windows.Forms.ToolStripButton.</summary>
    public class ToolStripButton : ToolStripItem
    {
        public ToolStripButton() { }
        public ToolStripButton(string text) { Text = text; }
        public bool Checked { get; set; }
        public System.Drawing.Font Font { get; set; }
        public object ImageScaling { get; set; }
        public ToolStripItemOverflow Overflow { get; set; }
        public bool Available { get; set; } = true;
        public System.Drawing.ContentAlignment ImageAlign { get; set; }
        public object Alignment { get; set; }
        public object DisplayStyle { get; set; }
        public object TextImageRelation { get; set; }
        public System.Drawing.Rectangle Bounds { get; set; }
        public void SetBounds(System.Drawing.Rectangle r) { }
        public event EventHandler VisibleChanged;
        public event EventHandler AvailableChanged;
        public event EventHandler CheckedChanged;
        protected virtual void OnCheckedChanged(EventArgs e) => CheckedChanged?.Invoke(this, e);
        protected new virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);
        public new event EventHandler Click;
    }

    /// <summary>Minimal stub for System.Windows.Forms.ToolTip.</summary>
    public class ToolTip : IDisposable
    {
        public ToolTip() { }
        public ToolTip(System.ComponentModel.IContainer container) { }
        public void SetToolTip(object control, string caption) { }
        public void Dispose() { }
    }

    /// <summary>Minimal stub for System.Windows.Forms.LinkLabel (maps to Button in Avalonia).</summary>
    public class LinkLabel
    {
        public string Text { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;
        public bool Visible { get; set; } = true;
        public bool TabStop { get; set; }
        public string Name { get; set; } = "";
        public object Tag { get; set; }
        public LinkArea LinkArea { get; set; }
        public System.Drawing.Font Font { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public bool AutoSize { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public object Parent { get; set; }
        public event LinkLabelLinkClickedEventHandler LinkClicked;
        protected virtual void OnLinkClicked(LinkLabelLinkClickedEventArgs e) => LinkClicked?.Invoke(this, e);
    }

    public class LinkLabelLinkClickedEventArgs : EventArgs
    {
        public LinkLabelLinkClickedEventArgs() { }
    }
    public delegate void LinkLabelLinkClickedEventHandler(object sender, LinkLabelLinkClickedEventArgs e);

    // ToolStripItemCollection and ToolStripMenuItem are NOT in GMDCExporterbase, so define them here.

    /// <summary>Minimal stub for System.Windows.Forms.ToolStripItemCollection.</summary>
    public class ToolStripItemCollection : System.Collections.IEnumerable
    {
        private readonly System.Collections.Generic.List<ToolStripItem> _items = new();
        public int Count => _items.Count;
        public ToolStripItem this[int i] => _items[i];
        public void Add(ToolStripItem item) { _items.Add(item); }
        public void Add(string text) { _items.Add(new ToolStripMenuItem(text)); }
        public void Insert(int index, ToolStripItem item) { _items.Insert(Math.Clamp(index, 0, _items.Count), item); }
        public void Remove(ToolStripItem item) { _items.Remove(item); }
        public void Clear() { _items.Clear(); }
        public System.Collections.IEnumerator GetEnumerator() => _items.GetEnumerator();
    }

    /// <summary>Minimal stub for System.Windows.Forms.ToolStripMenuItem.</summary>
    public class ToolStripMenuItem : ToolStripItem
    {
        public ToolStripMenuItem() { }
        public ToolStripMenuItem(string text) { Text = text; }
        public bool Checked { get; set; }
        public object ShortcutKeys { get; set; }
        public ToolStripItemCollection DropDownItems { get; } = new ToolStripItemCollection();
        public event EventHandler CheckedChanged;
        public void PerformClick() { }
    }

    // Control and ControlCollection are defined in SimPE.GMDCExporterbase (ListViewEx.cs).
    // WorkSpaceHelper now references GMDCExporterbase via Wizardbase, so they must not be
    // redefined here to avoid CS0433 ambiguity.

    /// <summary>Minimal stub for System.Windows.Forms.FormClosingEventArgs.</summary>
    public class FormClosingEventArgs : EventArgs
    {
        public bool Cancel { get; set; }
        public CloseReason CloseReason { get; }
    }
    public delegate void FormClosingEventHandler(object sender, FormClosingEventArgs e);

    public enum CloseReason { None, WindowsShutDown, MdiFormClosing, UserClosing, TaskManagerClosing, FormOwnerClosing, ApplicationExitCall }

    // Shortcut is defined in SimPE.GMDCExporterbase (ListViewEx.cs) — do not redefine here.

    /// <summary>Minimal stub for System.Windows.Forms.SaveFileDialog.</summary>
    public class SaveFileDialog : IDisposable
    {
        public string FileName { get; set; } = string.Empty;
        public string Filter { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string InitialDirectory { get; set; } = string.Empty;
        public bool OverwritePrompt { get; set; } = true;

        /// <summary>
        /// Pre-set a filename so the next ShowDialog() returns OK immediately
        /// without showing a dialog. Used when the caller needs to prompt the user
        /// before entering a synchronous call chain that creates SaveFileDialog internally.
        /// </summary>
        [System.ThreadStatic]
        public static string PresetFileName;

        public DialogResult ShowDialog() => ShowDialog(null);
        public DialogResult ShowDialog(object owner)
        {
            // If a filename was pre-set by the caller, consume it and return OK
            if (!string.IsNullOrEmpty(PresetFileName))
            {
                FileName = PresetFileName;
                PresetFileName = null;
                return DialogResult.OK;
            }

            return FileDialogHelper.RunOnUIThread(async (window) =>
            {
                var result = await window.StorageProvider.SaveFilePickerAsync(
                    new Avalonia.Platform.Storage.FilePickerSaveOptions
                    {
                        Title = string.IsNullOrEmpty(Title) ? "Save As" : Title,
                        FileTypeChoices = FileDialogHelper.ParseFilter(Filter),
                    });
                if (result != null)
                {
                    FileName = result.Path.LocalPath;
                    return DialogResult.OK;
                }
                return DialogResult.Cancel;
            });
        }

        public void Dispose() { }
    }

    /// <summary>Minimal stub for System.Windows.Forms.FolderBrowserDialog.</summary>
    public class FolderBrowserDialog : IDisposable
    {
        public string SelectedPath { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool ShowNewFolderButton { get; set; } = true;
        public System.Environment.SpecialFolder RootFolder { get; set; }
        public DialogResult ShowDialog() => DialogResult.Cancel;
        public DialogResult ShowDialog(object owner) => DialogResult.Cancel;
        public void Dispose() { }
    }

    /// <summary>Minimal stub for System.Windows.Forms.OpenFileDialog.</summary>
    public class OpenFileDialog : IDisposable
    {
        public string FileName { get; set; } = string.Empty;
        public string[] FileNames { get; private set; } = new string[0];
        public string Filter { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public bool Multiselect { get; set; }
        public string InitialDirectory { get; set; } = string.Empty;

        public DialogResult ShowDialog() => ShowDialog(null);
        public DialogResult ShowDialog(object owner)
        {
            return FileDialogHelper.RunOnUIThread(async (window) =>
            {
                var results = await window.StorageProvider.OpenFilePickerAsync(
                    new Avalonia.Platform.Storage.FilePickerOpenOptions
                    {
                        Title = string.IsNullOrEmpty(Title) ? "Open" : Title,
                        AllowMultiple = Multiselect,
                        FileTypeFilter = FileDialogHelper.ParseFilter(Filter),
                    });
                if (results != null && results.Count > 0)
                {
                    FileName = results[0].Path.LocalPath;
                    FileNames = new string[results.Count];
                    for (int i = 0; i < results.Count; i++)
                        FileNames[i] = results[i].Path.LocalPath;
                    return DialogResult.OK;
                }
                return DialogResult.Cancel;
            });
        }

        public void Dispose() { }
    }

    /// <summary>Helpers for bridging sync WinForms dialog calls to async Avalonia StorageProvider.</summary>
    public static class FileDialogHelper
    {
        public static System.Collections.Generic.List<Avalonia.Platform.Storage.FilePickerFileType> ParseFilter(string filter)
        {
            var types = new System.Collections.Generic.List<Avalonia.Platform.Storage.FilePickerFileType>();
            if (string.IsNullOrEmpty(filter)) return types;

            string[] parts = filter.Split('|');
            for (int i = 0; i + 1 < parts.Length; i += 2)
            {
                string name = parts[i].Trim();
                string[] patterns = parts[i + 1].Split(';');
                types.Add(new Avalonia.Platform.Storage.FilePickerFileType(name)
                {
                    Patterns = new System.Collections.Generic.List<string>(patterns),
                });
            }
            return types;
        }

        /// <summary>
        /// Runs an async dialog callback that needs the main window, blocking the caller
        /// without deadlocking regardless of which thread we're on.
        /// </summary>
        public static DialogResult RunOnUIThread(System.Func<Avalonia.Controls.Window, System.Threading.Tasks.Task<DialogResult>> action)
        {
            var window = (Avalonia.Application.Current?.ApplicationLifetime
                as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            if (window == null) return DialogResult.Cancel;

            if (Avalonia.Threading.Dispatcher.UIThread.CheckAccess())
            {
                // Already on UI thread — use a ManualResetEventSlim + Post to avoid deadlock.
                // We run a blocking wait that pumps dispatcher jobs via MainLoop.
                DialogResult result = DialogResult.Cancel;
                var tcs = new System.Threading.Tasks.TaskCompletionSource<DialogResult>();

                async void RunDialog()
                {
                    try { tcs.SetResult(await action(window)); }
                    catch { tcs.SetResult(DialogResult.Cancel); }
                }

                // Post the async work so it starts on the next dispatcher cycle
                Avalonia.Threading.Dispatcher.UIThread.Post(RunDialog);

                // Block while pumping — use Avalonia's built-in MainLoop
                // which processes events and avoids deadlock
                while (!tcs.Task.IsCompleted)
                {
                    Avalonia.Threading.Dispatcher.UIThread.RunJobs(Avalonia.Threading.DispatcherPriority.Background);
                    if (!tcs.Task.IsCompleted)
                        System.Threading.Thread.Yield();
                }
                return tcs.Task.Result;
            }
            else
            {
                // Background thread — dispatch and wait
                return Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(async () => await action(window)).GetAwaiter().GetResult();
            }
        }
    }

    /// <summary>Minimal stub for System.Windows.Forms.Application.</summary>
    public static class Application
    {
        public static void Run(object form) { }
        public static void EnableVisualStyles() { }
        public static void DoEvents() { }
        public static bool UseWaitCursor { get; set; }
    }

    /// <summary>Minimal stub for System.Windows.Forms.ToolStripSeparator.</summary>
    public class ToolStripSeparator : ToolStripItem { }

    /// <summary>Minimal stub for System.Windows.Forms.Button.</summary>
    public class Button
    {
        public string Text { get; set; } = "";
        public bool Enabled { get; set; } = true;
        public bool Visible { get; set; } = true;
        public bool TabStop { get; set; } = true;
        public string Name { get; set; } = "";
        public object Tag { get; set; }
        public void Focus() { }
        public event EventHandler Click;
    }

    /// <summary>Minimal stub for System.Windows.Forms.MessageBox.</summary>
    public static class MessageBox
    {
        public static DialogResult Show(string text, string caption = "", SimPe.MessageBoxButtons buttons = SimPe.MessageBoxButtons.OK, SimPe.MessageBoxIcon icon = SimPe.MessageBoxIcon.None) => DialogResult.Cancel;
        public static DialogResult Show(object owner, string text, string caption = "", SimPe.MessageBoxButtons buttons = SimPe.MessageBoxButtons.OK, SimPe.MessageBoxIcon icon = SimPe.MessageBoxIcon.None) => DialogResult.Cancel;
        public static DialogResult Show(string text, string caption, SimPe.MessageBoxButtons buttons) => DialogResult.Cancel;
        public static DialogResult Show(object owner, string text, string caption, SimPe.MessageBoxButtons buttons) => DialogResult.Cancel;
    }

    public enum PictureBoxSizeMode { Normal, StretchImage, AutoSize, CenterImage, Zoom }

    /// <summary>
    /// Avalonia-native replacement for System.Windows.Forms.PictureBox.
    /// Wraps an Avalonia Image with click support. Used in OptionForm and other plugin panels.
    /// </summary>
    public class PictureBox
    {
        public int Left   { get; set; }
        public int Top    { get; set; }
        public int Width  { get; set; }
        public int Height { get; set; }
        public object Parent { get; set; }
        public object Tag    { get; set; }
        public AnchorStyles Anchor { get; set; }
        public System.Drawing.Color BackColor { get; set; }
        public System.Drawing.Image Image    { get; set; }
        public PictureBoxSizeMode SizeMode   { get; set; }
        public event EventHandler Click;
        protected virtual void OnClick(EventArgs e) => Click?.Invoke(this, e);
    }

    /// <summary>No-argument ShowDialog stub for Avalonia.Controls.Window — shows non-blocking.</summary>
    public static class WindowExtensions
    {
        public static void ShowDialog(this Avalonia.Controls.Window w) => w.Show();
    }
}
