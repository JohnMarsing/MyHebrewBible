using Ardalis.SmartEnum;

namespace PWA.HealthChecks.Enums;

public enum GroupType
{
	Database = 1,
	Sentry = 2
}

public abstract class Button : SmartEnum<Button>
{
	#region Id's
	private static class Id
	{
		internal const int TableRowCntTable = 1;
		internal const int Gen01 = 2;

		internal const int Throw = 3;
		internal const int ThrowAndCatch = 4;
		internal const int TestSqlite = 5;
	}
	#endregion

	#region Declared Public Instances
	public static readonly Button TableRowCntTable = new TableRowCntTableSE();
	public static readonly Button Gen01 = new Gen01SE();
	public static readonly Button Throw = new ThrowSE();
	public static readonly Button ThrowAndCatch = new ThrowAndCatchSE();
	public static readonly Button TestSqlite = new TestSqliteSE();
	#endregion

	private Button(string name, int value) : base(name, value)
	{
	}

	#region Extra Fields
	public abstract GroupType GroupTypeEnum { get; }
	public abstract string Title { get; }
	public abstract string Icon { get; }
	#endregion

	private sealed class TableRowCntTableSE : Button
	{
		public TableRowCntTableSE() : base(nameof(TableRowCntTable), Id.TableRowCntTable) { }
		public override GroupType GroupTypeEnum => GroupType.Database;
		public override string Title => "Table Row Count";
		public override string Icon => "fa-solid fa-table";
	}

	private sealed class Gen01SE : Button
	{
		public Gen01SE() : base(nameof(Gen01), Id.Gen01) { }
		public override GroupType GroupTypeEnum => GroupType.Database;
		public override string Title => "Book & Chapter Gen-01";
		public override string Icon => "fa-solid fa-minus";
	}

	private sealed class ThrowSE : Button
	{
		public ThrowSE() : base(nameof(Throw), Id.Throw) { }
		public override GroupType GroupTypeEnum => GroupType.Sentry;
		public override string Title => "Throw (no catch)";
		public override string Icon => "fa-solid fa-bomb";
	}

	private sealed class ThrowAndCatchSE : Button
	{	
		public ThrowAndCatchSE() : base(nameof(ThrowAndCatch), Id.ThrowAndCatch) { }
		public override GroupType GroupTypeEnum => GroupType.Sentry;
		public override string Title => "Throw and Catch";
		public override string Icon => "fa-solid fa-arrow-rotate-left";
	}

	private sealed class TestSqliteSE : Button
	{
		public TestSqliteSE() : base(nameof(TestSqlite), Id.TestSqlite) { }
		public override GroupType GroupTypeEnum => GroupType.Database;
		public override string Title => "Test Sqlite";
		public override string Icon => "fa-solid fa-database";
	}
}