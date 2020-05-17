using System;
using System.Drawing;

namespace GalaxyShooting.Rendering
{
    // Note: strongly hard-coded, and coupled w/ font.png
    public sealed class TextRenderer : RendererBase
    {
        const int textHeight = 32;

        private String[] atlasInfo;
        private int[] gap;

        public TextRenderer(int screenSizeX, int screenSizeY)
            : base(screenSizeX, screenSizeY)
        {
            atlasInfo = new string[]
            {
                " !\"#$%&'()*+,-./",
                "0123456789:;<=>?",
                "@ABCDEFGHIJKLMNO",
                "PQRSTUVWXYZ[\\]^_",
                "`abcdefghijklmno",
                "pqrstuvwxyz{|}~",
            };
            gap = new int[]
            {
                4,
                4,
                3,
                4,
                4,
                4,
                4
            };
        }

        public void RenderText(String text, int x, int y)
        {
            const Byte thr = Byte.MaxValue / 2;

            using (Image image = Image.FromFile("Resources/font.png"))
            {
                Bitmap bitmap = (Bitmap)image;

                int xProgression = 0;
                foreach (Char ch in text)
                {
                    int line = 0;
                    int index = 0;

                    for (line = 0; line < atlasInfo.Length; line++)
                        if (atlasInfo[line].Contains(ch.ToString()))
                        {
                            for (index = 0; index < atlasInfo[line].Length; index++)
                                if (atlasInfo[line][index] == ch)
                                    break;

                            break;
                        }

                    int startY = gap[0];
                    for (int idx = 0; idx < line; idx++)
                        startY += gap[idx + 1] + textHeight;

                    int endY = startY + textHeight;

                    int startX = 0;
                    int endX = 0;

                    int count = 0;
                    for (int xTex = 0; ; xTex++)
                    {
                        Color currColor = bitmap.GetPixel(xTex, (startY + endY) / 2);
                        Color nextColor = bitmap.GetPixel(xTex + 1, (startY + endY) / 2);

                        // magenta -> white
                        if (currColor.R > thr && currColor.G < thr && currColor.B > thr
                            && nextColor.R > thr && nextColor.G > thr && nextColor.B > thr)
                        {
                            if (count == index)
                            {
                                startX = xTex + 1;
                                continue;
                            }

                            count++;
                        }
                        // white -> magenta
                        if (currColor.R > thr && currColor.G > thr && currColor.B > thr
                            && nextColor.R > thr && nextColor.G < thr && nextColor.B > thr)
                        {
                            if (startX != 0)
                            {
                                endX = xTex;
                                break;
                            }
                        }
                    }

                    const int compressionRate = 2;
                    const int margin = 2;

                    for (int xTex = margin; startX + compressionRate * xTex <= endX - margin; xTex++)
                    {
                        for (int yTex = 0; startY + compressionRate * yTex <= endY - margin; yTex++)
                        {
                            if (x + xProgression < 0 || x + xProgression >= Screen.ScreenSizeX || y + yTex < 0 || y + yTex >= Screen.ScreenSizeY)
                                continue;

                            if (startY + compressionRate * yTex > image.Height)
                                continue;

                            Color c = bitmap.GetPixel(startX + compressionRate * xTex, startY + compressionRate * yTex);
                            Screen.SetPixel(x + xProgression, y + yTex, c.G < thr && c.R < thr);
                        }
                        xProgression++;
                    }
                }
            }
        }
    }
}
