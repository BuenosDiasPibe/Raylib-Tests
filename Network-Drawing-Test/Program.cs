using Raylib_cs;

namespace Network_Drawing_Test;
public static class Graphics_Globals
{
  public static readonly int WindowWidth = 1000;
  public static readonly int WindowHeight = 600;
}
public class Program
{
  [System.STAThread]
  public static void Main(string[] args)
  {
    Raylib.InitWindow(Graphics_Globals.WindowWidth, Graphics_Globals.WindowHeight, "Drawing");

    while(!Raylib.WindowShouldClose())
    {
      Raylib.BeginDrawing();
      Raylib.ClearBackground(Raylib.GetColor(0x181818FF));



      Raylib.EndDrawing();
    }
    Raylib.CloseWindow();
  }
}
