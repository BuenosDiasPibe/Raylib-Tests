using System.Text;
using static Raylib_cs.Raylib;
namespace Raylib_cs
{
  internal static class Program
  {
    static readonly int WindowWidth = 800;
    static readonly int WindowHeight = 600;
    static string txtWrited = "hola.txt";
    static int lineSpacing = 0;
    static int fSize = 50;

    [System.MTAThread]
    public static void Main()
    {
      InitWindow(WindowWidth,WindowHeight, "Oh my gaahhhh");
      StringBuilder stringBuilded = new();
      Font f = LoadFontEx("MonogramExtendedItalic.ttf", fSize, null, 0);
      Texture2D texture = LoadTexture("grandma.png");
      SetTargetFPS(60);
      int enters = 0;
      using(var st = new StreamReader(txtWrited))
      {
        stringBuilded.Append(st.ReadToEnd());
      }
      int vertical = 0;
      SetTextLineSpacing(lineSpacing);

      while(!Raylib.WindowShouldClose())
      {
        BeginDrawing();
        ClearBackground(GetColor(0x181818FF));
        DrawTexture(texture, 100, 100, Color.White);

        int al = GetCharPressed();
        if(al > 0)
        {
          stringBuilded.Append((char)al);
        }
        if(IsKeyPressed(KeyboardKey.Backspace) && stringBuilded.Length > 0)
        {
          stringBuilded.Remove(stringBuilded.Length-1, 1);
        }
        if(IsKeyPressed(KeyboardKey.Enter))
        {
          stringBuilded.Append("\n");
          enters++;
        }
        if((fSize+lineSpacing)*(enters-1)+vertical >= WindowHeight-fSize)
        {
          vertical++;
          enters--;
        }
        DrawText(stringBuilded.ToString(), 0, -(fSize*vertical), fontSize: fSize, Color.White);
        string aa = stringBuilded.ToString().Split('\n').Last();
        int p = MeasureText(aa, fSize);
        if(p>WindowWidth)
        {
          for(int i = stringBuilded.Length - 1; i >= 0; i--)
          {
            if(stringBuilded[i].Equals(' '))
            {
              stringBuilded[i] = '\n';
              enters++;
              break;
            }
          }
        }
        DrawRectangle(new(p, y: enters*(fSize+lineSpacing), 20, fSize), Color.White);
        string ta = $"v: {vertical}\ne: {enters}";
        DrawText(ta, WindowWidth-MeasureText(ta, fSize), WindowHeight-100, 50, GetColor(0x404040FF));

        Raylib.EndDrawing();
      }
      if(!File.Exists(txtWrited))
      {
        File.Create(txtWrited);
      }
      using(StreamWriter stream = new StreamWriter(txtWrited, false))
      {
        stream.WriteLine(stringBuilded.ToString());
      }
      CloseWindow();
    }
    public static void DrawRectangle(Rectangle r, Color color)
    {
      Raylib.DrawRectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height, color);
    }
  }
}

