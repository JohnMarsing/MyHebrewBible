using HomeHelper =  PWA.Features.Home.Helper;
namespace PWA.Features.Home;

public record AbrvChapterVerse(string Abrv, int Chapter, int Verse, bool VerseIsNotDefault, int ScriptureId, int LastVerse)
{
	public static AbrvChapterVerse Default => new(
		RCL.Enums.BibleBook.Genesis.Name, 1, 1, false, 1, HomeHelper.LastVerseCount(RCL.Enums.BibleBook.Genesis, 1));
	//                                                                    
}

