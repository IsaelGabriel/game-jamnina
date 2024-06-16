using System.Numerics;
using Raylib_cs;

public class TesterScene : IScene
{
    private const int TargetViewHeight = 400;
    private Player _player = new Player();
    private List<IRenderable> _renderables = new List<IRenderable>();
    private List<IUpdatable> _updatables = new List<IUpdatable>();

    public TesterScene() {
        _renderables.Add(_player);
        _updatables.Add(_player);
        _renderables.AddRange((List<IRenderable>) [
            new BlankEntity(400, 400, 5, 16, Color.SkyBlue),
            new BlankEntity(120, 290, 4, 16, Color.Lime),
            new BlankEntity(300, 300, 6, 16, Color.DarkPurple)
        ]);
    }

    public void Start() {
        Engine.SetCameraZoom(Engine.DefaultWindowHeight / TargetViewHeight);
        Engine.SetCameraTarget(Vector2.One * TargetViewHeight / 2);
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
