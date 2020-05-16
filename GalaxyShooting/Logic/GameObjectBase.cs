using GalaxyShooting.Rendering;

namespace GalaxyShooting.Logic
{
    public abstract class GameObjectBase
    {
        public abstract void Update();
        public abstract void Render(WireFrameRenderer renderer);
    }
}
