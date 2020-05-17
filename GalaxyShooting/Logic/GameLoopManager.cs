using GalaxyShooting.Rendering;
using System.Diagnostics;
using System.Threading;

namespace GalaxyShooting.Logic
{
    public static class GameLoopManager
    {
        private static GameLoopBase currentGameLoop;

        public static void Start(GameLoopBase gameLoop)
        {
            int msInterval = 1000 / 60;

            Stopwatch sw = new Stopwatch();
            currentGameLoop = gameLoop;

            while (true)
            {
                sw.Restart();
                Loop();

                if (currentGameLoop.End())
                    break;

                int ms = (int)sw.ElapsedMilliseconds;
                if (ms < msInterval)
                    Thread.Sleep(msInterval - ms);
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
            Screen.Flush();
        }
    }
}
