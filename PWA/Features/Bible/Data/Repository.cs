using Dapper;
using PWA.Data;

namespace PWA.Features.Bible.Data;

public interface IRepository
{
	Task<List<BookChapterWithAT>> GetBookChapterWithAT(int bookID, int chapter);
	Task<List<WordPartByScriptureId>> GetWordPartByScriptureId(int scriptureId);
	Task<List<WordPartByScripureIdAndStrongs>>  GetWordPartsByScripureIdAndStrongs(int scriptureId, int strongs);
	Task<List<WordPartKjv>> GetWordPartKjv(int scriptureId);
	//Task<List<ParashaWithAT>> GetParashaWithAT(int id);
	Task<List<BookChapterWithAT>> GetParashaWithAT(int id);
}


public class Repository : BaseRepositoryAsync, IRepository
{
#region DI
	public Repository(SqliteDataService dataService, ILogger<Repository> logger)
		: base(dataService, logger)
	{
	}
	#endregion

	#region BibleVerse
	public async Task<List<BookChapterWithAT>> GetBookChapterWithAT(int bookID, int chapter)
	{
		Logger.LogDebug("Get B/C: {bookID}/{chapter}", bookID, chapter);
		var parms = new DynamicParameters(new { BookId = bookID, Chapter = chapter });
		string sql = @"
SELECT ID, BCV, Verse, VerseOffset, KJV, DescH, DescD  
FROM Scripture
WHERE BookID=@BookId and Chapter=@Chapter
ORDER BY ID
		";

		string sqlDetail = @"
SELECT Id, BCV, BookID, Chapter, Verse
, ScriptureID, WordCount, SegmentCount, WordEnum
, Hebrew1, Hebrew2, Hebrew3
, KjvWord, Strongs, Transliteration, FinalEnum
--, HasTwo
FROM vwAlephTavBookChapterWordPart
--WHERE BookID=@BookId and Chapter=@Chapter
ORDER BY Id	
		";

		return await WithConnectionAsync(async connection =>
		{

			var wordPart = await connection.QueryAsync<WordPart>(sqlDetail, parms);

			if (wordPart is not null && wordPart.Any())
			{
				var sciptureList = await connection.QueryAsync<BookChapterWithAT>(sql, parms);
				var query =
					from s in sciptureList
					join wp in wordPart
					on s.ID equals wp.ScriptureID into wpGroup
					select new BookChapterWithAT
					{
						ID = s.ID,
						BCV = s.BCV,
						Verse = s.Verse,
						VerseOffset = s.VerseOffset,
						KJV = s.KJV,
						DescH = s.DescH,
						DescD = s.DescD,
						WordPartList = wpGroup.ToList()
					};
				return query.ToList();
			}
			else
			{
				var sciptureList = await connection.QueryAsync<BookChapterWithAT>(sql, parms);
				return sciptureList.ToList();
			}
		}, sql);
	}
	#endregion

	#region WordPart
	//GetWordPartKjv
	public async Task<List<WordPartByScriptureId>> GetWordPartByScriptureId(int scriptureId)
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

	public async Task<List<WordPartByScripureIdAndStrongs>> GetWordPartsByScripureIdAndStrongs(int scriptureId, int strongs)
	{
		var parms = new DynamicParameters(new { ScriptureID = scriptureId, Strongs = strongs });

		string sql = @"
SELECT ScriptureID, WordCount, SegmentCount, WordEnum, Hebrew1, Hebrew2, Hebrew3, KjvWord, Strongs, Transliteration, FinalEnum
FROM WordPart 
WHERE ScriptureID=@ScriptureID and Strongs=@Strongs
ORDER BY WordCount, SegmentCount
";
	/*
		string SqlOrderBy = "";

		string SqlDetail = @"
SELECT Id, BCV, BookID, Chapter, Verse
, ScriptureID, WordCount, SegmentCount, WordEnum
, Hebrew1, Hebrew2, Hebrew3
, KjvWord, Strongs, Transliteration, FinalEnum
--, HasTwo
FROM vwAlephTavTriennialWordPart
--WHERE TriennialId=@TriennialId
ORDER BY Id	
		";
		*/

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<WordPartByScripureIdAndStrongs>(sql, parms);
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

	#endregion

	#region Parasha

	//	public async Task<List<ParashaWithAT>> GetParashaWithAT(int triennialId)

	public async Task<List<BookChapterWithAT>> GetParashaWithAT(int triennialId)
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
			//var rows = await connection.QueryAsync<ParashaWithAT>(sql, parms);
			var rows = await connection.QueryAsync<BookChapterWithAT>(sql, parms);
			return rows.ToList();
		}, sql);
	}
	#endregion

}

// Ignore Spelling: Descr, bookid, Mitzvah, mitzvot, wordpart, wordpartkjv, scriptureid, bookchapterwithat, verselist, begverse endverse Nav, triennialid, alephtavkjvverse, alephtavhebrewverse, alephtavbookchapterwordpartcontext, alephtavtriennialwordpartcontext, Strongs, ifnull