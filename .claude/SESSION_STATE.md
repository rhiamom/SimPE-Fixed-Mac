# Session State — Where We Are Right Now

Updated: **2026-04-26** (end of Windows-desktop session, switching to Mac laptop)

This file is the handoff between Claude sessions. Update it when pausing work or switching machines. Only the most recent state is kept here — git history is the long-term record.

## Recent commits leading into this state

```
7e9ca54 BHAV Operand Wizard + instruction list: port real behavior instead of shims
d1461fb BHAV form zero, raw, wiz buttons
2d0e071 BHAV form port: responsive layout, rows rendering, connector arrows
```

## What was just done in this session (2026-04-26, Windows desktop)

The session pivoted twice. Started by reviving the Avalonia form-port pilot via agent automation; mid-session paused to fix infrastructure that was blocking visual verification.

### Form-port automation pilot

Created the form-port agent at [.claude/agents/form-port.md](agents/form-port.md). Three small-form pilots ran cleanly:

| Form | Patterns exercised | Build | Bugs caught at human review |
|---|---|---|---|
| `PetTraitSelect` | Canvas-in-DockPanel, absolute Points, RadioButton | ✅ | RadioButton.GroupName scoping (window-scoped, would have coupled all 5 instances inside ExtSDesc) |
| `ProfileChooser` | DockPanel + Canvas + WrapPanel, dialog, ComboBox | ✅ | ComboBox.DropDownStyle=Simple was mapped to Avalonia ComboBox; lost editable text. Re-fixed to ListBox+TextBox composite |
| `ScoreItemDefault` | DockPanel + LastChildFill, Dock.Top/Fill, multiline TextBox | ✅ | None |

Both bugs are now baked into the agent definition AND the CLAUDE.md hard rules so future runs/sessions don't repeat them.

### ExtSDesc (the big one) — partially ported, in chunks

`ExtSDesc` (file `SimPE.Sims/ExtSDescUI.cs`, namespace `SimPe.PackedFiles.UserInterface`) is the SimDescription editor — ~250 controls, hosts the 5 PetTraitSelects, has 12 mode-switching panels. The agent did a Phase 1 structural scan and proposed a 6-chunk plan:

1. ✅ **Skeleton** — DockPanel root, toolbar StackPanel of 14 ButtonCompats wired to `ChoosePage`, 12 empty mode panels stacked at (0,80) 696×264, 2 ContextMenus with 19 MenuItem handlers, ~280 fields migrated out of Stubs.cs (with real types restored: 6× XPTaskBoxSimple, 56× LabeledProgressBar, 3× EnumComboBox, 11× TransparentCheckBox).
2. ✅ **`pnId`** — 17 children placed at exact resx coordinates. 9 events wired.
3. ⏸️ **`pnChar`** — 32 children including the pet/human sub-panel toggle and the 5 PetTraitSelects + 10 personality bars. **NEXT IN QUEUE.**
4. ⏸️ **`pnInt`** — pnSimInt (18 bars) + pnPetInt (9 bars).
5. ⏸️ **`pnSkill` + `pnCareer` + `pnRel` + `pnMisc`** — ~50 children.
6. ⏸️ **`pnEP1`/`pnEP2`/`pnEP3`/`pnEP7`/`pnVoyage`** — ~70 children, includes `lvCollectibles` ListView, `sblb` SimBusinessList, etc.

