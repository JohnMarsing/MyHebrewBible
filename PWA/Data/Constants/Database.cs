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
	//public const int CurrentSchemaVersion = 2;  // 2026-06-17; 037-detect-db-changes; 
	//public const int CurrentSchemaVersion = 3;  // 2026-06-22; 038-TSK-UI; added vwTSK1 and vwTSK2
	//public const int CurrentSchemaVersion = 5;  // 2026-06-23; 038-TSK-UI; dropped vwTSK, vwTSK_OLD, vwTSK_Combined, vwTSK_GroupByBCV
	//public const int CurrentSchemaVersion = 6;  // 2026-06-28; 039-Append-NT; Added NT to WordPartKjv
	public const int CurrentSchemaVersion = 7;  // 2026-06-28; Added vwTSK1, vwTSK1, Book and Book Data
	/*
	 ToDo: in the future add more databases
	public static class Notes
	 */

}