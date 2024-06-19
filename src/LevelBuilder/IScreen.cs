using Raylib_cs;

public interface IScreen {
    public int targetFPS { get; }
    public string title { get; }
    public Color clearColor { get; }

    public void Start();
    public void Update();
    public void End();
    public void Render();
}