namespace PWA.Features.Bible.Data;

public static class DataExtensions
{
	public static VerseModel MapFromBookChapter(this BookChapterWithOT bc, int bookId, int chapter)
	{
		return new VerseModel
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

	public static VerseModelNT MapFromBookChapterNT(this BookChapterNT bc, int bookId, int chapter)
	{
		return new VerseModelNT
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
						.Select(wp => new WordPartNT
						{
							Id = wp.Id,
							BCV = wp.BCV,
							BookID = wp.BookID,
							Chapter = wp.Chapter,
							Verse = wp.Verse,
							ScriptureID = wp.ScriptureID,
							WordCount = wp.WordCount,
							Greek = wp.Greek,
							KjvWord = wp.KjvWord,
							Strongs = wp.Strongs,
							Transliteration = wp.Transliteration,
							LexicalGK = wp.LexicalGK
						})
						.ToList() ?? []
		};
	}

	public static VerseModel MapFromBookChapterFilter(this BookChapterWithOT bc, int bookId, int chapter, string filterText)
	{
		return new VerseModel
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

	public static VerseModelNT MapFromBookChapterFilterNT(this BookChapterNT bc, int bookId, int chapter, string filterText)
	{
		return new VerseModelNT
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
						.Select(wp => new WordPartNT
						{
							Id = wp.Id,
							BCV = wp.BCV,
							BookID = wp.BookID,
							Chapter = wp.Chapter,
							Verse = wp.Verse,
							ScriptureID = wp.ScriptureID,
							WordCount = wp.WordCount,
							Greek = wp.Greek,
							KjvWord = wp.KjvWord,
							Strongs = wp.Strongs,
							Transliteration = wp.Transliteration,
							LexicalGK = wp.LexicalGK
						})
						.ToList() ?? new()
		};
	}

	public static VerseModel MapFromParasha(ParashaWithAT parasha)
	{
		return new VerseModel
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

	public static IReadOnlyList<TableOfContentRecord> ToTableOfContentMapping(this IEnumerable<VerseModel> items)
	{
		return items
			.Where(v => v.Header.DescD != "NULL")
			.OrderBy(v => v.Header.ID)
			.Select(v => new TableOfContentRecord(v.Header.Verse, v.Header.ID, v.Header.DescD!, v.Header.VerseOffset))
			.ToList();
	}

	public static IReadOnlyList<TableOfContentRecord> ToTableOfContentMappingNT(this IEnumerable<VerseModelNT> items)
	{
		return items
			.Where(v => v.Header.DescD != "NULL")
			.OrderBy(v => v.Header.ID)
			.Select(v => new TableOfContentRecord(v.Header.Verse, v.Header.ID, v.Header.DescD!, v.Header.VerseOffset))
			.ToList();
	}

	public static string? GetTitle(this IEnumerable<VerseModel> items)
	{
		return items.FirstOrDefault()?.Header.DescH;
	}

	public static string? GetTitleNT(this IEnumerable<VerseModelNT> items)
	{
		return items.FirstOrDefault()?.Header.DescH;
	}

}