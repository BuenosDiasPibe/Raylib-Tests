using System.Numerics;
using Raylib_cs;
namespace Simple_2D_Game
{
  public class Ball
  {
    public Ball(Vector2 position, float radious, float velocity, Color color, Sound sound)
    {
      this.position = position;
      this.radious = radious;
      this.velocity = velocity;
      this.color = color;
      this.soundEffect = sound;
    }
    public Ball(){}
    public Vector2 position;
    public float radious;
    public float velocity;
    public Vector2 velocity_vector;
    public Color color;
    public bool start_sound = false;

    public Sound soundEffect;

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
