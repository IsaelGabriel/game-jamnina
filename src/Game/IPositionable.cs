using System.Numerics;

// NOTE: Position system follows Raylib/Screen plan, (0,0) = TopLeft
public interface IPositionable {
    public Vector2 position { get; set; }
    public Vector2 TranslatePosition(Vector2 vector) {
        this.position += vector;
        return position;
    }
}