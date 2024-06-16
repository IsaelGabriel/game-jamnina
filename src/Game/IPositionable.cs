using System.Numerics;

public interface IPositionable {
    public Vector2 position { get; set; }
    public Vector2 TranslatePosition(Vector2 vector) {
        this.position += vector;
        return position;
    }
}