using Microsoft.AspNetCore.Components;
using PWA.State;

namespace PWA;

public partial class CascadingAppState
{
	[Inject] public AppState? AppState { get; set; }
	[Parameter] public RenderFragment? ChildContent { get; set; }

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await AppState!.Initialize();
		}
	}

}

// Ignore Spelling: App