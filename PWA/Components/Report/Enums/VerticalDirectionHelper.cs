namespace PWA.Components.Report.Enums;

public class VerticalDirectionHelper
{
	// Valid means 1) it's of interest and 2) if it is of interest, which one of the two directions is it.
	// Called by Sections.razor e.g. `<div id="verse-@item.ID" tabindex="0" @onkeydown="HandleKeyDown">`
	public static (bool, VerticalDirection?) IsKeyDownValid(string key) 
	{
		if (key != "ArrowDown" && key != "ArrowUp") { return (false, null); }

		if (key == "ArrowDown")
		{ 
			return (true, VerticalDirection.Down);
		}
		else
		{
			return (true, VerticalDirection.Up);
		}
	}

	/*
	Returned tuple:
	1. Msg: blank if valid else "Topped out" or "Bottomed out"
	2. verse id: e.g. John 3:16 is verse 16
	3. scripture id: the unique id for the verse in the database

	Parameters:
	1. dir: up or down
	2. currentScriptureId: where the focus is currently
	3. verses: the list of verses in the current chapter
	
	 Called by: Sections.razor two ways
	1. HandleKeyDown() ...  @onkeydown="HandleKeyDown"
	2. <VerseScrollButtons>
	*/

	public static (string, int, int) GetVerseAndScriptureId(
		VerticalDirection dir, int currentScriptureId, List<ReportModel> verses)
	{

		int i = verses.FindIndex(v => v.ID == currentScriptureId);

		if (dir == VerticalDirection.Down)
		{
			if (i < verses.Count - 1)
			{
				return ("", verses[i + 1].Verse, currentScriptureId + 1);
			}
			else
			{
				return (dir.ChapterBoundryMessage, 0, 0);
			}
		}
		else 
		{
			if (i > 0)
			{
				return ("", verses[i - 1].Verse, currentScriptureId - 1);
			}
			else
			{
				return (dir.ChapterBoundryMessage, 0, 0);
			}

		}
	}
}
