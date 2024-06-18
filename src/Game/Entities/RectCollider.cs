using System.Numerics;

public class RectCollider : IPositionable
{
    private static List<RectCollider> _colliders = new List<RectCollider>();
    
    private Entity? _owner;
    private Vector2 _position = Vector2.Zero;
    private Vector2 _size = Vector2.One;

    public Entity? owner => _owner;
    public Vector2 position {
        get => _position;
        set => _position = value;
    }
    public Vector2 size {
        get=>_size;
        set {
            value.X = Math.Max(1f, Math.Abs(value.X));
            value.Y = Math.Max(1f, Math.Abs(value.Y));
            _size = value;
        }
    }

    #region Borders
        public Vector2 topLeft {
            get=>_position;
            set { _position = value; }
        }
        public Vector2 topRight {
            get => _position + new Vector2(_size.X, 0f);
            set {
                _position = value - new Vector2(_size.X, 0f);
            } 
        }
        public Vector2 center {
            get => _position + _size/2;
            set {
                _position = value - _size/2;
            } 
        }
        public Vector2 bottomLeft {
            get => _position + new Vector2(0f, _size.Y);
            set {
                _position = value - new Vector2(0f, _size.Y);
            } 
        }
        public Vector2 bottomRight {
            get => _position + _size;
            set {
                _position = value - _size;
            } 
        }
    #endregion


    public RectCollider(Vector2 position, Vector2 size, Entity? owner) {
        this.position = position;
        this.size = size;
        this._owner = owner;
        _colliders.Add(this);
    }

    public static RectCollider SquareCollider(Vector2 position, float side, Entity? owner) {
        return new RectCollider(position, Vector2.One * side, owner);
    }

    public static void ResetColliders() {
        _colliders = new List<RectCollider>();
    }

    public bool PointInsideCollider(Vector2 point) {
        point -= position;
        bool matchX = point.X >= 0f && point.X <= size.X;
        bool matchY = point.Y >= 0f && point.Y <= size.Y;
        return matchX && matchY;
    }

    public Vector2 TrimMovementWithCollider(RectCollider collider, Vector2 movement) {
        if(movement.Length() == 0f) return movement;

        Vector2 near = (collider.topLeft - this.size/2 - this.center) / movement;
        Vector2 far = (collider.bottomRight + this.size/2 - this.center) / movement;
        int[] contact = [0, 0];


        if(float.IsNaN(near.X)) near.X = 0f;
        if(float.IsNaN(near.Y)) near.Y = 0f;
        if(float.IsNaN(far.X)) far.X = 0f;
        if(float.IsNaN(far.Y)) far.Y = 0f;

        if(near.X > far.X) {
            float t = near.X;
            near.X = far.X;
            far.X = t;
        }

        if(near.Y > far.Y) {
            float t = near.Y;
            near.Y = far.Y;
            far.Y = t;
        }

        if(near.X > far.Y || near.Y > far.X) return movement;

        float nearest = Math.Max(near.X, near.Y);
        float furthest = Math.Max(far.X, far.Y);

        if(furthest < 0f) return movement;
        if(nearest < 0f || nearest > 1f) return movement;

        if(near.X > near.Y) {
            if(movement.X < 0f) contact = [1, 0];
            else contact = [-1, 0];
        }else if(near.X < near.Y) {
            if(movement.Y < 0f) contact = [0, 1];
            else contact = [0, -1];
        }

        Vector2 movementTillCollision = movement * (nearest - 0.000001f);
        Vector2 leftOverDirection = new Vector2(1 - int.Abs(contact[0]), 1 - int.Abs(contact[1]));
        float difference = float.Abs(near.X - near.Y);
        if(float.IsNaN(difference) || !float.IsFinite(difference)) difference = 0f;

        return movementTillCollision + leftOverDirection * (movement - movementTillCollision);
    }

    public RectCollider? MoveWithCollision(Vector2 movement) {
        RectCollider? collision = null;

        foreach(RectCollider collider in _colliders) {
            if(collider == this) continue;
            Vector2 trimmedMovement = TrimMovementWithCollider(collider, movement);
            if(!trimmedMovement.Equals(movement)) {
                collision = collider;
            }
            movement = trimmedMovement;
        }

        position += movement;

        return collision;
    }

    public void Destroy() {
        _colliders.Remove(this);
    }
}