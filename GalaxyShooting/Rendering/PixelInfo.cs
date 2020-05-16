using System;
using System.Diagnostics;

namespace GalaxyShooting.Rendering
{
    [DebuggerDisplay("{Color}")]
    public sealed class PixelInfo
    {
        public ConsoleColor Color;
        public double Depth;

        public void Clear()
        {
            Color = ConsoleColor.Black;
            Depth = 1;
        }
    }
}