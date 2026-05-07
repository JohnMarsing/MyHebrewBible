using Ardalis.SmartEnum;

namespace PWA.Features.ScrollSpy.Enums
{

	public enum ActionGroupEnum
	{
		Left = 1,
		Center = 2,
		Right = 3
	}

	public abstract class Action : SmartEnum<Action>
	{
		#region Id's
		private static class Id
		{
			internal const int Find = 1;
			internal const int BCV = 2; // Book Chapter Verse
			internal const int ToC = 3;
			internal const int PrevVerse = 4;
			internal const int GoToVerse = 5;
			internal const int NextVerse = 6;
		}
		#endregion

		#region Declared Public Instances
		public static readonly Action Find = new FindSE();
		public static readonly Action BCV = new BCVSE(); // Book Chapter Verse
		public static readonly Action Toc = new TocSE();
		public static readonly Action PrevVerse	= new PrevVerseSE();
		public static readonly Action GoToVerse = new GoToVerseSE();
		public static readonly Action NextVerse = new NextVerseSE();
		// SE=SmartEnum
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

		private sealed class PrevVerseSE : Action
		{
			public PrevVerseSE() : base($"{nameof(Id.PrevVerse)}", Id.PrevVerse) { }
			public override string Title => "Go to a previous verse";
			public override string Label => "Prev";
			public override string Icon => "fa-solid fa-arrow-left";
			public override string BtnColor => "btn-primary";
			public override ActionGroupEnum ActionGroupEnum => ActionGroupEnum.Right;
		}

		private sealed class GoToVerseSE : Action
		{
			public GoToVerseSE() : base($"{nameof(Id.GoToVerse)}", Id.GoToVerse) { }
			public override string Title => "Go to a verse";
			public override string Label => "List";
			public override string Icon => "fa-solid fa-g";
			public override string BtnColor => "btn-primary";
			public override ActionGroupEnum ActionGroupEnum => ActionGroupEnum.Right;
		}

		private sealed class NextVerseSE : Action
		{
			public NextVerseSE() : base($"{nameof(Id.NextVerse)}", Id.NextVerse) { }
			public override string Title => "Go to the next verse";
			public override string Label => "Next";
			public override string Icon => "fa-solid fa-arrow-right";
			public override string BtnColor => "btn-primary";
			public override ActionGroupEnum ActionGroupEnum => ActionGroupEnum.Right;
		}
		#endregion
	}
}