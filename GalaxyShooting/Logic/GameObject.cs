using GalaxyShooting.Rendering;

namespace GalaxyShooting.Logic
{
    public abstract class GameObject
    {
        public abstract void Update();
        public abstract void Render(WireFrameRenderer renderer);
    }
}
