using System.Numerics;
using Raylib_cs;

class ShooterEntity : Entity
{
    private const float _projectileSpeed = 195f;
    private const float _projectileDuration = 10f;
    private float _interval = 5f;
    private float _intervalCount = 5f;

    public ShooterEntity(Vector2 position, uint health, float angle, float interval) : base(position)
    {
        _health = Math.Max(1, health);
        _color = Color.Red;
        _rotation = angle;
        _interval = Math.Max(0.25f, interval);
        _intervalCount = _interval;
    }

    public override void Update()
    {
        _intervalCount -= Raylib.GetFrameTime();
        if(_intervalCount <= 0f) {
            Vector2 projectilePosition = collider.center + new Vector2((float) Math.Cos(_rotation), (float) Math.Sin(_rotation)) * Engine.TileRadius * 2f;
            HealthEffectProjectile projectile = new HealthEffectProjectile(-1, projectilePosition, Vector2.One * 8, _projectileSpeed, this._rotation, _projectileDuration, EffectSource.Entity);
            Engine.CurrentScene?.AddObject(projectile);
            _intervalCount = _interval;
        }
    }
}