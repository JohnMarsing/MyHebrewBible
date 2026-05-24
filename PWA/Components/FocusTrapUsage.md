
## `FocusTrap.razor`
- used to wrap EditForms(wrapped in a Modals) to control the tabbing
- `FocusTrap.razor.js` not shown
- 

```html
@inject IJSRuntime JSRuntime
<div @ref="_containerElement">
  @ChildContent
</div>
@code 
```

```csharp
{
  [Parameter, EditorRequired] public RenderFragment ChildContent { get; set; } = default!;

  private ElementReference _containerElement;
  private IJSObjectReference? _jsModule;

  protected override async Task OnAfterRenderAsync(bool firstRender)
  {
    if (firstRender)
    {
      _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/FocusTrap.razor.js");
      await _jsModule.InvokeVoidAsync("trapFocus", _containerElement);
    }
  }

  // Called by components that embed `FocusTrap` e.g. when a Modal HandleCancel event is fired
  public async ValueTask ReleaseAsync()
  {
    if (_jsModule is not null)
    {
      await _jsModule.InvokeVoidAsync("releaseFocusTrap", _containerElement);
    }
  }
}
```

## `FocusTrap` usage


### Code snippet from `FindForm.razor`
- C:\Source\repos\MyHebrewBible\PWA\Features\Bible\ActionComponents\Find\
- FindForm uses a `@ref` parameter to that when HandleCancel event is executed, it can all 

```html
<FocusTrap @ref="_focusTrap">

  <!-- my modal goes here -->

  <div class="modal fade show d-block" tabindex="-1" role="dialog" aria-modal="true" aria-labelledby="findFormModalLabel">
    <div class="modal-dialog" role="document">
      <div class="modal-content">

        <!-- `HandleCancel` event defined  -->
        <div class="modal-header">
          <h5 class="modal-title" id="findFormModalLabel">Verse Search Criteria</h5>
          <button type="button" class="btn-close" aria-label="Close" 
            @onclick="HandleCancel" @ref="_closeButton">
          </button>
        </div>
        
      <!-- modal details and <EditForm>  -->

      </div>
    </div>
  </div>

</FocusTrap>
```

```csharp
  private FocusTrap? _focusTrap;
  private async Task HandleCancel()
  {
    if (_focusTrap is not null)  {  await _focusTrap.ReleaseAsync();  }
    await OnCancel.InvokeAsync();
  }
```

<!-- 
private ElementReference _submitButton; <!-- bottom of <EditForm> --> 
-->

