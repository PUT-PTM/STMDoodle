using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace PTM
{
    class Player
    {
        
        static int kierunek;
        int licznik = 0;
        Thread watek = new Thread(new ThreadStart(watekAkcelerometr));





        #region Variables
        Rectangle podloga = new Rectangle(0, MyStaticValues.WinSize.Y - 16, MyStaticValues.WinSize.X, 16);
        int poprzednieX = 0;
        int poprzednieWidth = 0;
        Coin badanyCoin;
        List<Krawedz> listaKrawedzi = new List<Krawedz>();
        List<Krawedz> listaKrawedziDoUsuniecia = new List<Krawedz>();
        Texture2D podlogaTexture;
        Texture2D coinTexture;
        Vector2 maxPosition;
        bool koniecGry = false;
        int dlugoscListy;
        Krawedz badanaKrawedz;
        Song song;
        int iloscCoinow = 0;
        List<Coin> listaCoinow = new List<Coin>();
        List<Coin> listaCoinowDoUsuniecia = new List<Coin>();

        public Rectangle coin;
        // TODO double frameRate;

        PlayerAnimation gracz = new PlayerAnimation();
        KeyboardState keyState;
        ContentManager content;
        Texture2D playerSprite;
        SpriteFont font;
        Vector2 position;
        public Rectangle playerRect;
        float moveSpeed = 500;
        float jumpSpeed = 1200;
        bool jump = false;
        Vector2 velocity;
        const float gravity = 40f;
        Vector2 positionBefore;
        bool opadanie = false;
        bool inter = false;


        int totalWynik;
        int wynik = 0;
        string czas = "0";

        Texture2D Bground;
        #endregion
        #region Properties
        public int Wynik { get { return totalWynik; } }
        public bool Koniecgry
        {
            get { return koniecGry; }
        }
        public Vector2 Position
        {
            get { return position; }
        }
        public bool Opada
        { get { return opadanie; } }
        #endregion

        public void Initialize()
        {

            

            for (int i = 0; i < 600; i++)
            {
                Krawedz k = new Krawedz(poprzednieX, poprzednieWidth);
                k.prostokat.Y += (i * -200) + (MyStaticValues.WinSize.Y - 50);
                listaKrawedzi.Add(k);
                poprzednieX = k.prostokat.X;
                poprzednieWidth = k.prostokat.Width;
            }
            for (int i = 0; i < 100; i++)
            {
                Coin c = new Coin();
                listaCoinow.Add(c);
            }

            position = new Vector2(((MyStaticValues.WinSize.X - gracz.FrameWidth) / 2), (MyStaticValues.WinSize.Y - gracz.FrameHeight));
            velocity = Vector2.Zero;

            coin = new Rectangle(100, 100, 16, 16);

            //watek.Start();

        }

        public void LoadContent(ContentManager Content)
        {
            //sPort.DataReceived += sPort_DataReceived;
            content = new ContentManager(Content.ServiceProvider, "Content");
            playerSprite = content.Load<Texture2D>("Sprites/Sprite");
            font = content.Load<SpriteFont>("font1");
            podlogaTexture = content.Load<Texture2D>("Sprites/podloga");
            coinTexture = content.Load<Texture2D>("Sprites/coin");
            Bground = content.Load<Texture2D>("Sprites/BabaArena");
            song = content.Load<Song>("Audio/veridisquo");
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.1f;
            gracz.LoadContent(content, playerSprite, "", position);



        }



        public void Update(GameTime gameTime)
        {
            //if(Program.sPort.BytesToRead  == 1 )
            //{
            //    int data = (int)Program.sPort.ReadByte();
            //    //string trash = sPort.ReadExisting();
            //    if (data >= 128)
            //        data -= 255;
            //    kierunek = data;
            //}else
            //    if (Program.sPort.BytesToRead > 1)
            //{

            //    string trash = Program.sPort.ReadExisting();
                
            //}

            #region Ruchome krawędzie
            foreach (Krawedz k in listaKrawedzi)
            {
                Vector2 kr = new Vector2(0, k.prostokat.Y);
                if (k.ruchoma == true && k.kierunek == "prawo")
                {
                    kr.X = 500 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    k.prostokat.X += (int)kr.X;
                }
                if (k.ruchoma == true && k.kierunek == "lewo")
                {
                    kr.X = 500 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    k.prostokat.X -= (int)kr.X;
                }
                if (k.prostokat.X > MyStaticValues.WinSize.X - k.prostokat.Width)
                {
                    k.kierunek = "lewo";
                }
                if (k.prostokat.X < 0)
                {
                    k.kierunek = "prawo";
                }
            }
            #endregion
            if (position.Y < maxPosition.Y)
            {
                maxPosition = position;
                maxPosition.X = 0;
            }
            else
                if (maxPosition.Y + 700 < position.Y)
                {
                    koniecGry = true;
                }
            totalWynik = (int)((maxPosition.Y - 2 * maxPosition.Y) / 100) + wynik;


            //string test;

            //sport.DataReceived() += test;

            positionBefore = position;
            keyState = Keyboard.GetState();

            czas = gameTime.TotalGameTime.ToString();

            #region Sterowanie

            if (keyState.IsKeyDown(Keys.Enter) || keyState.IsKeyDown(Keys.Space))
                ScreenManager.Instance.AddScreen(new PlayScreen());
            gracz.IsActive = true;
            if (opadanie)
            {
                gracz.CurrentFrame = new Vector2(gracz.CurrentFrame.X, 2);
            }
            else
            {
                gracz.CurrentFrame = new Vector2(gracz.CurrentFrame.X, 0);
            }
            if (kierunek > 0)
            {
                gracz.CurrentFrame = new Vector2(0, gracz.CurrentFrame.Y);
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * kierunek/48;
            }
            else if (kierunek < 0)
            {
                gracz.CurrentFrame = new Vector2(1, gracz.CurrentFrame.Y);
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * kierunek/48;
            }
            if (jump)
            {
                gracz.CurrentFrame = new Vector2(gracz.CurrentFrame.X, 1);
                velocity = Vector2.Zero;
                velocity.Y -= jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                jump = false;
            }

            if (!jump)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
            {
                velocity.Y = 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                opadanie = false;
            }
            position += velocity;


            if (position.Y >= MyStaticValues.WinSize.Y - gracz.FrameHeight)
            {
                jump = true;
            }
            #endregion




            #region colision
            CheckBorders();

            foreach (Krawedz k in listaKrawedzi)
            {
                if (playerRect.Intersects(k.prostokat) && opadanie)
                {
                    gracz.CurrentFrame = new Vector2(gracz.CurrentFrame.X, 1);
                    position.Y = k.prostokat.Y - gracz.FrameHeight;
                    inter = true;
                    jump = true;
                }
                else
                    inter = false;
            }

            if (playerRect.Intersects(podloga))
            {
                position.Y = podloga.Y - gracz.FrameHeight;
                inter = true;
                jump = true;
            }
            else
                inter = false;

            foreach (Coin c in listaCoinow)
            {
                if (playerRect.Intersects(c.prostokat))
                {
                    inter = true;
                    wynik += 20;
                    iloscCoinow++;
                    listaCoinowDoUsuniecia.Add(c);
                }
                else
                    inter = false;
            }
            #endregion
            #region Rysowanie bloków
            dlugoscListy = listaKrawedzi.Count;
            foreach (Krawedz k in listaKrawedziDoUsuniecia)
            {
                for (int i = 0; i < dlugoscListy - 1; i++)
                {
                    badanaKrawedz = listaKrawedzi[i];
                    if (k == badanaKrawedz)
                    {
                        listaKrawedzi.Remove(k);
                    }
                }
            }
            listaKrawedziDoUsuniecia.RemoveRange(0, listaKrawedziDoUsuniecia.Count);



            dlugoscListy = listaCoinow.Count;
            foreach (Coin c in listaCoinowDoUsuniecia)
            {
                for (int i = 0; i < dlugoscListy - 1; i++)
                {
                    try
                    {
                        badanyCoin = listaCoinow[i];
                    }
                    catch
                    {
                        return;
                    }
                    if (c == badanyCoin)
                    {
                        listaCoinow.Remove(c);
                    }
                }
            }
            listaCoinowDoUsuniecia.RemoveRange(0, listaCoinowDoUsuniecia.Count);
            #endregion
            gracz.Position = position;
            gracz.Update(gameTime);
            playerRect = new Rectangle((int)position.X, (int)position.Y, gracz.FrameWidth, gracz.FrameHeight);
            gracz.IsActive = false;
/*
            if (licznik == 30)
            {
                updateKierunek();
                licznik = 0;
            }
            else
                licznik++;*/
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Bground, new Vector2(0, -1024 + MyStaticValues.WinSize.Y), Color.White);
            //spriteBatch.Draw(coinTexture, coin, Color.White);

            foreach (Coin c in listaCoinow)
            {
                spriteBatch.Draw(coinTexture, c.prostokat, Color.White);
                if (c.prostokat.Y > maxPosition.Y + 500)
                    listaCoinowDoUsuniecia.Add(c);
            }

            foreach (Krawedz k in listaKrawedzi)
            {
                spriteBatch.Draw(podlogaTexture, k.prostokat, Color.White);
                if (k.prostokat.Y > maxPosition.Y + 500)
                    listaKrawedziDoUsuniecia.Add(k);
            }

            spriteBatch.Draw(podlogaTexture, podloga, Color.White);
            gracz.Draw(spriteBatch);
            //spriteBatch.Draw(playerSprite, position, Color.White);

            /*
            if (koniecGry == true)
            {
                spriteBatch.DrawString(font, "Koniec gry", (maxPosition + new Vector2(0, -300)), Color.Yellow);
                // Compose a string that consists of three lines.
                string lines = wynik.ToString();

                // Write the string to a file.
                // System.IO.StreamWriter file = new System.IO.StreamWriter("test.txt");
                // file.WriteLine(lines);

                //file.Close();
            }*/
            spriteBatch.DrawString(font, "Daft Punk - Veridis Quo", new Vector2(
               (MyStaticValues.WinSize.X - (int)font.MeasureString("Daft Punk - Veridis Quo").X - 10),
               (MyStaticValues.WinSize.Y - (int)font.MeasureString("Daft Punk - Veridis Quo").Y - 20)), Color.Black);
            spriteBatch.End();
            spriteBatch.Begin();
            /*spriteBatch.DrawString(font,
                MyStaticValues.nazwa + " " +
                MyStaticValues.wersja.ToString().Replace(',', '.') + "\nX: " +
                position.X.ToString() + " Y: " +
                position.Y.ToString() +
                "\nKierunek: " + kierunek.ToString() +
                "\nWynik: " + ((int)(((maxPosition.Y - 2 * maxPosition.Y) / 100) + wynik)).ToString() +
                "\nIloscCoinow: " + iloscCoinow.ToString() +
                "\nCzas: " + czas.ToString() +
                "\nOpadanie: " + opadanie.ToString() +
                "\nKoniec: " + koniecGry.ToString(), Vector2.Zero, Color.White);*/
            spriteBatch.End();
            spriteBatch.Begin();
        }

        private void CheckBorders()
        {
            if (position.X < 0)
                position.X = 0;
            if (position.X + gracz.FrameWidth > MyStaticValues.WinSize.X)
                position.X = MyStaticValues.WinSize.X - gracz.FrameWidth;
            if (position.Y + gracz.FrameHeight > MyStaticValues.WinSize.Y)
                position.Y = MyStaticValues.WinSize.Y - gracz.FrameHeight;
            if (positionBefore.Y < position.Y)
                opadanie = true;
            else opadanie = false;
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sport = (SerialPort)sender;
            //string indata = sport.ReadExisting();
            int data = (int)sport.ReadByte();
            string trash = sport.ReadExisting();
            if (data >= 128)
                data -= 255;
            kierunek = data;
        }


        static void updateKierunek()
        {
            int data = (int)Program.sPort.ReadByte();
            string trash = Program.sPort.ReadExisting();
            if (data >= 128)
                data -= 255;
            kierunek = data;
        }

       static void watekAkcelerometr()
        {
           while (true)
            updateKierunek();
        }
    }
}