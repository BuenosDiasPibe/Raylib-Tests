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
  public class Program
  {
    [System.STAThread]
    public static void Main()
    {
      Raylib.InitWindow(Const.WindowWidth, Const.WindowHeight, "Simple Game"); // this is the "LoadContent" part
      Raylib.InitAudioDevice();

      Entity player = new(
          new(Const.WindowWidth/2-25, 250, 50, 100),
          Color.White
      );
      player.AddComponent(new MovementComponent());
      Ball ball = new(
          new(Const.WindowWidth/2, Const.WindowHeight/2+50),
          radious: 20,
          velocity: 10,
          Raylib.GetColor(0x83a598),
          Raylib.LoadSound("colision.mp3")
          );
      ball.StartGame();

      Raylib.SetTargetFPS(Const.FPS);
      while(!Raylib.WindowShouldClose()) // this is the "Update" part
      {
        ball.Update();
        //ball.color = Raylib.GetColor(0x83a598);

        player.Update();

        player.DestinationRectangle.Y = 
          Math.Max(0,
            Math.Min(player.DestinationRectangle.Y+player.velocity.Y,
              Const.WindowHeight-player.DestinationRectangle.Height
          )
        );

        if(ball.intersects(ball, player.DestinationRectangle))
        {
          ball.velocity_vector.X *= -1;
          ball.velocity_vector.Y *= -1;
          ball.start_sound = true;
          Console.WriteLine("collided");
        }

        ball.play_sound();
        Raylib.ClearBackground(Raylib.GetColor(0x1d2021FF)); // this is the "Draw" part
        Raylib.BeginDrawing();
        Raylib.DrawRectangle(
            (int)player.DestinationRectangle.X,
            (int)player.DestinationRectangle.Y,
            (int)player.DestinationRectangle.Width,
            (int)player.DestinationRectangle.Height,
            player.color
        );

        ball.Draw();

        Raylib.EndDrawing();
      }
      Raylib.CloseAudioDevice();
      Raylib.CloseWindow();
    }
  }
  public class Ball(Vector2 position, float radious, float velocity, Color color, Sound sound)
  {
    public Vector2 position = position;
    public float radious = radious;
    public float velocity = velocity;
    public Vector2 velocity_vector;
    public Color color = color;
    public bool start_sound = false;

    public Sound soundEffect = sound;

    public void StartGame()
    {
      velocity_vector = new(velocity, velocity);
    }

    public void Update()
    {
      start_sound = false;
      if(position.X + radious >= Const.WindowWidth){ velocity_vector.X = -velocity; start_sound = true;}
      if(position.X - radious <= 0){ velocity_vector.X = velocity;start_sound = true;}
      if(position.Y + radious >= Const.WindowHeight){ velocity_vector.Y = -velocity;start_sound = true;}
      if(position.Y - radious <= 0){ velocity_vector.Y = velocity;start_sound = true;}

      position += velocity_vector;
    }

    public void Draw()
    {
      Raylib.DrawCircle((int)position.X, (int)position.Y, radious, color);
    }

    public bool intersects(Ball circle, Rectangle rect)
    {
      return false;
    }
    public void play_sound()
    {
      if(start_sound)
      {
        Raylib.PlaySound(soundEffect);
      }
    }
  }
}
