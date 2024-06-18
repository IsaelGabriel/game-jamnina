using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

public class Link : IUpdatable, IRenderable {
    private static List<Link> _links = new List<Link>();
    private static bool _linksVisible = true;

    public static List<Link> Links => _links;


    public int updatePriority => 1;

    private Entity?[] _entities = [null, null];

    public Entity[] entities {
        get {
            if(_entities.Contains(null)) return [];
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _entities;
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }

    public int renderLayer => (int) RenderLayer.Link;

    public bool visible { get => _linksVisible; set => _linksVisible = value; }

    public Link(Entity[] entities) {
        if(entities.Length != 2) return;
        this._entities = entities;
        Engine.CurrentScene?.AddObject(this);
    }

    public static void ResetLinks() {
        foreach(Link link in _links) {
            link.Destroy();
        }
        _links = new List<Link>();
    }

    public void Destroy() {
        _links.Remove(this);
    }

    public void Update()
    {
        if(_entities.Contains(null)) Destroy();
    }

    public static List<Entity> AffectEntities (List<Entity> entities, int healthChange) {
        Entity currentEntity = entities.Last();
        currentEntity.health += healthChange;
        
        foreach(Link link in _links) {
            if(link._entities.Contains(currentEntity)) {
                foreach(Entity entity in link._entities) {
                    if(!entities.Contains(entity)) {
                        entities.Add(entity);
                        entities = AffectEntities(entities, healthChange);
                    }
                }
            }
        }
        return entities;
    }

    public static List<Entity> GetLinkedEntities(Entity source) {
        List<Entity> entities = [];

        foreach(Link link in _links) {
            if(link.entities.Contains(source)) {
                foreach(Entity? entity in link.entities) {
                    if(entity == null || entity == source) continue;
                    entities.Add(entity);
                }
            }
        }

        return entities;
    }

    public void Render()
    {
        if(entities.Length == 0) return;
        Raylib.DrawLineEx(entities[0].collider.center, entities[1].collider.center, 4f, Color.Green);
        Raylib.DrawCircleLinesV((entities[0].collider.center + entities[1].collider.center) / 2, 8, Color.Green);
    }
}