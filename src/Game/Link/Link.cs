using Raylib_cs;

public class Link : IUpdatable {
    private static List<Link> _links = new List<Link>();

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

    public Link(Entity[] entities) {
        if(entities.Length != 2) return;
        this._entities = entities;
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
}