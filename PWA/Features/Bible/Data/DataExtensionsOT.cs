namespace PWA.Features.Bible.Data;

public static class DataExtensionsOT
{
	public static VerseModelOT MapFromBookChapterOT(this BookChapterWithOT bc, int bookId, int chapter)
	{
		return new VerseModelOT
		{
			Header = new VerseHeader
			{
				ID = bc.Header.ID,
				BCV = bc.Header.BCV,
				Verse = bc.Header.Verse,
				VerseOffset = bc.Header.VerseOffset,
				KJV = bc.Header.KJV,
				DescH = bc.Header.DescH,
				DescD = bc.Header.DescD,
				BookID = bookId,
				Chapter = chapter,
				TskRowCount = bc.Header.TskRowCount
			},
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

	public static VerseModelOT MapFromBookChapterFilterOT(this BookChapterWithOT bc, int bookId, int chapter, string filterText)
	{
		return new VerseModelOT
		{
			Header = new VerseHeader
			{
				ID = bc.Header.ID,
				BCV = bc.Header.BCV,
				Verse = bc.Header.Verse,
				VerseOffset = bc.Header.VerseOffset,
				KJV = bc.Header.KJV,
				DescH = bc.Header.DescH,
				DescD = bc.Header.DescD,
				BookID = bookId,
				Chapter = chapter
			},
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

	public static VerseModelOT MapFromParashaOT(ParashaWithAT parasha)
	{
		return new VerseModelOT
		{
			Header = new VerseHeader
			{
				ID = parasha.ID,
				BCV = parasha.BCV,
				Verse = parasha.Verse,
				VerseOffset = parasha.VerseOffset,
				KJV = parasha.KJV,
				DescH = parasha.DescH,
				DescD = parasha.DescD,
				BookID = parasha.BookID,
				Chapter = parasha.Chapter
			},
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

	public static IReadOnlyList<TableOfContentRecord> ToTableOfContentMappingOT(this IEnumerable<VerseModelOT> items)
	{
		return items
			.Where(v => v.Header.DescD != "NULL")
			.OrderBy(v => v.Header.ID)
			.Select(v => new TableOfContentRecord(v.Header.Verse, v.Header.ID, v.Header.DescD!, v.Header.VerseOffset))
			.ToList();
	}


	public static string? GetTitleOT(this IEnumerable<VerseModelOT> items)
	{
		return items.FirstOrDefault()?.Header.DescH;
	}

}