using Raylib_cs;
namespace Simple_2D_Game
{
  public class GameplayScene : Scene
  {
    float timePassed;
    TimeSpan time;

    Entity player = new();
    Entity enemy = new();
    Ball ball = new();
    GameOverScene gameOver;
    SceneManager sm;
    public GameplayScene(SceneManager sm)
    {
      this.sm = sm;
      gameOver = new(sm);
    }

    public void LoadContent()
    {
      timePassed = 0;
      player = new(
          new(20, Const.WindowHeight/2-50, 25, 100),
          Color.White
      );
      player.AddComponent(new MovementComponent());
      enemy = new(
          new(Const.WindowWidth-40, Const.WindowHeight/2-50, 25, 100),
          Raylib.GetColor(0xcc241dFF)
      );
      ball = new(
          new(Const.WindowWidth/2, Const.WindowHeight/2+90),
          radious: 20,
          velocity: 10,
          Raylib.GetColor(0x83a598),
          Raylib.LoadSound("colision.mp3")
      );
      ball.StartGame();
    }

    public void UnloadContent()
    { }

    public void Update(float deltaTime)
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
        ball.position.X = player.DestinationRectangle.X+player.DestinationRectangle.Width+ball.radious;
        Random rn = new();
        ball.velocity_vector.X *= (float)(-1 - rn.NextDouble()/8);
        ball.velocity_vector.Y *= (float)(-1 - rn.NextDouble()/8);
        ball.start_sound = true;
      }
      if(ball.intersects(ball, enemy.DestinationRectangle))
      {
        ball.position.X = enemy.DestinationRectangle.X-enemy.DestinationRectangle.Width-ball.radious;
        Random rn = new();
        ball.velocity_vector.X *= (float)(-1 - rn.NextDouble()/8);
        ball.velocity_vector.Y *= (float)(-1 - rn.NextDouble()/8);
        ball.start_sound = true;
      }

      ball.play_sound();
      timePassed+= deltaTime;
      time = TimeSpan.FromSeconds(timePassed);
      if(time.Seconds >= 30)
      {
        sm.AddScene(gameOver);
      }
    }
    public void Draw(float deltaTime)
    {
      player.Draw();
      enemy.Draw();
      ball.Draw();

      string texxt = $"p: {Globals.player_points} / e: {Globals.enemy_points}";
      int texxt_width = Raylib.MeasureText(texxt, 40);
      Raylib.DrawText(texxt, Const.WindowWidth/2 - texxt_width/2, 30, 40, Raylib.GetColor(0x504945FF));

      string timea = $"time: {time.Seconds}";
      int texxt_time = Raylib.MeasureText(timea, 40);
      Raylib.DrawText(timea, Const.WindowWidth/2 - texxt_time/2, 30+40, 30, Raylib.GetColor(0x504945FF));
    }
  }
}
