using Dapper;
using PWA.Data;

namespace PWA.HealthChecks.Database.Data;

public interface IRepository
{
	Task<List<TableRowCountQuery>> GetTableRowCountQuery();
}

#region DI
public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(SqliteDataService dataService, ILogger<Repository> logger)
		: base(dataService, logger)
	{
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
	#endregion
}

