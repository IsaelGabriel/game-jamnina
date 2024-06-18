using System.Numerics;
using Raylib_cs;

public class LinkProjectile : Projectile
{
    private Entity _linkSource;

    public LinkProjectile(Vector2 center, Vector2 size, float velocity, float angle, float duration, Entity linkSource) :
    base(center, size, velocity, angle, duration) {
        _linkSource = linkSource;        
    }

    protected override void HandleCollision(RectCollider collider)
    {
        if(collider.owner == null || collider.owner == _linkSource) return;

        new Link([_linkSource, collider.owner]);

        Destroy();
    }

    public override void Render()
    {
        Raylib.DrawRectangleV(position, size, Color.Green);
    }
}