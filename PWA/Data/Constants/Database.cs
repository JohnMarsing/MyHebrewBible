namespace PWA.Data.Constants;

public static class Database
{
	public const string DbFileName = "MhbSqlite.db";
	public static string DbPath => $"/{DbFileName}";
	public static string ConnectionString => $"Data Source={DbPath};Mode=ReadOnly;Cache=Shared";
	//public const string ConnectionStringKey = "ConnectionStrings:MhbSqlite"; use if you have a appsettings.json file
}