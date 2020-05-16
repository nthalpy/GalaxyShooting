using GalaxyShooting.Rendering;
using System.Collections.Generic;

namespace GalaxyShooting.Logic
{
    public sealed class TestGameLoop : GameLoopBase
    {
        private readonly Camera camera;
        private readonly WireFrameRenderer renderer;

        private readonly List<GameObjectBase> objects;

        public TestGameLoop()
        {
            camera = new Camera(2, 90, 0.1, 100);
            renderer = new WireFrameRenderer(320, 160);
            objects = new List<GameObjectBase>
            {
                new RenderTestObject()
            };
        }

        public override void Update()
        {
            foreach (GameObjectBase obj in objects)
                obj.Update();
        }

        public override void Render()
        {
            renderer.ClearBuffer();

            foreach (GameObjectBase obj in objects)
                obj.Render(renderer);

            renderer.RenderToBuffer(camera);

            renderer.SwapBuffer();
        }
    }
}
