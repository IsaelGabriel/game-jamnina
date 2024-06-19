using Raylib_cs;
using System.IO;
using System.Runtime.CompilerServices;

public static class LevelBuilder {
    public const int ScreenWidth = 600;
    public const int ScreenHeight = 600;
    private static List<GameBlock> _gameBlocks = [];
    private static float _selectionThickness = 2f;
    private static IScreen _screen = new LevelScreen("");
    public static IScreen CurrentScreen {
        get=> _screen;
        set {
            _screen.End();
            _screen = value;
            Raylib.SetWindowTitle(_screen.title);
            Raylib.SetTargetFPS(_screen.targetFPS);
            _screen.Start();
        }
    }

    static void Main() {
        /* Reads all levels from folder
        foreach(String s in Directory.GetFiles(@"../Levels", "*.gamelevel")) {
            Console.WriteLine(s.Replace('\\', '/'));
        }
        return;
        */
        Raylib.InitWindow(ScreenWidth, ScreenHeight, "Level builder");
        Raylib.SetTargetFPS(60);
        
        Raylib.SetTargetFPS(CurrentScreen.targetFPS);
        Raylib.SetWindowTitle(CurrentScreen.title);
        CurrentScreen.Start();

        while(!Raylib.WindowShouldClose()) {
            CurrentScreen.Update();
            Render();
        }
        Raylib.CloseWindow();
    }

    static void Render() {
        Raylib.BeginDrawing();   
            Raylib.ClearBackground(CurrentScreen.clearColor);
            CurrentScreen.Render();
        Raylib.EndDrawing();
    }
}