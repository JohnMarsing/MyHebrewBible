namespace PWA.Features.Home;

/*
Why doesn't this work???...
//public static AbrvChapterVerse Default => new AbrvChapterVerse(GlobalEnums.BibleBook.Genesis.Name, 1, 1, 1); 
 
 */


public record AbrvChapterVerse(string Abrv, int Chapter, int Verse, bool VerseIsNotDefault, int ScriptureId)
{
	public static AbrvChapterVerse Default => new AbrvChapterVerse(RCL.Enums.BibleBook.Genesis.Name, 1, 1, false, 1);
	//                                                                     Enums.BibleBook.Genesis.Name
}

// Ignore Spelling: Abrv 