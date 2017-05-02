using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    public interface ISysDrawable
    {
        void Draw(BasicEffect effect, GameTime gametime);
    }
}
