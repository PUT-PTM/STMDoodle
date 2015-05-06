using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PTM
{
    class Block
    {
        Texture2D podlogaTexture;
        Rectangle podlogaRectangle;
        Rectangle podlogaRectangle2;
        int rectangleWidth = 256;
        int rectangleHeight = 16;
        int rect1X, rect1Y;
        int rect2X, rect2Y;
        Texture2D test;
        float przewijanie = 100;
        
        public Rectangle Podloga1
        {
            get { return podlogaRectangle; }
        }
        public Rectangle Podloga2
        {
            get { return podlogaRectangle2; }
        }

        public void Initialize()
        {
            rect1X = Program.Losowaczka.Next(MyStaticValues.WinSize.X - podlogaRectangle.Width);
            rect1Y = 400;
            rect2X = Program.Losowaczka.Next(MyStaticValues.WinSize.X - podlogaRectangle2.Width);
            rect2Y = 500;
            podlogaRectangle = new Rectangle(
                rect1X, rect1Y,
                rectangleWidth, rectangleHeight);
            podlogaRectangle2 = new Rectangle(
                rect2X, rect2Y,
                rectangleWidth, rectangleHeight);
            
        }
        public void LoadContent(ContentManager content)
        {
            podlogaTexture = content.Load<Texture2D>("Sprites/podloga");


            test = content.Load<Texture2D>("Sprites/podloga");
                //new Texture2D(GraphicsDevice, 1, 1);
           // test.SetData(new[] { Color.White });
        }
        public void Update(GameTime gameTime)
        {
            rect1Y += (int)(przewijanie * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (rect1Y >= MyStaticValues.WinSize.Y)
            {
                rect1Y = rect2Y - 200;
                rect1X = Program.Losowaczka.Next(MyStaticValues.WinSize.X - rectangleWidth);
            }
            podlogaRectangle = new Rectangle(rect1X, rect1Y, rectangleWidth, rectangleHeight);

            rect2Y += (int)(przewijanie * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (rect2Y >= MyStaticValues.WinSize.Y)
            {
                rect2Y = rect1Y - 200;
                rect2X = Program.Losowaczka.Next(MyStaticValues.WinSize.X - rectangleWidth);
            }
            podlogaRectangle2 = new Rectangle(rect2X, rect2Y, rectangleWidth, rectangleHeight);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(podlogaTexture, podlogaRectangle, Color.White);
            spriteBatch.Draw(podlogaTexture, podlogaRectangle2, Color.White);

            /*
            int bw = 2; // Border width

            spriteBatch.Draw(test, new Rectangle(podlogaRectangle.Left, podlogaRectangle.Top, bw, podlogaRectangle.Height), Color.Yellow); // Left
            spriteBatch.Draw(test, new Rectangle(podlogaRectangle.Right, podlogaRectangle.Top, bw, podlogaRectangle.Height), Color.Yellow); // Right
            spriteBatch.Draw(test, new Rectangle(podlogaRectangle.Left, podlogaRectangle.Top, podlogaRectangle.Width, bw), Color.Yellow); // Top
            spriteBatch.Draw(test, new Rectangle(podlogaRectangle.Left, podlogaRectangle.Bottom, podlogaRectangle.Width, bw), Color.Yellow); // B
            */
            //przecinanie = player1.playerRect.Intersects(podlogaRectangle);
        }

    }
}
