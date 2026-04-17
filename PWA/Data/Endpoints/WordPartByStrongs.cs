namespace PWA.Data.Endpoints;

public class WordPartByStrongs
{
	public int ScriptureID { get; set; }
	public int WordCount { get; set; }
	public int SegmentCount { get; set; }
	public int WordEnum { get; set; }
	public string? Hebrew1 { get; set; }
	public string? Hebrew2 { get; set; }
	public string? Hebrew3 { get; set; }
	public string? KjvWord { get; set; }
	public int Strongs { get; set; }
	public string? Transliteration { get; set; }
	public int? FinalEnum { get; set; }
}
