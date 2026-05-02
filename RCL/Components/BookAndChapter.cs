using RCL.Enums;

namespace RCL.Components;

//ToDo: can I put in here `HomeHelper.LastVerseCount(bibleBook, chapter)`
public record BookAndChapter(BibleBook? BibleBook, int Chapter)
{
    public string Title => BibleBook is not null ? $"{BibleBook.Title} {Chapter}" : string.Empty;
}
