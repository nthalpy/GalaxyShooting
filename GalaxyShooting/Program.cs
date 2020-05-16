using GalaxyShooting.Logic;
using System;
using System.Text;

namespace GalaxyShooting
{
    public static class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            //GameLoopManager.Start(new GameTitleLoop());
            GameLoopManager.Start(new TestGameLoop());
        }
    }
}
