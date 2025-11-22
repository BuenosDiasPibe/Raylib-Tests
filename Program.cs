using System.Text;

namespace Raylib_cs
{
  internal static class Program
  {
    static readonly int WindowWidth = 800;
    static readonly int WindowHeight = 600;
    static readonly string text = "fuck you";
    static readonly int fontSize = 100;

    [System.MTAThread]
    public static void Main()
    {
      Raylib.InitWindow(WindowWidth,WindowHeight, "Oh my gaahhhh");
      var a = Raylib.MeasureText(text, fontSize);
      int fSize = 50;
      StringBuilder stringBuilded = new();
      Font f = Raylib.LoadFontEx("MonogramExtendedItalic.ttf", fSize, null, 0);
      Rectangle r = new(WindowWidth/2-a/2, WindowHeight/2-fontSize/2, a, fontSize);
      Raylib.SetTargetFPS(60);
      while(!Raylib.WindowShouldClose())
      {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        DrawRectangle(r, Color.Gray);
        int al = Raylib.GetCharPressed();
        if(al > 0)
        {
          stringBuilded.Append((char)al);
        }
        if(Raylib.IsKeyPressed(KeyboardKey.Backspace) && stringBuilded.Length > 0)
        {
          stringBuilded.Remove(stringBuilded.Length-1, 1);
        }
        if(Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
          stringBuilded.Append("\n");
        }

        Raylib.DrawText(text, WindowWidth/2-a/2, WindowHeight/2-fontSize/2, fontSize, Color.White);

        Raylib.DrawTextEx(f, stringBuilded.ToString(), new(0,0), fSize, 0, Color.White);

        Raylib.EndDrawing();
      }
      Raylib.CloseWindow();
    }
    public static void DrawRectangle(Rectangle r, Color color)
    {
      Raylib.DrawRectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height, color);
    }
  }
}

