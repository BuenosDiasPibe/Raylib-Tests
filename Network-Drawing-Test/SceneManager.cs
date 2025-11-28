namespace Network_Drawing_Test;
public class SceneManager
{
  private Stack<IScene> manager = new();
  public SceneManager() {}
  public void AddScene(IScene scene)
  {
    scene.LoadContent();
    manager.Push(scene);
  }
  public void RemoveScene()
  {
    manager.Pop().UnloadContent();
  }
  public IScene GetScene()
  {
    return manager.Peek();
  }
}
