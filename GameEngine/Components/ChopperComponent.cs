using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components
{
    public class ChopperComponent : Component
    {
        public readonly bool isChopper = true;
        public float rotorAngle { get; set; }

        public ChopperComponent()
        {
            rotorAngle = MathHelper.PiOver4 / 60;
        }
    }
}
