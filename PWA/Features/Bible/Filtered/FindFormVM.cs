using System.ComponentModel.DataAnnotations;

namespace PWA.Features.Bible.Filtered;

public class FindFormVM
{
  [Required(ErrorMessage = "Search text is required.")]
  [MaxLength(20, ErrorMessage = "Search text cannot exceed 20 characters.")]
  public string? SearchText { get; set; }
    
}