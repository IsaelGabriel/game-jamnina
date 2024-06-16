public interface IScene {
    public void Start();
    public List<IUpdatable> GetUpdatables();
    public List<IRenderable> GetRenderables();
}