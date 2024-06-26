﻿using System.Numerics;
using Raylib_cs;

public static class Engine {
    public const int TileRadius = 16;
    
    public const int DefaultWindowWidth = 600;
    public const int DefaultWindowHeight = 600;
    private const string WindowTitle = "Game Jamnina";
    private const int DefaultTargetFPS = 60;
    private static bool _shouldDrawFPS = true;
    private static Camera2D _camera;
    
    private static Color _clearColor = Color.DarkGray;

    private static IScene? _currentScene = new TesterScene();

    public static Camera2D Camera{ get => _camera; }
    public static IScene? CurrentScene{ get => _currentScene; }

    static void Main() {
        Raylib.InitWindow(DefaultWindowWidth, DefaultWindowHeight, WindowTitle);
        _camera = new Camera2D(Vector2.Zero, Vector2.Zero, 0f, 1f);
        Raylib.SetTargetFPS(DefaultTargetFPS);

        _currentScene?.Start();

        while(!Raylib.WindowShouldClose()) {
            Update();
            Render();
        }

        Raylib.CloseWindow();
    }

    private static void Update() {
        _currentScene?.Update();
    }

    private static void Render() {
        Raylib.BeginDrawing();
            Raylib.ClearBackground(_clearColor);
            Raylib.BeginMode2D(_camera);

                
                _currentScene?.Render();


            Raylib.EndMode2D();
            if(_shouldDrawFPS) Raylib.DrawFPS(0, 0); // FPS has maximum render priority.
        Raylib.EndDrawing();
    }

    public static void SetCameraTarget(Vector2 target) {
        _camera.Target = target;
    }

    public static void SetCameraZoom(float zoom) {
        _camera.Zoom = Math.Max(1f, zoom);
    }
}