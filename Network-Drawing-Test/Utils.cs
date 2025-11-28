using Raylib_cs;
namespace Network_Drawing_Test;

public static class Graphics_Globals
{
  public static readonly int WindowWidth = 1000;
  public static readonly int WindowHeight = 600;
}

public static class Colors // https://github.com/morhetz/gruvbox?tab=readme-ov-file
{
  public const uint Background = 0x282828FF;
  public const uint Red        = 0xcc241d;
  public const uint Green      = 0x98971FF;
  public const uint Yellow     = 0xd79921FF;
  public const uint Blue       = 0x458588FF;
  public const uint Purple     = 0xb16286FF;
  public const uint Aqua       = 0x689d6aFF;
  public const uint Gray       = 0xa89984FF;
  public const uint Foreground = 0xebdbb2FF;
  public static Color giveRandomColor()
  {
    List<uint> colors = new()
    {
      Red,
      Green,
      Yellow,
      Purple
    };
    Random r = new();
    return Raylib.GetColor(colors[r.Next(colors.Count)]);
  }
}

