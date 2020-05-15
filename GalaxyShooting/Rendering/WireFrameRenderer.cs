using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace GalaxyShooting.Rendering
{
    /// <summary>
    /// Rendering을 담당해주는 객체
    /// </summary>
    public sealed class WireFrameRenderer
    {
        private int screenSizeX;
        private int screenSizeY;

        private List<Triangle> triangleList;

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
        }
    }
}
