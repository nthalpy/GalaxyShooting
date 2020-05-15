using GalaxyShooting.Logic;

namespace GalaxyShooting
{
    public static class Program
    {
        private static void Main()
        {
            GameLoopManager.Start(new TestGameLoop());
        }
    }
}
