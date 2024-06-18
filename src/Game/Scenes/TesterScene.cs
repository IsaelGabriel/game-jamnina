using System.Numerics;
using System.Text.RegularExpressions;
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
            new Entity(new Vector2(10, 80), entitySize, 5, Color.SkyBlue),
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
            Link.Links.Add(new Link([_player, entity]));
        }
    }

    public List<IRenderable> GetRenderables()
    {
        List<IRenderable> renderables = _renderables;
        return renderables.Distinct().ToList();
    }

    public List<IUpdatable> GetUpdatables()
    {
        List<IUpdatable> updatables = _updatables;
        return updatables.Distinct().ToList();
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
        foreach(Link link in Link.Links) {
            Raylib.DrawLineEx(link.entities[0].collider.center, link.entities[1].collider.center, 4f, Color.Green);
            Raylib.DrawCircleLinesV((link.entities[0].collider.center + link.entities[1].collider.center) / 2, 8, Color.Green);
        }
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
}
