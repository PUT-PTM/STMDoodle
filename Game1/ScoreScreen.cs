using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace PTM
{
    class ScoreScreen : GameScreen
    {
        PTM.Klasy.KbHandler klawa = new PTM.Klasy.KbHandler();
        KeyboardState keyState;
        SpriteFont font;
        List<string> listaWynikow = new List<string>();
        string test;
        int wynik;

        public ScoreScreen(int score)
        {
            wynik = score;
        }

        public override void Initialize()
        {
            base.Initialize();
            StreamReader reader = new StreamReader("test.txt");
            for (int i = 0; i < 10; i++)
            {
                test =  reader.ReadLine();
                listaWynikow.Add(test);
            }
            reader.Close();

            string name = "Nowaczek";
            
            File.AppendAllText("test.txt", "\n" + name + " " + wynik);
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
            klawa.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            int position = 0;
            int miejsce = 1;
            foreach (string s in listaWynikow)
            {
                spriteBatch.DrawString(font, miejsce + ". " + s, new Vector2(0, position), Color.Pink);
                position += 30;
                miejsce++;
            }
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