namespace PWA.Data.Endpoints;

public class WordPartKjv
{
	public int ScriptureID { get; set; }
	public int WordCount { get; set; }
	public int Strongs { get; set; } // fn 1
	public string? Word { get; set; }
}