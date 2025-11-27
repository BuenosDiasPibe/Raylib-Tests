/*
 * using this example: https://www.raylib.com/examples/shapes/loader.html?name=shapes_lines_drawing
 */
using Raylib_cs;
using System.Numerics;

namespace Network_Drawing_Test;
public static class Graphics_Globals
{
  public static readonly int WindowWidth = 1000;
  public static readonly int WindowHeight = 600;
}
// https://github.com/morhetz/gruvbox?tab=readme-ov-file
public static class Colors
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
}
public class Program
{
  [System.STAThread]
  public static void Main(string[] args)
  {
    Raylib.InitWindow(Graphics_Globals.WindowWidth, Graphics_Globals.WindowHeight, "Drawing");

    while(!Raylib.WindowShouldClose())
    {
      Vector2 mousePosition = Raylib.GetMousePosition();
      Raylib.BeginDrawing();
      Raylib.ClearBackground(Raylib.GetColor(Colors.Background));

      Raylib.EndDrawing();
    }
    Raylib.CloseWindow();
  }
}
