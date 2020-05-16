using System;

namespace GalaxyShooting.Rendering
{
    public sealed class ScreenBuffer
    {
        private int screenSizeX;
        private int screenSizeY;

        private Char[,] buffer;

        public ScreenBuffer(int screenSizeX, int screenSizeY)
        {
            this.screenSizeX = screenSizeX;
            this.screenSizeY = screenSizeY;

            buffer = new Char[screenSizeX, screenSizeY];

            ClearBuffer();
        }

        public void ClearBuffer()
        {
            for (int y = 0; y < screenSizeY; y++)
                for (int x = 0; x < screenSizeX; x++)
                    buffer[x, y] = (Char)0x2800;
        }

        public void SetPixel(int screenX, int screenY, Char ch)
        {
            buffer[screenX, screenY] = ch;
        }
        public Char GetPixel(int screenX, int screenY)
        {
            return buffer[screenX, screenY];
        }
    }
}