namespace PWA.Features.Bible.Parasha.Enums;

// See 057-Strongs-Frequency-Analysis\Notes.md re.  `VerseRange.cs` ! `GetSatVerseList()`
public record VerseRange(RCL.Enums.BibleBook BibleBook, string ChapterVerse, int	BegId, int EndId);

