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
        private Matrix4x4 sizeMatrix;
        private Quaternion rot;

        private Random rd;

        private double xSize;
        private double ySize;
        private double zSize;

        public RenderTestObject()
        {
            currentRotationMatrix = Matrix4x4.Identity;
            rd = new Random(this.GetHashCode());
            xSize = 2 * rd.NextDouble() + 1;
            ySize = 2 * rd.NextDouble() + 1;
            zSize = 2 * rd.NextDouble() + 1;
            sizeMatrix = Matrix4x4.CreateSizeTransformMatrix(xSize, ySize, zSize);
        }

        public override int Score()
        {
            return (int)(xSize * ySize * zSize * 10);
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

        public override bool Collision(Bullet bullet)
        {
            double dist = (Position - bullet.Position).Length();
            if (dist < (xSize + ySize + zSize) / 1.5)
                return true;
            else
                return false;
        }

        public override void Render(WireFrameRenderer renderer)
        {
            Matrix4x4 translateMatrix = Matrix4x4.CreateTranslateMatrix(Position);
            Matrix4x4 rotateMatrix = currentRotationMatrix;


            foreach (Triangle triangle in Cube.Tris)
            {
                renderer.EnqueueTriangle(new Triangle(
                    (translateMatrix * (rotateMatrix * (sizeMatrix * triangle.A.ToXYZ1()))).HomogeneousToXYZ(),
                    (translateMatrix * (rotateMatrix * (sizeMatrix * triangle.B.ToXYZ1()))).HomogeneousToXYZ(),
                    (translateMatrix * (rotateMatrix * (sizeMatrix * triangle.C.ToXYZ1()))).HomogeneousToXYZ()
                ));
            }
        }
    }
}
