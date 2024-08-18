using System;

namespace GamepadTester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GamepadTesterGame game = new GamepadTesterGame())
            {
                game.Run();
            }
        }
    }
}

