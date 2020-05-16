using System.Diagnostics;
using System;

namespace GalaxyShooting.Rendering
{
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    public struct Vector3
    {
        public double X;
        public double Y;
        public double Z;

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector4 ToXYZ1()
        {
            return new Vector4(this, 1);
        }

        public static Vector3 Normalize(Vector3 vec)
        {
            double size = Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y + vec.Z * vec.Z);
            return new Vector3(vec.X / size, vec.Y / size, vec.Z / size);
        }

        public static Vector3 operator -(Vector3 lhs)
        {
            return new Vector3(-lhs.X, -lhs.Y, -lhs.Z);
        }
        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(
                lhs.X + rhs.X,
                lhs.Y + rhs.Y,
                lhs.Z + rhs.Z);
        }
    }
}
