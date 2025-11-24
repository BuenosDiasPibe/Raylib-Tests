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
    { sceneManager.Pop(); }
  }
}
