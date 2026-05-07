using HomeHelper = PWA.Features.Bible.Helper;
using PWA.Enums;

namespace PWA.Features.Bible;

// ToDo: When I abandon the  Picker/Buttons3by4 then `bool VerseIsNotDefault` becomes unnecessary (I think)
public record AbrvChapterVerse(string Abrv, int Chapter, int Verse, bool VerseIsNotDefault, int ScriptureId, int LastVerse)
{
	public static AbrvChapterVerse Default => new(
		RCL.Enums.BibleBook.Genesis.Name, 1, 1, false, 1, HomeHelper.LastVerseCount(RCL.Enums.BibleBook.Genesis, 1));
	                                                                
	public string NavigateToUrl() => $"{Nav.Bible.Index}/{Abrv}/{Chapter}/{Verse}/{ScriptureId}"; 
}

