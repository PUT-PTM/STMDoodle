using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PTM
{
    class Player
    {
        List<Krawedz> listaKrawedzi = new List<Krawedz>();
        Texture2D podlogaTexture;
        Vector2 maxPosition;
        bool koniecGry = false;
        // TODO double frameRate;

        Block blok;

        ContentManager content;
        Texture2D playerSprite;
        SpriteFont font;
        Vector2 position;
        public Rectangle playerRect;
        float moveSpeed = 500;
        float jumpSpeed = 1200;
        bool jump = false;
        Vector2 velocity;
        const float gravity = 40f;
        Vector2 positionBefore;
        bool opadanie = false;
        bool inter = false;

        
        int wynik = 0;
        string czas;

        public Vector2 Position
        {
            get { return position; }
        } 
        public bool Opada
        { get { return opadanie; } }

        KeyboardState keyState;
        public void Initialize()
        {
            for (int i = 0; i < 600; i++)
            {
                Krawedz k = new Krawedz();
                k.prostokat.Y += (i * -200) + (MyStaticValues.WinSize.Y - 50);
                listaKrawedzi.Add(k);
            }
            position = new Vector2((MyStaticValues.WinSize.X - 200), (MyStaticValues.WinSize.Y - 200));
            velocity = Vector2.Zero;
            blok = new Block();
            blok.Initialize();
        }

        public void LoadContent(ContentManager Content)
        {
            
            content = new ContentManager(Content.ServiceProvider, "Content");
            blok.LoadContent(Content);
            playerSprite = content.Load<Texture2D>("PlayerSprite");
            font = content.Load<SpriteFont>("SpriteFont1");
            podlogaTexture = content.Load<Texture2D>("Sprites/podloga");
            
            
        }

        public void Update(GameTime gameTime)
        {


            blok.Update(gameTime,position);
            if (position.Y < maxPosition.Y)
            { 
                maxPosition = position;
                maxPosition.X = 0;
            }
            else
                if (maxPosition.Y + 700 < position.Y)
                {
                    koniecGry = true;
                }


            positionBefore = position;
            keyState = Keyboard.GetState();

            czas = gameTime.TotalGameTime.ToString();

            

            if (keyState.IsKeyDown(Keys.Right))
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else if (keyState.IsKeyDown(Keys.Left))
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (jump)
            {
                velocity = Vector2.Zero;
                velocity.Y -= jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                jump = false;
                wynik++;
            }
            if (!jump)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
            {
                velocity.Y = 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                opadanie = false;
            }
            position += velocity;


            if( position.Y >= MyStaticValues.WinSize.Y - playerSprite.Height)
            {
                jump = true;
            }

            playerRect = new Rectangle((int)position.X, (int)position.Y, playerSprite.Width, playerSprite.Height);
            
            
            #region colision
            CheckBorders();

            foreach (Krawedz k in listaKrawedzi)
            {
                if (playerRect.Intersects(k.prostokat) && opadanie)
                {
                    position.Y = k.prostokat.Y - playerSprite.Height;
                    inter = true;
                    jump = true;
                }
                else
                    inter = false;
            }               
            #endregion

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Krawedz k in listaKrawedzi)
            {
                spriteBatch.Draw(podlogaTexture, k.prostokat, Color.White);
            }
            spriteBatch.Draw(playerSprite, position, Color.White);
            spriteBatch.DrawString(font,
                MyStaticValues.nazwa + " " +
                MyStaticValues.wersja.ToString().Replace(',', '.') + "\nX: " +
                position.X.ToString() + " Y: " +
                position.Y.ToString() +
                "\nJump: " + jump.ToString() +
                " Up: " + keyState.IsKeyDown(Keys.Up).ToString() +
                "\nWynik: " + ((maxPosition.Y - 2*maxPosition.Y)/100).ToString() +
                "\nCzas: " + czas.ToString() +
                "\nOpadanie: " + opadanie.ToString() +
                "\nIntersect: " + inter.ToString() +
                "\nKoniec: " + koniecGry.ToString(), maxPosition + new Vector2(0, - 100), Color.White);
            if (koniecGry == true)
            {
                spriteBatch.DrawString(font, "Koniec gry", (maxPosition + new Vector2(0, -300)), Color.Yellow);
            }

        }

        private void CheckBorders()
        {
            if (position.X < 0)
                position.X = 0;
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