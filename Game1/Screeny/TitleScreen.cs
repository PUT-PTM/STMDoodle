using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
            spriteBatch.DrawString(font, "STM DOODLE " + MyStaticValues.wersja.ToString().Replace(',','.'), 
                new Vector2(MyStaticValues.WinSize.X / 2 - (int)font.MeasureString("STM Doodle x.xx").X / 2,
                    MyStaticValues.WinSize.Y / 2 - (int)font.MeasureString("STM Doodle").Y / 2), Color.White);
            spriteBatch.DrawString(font, "To Play Press Enter",
                new Vector2(MyStaticValues.WinSize.X / 2 - (int)font.MeasureString("To Play Press Enter").X / 2,
                    MyStaticValues.WinSize.Y / 2 + 100 - (int)font.MeasureString("To Play Press Enter").Y / 2), Color.DarkGreen);
            spriteBatch.DrawString(font, "Piotr Nowak\nDamian Rusin",
                new Vector2(MyStaticValues.WinSize.X / 2 - (int)font.MeasureString("Piotr Nowak").X / 2,
                    MyStaticValues.WinSize.Y / 2 + 200 - (int)font.MeasureString("Piotr Nowak").Y / 2), Color.SeaShell);
            spriteBatch.DrawString(font, "2015",
                new Vector2(MyStaticValues.WinSize.X / 2 - (int)font.MeasureString("Piotr Nowak").X / 2,
                    MyStaticValues.WinSize.Y / 2 + 275 - (int)font.MeasureString("Piotr Nowak").Y / 2), Color.SeaShell);
            base.Draw(spriteBatch);
        }
    }
}