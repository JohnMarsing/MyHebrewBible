using RCL.Enums;

namespace RCL.Components;

public record BookChapterVerseRecord(BibleBook? BibleBook, int Chapter, int Verse)
{
	public string Title => BibleBook is not null ? $"{BibleBook.Title} {Chapter}" : string.Empty;
	public int ScriptureId => BibleBook is not null ? BibleBookHelper.GetScriptureId(BibleBook, Chapter, Verse) : 0;
	public int LastVerse => BibleBook is not null ? BibleBook.LastVerses[Chapter - 1] : 0;


	public string PrevLabel =>
	Verse == 1
	? string.Empty
	: $"{BibleBook!.Abrv} {Chapter}:{Verse - 1}";

	// Test: Gen 1:02 ==> Gen 1:01 because 2 != 1
	// Test: Gen 1:01 ==> null      because 1 == 1 (no previous within chapter)
	public BookChapterVerseRecord? PrevBCV =>
		Verse == 1
		? null
		: new BookChapterVerseRecord(BibleBook, Chapter, Verse - 1);


	public string NextLabel =>
		Verse == BibleBook!.LastVerses[Chapter - 1]
		? string.Empty
		: $"{BibleBook.Abrv} {Chapter}:{Verse + 1}";

	// Test: Deu 34:11 ==> Det 34:12 because 11 != 12
	// Test: Deu 34:12 ==> null      because 12 == 12
	public BookChapterVerseRecord? NextBCV =>
		Verse == BibleBook!.LastVerses[Chapter - 1]
		? null
		: new BookChapterVerseRecord(BibleBook, Chapter, Verse + 1);

}
