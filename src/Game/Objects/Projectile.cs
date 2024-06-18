using System.Numerics;
using Raylib_cs;

class Projectile : IUpdatable, IRenderable
{
    private readonly RectCollider _collider;
    private bool _visible = true;
    private bool _hasCountdown = false;
    private float _countdown = 0f;

    public int updatePriority => 5;

    public int renderLayer => ((int) RenderLayer.Object) + 1;

    public bool visible { get => _visible; set => _visible = value; }
    
    public float angle = 0f;
    public float velocity = 0f;
    public Vector2 position {get=>_collider.position; set=>_collider.position = value;}
    public Vector2 size { get=>_collider.size; set=>_collider.size = value; }


    public Projectile(Vector2 center, Vector2 size, float velocity, float angle, float duration) {
        _collider = new RectCollider(center - size/2, size, null);
        _collider.solid = false;
        this.velocity = velocity;
        this.angle = angle;
        this._countdown = duration;
        if(duration > 0f) _hasCountdown = true;
    }

    public virtual void Render()
    {
        Raylib.DrawRectangleV(_collider.position, size, Color.Gray);
    }

    public virtual void Update()
    {
        Vector2 movement = new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle)) * velocity * Raylib.GetFrameTime();
        if(_hasCountdown) {
            _countdown -= Raylib.GetFrameTime();
            if(_countdown <= 0f) {
                Destroy();
                return;
            }
        }
        HandleCollision(_collider.MoveWithCollision(movement));
    }

    protected virtual void HandleCollision(RectCollider? collider) {
        if(collider == null) return;
        Destroy();
    }

    public virtual void Destroy() {
        Engine.CurrentScene?.RemoveObject(this);
    }
}