using System.Diagnostics;
using Dapper;
using PWA.Data;

namespace PWA.Features.Bible.Data;

public interface IRepositoryOT
{
	Task<List<BookChapterWithOT>> GetBookChapterOT(int bookID, int chapter);
	Task<List<WordPartByScriptureId>> GetWordPartByScriptureIdOT(int scriptureId);
	Task<List<WordPartByScriptureIdAndStrongs>> GetHebrewBySelectedWordOT(int scriptureId, int strongs);
	Task<List<WordPartByScriptureIdAndStrongs>> GetWordPartsByBCFOT(int bibleBookId, int chapter, string filter);
	Task<List<WordPartKjv>> GetWordPartKjv(int scriptureId); // Both OT and NT verses uses this
	Task<List<ParashaWithAT>> GetParashaWithAT(int id);
}

public class RepositoryOT : BaseRepositoryAsync, IRepositoryOT
{
	#region DI
	public RepositoryOT(SqliteWasmBlazorService sqliteService, ILogger<RepositoryOT> logger)
		: base(sqliteService, logger)
	{
	}
	#endregion


	public async Task<List<WordPartByScriptureId>> GetWordPartByScriptureIdOT(int scriptureId)
	{
		var parms = new DynamicParameters(new { ScriptureID = scriptureId });

		string sql = @"
SELECT ScriptureID, WordCount, SegmentCount, WordEnum, Hebrew1, Hebrew2, Hebrew3, KjvWord, Strongs, Transliteration, FinalEnum
FROM WordPart 
WHERE ScriptureID=@ScriptureID
ORDER BY WordCount
";

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<WordPartByScriptureId>(sql, parms);
			return rows.ToList();
		}, sql);
	}

	public async Task<List<WordPartByScriptureIdAndStrongsNT>> GetWordPartsByScriptureIdAndStrongs(int scriptureId, int strongs)
	{
		var parms = new DynamicParameters(new { ScriptureID = scriptureId, Strongs = strongs });

		string sql = @"
SELECT ScriptureID, WordCount, SegmentCount, WordEnum, Hebrew1, Hebrew2, Hebrew3, KjvWord, Strongs, Transliteration, FinalEnum
FROM WordPart 
WHERE ScriptureID=@ScriptureID and Strongs=@Strongs
ORDER BY WordCount, SegmentCount
";
		/* ToDo: do I need this? see notes below */
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<WordPartByScriptureIdAndStrongsNT>(sql, parms);
			return rows.ToList();
		}, sql);
	}

	public async Task<List<WordPartByScriptureIdAndStrongs>> GetHebrewBySelectedWordOT(int scriptureId, int strongs)
	{
		var parms = new DynamicParameters(new { ScriptureID = scriptureId, Strongs = strongs });

		string sql = @"
SELECT ScriptureID, WordCount, SegmentCount, WordEnum, Hebrew1, Hebrew2, Hebrew3, KjvWord, Strongs, Transliteration, FinalEnum
FROM WordPart 
WHERE ScriptureID=@ScriptureID and Strongs=@Strongs
ORDER BY WordCount, SegmentCount
";
		/* ToDo: do I need this? see notes below */
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<WordPartByScriptureIdAndStrongs>(sql, parms);
			return rows.ToList();
		}, sql);
	}

	public async Task<List<WordPartByScriptureIdAndStrongs>> GetWordPartsByBCFOT(int bibleBookId, int chapter, string filter)
	{
		var parms = new DynamicParameters(new { BookID = bibleBookId, Chapter = chapter, Filter = filter });

		string sql = @"
SELECT wp.ScriptureID, wp.WordCount, wp.SegmentCount,  wp.WordEnum,
       wp.Hebrew1, wp.Hebrew2, wp.Hebrew3, 
       wp.KjvWord, wp.Strongs, wp.Transliteration,  wp.FinalEnum
FROM WordPart wp
JOIN Scripture s ON s.Id = wp.ScriptureID
WHERE s.BookID = @BookID
  AND s.Chapter = @Chapter
  AND wp.KjvWord LIKE '%' || @Filter || '%'
ORDER BY s.Id, wp.WordCount, wp.SegmentCount
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<WordPartByScriptureIdAndStrongs>(sql, parms);
			return rows.ToList();
		}, sql);
	}

	public async Task<List<WordPartKjv>> GetWordPartKjv(int scriptureId)
	{
		var parms = new DynamicParameters(new { ScriptureID = scriptureId });
		string sql = @"
SELECT ScriptureID, WordCount, ifnull(Strongs,0) AS Strongs, Word
FROM WordPartKjv
WHERE ScriptureID=@ScriptureID
ORDER BY WordCount
";

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<WordPartKjv>(sql, parms);
			return rows.ToList();
		}, sql);
	}

	
	#region Parasha

	public async Task<List<ParashaWithAT>> GetParashaWithAT(int triennialId)
	{
		var parms = new DynamicParameters(new { TriennialId = triennialId });
		string sql = @"
SELECT s.ID, t.SectionId, t.GroupCount, t.ScriptureID_Beg, t.VerseRange
, s.BCV, s.BookID, s.Chapter, s.Verse, s.VerseOffset, s.KJV, s.DescH, s.DescD
FROM  Scripture s
CROSS JOIN vwParashaTableOfContents t 
wHERE t.Id = @TriennialId AND s.ID BETWEEN ScriptureID_Beg AND ScriptureID_End
ORDER BY s.ID
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<ParashaWithAT>(sql, parms);
			return rows.ToList();
		}, sql);
	}
	#endregion

	public async Task<List<BookChapterWithOT>> GetBookChapterOT(int bookID, int chapter)
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
SELECT Id, BCV, BookID, Chapter, Verse
, ScriptureID, WordCount, SegmentCount, WordEnum
, Hebrew1, Hebrew2, Hebrew3
, KjvWord, Strongs, Transliteration, FinalEnum
--, HasTwo
FROM vwAlephTavBookChapterWordPart
WHERE BookID=@BookId and Chapter=@Chapter
ORDER BY Id	
		";

		var stopwatch = Stopwatch.StartNew();

		var result = await WithConnectionAsync(async connection =>
		{

			var wordPart = await connection.QueryAsync<WordPart>(sqlDetail, parms);

			if (wordPart is not null && wordPart.Any())
			{
				var scriptureList = await connection.QueryAsync<BookChapterHeader>(sql, parms);
				var query =
					from s in scriptureList
					join wp in wordPart
					on s.ID equals wp.ScriptureID into wpGroup
					select new BookChapterWithOT
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
				var query = scriptureList.Select(s => new BookChapterWithOT
				{
					Header = s,
					WordPartList = null
				});
				return query.ToList();
			}
		}, sql);

		stopwatch.Stop();
		Logger.LogDebug("GetBookChapterOT({BookID},{Chapter}) took {Elapsed} ms. Returned {Count} verses.",
			bookID, chapter, stopwatch.ElapsedMilliseconds, result?.Count ?? 0);

		return result;
	}

}







// Ignore Spelling: parms, parm, Descr, bookid, Mitzvah, mitzvot, wordpart, wordpartkjv, scriptureid, bookchapterwithat, verselist, begverse endverse Nav, triennialid, alephtavkjvverse, alephtavhebrewverse, alephtavbookchapterwordpartcontext, alephtavtriennialwordpartcontext, Strongs, ifnull