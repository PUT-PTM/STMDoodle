﻿using Microsoft.Xna.Framework;
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
            
            ScreenManager.Instance.Initialize();
            ScreenManager.Instance.Dimensions = new Vector2(MyStaticValues.WinSize.X, MyStaticValues.WinSize.Y);
            
            graphics.PreferredBackBufferWidth = MyStaticValues.WinSize.X;//(int)ScreenManager.Instance.Dimensions.X;
            graphics.PreferredBackBufferHeight = MyStaticValues.WinSize.Y;// (int)ScreenManager.Instance.Dimensions.Y;
            graphics.ApplyChanges();
           
           
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

            IsMouseVisible = true;
             ScreenManager.Instance.LoadContent(Content);
             
            
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
            
            ScreenManager.Instance.Update(gameTime);
             

            
            
            
            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(24,144,248));
            // TODO: Add your drawing code here
           
            spriteBatch.Begin();
            

            ScreenManager.Instance.Draw(spriteBatch);
            
            
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}