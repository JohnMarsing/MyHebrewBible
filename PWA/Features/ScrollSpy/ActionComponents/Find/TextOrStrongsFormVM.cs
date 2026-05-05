using System.ComponentModel.DataAnnotations;

namespace PWA.Features.ScrollSpy.ActionComponents.Find;

public class TextOrStrongsFormVM : IValidatableObject 
{
  [MaxLength(15, ErrorMessage = "Search text cannot exceed 15 characters.")]
  public string? SearchText { get; set; }

  /// <summary>
  /// True = Greek (G), False = Hebrew (H)
  /// </summary>
  public bool IsGreek { get; set; }

  [Range(1, 9999, ErrorMessage = "Strong's number must be between 1 and 9999.")]
  public int? StrongsNumber { get; set; }

  public WhereLogicalOperator WhereLogicalOperator { get; set; } = WhereLogicalOperator.And;

  public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
  {
    if (string.IsNullOrWhiteSpace(SearchText) && StrongsNumber is null)
    {
      yield return new ValidationResult(
        "Either a search text or a Strong's number is required.",
        [nameof(SearchText), nameof(StrongsNumber)]);
    }
  }
}