using System;
using GalaxyShooting.Rendering;
using GalaxyShooting.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyShooting.Logic
{
    public class Bullet : GameObjectBase
    {
        public Vector3 Position;
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

        public override void Update()
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

        public override bool Collision(Bullet obj)
        {
            return false;
        }

        public override void Render(WireFrameRenderer renderer)
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
