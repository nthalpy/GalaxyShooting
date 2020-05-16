namespace GalaxyShooting.Rendering
{
    public struct Matrix4x4
    {
        public Vector4 Row0;
        public Vector4 Row1;
        public Vector4 Row2;
        public Vector4 Row3;

        public Matrix4x4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }

        private static double SumOfElementwiseProduct(Vector4 l, Vector4 r)
        {
            return l.X * r.X + l.Y * r.Y + l.Z * r.Z + l.W * r.W;
        }

        public static Matrix4x4 CreateTranslateMatrix(Vector3 position)
        {
            return new Matrix4x4(
                new Vector4(1, 0, 0, position.X),
                new Vector4(0, 1, 0, position.Y),
                new Vector4(0, 0, 1, position.Z),
                new Vector4(0, 0, 0, 1)
            );
        }

        public static Vector4 operator *(Matrix4x4 m, Vector4 v)
        {
            return new Vector4(
                SumOfElementwiseProduct(m.Row0, v),
                SumOfElementwiseProduct(m.Row1, v),
                SumOfElementwiseProduct(m.Row2, v),
                SumOfElementwiseProduct(m.Row3, v)
            );
        }
    }
}
