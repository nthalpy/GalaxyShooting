using System;
using System.Threading;

namespace GalaxyShooting.Logic
{
    public static class GameLoopManager
    {
        private static GameLoop currentGameLoop;

        public static void Start(GameLoop gameLoop)
        {
            currentGameLoop = gameLoop;

            while (true)
            {
                Loop();
                Thread.Sleep(TimeSpan.FromMilliseconds(50));
            }
        }

        public static void ChangeLoop(GameLoop gameLoop)
        {
            currentGameLoop = gameLoop;
        }

        private static void Loop()
        {
            GameLoop loop = currentGameLoop;

            loop.Update();
            loop.Render();
        }
    }
}
