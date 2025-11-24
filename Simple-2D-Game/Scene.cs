namespace Simple_2D_Game {
  public interface Scene {
    public void LoadContent();
    public void Update(float deltaTime);
    public void Draw(float deltaTime);
    public void UnloadContent();
  }
}
