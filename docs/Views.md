# Views

## vwAlephTavBookChapterWordPart.sql
```sql
CREATE VIEW vwAlephTavBookChapterWordPart 
AS  
 
/* 
SELECT *  
FROM vwAlephTavBookChapterWordPart 
WHERE BookID=2 AND Chapter=20 
ORDER BY Id 
 
 
 
SELECT  
at.Verse 
, at.HasTwo 
, atwp.* 
FROM AlephTavVerse at 
  INNER JOIN vwAlephTavBookChapterWordPart atwp 
    ON at.ScriptureID=atwp.ScriptureID 
WHERE at.ScriptureID=1 
--WHERE BookID=1 AND Chapter=14 
ORDER BY Id 
 
SELECT * FROM vwAlephTavBookChapterWordPart  
WHERE BookID=1 AND Chapter=2 
ORDER BY Id 
 
 
SELECT * FROM vwAlephTavBookChapterWordPart  
WHERE BookID=1 AND Chapter=14 
ORDER BY Id 
 
SELECT Id, BCV, BookID, Chapter 
, ScriptureID, WordCount, SegmentCount, WordEnum 
, Hebrew1, Hebrew2, Hebrew3 
, KjvWord, Strongs, Transliteration, FinalEnum 
FROM vwAlephTavBookChapterWordPart 
ORDER BY Id 
 
--DROP VIEW vwAlephTavBookChapterWordPart 
 
*/ 
 
SELECT  
atwp.Id, s.BCV, s.BookID, s.Chapter, s.Verse 
, wp.ScriptureID, wp.WordCount, wp.SegmentCount, wp.WordEnum, wp.Hebrew1, wp.Hebrew2, wp.Hebrew3, wp.KjvWord, wp.Strongs, wp.Transliteration, wp.FinalEnum 
FROM WordPart wp 
  INNER JOIN AlephTavVerseWordPart atwp 
    ON wp.ScriptureID=atwp.ScriptureID AND wp.WordCount=atwp.WordCount 
  INNER JOIN Scripture s  
    ON atwp.ScriptureID = s.Id;

```

## vwAlephTavTriennialWordPart.sql
```sql
CREATE VIEW vwAlephTavTriennialWordPart 
AS  
 
/* 
 
SELECT *  
FROM vwAlephTavTriennialWordPart 
--WHERE TriennialId=1  
WHERE TriennialId=8 
ORDER BY Id 
 
*/ 
 
SELECT t.Id AS TriennialId, t.VerseRange AS TriennialVerseRange, atwp.* 
FROM vwAlephTavVerseWordPart atwp 
CROSS JOIN  Triennial t 
WHERE atwp.ScriptureID BETWEEN t.ScriptureID_Beg AND t.ScriptureID_End;

```

## vwParashaTableOfContents.sql
```sql
CREATE VIEW vwParashaTableOfContents  
AS   
  
/*  
SELECT *   
FROM vwParashaTableOfContents  
WHERE Id=15  
ORDER BY SectionId, RowCnt  
 
*/  
  
SELECT SectionId  
, ROW_NUMBER() OVER (PARTITION BY Id, SectionId ORDER BY SectionId, RowCnt) AS GroupCount  
, BookId, AnnualId, VerseRange, ScriptureID_Beg, ScriptureID_End, Id, RowIdentity, RowCnt  
FROM Triennial;

```

## vwTableRowCount.sql
```sql
CREATE VIEW vwTableRowCount
AS   
  
/*  
SELECT * FROM vwTableRowCount  

DROP VIEW vwTableRowCount

*/  

SELECT
(SELECT count(*) FROM AlephTavVerse)           AS AlephTavVerseCnt,          --    612
(SELECT count(*) FROM AlephTavVerseWordPart)   AS AlephTavVerseWordPartCnt,  --  3,165
(SELECT count(*) FROM Article)                 AS ArticleCnt,                --    630
(SELECT count(*) FROM JotAndTittle)            AS JotAndTittleCnt,           --     74
(SELECT count(*) FROM Mitzvot)                 AS MitzvotCnt,                --    645  
(SELECT count(*) FROM WordPart)                AS WordPartCnt,               -- 304,574
(SELECT count(*) FROM WordPartKjv)             AS WordPartKjvCnt;            -- 322,350

```

## vwUtilScripts.sql
```sql
/*
VACUUM;

SELECT name FROM sqlite_master WHERE type = 'index';

*/
```
