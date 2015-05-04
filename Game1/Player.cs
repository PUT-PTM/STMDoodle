using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PTM
{
    class Player
    {
        Texture2D playerSprite;
        SpriteFont font;
        Vector2 position;
        Rectangle playerRect = new Rectangle(0, 0, 50, 50);
        float moveSpeed = 500;
        float jumpSpeed = 1500;
        bool jump = false;
        Vector2 velocity;
        const float gravity = 40f;
        
        int wynik = 0;
        string czas;

        KeyboardState keyState;
        public void Initialize()
        {
            position = velocity = Vector2.Zero;
        }

        public void LoadContent(ContentManager content)
        {
            playerSprite = content.Load<Texture2D>("PlayerSprite");
            font = content.Load<SpriteFont>("SpriteFont1");
        }

        public void Update(GameTime gameTime)
        {
            
            keyState = Keyboard.GetState();

            czas = gameTime.TotalGameTime.ToString();

            if (keyState.IsKeyDown(Keys.Right))
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (keyState.IsKeyDown(Keys.Left))
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (jump)
            {
                velocity.Y -= jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                jump = false;
                wynik++;
            }
            if (!jump)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                velocity.Y = 0;
            position += velocity;


            if( position.Y >= MyStaticValues.WinSize.Y - playerSprite.Height)
            {
                position.Y--;
                jump = true;
            }
            
            CheckBorders();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerSprite, position, Color.White);
            spriteBatch.DrawString(font, MyStaticValues.nazwa + " " + MyStaticValues.wersja + "\nX: " + position.X.ToString() + " Y: " + position.Y.ToString()
                + "\nJump: " + jump.ToString() + " Up: " + keyState.IsKeyDown(Keys.Up).ToString() + "\nWynik: " + (wynik/2).ToString()
                + "\nCzas: " + czas.ToString(), Vector2.Zero, Color.White);
        }

        private void CheckBorders()
        {
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X + playerSprite.Width > MyStaticValues.WinSize.X)
                position.X = MyStaticValues.WinSize.X - playerSprite.Width;
            if (position.Y + playerSprite.Height > MyStaticValues.WinSize.Y)
                position.Y = MyStaticValues.WinSize.Y - playerSprite.Height;
        }
    }
}