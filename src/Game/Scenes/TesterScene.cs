using System.Numerics;
using Raylib_cs;

public class TesterScene : IScene
{
    private const int TargetViewHeight = 600;
    private Player _player = new Player(Vector2.Zero);
    private List<IRenderable> _renderables = new List<IRenderable>();
    private List<IUpdatable> _updatables = new List<IUpdatable>();

    private List<Entity> _entities;

    public List<Entity> entities => _entities;

    public TesterScene() {
        Vector2 entitySize = Vector2.One * Engine.TileRadius * 2;
        _renderables.Add(_player);
        _updatables.Add(_player);
        _entities = (List<Entity>) [
            new Entity(new Vector2(10, 80), entitySize, 5, Color.SkyBlue),
            new Entity(new Vector2(-50, 100), entitySize, 4, Color.Beige),
            new Entity(new Vector2(150, -100), entitySize, 6, Color.DarkPurple)
        ];
        _renderables.Add(new Wall(new Vector2(-100, -100)));
    }

    public void Start() {
        Engine.SetCameraZoom(Raylib.GetScreenHeight() / TargetViewHeight);
        Engine.SetCameraTarget(-Vector2.One * TargetViewHeight / 2);
        _player.Start();
        foreach(Entity entity in _entities) {
            entity.Start();
            Link.Links.Add(new Link([_player, entity]));
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
}
