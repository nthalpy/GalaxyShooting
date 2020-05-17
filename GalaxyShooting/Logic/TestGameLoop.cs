using GalaxyShooting.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GalaxyShooting.Logic
{
    public sealed class TestGameLoop : GameLoopBase
    {
        private readonly Camera camera;
        private readonly WireFrameRenderer wireframeRenderer;
        private readonly CrosshairRenderer crosshairRenderer;

        private readonly List<GameObjectBase> objects;

        private readonly GunLauncher gunLauncher;

        private List<GameObjectBase> dead;

        public TestGameLoop()
        {
            camera = new Camera(2, 45, 0.1, 100);
            wireframeRenderer = new WireFrameRenderer(Screen.ScreenSizeX, Screen.ScreenSizeY);
            crosshairRenderer = new CrosshairRenderer(Screen.ScreenSizeX, Screen.ScreenSizeY);
            objects = new List<GameObjectBase>();
            dead = new List<GameObjectBase>();

            Random rd = new Random();

            gunLauncher = new GunLauncher(2, camera);

            objects.Add(gunLauncher);

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
            {
                obj.Update();
                if (obj.Collision(gunLauncher.bullets[0]))
                {
                    Debug.WriteLine("kill");
                    dead.Add(obj);
                }
            }
            objects.RemoveAll(dead.Contains);
            dead.Clear();
        }

        public override void Render()
        {
            wireframeRenderer.ClearBuffer();

            foreach (GameObjectBase obj in objects)
                obj.Render(wireframeRenderer);

            wireframeRenderer.RenderToBuffer(camera);

            wireframeRenderer.SwapBuffer();

            crosshairRenderer.Render();
        }
    }
}
