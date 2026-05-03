namespace PWA.Features.ScrollSpy.ActionComponents.Find;

public record FilterRecord(
  string? SearchText,
  string? Strongs,
  WhereLogicalOperator WhereLogicalOperator,
  string Description)
{
  public static FilterRecord From(TextOrStrongsFormVM vm)
  {
    var prefix = vm.IsGreek ? "G" : "H";
    var strongs = vm.StrongsNumber.HasValue ? $"{prefix}{vm.StrongsNumber}" : null;
    var op = vm.WhereLogicalOperator.ToString().ToUpper();

    var description = (vm.SearchText, strongs) switch
    {
      ({ } t, { } s) => $"{t} {op} {s}",
      ({ } t, null)  => $"{t}",
      (null, { } s)  => $"{s}",
      _              => string.Empty
    };

    return new FilterRecord(vm.SearchText, strongs, vm.WhereLogicalOperator, description);
  }
}

/*
### `From(TextOrStrongsFormVM)` 
- is a static factory method named. 
- This is a common naming convention in C# for static methods that create an instance of a type from something else. 
- Called by PWA\Features\ScrollSpy\ActionComponents\Find\TextOrStrongsForm.razor
*/