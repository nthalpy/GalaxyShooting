using System;
using System.Diagnostics;

namespace GalaxyShooting.Rendering
{
    public static class Screen
    {
        public const int BrailleBase = 0x2800;
        public const int ScreenSizeX = 320;
        public const int ScreenSizeY = 160;

        private const int xPerChar = 2;
        private const int yPerChar = 4;

        private static bool[,] pixelInfo;
        private static Char[,] screenCache;

        public static void Initialize()
        {
            pixelInfo = new bool[ScreenSizeX, ScreenSizeY];
            screenCache = new char[ScreenSizeX / xPerChar + 2, ScreenSizeY / yPerChar + 2];

            Console.SetWindowSize(ScreenSizeX / xPerChar + 2, ScreenSizeY / yPerChar + 2);
        }

        public static void Clean()
        {
            for (int y = 0; y < ScreenSizeY; y++)
                for (int x = 0; x < ScreenSizeX; x++)
                    pixelInfo[x, y] = false;
        }

        public static void SetPixel(int x, int y, bool val)
        {
            pixelInfo[x, y] = val;
        }

        public static void Flush()
        {
            // braille:
            // 0 3
            // 1 4
            // 2 5
            // 6 7

            for (int screenY = 0; screenY * yPerChar < ScreenSizeY; screenY++)
            {
                for (int screenX = 0; screenX * xPerChar < ScreenSizeX; screenX++)
                {
                    int chVal = BrailleBase;
                    for (int dx = 0; dx < xPerChar; dx++)
                    {
                        int bufferX = screenX * xPerChar + dx;
                        if (bufferX >= ScreenSizeX)
                            break;

                        for (int dy = 0; dy < yPerChar - 1; dy++)
                        {
                            int bufferY = screenY * yPerChar + dy;
                            if (bufferY >= ScreenSizeY)
                                break;

                            if (pixelInfo[bufferX, bufferY])
                                chVal += 1 << (3 * dx + dy);
                        }

                        {
                            int bufferY = screenY * yPerChar + 3;
                            if (bufferY >= ScreenSizeY)
                                continue;
                            if (pixelInfo[bufferX, bufferY])
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
            }

            // caret이 screen 아래에 가도록
            Console.SetCursorPosition(0, ScreenSizeY / yPerChar + 1);
        }
    }
}
