namespace PWA.Features.Bible.Data;

public class WordPartByScriptureIdAndStrongsNT
{
	public int ScriptureID { get; set; }
	public int WordCount { get; set; }
	public string? Greek { get; set; }
	public string? KjvWord { get; set; }
	public int Strongs { get; set; }
	public string? Transliteration { get; set; }
	public string? LexicalGK { get; set; } 

}
