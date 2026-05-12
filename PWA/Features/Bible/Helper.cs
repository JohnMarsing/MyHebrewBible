using RCL.Enums;
using RCL.Constants;
using PWA.Features.Bible.Enums;

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

	// ToDo: Compare dupe code to Helpers\HebrewFormat.cs
	public static string GetSpan(int wordEnumLong, string hebrew1, string hebrew2, string hebrew3)
	{
		WordEnum wordEnum = (WordEnum)wordEnumLong;

		return wordEnum switch
		{
			WordEnum.SimpleSingle => hebrew1,
			WordEnum.NrlPrefix => $"<span class='nrl'>{hebrew1}</span>{hebrew2}",
			WordEnum.NrlSuffix => $"{hebrew1}<span class='nrl'>{hebrew2}</span>",
			WordEnum.NrlPrefixAndSuffix => $"<span class='nrl'>{hebrew1}</span>{hebrew2}<span class='nrl'>{hebrew3}</span>",
			WordEnum.Sat => $"<span class='at-red'>{hebrew1}</span>",
			_ => $"<span class='at-red'>{hebrew1}</span> <span class='last-seg-type-paseq'>׀</span>"  // WordEnum.SatAndPaseq
		};
	}

	private const string Maqqef = "־";  // A Maqqef is a Hebrew hyphen

	// If the last part of the word is a Maqqef, don't add a space to the end of the word

	public static string CheckMaqqef(int wordEnumLong, string hebrew1, string hebrew2, string hebrew3)
	{
		WordEnum wordEnum = (WordEnum)wordEnumLong;

		return wordEnum switch
		{
			WordEnum.SimpleSingle => (hebrew1.EndsWith(Maqqef)) ? "" : " ",
			WordEnum.NrlPrefix => (hebrew2.EndsWith(Maqqef)) ? "" : " ",
			WordEnum.NrlSuffix => (hebrew2.EndsWith(Maqqef)) ? "" : " ",
			WordEnum.NrlPrefixAndSuffix => (hebrew3.EndsWith(Maqqef)) ? "" : " ",
			WordEnum.Sat => (hebrew1.EndsWith(Maqqef)) ? "" : " ",
			_ => (hebrew1.EndsWith(Maqqef)) ? "" : " "
		};
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