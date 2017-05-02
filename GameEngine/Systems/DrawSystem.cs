using Microsoft.Xna.Framework;
using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    public class DrawSystem : IUdatable
    {

        private SpriteBatch spriteBatch;

        public DrawSystem(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }
        public void Update(GameTime gameTime)
        {
            //Transform trans;
            //MySprite sprite;

            //foreach(ulong id in ComponentManager.GetAllIds<MySprite>())
            //{
            //    trans = ComponentManager.GetComponent<Transform>(id);
            //    sprite = ComponentManager.GetComponent<MySprite>(id);

            //    spriteBatch.Draw(sprite.Texture,
            //        new Vector2(trans.Xpos, trans.Ypos),
            //        new Rectangle(0, 0, sprite.Width, sprite.Height),
            //        Color.White);
            //}
        }
    }
}