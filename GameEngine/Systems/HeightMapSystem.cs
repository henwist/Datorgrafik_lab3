using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Systems
{

    public class HeightmapSystem :IUdatable
    {

        private List<Component> heightmapComponents;
        private List<HeightmapObject> hmobjects;
        private GraphicsDevice gd;


        public HeightmapSystem(GraphicsDevice gd, List<HeightmapObject> hmobjects)
        {
            this.gd = gd;
            this.hmobjects = hmobjects;

            heightmapComponents = new List<Component>();

            CreateHeightmapComponents();

            LoadHeightData();
            SetUpVertices();
            SetUpIndices();

            SetUpNormals();

            TransferToGraphicsCard();

        }

        float rot = 0.001f;
        public void Draw(BasicEffect effect)
        {

            foreach (HeightmapComponent cmp in heightmapComponents)
            {
                effect.Texture = cmp.texture;

                //cmp.vertexBuffer.SetData<VertexPositionNormalTexture>(cmp.vertices, 0, cmp.vertexCount);
                gd.SetVertexBuffer(cmp.vertexBuffer);

                //cmp.indexBuffer.SetData<int>(cmp.indices, 0, cmp.indexCount);
                gd.Indices = cmp.indexBuffer;

                //if (cmp.texture.Name.Contains("fire"))
                //{
                //    cmp.objectWorld = Matrix.CreateScale(cmp.scaleFactor)
                //                    * Matrix.CreateTranslation(cmp.position + (float)System.Math.Sin(rot) *100*  new Vector3(rot, rot, rot));
                //}
                //else
                //{
                //    cmp.objectWorld = Matrix.CreateScale(cmp.scaleFactor)
                //                     * Matrix.CreateTranslation(cmp.position);
                //}
                //rot += 0.002f;


                cmp.objectWorld = Matrix.CreateScale(cmp.scaleFactor)
                                 * Matrix.CreateTranslation(cmp.position);

                //cmp.world = cmp.objectWorld * cmp.world;

                effect.World = cmp.objectWorld * cmp.world;

                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    gd.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, cmp.indexCount / 3);
                }
            }
        }


        private void TransferToGraphicsCard()
        {
            foreach (HeightmapComponent cmp in heightmapComponents)
            {
                cmp.vertexBuffer.SetData<VertexPositionNormalTexture>(cmp.vertices, 0, cmp.vertexCount);
                gd.SetVertexBuffer(cmp.vertexBuffer);

                cmp.indexBuffer.SetData<int>(cmp.indices, 0, cmp.indexCount);
                gd.Indices = cmp.indexBuffer;

            }
        }


        private void CreateHeightmapComponents()
        {
            foreach(HeightmapObject hmobj in hmobjects)
            {
                ComponentManager.StoreComponent(ComponentManager.GetNewId(),
                    new HeightmapComponent(gd, hmobj.scaleFactor, hmobj.terrainMapName, hmobj.textureName, hmobj.world));
            }

            heightmapComponents = ComponentManager.GetComponents<HeightmapComponent>();
        }


        private void SetUpVertices()
        {
            Random rnd = new Random();
            int index = 0;

            foreach (HeightmapComponent cmp in heightmapComponents)
            {
                for (int x = 0; x < cmp.terrainWidth; x++)
                {
                    for (int y = 0; y < cmp.terrainHeight; y++)
                    {
                        index = x + y * cmp.terrainWidth;

                        cmp.vertices[index].Position = new Vector3(x,
                                                                                      cmp.heightData[x, y],
                                                                                      -y);

                        cmp.vertices[index].Position = Vector3.Transform(cmp.vertices[index].Position,
                                                                                                Matrix.CreateScale(cmp.scaleFactor));

                        cmp.vertices[index].Normal = new Vector3(rnd.Next(0, 101) / 100f, rnd.Next(0, 101) / 100f, rnd.Next(0, 101) / 100f);
                        cmp.vertices[index].TextureCoordinate = new Vector2(0, 0);
                    }
                }

                index = 0;
            }
        }

        private void SetUpNormals()
        {

            int counter = 0;

            Vector3 v1 = Vector3.Zero;
            Vector3 v2 = Vector3.Zero;

            foreach (HeightmapComponent cmp in heightmapComponents)
                for (int y = 0; y < cmp.terrainHeight - 1; y++)
                {
                    for (int x = 0; x < cmp.terrainWidth - 1; x++)
                    {
                        int lowerLeft = x + y * cmp.terrainWidth;
                        int lowerRight = (x + 1) + y * cmp.terrainWidth;
                        int topLeft = x + (y + 1) * cmp.terrainWidth;
                        int topRight = (x + 1) + (y + 1) * cmp.terrainWidth;

                        v1 = Vector3.Cross(cmp.vertices[topLeft].Position, cmp.vertices[lowerLeft].Position);
                        //cmp.indices[counter++] = lowerRight;
                        //cmp.indices[counter++] = lowerLeft;

                        v2 = Vector3.Cross(cmp.vertices[topRight].Position, cmp.vertices[lowerRight].Position);

                        cmp.vertices[lowerLeft].Normal = Vector3.Normalize(Vector3.Add(v1, cmp.vertices[lowerRight].Normal));
                        cmp.vertices[topRight].Normal = Vector3.Normalize(Vector3.Add(v2, cmp.vertices[lowerLeft].Normal));
                        //cmp.indices[counter++] = topLeft;
                        //cmp.indices[counter++] = topRight;
                        //cmp.indices[counter++] = lowerRight;
                    }
                }
        }


        private void SetUpIndices()
        {

            int counter = 0;

            foreach (HeightmapComponent cmp in heightmapComponents)
            {
                for (int y = 0; y < cmp.terrainHeight - 1; y++)
                {
                    for (int x = 0; x < cmp.terrainWidth - 1; x++)
                    {
                        int lowerLeft = x + y * cmp.terrainWidth;
                        int lowerRight = (x + 1) + y * cmp.terrainWidth;
                        int topLeft = x + (y + 1) * cmp.terrainWidth;
                        int topRight = (x + 1) + (y + 1) * cmp.terrainWidth;

                        cmp.indices[counter++] = topLeft;
                        cmp.indices[counter++] = lowerRight;
                        cmp.indices[counter++] = lowerLeft;

                        cmp.indices[counter++] = topLeft;
                        cmp.indices[counter++] = topRight;
                        cmp.indices[counter++] = lowerRight;
                    }
                }

                counter = 0;
           }
        }


        private void LoadHeightData()
        {
            foreach (HeightmapComponent cmp in heightmapComponents)
                for (int x = 0; x < cmp.terrainWidth; x++)
                {
                    for (int y = 0; y < cmp.terrainHeight; y++)
                    {
                        System.Drawing.Color color = cmp.bmpHeightdata.GetPixel(x, y);
                        cmp.heightData[x, y] = ((color.R + color.G + color.B) / 3);
                    }
                }

        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }

}
