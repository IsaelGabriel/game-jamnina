using System.Numerics;

public interface ICollidable : IPositionable {
    private static List<ICollidable> _collidables = new List<ICollidable>();
    public static List<ICollidable> Collidables { 
        get=>_collidables;
        set{
            if(value == null) _collidables = [];
            else _collidables = value;
        }
    }
    public bool VectorHitCollider(Vector2 vector);
    public bool VectorInsideCollider(Vector2 vector);
    public Vector2 TrimVectorToColliderBorder(Vector2 from, Vector2 to);
    public Vector2 GetPossibleMovement(Vector2 desiredMovement) {
        float distance = desiredMovement.Length();
        while(MaximumClearRadius(position, distance) > 0.0001f) {

        }
        return Vector2.Zero;
    }
    public float MaximumClearRadius(Vector2 position, float distance) {
        float smallerDistance = distance;
        foreach(ICollidable collidable in _collidables) {
            if(collidable == this) continue;
            Vector2 lineBetween = collidable.TrimVectorToColliderBorder(position, collidable.position);
            float colliderDistance = lineBetween.Length();
            if(colliderDistance < smallerDistance) smallerDistance = colliderDistance;
        }
        return smallerDistance;
    }
}