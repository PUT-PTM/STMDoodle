using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PTM
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class STMDoodle : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Texture2D podlogaTexture;
        Rectangle podlogaRectangle;
        Rectangle podlogaRectangle2;
        bool przecinanie;

        Texture2D test;

        Player player1;
        public STMDoodle()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            /*
            ScreenManager.Instance.Initialize();
            ScreenManager.Instance.Dimensions = new Vector2(800, 600);
            */
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = MyStaticValues.WinSize.X;//(int)ScreenManager.Instance.Dimensions.X;
            graphics.PreferredBackBufferHeight = MyStaticValues.WinSize.Y;// (int)ScreenManager.Instance.Dimensions.Y;
            graphics.ApplyChanges();
            player1 = new Player();
            test = new Texture2D(GraphicsDevice, 1, 1);
            test.SetData(new[] { Color.White });
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            podlogaTexture = Content.Load<Texture2D>("Sprites/podloga");

            podlogaRectangle.Width = 256;
            podlogaRectangle.Height = 16;
            podlogaRectangle2.Width = 256;
            podlogaRectangle2.Height = 16;
            Random Losowaczka = new Random();
            podlogaRectangle.X = Losowaczka.Next(MyStaticValues.WinSize.X - podlogaRectangle.Width); ;
            podlogaRectangle2.X = Losowaczka.Next(MyStaticValues.WinSize.X - podlogaRectangle.Width); ;
            podlogaRectangle.Y = 400;
            podlogaRectangle2.Y = 500;
            /*
             * ScreenManager.Instance.LoadContent(Content);
             * 
             * */
            player1.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here
            /*
             * ScreenManager.Instance.Update(gameTime);
             * 
             */
            player1.Update(gameTime);
            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(podlogaTexture, podlogaRectangle, new Rectangle(0, 0, 256, 16), Color.White);
            spriteBatch.Draw(podlogaTexture, podlogaRectangle2, new Rectangle(0, 0, 256, 16), Color.White);
            /*
            ScreenManager.Instance.Draw(spriteBatch);
             * 
             */
            int bw = 2; // Border width

            spriteBatch.Draw(test, new Rectangle(podlogaRectangle.Left, podlogaRectangle.Top, bw, podlogaRectangle.Height), Color.Yellow); // Left
            spriteBatch.Draw(test, new Rectangle(podlogaRectangle.Right, podlogaRectangle.Top, bw, podlogaRectangle.Height), Color.Yellow); // Right
            spriteBatch.Draw(test, new Rectangle(podlogaRectangle.Left, podlogaRectangle.Top, podlogaRectangle.Width, bw), Color.Yellow); // Top
            spriteBatch.Draw(test, new Rectangle(podlogaRectangle.Left, podlogaRectangle.Bottom, podlogaRectangle.Width, bw), Color.Yellow); // B

            przecinanie = player1.playerRect.Intersects(podlogaRectangle);

            player1.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}