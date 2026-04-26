# SimPE Avalonia Port — Claude Session Instructions

This file is auto-loaded into every Claude Code session in this repo. It captures the binding rules and hard-won knowledge that has accumulated across sessions. Read it in full before doing any non-trivial work.

For the deep architectural overview, see [ARCHITECTURE.md](ARCHITECTURE.md).
For the current state of in-flight work, see [.claude/SESSION_STATE.md](.claude/SESSION_STATE.md).

## What this project is

SimPE (Sims 2 Package Editor) is an old WinForms app being ported to Avalonia for cross-platform support (Mac/Linux). The Mac audience has a hard deadline: macOS 28 drops Rosetta in fall 2027, stranding the Aspyr Super Collection Sims 2 players who need this tool. As of 2026-04-26 the project is evaluating whether agent-driven form-port automation can deliver enough speedup to land before that cliff (see SESSION_STATE.md). If automation doesn't pan out, fallback is making the Windows build run reliably under Wine/CrossOver/Lutris on Linux.

## Form-port automation

There is a project-level agent at [.claude/agents/form-port.md](.claude/agents/form-port.md) that ports a single WinForms form to Avalonia per invocation. **Use it for any form-port work.** It reads the WinForms `*.Designer.cs` + `*.resx`, writes a real `InitializeComponent` into the form's own `.cs` file, deletes the matching shim from the project's `*.Stubs.cs`, and verifies the build is green. Invoke with the form's class name (e.g. "PetTraitSelect").

Successful pilot ports completed via this agent: PetTraitSelect, ProfileChooser, ScoreItemDefault, plus Chunks 1 and 2 of ExtSDesc (skeleton + pnId).

## Hard rules — these have bitten us, do not violate

### File-layout / build-system gotchas

- **`*.Designer.cs` is globally excluded from compilation** by `Directory.Build.targets:21`. Edits to Designer.cs are silently ignored. Designer.cs is read-only reference for layout truth.
- **The real `InitializeComponent` lives in the form's own `.cs` file** (its main partial-class file), not in any Designer.cs and not in any Stubs.cs. See [`BconForm.cs:506`](_PJSE/pjse%20Coder/BconForm.cs#L506) as the canonical pattern.
- **Two unrelated kinds of "stubs" coexist in this codebase — do not confuse them:**
  1. **Form stubs** — `SimPE.Sims.Stubs.cs`, `SimPE.Main.Stubs.cs`, etc. Contain empty `InitializeComponent() { }` placeholders for unported forms. **DELETE the form's shim entry when porting** (else duplicate-method compile error). The "no-shimming" rule applies here: empty stubs are unported code, not placeholders — never add new ones to "make it compile."
  2. **`WinFormsStubs.cs` / `CommandlineWinFormsStubs.cs` etc.** — WinForms-API compatibility facade. Provides Avalonia-backed implementations of `System.Windows.Forms.*` types so the plugin ecosystem keeps compiling without rewrite. These ARE supposed to contain real working implementations. The naming is misleading.

### Avalonia / WinForms semantic differences (silent bugs)

- **`Window.Show()` is non-blocking; WinForms `ShowDialog()` blocks.** Code ported from WinForms that reads form state on the next line (`form.ShowDialog(); if (form.SelectedThing != null) ...`) silently breaks. The bridge is the pump-and-yield pattern — already implemented in `RemoteControl.ShowSubForm`, `WindowExtensions.ShowDialog`, and `MessageBox.Show`. **Never use raw `form.Show()` for code that expects modal blocking.**
- **`MessageBox.Show()` was previously a no-op stub** — every error/warning/confirmation in the entire app was invisible. Fixed 2026-04-26. Treat any trivial-body method in `WinFormsStubs.cs` as suspect until verified — `PerformClick`, `ShowDialog`, and `MessageBox.Show` were all compile-green no-ops hiding bugs.
- **`RadioButton.GroupName` is window-scoped in Avalonia** (parent-scoped in WinForms). DO NOT set `GroupName` on RBs inside a reusable UserControl — it will couple sibling instances of that control across the whole window. Shared logical parent provides scope automatically.
- **WinForms `ComboBox.DropDownStyle = Simple`** = editable combo with always-visible inline list. Avalonia `ComboBox` is dropdown-only AND non-editable, so a 1:1 swap loses BOTH behaviors. Port to **ListBox + TextBox composite**. Always check `DropDownStyle` in Designer.cs before mapping a ComboBox.
- **Two `DialogResult` enums coexist:** `System.Windows.Forms.DialogResult` (in WinFormsStubs.cs:48) and `SimPe.DialogResult` (in SimPE.Helper). Plugin code uses the System.Windows.Forms one. Identical values; cast to convert.

### Layout / theme rules

- **Visual fidelity to original SimPE 0.75 is required.** Ported Avalonia forms must match the original WinForms layout as exactly as possible — no redesigns. Compare against 0.75 SimPE running under Wine for visual checks.
- **Use a DockPanel root with a nested Canvas** for forms with absolute Point-positioned controls (see [BCON/BHAV](_PJSE/pjse%20Coder/BconForm.cs)). A top-level fixed-size Canvas leaves the tab area mostly empty because the SimPE plugin tab is much wider than the original 758-ish WinForms client size.
- **Padding/LineHeight live in the global theme** at [`SimPE.Main/App.axaml`](SimPE.Main/App.axaml), not per-form. `Style Selector="Button"` etc. set `MinHeight=0`, `Padding="6,1"`, `FontSize=10`, plus `LineHeight=12` on TextBlock. **Do not set per-control `Padding` in InitializeComponent** — it overrides the theme. Avalonia's Segoe UI line metrics are taller than MS Sans Serif at the same nominal pt; the global theme is what makes WinForms-pixel-height layouts not clip.
- **Compat-class field-shadowing gotchas:** `GroupBox.Children` is a dead list (use `Border + Canvas` wrapper, declare field as `Canvas`); `TextBoxCompat.Width` is shadowed (use `MinWidth`/`MaxWidth` inside StackPanel); some Stubs.cs files shadow real types as bases (e.g. `XPTaskBoxSimple` shadowed as `Border`, `LabeledProgressBar` as `ProgressBar`) — restore the real types when porting.

## Process rules

- **Layout is the porting bottleneck.** A complex form takes ~3 days to lay out properly; wiring takes minutes. Automation should target Designer.cs+resx → Canvas-in-DockPanel translation. Don't underweight layout in time estimates.
- **Human review catches WinForms-vs-Avalonia semantic bugs the agent misses.** Every pilot port so far had at least one issue (GroupName scoping, ComboBox.DropDownStyle, etc.) that compiled green but was wrong. Always read agent output before declaring done.
- **Do not stub a missing event handler.** If a referenced handler doesn't exist in the main .cs, STOP and report. The "no-shimming" rule applies.
- **`dotnet build` from the repo root must stay green** after every change. Previously-ported forms must remain compiled.

## User context

- Catherine Gramze (`rhiamom@mac.com`) is doing the port form-by-form. Assumes domain knowledge; picks up mid-port between sessions and switches between Windows desktop and Mac laptop.
- The Avalonia port is the Mac Sims 2 community's path forward; it's mission work, not casual.
