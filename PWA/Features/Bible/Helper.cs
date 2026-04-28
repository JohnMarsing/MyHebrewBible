using RCL.Enums;
using RCL.Constants;

namespace PWA.Features.Bible;

public static class Helper
{
	public static int LastVerseCount(BibleBook? BibleBook, int chapter)
	{
		return BibleBook!.LastVerses[chapter - 1];
	}

	public static bool HasNext(BibleBook bibleBook)
	{
		return bibleBook.Value != BookChapterFacts.LastBook;
	}
	


}

/*
	ToDo: Not referenced: 

	public static string PreviousText(BibleBook bibleBook)
	{
		if (bibleBook != BookChapterFacts.FirstBook)
		{
			return bibleBook.Title;
		}
		else
		{
			return string.Empty;
		}
	}

	public static bool HasPrevious(BibleBook bibleBook) { return bibleBook.Value != BookChapterFacts.FirstBook; }

	// Used by only by: Features!VerseList!Index!ReNavigateBackToThisPage()
	// - MyHebrewBible.Client\Features\VerseList\Index.razor	153  
	// 
	public static string GetNavigateToUrlVerseList(BibleBook? bibleBook, int chapter, int begVerse, int endVerse)
	{
		return $"VerseList/{bibleBook!.Value}/{chapter}/{begVerse}/{endVerse}";
	}

}
 
 */