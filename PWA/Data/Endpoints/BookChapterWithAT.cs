namespace PWA.Data.Endpoints;

public record BookChapterWithAT
{
	public int ID { get; init; }
	public string? BCV { get; init; }
	public int Verse { get; init; }
	public string? VerseOffset { get; init; }
	public string? KJV { get; init; }
	public string? DescH { get; init; }
	public string? DescD { get; init; }
	public List<CommonDtos.WordPart>? WordPartList { get; init; }
}
