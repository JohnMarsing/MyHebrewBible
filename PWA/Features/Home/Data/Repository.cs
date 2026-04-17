using Dapper;
using PWA.Data;
using PWA.Data.Constants;
using PWA.Data.Endpoints;
using PWA.Data.Endpoints.CommonDtos;
using PWA.Data.Enums;

namespace PWA.Features.Home.Data;

public interface IRepository
{
	Task<List<BookChapterWithAT>> GetBookChapterWithAT(int bookID, int chapter);
	Task<List<WordPartByScriptureId>> GetWordPartByScriptureId(int scriptureId);
	Task<List<WordPartByStrongs>> GetWordPartsByStrongs(int scriptureId, int strongs);
	Task<List<WordPartKjv>> GetWordPartKjv(int scriptureId);
}

#region DI
public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(SqliteDataService dataService, ILogger<Repository> logger)
		: base(dataService, logger)
	{
	}

	#region BibleVerse
	public async Task<List<BookChapterWithAT>> GetBookChapterWithAT(int bookID, int chapter)
	{
		Logger.LogDebug("Get B/C: {bookID}/{chapter}", bookID, chapter);
		var parms = new DynamicParameters(new { BookId = bookID, Chapter = chapter });
		var sql = Api.BookChapterWithAT.Sql;

		return await WithConnectionAsync(async connection =>
		{
			var wordPart = await connection.QueryAsync<WordPart>(Api.BookChapterWithAT.SqlDetail, parms);

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
	public async Task<List<WordPartByScriptureId>> GetWordPartByScriptureId(int scriptureId)
	{
		var parms = new DynamicParameters(new { ScriptureID = scriptureId });
		var sql = Api.WordPartByScriptureId.Sql;

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<WordPartByScriptureId>(sql, parms);
			return rows.ToList();
		}, sql);
	}

	public async Task<List<WordPartByStrongs>> GetWordPartsByStrongs(int scriptureId, int strongs)
	{
		var parms = new DynamicParameters(new { ScriptureID = scriptureId, Strongs = strongs });
		var sql = Api.WordPartByStrongs.Sql;

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<WordPartByStrongs>(sql, parms);
			return rows.ToList();
		}, sql);
	}

	public async Task<List<WordPartKjv>> GetWordPartKjv(int scriptureId)
	{
		var parms = new DynamicParameters(new { ScriptureID = scriptureId });
		var sql = Api.WordPartKjv.Sql;

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<WordPartKjv>(sql, parms);
			return rows.ToList();
		}, sql);
	}
	#endregion
}
#endregion
