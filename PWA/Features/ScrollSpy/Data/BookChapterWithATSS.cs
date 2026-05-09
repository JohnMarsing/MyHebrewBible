namespace PWA.Features.ScrollSpy.Data;

public record BookChapterWithATSS
{
	public int ID { get; init; }
	public string? BCV { get; init; }
	public int Verse { get; init; }
	public string? VerseOffset { get; init; }
	public string? KJV { get; init; }
	public string? DescH { get; init; }
	public string? DescD { get; init; }
	public List<WordPart>? WordPartList { get; init; }
}
