using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace GalaxyShooting.Rendering
{
    public sealed class ImageRenderer
    {
        // assume screen is 320-160 sized
        // todo: generalize this

        private Char[,] screenCache;

        public ImageRenderer()
        {
            screenCache = new Char[320, 160];
        }

        public void RenderImage(String path)
        {
            const int screenSizeX = 320;
            const int screenSizeY = 160;
            const int xPerChar = 2;
            const int yPerChar = 4;
            const int BrailleBase = 0x2800;

            using (Image image = Image.FromFile(path))
            {
                Bitmap bitmap = (Bitmap)image;

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

                                if (bitmap.GetPixel(bufferX, bufferY).R > Byte.MaxValue / 2)
                                    chVal += 1 << (3 * dx + dy);
                            }

                            {
                                int bufferY = screenY * yPerChar + 3;
                                if (bufferY >= screenSizeY)
                                    continue;
                                if (bitmap.GetPixel(bufferX, bufferY).R > Byte.MaxValue / 2)
                                    chVal += 1 << (6 + dx);
                            }
                        }

                        Debug.Assert(0x2800 <= chVal && chVal <= 0x28FF);

                        Char oldCh = screenCache[screenX, screenY];
                        Char newCh = (Char)chVal;
                        if (newCh != oldCh)
                        {
                            screenCache[screenX, screenY] = newCh;
                            Console.SetCursorPosition(screenX, screenY);
                            Console.Write(newCh);
                        }
                    }

                    // caret이 screen 아래에 가도록
                    Console.SetCursorPosition(0, screenSizeY / yPerChar + 1);
                }
            }
        }
    }
}
