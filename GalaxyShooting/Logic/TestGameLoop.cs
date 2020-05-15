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
            // TODO:
            // 카메라 구현 후에 camera 테스트를 위해 객체 생성을 해야 함
            // @moyamong

            renderer = new WireFrameRenderer(160, 80);
            objects = new List<GameObject>
            {
                new RenderTestObject()
            };
        }

        public override void Update()
        {
            foreach (GameObject obj in objects)
                obj.Update();
        }

        public override void Render()
        {
            renderer.ClearBuffer();

            foreach (GameObject obj in objects)
                obj.Render(renderer);

            renderer.RenderToBuffer(camera);

            renderer.SwapBuffer();
        }
    }
}
