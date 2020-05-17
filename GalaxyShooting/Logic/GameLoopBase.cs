namespace GalaxyShooting.Logic
{
    public abstract class GameLoopBase
    {
        public abstract void Update();
        public abstract void Render();
        public abstract bool End();
    }
}
