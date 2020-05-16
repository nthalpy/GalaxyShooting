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

        private readonly double aspect;
        private readonly double verticalFOV;
        private readonly double zNear;
        private readonly double zFar;

        public Camera(double aspect, double verticalFOV, double zNear, double zFar)
        {
            this.aspect = aspect;
            this.verticalFOV = verticalFOV;
            this.zNear = zNear;
            this.zFar = zFar;
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

    }
}
