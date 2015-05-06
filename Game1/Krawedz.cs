using Microsoft.Xna.Framework;

namespace PTM
{
    class Krawedz
    {
        int szerokosc = 256;
        int wysokosc = 16;
        public Rectangle prostokat;

        public Krawedz()
        {
            this.prostokat = new Rectangle(Program.Losowaczka.Next(MyStaticValues.WinSize.X - szerokosc), 0, szerokosc, wysokosc);
        }
    }
}