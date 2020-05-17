using GalaxyShooting.Rendering;
using GalaxyShooting.Input;
using System;

namespace GalaxyShooting.Logic
{
    public sealed class GameTitleLoop : GameLoopBase
    {
        private ImageRenderer renderer;

        const int blinkFrameInterval = 50;
        private int frameCount;

        private bool end;

        public GameTitleLoop()
        {
            renderer = new ImageRenderer(Screen.ScreenSizeX, Screen.ScreenSizeY);
            end = false;
        }

        public override void Update()
        {
            if (InputManager.IsPressed(VK.KEY_G))
            {
                end = true;
            }
        }
        public override void Render()
        {
            if (frameCount % blinkFrameInterval == 0)
                renderer.RenderImage("Resources/title.png");
            else if (frameCount % blinkFrameInterval == blinkFrameInterval / 2)
                renderer.RenderImage("Resources/title2.png");

            frameCount++;
        }

        //private void KeyDown(object sender, keypre)

        public override bool End()
        {
            return end;
        }
    }
}
