using GalaxyShooting.Rendering;

namespace GalaxyShooting.Logic
{
    public sealed class GameTitleLoop : GameLoopBase
    {
        private ImageRenderer renderer;

        const int blinkFrameInterval = 50;
        private int frameCount;

        public GameTitleLoop()
        {
            renderer = new ImageRenderer(Screen.ScreenSizeX, Screen.ScreenSizeY);
        }

        public override void Update()
        {
            // TODO: add logic which move loop to in-game loop
        }
        public override void Render()
        {
            if (frameCount % blinkFrameInterval == 0)
                renderer.RenderImage("Resources/title.png");
            else if (frameCount % blinkFrameInterval == blinkFrameInterval / 2)
                renderer.RenderImage("Resources/title2.png");

            frameCount++;
        }
    }
}
