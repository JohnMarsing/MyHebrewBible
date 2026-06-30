using HomeHelper = PWA.Features.Bible.Helper;
using PWA.Enums;

namespace PWA.Features.Bible;

// ToDo: When I abandon the  Picker/Buttons3by4 then `bool VerseIsNotDefault` becomes unnecessary (I think)
public record AbrvChapterVerse(string Abrv, int Chapter, int Verse, bool VerseIsNotDefault, int ScriptureId, int LastVerse)
{
	public static AbrvChapterVerse Default => new(
		RCL.Enums.BibleBook.Genesis.Abrv, 1, 1, false, 1, HomeHelper.LastVerseCount(RCL.Enums.BibleBook.Genesis, 1));
	                                                                
	public string NavigateToUrl() => $"{Nav.Bible.Index}/{Abrv}/{Chapter}/{Verse}/{ScriptureId}";


	public static AbrvChapterVerse FromBookBBCVID(RCL.Enums.BibleBookChapterVerseId bbcvid)
	{
		return new AbrvChapterVerse(
			bbcvid.BibleBook!.Abrv,
			bbcvid.Chapter,
			bbcvid.Verse,
			bbcvid.Verse != 1,
			RCL.Enums.BibleBookHelper.GetScriptureId(bbcvid.BibleBook, bbcvid.Chapter, bbcvid.Verse),
			PWA.Features.Bible.Helper.LastVerseCount(bbcvid.BibleBook, bbcvid.Chapter));
	}
	/*
	public static AbrvChapterVerse FromBookId(int bookId, int chapter, int verse)
	{
		var bibleBook = RCL.Enums.BibleBook.FromValue(bookId);
		return new AbrvChapterVerse(
			bibleBook.Abrv,
			chapter,
			verse,
			verse != 1,
			RCL.Enums.BibleBookHelper.GetScriptureId(bibleBook, chapter, verse),
			PWA.Features.Bible.Helper.LastVerseCount(bibleBook, chapter));
	}
	*/
}

