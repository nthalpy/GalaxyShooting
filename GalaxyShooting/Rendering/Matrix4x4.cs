namespace GalaxyShooting.Rendering
{
    public struct Matrix4x4
    {
        public static Matrix4x4 Identity = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1)
        );

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

        public static Matrix4x4 CreateSizeTransformMatrix(double x, double y, double z)
        {
            return new Matrix4x4(
                new Vector4(x, 0, 0, 0),
                new Vector4(0, y, 0, 0),
                new Vector4(0, 0, z, 0),
                new Vector4(0, 0, 0, 1)
            );
        }

        public static Matrix4x4 CreateRotationMatrix(Quaternion q) //rotation으로 쓰고 싶었지만 너무 길어서
        {
            return new Matrix4x4(
                new Vector4(1 - 2 * q.Y * q.Y - 2 * q.Z * q.Z, 2 * q.X * q.Y - 2 * q.Z * q.W, 2 * q.X * q.Z + 2 * q.Y * q.W, 0),
                new Vector4(2 * q.X * q.Y + 2 * q.Z * q.W, 1 - 2 * q.X * q.X - 2 * q.Z * q.Z, 2 * q.Y * q.Z - 2 * q.X * q.W, 0),
                new Vector4(2 * q.X * q.Z - 2 * q.Y * q.W, 2 * q.Y * q.Z + 2 * q.X * q.W, 1 - 2 * q.X * q.X - 2 * q.Y * q.Y, 0),
                new Vector4(0, 0, 0, 1)
            );
        }

        public static Matrix4x4 operator *(Matrix4x4 m, Matrix4x4 n)
        {
            Vector4 col0 = new Vector4(n.Row0.X, n.Row1.X, n.Row2.X, n.Row3.X);
            Vector4 col1 = new Vector4(n.Row0.Y, n.Row1.Y, n.Row2.Y, n.Row3.Y);
            Vector4 col2 = new Vector4(n.Row0.Z, n.Row1.Z, n.Row2.Z, n.Row3.Z);
            Vector4 col3 = new Vector4(n.Row0.W, n.Row1.W, n.Row2.W, n.Row3.W);
            return new Matrix4x4(
                new Vector4(SumOfElementwiseProduct(m.Row0, col0), SumOfElementwiseProduct(m.Row0, col1), SumOfElementwiseProduct(m.Row0, col2), SumOfElementwiseProduct(m.Row0, col3)),
                new Vector4(SumOfElementwiseProduct(m.Row1, col0), SumOfElementwiseProduct(m.Row1, col1), SumOfElementwiseProduct(m.Row1, col2), SumOfElementwiseProduct(m.Row1, col3)),
                new Vector4(SumOfElementwiseProduct(m.Row2, col0), SumOfElementwiseProduct(m.Row2, col1), SumOfElementwiseProduct(m.Row2, col2), SumOfElementwiseProduct(m.Row2, col3)),
                new Vector4(SumOfElementwiseProduct(m.Row3, col0), SumOfElementwiseProduct(m.Row3, col1), SumOfElementwiseProduct(m.Row3, col2), SumOfElementwiseProduct(m.Row3, col3))
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
