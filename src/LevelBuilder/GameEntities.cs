using System.Numerics;
using Raylib_cs;

public class GameBlock(int x, int y)
{
    public int x = x, y = y;
    protected int _renderX => this.x * LevelBuilder.TileSize + LevelBuilder.Margin;
    protected int _renderY => this.y * LevelBuilder.TileSize + LevelBuilder.Margin;

    public override string ToString(){ return "\n"; }
    public virtual void Render() {
        Raylib.DrawRectangle(_renderX, _renderY, LevelBuilder.TileSize, LevelBuilder.TileSize, Color.LightGray);
    }

}

public class Wall(int x, int y) : GameBlock(x, y) {
    public override string ToString(){ return $"wall {x},{y}"; }
    public override void Render() {
        Raylib.DrawRectangle(_renderX, _renderY, LevelBuilder.TileSize, LevelBuilder.TileSize, Color.LightGray);
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
        Vector2 renderCenter = new Vector2(_renderX, _renderY) + Vector2.One * LevelBuilder.TileSize/2;
        float renderAngle = rotation * 180f/ (float)Math.PI;
        Raylib.DrawPoly(renderCenter, health, (float) LevelBuilder.TileSize/2, renderAngle, _color);
        Raylib.DrawPoly(renderCenter + new Vector2((float) Math.Cos((double) rotation), (float) Math.Sin((double) rotation)) * LevelBuilder.TileSize/2, health, (float)LevelBuilder.TileSize/5, renderAngle, _color);
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
    private const int _modesLength = 8;
    private int _mode = _modesLength;
    private float _interval = 2f;
    public override int mode { get=>(int) _mode; set{
        _mode = value % _modesLength;
        rotation = (float) _mode / (_modesLength / 2) * (float) Math.PI;
    }}
    public Shooter(int x, int y) : base(x, y)
    {
        _color = Color.Red;
    }
    public override string ToString(){ return $"shooter {x},{y},{health},{rotation},{_interval}"; }
}