# `Sections.razor` hierarchy

Source file: `PWA\Features\Bible\Sections.razor`

## Purpose

`Sections.razor` renders the main Bible verse list for the current chapter and switches between:

- **normal list mode**
- **drilldown mode** for a selected verse
- **word detail mode** for a selected Hebrew word

It also manages:

- keyboard verse navigation
- focus/scroll behavior through JavaScript interop
- drilldown open/close state
- selected word state inside the drilldown card

---

## Who calls `Sections.razor`

### Direct caller

- `PWA\Features\Bible\Index.razor`
  - renders `<Sections ... />` when:
    - current action is **not** `Parasha`
    - current action is **not** `Find`


### Call site

`PWA\Features\Bible\Index.razor:72-75`


### Parameters passed in

- `FilteredVerses`
- `ParmScrollToScriptureId`
- `UserSettings`
- `HighlightText`

---

## What `Sections.razor` calls

## Render hierarchy

### Per verse, always attempted when `FilteredVerses` is not null/empty


---

## Direct component dependencies

`Sections.razor` directly renders these components:

- `TitleSubtitle`
- `ParashaDisplay`
- `Paragraph`
- `SatAndSurroundingWords`
- `CardHeader`
- `ParagraphWithWordButtons`
- `HebrewParagraph`
- `HebrewTableFiltered`

---

## Event and callback flow

## Incoming

### From parent `Index.razor`

- `FilteredVerses`
- `ParmScrollToScriptureId`
- `UserSettings`
- `HighlightText`

### Cascading input

- `ProcessError`

---

## Outgoing to child components

### `Paragraph`

- `BookAndChapter`
- `Verse`
- `ScrollToScriptureId`
- `ShowStandaloneAlephTavIcon`
- `HighlightText`
- `OnVerseSelectedPassThrough="ReturnedVerse"`

### `CardHeader`

- `BookAndChapter`
- `BCV`
- `VerseNumber`
- `Filter`
- `OnFilter="ReturnedFilter"`
- `OnClose="ReturnedCloseEvent"`

### `ParagraphWithWordButtons`

- `ScriptureId`
- `WordSelected`
- `OnWordSelected="ReturnedWord"`

### `HebrewParagraph`

- `ScriptureId`
- `WordSelected`
- `HebrewWordNumbers`
- `OnWordSelected="ReturnedWord"`

### `HebrewTableFiltered`

- `ScriptureId`
- `WordSelected`
- `IsXs`

---

## Internal method call hierarchy

```
OnParametersSetAsync 
└── CurrentScrollToScriptureId = ParmScrollToScriptureId
HandleKeyDown ├── DirHelper.IsKeyDownValid(e.Key) ├── SetJustScriptureId(dirEnums) │   └── DirHelper.GetVerseAndScriptureId(...) └── SetFocusToElement() └── _module.InvokeVoidAsync(JS.FunctionSetFocus, ...)
ReturnedCloseEvent ├── DrilldownScriptureId = 0 ├── Task.Delay(5) └── _module.InvokeVoidAsync(JS.FunctionSetFocus, ...)
OnAfterRenderAsync(firstRender) ├── JSRuntime.InvokeAsync<IJSObjectReference>("import", JS.Path) ├── _module.InvokeVoidAsync(JS.FunctionScrollTo, ...) └── _module.InvokeVoidAsync(JS.FunctionSetFocus, ...)
DisposeAsync └── _module.DisposeAsync()
```