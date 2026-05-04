using RCL.Enums;

namespace RCL.Components;

public record BookChapterVerseRecord(BibleBook? BibleBook, int Chapter, int Verse)
{
	public string Title => BibleBook is not null ? $"{BibleBook.Title} {Chapter}" : string.Empty;
	public int ScriptureId => BibleBook is not null ? BibleBookHelper.GetScriptureId(BibleBook, Chapter, Verse) : 0;
}
