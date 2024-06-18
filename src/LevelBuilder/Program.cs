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
    private static int i = 0, j = 0;
    private static float _selectionThickness = 2f;

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

        i += (int) (Raylib.IsKeyPressed(KeyboardKey.Down) - Raylib.IsKeyPressed(KeyboardKey.Up));
        j += (int) (Raylib.IsKeyPressed(KeyboardKey.Right) - Raylib.IsKeyPressed(KeyboardKey.Left));

        i = Math.Clamp(i, 0, Rows - 1);
        j = Math.Clamp(j, 0, Rows - 1);

        GameBlock? currentBlock = GetBlock(j, i);
        if(currentBlock != null) {
            if(currentBlock.GetType().IsSubclassOf(typeof(GameEntity))) {
                ((GameEntity) currentBlock).mode += (int) (Raylib.IsKeyPressed(KeyboardKey.Kp6) - Raylib.IsKeyPressed(KeyboardKey.Kp4));
                ((GameEntity) currentBlock).health += (int) (Raylib.IsKeyPressed(KeyboardKey.Equal) - Raylib.IsKeyPressed(KeyboardKey.Minus));
            }
        }

        int k = Raylib.GetKeyPressed();
        GameBlock? gb = null; 
        bool setBlock = false;
        while(k != 0) {
            switch(k - (int) KeyboardKey.Zero) {
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

    static void Render() {
        Raylib.BeginDrawing();   
            Raylib.ClearBackground(_clearColor);
            
            for(int x = 0; x < Rows; x++) {
                for(int y = 0; y < Rows; y++) {
                    Raylib.DrawRectangle(x * TileSize + 2 + _margin, y * TileSize + 2 + _margin, TileSize - 4, TileSize - 4, Color.DarkGray);
                }
            }
            //Raylib.DrawRectangle(_margin, _margin, _tileSize * _rows, _tileSize * _rows, Color.DarkGray);
            foreach(GameBlock block in _gameBlocks) {
                block.Render();
            }
            Rectangle selectionRect = new Rectangle(j * TileSize + _margin, i * TileSize + _margin, TileSize, TileSize);
            Raylib.DrawRectangleLinesEx(selectionRect, _selectionThickness, Color.Green);

        Raylib.EndDrawing();
    }

    static void SetBlockPosition(GameBlock? block, int x, int y) {
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
    static GameBlock? GetBlock(int x, int y) {
        foreach(GameBlock gb in _gameBlocks) {
            if(gb.x == x && gb.y == y) {
                return gb;
            }
        }
        return null;
    }
}