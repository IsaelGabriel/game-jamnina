public interface IScene {
    public List<Entity> entities { get; }
    public void Start();
    public List<IUpdatable> GetUpdatables();
    public List<IRenderable> GetRenderables();
    public void Update();
    public void Render();
}