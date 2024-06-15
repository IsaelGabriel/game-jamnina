using System.Data;
using Raylib_cs;

public static class Engine {
    private const int DefaultWindowWidth = 600;
    private const int DefaultWindowHeight = 600;
    private const string WindowTitle = "Game Jamnina";
    private const int DefaultTargetFPS = 30;
    private static bool _shouldDrawFPS = true;
    
    private static Color _clearColor = Color.DarkGray;

    private static IScene _currentScene;

    static void Main() {
        Raylib.InitWindow(DefaultWindowWidth, DefaultWindowHeight, WindowTitle);

        Raylib.SetTargetFPS(DefaultTargetFPS);

        while(!Raylib.WindowShouldClose()) {
            Update();
            Render();
        }

        Raylib.CloseWindow();
    }

    private static void Update() {
        List<IUpdatable> updatables = _currentScene.GetUpdatables();
        
        updatables.Sort((a, b)=>a.CompareUpdatePriorityTo(b));
        foreach(IUpdatable updatable in updatables) {
            updatable.Update();
        }
    }

    private static void Render() {
        List<IRenderable> renderables = _currentScene.GetRenderables();

        renderables.Sort((a, b)=>a.CompareRenderLayerTo(b));

        Raylib.BeginDrawing();

            Raylib.ClearBackground(_clearColor);

            foreach(IRenderable renderable in renderables) {
                if(renderable.Visible()) renderable.Render();
            }

            if(_shouldDrawFPS) Raylib.DrawFPS(0, 0); // FPS has maximum render priority.

        Raylib.EndDrawing();
    }
}