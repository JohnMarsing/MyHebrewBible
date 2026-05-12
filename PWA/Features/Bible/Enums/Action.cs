using Ardalis.SmartEnum;

namespace PWA.Features.Bible.Enums;

public enum ActionGroupEnum
{
	Left = 1,
	Center = 2,
	Right = 3
}

//ToDo: Action is not the best name because it conflicts with `	public event Action? OnChange;` like in PWA\Features\Bible\State.cs
//      Consider renaming it Command or something
public abstract class Action : SmartEnum<Action>
{
	#region Id's
	private static class Id
	{
		internal const int Find = 1;
		internal const int BCV = 2; // Book Chapter Verse
		internal const int ToC = 3;
	}
	#endregion

	#region Declared Public Instances
	public static readonly Action Find = new FindSE();
	public static readonly Action BCV = new BCVSE(); // Book Chapter Verse
	public static readonly Action Toc = new TocSE();
	#endregion

	private Action(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Title { get; }
	public abstract string Label { get; }
	public abstract string Icon { get; }
	public abstract string BtnColor { get; }
	public abstract ActionGroupEnum ActionGroupEnum { get; }
	#endregion

	#region Private Instantiation

	private sealed class FindSE : Action
	{
		public FindSE() : base($"{nameof(Id.Find)}", Id.Find) { }
		public override string Title => "Find text and/or Strong's";
		public override string Label => "Find";
		public override string Icon => "fa-solid fa-magnifying-glass";
		public override string BtnColor => "btn-info";
		public override ActionGroupEnum ActionGroupEnum => ActionGroupEnum.Left;
	}

	private sealed class BCVSE : Action
	{
		public BCVSE() : base($"{nameof(Id.BCV)}", Id.BCV) { }
		public override string Title => "Change book/chapter/verse";
		public override string Label => "B/C/V";
		public override string Icon => "fa-solid fa-arrow-up";
		public override string BtnColor => "btn-info";
		public override ActionGroupEnum ActionGroupEnum => ActionGroupEnum.Center;
	}

	private sealed class TocSE : Action
	{
		public TocSE() : base($"{nameof(Id.ToC)}", Id.ToC) { }
		public override string Title => "Table of Content";
		public override string Label => "TOC";
		public override string Icon => "fa-solid fa-t";  // fa-solid fa-arrow-up
		public override string BtnColor => "btn-info";
		public override ActionGroupEnum ActionGroupEnum => ActionGroupEnum.Right;
	}
	#endregion
}