using System.Data;

namespace PWA.Database;

// ToDo: Only references SqliteConnectionFactory, which is Not being used
public interface IDbConnectionFactory
{
	public Task<IDbConnection> CreateConnectionAsync();
}
