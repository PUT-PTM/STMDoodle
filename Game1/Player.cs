using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        Rectangle playerRect;
        float moveSpeed = 500;
        float jumpSpeed = 1000;
        bool jump = false;
        Vector2 velocity;
        const float gravity = 40f;

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

            if (keyState.IsKeyDown(Keys.Right))
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (keyState.IsKeyDown(Keys.Left))
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (keyState.IsKeyDown(Keys.Up) && jump)
            {
                velocity.Y -= jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                jump = false;
            }
            if (!jump)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                velocity.Y = 0;
            position += velocity;

            jump = position.Y >= 350;
            if (jump)
                position.Y = 350;
            
            CheckBorders();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerSprite, position, Color.White);
            spriteBatch.DrawString(font, "X: " + position.X.ToString() + " Y: " + position.Y.ToString()
                + "\nJump: " + jump.ToString() + " Up: " + keyState.IsKeyDown(Keys.Up).ToString(), Vector2.Zero, Color.White);
        }
        private void CheckBorders()
        {
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X + playerSprite.Height > 600)
                position.X = 600 - playerSprite.Height;
            if (position.Y + playerSprite.Width > 400)
                position.Y = 400 - playerSprite.Width;
            
                  
        }
    }
}
