public interface IRenderable {
    public int renderLayer { get; }
    public void Render();
    public bool Visible();

    public int CompareRenderLayerTo(IRenderable renderable) {
        int difference = (int) renderable.renderLayer - (int) this.renderLayer;
        if(difference > 0) return 1;
        if(difference < 0) return -1;
        return 0;
    }
}
