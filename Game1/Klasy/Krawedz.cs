using Microsoft.Xna.Framework;
using System;

namespace PTM
{
    class Krawedz
    {
        int szerokosc;
        int wysokosc = 16;
        public string kierunek = "prawo";
        public bool ruchoma = false;
        public Rectangle prostokat;

        public Krawedz(int poprzednieX, int poprzednieWidth)
        {
            this.szerokosc = Program.Losowaczka.Next(64, 257);
            int test = Program.Losowaczka.Next(0, 10);
            if (test == 1)
            {
                this.ruchoma = true;
            }
            test = Program.Losowaczka.Next(0, 2);
            if (test == 1)
            {
                this.kierunek = "lewo";
            }
            int X;
            X = Program.Losowaczka.Next(MyStaticValues.WinSize.X - szerokosc);
            while (Math.Abs(X - poprzednieX - poprzednieWidth) > 400)
            {
                X = Program.Losowaczka.Next(MyStaticValues.WinSize.X - szerokosc);
            }
            this.prostokat = new Rectangle(X, 0, szerokosc, wysokosc);
        }
    }
}