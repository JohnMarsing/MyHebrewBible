using HomeHelper = PWA.Features.Bible.Helper;
namespace PWA.Features.Bible;

public record AbrvChapterVerse(string Abrv, int Chapter, int Verse, bool VerseIsNotDefault, int ScriptureId, int LastVerse)
{
	public static AbrvChapterVerse Default => new(
		RCL.Enums.BibleBook.Genesis.Name, 1, 1, false, 1, HomeHelper.LastVerseCount(RCL.Enums.BibleBook.Genesis, 1));
	//                                                                    
}

