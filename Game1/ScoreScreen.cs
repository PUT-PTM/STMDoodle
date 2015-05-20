using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PTM
{
    class ScoreScreen : GameScreen
    {
        KeyboardState keyState;
        SpriteFont font;
        int wynik;

        public ScoreScreen(int score)
        {
            wynik = score;
        }

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
                ScreenManager.Instance.AddScreen(new TitleScreen());
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Wynik: " + wynik,
                new Vector2(MyStaticValues.WinSize.X / 2 - (int)font.MeasureString("Wynik: " + wynik).X / 2,
                    MyStaticValues.WinSize.Y / 2 - (int)font.MeasureString("Wynik: " + wynik).Y / 2), Color.Cyan);
            spriteBatch.DrawString(font, "To Continue Press Enter",
                new Vector2(MyStaticValues.WinSize.X / 2 - (int)font.MeasureString("To Continue Press Enter").X / 2,
                    MyStaticValues.WinSize.Y / 2 + 100 - (int)font.MeasureString("To Continue Press Enter").Y / 2), Color.YellowGreen);
            base.Draw(spriteBatch);
        }
    }
}
