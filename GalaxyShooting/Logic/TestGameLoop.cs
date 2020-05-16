using GalaxyShooting.Rendering;
using System;
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
            camera = new Camera(2, 45, 0.1, 100);
            renderer = new WireFrameRenderer(320, 160);
            objects = new List<GameObjectBase>();

            Random rd = new Random();

            for (int idx = 0; idx < 20; idx++)
            {
                RenderTestObject obj = new RenderTestObject();
                obj.Position = new Vector3(50 * (2 * rd.NextDouble() - 1), 50 * (2 * rd.NextDouble() - 1), 50 * (2 * rd.NextDouble() - 1));

                objects.Add(obj);
            }
        }

        public override void Update()
        {
            camera.Update();

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
