using Microsoft.Xna.Framework;

namespace PTM
{
    class Coin
    {
        int szerokosc = 16;
        int wysokosc = 16;
        public Rectangle prostokat;

        public Coin()
        {

            this.prostokat = new Rectangle(Program.Losowaczka.Next(0, MyStaticValues.WinSize.X), Program.Losowaczka.Next(0, 20000) - 19000, szerokosc, wysokosc);
        }
    }
}