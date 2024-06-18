public interface IScene {
    public void Start();
    public List<IUpdatable> GetUpdatables();
    public List<IRenderable> GetRenderables();
    public void AddObject<T>(T obj);
    public void AddObjectList<T>(List<T> obj);
    public void RemoveObject<T>(T obj);
    public void Update();
    public void Render();
}