using RCL.Enums;

namespace RCL.Components;

public record BookChapterVerseRecord(BibleBook? BibleBook, int Chapter, int Verse);

public static class BookChapterVerseHelper
{
	public static string Dump(BookChapterVerseRecord? BCV)
	{
		return $"{BCV!.BibleBook!.Abrv} {BCV.Chapter}:{BCV.Verse}";
	}
}

/*
namespace MyHebrewBible.Client.Features.BookChapter.Toolbar.NumberPad; 
- BookChapterVerseRecord was BookChapterVerse
- BookChapterVerseRecord? BCV was BookChapterVerse? BCV
 */