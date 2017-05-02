using GameEngine.Systems;
using GameEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using GameEngine.Objects;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Managers
{
    public class SceneManager
    {

        //private PhysicsSystem phys_sys;
        private GraphicsDevice gd;

        private List<HeightmapObject> heightmapObjects;

        private HeightmapSystem heightmapSystem;

        private Matrix world;

        public SceneManager(GraphicsDevice gd, Matrix world)//, SpriteBatch spriteBatch, Rectangle window)
        {
            //phys_sys = new PhysicsSystem(window);
            this.gd = gd;
            this.world = world;

            heightmapObjects = new List<HeightmapObject>();
            createHeightmapObjects();

            heightmapSystem = new HeightmapSystem(gd, heightmapObjects);

            //LoadComponents();
            //draw_sys = new DrawSystem(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            //phys_sys.Update(gameTime);
        }

        public void Draw(BasicEffect effect, GameTime gameTime)
        {
            heightmapSystem.Draw(effect);
            //draw_sys.Update(gameTime);
        }

        private void LoadComponents()
        {
            //Terrain component

        }

        private void createHeightmapObjects()
        {
            HeightmapObject hmobj = new HeightmapObject();
            hmobj.scaleFactor = 0.5f*Vector3.One;
            hmobj.position = Vector3.Zero;
            hmobj.terrainMapName = "..\\..\\..\\..\\Content\\Textures\\US_Canyon.png";
            hmobj.textureName = "..\\..\\..\\..\\Content\\Textures\\grass.png";
            hmobj.objectWorld = Matrix.Identity;
            hmobj.world = Matrix.Identity;
            heightmapObjects.Add(hmobj);

            //HeightmapObject hmobj2 = new HeightmapObject();
            //hmobj2.scaleFactor = Vector3.One;
            //hmobj2.position = Vector3.Zero;
            //hmobj2.terrainMapName = "..\\..\\..\\..\\Content\\Textures\\play.png";
            //hmobj2.textureName = "..\\..\\..\\..\\Content\\Textures\\fire.png";
            //hmobj2.objectWorld = Matrix.Identity;
            //hmobj2.world = Matrix.Identity;
            //heightmapObjects.Add(hmobj2);

        }

    }
}