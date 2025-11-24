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

      Entity player = new(
          new(20, Const.WindowHeight/2-50, 25, 100),
          Color.White
      );
      player.AddComponent(new MovementComponent());

      Entity enemy = new(
          new(Const.WindowWidth-40, Const.WindowHeight/2-50, 25, 100),
          Raylib.GetColor(0xcc241dFF)
      );
      Ball ball = new(
          new(Const.WindowWidth/2, Const.WindowHeight/2+90),
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

        player.Update();
        enemy.Update();

        player.DestinationRectangle.Y = 
          Math.Max(0,
            Math.Min( player.DestinationRectangle.Y + player.velocity.Y,
              Const.WindowHeight - player.DestinationRectangle.Height
          )
        );

        if(ball.position.X > Const.WindowWidth/2)
        {
          if(enemy.DestinationRectangle.Y < ball.position.Y)
          {
            enemy.DestinationRectangle.Y += 5;
          }
          else if(enemy.DestinationRectangle.Y + enemy.DestinationRectangle.Height > ball.position.Y)
          {
            enemy.DestinationRectangle.Y -= 5;
          }
        }
        enemy.DestinationRectangle.Y = 
          Math.Max(0,
            Math.Min( enemy.DestinationRectangle.Y + enemy.velocity.Y,
              Const.WindowHeight - enemy.DestinationRectangle.Height
          )
        );


        if(ball.intersects(ball, player.DestinationRectangle))
        {
          if(ball.position.X < player.DestinationRectangle.X + player.DestinationRectangle.Width/12)
          {
            ball.position.X = player.DestinationRectangle.X+player.DestinationRectangle.Width+ball.radious;
          }
          Random rn = new();
          ball.velocity_vector.X *= (float)(-1 - rn.NextDouble()/8);
          ball.velocity_vector.Y *= (float)(-1 - rn.NextDouble()/8);
          ball.start_sound = true;
        }
        if(ball.intersects(ball, enemy.DestinationRectangle))
        {
          if(ball.position.X > enemy.DestinationRectangle.X + enemy.DestinationRectangle.Width/12)
          {
            ball.position.X = enemy.DestinationRectangle.X-enemy.DestinationRectangle.Width-ball.radious;
          }
          Random rn = new();
          ball.velocity_vector.X *= (float)(-1 - rn.NextDouble()/8);
          ball.velocity_vector.Y *= (float)(-1 - rn.NextDouble()/8);
          ball.start_sound = true;
        }

        ball.play_sound();

        Raylib.ClearBackground(Raylib.GetColor(0x1d2021FF)); // this is the "Draw" part
        Raylib.BeginDrawing();

        player.Draw();
        enemy.Draw();
        ball.Draw();
        string texxt = $"p: {Globals.player_points} / e: {Globals.enemy_points}";
        int texxt_width = Raylib.MeasureText(texxt, 40);
        Raylib.DrawText(texxt, Const.WindowWidth/2 - texxt_width/2, 30, 40, Raylib.GetColor(0x504945FF));

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
      if(position.X + radious >= Const.WindowWidth)
      {
        velocity_vector.X = -velocity;
        start_sound = true;
        Globals.player_points++;
      }
      if(position.X - radious <= 0)
      {
        velocity_vector.X = velocity;
        start_sound = true;
        Globals.enemy_points++;
      }
      if(position.Y + radious >= Const.WindowHeight){ velocity_vector.Y = -velocity;start_sound = true;}
      if(position.Y - radious <= 0){ velocity_vector.Y = velocity;start_sound = true;}

      position += velocity_vector;
    }

    public void Draw()
    {
      Raylib.DrawCircle((int)position.X, (int)position.Y, radious, color);
    }

    public bool intersects(Ball circle, Rectangle rect) // probably can be simplfied
    {
      // https://youtu.be/r0wAEi86vTA?si=nj4Tv0elST1iHeh9 (im not kidding)
      // https://www.jeffreythompson.org/collision-detection/circle-rect.php
      float tx = circle.position.X;
      float ty = circle.position.Y;

      if(circle.position.X < rect.X) {tx = rect.X;}
      else if(circle.position.X > rect.X + rect.Width) {tx = rect.X + rect.Width;}

      if(circle.position.Y < rect.Y) {ty = rect.Y;}
      else if(circle.position.Y > rect.Y + rect.Height) {ty = rect.Y + rect.Height;}

      float dX = circle.position.X - tx;
      float dY = circle.position.Y - ty;

      float distance = (float)Math.Sqrt((dX*dX)+(dY*dY));

      return distance < circle.radious;
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