Build is green with chunks 1 and 2. The form has no host renderer yet (mode panels are empty after Chunk 1 except for Chunk 2's pnId), so visual verification is limited until enough chunks complete.

### Infrastructure fixes — Tools menu and modal dialogs

While trying to verify form-port output, we discovered the Tools menu was empty and dialogs weren't appearing. Fixed three silent-noop bugs in [SimPE.WorkSpaceHelper/WinFormsStubs.cs](../SimPE.WorkSpaceHelper/WinFormsStubs.cs):

1. **`ToolStripMenuItem.PerformClick()`** was `{ }` — fixed to call `OnClick(EventArgs.Empty)`. Plugin items registered in `miTools.DropDownItems` now actually fire their handlers.
2. **`WindowExtensions.ShowDialog(this Window)`** was `=> w.Show()` (non-blocking). Fixed to use the pump-and-yield modal bridge with the MainWindow as owner. ProfileChooser, About, NeighborhoodForm now actually wait for the user.
3. **`MessageBox.Show(...)`** was `=> DialogResult.Cancel` with no UI. Fixed to build a real Avalonia modal Window (TextBlock + button row, supports OK/OKCancel/YesNo/YesNoCancel/RetryCancel/AbortRetryIgnore). Same pump-and-yield bridge.

Also added a one-shot tree-mirror at [SimPE.Main/Main.Setup.cs](../SimPE.Main/Main.Setup.cs): `MirrorWinFormsMenuToAvalonia(miTools.DropDownItems, avlnTools.Items)` runs after PluginManager loads. PluginManager populates the WinForms-stub `miTools`; the mirror copies that tree into the visible Avalonia `avlnTools` (declared empty in MainForm.axaml). EnabledChanged is wired so plugins toggling state at runtime stay in sync.

Tools menu is confirmed working (user verified visually).

## Open / pending — pick up here

### To verify on Mac (with real game files)

These should now work because of the WinFormsStubs fixes — they need a re-test with a real Sims 2 install:

- **NeighborhoodForm popup** — click Tools → Neighbourhood → Neighborhood Browser. Should now either open the selection window (if SaveGame folder is configured) OR show a MessageBox warning "Neighbourhood Folder was not found" (if not). Either result is progress vs the previous silent failure.
- **ProfileChooser** (Settings → Save Profile...) — should now actually wait for input and let you type a new profile name. Was previously returning `DialogResult.Cancel` immediately.
- **Any plugin tool** that opens a dialog or shows a MessageBox.

### Open issue waiting for clarification

- **Dropdown menus require holding the mouse button to stay open.** User reported this as part of today's pain points. Awaiting answers to two questions:
  1. Does this happen on all menus (File, Settings, Help) or only Tools/the new ones?
  2. Did this exist before the Tools menu mirror was added today, or is it new?

  If pre-existing on all menus → Avalonia FluentTheme behavior, fix in App.axaml with a `Style Selector="MenuItem"` override. If only on the mirrored Tools items → the mirror code in `Main.Setup.cs` is doing something different from how axaml-declared menus are wired. Investigate the Avalonia `MenuItem.IsSubMenuOpen` / focus model.

### Resume form-port

Once the menu issue is resolved (or deemed lower-priority), resume ExtSDesc Chunk 3 (pnChar). The agent definition handles it — invoke the form-port agent (or general-purpose agent reading [.claude/agents/form-port.md](agents/form-port.md)) with explicit Chunk 3 instructions: add the children of `pnChar`, including the `pnHumanChar` and `pnPetChar` sub-panels, the 5 PetTraitSelects, the 10 personality LabeledProgressBars, panel1/panel2 dividers, gender meters (pbMan/pbWoman). Field declarations are already in [SimPE.Sims/ExtSDescUI.cs](../SimPE.Sims/ExtSDescUI.cs) from Chunk 1; only instantiate. Replace the `// CHUNK 3: pnChar children go here` marker.

## Things you should NOT lose track of

- `Window.Show()` vs `ShowDialog()` blocking semantics — the bridge pattern in WinFormsStubs.cs / RemoteControl.cs is the standard, do not reinvent. See CLAUDE.md "Avalonia / WinForms semantic differences."
- The form-port agent has accumulated rules about RadioButton.GroupName (don't set it on UserControl-internal RBs) and ComboBox.DropDownStyle=Simple (use ListBox+TextBox). Both are in the agent definition AND in CLAUDE.md.
- `WinFormsStubs.cs` is a compatibility facade with real implementations — NOT the kind of stubs the no-shimming rule targets. Treat any trivial-body method there as suspect until verified.
- The user works across Windows desktop and Mac laptop; this file IS the handoff. Update it before pausing.
