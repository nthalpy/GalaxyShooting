using System.Collections.Generic;
using GalaxyShooting.Input;
using GalaxyShooting.Rendering;

namespace GalaxyShooting.Logic
{
    public sealed class GunLauncher
    {

        public List<Bullet> bullets;
        int maxBullet;
        Camera camera;

        public GunLauncher(int MaxBullet, Camera cam)
        {
            maxBullet = MaxBullet;
            bullets = new List<Bullet>();
            for (int i = 0; i < maxBullet; i++)
            {
                /*
                Bullet b;
                if (i % 2 == 0)
                    b = new Bullet(new Vector3(0.2, 0, 0));
                else
                    b = new Bullet(new Vector3(-0.2, 0, 0));
                */
                Bullet b = new Bullet();
                bullets.Add(b);
            }
            camera = cam;
        }

        public void Update()
        {
            if (InputManager.IsPressed(VK.SPACE))
            {
                foreach (Bullet bullet in bullets)
                {
                    if (!bullet.activated)
                    {
                        bullet.start(camera);
                        break;
                    }
                }
            }

            foreach (Bullet bullet in bullets)
            {
                if (bullet.activated)
                {
                    bullet.Update();
                }
            }
        }

        public void Render(WireFrameRenderer renderer)
        {
            foreach (Bullet bullet in bullets)
            {
                if (bullet.activated)
                {
                    bullet.Render(renderer);
                }
            }
        }
    }
}
