using GalaxyShooting.Rendering;
using System.Collections.Generic;

namespace GalaxyShooting.Model
{
    public static class TestPlane
    {
        public static readonly List<Triangle> Tris;

        static TestPlane()
        {
            Tris = new List<Triangle>();

            AddTris(
                new Vector3[]
                {
                    new Vector3(0, 0, 4),

                    new Vector3(1, 1, 2),
                    new Vector3(1.5, 0, 2),
                    new Vector3(-1.5, 0, 2),
                    new Vector3(-1, 1, 2),

                    new Vector3(1.2, 1.5, -4),
                    new Vector3(1.6, 0, -4),
                    new Vector3(-1.6, 0, -4),
                    new Vector3(-1.2, 1.5, -4),
                },
                new int[]
                {
                    // cockpit
                    0, 1, 2,
                    0, 2, 3,
                    0, 3, 4,
                    0, 4, 1,

                    // body
                    1, 5, 2,
                    5, 2, 6,
                    2, 6, 3,
                    6, 3, 7,
                    3, 7, 4,
                    7, 4, 8,
                    4, 8, 1,
                    8, 1, 5,

                    // end of body
                    5, 6, 8,
                    6, 8, 7,
                });
        }

        private static void AddTris(Vector3[] vbo, int[] ebo)
        {
            for (int idx = 0; idx < ebo.Length / 3; idx++)
            {
                Tris.Add(new Triangle(
                    vbo[ebo[3 * idx]],
                    vbo[ebo[3 * idx + 1]],
                    vbo[ebo[3 * idx + 2]]
                ));
            }
        }
    }
}
