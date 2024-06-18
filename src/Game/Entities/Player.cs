using Raylib_cs;
using System.Numerics;

public class Player : Entity
{
    private const RenderLayer DefaultRenderLayer = RenderLayer.Player;
    private const float BaseSpeed = 200f;
    private const uint BaseHealth = 5;

    public Player(Vector2 position) : base(position)
    {
        _updatePriority = 0;
        _renderLayer = (int) RenderLayer.Player;
        _health = BaseHealth;
        _color = Color.Lime;
    }

    public override void Update()
    {
        // Move
        Vector2 positionDifference = new Vector2(0, 0);
        
        positionDifference.X = Raylib.IsKeyDown(KeyboardKey.D) - Raylib.IsKeyDown(KeyboardKey.A);
        positionDifference.Y = Raylib.IsKeyDown(KeyboardKey.S) - Raylib.IsKeyDown(KeyboardKey.W);
        
        if(positionDifference.Length() != 0) {
            positionDifference = Vector2.Normalize(positionDifference);            
        }
        RectCollider? c = collider.MoveWithCollision(positionDifference * Raylib.GetFrameTime() * BaseSpeed);

        // Rotate
        Vector2 mousePosition = Raylib.GetMousePosition();
        Vector2 screenPosition = Raylib.GetWorldToScreen2D(position, Engine.Camera);
        Vector2 rotationVector = -Vector2.Normalize(screenPosition - mousePosition);

        _rotation = (float) Math.Atan2(rotationVector.Y, rotationVector.X);

        if(Raylib.IsKeyPressed(KeyboardKey.Space)) {
            Engine.CurrentScene?.GetRenderables().Add(new Player(position + Vector2.One * 64));
        }
    }
}
