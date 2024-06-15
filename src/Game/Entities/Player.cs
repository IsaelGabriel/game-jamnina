using Raylib_cs;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
public class Player : IPositionable, IRenderable, IUpdatable
{
    private const int RenderLayer = 0;
    private const int UpdatePriority = 0;
    private const float BaseSpeed = 200f;
    private Vector2 _position = new Vector2(400, 400);

    #region IPositionable
        public Vector2 GetPosition()
        {
            return this._position;
        }

        public Vector2 SetPosition(Vector2 position)
        {
            this._position = position;
            return this._position;
        }

    #endregion

    #region IRenderable
        public int GetRenderLayer()
        {
            return RenderLayer;
        }


        public void Render()
        {
            Raylib.DrawRectangle((int) this.GetPosition().X, (int) this.GetPosition().Y, 32, 32, Color.Gold);
        }

        public bool Visible()
        {
            return true;
        }

    #endregion
    #region IUpdatable
        public void Update()
        {
            Vector2 positionDifference = new Vector2(0, 0);
            positionDifference.X = Raylib.IsKeyDown(KeyboardKey.D) - Raylib.IsKeyDown(KeyboardKey.A);
            positionDifference.Y = Raylib.IsKeyDown(KeyboardKey.S) - Raylib.IsKeyDown(KeyboardKey.W);
            if(positionDifference.Length() != 0)
                positionDifference = Vector2.Normalize(positionDifference);

            ((IPositionable)this).TranslatePosition(positionDifference * Raylib.GetFrameTime() * BaseSpeed);
        }

        public int GetUpdatePriority()
        {
            return UpdatePriority;
        }

    #endregion
}