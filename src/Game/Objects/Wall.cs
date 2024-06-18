using System.Numerics;
using Raylib_cs;

class Wall : IRenderable, IPositionable
{
    private readonly RectCollider _collider;
    private bool _visible = true;

    public int renderLayer => (int) RenderLayer.Object;

    public bool visible { get => _visible; set => _visible = value; }
    public Vector2 position { get => _collider.position; set => _collider.position = value; }

    public RectCollider collider => _collider;

    public Wall(Vector2 position) {
        _collider = new RectCollider(position, Vector2.One * Engine.TileRadius * 2, null, (RectCollider r) => {});
    }

    public void Render()
    {
        Raylib.DrawRectangleV(position, _collider.size, Color.LightGray);
    }
}