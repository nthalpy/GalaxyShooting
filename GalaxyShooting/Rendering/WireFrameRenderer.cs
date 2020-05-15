using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GalaxyShooting.Rendering
{
    /// <summary>
    /// Rendering을 담당해주는 객체
    /// </summary>
    public sealed class WireFrameRenderer
    {
        private readonly int screenSizeX;
        private readonly int screenSizeY;

        private readonly List<Triangle> triangleList;

        private PixelBuffer foregroundBuffer;
        private PixelBuffer backgroundBuffer;

        public WireFrameRenderer(int screenSizeX, int screenSizeY)
        {
            this.screenSizeX = screenSizeX;
            this.screenSizeY = screenSizeY;

            foregroundBuffer = new PixelBuffer(screenSizeX, screenSizeY);
            backgroundBuffer = new PixelBuffer(screenSizeX, screenSizeY);

            triangleList = new List<Triangle>();
        }

        public void EnqueueTriangle(Triangle triangle)
        {
            triangleList.Add(triangle);
        }

        /// <summary>
        /// Enqueue 된 triangle들의 world 좌표를 camera의 matrix를 통해서 NDC에 mapping
        /// mapping한 것을 pixel buffer에 write
        /// </summary>
        public void RenderToBuffer(Camera cam)
        {
            // temporary matrix
            Matrix4x4 mvp = new Matrix4x4
            {
                Row0 = new Vector4(1, 0, 0, 0),
                Row1 = new Vector4(0, 1, 0, 0),
                Row2 = new Vector4(0, 0, 1, 0),
                Row3 = new Vector4(0, 0, 0, 1),
            };

            foreach (Triangle triangle in triangleList)
            {
                Vector3 a = (mvp * new Vector4(triangle.A, 1)).HomogeneousToXYZ();
                Vector3 b = (mvp * new Vector4(triangle.B, 1)).HomogeneousToXYZ();
                Vector3 c = (mvp * new Vector4(triangle.C, 1)).HomogeneousToXYZ();

                RenderTriangleToBuffer(a, b, c);
            }
        }

        private void RenderTriangleToBuffer(Vector3 a, Vector3 b, Vector3 c)
        {
            // TODO: 
            // render three line (a-b, b-c, a-c)
        }

        public void ClearBuffer()
        {
            foregroundBuffer.ClearBuffer();
        }
        public void SwapBuffer()
        {
            PixelBuffer tmp = foregroundBuffer;
            foregroundBuffer = backgroundBuffer;
            backgroundBuffer = tmp;

            UpdateScreen();
        }

        /// <summary>
        /// foreground의 내용물을 stdout에 update.
        /// </summary>
        private void UpdateScreen()
        {
            const int xPerChar = 2;
            const int yPerChar = 4;
            const int BrailleBase = 0x2800;

            Console.Clear();

            for (int screenY = 0; screenY * yPerChar < screenSizeY; screenY++)
            {
                for (int screenX = 0; screenX * xPerChar < screenSizeX; screenX++)
                {
                    // braille:
                    // 0 3
                    // 1 4
                    // 2 5
                    // 6 7

                    int chVal = BrailleBase;

                    for (int dx = 0; dx < xPerChar; dx++)
                    {
                        int bufferX = screenX * xPerChar + dx;
                        if (bufferX >= screenSizeX)
                            break;

                        for (int dy = 0; dy < yPerChar - 1; dy++)
                        {
                            int bufferY = screenY * yPerChar + dy;
                            if (bufferY >= screenSizeY)
                                break;

                            if (foregroundBuffer.GetPixel(bufferX, bufferY) != ConsoleColor.Black)
                                chVal += 1 << (3 * dx + dy);
                        }

                        {
                            int bufferY = screenY * yPerChar + 3;
                            if (bufferY >= screenSizeY)
                                continue;

                            if (foregroundBuffer.GetPixel(bufferX, bufferY) != ConsoleColor.Black)
                                chVal += 1 << (6 + dx);
                        }
                    }

                    // TODO:
                    // write this to buffer, and update console prompt only pixel which needs.
                    Debug.Assert(0x2800 <= chVal && chVal <= 0x28FF);
                    Console.Write((Char)chVal);
                }
                Console.WriteLine();
            }
        }
    }
}
