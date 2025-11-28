using Raylib_cs;

namespace Network_Drawing_Test;
public class Program
{
  public static SceneManager manager = new();
  [System.STAThread]
  public static void Main(string[] args)
  {
    manager.AddScene(new TestScene());
    Raylib.InitWindow(Graphics_Globals.WindowWidth, Graphics_Globals.WindowHeight, "Drawing");
    Raylib.InitAudioDevice();
    manager.GetScene().LoadContent();

    while(!Raylib.WindowShouldClose())
    {
      manager.GetScene().Update();

      Raylib.BeginDrawing();
      Raylib.ClearBackground(Raylib.GetColor(Colors.Background));

      manager.GetScene().Draw();
      manager.GetScene().DrawUI();

      Raylib.EndDrawing();
    }
    manager.GetScene().UnloadContent();
    Raylib.CloseWindow();
    Raylib.CloseAudioDevice();
  }
}
