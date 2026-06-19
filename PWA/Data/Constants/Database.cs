namespace PWA.Data.Constants;

public static class Database
{
	public const string DbFileName = "MhbSqlite.db";
	public static string DbPath => $"/{DbFileName}";
	public static string ConnectionString => $"Data Source={DbPath};Mode=ReadOnly;Cache=Shared";

	/*
	Increment this whenever you add a new table (or other schema change) to MhbSqlite.db.
	The published .db file MUST also have: PRAGMA user_version = this value;
	*/
	public const int CurrentSchemaVersion = 2;  // 2026-06-17; 037-detect-db-changes; 

	/*
	 ToDo: in the future add more databases
	public static class Notes
	 */

}