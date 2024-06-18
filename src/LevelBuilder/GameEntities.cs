using System.Numerics;
using Raylib_cs;

public class GameBlock(int x, int y)
{
    public int x = x, y = y;

    public override string ToString(){ return "\n"; }
    public virtual void Render() {
        Raylib.DrawRectangle(x, y, LevelBuilder.TileSize, LevelBuilder.TileSize, Color.LightGray);
    }

}

public class Wall(int x, int y) : GameBlock(x, y) {
    public override string ToString(){ return $"wall {x},{y}"; }
    public override void Render() {
        Raylib.DrawRectangle(x, y, LevelBuilder.TileSize, LevelBuilder.TileSize, Color.LightGray);
    }
}

public class GameEntity(int x, int y) : GameBlock(x, y) {
    private int _health = 5;
    protected Color _color = Color.Beige;
    public float rotation = 0f;
    public virtual int mode { get; set; }
    public int health { 
        get=>_health;
        set=>_health = Math.Max(1, value);
    }
    public override void Render() {
        Vector2 renderCenter = new Vector2(x, y) + Vector2.One * LevelBuilder.TileSize/2;
        Raylib.DrawPoly(renderCenter, health, (float) LevelBuilder.TileSize/2, 0f, Color.Lime);
        Raylib.DrawPoly(renderCenter + new Vector2((float) Math.Cos((double) rotation), (float) Math.Sin((double) rotation)) * LevelBuilder.TileSize/2, health, (float)LevelBuilder.TileSize/5, rotation * 180/ (float)Math.PI, _color);
    }
}

public class Player : GameEntity {
    
    public override int mode { get=>0; }

    public Player(int x, int y) : base(x, y)
    {
        _color = Color.Lime;
    }

    public override string ToString(){ return $"player {x},{y},{health}"; }
}

public class Shooter : GameEntity {
    private float _interval = 2f;
    public override int mode { get=>(int) rotation; set{
        rotation = Math.Abs(value % 12);
    }}
    public Shooter(int x, int y) : base(x, y)
    {
        _color = Color.Red;
    }
    public override string ToString(){ return $"shooter {x},{y},{health},{rotation},{_interval}"; }
}