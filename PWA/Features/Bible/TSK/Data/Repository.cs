using Dapper;
using PWA.Data;

namespace PWA.Features.Bible.TSK.Data;

public interface IRepository
{
	Task<List<Record>> GetTSK(int scriptureId);
}

public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(SqliteWasmBlazorService sqliteService, ILogger<Repository> logger)
		: base(sqliteService, logger) 	{	}

	public async Task<List<Record>> GetTSK(int scriptureId)
	{
		Logger.LogDebug("Get TSK for ScriptureID: {ScriptureID}", scriptureId);
		var parms = new DynamicParameters(new { ScriptureID = scriptureId });
		//v2.RelatedId, v2.BCV2 AS BCV, v2.Votes, s.KJV
		string sql = @"
SELECT 
  ROW_NUMBER() OVER (ORDER BY v2.BookID, v2.Chapter, v2.Verse) AS RowNum, 
  v2.BookID, v2.Chapter, v2.Verse, v2.BCV2 AS BCV, v2.Votes, s.KJV
FROM vwTSK2 v2
LEFT JOIN Scripture s ON v2.RelatedId = s.Id
WHERE v2.ScriptureID = @ScriptureID
ORDER BY v2.RelatedId;
";
		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<Record>(sql, parms);
			return rows.ToList();
		}, sql);
	}

}
