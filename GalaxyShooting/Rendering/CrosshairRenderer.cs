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
            const int width = 3;

            for (int dy = 0; dy < width; dy++)
                for (int dx = 0; dx < sizeX; dx++)
                    Screen.SetPixel(screenSizeX / 2 - sizeX / 2 + dx, screenSizeY / 2 - width / 2 + dy, true);

            for (int dx = 0; dx < width; dx++)
                for (int dy = 0; dy < sizeY; dy++)
                    Screen.SetPixel(screenSizeX / 2 - width / 2 + dx, screenSizeY / 2 - sizeY / 2 + dy, true);
        }
    }
}
