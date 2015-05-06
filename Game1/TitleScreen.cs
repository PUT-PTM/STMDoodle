using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace PTM
{
    class TitleScreen : GameScreen
    {
        KeyboardState keyState;
        SpriteFont font;

        public override void Initialize()
        {
            base.Initialize();
        }
        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            if (font == null)
                font = content.Load<SpriteFont>("SpriteFont1");
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Enter) || keyState.IsKeyDown(Keys.Space))
                ScreenManager.Instance.AddScreen(new PlayScreen());
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "STM DOODLE", 
                new Vector2(MyStaticValues.WinSize.X / 2 - (int)font.MeasureString("STM Doodle").X / 2,
                    MyStaticValues.WinSize.Y / 2 - (int)font.MeasureString("STM Doodle").Y / 2), Color.Cyan);
            spriteBatch.DrawString(font, "To Play Press Enter",
                new Vector2(MyStaticValues.WinSize.X / 2 - (int)font.MeasureString("To Play Press Enter").X / 2,
                    MyStaticValues.WinSize.Y / 2 + 100 - (int)font.MeasureString("To Play Press Enter").Y / 2), Color.YellowGreen);
            base.Draw(spriteBatch);
        }
    }
}
