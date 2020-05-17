using GalaxyShooting.Rendering;

namespace GalaxyShooting.Logic
{
    public abstract class GameObjectBase
    {
        public abstract void Update();
        public abstract bool Collision(Bullet obj);
        public abstract void Render(WireFrameRenderer renderer);
    }
}
