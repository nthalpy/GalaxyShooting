using System;
using System.Drawing;

namespace GalaxyShooting.Rendering
{
    public sealed class ImageRenderer : RendererBase
    {
        public ImageRenderer(int screenSizeX, int screenSizeY)
            : base(screenSizeX, screenSizeY)
        {
        }

        public void RenderImage(String path)
        {
            using (Image image = Image.FromFile(path))
            {
                Bitmap bitmap = (Bitmap)image;

                for (int y = 0; y < bitmap.Height; y++)
                    for (int x = 0; x < bitmap.Width; x++)
                        Screen.SetPixel(x, y, bitmap.GetPixel(x, y).R > Byte.MaxValue / 2);
            }
        }
    }
}
