using Ardalis.SmartEnum;

namespace PWA.Features.Home.Enums;

public abstract class Button : SmartEnum<Button>
{
	#region Id's
	private static class Id
	{
		internal const int LastBCV = 1;
		internal const int RecentList = 2;
		internal const int ParashaList = 3;
	}
	#endregion

	#region Declared Public Instances
	public static readonly Button LastBCV = new LastBCVSE();
	public static readonly Button RecentList = new RecentListSE();
	public static readonly Button ParashaList = new ParashaListSE();
	#endregion

	private Button(string name, int value) : base(name, value)
	{
	}

	#region Extra Fields
	public abstract string Title { get; }
	public abstract string HeaderCSS { get; }
	public abstract string HeaderFontSize { get; }
	public abstract string BodyCSS { get; }
	public abstract string BodyFontSize { get; }
	#endregion

	private sealed class LastBCVSE : Button
	{
		public LastBCVSE() : base(nameof(LastBCV), Id.LastBCV) { }
		public override string Title => "Last Entry";
		public override string HeaderCSS => "bg-primary text-white";
		public override string HeaderFontSize => "fs-3";
		public override string BodyCSS => "list-group-item-primary";
		public override string BodyFontSize => "fs-3";
	}

	private sealed class RecentListSE : Button
	{
		public RecentListSE() : base(nameof(RecentList), Id.RecentList) { }
		public override string Title => "Recent Entries";
		public override string HeaderCSS => "bg-danger text-white";
		public override string HeaderFontSize => "fs-3";
		public override string BodyCSS => "list-group-item-danger";
		public override string BodyFontSize => "fs-3";
	}

	private sealed class ParashaListSE : Button
	{
		public ParashaListSE() : base(nameof(ParashaList), Id.ParashaList) { }
		public override string Title => "Current Parasha";
		public override string HeaderCSS => "bg-warning text-black";
		public override string HeaderFontSize => "fs-3";
		public override string BodyCSS => "list-group-item-warning";
		public override string BodyFontSize => "fs-3";
	}
}
