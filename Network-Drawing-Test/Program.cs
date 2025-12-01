using Raylib_cs;

namespace Network_Drawing_Test;
public class Program
{

  [System.STAThread]
  public static void Main(string[] args)
  {
    Raylib.InitWindow(Graphics_Globals.WindowWidth, Graphics_Globals.WindowHeight, "Drawing");
    Raylib.InitAudioDevice();

    SceneManager manager = new();
    manager.AddScene(new TestScene());

    while(!Raylib.WindowShouldClose())
    {
        manager.GetScene().Update();
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Raylib.GetColor(Colors.Background));

        manager.GetScene().Draw();

        Raylib.EndDrawing();
    }
    manager.GetScene().UnloadContent();
    Raylib.CloseWindow();
    Raylib.CloseAudioDevice();
  }
}
