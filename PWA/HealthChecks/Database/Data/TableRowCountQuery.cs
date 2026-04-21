namespace PWA.HealthChecks.Database.Data;

public record TableRowCountQuery
{
	public int RowCnt { get; init; }
	public string? Name { get; init; }
}
