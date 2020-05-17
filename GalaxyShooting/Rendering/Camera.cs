using GalaxyShooting.Input;
using System;

namespace GalaxyShooting.Rendering
{
    /// <summary>
    /// Perspective camera의 객체
    /// </summary>
    public sealed class Camera
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 direction;

        private Matrix4x4 invRotationMatrix;
        public Matrix4x4 currentRotationMatrix;

        private readonly double aspect;
        private readonly double verticalFOV;
        private readonly double zNear;
        private readonly double zFar;

        double speed = 0.5;

        public Camera(double aspect, double verticalFOV, double zNear, double zFar)
        {
            this.aspect = aspect;
            this.verticalFOV = verticalFOV;
            this.zNear = zNear;
            this.zFar = zFar;

            Position = new Vector3(0, 0, 0);

            invRotationMatrix = Matrix4x4.Identity;
            currentRotationMatrix = Matrix4x4.Identity;
        }

        /// <summary>
        /// World space의 점들을 NDC로 mapping하는 matrix를 생성하는 메서드
        /// </summary>
        public Matrix4x4 GetPerspectiveMatrix()
        {
            double tanHalf = Math.Tan(verticalFOV * Math.PI / 360);
            double zRange = zNear - zFar;

            Matrix4x4 perspectiveMatrix = new Matrix4x4();
            perspectiveMatrix.Row0 = new Vector4(1 / (aspect * tanHalf), 0, 0, 0);
            perspectiveMatrix.Row1 = new Vector4(0, 1 / (tanHalf), 0, 0);
            perspectiveMatrix.Row2 = new Vector4(0, 0, (-zNear - zFar) / zRange, 2 * zFar * zNear / zRange);
            perspectiveMatrix.Row3 = new Vector4(0, 0, 1, 0);

            return perspectiveMatrix;
        }

        public void Update()
        {
            double yaw = 0;
            double pitch = 0;
            double roll = 0;

            if (InputManager.IsPressed(VK.RIGHT))
                pitch += Math.PI / 120;
            if (InputManager.IsPressed(VK.LEFT))
                pitch -= Math.PI / 120;
            if (InputManager.IsPressed(VK.KEY_E))
                yaw -= Math.PI / 120;
            if (InputManager.IsPressed(VK.KEY_Q))
                yaw += Math.PI / 120;
            if (InputManager.IsPressed(VK.UP))
                roll -= Math.PI / 120;
            if (InputManager.IsPressed(VK.DOWN))
                roll += Math.PI / 120;

            Quaternion rotX = Quaternion.AxisAngle(Vector3.Right, roll);
            Quaternion rotY = Quaternion.AxisAngle(Vector3.Up, pitch);
            Quaternion rotZ = Quaternion.AxisAngle(Vector3.Forward, yaw);

            currentRotationMatrix *= Matrix4x4.CreateRotationMatrix(rotZ) * Matrix4x4.CreateRotationMatrix(rotY) * Matrix4x4.CreateRotationMatrix(rotX);
            invRotationMatrix = Matrix4x4.CreateRotationMatrix(-rotX) * Matrix4x4.CreateRotationMatrix(-rotY) * Matrix4x4.CreateRotationMatrix(-rotZ) * invRotationMatrix;

            direction = (currentRotationMatrix * Vector3.Forward.ToXYZ1()).HomogeneousToXYZ();

            if (InputManager.IsPressed(VK.KEY_W))
                Position += direction * speed;
            if (InputManager.IsPressed(VK.KEY_S))
                Position -= direction * speed;
        }

        public Matrix4x4 GetViewMatrix()
        {
            return invRotationMatrix * Matrix4x4.CreateTranslateMatrix(-Position);
        }
    }
}
