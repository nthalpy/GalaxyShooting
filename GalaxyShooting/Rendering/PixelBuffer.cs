using System;

namespace GalaxyShooting.Rendering
{
    public sealed class PixelBuffer
    {
        private int screenSizeX;
        private int screenSizeY;

        // Memo:
        // nested array is a *bit* slower than array of array
        // refactor this to array of array if too slow..
        // @Harnel
        private PixelInfo[,] buffer;

        public PixelBuffer(int screenSizeX, int screenSizeY)
        {
            this.screenSizeX = screenSizeX;
            this.screenSizeY = screenSizeY;

            buffer = new PixelInfo[screenSizeX, screenSizeY];

            for (int y = 0; y < screenSizeY; y++)
                for (int x = 0; x < screenSizeX; x++)
                    buffer[x, y] = new PixelInfo();

            ClearBuffer();
        }

        public void ClearBuffer()
        {
            for (int y = 0; y < screenSizeY; y++)
                for (int x = 0; x < screenSizeX; x++)
                    buffer[x, y].Clear();
        }

        public void SetPixel(int x, int y, ConsoleColor color, double depth)
        {
            // TODO: do depth testing
        }
        public ConsoleColor GetPixel(int x, int y)
        {
            return buffer[x, y].Color;
        }
    }
}
