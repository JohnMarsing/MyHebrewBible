using Dapper;
using PWA.Data;

namespace PWA.Features.ScrollSpy.Data;

public interface IRepository
{
	Task<List<BookChapterWithAT>> GetBookChapterWithAT(int bookID, int chapter);
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


}