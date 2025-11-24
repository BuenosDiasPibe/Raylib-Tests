using System.Numerics;
using Raylib_cs;
namespace Simple_2D_Game
{
  public class MenuScene(SceneManager sm) : Scene
  {
    SceneManager sm = sm;

    static readonly int padding = 10;

    string Title = "Pong Ping!";
    Rectangle titleRec = new();
    Vector2 titlePosition = new();
    readonly int TitleSize = 70;
    static Color titleColor;

    string subTitle = "where your dreams come :True:";
    Rectangle subTitleRec= new();
    Vector2 subTitlePosition = new();
    readonly int subTitleSize = 40;
    static Color subTitleColor;

    string startButtonString = "START";
    Rectangle startButtonRec= new();
    Vector2 startButtonPosition = new();
    readonly int buttonSize = 20;
    static Color buttonColor;
    static Color buttonTextColor;

    public void LoadContent()
    {
      titleRec.Width = Raylib.MeasureText(Title, TitleSize) + padding;
      titleRec.Height = TitleSize + padding/2;
      titlePosition = new(
        Const.WindowWidth/2 - titleRec.Width/2,
        Const.WindowHeight/2 - titleRec.Height/2 - buttonSize
      );
      titleRec.Position = new(
          titlePosition.X - padding/2,
          titlePosition.Y -padding/2
      );
      titleColor = Raylib.GetColor(0xd79921FF);

      subTitleRec.Width = Raylib.MeasureText(subTitle, subTitleSize) + padding/2;
      subTitleRec.Height = subTitleSize + padding/2;
      subTitlePosition = new(
        Const.WindowWidth/2 - subTitleRec.Width/2,
        titlePosition.Y + subTitleRec.Height + titleRec.Height/2
      );
      subTitleRec.Position = new(
          subTitlePosition.X - padding/2,
          subTitlePosition.Y -padding/2
      );
      subTitleColor = Raylib.GetColor(0xfabd2fFF);

      startButtonRec.Width = Raylib.MeasureText(startButtonString, buttonSize) + padding*2;
      startButtonRec.Height = buttonSize + padding*2;
      startButtonPosition = new(
          Const.WindowWidth/2 - startButtonRec.Width/2 - padding,
          Const.WindowHeight/2 + subTitleRec.Height + titleRec.Height - padding
      );
      startButtonRec.Position = new(
          startButtonPosition.X -padding,
          startButtonPosition.Y -padding
      );
      buttonColor = Raylib.GetColor(0x458588FF);
      buttonTextColor = Color.White;
    }

    bool stateClick = false;
    public void Update(float deltaTime)
    {
      buttonColor = Raylib.GetColor(0x458588FF);
      buttonTextColor = Color.White;
      Vector2 mPos = Raylib.GetMousePosition();
      if(intersectPoint(startButtonRec, mPos))
      {
        buttonColor = Raylib.GetColor(0xd3869bFF);
        buttonTextColor = Raylib.GetColor(0x282828FF);
        if(Raylib.IsMouseButtonUp(MouseButton.Left) && stateClick)
        {
          sm.AddScene(new GameplayScene(sm));
        }
      }
      stateClick = Raylib.IsMouseButtonDown(MouseButton.Left);
    }

    public void Draw(float deltaTime)
    {
      Raylib.DrawRectangleRec(titleRec, titleColor);
      Raylib.DrawText(Title, (int)titlePosition.X, (int)titlePosition.Y, TitleSize, Color.White);

      Raylib.DrawRectangleRec(subTitleRec, subTitleColor);
      Raylib.DrawText(subTitle, (int)subTitlePosition.X, (int)subTitlePosition.Y, subTitleSize, Color.White);

      Raylib.DrawRectangleRec(startButtonRec, buttonColor);
      Raylib.DrawText(startButtonString, (int)startButtonPosition.X, (int)startButtonPosition.Y, 
          buttonSize, buttonTextColor);
    }

    public void UnloadContent()
    { }

    private bool intersectPoint(Rectangle r, Vector2 p)
    {
      return r.X < p.X && r.X + r.Width > p.X &&
            r.Y < p.Y && r.Y + r.Height > p.Y;
    }

  }
}
