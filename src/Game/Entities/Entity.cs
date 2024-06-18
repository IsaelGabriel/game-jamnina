using System.Numerics;
using Raylib_cs;

public class Entity : IUpdatable, IRenderable
{
    private readonly RectCollider _collider;
    private static bool _showColliders = false;
    protected int _updatePriority = 0, _renderLayer = (int) RenderLayer.Entity;
    protected bool _visible = true;

    public int updatePriority => _updatePriority;

    public int renderLayer => _renderLayer;

    public bool visible { get => _visible; set => _visible = value; }

    protected uint _health = 5;
    protected Color _color = Color.DarkBlue;
    protected float _rotation = 0f;

    public int health {
        get => (int) _health;
        set => _health = (uint) Math.Max(0, value);
    }
    public RectCollider collider => _collider;

    public Vector2 position {
        get => collider.position;
        set { collider.position = value; }
    }

    public Entity(Vector2 position, Vector2 size, uint health, Color color) {
        this._collider = new RectCollider(position, size, this);
        this._health = health;
        this._color = color;
    }

    public Entity(float x, float y, float width, float height, uint health, Color color) {
        this._collider = new RectCollider(new Vector2(x,y), new Vector2(width, height), this);
        this._health = health;
        this._color = color;
    }

    public Entity(Vector2 position)
    {
        this._collider = new RectCollider(position, Vector2.One * Engine.TileRadius * 2, this);
    }

    public virtual void Start() {}

    public virtual void Render()
    {
        Raylib.DrawPoly(collider.center, (int) _health, Engine.TileRadius, _rotation * 180f / (float) Math.PI, _color);
        Raylib.DrawPoly(collider.center + new Vector2((float) Math.Cos((double) _rotation), (float) Math.Sin((double) _rotation)) * Engine.TileRadius, (int) _health, Engine.TileRadius / 4, _rotation * 180f / (float) Math.PI, _color);
        if(_showColliders) Raylib.DrawRectangleLinesEx(new Rectangle(collider.topLeft, collider.size), 2, Color.Green);
    }

    public virtual void Update() {}
}