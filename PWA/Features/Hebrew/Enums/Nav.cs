using Ardalis.SmartEnum;

namespace PWA.Features.Hebrew.Enums;

public abstract class Nav : SmartEnum<Nav>
{
  #region Id's
  private static class Id
  {
    internal const int Chart = 0; // Sub-menu Main Page
		internal const int Definitions = 1;
    internal const int Print = 2;
    internal const int Peshitta = 3;
    internal const int QereKetiv = 4;
    internal const int Unicode = 5;
    internal const int Zepc3v8 = 6;
  }
  #endregion

  #region Declared Public Instances
  public static readonly Nav Chart = new ChartSE(); // Sub-menu Main Page
	public static readonly Nav Definitions = new DefinitionsSE();
  public static readonly Nav Print = new PrintSE();
  public static readonly Nav Peshitta = new PeshittaSE();
  public static readonly Nav QereKetiv = new QereKetivSE();
  public static readonly Nav Unicode = new UnicodeSE();
  public static readonly Nav Zepc3v8 = new Zepc3v8SE();
  #endregion

  private Nav(string name, int value) : base(name, value)  // Constructor
  {
  }

  #region Extra Fields
  public abstract string Index { get; }
  public abstract string Title { get; }
  public abstract string Icon { get; }
	#endregion

	#region Private Instantiation

	private sealed class ChartSE : Nav
	{
		public ChartSE() : base($"{nameof(Id.Chart)}", Id.Chart) { }
		public override string Index => PWA.Enums.Nav.Hebrew.Index;
		public override string Title => PWA.Enums.Nav.Hebrew.Title;
		public override string Icon => PWA.Enums.Nav.Hebrew.Icon;
	}

	private sealed class DefinitionsSE : Nav
  {
    public DefinitionsSE() : base($"{nameof(Id.Definitions)}", Id.Definitions) { }
    public override string Index => "/Definitions";
    public override string Title => "Definitions";
    public override string Icon => "fab fa-wordpress";
  }

  private sealed class PrintSE : Nav
  {
    public PrintSE() : base($"{nameof(Id.Print)}", Id.Print) { }
    public override string Index => "/Print";
    public override string Title => "Print";
    public override string Icon => "fa fa-print";
  }

  private sealed class PeshittaSE : Nav
  {
    public PeshittaSE() : base($"{nameof(Id.Peshitta)}", Id.Peshitta) { }
    public override string Index => "/Peshitta";
    public override string Title => "Peshitta";
    public override string Icon => "fas fa-ruble-sign";
  }

  private sealed class QereKetivSE : Nav
  {
    public QereKetivSE() : base($"{nameof(Id.QereKetiv)}", Id.QereKetiv) { }
    public override string Index => "/QereKetiv";
    public override string Title => "QereKetiv";
    public override string Icon => "fas fa-list";
  }

  private sealed class UnicodeSE : Nav
  {
    public UnicodeSE() : base($"{nameof(Id.Unicode)}", Id.Unicode) { }
    public override string Index => "/Unicode";
    public override string Title => "Unicode";
    public override string Icon => "fa fa-underline";
  }

  private sealed class Zepc3v8SE : Nav
  {
    public Zepc3v8SE() : base($"{nameof(Id.Zepc3v8)}", Id.Zepc3v8) { }
    public override string Index => "/Zepc3v8";
    public override string Title => "Zep 3:8";
    public override string Icon => "fa fa-plane";
  }

  #endregion
}