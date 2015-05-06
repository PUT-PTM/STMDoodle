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
    public class PlayScreen : GameScreen
    {
        KeyboardState keyState;
        SpriteFont font;

        Player player = new Player();
        Camera camera = new Camera();
        
        public override void Initialize()
        {
            player.Initialize();
        }
        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            if (font == null)
                font = content.Load<SpriteFont>("SpriteFont1");
            player.LoadContent(content);
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();

            player.Update(gameTime);
            camera.Update(player.Position);

            if (player.Koniecgry)
                ScreenManager.Instance.AddScreen(new TitleScreen());
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                null, null, null, null, camera.ViewMatrix);

            player.Draw(spriteBatch);


            base.Draw(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();
        }
    }
}
