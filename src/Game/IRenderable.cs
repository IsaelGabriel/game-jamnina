public interface IRenderable {
    public void Render();
    public int GetRenderLayer();
    public bool Visible();

    public int CompareRenderLayerTo(IRenderable renderable) {
        int difference = this.GetRenderLayer() - renderable.GetRenderLayer();
        if(difference > 0) return 1;
        if(difference < 0) return -1;
        return 0;
    }
}
