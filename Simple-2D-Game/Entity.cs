using Raylib_cs;
using System.Numerics;
namespace Simple_2D_Game
{
  public class Entity
  {
    public Texture2D texture {get;}
    public Vector2 velocity = new();
    public Rectangle sourceRectangle = new();
    public Rectangle DestinationRectangle = new();
    public Vector2 origin = new();
    public float rotation = 0;
    public Color color;


    public List<Component> componentList = new();

    public Entity(){}

    public Entity(Texture2D tex, Rectangle DestinationRectangle, Color color)
    {
      this.texture = tex;
      this.DestinationRectangle = DestinationRectangle;
      this.sourceRectangle.Size = texture.Dimensions;
    }
    public Entity(Rectangle DestinationRectangle, Color color)
    {
      this.DestinationRectangle = DestinationRectangle;
      this.color = color;
    }

    public void AddComponent(Component component)
    {
      component.Owner = this;
      componentList.Add(component);
      component.Start();
    }
    public void Update()
    {
      foreach(var component in componentList.ToList())
      {
        component.Update();
      }
    }
    public void Draw()
    {
      if(texture.Dimensions != Vector2.Zero) // not easy way to tell if texture is null
      {
        Raylib.DrawTexturePro(texture, sourceRectangle, DestinationRectangle, origin, rotation, color);
      }
      else
      {
        Raylib.DrawRectangle(
          (int)DestinationRectangle.X,
          (int)DestinationRectangle.Y,
          (int)DestinationRectangle.Width,
          (int)DestinationRectangle.Height, 
          color
        );
      }
    }
  }
}
