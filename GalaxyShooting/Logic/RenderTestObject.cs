using GalaxyShooting.Input;
using GalaxyShooting.Model;
using GalaxyShooting.Rendering;
using System;
using System.Diagnostics;

namespace GalaxyShooting.Logic
{
    /// <summary>
    /// 렌더링 테스트용 object.
    /// </summary>
    public sealed class RenderTestObject : GameObjectBase
    {
        public Vector3 Position;
        public Quaternion RotationX;
        public Quaternion RotationY;
        public Quaternion RotationZ;

        private Matrix4x4 currentRotationMatrix;

        private Quaternion rot;

        public RenderTestObject()
        {
            currentRotationMatrix = new Matrix4x4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1));

            Random rd = new Random();

            Vector3 axis = Vector3.Normalize(new Vector3(rd.NextDouble() - 0.5, rd.NextDouble() - 0.5, rd.NextDouble() - 0.5));
            rot = Quaternion.AxisAngle(axis, Math.PI / 30);
        }

        public override void Update()
        {
            double yaw = 0;
            double pitch = 0;
            double roll = 0;

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

            Quaternion rotX = Quaternion.AxisAngle(new Vector3(1, 0, 0), roll);
            Quaternion rotY = Quaternion.AxisAngle(new Vector3(0, 1, 0), pitch);
            Quaternion rotZ = Quaternion.AxisAngle(new Vector3(0, 0, 1), yaw);

            Matrix4x4 rotMatrixChange = Matrix4x4.CreateRotationMatrix(rotX) * Matrix4x4.CreateRotationMatrix(rotY) * Matrix4x4.CreateRotationMatrix(rotZ);
            currentRotationMatrix *= rotMatrixChange;

            currentRotationMatrix *= Matrix4x4.CreateRotationMatrix(rot);
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
