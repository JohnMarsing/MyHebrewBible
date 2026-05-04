using System.ComponentModel.DataAnnotations;
using RCL.Enums;

namespace PWA.Features.ScrollSpy.ActionComponents.LookupBCV;

public class BCVFormVM
{
  [Required(ErrorMessage = "Bible Book is required")]
  public BibleBook? SelectedBook { get; set; }

  [Required(ErrorMessage = "Chapter is required")]
  [Range(1, int.MaxValue, ErrorMessage = "Chapter must be at least 1")]
  public int? Chapter { get; set; }

  [Required(ErrorMessage = "Verse is required")]
  [Range(1, int.MaxValue, ErrorMessage = "Verse must be at least 1")]
  public int? Verse { get; set; }

  public string SearchText { get; set; } = string.Empty;

  public bool IsValid()
  {
    if (SelectedBook is null || Chapter is null || Chapter < 1 || Verse is null || Verse < 1)
      return false;

    if (Chapter > SelectedBook.LastChapter)
      return false;

    return Verse <= SelectedBook.MaxLastVerses();
  }
}