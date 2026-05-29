using PWA.Features.Bible.Data;

namespace PWA.Features.Bible.Data;

public static class DataExtensions
{
	public static VerseModel MapFromBookChapter(this BookChapterWithAT bc, int bookId, int chapter)
	{
		return new VerseModel
		{
			ID = bc.ID,
			BCV = bc.BCV,
			Verse = bc.Verse,
			VerseOffset = bc.VerseOffset,
			KJV = bc.KJV,
			DescH = bc.DescH,
			DescD = bc.DescD,
			BookID = bookId,
			Chapter = chapter,
			WordPartList = bc.WordPartList?
						.Select(wp => new WordPart
						{
							Id = wp.Id,
							BCV = wp.BCV,
							BookID = wp.BookID,
							Chapter = wp.Chapter,
							Verse = wp.Verse,
							ScriptureID = wp.ScriptureID,
							WordCount = wp.WordCount,
							SegmentCount = wp.SegmentCount,
							WordEnum = wp.WordEnum,
							Hebrew1 = wp.Hebrew1,
							Hebrew2 = wp.Hebrew2,
							Hebrew3 = wp.Hebrew3,
							KjvWord = wp.KjvWord,
							Strongs = wp.Strongs,
							Transliteration = wp.Transliteration,
							FinalEnum = wp.FinalEnum
						})
						.ToList() ?? new()
		};
	}

public static VerseModel MapFromBookChapterFilter(this BookChapterWithAT bc, int bookId, int chapter, string filterText)
{
	return new VerseModel
	{
		ID = bc.ID,
		BCV = bc.BCV,
		Verse = bc.Verse,
		VerseOffset = bc.VerseOffset,
		KJV = bc.KJV,
		DescH = bc.DescH,
		DescD = bc.DescD,
		BookID = bookId,
		Chapter = chapter,
		WordPartList = bc.WordPartList?
					.Where(wp => wp.KjvWord != null && wp.KjvWord.Contains(filterText, StringComparison.OrdinalIgnoreCase))
					.Select(wp => new WordPart
					{
						Id = wp.Id,
						BCV = wp.BCV,
						BookID = wp.BookID,
						Chapter = wp.Chapter,
						Verse = wp.Verse,
						ScriptureID = wp.ScriptureID,
						WordCount = wp.WordCount,
						SegmentCount = wp.SegmentCount,
						WordEnum = wp.WordEnum,
						Hebrew1 = wp.Hebrew1,
						Hebrew2 = wp.Hebrew2,
						Hebrew3 = wp.Hebrew3,
						KjvWord = wp.KjvWord,
						Strongs = wp.Strongs,
						Transliteration = wp.Transliteration,
						FinalEnum = wp.FinalEnum
					})
					.ToList() ?? new()
	};
}

	public static VerseModel MapFromParasha(ParashaWithAT parasha)  
	{
		return new VerseModel
		{
			ID = parasha.ID,
			BCV = parasha.BCV,
			Verse = parasha.Verse,
			VerseOffset = parasha.VerseOffset,
			KJV = parasha.KJV,
			DescH = parasha.DescH,
			DescD = parasha.DescD,
			BookID = parasha.BookID,
			Chapter = parasha.Chapter,
			WordPartList = parasha.WordPartList?
				.Select(wp => new WordPart
				{
					Id = wp.Id,
					BCV = wp.BCV,
					BookID = wp.BookID,
					Chapter = wp.Chapter,
					Verse = wp.Verse,
					ScriptureID = wp.ScriptureID,
					WordCount = wp.WordCount,
					SegmentCount = wp.SegmentCount,
					WordEnum = wp.WordEnum,
					Hebrew1 = wp.Hebrew1,
					Hebrew2 = wp.Hebrew2,
					Hebrew3 = wp.Hebrew3,
					KjvWord = wp.KjvWord,
					Strongs = wp.Strongs,
					Transliteration = wp.Transliteration,
					FinalEnum = wp.FinalEnum
				})
				.ToList() ?? new()
		};
	}


	public static IReadOnlyList<TableOfContentRecord> ToTableOfContentMapping(this IEnumerable<VerseModel> items)
	{
		return items
			.Where(v => v.DescD != "NULL")
			.OrderBy(v => v.ID)
			.Select(v => new TableOfContentRecord(v.Verse, v.ID, v.DescD!, v.VerseOffset))
			.ToList();
	}

	public static string? GetTitle(this IEnumerable<VerseModel> items)
	{
		return items.FirstOrDefault()?.DescH;
	}
}