using Raylib_cs;

public static class LevelBuilder {
    private const int _margin = 32;
    private const int _tileSize = 32;
    private const int _rows = 16;
    private const int _targetFPS = 20;
    private static Color _clearColor = Color.DarkBlue;
    public static int Rows => _rows;
    public static int TileSize => _tileSize;
    private static List<GameBlock> _gameBlocks = new List<GameBlock>();

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
        
        foreach(GameBlock gb in _gameBlocks) {
            if(gb.x == x && gb.y == y) _gameBlocks.Remove(gb);
            break;
        }
        block.x = x * TileSize + _margin;
        block.y = y * TileSize + _margin;
        if(!_gameBlocks.Contains(block)) _gameBlocks.Add(block);
    }
}