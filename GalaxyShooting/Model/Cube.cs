using GalaxyShooting.Rendering;

namespace GalaxyShooting.Model
{
    public static class Cube
    {
        public static readonly Triangle[] Tris;

        static Cube()
        {
            Vector3[] vbo = new Vector3[]
            {
                new Vector3(-1, -1, -1),
                new Vector3(-1, -1, 1),
                new Vector3(-1, 1, -1),
                new Vector3(-1, 1, 1),
                new Vector3(1, -1, -1),
                new Vector3(1, -1, 1),
                new Vector3(1, 1, -1),
                new Vector3(1, 1, 1),
            };
            int[] ebo = new int[]
            {
                0, 6, 4,
                0, 2, 6,
                0, 3, 2,
                0, 1, 3,
                2, 7, 6,
                2, 3, 7,
                4, 6, 7,
                4, 7, 5,
                0, 4, 5,
                0, 5, 1,
                1, 5, 7,
                1, 7, 3
            };

            Tris = new Triangle[ebo.Length / 3];
            for (int idx = 0; idx < ebo.Length / 3; idx++)
            {
                Tris[idx] = new Triangle(
                    vbo[ebo[3 * idx]],
                    vbo[ebo[3 * idx + 1]],
                    vbo[ebo[3 * idx + 2]]
                );
            }
        }
    }
}
