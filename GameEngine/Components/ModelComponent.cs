using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Managers;

namespace GameEngine.Components
{
    public class ModelComponent : Component
    {
        public Model model { get; protected set; }
        public Matrix world { get; set; }

        public Matrix scale { get; protected set; }
        public Matrix translation { get; protected set; }
        public Matrix rotation { get; protected set; }
        public Matrix[] chopperMeshWorldMatrices { get; protected set; }

        public ModelComponent(Model m, float scale, Vector3 translation, 
                              float rotationx, float rotationy, float rotationz, bool chopper)
        {
            model = m;
            this.scale = Matrix.CreateScale(scale);
            this.translation = Matrix.CreateTranslation(translation);
            rotation = Matrix.CreateRotationX(rotationx)
                     * Matrix.CreateRotationY(rotationy)
                     * Matrix.CreateRotationZ(rotationz);

            if (chopper)
            {
                chopperMeshWorldMatrices = new Matrix[3];
                ModelMesh[] meshes = model.Meshes.ToArray();

                chopperMeshWorldMatrices[0] = Matrix.CreateTranslation(meshes[0].ParentBone.Transform.Translation);
                chopperMeshWorldMatrices[1] = Matrix.Identity;
                chopperMeshWorldMatrices[2] = Matrix.CreateTranslation(meshes[2].ParentBone.Transform.Translation);
            }
        }

        public override void Update(GameTime gametime)
        {

        }
    }
}
