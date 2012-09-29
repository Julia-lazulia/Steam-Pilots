using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cloud9
{
    public class AirShip : Mount
    {
        #region Properties
        int maxSpeedX = 400;
        int maxSpeedY = 400;
        int accelX = 800;
        int accelY = 1000;
        #endregion

        #region Initialization
        public AirShip()
        {
            Dictionary<string, Animation> Animations = new Dictionary<string, Animation>();
            Animations.Add("Idle", new Animation(0f, 1, false, "Ships/full"));

            sprite = new Sprite(Animations);
            sprite.PlayAnimation("Idle");

            requiresGroundToStep = false;
            stepValue = 300f;
            position = new Vector2(5000f, 500f);
            velocity = new Vector2(0f, 100f);
            drawPriority = 0f;
            collidesWithTiles = true;
            collidesWithOtherEntities = false;
            gravityEffect = 10f;
            active = true;
            radius = 42f;
            tileHeight = 5;
            tileWidth = 7;
            spriteEffects = SpriteEffects.None;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get player offset
        /// </summary>
        /// <returns></returns>
        public override Vector2 GetPlayerOffset()
        {
            return new Vector2(((spriteEffects == SpriteEffects.None ? 1 : -1) * 20), 0f);
        }

        /// <summary>
        /// Update input
        /// </summary>
        protected override void UpdateInput()
        {
            if (Input.Instance.KeyDown(Keys.A))
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
                if (velocity.X > -maxSpeedX)
                {
                    velocity.X = velocity.X - accelX * World.ElapsedSeconds;
                    if (velocity.X < -maxSpeedX)
                    {
                        velocity.X = -maxSpeedX;
                    }
                }
            }
            else
            {
                if (Input.Instance.KeyDown(Keys.D))
                {
                    spriteEffects = SpriteEffects.None;
                    if (velocity.X < maxSpeedX)
                    {
                        velocity.X = velocity.X + accelX * World.ElapsedSeconds;
                        if (velocity.X > maxSpeedX)
                        {
                            velocity.X = maxSpeedX;
                        }
                    }
                }
            }
            if (Input.Instance.KeyDown(Keys.W))
            {
                if (velocity.Y > -maxSpeedY)
                {
                    velocity.Y = velocity.Y - accelY * World.ElapsedSeconds;
                    if (velocity.Y < -maxSpeedY)
                    {
                        velocity.Y = -maxSpeedY;
                    }
                }
            }
            else
            {
                if (Input.Instance.KeyDown(Keys.S))
                {
                    if (velocity.Y < maxSpeedY)
                    {
                        velocity.Y = velocity.Y + accelY * World.ElapsedSeconds;
                        if (velocity.Y > maxSpeedY)
                        {
                            velocity.Y = maxSpeedY;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
