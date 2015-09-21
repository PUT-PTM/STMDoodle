#region Using Statements
using System;
using System.IO.Ports;
#endregion

namespace PTM
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        public static Random Losowaczka = new Random();
        public static SerialPort sPort = new SerialPort("COM11");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            sPort.Open();
            using (var game = new STMDoodle())

                game.Run();
            
        }
    }
#endif
}