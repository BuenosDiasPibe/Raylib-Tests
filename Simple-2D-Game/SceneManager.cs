namespace Simple_2D_Game
{
  public class SceneManager
  {
    private Stack<Scene> sceneManager = new();

    public void AddScene(Scene scene)
    {
      scene.LoadContent();
      sceneManager.Push(scene);
    }

    public Scene GetScene()
    { return sceneManager.Peek(); }

    public void RemoveScene()
    {
      if(sceneManager.Count == 1)
      {
        Console.WriteLine("helloo");
        throw new System.InvalidOperationException("you have only one scene");
      }
      sceneManager.Pop();
      sceneManager.Peek().LoadContent();
    }
  }
}
