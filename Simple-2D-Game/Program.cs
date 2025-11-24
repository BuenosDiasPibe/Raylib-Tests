using Raylib_cs;
namespace Simple_2D_Game
{
  public class Program
  {
    [System.STAThread]
    public static void Main()
    {
      Raylib.InitWindow(Const.WindowWidth, Const.WindowHeight, "Simple Game"); // this is the "LoadContent" part
      Raylib.InitAudioDevice();
      SceneManager sceneManager = new();
      sceneManager.AddScene(new MenuScene(sceneManager));
      //sceneManager.AddScene(new GameplayScene());

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
