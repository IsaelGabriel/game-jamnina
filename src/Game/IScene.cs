public interface IScene {
    public List<IUpdatable> GetUpdatables();
    public List<IRenderable> GetRenderables();
}