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
            renderer.EnqueueTriangle(new Triangle(
                new Vector3(-0.5, -0.5, 0),
                new Vector3(0.5, 0, 0),
                new Vector3(0, 0.5, 0)
            ));
        }
    }
}
