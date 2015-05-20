#region Using Statements
using System;

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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new STMDoodle())
                game.Run();
        }
    }
#endif
}
