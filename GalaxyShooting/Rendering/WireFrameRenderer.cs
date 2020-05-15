using System;

namespace GalaxyShooting.Rendering
{
    /// <summary>
    /// Rendering을 담당해주는 객체
    /// </summary>
    public sealed class WireFrameRenderer
    {
        private int screenSizeX;
        private int screenSizeY;

        public WireFrameRenderer(int screenSizeX, int screenSizeY)
        {
            this.screenSizeX = screenSizeX;
            this.screenSizeY = screenSizeY;
        }

        public void EnqueueTriangle(Triangle triangle)
        {

        }

        /// <summary>
        /// Enqueue 된 triangle들을 buffer에 render.
        /// </summary>
        public void RenderToBuffer(Camera cam)
        {
            // Flow:
            // 1. World 좌표를 camera의 matrix를 통해서 NDC에 mapping
            // 2. NDC에 mapping된 것을 braille을 통해서 buffer에 render

            throw new NotImplementedException();
        }

        public void ClearBuffer()
        {

        }
        public void SwapBuffer()
        {

        }
    }
}
