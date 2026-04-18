using Dapper;
using PWA.Data;
using PWA.Data.Constants;
using PWA.Data.Endpoints;
using PWA.Data.Endpoints.CommonDtos;
using PWA.Data.Enums;

namespace PWA.Features.Parasha.Data;

public interface IRepository
{
	Task<List<ParashaWithAT>> GetParashaWithAT(int id);
}

#region DI
public class Repository : BaseRepositoryAsync, IRepository
{
	public Repository(SqliteDataService dataService, ILogger<Repository> logger)
		: base(dataService, logger)
	{
	}

	//TriennialId = triennialId
	public async Task<List<ParashaWithAT>> GetParashaWithAT(int triennialId)
	{
		var parms = new DynamicParameters(new { TriennialId = triennialId });
		var sql = Api.ParashaWithAT.Sql;

		return await WithConnectionAsync(async connection =>
		{
			var rows = await connection.QueryAsync<ParashaWithAT>(sql, parms);
			return rows.ToList();
		}, sql);
	}
	#endregion
}

