using System;
using System.Diagnostics;
using GalaxyShooting.Input;

namespace GalaxyShooting.Rendering
{
    /// <summary>
    /// Perspective camera의 객체
    /// </summary>
    public sealed class Camera
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public Matrix4x4 currentRotationMatrix;

        private readonly double aspect;
        private readonly double verticalFOV;
        private readonly double zNear;
        private readonly double zFar;

        double yaw = 0;
        double pitch = 0;
        double roll = 0;

        double speed = 0.5;

        public Camera(double aspect, double verticalFOV, double zNear, double zFar)
        {
            this.aspect = aspect;
            this.verticalFOV = verticalFOV;
            this.zNear = zNear;
            this.zFar = zFar;

            currentRotationMatrix = new Matrix4x4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1));
        }

        /// <summary>
        /// World space의 점들을 NDC로 mapping하는 matrix를 생성하는 메서드
        /// </summary>
        public Matrix4x4 GetMatrix()
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

            if (InputManager.IsPressed(VK.KEY_U))
                pitch += Math.PI / 30;
            if (InputManager.IsPressed(VK.KEY_O))
                pitch -= Math.PI / 30;
            if (InputManager.IsPressed(VK.KEY_J))
                yaw -= Math.PI / 30;
            if (InputManager.IsPressed(VK.KEY_L))
                yaw += Math.PI / 30;
            if (InputManager.IsPressed(VK.KEY_K))
                roll -= Math.PI / 30;
            if (InputManager.IsPressed(VK.KEY_I))
                roll += Math.PI / 30;

            //Debug.WriteLine(pitch);

            Quaternion rotX = Quaternion.AxisAngle(new Vector3(1, 0, 0), roll);
            Quaternion rotY = Quaternion.AxisAngle(new Vector3(0, 1, 0), pitch);
            Quaternion rotZ = Quaternion.AxisAngle(new Vector3(0, 0, 1), yaw);

            currentRotationMatrix = Matrix4x4.CreateRotationMatrix(rotZ) * Matrix4x4.CreateRotationMatrix(rotY) * Matrix4x4.CreateRotationMatrix(rotX);

            Vector4 direction4 = currentRotationMatrix * new Vector4(0, 0, -1, 1);
            Vector3 direction = new Vector3(direction4.X * speed, direction4.Y * speed, -direction4.Z * speed);

            if (InputManager.IsPressed(VK.KEY_Y))
                Position += direction;
            if (InputManager.IsPressed(VK.KEY_H))
                Position -= direction;

            //currentRotationMatrix *= rotMatrixChange;
        }
    }
}
