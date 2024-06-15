using Raylib_cs;

namespace Game;

public static class Engine {
    private const int DefaultWindowWidth = 600;
    private const int DefaultWindowHeight = 600;
    private const string WindowTitle = "Game Jamnina";
    private const int DefaultTargetFPS = 30;
    private static bool shouldDrawFPS = true;
    
    private static Color clearColor = Color.DarkGray;


    static void Main() {
        Raylib.InitWindow(DefaultWindowWidth, DefaultWindowHeight, WindowTitle);

        Raylib.SetTargetFPS(DefaultTargetFPS);

        while(!Raylib.WindowShouldClose()) {
            Render();
        }

        Raylib.CloseWindow();
    }

    private static void Render() {
        Raylib.BeginDrawing();

            Raylib.ClearBackground(clearColor);

            if(shouldDrawFPS) Raylib.DrawFPS(0, 0);

        Raylib.EndDrawing();
    }
}