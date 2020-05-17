using System;
using System.Diagnostics;

namespace GalaxyShooting.Rendering
{
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    public struct Vector3
    {
        public static Vector3 Right = new Vector3(1, 0, 0);
        public static Vector3 Left = new Vector3(-1, 0, 0);
        public static Vector3 Up = new Vector3(0, 1, 0);
        public static Vector3 Down = new Vector3(0, -1, 0);
        public static Vector3 Forward = new Vector3(0, 0, 1);
        public static Vector3 Backward = new Vector3(0, 0, -1);

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

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public static Vector3 Normalize(Vector3 vec)
        {
            double size = vec.Length();
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
        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(
                lhs.X - rhs.X,
                lhs.Y - rhs.Y,
                lhs.Z - rhs.Z);
        }
        public static Vector3 operator *(Vector3 vector, double coeff)
        {
            return new Vector3(
                vector.X * coeff,
                vector.Y * coeff,
                vector.Z * coeff);
        }
    }
}
