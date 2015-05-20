using Microsoft.Xna.Framework;

namespace PTM
{
    class Krawedz
    {
        int szerokosc = 256;
        int wysokosc = 16;
        public bool ruchoma = false;
        public bool zepsuty = false;
        public Rectangle prostokat;

        public Krawedz()
        {
            this.szerokosc = Program.Losowaczka.Next(64, 256);
            int test = Program.Losowaczka.Next(0, 1);
            if (test == 1)
            {
                this.ruchoma = true;
            }
            this.prostokat = new Rectangle(Program.Losowaczka.Next(MyStaticValues.WinSize.X - szerokosc), 0, szerokosc, wysokosc);
        }
    }
}