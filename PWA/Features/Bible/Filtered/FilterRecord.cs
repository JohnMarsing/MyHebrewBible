namespace PWA.Features.Bible.Filtered;

public record FilterRecord(string? SearchText,string Description)
{
	// Called by FindForm.HandleValidSubmit like this
	//   await OnFilterSelected.InvokeAsync(FilterRecord.From(VM));
	//   FindFormVM VM; FindFormVM.cs only has string SearchText
	public static FilterRecord From(FindFormVM vm)
  {
    return new FilterRecord(vm.SearchText, vm.SearchText ?? string.Empty);
  }
}

/*
- SearchText is the actual filter input.
_ Description is the display label for that filter. 
 */