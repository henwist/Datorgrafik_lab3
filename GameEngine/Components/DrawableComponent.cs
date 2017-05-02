using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public abstract class DrawableComponent : Component
    {
        public DrawableComponent(GraphicsDevice device)
        {
            
        }
        public virtual void Draw(GameTime gametime, CameraComponent camera)
        {

        }
    }
}
