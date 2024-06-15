
public class TesterScene : IScene
{
    private Player _player = new Player();
    private List<IRenderable> _renderables = new List<IRenderable>();
    private List<IUpdatable> _updatables = new List<IUpdatable>();

    public TesterScene() {
        _renderables.Add(_player);
        _updatables.Add(_player);
    }


    public List<IRenderable> GetRenderables()
    {
        return _renderables;
    }

    public List<IUpdatable> GetUpdatables()
    {
        return _updatables;
    }
}
