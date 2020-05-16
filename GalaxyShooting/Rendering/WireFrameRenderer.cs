﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GalaxyShooting.Rendering
{
    /// <summary>
    /// Rendering을 담당해주는 객체
    /// </summary>
    public sealed class WireFrameRenderer
    {
        private readonly int screenSizeX;
        private readonly int screenSizeY;

        private readonly List<Triangle> triangleList;

        private PixelBuffer foregroundBuffer;
        private PixelBuffer backgroundBuffer;
        private Char[,] screenCache;

        public WireFrameRenderer(int screenSizeX, int screenSizeY)
        {
            this.screenSizeX = screenSizeX;
            this.screenSizeY = screenSizeY;

            foregroundBuffer = new PixelBuffer(screenSizeX, screenSizeY);
            backgroundBuffer = new PixelBuffer(screenSizeX, screenSizeY);
            screenCache = new Char[screenSizeX, screenSizeY];

            triangleList = new List<Triangle>();

            Console.SetWindowSize(screenSizeX / 2 + 2, screenSizeY / 4 + 2);
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
            Matrix4x4 mvp = cam.GetMatrix();

            foreach (Triangle triangle in triangleList)
            {
                Vector3 a = (mvp * new Vector4(triangle.A, 1)).HomogeneousToXYZ();
                Vector3 b = (mvp * new Vector4(triangle.B, 1)).HomogeneousToXYZ();
                Vector3 c = (mvp * new Vector4(triangle.C, 1)).HomogeneousToXYZ();

                RenderTriangleToBuffer(a, b, c);
            }

            triangleList.Clear();
        }

        private void RenderTriangleToBuffer(Vector3 a, Vector3 b, Vector3 c)
        {
            int x1 = (int)Math.Round(a.X * screenSizeX / 2 + screenSizeX / 2);
            int x2 = (int)Math.Round(b.X * screenSizeX / 2 + screenSizeX / 2);
            int x3 = (int)Math.Round(c.X * screenSizeX / 2 + screenSizeX / 2);
            int y1 = (int)Math.Round(a.Y * screenSizeY / 2 + screenSizeY / 2);
            int y2 = (int)Math.Round(b.Y * screenSizeY / 2 + screenSizeY / 2);
            int y3 = (int)Math.Round(b.Y * screenSizeY / 2 + screenSizeY / 2);

            double depth1 = a.Z;
            double depth2 = b.Z;
            double depth3 = c.Z;

            int xDiff = Math.Max(x2, x3) - Math.Min(x2, x3);
            int yDiff = Math.Max(y2, y3) - Math.Min(y2, y3);

            int r = Math.Max(xDiff, yDiff);

            double x0 = x2;
            double y0 = y2;

            double depth0 = depth2;

            int x = 0;
            int y = 0;

            for (int i = 0; i < r; i++)
            { //inner space
                if (xDiff != 0)
                    x0 += (double)(x3 - x2) / r;

                if (yDiff != 0)
                    y0 += (double)(y3 - y2) / r;

                depth0 += (depth3 - depth2) / r;

                x = (int)Math.Round(x0);
                y = (int)Math.Round(y0);

                int xBlankDiff = Math.Max(x1, x) - Math.Min(x1, x);
                int yBlankDiff = Math.Max(y1, y) - Math.Min(y1, y);

                int rBlank = Math.Max(xBlankDiff, yBlankDiff);

                double x4 = x1;
                double y4 = y1;

                double depth4 = depth1;

                int xBlank = 0;
                int yBlank = 0;

                for (int j = 0; j < rBlank; j++)
                {
                    if (xBlankDiff != 0)
                        x4 += (double)(x - x1) / rBlank;

                    if (yDiff != 0)
                        y4 += (double)(y - y1) / rBlank;

                    depth4 += (depth0 - depth1) / rBlank;

                    xBlank = (int)Math.Round(x4);
                    yBlank = (int)Math.Round(y4);

                    if (xBlank < 0 || xBlank >= screenSizeX || yBlank < 0 || yBlank >= screenSizeY)
                        continue;

                    backgroundBuffer.SetPixel(x, y, ConsoleColor.Black, depth4);
                }
            }
            RenderLine(a, b);
            RenderLine(b, c);
            RenderLine(a, c);
        }

        private void RenderLine(Vector3 a, Vector3 b)
        {
            //I ignored z value
            //@moyamong
            int x1 = (int)Math.Round(a.X * screenSizeX / 2 + screenSizeX / 2);
            int x2 = (int)Math.Round(b.X * screenSizeX / 2 + screenSizeX / 2);
            int y1 = (int)Math.Round(a.Y * screenSizeY / 2 + screenSizeY / 2);
            int y2 = (int)Math.Round(b.Y * screenSizeY / 2 + screenSizeY / 2);

            double depth1 = a.Z;
            double depth2 = b.Z;

            int xDiff = Math.Max(x1, x2) - Math.Min(x1, x2);
            int yDiff = Math.Max(y1, y2) - Math.Min(y1, y2);

            int r = Math.Max(xDiff, yDiff);

            double x0 = x1;
            double y0 = y1;

            double depth0 = depth1;

            int x = 0;
            int y = 0;

            for (int i = 0; i < r; i++)
            {
                if (xDiff != 0)
                    x0 += (double)(x2 - x1) / r;

                if (yDiff != 0)
                    y0 += (double)(y2 - y1) / r;

                depth0 += (depth2 - depth1) / r;

                x = (int)Math.Round(x0);
                y = (int)Math.Round(y0);

                if (x < 0 || x >= screenSizeX || y < 0 || y >= screenSizeY)
                    continue;

                backgroundBuffer.SetPixel(x, y, ConsoleColor.White, depth0);
            }
        }

        public void ClearBuffer()
        {
            backgroundBuffer.ClearBuffer();
        }
        public void SwapBuffer()
        {
            PixelBuffer pixelBuffer = foregroundBuffer;
            foregroundBuffer = backgroundBuffer;
            backgroundBuffer = pixelBuffer;

            UpdateScreen();
        }

        /// <summary>
        /// foreground의 내용물을 stdout에 update.
        /// </summary>
        private void UpdateScreen()
        {
            const int xPerChar = 2;
            const int yPerChar = 4;
            const int BrailleBase = 0x2800;

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

                            if (foregroundBuffer.GetPixel(bufferX, bufferY) != ConsoleColor.Black)
                                chVal += 1 << (3 * dx + dy);
                        }

                        {
                            int bufferY = screenY * yPerChar + 3;
                            if (bufferY >= screenSizeY)
                                continue;

                            if (foregroundBuffer.GetPixel(bufferX, bufferY) != ConsoleColor.Black)
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
            Console.SetCursorPosition(0, screenSizeY / yPerChar + 1);
        }
    }
}
