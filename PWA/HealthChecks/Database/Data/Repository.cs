using Dapper;
using PWA.Data;

namespace PWA.HealthChecks.Database.Data;

public interface IRepository
{
	Task<List<TableRowCountQuery>> GetTableRowCountQuery();
	Task<List<BookChapter>> GetBookChapter(int bookID, int chapter);
}

#region DI
public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(SqliteDataService dataService, ILogger<Repository> logger)
		: base(dataService, logger)
	{
	}
#endregion

	public async Task<List<BookChapter>> GetBookChapter(int bookID, int chapter)
	{
		Logger.LogDebug("Get B/C: {bookID}/{chapter}", bookID, chapter);
		var parms = new DynamicParameters(new { BookId = bookID, Chapter = chapter });
		string sql = @"
SELECT ID, BCV, Verse, VerseOffset, KJV, DescH, DescD  
FROM Scripture
WHERE BookID=@BookId and Chapter=@Chapter
ORDER BY ID
		";

		return await WithConnectionAsync(async connection =>
		{
			var sciptureList = await connection.QueryAsync<BookChapter>(sql, parms);
			return sciptureList.ToList();
		}, sql);
	}


	public async Task<List<TableRowCountQuery>> GetTableRowCountQuery() // int id
	{
		//var parms = new DynamicParameters(new { Id = id });
		var sql = $@"
-- DECLARE @yearId int=9999
SELECT
RowCnt, Name
FROM vwTableRowCount
ORDER BY Name
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<TableRowCountQuery>(sql); //, parms
			return rows.ToList();
		}, sql);
	}
}

