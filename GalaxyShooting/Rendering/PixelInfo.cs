using System;
using System.Diagnostics;

namespace GalaxyShooting.Rendering
{
    [DebuggerDisplay("{Color}")]
    public sealed class PixelInfo
    {
        public ConsoleColor Color;

        public void Clear()
        {
            Color = ConsoleColor.Black;
        }
    }
}