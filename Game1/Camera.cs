using Microsoft.Xna.Framework;

namespace PTM
{
    public class Camera
    {
        Vector2 position;
        Vector2 prevPosition;
        Matrix viewMatrix;

        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }
        public void Update(Vector2 playerPosition)
        {
            prevPosition = position;
            position.Y = playerPosition.Y - (MyStaticValues.WinSize.Y / 2);
            position.X = 0;
            
            if (prevPosition.Y < position.Y)
               position = prevPosition;

            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }
    }
}
