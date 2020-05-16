using System;
using System.Threading;

namespace GalaxyShooting.Logic
{
    public static class GameLoopManager
    {
        private static GameLoopBase currentGameLoop;

        public static void Start(GameLoopBase gameLoop)
        {
            currentGameLoop = gameLoop;

            while (true)
            {
                Loop();
                Thread.Sleep(TimeSpan.FromMilliseconds(50));
            }
        }

        public static void ChangeLoop(GameLoopBase gameLoop)
        {
            currentGameLoop = gameLoop;
        }

        private static void Loop()
        {
            GameLoopBase loop = currentGameLoop;

            loop.Update();
            loop.Render();
        }
    }
}
