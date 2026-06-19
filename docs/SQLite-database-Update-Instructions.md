
# SQLite Database Update Instructions
When a new database object (e.g. table, column, index, etc.) is added to the SQLite database, 
the following steps must be taken to ensure that existing databases are properly updated:

1. Increment the database schema version number in both the 
- `Database.cs` file and the 
- `SQLite` database itself.
2. Adjust `vwTableRowCount` view in the SQLite database (if necessary)

```sql
PRAGMA user_version = 2;   -- 2026-06-17; 037-detect-db-changes; 
```

###  `Database.cs` 
- `namespace PWA.Data.Constants;`

```csharp
namespace PWA.Data.Constants;
//...
	public const int CurrentSchemaVersion = 2;  // 2026-06-17; 037-detect-db-changes; 
}
```