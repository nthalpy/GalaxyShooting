using System;

namespace GalaxyShooting.Rendering
{
    public struct Quaternion
    {
        public double X;
        public double Y;
        public double Z;
        public double W;

        public Quaternion(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static Quaternion operator-(Quaternion q)
        {
            return new Quaternion(-q.X, -q.Y, -q.Z, q.W);
        }

        public static Quaternion AxisAngle(Vector3 axis, double theta)
        {
            return new Quaternion(Math.Sin(theta / 2) * axis.X, Math.Sin(theta / 2) * axis.Y, Math.Sin(theta / 2) * axis.Z, Math.Cos(theta / 2));
        }
    }
}