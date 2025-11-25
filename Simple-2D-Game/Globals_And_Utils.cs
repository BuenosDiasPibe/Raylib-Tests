using Raylib_cs;
using System.Numerics;
namespace Simple_2D_Game
{
  public static class Const
  {
    public static readonly int WindowWidth  = 1000;
    public static readonly int WindowHeight = 600;
    public static readonly int FPS = 60;
  }
  public static class Globals
  {
    public static int enemy_points = 0;
    public static int player_points = 0;
  }
  public static class Utils
  {
    public static bool intersectPoint(Rectangle r, Vector2 p)
    {
      return r.X < p.X && r.X + r.Width > p.X &&
            r.Y < p.Y && r.Y + r.Height > p.Y;
    }
  }
}
