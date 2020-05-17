using System;
using GalaxyShooting.Rendering;
using GalaxyShooting.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyShooting.Logic
{
    public class Bullet
    {
        public Vector3 Position;
        Vector3 direction;
        Vector3 PositionBias;
        double speed = 0.8;

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
            direction = camera.direction;
            Position = camera.Position + direction * 0.5;
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
            Matrix4x4 translateMatrix = Matrix4x4.CreateTranslateMatrix(Position + PositionBias);

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
}
