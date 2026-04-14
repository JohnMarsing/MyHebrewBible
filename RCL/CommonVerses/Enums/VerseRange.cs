using RCL.Enums;

namespace RCL.CommonVerses.Enums;

public record VerseRange(BibleBook BibleBook, string ChapterVerse, int Chapter, int BegVerse, int EndVerse);
