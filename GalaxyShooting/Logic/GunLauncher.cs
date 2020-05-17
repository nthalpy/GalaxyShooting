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
        public class Bullet
        {
            Vector3 Position;
            Vector3 direction;
            Vector3 PositionBias;
            double speed = 0.5;

            private int timeSpan;
            private readonly int maxTimeSpan = 40;

            private Quaternion rot;
            private Matrix4x4 RotationMatrix;

            public bool activated;

            public Bullet(Vector3 pos)
            {
                //rot = Quaternion.AxisAngle(direction, 0);
                activated = false;
                RotationMatrix = Matrix4x4.Identity;
                PositionBias = pos;
            }

            public Bullet()
            {
                activated = false;
                RotationMatrix = Matrix4x4.Identity;
            }

            public void start(Camera camera)
            {
                Position = camera.Position;
                direction = camera.direction;
                rot = Quaternion.AxisAngle(direction, Math.PI / 30);
                RotationMatrix = camera.currentRotationMatrix;
                //RotationMatrix = Matrix4x4.Identity;

                timeSpan = 0;
                activated = true;
            }

            public void Update()
            {
                if (!activated)
                    return;

                Position += direction * speed;

                //RotationMatrix *= Matrix4x4.CreateRotationMatrix(rot);

                timeSpan++;
                if (timeSpan >= maxTimeSpan)
                {
                    activated = false;
                }
            }

            public void Render(WireFrameRenderer renderer)
            {
                Matrix4x4 translateMatrix = Matrix4x4.CreateTranslateMatrix(Position+PositionBias);

                foreach (Triangle triangle in BulletModel.Tris)
                {
                    renderer.EnqueueTriangle(new Triangle(
                        (translateMatrix * (RotationMatrix * triangle.A.ToXYZ1())).HomogeneousToXYZ(),
                        (translateMatrix * (RotationMatrix * triangle.B.ToXYZ1())).HomogeneousToXYZ(),
                        (translateMatrix * (RotationMatrix * triangle.C.ToXYZ1())).HomogeneousToXYZ()
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
