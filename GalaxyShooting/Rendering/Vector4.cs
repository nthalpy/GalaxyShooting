using System;

namespace GalaxyShooting.Rendering
{
    public struct Vector4
    {
        public double X;
        public double Y;
        public double Z;
        public double W;

        public Vector4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Vector4(Vector3 xyz, double w)
        {
            X = xyz.X;
            Y = xyz.Y;
            Z = xyz.Z;
            W = w;
        }

        public Vector3 HomogeneousToXYZ()
        {
            return new Vector3(X / W, Y / W, Z / W);
        }

        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(
                lhs.X + rhs.X,
                lhs.Y + rhs.Y,
                lhs.Z + rhs.Z,
                lhs.W + rhs.W);
        }

    }
}
