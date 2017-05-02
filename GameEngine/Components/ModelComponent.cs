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
        public Matrix[] modelMeshWorldMatrices { get; protected set; }

        public ModelComponent(Model m, float scale, Vector3 translation, 
                              float rotationx, float rotationy, float rotationz, bool chopper)
        {
            model = m;
            this.scale = Matrix.CreateScale(scale);
            this.translation = Matrix.CreateTranslation(translation);
            rotation = Matrix.CreateRotationX(rotationx)
                     * Matrix.CreateRotationY(rotationy)
                     * Matrix.CreateRotationZ(rotationz);


            modelMeshWorldMatrices = new Matrix[model.Meshes.Count];
            ModelMesh[] meshes = model.Meshes.ToArray();

            for (int i = 0; i < model.Meshes.Count; i++)
            {
                modelMeshWorldMatrices[i] = Matrix.CreateTranslation(meshes[i].ParentBone.Transform.Translation);
            }
        }

        public override void Update(GameTime gametime)
        {

        }
    }
}
