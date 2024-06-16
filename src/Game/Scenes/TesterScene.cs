using System.Numerics;
using Raylib_cs;

public class TesterScene : IScene
{
    private const int TargetViewHeight = 600;
    private Player _player = new Player(Vector2.Zero);
    private List<IRenderable> _renderables = new List<IRenderable>();
    private List<IUpdatable> _updatables = new List<IUpdatable>();

    private List<Entity> _entities;

    public TesterScene() {
        _renderables.Add(_player);
        _updatables.Add(_player);
        _entities = (List<Entity>) [
            new Entity(10, 80, 5, Color.SkyBlue),
            new Entity(-50, 100, 4, Color.Beige),
            new Entity(150, -100, 6, Color.DarkPurple)
        ];
    }

    public void Start() {
        Engine.SetCameraZoom(Engine.DefaultWindowHeight / TargetViewHeight);
        Engine.SetCameraTarget(-Vector2.One * TargetViewHeight / 2);
        foreach(Entity entity in _entities) {
            entity.Start();
        }
    }


    public List<IRenderable> GetRenderables()
    {
        List<IRenderable> renderables = _renderables;
        renderables.AddRange(_entities); 
        return renderables.Distinct().ToList();
    }

    public List<IUpdatable> GetUpdatables()
    {
        List<IUpdatable> updatables = _updatables;
        updatables.AddRange(_entities);
        return updatables.Distinct().ToList();
    }
}
