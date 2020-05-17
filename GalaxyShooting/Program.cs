using GalaxyShooting.Logic;
using GalaxyShooting.Rendering;
using System;
using System.Text;

namespace GalaxyShooting
{
    public static class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Screen.Initialize();

            //GameLoopManager.Start(new GameTitleLoop());
            GameLoopManager.Start(new TestGameLoop());
        }
    }
}
