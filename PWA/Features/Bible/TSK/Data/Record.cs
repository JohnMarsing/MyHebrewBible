namespace PWA.Features.Bible.TSK.Data;



public record Record
{
	public long RowNum { get; set; }
	//public int RelatedId { get; set; }
	public int BookID { get; set; }
	public int Chapter { get; set; }
	public int Verse { get; set; }
	public int Votes { get; set; } = 0;
	public string BCV { get; set; } = string.Empty;
	public string? KJV { get; set; }
}

/*
Dapper doesn't like the positional record declaration style
```
public record Record(long RowNum, int RelatedId, int Votes, string BCV, string? KJV);
```
*/