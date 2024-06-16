using System.Numerics;
using Raylib_cs;

public class Entity : IUpdatable, IRenderable, IPositionable
{
    protected int _updatePriority, _renderLayer;
    protected bool _visible = true;
    protected Vector2 _position;

    public int updatePriority => _updatePriority;

    public int renderLayer => _renderLayer;

    public bool visible { get => _visible; set => _visible = value; }
    public Vector2 position { get => _position; set => _position = value; }

    protected uint _health = 5;
    protected Color _color = Color.DarkBlue;
    protected float _rotation = 0f;


    public Entity(Vector2 position, uint health, Color color) {
        this._position = position;
        this._health = health;
        this._color = color;
    }

    public Entity(float x, float y, uint health, Color color) {
        this._position = new Vector2(x, y);
        this._health = health;
        this._color = color;
    }

    public Entity(Vector2 position)
    {
        this._position = position;
    }

    public virtual void Start() {}

    public virtual void Render()
    {
        Raylib.DrawPoly(position, (int) _health, Engine.TileRadius, _rotation * 180f / (float) Math.PI, _color);
        Raylib.DrawPoly(_position + new Vector2((float) Math.Cos((double) _rotation), (float) Math.Sin((double) _rotation)) * Engine.TileRadius, (int) _health, Engine.TileRadius / 4, _rotation * 180f / (float) Math.PI, _color);
    }

    public virtual void Update() {}

}