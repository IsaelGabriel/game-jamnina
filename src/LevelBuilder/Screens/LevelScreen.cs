using System.Numerics;
using Raylib_cs;

public class LevelScreen : IScreen
{

    private const string SampleLevelPath = @"../Levels/sample-level.gamelevel";
    private string _levelPath = SampleLevelPath;
    private static Vector2 _margin = Vector2.Zero;
    private const int _tileSize = 32;
    private const int _rows = 16;
    public static int Rows => _rows;
    public static int TileSize => _tileSize;
    public static Vector2 Margin => _margin;
    private List<GameBlock> _gameBlocks = [];
    private static int i = 0, j = 0;
    private static float _selectionThickness = 2f;

    public int targetFPS => 30;

    public string title => $"Level \"{_levelPath}\"";
    public string levelPath {
        get=> _levelPath;
        set=> _levelPath = string.IsNullOrEmpty(value)? SampleLevelPath : value;
    }
    public Color clearColor => Color.DarkBlue;

    public LevelScreen(string path) {
        levelPath = path;
    }

    public void Start()
    {
        _margin = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        _margin -= Vector2.One * Rows * TileSize;
        _margin /= 2f;
        _margin.X = (float) Math.Floor(_margin.X);
        _margin.Y = (float) Math.Floor(_margin.Y);
        if(!ImportLevel(levelPath)){
            ImportLevel(SampleLevelPath);
            // TODO: Return to previous screen in case level doesn't exist. 
        }
    }

    public void Update() {

        i += (int) (Raylib.IsKeyPressed(KeyboardKey.S) - Raylib.IsKeyPressed(KeyboardKey.W));
        j += (int) (Raylib.IsKeyPressed(KeyboardKey.D) - Raylib.IsKeyPressed(KeyboardKey.A));

        i = Math.Clamp(i, 0, Rows - 1);
        j = Math.Clamp(j, 0, Rows - 1);

        GameBlock? currentBlock = GetBlock(j, i);
        if(currentBlock != null) {
            if(currentBlock.GetType().IsSubclassOf(typeof(GameEntity))) {
                ((GameEntity) currentBlock).mode += (int) (Raylib.IsKeyPressed(KeyboardKey.Right) - Raylib.IsKeyPressed(KeyboardKey.Left));
                ((GameEntity) currentBlock).health += (int) (Raylib.IsKeyPressed(KeyboardKey.Up) - Raylib.IsKeyPressed(KeyboardKey.Down));
            }
        }

        int k = Raylib.GetKeyPressed();
        GameBlock? gb = null; 
        bool setBlock = false;
        while(k != 0) {
            switch(k - (int) KeyboardKey.Kp0) {
                case 0:
                    gb = null;
                    setBlock = true;
                break;
                case 1:
                    gb = new Player(0, 0);
                    setBlock = true;
                break;
                case 2:
                    gb = new Wall(0, 0);
                    setBlock = true;
                break;
                case 3:
                    gb = new Shooter(0, 0);
                    ((Shooter)gb).rotation = (float)Math.PI / 2.0f;
                    setBlock = true;
                break;
                default: break;
            }
            k = Raylib.GetKeyPressed();
        }
        if(setBlock) SetBlockPosition(gb, j, i);
    }

    public void End()
    {
        throw new NotImplementedException();
    }


    public void Render() {  
        for(int x = 0; x < Rows; x++) {
            for(int y = 0; y < Rows; y++) {
                Vector2 position = new Vector2(x, y) * TileSize + Vector2.One * 2 + _margin;
                Raylib.DrawRectangleV(position, Vector2.One * (TileSize - 4), Color.DarkGray);
            }
        }
        //Raylib.DrawRectangle(_margin, _margin, _tileSize * _rows, _tileSize * _rows, Color.DarkGray);
        foreach(GameBlock block in _gameBlocks) {
            block.Render();
        }
        Rectangle selectionRect = new Rectangle(j * TileSize +(int) _margin.X, i * TileSize + (int)_margin.Y, TileSize, TileSize);
        Raylib.DrawRectangleLinesEx(selectionRect, _selectionThickness, Color.Green);

    }

    private void SetBlockPosition(GameBlock? block, int x, int y) {
        if(x < 0 || x > Rows || y < 0 || y > Rows) return;
        
        List<GameBlock> toRemove = [];

        foreach(GameBlock gb in _gameBlocks) {
            if(gb.x == x && gb.y == y) {
                toRemove.Add(gb);
            }
        }
        foreach(GameBlock gb in toRemove) {
            while(_gameBlocks.Contains(gb)) _gameBlocks.Remove(gb);
        }
        if(block != null) {
            block.x = x;
            block.y = y;
            _gameBlocks.Add(block);
        }
    }

    private GameBlock? GetBlock(int x, int y) {
        foreach(GameBlock gb in _gameBlocks) {
            if(gb.x == x && gb.y == y) {
                return gb;
            }
        }
        return null;
    }
    
    private bool ImportLevel(string path) {
        if (!File.Exists(path)) return false;

        foreach(string line in File.ReadLines(path)) {
            try {
                string[] items = line.Split(' ');
                string blockType = items[0];
                string[] args = items[1].Split(',');
                int x = int.Parse(args[0]);
                int y = int.Parse(args[1]);
                GameBlock? gb = null;
                switch(blockType) {
                    case "player":
                        gb = new Player(x, y) {
                            health = int.Parse(args[2])
                        };
                    break;
                    case "shooter":
                        gb = new Shooter(x, y) {
                            health = int.Parse(args[2]),
                            mode = int.Parse(args[3]),
                            interval = int.Parse(args[4])
                        };
                    break;
                    case "wall":
                        gb = new Wall(x, y);
                    break;
                    default: break;
                }
                if(gb != null) SetBlockPosition(gb, x, y);
            }catch(FormatException) {
                Console.WriteLine($"Invalid format \"{line}\" at \"{path}\"");
                return false;
            }
        }
        return true;
    }

}