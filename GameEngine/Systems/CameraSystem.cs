using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Systems
{
    public class CameraSystem : IUdatable
    {
        private static CameraSystem instance;
        public GraphicsDevice device { get; protected set; }

        public static CameraSystem Instance
        {
            get
            {
                if (instance == null)
                    instance = new CameraSystem();
                return instance;
            }
        }

        private CameraSystem()
        {

        }


        public void Update(GameTime gameTime)
        {

            foreach (ulong m in ComponentManager.GetAllEntitiesWithComp<CameraComponent>())
            {
                TransformComponent transform = ComponentManager.GetComponent<TransformComponent>(m);
                CameraComponent curCam = ComponentManager.GetComponent<CameraComponent>(m);

                Vector3 offSet = new Vector3(0, 0.5f, 0.6f);
                curCam.cameraPosition = Vector3.Transform(offSet, Matrix.CreateFromQuaternion(transform.qRot));
                curCam.cameraPosition += transform.position;

                Vector3 up = Vector3.Transform(Vector3.Up, transform.qRot);

                curCam.viewMatrix = Matrix.CreateLookAt(curCam.cameraPosition, transform.position, up);
                curCam.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.AspectRatio, 0.2f, 500f);
            }
        }
    }
}
