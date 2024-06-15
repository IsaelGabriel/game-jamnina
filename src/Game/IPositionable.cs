using System.Numerics;

public interface IPositionable {
    public Vector2 GetPosition();
    public Vector2 SetPosition(Vector2 position); // Returns new position
    public Vector2 TranslatePosition(Vector2 vector) {
        return this.SetPosition(this.GetPosition() + vector);
    }
}