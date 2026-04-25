using Ardalis.SmartEnum;

namespace PWA.Components.Report.Enums;

public abstract class VerticalDirection : SmartEnum<VerticalDirection>
{
  #region Id's
  private static class Id
  {
    internal const int Down = 1;
    internal const int Up = 2;
  }
  #endregion

  #region Declared Public Instances
  public static readonly VerticalDirection Down = new DownSE();
  public static readonly VerticalDirection Up = new UpSE();
  // SE=SmartEnum
  #endregion

  private VerticalDirection(string name, int value) : base(name, value) { } // Constructor

	#region Extra Fields
	public abstract string Title { get; }
	public abstract string Icon { get; }
	public abstract string ChapterBoundryMessage { get; }
	public abstract string KeyboardKey { get; }
	#endregion


	#region Private Instantiation

	private sealed class DownSE : VerticalDirection
  {
    public DownSE() : base($"{nameof(Id.Down)}", Id.Down) { }
    public override string Title => "go down to verse ";
    public override string Icon => "fa-solid fa-arrow-down";  
    public override string ChapterBoundryMessage => "Bottomed out";
		public override string KeyboardKey => "ArrowDown";
	}

  private sealed class UpSE : VerticalDirection
  {
    public UpSE() : base($"{nameof(Id.Up)}", Id.Up) { }
    public override string Title => "go up to verse";
    public override string Icon => "fa-solid fa-arrow-up";
    public override string ChapterBoundryMessage => "Topped out";
		public override string KeyboardKey => "ArrowUp";
	}
  #endregion
}
