using System.Numerics;
using Raylib_cs;

public class GameBlock(int x, int y)
{
    public int x = x, y = y;
    protected int _renderX => this.x * LevelScreen.TileSize + (int) LevelScreen.Margin.X;
    protected int _renderY => this.y * LevelScreen.TileSize + (int) LevelScreen.Margin.Y;

    public override string ToString(){ return "\n"; }
    public virtual void Render() {
        Raylib.DrawRectangle(_renderX, _renderY, LevelScreen.TileSize, LevelScreen.TileSize, Color.LightGray);
    }

}

public class Wall(int x, int y) : GameBlock(x, y) {
    public override string ToString(){ return $"wall {x},{y}"; }
    public override void Render() {
        Raylib.DrawRectangle(_renderX, _renderY, LevelScreen.TileSize, LevelScreen.TileSize, Color.LightGray);
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
        Vector2 renderCenter = new Vector2(_renderX, _renderY) + Vector2.One * LevelScreen.TileSize/2;
        float renderAngle = rotation * 180f/ (float)Math.PI;
        Raylib.DrawPoly(renderCenter, health, (float) LevelScreen.TileSize/2, renderAngle, _color);
        Raylib.DrawPoly(renderCenter + new Vector2((float) Math.Cos((double) rotation), (float) Math.Sin((double) rotation)) * LevelScreen.TileSize/2, health + 2, (float)LevelScreen.TileSize/8, renderAngle, _color);
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
    private int _mode = 0;
    private float _interval = 2f;
    public float interval {
        get=>_interval;
        set{
            _interval = Math.Clamp(value, 0.25f, 15f);
        }
    }
    public override int mode { get=>(int) _mode; set{
        _mode = value % _modesLength;
        rotation = (float) _mode / (_modesLength / 2) * (float) Math.PI;
    }}
    public Shooter(int x, int y) : base(x, y)
    {
        _color = Color.Red;
    }
    public override string ToString(){ return $"shooter {x},{y},{health},{rotation},{interval}"; }
}