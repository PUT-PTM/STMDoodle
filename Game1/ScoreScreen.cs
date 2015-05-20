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
        StreamReader reader;

        private string _stringValue = string.Empty;

        public ScoreScreen(int score)
        {
            wynik = score;
        }

        public override void Initialize()
        {
            base.Initialize();
            try
            {
                reader = new StreamReader("test.txt");
            }
            catch
            {
                FileStream plik = File.Create("test.txt");
                plik.Close();
                for (int i = 0; i < 9; i++)
                {
                    File.AppendAllText("test.txt", "Nowaczek 0\n");
                }
                File.AppendAllText("test.txt", "Nowaczek 0");
                reader = new StreamReader("test.txt");
            }
            for (int i = 0; i < 10; i++)
            {
                test = reader.ReadLine();
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

            var keyboardState = Keyboard.GetState();
            var keys = keyboardState.GetPressedKeys();

            if (keys.Length > 0)
            {
                var keyValue = keys[0].ToString();
                _stringValue += keyValue;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            int position = 0;
            int miejsce = 1;
            foreach (string s in listaWynikow)
            {
                spriteBatch.DrawString(font, miejsce + ". " + s, new Vector2(0, position), Color.Black);
                position += 30;
                miejsce++;
            }
            spriteBatch.DrawString(font, "Wynik: " + wynik,
                new Vector2(MyStaticValues.WinSize.X / 2 - (int)font.MeasureString("Wynik: " + wynik).X / 2,
                    MyStaticValues.WinSize.Y / 2 - (int)font.MeasureString("Wynik: " + wynik).Y / 2), Color.DarkBlue);
            //spriteBatch.DrawString(font, _stringValue, new Vector2(0,200), Color.White);
            spriteBatch.DrawString(font, "To Continue Press Enter",
                new Vector2(MyStaticValues.WinSize.X / 2 - (int)font.MeasureString("To Continue Press Enter").X / 2,
                    MyStaticValues.WinSize.Y / 2 + 100 - (int)font.MeasureString("To Continue Press Enter").Y / 2), Color.Black);
            base.Draw(spriteBatch);
        }
    }
}