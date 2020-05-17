using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using GalaxyShooting.Input;
using GalaxyShooting.Model;
using GalaxyShooting.Rendering;

namespace GalaxyShooting.Logic
{
    public sealed class GunLauncher : GameObjectBase
    {

        public class Bullet
        {
            Vector3 Position;
            Vector3 direction;
            double speed;

            private int timeSpan;
            private readonly int maxTimeSpan = 100;

            private Quaternion rot;
            private Matrix4x4 RotationMatrix;

            public bool activated;

            public Bullet()
            {
                //rot = Quaternion.AxisAngle(direction, 0);
                activated = false;
            }

            public void start(Camera camera)
            {
                Position = camera.Position;
                direction = camera.direction;
                rot = Quaternion.AxisAngle(direction, 0);
                RotationMatrix = Matrix4x4.CreateRotationMatrix(rot);
                timeSpan = 0;
                activated = true;
            }

            public void Update()
            {
                if (!activated)
                    return;

                Position += direction * speed;

                timeSpan++;
                if (timeSpan >= maxTimeSpan)
                {
                    activated = false;
                }
            }

            public void Render(WireFrameRenderer renderer)
            {
                Matrix4x4 modelMatrix = Matrix4x4.CreateTranslateMatrix(Position);

                foreach (Triangle triangle in BulletModel.Tris)
                {
                    renderer.EnqueueTriangle(new Triangle(
                        (modelMatrix * (RotationMatrix * triangle.A.ToXYZ1())).HomogeneousToXYZ(),
                        (modelMatrix * (RotationMatrix * triangle.A.ToXYZ1())).HomogeneousToXYZ(),
                        (modelMatrix * (RotationMatrix * triangle.A.ToXYZ1())).HomogeneousToXYZ()
                    ));
                }
            }
        }

        List<Bullet> bullets;
        int maxBullet;
        Camera camera;

        public GunLauncher(int MaxBullet, Camera cam)
        {
            maxBullet = MaxBullet;
            bullets = new List<Bullet>();
            for (int i = 0; i < maxBullet; i++)
            {
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
