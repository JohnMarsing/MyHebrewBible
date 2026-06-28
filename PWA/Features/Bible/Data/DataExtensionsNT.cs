namespace PWA.Features.Bible.Data;

public static class DataExtensionsNT
{
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

	public static IReadOnlyList<TableOfContentRecord> ToTableOfContentMappingNT(this IEnumerable<VerseModelNT> items)
	{
		return items
			.Where(v => v.Header.DescD != "NULL")
			.OrderBy(v => v.Header.ID)
			.Select(v => new TableOfContentRecord(v.Header.Verse, v.Header.ID, v.Header.DescD!, v.Header.VerseOffset))
			.ToList();
	}

	public static string? GetTitleNT(this IEnumerable<VerseModelNT> items)
	{
		return items.FirstOrDefault()?.Header.DescH;
	}

}
