using Raylib_cs;

public static class LevelBuilder {
    private const int _margin = 32;
    private const int _tileSize = 32;
    private const int _rows = 16;
    private const int _targetFPS = 20;
    private static Color _clearColor = Color.DarkBlue;
    public static int Rows => _rows;
    public static int TileSize => _tileSize;
    public static int Margin => _margin;
    private static List<GameBlock> _gameBlocks = [];

    static void Main() {
        int windowSize = _tileSize * _rows;
        Raylib.InitWindow(windowSize+_margin*2, windowSize+_margin*2, "Level builder");
        Raylib.SetTargetFPS(_targetFPS);
        
        SetBlockPosition(new Player(0, 0), 0, 0);
        SetBlockPosition(new Wall(1, 2), 3, 4);
        
        while(!Raylib.WindowShouldClose()) {
            HandleInput();
            Render();
        }
        Raylib.CloseWindow();
    }

    static void HandleInput() {
        int k = Raylib.GetKeyPressed();

        GameBlock? gb = null; 
        while(k != 0) {
            switch(k - (int) KeyboardKey.Zero) {
                case 0:
                    gb = new Player(0, 0);
                break;
                case 1:
                    gb = new Wall(0, 0);
                break;
                case 2:
                    gb = new Shooter(0, 0);
                    ((Shooter)gb).rotation = (float)Math.PI / 2.0f;
                break;
                default: break;
            }

            k = Raylib.GetKeyPressed();
        }
        if(gb != null) SetBlockPosition(gb, 0, 0);
    }

    static void Render() {
        Raylib.BeginDrawing();   
            Raylib.ClearBackground(_clearColor);
            Raylib.DrawRectangle(_margin, _margin, _tileSize * _rows, _tileSize * _rows, Color.DarkGray);
            foreach(GameBlock block in _gameBlocks) {
                block.Render();
            }

        Raylib.EndDrawing();
    }

    static void SetBlockPosition(GameBlock block, int x, int y) {
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
        block.x = x;
        block.y = y;
        _gameBlocks.Add(block);
    }
    static GameBlock? GetBlock(int x, int y) {
        foreach(GameBlock gb in _gameBlocks) {
            if(gb.x == x && gb.y == y) {
                return gb;
            }
        }
        return null;
    }
}