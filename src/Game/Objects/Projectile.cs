using System.Numerics;
using Raylib_cs;

public class Projectile : IUpdatable, IRenderable
{
    protected readonly RectCollider _collider;
    private bool _visible = true;
    protected bool _toDestroy = false;
    protected float _countdown = 0f;
    protected float _duration = 0f;

    public int updatePriority => 5;

    public int renderLayer => ((int) RenderLayer.Object) + 1;

    public bool visible { get => _visible; set => _visible = value; }
    
    public float angle = 0f;
    public float velocity = 0f;
    public Vector2 position {get=>_collider.position; set=>_collider.position = value;}
    public Vector2 size { get=>_collider.size; set=>_collider.size = value; }

    public RectCollider collider => _collider;

    public Projectile(Vector2 center, Vector2 size, float velocity, float angle, float duration) {
        _collider = new RectCollider(center - size/2, size, null);
        _collider.solid = false;
        this.velocity = velocity;
        this.angle = angle;
        this._duration = duration;
        this._countdown = duration;
        Engine.CurrentScene?.AddObject(this);
    }

    public virtual void Render()
    {
        Raylib.DrawRectangleV(_collider.position, size, Color.Gray);
    }

    public virtual void Update()
    {
        if(_toDestroy) return;
        Vector2 movement = new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle)) * velocity * Raylib.GetFrameTime();
        if(_duration > 0f) {
            _countdown -= Raylib.GetFrameTime();
            if(_countdown <= 0f) {
                Destroy();
                return;
            }
        }
        RectCollider? collision = _collider.MoveWithCollision(movement);
        if(collision != null) HandleCollision(collision);
    }

    protected virtual void HandleCollision(RectCollider collider) {}

    public void Destroy() {
        _toDestroy = true;
        Engine.CurrentScene?.RemoveObject(this);
    }
}