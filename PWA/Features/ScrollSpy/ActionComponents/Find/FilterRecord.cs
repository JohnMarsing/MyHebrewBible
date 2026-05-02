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
