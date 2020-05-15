using GalaxyShooting.Rendering;
using System.Collections.Generic;

namespace GalaxyShooting.Logic
{
    public sealed class TestGameLoop : GameLoop
    {
        private readonly Camera camera;
        private readonly WireFrameRenderer renderer;

        private readonly List<GameObject> objects;

        public TestGameLoop()
        {
            renderer = new WireFrameRenderer(160, 80);
            objects = new List<GameObject>();
        }

        public override void Update()
        {
        }

        public override void Render()
        {
            renderer.RenderToBuffer(camera);

            renderer.SwapBuffer();
            renderer.ClearBuffer();
        }
    }
}
