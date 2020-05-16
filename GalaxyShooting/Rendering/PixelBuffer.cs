using System;
using System.Diagnostics;

namespace GalaxyShooting.Rendering
{
    public sealed class PixelBuffer
    {
        [DebuggerDisplay("{Color}")]
        private sealed class PixelInfo
        {
            public ConsoleColor Color;
            public double Depth;

            public void Clear()
            {
                Color = ConsoleColor.Black;
                Depth = 1;
            }
        }

        private readonly int screenSizeX;
        private readonly int screenSizeY;

        // Memo:
        // nested array is a *bit* slower than array of array
        // refactor this to array of array if too slow..
        // @Harnel
        private readonly PixelInfo[,] buffer;

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
                {
                    buffer[x, y].Clear();
                }
        }

        public void SetPixel(int x, int y, ConsoleColor color, double depth)
        {
            if (depth > 1 || depth <= -1 || buffer[x, y].Depth < depth)
                return;

            buffer[x, y].Color = color;
            buffer[x, y].Depth = depth;
        }
        public ConsoleColor GetPixel(int x, int y)
        {
            return buffer[x, y].Color;
        }
    }
}
