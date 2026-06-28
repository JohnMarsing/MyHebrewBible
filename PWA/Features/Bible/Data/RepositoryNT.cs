using System.Diagnostics;
using Dapper;
using PWA.Data;

namespace PWA.Features.Bible.Data;

public interface IRepositoryNT
{
	Task<List<BookChapterNT>> GetBookChapterNT(int bookID, int chapter);
	Task<List<WordPartByScriptureIdNT>> GetWordPartByScriptureIdNT(int id);
	Task<List<WordPartByScriptureIdAndStrongsNT>> GetGreekBySelectedWord(int scriptureId, int strongs);
	Task<List<WordPartByScriptureIdAndStrongsNT>> GetWordPartsByBCFNT(int bibleBookId, int chapter, string filter);
}

public class RepositoryNT : BaseRepositoryAsync, IRepositoryNT
{
	#region DI
	public RepositoryNT(SqliteWasmBlazorService sqliteService, ILogger<RepositoryNT> logger)
		: base(sqliteService, logger)
	{
	}
	#endregion

	public async Task<List<BookChapterNT>> GetBookChapterNT(int bookID, int chapter)
	{
		Logger.LogDebug("Get B/C: {bookID}/{chapter}", bookID, chapter);
		var parms = new DynamicParameters(new { BookId = bookID, Chapter = chapter });
		string sql = @"
SELECT s.ID, s.BCV, s.Verse, s.VerseOffset, s.KJV, s.DescH, s.DescD
,  COALESCE((
        SELECT COUNT(*) 
        FROM vwTSK2 t 
        WHERE t.ScriptureID = s.ID
    ), 0) AS TskRowCount
FROM Scripture s
WHERE s.BookID=@BookId and s.Chapter=@Chapter
ORDER BY s.ID
		";

		string sqlDetail = @"
SELECT s.Id, s.BCV, s.BookID, s.Chapter, s.Verse
, wp.ScriptureID, wp.WordCount, wp.Greek, wp.KjvWord, wp.Strongs, wp.Transliteration, wp.LexicalGK
FROM WordPartNT wp  
  INNER JOIN Scripture s   
    ON wp.ScriptureID = s.Id
WHERE s.BookID=@BookId AND s.Chapter=@Chapter   -- IMPORTANT: was missing – caused full table scan
ORDER BY wp.ScriptureID;
		";

		var stopwatch = Stopwatch.StartNew();

		var result = await WithConnectionAsync(async connection =>
		{
			var wordPart = await connection.QueryAsync<WordPartNT>(sqlDetail, parms);

			if (wordPart is not null && wordPart.Any())
			{
				var scriptureList = await connection.QueryAsync<BookChapterHeader>(sql, parms);
				var query =
					from s in scriptureList
					join wp in wordPart
					on s.ID equals wp.ScriptureID into wpGroup
					select new BookChapterNT
					{
						Header = new BookChapterHeader
						{
							ID = s.ID,
							BCV = s.BCV,
							Verse = s.Verse,
							VerseOffset = s.VerseOffset,
							KJV = s.KJV,
							DescH = s.DescH,
							DescD = s.DescD,
							TskRowCount = s.TskRowCount
						},
						WordPartList = wpGroup.ToList()
					};
				return query.ToList();
			}
			else
			{
				var scriptureList = await connection.QueryAsync<BookChapterHeader>(sql, parms);
				var query = scriptureList.Select(s => new BookChapterNT
				{
					Header = s,
					WordPartList = null
				});
				return query.ToList();
			}
		}, sql);

		stopwatch.Stop();
		Logger.LogDebug("GetBookChapterNT({BookID},{Chapter}) took {Elapsed} ms. Returned {Count} verses.",
			bookID, chapter, stopwatch.ElapsedMilliseconds, result?.Count ?? 0);

		return result;
	}

	public async Task<List<WordPartByScriptureIdNT>> GetWordPartByScriptureIdNT(int scriptureId)
	{
		var parms = new DynamicParameters(new { ScriptureID = scriptureId });

		string sql = @"
SELECT ScriptureID, WordCount, Greek, KjvWord, Strongs, Transliteration, LexicalGK
FROM WordPartNT
WHERE ScriptureID=@ScriptureID
ORDER BY WordCount
";

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<WordPartByScriptureIdNT>(sql, parms);
			return rows.ToList();
		}, sql);
	}

	public async Task<List<WordPartByScriptureIdAndStrongsNT>> GetGreekBySelectedWord(int scriptureId, int strongs)
	{
		var parms = new DynamicParameters(new { ScriptureID = scriptureId, Strongs = strongs });

		string sql = @"
SELECT ScriptureID, WordCount, Greek, KjvWord, Strongs, Transliteration, LexicalGK
FROM WordPartNT
WHERE ScriptureID=@ScriptureID and Strongs=@Strongs
ORDER BY WordCount
";
		/* ToDo: do I need this? see notes below */
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<WordPartByScriptureIdAndStrongsNT>(sql, parms);
			return rows.ToList();
		}, sql);
	}

	public async Task<List<WordPartByScriptureIdAndStrongsNT>> GetWordPartsByBCFNT(int bibleBookId, int chapter, string filter)
	{
		var parms = new DynamicParameters(new { BookID = bibleBookId, Chapter = chapter, Filter = filter });

		string sql = @"
SELECT wp.ScriptureID, wp.WordCount, wp.Greek, wp.KjvWord, wp.Strongs, wp.Transliteration,  wp.LexicalGK
FROM WordPartNT wp
JOIN Scripture s ON s.Id = wp.ScriptureID
WHERE s.BookID = @BookID
  AND s.Chapter = @Chapter
  AND wp.KjvWord LIKE '%' || @Filter || '%'
ORDER BY s.ID, wp.WordCount
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<WordPartByScriptureIdAndStrongsNT>(sql, parms);
			return rows.ToList();
		}, sql);
	}
}