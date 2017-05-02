using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using GameEngine.Components;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Systems
{
    public class TransformSystem : IUdatable
    {
        private static TransformSystem instance;

        public static TransformSystem Instance
        {
            get
            {
                if (instance == null)
                    instance = new TransformSystem();
                return instance;
            }
        }

        

        private void Move(ref Vector3 position, Quaternion qRot, float speed)
        {
            position += Vector3.Transform(new Vector3(0, 0, -1), qRot) * speed;
        }

        public void Update(GameTime gameTime)
        {
            List<ulong> comps = ComponentManager.GetAllEntitiesWithComp<TransformComponent>();

            foreach (ulong c in comps)
            {
                CameraComponent camera = ComponentManager.GetComponent<CameraComponent>(c);
                TransformComponent transform = ComponentManager.GetComponent<TransformComponent>(c);

                //float leftRightRot = 0;
                //float upDownRot = 0;

                //float turningSpeed = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000f;
                //turningSpeed += .025f;

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    transform.position.X += transform.speed.X * gameTime.ElapsedGameTime.Milliseconds;
                    //leftRightRot += turningSpeed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    transform.position.X -= transform.speed.X * gameTime.ElapsedGameTime.Milliseconds;
                    //leftRightRot -= turningSpeed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    transform.position.Z -= transform.speed.Z * gameTime.ElapsedGameTime.Milliseconds;
                    //upDownRot += turningSpeed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    transform.position.Z += transform.speed.Z * gameTime.ElapsedGameTime.Milliseconds;
                    //upDownRot -= turningSpeed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    transform.position.Y += transform.speed.Y * gameTime.ElapsedGameTime.Milliseconds;
                    //upDownRot += turningSpeed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    transform.position.Y -= transform.speed.Y * gameTime.ElapsedGameTime.Milliseconds;
                    //upDownRot -= turningSpeed;
                }

                Vector3 axis = Vector3.Zero;
                float angle = (float)-gameTime.ElapsedGameTime.TotalMilliseconds * .01f;

                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    axis = new Vector3(1f, 0, 0);
                    //upDownRot += turningSpeed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    axis = new Vector3(-1f, 0, 0);
                    //upDownRot += turningSpeed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    axis = new Vector3(0, -1f, 0);
                    //upDownRot += turningSpeed;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    axis = new Vector3(0, 1f, 0);
                    //upDownRot += turningSpeed;
                }

                //Quaternion extraRot = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, -1), leftRightRot)
                //                        * Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), upDownRot);
                Quaternion extraRot = Quaternion.CreateFromAxisAngle(axis, angle);
                extraRot.Normalize();
                transform.qRot *= extraRot;
            }
        }
    }
}
