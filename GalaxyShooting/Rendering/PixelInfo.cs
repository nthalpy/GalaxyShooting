using System;

namespace GalaxyShooting.Rendering
{
    public sealed class PixelInfo
    {
        public ConsoleColor Color;

        public void Clear()
        {
            Color = ConsoleColor.Black;
        }
    }
}