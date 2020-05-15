namespace GalaxyShooting.Rendering
{
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

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(
                lhs.X + rhs.X,
                lhs.Y + rhs.Y,
                lhs.Z + rhs.Z);
        }
    }
}
