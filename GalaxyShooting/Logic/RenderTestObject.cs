using GalaxyShooting.Model;
using GalaxyShooting.Rendering;
using System;

namespace GalaxyShooting.Logic
{
    /// <summary>
    /// 렌더링 테스트용 object.
    /// </summary>
    public sealed class RenderTestObject : GameObjectBase
    {
        private int frameCount;

        public Vector3 Position;
        private Matrix4x4 currentRotationMatrix;
        private Quaternion rot;

        private Random rd;

        public RenderTestObject()
        {
            currentRotationMatrix = Matrix4x4.Identity;
            rd = new Random(this.GetHashCode());
        }

        public override void Update()
        {
            if (frameCount % 100 == 0)
            {
                Vector3 axis = Vector3.Normalize(new Vector3(rd.NextDouble() - 0.5, rd.NextDouble() - 0.5, rd.NextDouble() - 0.5));
                rot = Quaternion.AxisAngle(axis, Math.PI / 30);
            }

            currentRotationMatrix *= Matrix4x4.CreateRotationMatrix(rot);

            frameCount++;
        }

        public override void Render(WireFrameRenderer renderer)
        {
            Matrix4x4 modelMatrix = Matrix4x4.CreateTranslateMatrix(Position);

            Matrix4x4 rotateMatrix = currentRotationMatrix;

            foreach (Triangle triangle in Cube.Tris)
            {
                renderer.EnqueueTriangle(new Triangle(
                    (modelMatrix * (rotateMatrix * triangle.A.ToXYZ1())).HomogeneousToXYZ(),
                    (modelMatrix * (rotateMatrix * triangle.B.ToXYZ1())).HomogeneousToXYZ(),
                    (modelMatrix * (rotateMatrix * triangle.C.ToXYZ1())).HomogeneousToXYZ()
                ));
            }
        }
    }
}
