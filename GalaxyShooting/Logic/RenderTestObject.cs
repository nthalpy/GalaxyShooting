using GalaxyShooting.Input;
using GalaxyShooting.Rendering;
using System;

namespace GalaxyShooting.Logic
{
    /// <summary>
    /// 렌더링 테스트용 object.
    /// </summary>
    public sealed class RenderTestObject : GameObjectBase
    {
        private Vector3[] vbo;
        private int[] ebo;

        private Triangle[] tris;

        private double theta;

        private double yaw;
        private double pitch;
        private double roll;

        public Vector3 Position;
        public Quaternion RotationX;
        public Quaternion RotationY;
        public Quaternion RotationZ;

        public RenderTestObject()
        {
            vbo = new Vector3[]
            {
                new Vector3(-1, -1, -1),
                new Vector3(-1, -1, 1),
                new Vector3(-1, 1, -1),
                new Vector3(-1, 1, 1),
                new Vector3(1, -1, -1),
                new Vector3(1, -1, 1),
                new Vector3(1, 1, -1),
                new Vector3(1, 1, 1),
            };
            ebo = new int[]
            {
                0, 6, 4,
                0, 2, 6,
                0, 3, 2,
                0, 1, 3,
                2, 7, 6,
                2, 3, 7,
                4, 6, 7,
                4, 7, 5,
                0, 4, 5,
                0, 5, 1,
                1, 5, 7,
                1, 7, 3
            };

            tris = new Triangle[ebo.Length / 3];
            for (int idx = 0; idx < ebo.Length / 3; idx++)
            {
                tris[idx] = new Triangle(
                    vbo[ebo[3 * idx]],
                    vbo[ebo[3 * idx + 1]],
                    vbo[ebo[3 * idx + 2]]
                );
            }
        }

        public override void Update()
        {
            if (InputManager.IsPressed(VK.LEFT))
                theta -= Math.PI / 30;
            if (InputManager.IsPressed(VK.RIGHT))
                theta += Math.PI / 30;

            if (InputManager.IsPressed(VK.KEY_S))
                pitch += Math.PI / 30;
            if (InputManager.IsPressed(VK.KEY_W))
                pitch -= Math.PI / 30;
            if (InputManager.IsPressed(VK.KEY_A))
                yaw -= Math.PI / 30;
            if (InputManager.IsPressed(VK.KEY_D))
                yaw += Math.PI / 30;
            if (InputManager.IsPressed(VK.KEY_Q))
                roll -= Math.PI / 30;
            if (InputManager.IsPressed(VK.KEY_E))
                roll += Math.PI / 30;

            Position = new Vector3(5 * Math.Cos(theta), 5 * Math.Sin(theta), 7);
            RotationX = Quaternion.AxisAngle(new Vector3(1, 0, 0), roll);
            RotationY = Quaternion.AxisAngle(new Vector3(0, 1, 0), pitch);
            RotationZ = Quaternion.AxisAngle(new Vector3(0, 0, 1), yaw);
        }

        public override void Render(WireFrameRenderer renderer)
        {
            // TODO: implement rotation @moyamong
            Matrix4x4 modelMatrix = Matrix4x4.CreateTranslateMatrix(Position);

            Matrix4x4 rotateMatrix = Matrix4x4.CreateRotationMatrix(RotationZ) * (Matrix4x4.CreateRotationMatrix(RotationY) * Matrix4x4.CreateRotationMatrix(RotationX));

            foreach (Triangle triangle in tris)
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
