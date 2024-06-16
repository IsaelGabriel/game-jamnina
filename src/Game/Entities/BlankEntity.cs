using Raylib_cs;
using System.Numerics;

public class BlankEntity : IPositionable, IRenderable
{

    private Vector2 _position;
    private int _sides;
    private float _radius;
    private Color _color;

    public Vector2 position {
        get => _position;
        set => _position = value;
    }

    public int renderLayer {
        get => (int) RenderLayer.Enemies;
    }

    public BlankEntity(int x, int y, int sides, int radius, Color color) {
        this._position = new Vector2(x, y);
        this._sides = sides;
        this._radius = radius;
        this._color = color;
    } 


    public void Render()
    {
        Raylib.DrawPoly(position + Vector2.One * _radius, _sides, _radius, 0f, _color);
    }

    public bool Visible()
    {
        return true;
    }
}