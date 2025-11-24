using System.Numerics;
using Raylib_cs;
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
  public class Program
  {
    [System.STAThread]
    public static void Main()
    {
      Raylib.InitWindow(Const.WindowWidth, Const.WindowHeight, "Simple Game"); // this is the "LoadContent" part
      Raylib.InitAudioDevice();
      SceneManager sceneManager = new();
      sceneManager.AddScene(new GameplayScene());

      Raylib.SetTargetFPS(Const.FPS);
      while(!Raylib.WindowShouldClose()) // this is the "Update" part
      {
        sceneManager.GetScene().Update();

        Raylib.ClearBackground(Raylib.GetColor(0x1d2021FF)); // this is the "Draw" part
        Raylib.BeginDrawing();

        sceneManager.GetScene().Draw();

        Raylib.EndDrawing();
      }
      Raylib.CloseAudioDevice();
      Raylib.CloseWindow();
    }
  }
}
