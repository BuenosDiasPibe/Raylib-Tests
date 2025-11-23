using static Raylib_cs.Raylib; // static so its nothing related to update
using Raylib_cs; // related to update
namespace Simple_2D_Game
{
  public class MovementComponent : Component
  {
    public float max_velocity = 10;

    public override void Update()
    {
      Owner.velocity = new();
      if(IsKeyDown(KeyboardKey.W)) Owner.velocity.Y = -max_velocity;
      if(IsKeyDown(KeyboardKey.S)) Owner.velocity.Y = max_velocity;
      // if(IsKeyDown(KeyboardKey.A)) Owner.velocity.X = -max_velocity;
      // if(IsKeyDown(KeyboardKey.D)) Owner.velocity.X = max_velocity;
    }
  }
}
