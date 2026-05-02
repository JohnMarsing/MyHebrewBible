
```razor
<div class="fab-container">
	@if (IsMenuOpen)
	{
		<div class="fab-backdrop" @onclick="ToggleMenu"></div>

		<button class="fab-option fab-up" @onclick="() => ButtonClick(isDownward: false)" title="Previous Verse">
			<i class="fa-solid fa-arrow-up"></i>
		</button>

		<button class="fab-option fab-down" @onclick="() => ButtonClick(isDownward: true)" title="Next Verse">
			<i class="fa-solid fa-arrow-down"></i>
		</button>
	}

	<button class="fab fab-main @(IsMenuOpen ? "fab-open" : "")" @onclick="ToggleMenu" title="Verse Navigation">
		<i class="fa-solid fa-arrows-up-down"></i>
	</button>
</div>

@code
```

```csharp
@code {
	[Parameter, EditorRequired] public EventCallback<bool> OnNavigate { get; set; }

	private bool IsMenuOpen { get; set; } = false;
	private void ToggleMenu()
	{
		IsMenuOpen = !IsMenuOpen;
	}

	private async Task ButtonClick(bool isDownward)
	{
		IsMenuOpen = false;
		await OnNavigate.InvokeAsync(isDownward);
	}

}
```