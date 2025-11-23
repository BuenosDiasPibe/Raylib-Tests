namespace Simple_2D_Game
{
  public abstract class Component
  {
    public Entity Owner {get; internal set;} = new(); //fuck you lsp

    public virtual void Start()
    {
      if(Owner == null) throw new Exception("oh noo"); 
    }
    public abstract void Update();
  }
}
