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
            // TODO:
            // 카메라 구현 후에 camera 테스트를 위해 객체 생성을 해야 함
            // @moyamong
            camera = new Camera(1, 90, 0.1, 100);
            renderer = new WireFrameRenderer(40, 40);
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
