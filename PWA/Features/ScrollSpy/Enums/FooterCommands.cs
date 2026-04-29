using Ardalis.SmartEnum;

namespace PWA.Features.ScrollSpy.Enums
{
	public abstract class FooterCommands : SmartEnum<FooterCommands>
	{
		#region Id's
		private static class Id
		{
			internal const int Toc = 1;
			internal const int GoToVerse = 2;
			internal const int FindText = 3;
			internal const int ListVerses = 4;
			internal const int Navigate = 5;
		}
		#endregion

		#region Declared Public Instances
		public static readonly FooterCommands Toc = new TocSE();
		public static readonly FooterCommands GoToVerse = new GoToVerseSE();
		public static readonly FooterCommands FindText = new FindTextSE();
		public static readonly FooterCommands ListVerses = new ListVersesSE();
		public static readonly FooterCommands Navigate = new NavigateSE();
		// SE=SmartEnum
		#endregion

		private FooterCommands(string name, int value) : base(name, value) { } // Constructor

		#region Extra Fields
		public abstract string Title { get; }
		public abstract string Icon { get; }
		#endregion

		#region Private Instantiation

		private sealed class TocSE : FooterCommands
		{
			public TocSE() : base($"{nameof(Id.Toc)}", Id.Toc) { }
			public override string Title => "Table of Content";
			public override string Icon => "fa-solid fa-list";
		}

		private sealed class GoToVerseSE : FooterCommands
		{
			public GoToVerseSE() : base($"{nameof(Id.GoToVerse)}", Id.GoToVerse) { }
			public override string Title => "Go to a verse";
			public override string Icon => "fa-solid fa-search";
		}

		private sealed class FindTextSE : FooterCommands
		{
			public FindTextSE() : base($"{nameof(Id.FindText)}", Id.FindText) { }
			public override string Title => "Find text on the page";
			public override string Icon => "fa-solid fa-magnifying-glass";
		}

		private sealed class ListVersesSE : FooterCommands
		{
			public ListVersesSE() : base($"{nameof(Id.ListVerses)}", Id.ListVerses) { }
			public override string Title => "List all verses";
			public override string Icon => "fa-solid fa-list-ol";
		}

		private sealed class NavigateSE : FooterCommands
		{
			public NavigateSE() : base($"{nameof(Id.Navigate)}", Id.Navigate) { }
			public override string Title => "Navigate to book/chapter";
			public override string Icon => "fa-solid fa-book";
		}
		#endregion
	}
}