using GalaxyShooting.Input;
using GalaxyShooting.Rendering;
using System;

namespace GalaxyShooting.Logic
{
    /// <summary>
    /// 렌더링 테스트용 object.
    /// </summary>
    public sealed class RenderTestObject : GameObject
    {
        private Vector3[] vbo;
        private int[] ebo;

        private Triangle[] tris;

        private double theta;

        public Vector3 Position;

        public RenderTestObject()
        {
            vbo = new Vector3[]
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
            ebo = new int[]
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

            tris = new Triangle[ebo.Length / 3];
            for (int idx = 0; idx < ebo.Length / 3; idx++)
            {
                tris[idx] = new Triangle(
                    vbo[ebo[3 * idx]],
                    vbo[ebo[3 * idx + 1]],
                    vbo[ebo[3 * idx + 2]]
                );
            }

            Position = new Vector3(0, 0, 3);
        }

        public override void Update()
        {
            if (InputManager.IsPressed(VK.LEFT))
                theta -= Math.PI / 30;
            else if (InputManager.IsPressed(VK.RIGHT))
                theta += Math.PI / 30;

            Position = new Vector3(0.8 * Math.Cos(theta), 0.8 * Math.Sin(theta), 3);
        }

        public override void Render(WireFrameRenderer renderer)
        {
            // TODO: implement rotation @moyamong
            Matrix4x4 modelMatrix = Matrix4x4.CreateTranslateMatrix(Position);

            foreach (Triangle triangle in tris)
            {
                renderer.EnqueueTriangle(new Triangle(
                    (modelMatrix * triangle.A.ToXYZ1()).HomogeneousToXYZ(),
                    (modelMatrix * triangle.B.ToXYZ1()).HomogeneousToXYZ(),
                    (modelMatrix * triangle.C.ToXYZ1()).HomogeneousToXYZ()
                ));
            }
        }
    }
}
