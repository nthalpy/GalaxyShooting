namespace GalaxyShooting.Rendering
{
    public sealed class CrosshairRenderer : RendererBase
    {
        public CrosshairRenderer(int screenSizeX, int screenSizeY)
            : base(screenSizeX, screenSizeY)
        {
        }

        public void Render()
        {
            const int sizeX = 20;
            const int sizeY = 20;
            const int width = 2;

            int dx = 0;
            int dy = 0;

            for (dy = 0; dy < width; dy++)
                for (dx = 0; dx < sizeX; dx++)
                {
                    if (dx > sizeX / 4 && dx < sizeX * 3 / 4)
                        continue;
                    Screen.SetPixel(screenSizeX / 2 - sizeX / 2 + dx, screenSizeY / 2 - width / 2 + dy, true);
                }

            for (dx = 0; dx < width; dx++)
                for (dy = 0; dy < sizeY; dy++)
                {
                    if (dy > sizeY / 4 && dy < sizeY * 3 / 4)
                        continue;
                    Screen.SetPixel(screenSizeX / 2 - width / 2 + dx, screenSizeY / 2 - sizeY / 2 + dy, true);
                }
        }
    }
}
