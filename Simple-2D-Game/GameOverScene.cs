using System.Numerics;
using Raylib_cs;
namespace Simple_2D_Game
{
  public class GameOverScene : Scene
  {
    private SceneManager manager;
    //private GameplayScene reference;

    static int padding = 10;

    private string finalScore = "";
    Vector2 finalScorePosition = new();
    int finalScoreSize = 80;
    Color finalScoreColor;
    
    private string goBack = "Restart";
    Rectangle goBackRec = new();
    Vector2 goBackPosition = new();
    readonly int goBackSize = 30;
    static Color goBackColor;
    static Color goBackTextColor;

    public GameOverScene(SceneManager manager)
    {
      this.manager = manager;
    }

    public void LoadContent()
    {
      if( Globals.enemy_points == Globals.player_points)
      { finalScore = "TIE"; }
      else if(Globals.enemy_points > Globals.player_points)
      { finalScore = "ENEMY WINS"; }
      else
      { finalScore = "PLAYER WINS"; }
      finalScorePosition = new(
        Const.WindowWidth/2 - Raylib.MeasureText(finalScore, finalScoreSize)/2,
        Const.WindowHeight/2 - finalScoreSize/2 - goBackSize/2
      );
      finalScoreColor = Raylib.GetColor(0x998877FF);

      goBackPosition = new(
          Const.WindowWidth/2 - Raylib.MeasureText(goBack, goBackSize)/2,
          Const.WindowHeight/2 - goBackSize/2 + finalScoreSize/2
      );
      goBackRec = new(
          goBackPosition.X - padding/2,
          goBackPosition.Y - padding/2,
          Raylib.MeasureText(goBack, goBackSize) + padding,
          goBackSize + padding/2
      );
      goBackColor = Raylib.GetColor(0x776655FF);
      goBackTextColor = Color.White;
    }

    public void UnloadContent()
    { }

    bool stateClick = false;
    public void Update(float deltaTime)
    {
      goBackColor = Raylib.GetColor(0x458588FF);
      goBackTextColor = Color.White;
      Vector2 mPos = Raylib.GetMousePosition();
      if(Utils.intersectPoint(goBackRec, mPos))
      {
        goBackColor = Raylib.GetColor(0xd3869bFF);
        goBackTextColor = Raylib.GetColor(0x282828FF);
        if(Raylib.IsMouseButtonUp(MouseButton.Left) && stateClick)
        {
          manager.RemoveScene();
        }
      }
      stateClick = Raylib.IsMouseButtonDown(MouseButton.Left);

      if(Raylib.IsKeyPressed(KeyboardKey.Q))
      { manager.RemoveScene(); }
    }
    public void Draw(float deltaTime)
    {
      Raylib.DrawText(finalScore,
          (int)finalScorePosition.X,
          (int)finalScorePosition.Y,
          finalScoreSize,
          finalScoreColor
      );

      Raylib.DrawRectangleRec(goBackRec, goBackColor);
      Raylib.DrawText(goBack,
          (int)goBackPosition.X,
          (int)goBackPosition.Y,
          goBackSize,
          goBackTextColor
      );
    }
  }
}
