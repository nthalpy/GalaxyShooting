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
        private readonly double zNear;
        private readonly double zFar;

        public Camera(double aspect, double zNear, double zFar)
        {
            this.aspect = aspect;
            this.zNear = zNear;
            this.zFar = zFar;
        }

        /// <summary>
        /// World space의 점들을 NDC로 mapping하는 matrix를 생성하는 메서드
        /// </summary>
        public Matrix4x4 GetMatrix()
        {
            // TODO: Implement this @moyamong
            throw new NotImplementedException();
        }
    }
}
