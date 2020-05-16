using GalaxyShooting.Rendering;

namespace GalaxyShooting.Logic
{
    /// <summary>
    /// 렌더링 테스트용 object.
    /// </summary>
    public sealed class RenderTestObject : GameObject
    {
        public override void Update()
        {
        }
     
        public override void Render(WireFrameRenderer renderer)
        {
            /*
            renderer.EnqueueTriangle(new Triangle(
                new Vector3(-0.5, -0.5, 0),
                new Vector3(0.5, 0, 0),
                new Vector3(0, 0.5, 0)
            ));
            */

            //test for a qube
            renderer.EnqueueTriangle(new Triangle(
                new Vector3(1, 1, 2),
                new Vector3(1, -1, 2),
                new Vector3(1, 1, 4)
            ));
            renderer.EnqueueTriangle(new Triangle(
                new Vector3(1, 1, 4),
                new Vector3(1, -1, 4),
                new Vector3(1, -1, 2)
            ));

            renderer.EnqueueTriangle(new Triangle(
                new Vector3(-1, 1, 2),
                new Vector3(-1, -1, 2),
                new Vector3(-1, 1, 4)
            ));

            renderer.EnqueueTriangle(new Triangle(
                new Vector3(-1, 1, 4),
                new Vector3(-1, -1, 4),
                new Vector3(-1, -1, 2)
            ));

            renderer.EnqueueTriangle(new Triangle(
                new Vector3(1, 1, 2),
                new Vector3(-1, 1, 2),
                new Vector3(1, 1, 4)
            ));
            renderer.EnqueueTriangle(new Triangle(
                new Vector3(1, 1, 4),
                new Vector3(-1, 1, 4),
                new Vector3(-1, 1, 2)
            ));
            renderer.EnqueueTriangle(new Triangle(
                new Vector3(1, -1, 2),
                new Vector3(-1, -1, 2),
                new Vector3(1, -1, 4)
            ));
            renderer.EnqueueTriangle(new Triangle(
                new Vector3(1, -1, 4),
                new Vector3(-1, -1, 4),
                new Vector3(-1, -1, 2)
            ));
        }
    }
}
