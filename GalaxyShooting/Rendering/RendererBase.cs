using System;

namespace GalaxyShooting.Rendering
{
    public abstract class RendererBase
    {
        protected int screenSizeX;
        protected int screenSizeY;

        public RendererBase(int screenSizeX, int screenSizeY)
        {
            this.screenSizeX = screenSizeX;
            this.screenSizeY = screenSizeY;
        }
    }
}
