using System;

namespace GalaxyShooting.Rendering
{
    public struct Matrix4x4
    {
        public Vector4 Row0;
        public Vector4 Row1;
        public Vector4 Row2;
        public Vector4 Row3;

        public static Vector4 operator *(Matrix4x4 m, Vector4 v)
        {
            throw new NotImplementedException();
        }
    }
}
