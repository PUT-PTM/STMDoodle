using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PTM
{
    class Player
    {
        Block blok;

        ContentManager content;
        Texture2D playerSprite;
        SpriteFont font;
        Vector2 position;
        public Rectangle playerRect;
        float moveSpeed = 500;
        float jumpSpeed = 1500;
        bool jump = false;
        Vector2 velocity;
        const float gravity = 40f;
        Vector2 positionBefore;
        bool opadanie = false;
        bool inter = false;
        
        int wynik = 0;
        string czas;

        public Rectangle Position
        {
            get { return playerRect; }
        } 
        public bool Opada
        { get { return opadanie; } }

        KeyboardState keyState;
        public void Initialize()
        {
            position = velocity = Vector2.Zero;
           
            
            
            
        }

        public void LoadContent(ContentManager Content)
        {
            blok = new Block();
            content = new ContentManager(Content.ServiceProvider, "Content");
            blok.LoadContent(Content);
            blok.Initialize();
            playerSprite = content.Load<Texture2D>("PlayerSprite");
            font = content.Load<SpriteFont>("SpriteFont1");
            
            
        }

        public void Update(GameTime gameTime)
        {
            positionBefore = position;
            keyState = Keyboard.GetState();

            czas = gameTime.TotalGameTime.ToString();

            if (keyState.IsKeyDown(Keys.Right))
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (keyState.IsKeyDown(Keys.Left))
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyState.IsKeyDown(Keys.Up) && jump)
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
                //position.Y--;
                jump = true;
            }

            playerRect = new Rectangle((int)position.X, (int)position.Y, playerSprite.Width, playerSprite.Height);
            
            
            #region colision
            CheckBorders();



            if (playerRect.Intersects(blok.Podloga2) && opadanie)
            {
                position.Y = blok.Podloga2.Y - playerSprite.Height;
                inter = true;
                jump = true;
            }
            else
                inter = false;
            if (playerRect.Intersects(blok.Podloga1) && opadanie)
            {
                position.Y = blok.Podloga1.Y - playerSprite.Height;
                inter = true;
                jump = true;
            }
            else
                inter = false;


                
            #endregion

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            blok.Draw(spriteBatch);
            spriteBatch.Draw(playerSprite, position, Color.White);
            spriteBatch.DrawString(font, MyStaticValues.nazwa + " " + MyStaticValues.wersja + "\nX: " + position.X.ToString() + " Y: " + position.Y.ToString()
                + "\nJump: " + jump.ToString() + " Up: " + keyState.IsKeyDown(Keys.Up).ToString() + "\nWynik: " + (wynik/2).ToString()
                + "\nCzas: " + czas.ToString() + "\nOpadanie: " + opadanie.ToString() + "\n intersect: " + inter.ToString(), Vector2.Zero, Color.White);
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
            if (positionBefore.Y < position.Y)
                opadanie = true;
            else opadanie = false;

        }
    }
}