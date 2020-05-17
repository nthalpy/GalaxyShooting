using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using GalaxyShooting.Input;
using GalaxyShooting.Model;
using GalaxyShooting.Rendering;

namespace GalaxyShooting.Logic
{
    public sealed class GunLauncher : GameObjectBase
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

        public override void Update()
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

        public override bool Collision(Bullet bullet)
        {
            return false;
        }

        public override void Render(WireFrameRenderer renderer)
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
