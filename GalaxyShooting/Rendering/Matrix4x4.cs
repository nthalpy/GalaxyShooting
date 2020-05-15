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
            return new Vector4(
                SumOfElementwiseProduct(m.Row0, v),
                SumOfElementwiseProduct(m.Row1, v),
                SumOfElementwiseProduct(m.Row2, v),
                SumOfElementwiseProduct(m.Row3, v)
            );

            double SumOfElementwiseProduct(Vector4 l, Vector4 r)
            {
                return l.X * r.X + l.Y * r.Y + l.Z * r.Z + l.W * r.W;
            }
        }
    }
}
