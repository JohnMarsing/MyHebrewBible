
### `PageHeaderNavAnchor.razor`

@using MyHebrewBible.Client.Enums

<a class="btn btn-outline-primary" title="@NavTo?.Description" href="@NavTo?.Url" target="_blank">
	<i class="@NavTo?.Icon"></i>
</a>

@code {
	[Parameter, EditorRequired] public NavToAnchor? NavTo { get; set; }
}


### `MitzvotTable.razor`
- see C:\Source\repos\MyHebrewBible\PWA\Features\Lists\MitzvahPage\MitzvotTable.md

### `LearnMoreToggleTemplate.razor`
- Used by
  - Lists\MitzvahPage\Index.razor	24
  - Lists\JotAndTittle.razor	23


- not to be confused with `LearnMoreModalTemplate.razor`

### `Environment.razor`
- this wasn't used, it was commented out in About.razor
- don't know how this works with a PWA

```
@using Microsoft.AspNetCore.Components.WebAssembly.Hosting
@inject IWebAssemblyHostEnvironment Env

<div class="card">
  <div class="card-header">
    Environment
  </div>
  <div class="card-body">
    @if (IsDevelopmentMode)
    {
      <h3>In developer mode.</h3>
    }
    else
    {
      <h3>NOT in developer mode.</h3>
    }
  </div>

  <div class="card-body">
    <h3>Env.Environment: @Mode</h3>
  </div>

</div>


@code {
  // You would think the if == IsDevelopmentMode then Mode == Development but you would be wrong
  private bool IsDevelopmentMode => Env.IsDevelopment();
  private string? Mode => Env.Environment.ToString();
}

```