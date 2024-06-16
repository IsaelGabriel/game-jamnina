using Raylib_cs;
using System.Numerics;
public class Player : IPositionable, IRenderable, IUpdatable
{
    private const int RenderLayer = 0;
    private const int UpdatePriority = 0;
    private const float BaseSpeed = 200f;
    private Vector2 _position = new Vector2(400, 400);
    private Vector2 _size = new Vector2(32, 32);
    private float _rotationDegrees = 0f;

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
            Rectangle rec = new Rectangle(_position, _size);
            //Raylib.DrawRectanglePro(rec, _size * 0.5f, _rotationDegrees * 360f, Color.Gold);
            Raylib.DrawPoly(_position + _size/2, 3, _size.Length() / 2, _rotationDegrees * 180f / (float) Math.PI, Color.Gold);
        }

        public bool Visible()
        {
            return true;
        }

    #endregion
    #region IUpdatable
        public void Update()
        {
            // Move
            Vector2 positionDifference = new Vector2(0, 0);
            
            positionDifference.X = Raylib.IsKeyDown(KeyboardKey.D) - Raylib.IsKeyDown(KeyboardKey.A);
            positionDifference.Y = Raylib.IsKeyDown(KeyboardKey.S) - Raylib.IsKeyDown(KeyboardKey.W);
            
            if(positionDifference.Length() != 0)
                positionDifference = Vector2.Normalize(positionDifference);

            ((IPositionable)this).TranslatePosition(positionDifference * Raylib.GetFrameTime() * BaseSpeed);

            // Rotate
            Vector2 mousePosition = Raylib.GetMousePosition();
            Vector2 screenPosition = Raylib.GetWorldToScreen2D(_position + _size/2, Engine.Camera);
            Vector2 rotationVector = -Vector2.Normalize(screenPosition - mousePosition);

            _rotationDegrees = (float) Math.Atan2(rotationVector.Y, rotationVector.X);
        }

        public int GetUpdatePriority()
        {
            return UpdatePriority;
        }

    #endregion
}