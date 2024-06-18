using System.Numerics;
using Raylib_cs;

public class HealthEffectProjectile : Projectile
{
    private EffectSource _effectSource;
    private int _ammount;
    public HealthEffectProjectile(int ammount, Vector2 center, Vector2 size, float velocity, float angle, float duration, EffectSource effectSource) :
    base(center, size, velocity, angle, duration) {
        _effectSource = effectSource;
        _ammount = ammount;
    }

    protected override void HandleCollision(RectCollider collider)
    {
        if(collider.owner != null) {
            collider.owner.health += _ammount;
            if(_effectSource == EffectSource.Entity) {
                foreach(Entity entity in Link.GetLinkedEntities(collider.owner)) {
                    Vector2 diff = entity.collider.center - collider.center;
                    Vector2 direction = -Vector2.Normalize(-diff);
                    float newAngle = (float) Math.Atan2(direction.Y, direction.X);
                    HealthEffectProjectile newProjectile = new HealthEffectProjectile(this._ammount, collider.center + direction * Engine.TileRadius, this.size, this.velocity, newAngle, this._duration, EffectSource.Link);
                    Engine.CurrentScene?.AddObject(newProjectile);
                }
            }
        }

        Destroy();
    }

    public override void Render()
    {
        Color c = (_ammount < 0)? Color.Red : Color.Lime;
        Raylib.DrawRectangleV(position, size, c);
        //Raylib.DrawCircleV(this._collider.center, this.size.Length(), Color.Red);
    }
}