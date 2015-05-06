using System;

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
        Texture2D test;
        
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

            podlogaRectangle = new Rectangle(
                Program.Losowaczka.Next(MyStaticValues.WinSize.X - podlogaRectangle.Width), podlogaRectangle.Y = 400,
                256, 16);   
            podlogaRectangle2 = new Rectangle(
                Program.Losowaczka.Next(MyStaticValues.WinSize.X - podlogaRectangle.Width), podlogaRectangle2.Y = 500,
                256,16);
            
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

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(podlogaTexture, podlogaRectangle, Color.White);
            spriteBatch.Draw(podlogaTexture, podlogaRectangle2, Color.White);

            
            int bw = 2; // Border width

            spriteBatch.Draw(test, new Rectangle(podlogaRectangle.Left, podlogaRectangle.Top, bw, podlogaRectangle.Height), Color.Yellow); // Left
            spriteBatch.Draw(test, new Rectangle(podlogaRectangle.Right, podlogaRectangle.Top, bw, podlogaRectangle.Height), Color.Yellow); // Right
            spriteBatch.Draw(test, new Rectangle(podlogaRectangle.Left, podlogaRectangle.Top, podlogaRectangle.Width, bw), Color.Yellow); // Top
            spriteBatch.Draw(test, new Rectangle(podlogaRectangle.Left, podlogaRectangle.Bottom, podlogaRectangle.Width, bw), Color.Yellow); // B

            //przecinanie = player1.playerRect.Intersects(podlogaRectangle);
        }

    }
}
