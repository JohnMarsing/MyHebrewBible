namespace PWA.Features.Bible.Data;

public record WordPartNT
{
	public int Id { get; init; }                   
	public string? BCV { get; init; }
	public int BookID { get; init; }              
	public int Chapter { get; init; }             
	public int Verse { get; init; }               
	public int ScriptureID { get; init; }         
	public int WordCount { get; init; }
	public string? Greek { get; init; }
	public string? KjvWord { get; init; }
	public int Strongs { get; init; }              
	public string? Transliteration { get; init; }
	public string? LexicalGK { get; init; }				
}
