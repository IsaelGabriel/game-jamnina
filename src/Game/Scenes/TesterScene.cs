using System.Numerics;
using Raylib_cs;

public class TesterScene : IScene
{
    private const int TargetViewHeight = 600;
    private Player _player = new Player(Vector2.Zero);
    private List<IRenderable> _renderables = new List<IRenderable>();
    private List<IUpdatable> _updatables = new List<IUpdatable>();

    public TesterScene() {
        Vector2 entitySize = Vector2.One * Engine.TileRadius * 2;
        AddObject(_player);
        AddObjectList([
            new ShooterEntity(new Vector2(10, -200), 5,(float) -Math.PI, 1.5f),
            new Entity(new Vector2(-50, 100), entitySize, 4, Color.Beige),
            new Entity(new Vector2(150, -100), entitySize, 6, Color.DarkPurple)
        ]);
    }

    public void Start() {
        Engine.SetCameraZoom(Raylib.GetScreenHeight() / TargetViewHeight);
        Engine.SetCameraTarget(-Vector2.One * TargetViewHeight / 2);
        _player.Start();
        foreach(Entity entity in _updatables) {
            if(entity == _player) continue;
            entity.Start();
        }
    }

    public List<IRenderable> GetRenderables()
    {
        return _renderables.Distinct().ToList();
    }

    public List<IUpdatable> GetUpdatables()
    {
        return _updatables.Distinct().ToList();
    }

    public void Update()
    {
        List<IUpdatable> updatables = GetUpdatables();
        updatables.Sort((a, b)=>a.CompareUpdatePriorityTo(b));

        foreach(IUpdatable updatable in updatables) {
            updatable.Update();
        }
    }

    public void Render()
    {
        List<IRenderable> renderables = GetRenderables();
        renderables.Sort((a, b)=>a.CompareRenderLayerTo(b));
        foreach(IRenderable renderable in renderables) {
            if(renderable.visible) renderable.Render();
        }
    
    }

    public void AddObjectList<T>(List<T> objs) {
        foreach(T obj in objs) AddObject<T>(obj);
    }

    public void AddObject<T>(T obj)
    {
        if(obj == null) return;
        if(typeof(T).GetInterfaces().Contains(typeof(IRenderable)))
            _renderables.Add((IRenderable) obj);
        if(typeof(T).GetInterfaces().Contains(typeof(IUpdatable)))
            _updatables.Add((IUpdatable) obj);
    }

    public void RemoveObject<T>(T obj)
    {
        if(obj == null) return;
        if(typeof(T).GetInterfaces().Contains(typeof(IRenderable)))
            while(_renderables.Contains((IRenderable) obj))
                _renderables.Remove((IRenderable) obj);
        if(typeof(T).GetInterfaces().Contains(typeof(IUpdatable)))
            while(_updatables.Contains((IUpdatable) obj))
                _updatables.Remove((IUpdatable) obj);
    }
}
