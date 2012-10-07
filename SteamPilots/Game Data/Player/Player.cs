using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SteamPilots
{
    public class Player : Entity
    {
        #region Properties
        private const int playerMaxSpeed = 180;
        private const int playerAccel = 4000;
        private AirShip airShip;
        private byte currentTile = 1;
        #endregion

        #region Initialization
        /// <summary>
        /// Initialize player
        /// </summary>
        public Player()
        {
            // Animations
            Dictionary<string, Animation> Animations = new Dictionary<string,Animation>();
            Animations.Add("Idle", new Animation(0f, 1, false, "Player/playerIdle"));
            Animations.Add("Run", new Animation(0.3f, 4, true, "Player/playerRun"));

            sprite = new Sprite(Animations);
            sprite.PlayAnimation("Idle");
            airShip = new AirShip();
            airShip.Spawn();
            airShip.ChangeActivity();
            stepValue = 175f;
            layer = 2;
            position = new Vector2(5000f, 500f);
            velocity = new Vector2(0f, 100f);
            drawPriority = 0f;
            collidesWithTiles = true;
            collidesWithOtherEntities = true;
            gravityEffect = 1000f;
            active = true;
            radius = 16f;
            tileHeight = 2;
            tileWidth = 1;
            spriteEffects = SpriteEffects.None;
        }

        /// <summary>
        /// Update the player
        /// </summary>
        public override void Update()
        {
            HandleInput();
            base.Update();
        }

        /// <summary>
        /// Handle input
        /// </summary>
        private void HandleInput()
        {
            if(mount == null)
            {
                if (Input.Instance.KeyDown(Keys.A))
                {
                    sprite.PlayAnimation("Run");
                    spriteEffects = SpriteEffects.FlipHorizontally;
                    if (velocity.X > -180f)
                    {
                        velocity.X = velocity.X - playerAccel * World.ElapsedSeconds;
                        if (velocity.X < -180f)
                            velocity.X = -180f;
                    }
                }
                else if (Input.Instance.KeyDown(Keys.D))
                {
                    sprite.PlayAnimation("Run");
                    spriteEffects = SpriteEffects.None;
                    if (velocity.X < 180f)
                    {
                        velocity.X = velocity.X + playerAccel * World.ElapsedSeconds;
                        if (velocity.X > 180f)
                            velocity.X = 180f;
                    }
                }
                else if (isOnGround)
                {
                    sprite.PlayAnimation("Idle");
                    velocity.X = velocity.X / 2f;
                }
                if (Input.Instance.KeyNewPressed(Keys.Q))
                    if (layer > 1)
                        ChangeLayers(layer - 1);
                if (Input.Instance.KeyNewPressed(Keys.E))
                    if (layer < 3)
                        ChangeLayers(layer + 1);
                if (Input.Instance.KeyDown(Keys.W) && isOnGround)
                    velocity.Y = velocity.Y - 400f;
                if (Input.Instance.KeyDown(Keys.W) && velocity.Y < 0f)
                    velocity.Y = velocity.Y - gravityEffect / 2f * World.ElapsedSeconds;
            }
            
            if (Input.Instance.KeyDown(Keys.D1))
                currentTile = 1;
            if (Input.Instance.KeyDown(Keys.D2))
                currentTile = 2;
            if (Input.Instance.KeyDown(Keys.D3))
                currentTile = 3;
            if (Input.Instance.KeyDown(Keys.D4))
                currentTile = 4;
            
            if (Input.Instance.MouseLeftButtonNewPressed())
            {
                Vector2 tile = (Input.Instance.MousePosition() / World.Instance.ScreenScaling + World.Instance.CameraPosition) / 16f;
                if (World.Instance.GetLayer(layer).IsValidTile((int)tile.X, (int)tile.Y))
                    World.Instance.GetLayer(layer).SetTile((int)tile.X, (int)tile.Y, Tile.Air);
            }

            if (Input.Instance.MouseRightButtonNewPressed())
            {
                Vector2 tile = (Input.Instance.MousePosition() / World.Instance.ScreenScaling + World.Instance.CameraPosition) / 16f;
                if (World.Instance.GetLayer(layer).IsValidTile((int)tile.X, (int)tile.Y) && World.Instance.GetLayer(layer).CanPlace((int)tile.X, (int)tile.Y, Tile.GetTile(currentTile)))
                    World.Instance.GetLayer(layer).SetTile((int)tile.X, (int)tile.Y, Tile.GetTile(currentTile));
            }

            if (Input.Instance.KeyNewPressed(Keys.Space))
            {
                airShip.ChangeActivity();
                if (mount == null)
                {
                    Mount(airShip);
                    sprite.PlayAnimation("Idle");
                }
                else
                    Dismount();
            }

            if (Input.Instance.KeyNewPressed(Keys.Enter))
            {
                World.Instance.DrawTiles = !World.Instance.DrawTiles;
            }
        }
        #endregion
    }
}
