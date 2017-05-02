
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Drawing;
using System.IO;

namespace GameEngine.Components
{
    public class HeightmapComponent : Component
    {
        
        public int terrainWidth  { get; private set; }
        public int terrainHeight { get; private set; }

        public Vector3 scaleFactor { get; set; }
        public Vector3 position { get; set; }

        public int vertexCount   { get; private set; }
        public int indexCount    { get; private set; }

        public VertexPositionNormalTexture[] vertices { get; set; }

        public VertexBuffer vertexBuffer { get; set; }
        public IndexBuffer indexBuffer   { get; set; }

        public int[] indices       { get; set; }
        public float[,] heightData { get; set; }

        public Bitmap bmpHeightdata { get; private set; }

        public Bitmap bmpTexture { get; private set; }

        public Texture2D texture;

        public Matrix objectWorld { get; set; }
        public Matrix world { get; set; }


        public HeightmapComponent(GraphicsDevice gd, Vector3 scaleFactor, string pictureFileName, string textureFileName, Matrix world)
        {
            bmpHeightdata = new Bitmap(pictureFileName);
            terrainHeight = bmpHeightdata.Height;
            terrainWidth = bmpHeightdata.Width;

            //this.terrainHeight = terrainHeight;
            //this.terrainWidth = terrainWidth;

            this.scaleFactor = scaleFactor;

            vertexCount = terrainWidth * terrainHeight;
            indexCount = (terrainWidth - 1) * (terrainHeight - 1) * 6;

            vertices = new VertexPositionNormalTexture[vertexCount];
            indices = new int[indexCount];

            heightData = new float[terrainWidth, terrainHeight];

            vertexBuffer = new VertexBuffer(gd, typeof(VertexPositionNormalTexture), vertexCount, BufferUsage.None);
            indexBuffer = new IndexBuffer(gd, typeof(int), indexCount, BufferUsage.None);

            bmpTexture = new Bitmap(textureFileName);
            //texture = Texture2D.CreateTex2DFromBitmap(bmpTexture, gd) //; bmpTexture, gd);
            //texture = Texture2D.FromStream(gd, new FileStream(textureFileName, System.IO.FileMode.Open));// CreateTex2DFromBitmap(bmpTexture, gd);
            texture = Texture2D.FromStream(gd, new StreamReader(textureFileName).BaseStream);
            texture.Name = textureFileName;

            objectWorld = Matrix.Identity;
            this.world = world;
        }
    }
}
