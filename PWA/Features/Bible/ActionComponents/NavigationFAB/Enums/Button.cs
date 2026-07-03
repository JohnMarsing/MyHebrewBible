using Ardalis.SmartEnum;

namespace PWA.Features.Bible.ActionComponents.NavigationFAB.Enums;

public abstract class Button : SmartEnum<Button>
{
	private static class Id
	{
		internal const int Top = 1;
		internal const int Prev = 2;
		internal const int Next = 3;
		internal const int Bottom = 4;
		internal const int ChapterPrev = 5;
		internal const int ChapterNext = 6;
	}

	#region Instances
	public static readonly Button Top = new TopSE();
	public static readonly Button Prev = new PrevSE();
	public static readonly Button Next = new NextSE();
	public static readonly Button Bottom = new BottomSE();
	public static readonly Button ChapterPrev = new ChapterPrevSE();
	public static readonly Button ChapterNext = new ChapterNextSE();
	#endregion

	private Button(string name, int value) : base(name, value) { }

	#region Extra Fields
	public abstract string CssClass { get; }
	public abstract string IconClass { get; }
	public abstract string Title { get; }
	#endregion

	private sealed class TopSE : Button
	{
		public TopSE() : base(nameof(Top), Id.Top) { }
		public override string CssClass => "btn btn-info mt-1 rounded-circle";
		public override string IconClass => "fa-solid fa-t";
		public override string Title => "Go to Top";
	}

	private sealed class PrevSE : Button
	{
		public PrevSE() : base(nameof(Prev), Id.Prev) { }
		public override string CssClass => "btn btn-primary my-1 rounded-circle";
		public override string IconClass => "fas fa-arrow-up";
		public override string Title => "Previous verse / chapter";
	}

	private sealed class NextSE : Button
	{
		public NextSE() : base(nameof(Next), Id.Next) { }
		public override string CssClass => "btn btn-primary my-1 rounded-circle";
		public override string IconClass => "fas fa-arrow-down";
		public override string Title => "Next verse / chapter";
	}

	private sealed class BottomSE : Button
	{
		public BottomSE() : base(nameof(Bottom), Id.Bottom) { }
		public override string CssClass => "btn btn-info mb-1 rounded-circle";
		public override string IconClass => "fa-solid fa-b";
		public override string Title => "Go to Bottom";
	}

	private sealed class ChapterPrevSE : Button
	{
		public ChapterPrevSE() : base(nameof(ChapterPrev), Id.ChapterPrev) { }
		public override string CssClass => "btn btn-primary my-1 rounded-circle";
		//public override string IconClass => "fa-solid fa-caret-left"; 
		public override string IconClass => "fa-solid fa-arrow-left-long"; 
		public override string Title => "Previous chapter";
	}

	private sealed class ChapterNextSE : Button
	{
		public ChapterNextSE() : base(nameof(ChapterNext), Id.ChapterNext) { }
		public override string CssClass => "btn btn-primary my-1 rounded-circle";
		//public override string IconClass => "fa-solid fa-caret-right";
		public override string IconClass => "fa-solid fa-arrow-right-long";
		public override string Title => "Next chapter";
	}
}