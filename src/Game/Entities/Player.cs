using Raylib_cs;
using System.Numerics;

public class Player : IPositionable, IRenderable, IUpdatable
{
    private const RenderLayer DefaultRenderLayer = RenderLayer.Player;
    private const int UpdatePriority = 0;
    private const float BaseSpeed = 200f;
    private Vector2 _position = new Vector2(400, 400);
    private Vector2 _size = new Vector2(32, 32);
    private float _rotationDegrees = 0f;



    #region IPositionable

        public Vector2 position {
            get => _position;
            set => _position = value; 
        }

    #endregion

    #region IRenderable
        public int renderLayer {
            get => (int) DefaultRenderLayer;
        }

        public void Render()
        {
            Raylib.DrawPoly(_position + _size/2, 3, _size.Length() / 2, _rotationDegrees * 180f / (float) Math.PI, Color.Gold);

            Raylib.DrawPoly(_position + _size/2 + new Vector2((float) Math.Cos((double) _rotationDegrees), (float) Math.Sin((double) _rotationDegrees)) * _size, 8, 4, _rotationDegrees * 180f / (float) Math.PI, Color.Red);
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