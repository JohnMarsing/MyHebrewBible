namespace PWA.Features.Bible.Data;

//ToDo: Not being used
public record ParashaWithAT
{
	public int ID { get; init; }            // s.ID, 

	// used by List<TocRecord>; VM!.TableOfContentList = _tableOfContentList; {TOC_VM? VM}
	public int SectionId { get; init; }     // t.SectionId, 
	public int GroupCount { get; init; }    // t.GroupCount, 
	public int ScriptureID_Beg { get; init; } // t.ScriptureID_Beg, 
	public string? VerseRange { get; init; } // t.VerseRange,

	// used by ???
	public int BookID { get; init; }        // s.BookID, 
	public int Chapter { get; init; }       // s.Chapter, 

	public string? BCV { get; init; }        // s.BCV, 
	public int Verse { get; init; }         // s.Verse, 
	public string? VerseOffset { get; init; } //s.VerseOffset
	public string? KJV { get; init; }        // s.KJV, 
	public string? DescH { get; init; }      // s.DescH, 
	public string? DescD { get; init; }      // s.DescD
	public List<WordPart>? WordPartList { get; init; }
}
// Ignore Spelling: Cnt, BCV