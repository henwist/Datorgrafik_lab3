using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class TransformComponent : Component
    {
        public Matrix World;
        public Vector3 position;
        public float rotation;
        public Quaternion qRot;
        public float scale;
        //public float speed = 3f;
        public Vector3 speed = new Vector3(.1f, .1f, .1f);

        public TransformComponent(Vector3 pos, float rot, float scale)
        {
            position = pos;
            rotation = rot;
            this.scale = scale;
            World = Matrix.Identity;
        }
    }
}
