namespace PWA.Features.Bible.ActionComponents.Find;

public record FilterRecord(string? SearchText,string Description)
{
  public static FilterRecord From(FindFormVM vm)
  {
    return new FilterRecord(vm.SearchText, vm.SearchText ?? string.Empty);
  }
}
