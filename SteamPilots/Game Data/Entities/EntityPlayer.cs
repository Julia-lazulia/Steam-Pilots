using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SteamPilots
{
    public class EntityPlayer : Entity
    {
        #region Properties
        private const float playerMaxSpeed = 100f;
        private const float playerJumpSpeed = 250f;
        private const int playerAccel = 2000;
        public AirShip airShip;
        public byte currentSlot = 0;
        public GuiInventory inventory = null;
        public GuiManager currentGui = null;
        #endregion

        #region Initialization
        /// <summary>
        /// Initialize player
        /// </summary>
        public EntityPlayer()
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
            
            //Set up GUI
            currentGui = new GuiManager();
            inventory = new GuiInventory();
            inventory.visible = false;
            currentGui.AddGuiElement(inventory);
        }

        /// <summary>
        /// Update the player
        /// </summary>
        public override void Update()
        {
            if (currentGui != null)
            {
                currentGui.Update(World.GameTime);
            }
            HandleInput();
            base.Update();
        }

        /// <summary>
        /// Jump!
        /// </summary>
        public void Jump()
        {
            velocity.Y = velocity.Y - playerJumpSpeed;
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
                    if (velocity.X > -playerMaxSpeed)
                    {
                        velocity.X = velocity.X - playerAccel * World.ElapsedSeconds;
                        if (velocity.X < -playerMaxSpeed)
                            velocity.X = -playerMaxSpeed;
                    }
                }
                else if (Input.Instance.KeyDown(Keys.D))
                {
                    sprite.PlayAnimation("Run");
                    spriteEffects = SpriteEffects.None;
                    if (velocity.X < playerMaxSpeed)
                    {
                        velocity.X = velocity.X + playerAccel * World.ElapsedSeconds;
                        if (velocity.X > playerMaxSpeed)
                            velocity.X = playerMaxSpeed;
                    }
                }
                else if (isOnGround)
                {
                    sprite.PlayAnimation("Idle");
                    velocity.X = velocity.X / 2f;
                }
                /*
                if (Input.Instance.KeyNewPressed(Keys.Q))
                    if (layer > 1)
                        ChangeLayers(layer - 1);
                if (Input.Instance.KeyNewPressed(Keys.E))
                    if (layer < 3)
                        ChangeLayers(layer + 1);
                 */
                if (Input.Instance.KeyDown(Keys.W) && isOnGround)
                    Jump();
                if (Input.Instance.KeyDown(Keys.W) && velocity.Y < 0f)
                    velocity.Y = velocity.Y - gravityEffect / 2f * World.ElapsedSeconds;
            }
            
            // Changing slots (need to do!)
            if (Input.Instance.KeyDown(Keys.D0))
                currentSlot = 0;
            if (Input.Instance.KeyDown(Keys.D1))
                currentSlot = 1;
            if (Input.Instance.KeyDown(Keys.D2))
                currentSlot = 2;
            if (Input.Instance.KeyDown(Keys.D3))
                currentSlot = 3;
            if (Input.Instance.KeyDown(Keys.D4))
                currentSlot = 4;
            if (Input.Instance.KeyDown(Keys.D5))
                currentSlot = 5;
            if (Input.Instance.KeyDown(Keys.D6))
                currentSlot = 6;
            if (Input.Instance.KeyDown(Keys.D7))
                currentSlot = 7;
            if (Input.Instance.KeyDown(Keys.D8))
                currentSlot = 8;
            if (Input.Instance.KeyDown(Keys.D9))
                currentSlot = 9;

            if (Input.Instance.WheelScrolledUp() || Input.Instance.WheelScrolledDown())
            {
                int scrollValue = Input.Instance.ScrolledValue();
                currentSlot += (byte)scrollValue;
                if (currentSlot > 9) currentSlot = 9;
                if (currentSlot < 0) currentSlot = 0;
                Console.WriteLine(currentSlot);
            }

            if (Input.Instance.MouseLeftButtonNewPressed())
            {
                Vector2 tile = (Input.Instance.MousePosition() / GameStateManager.Main.ScreenScaling + World.Instance.CameraPosition) / Tile.SpriteSize;
                int tileId = World.Instance.GetForegroundLayer(layer).GetTile((int)tile.X, (int)tile.Y).TileIndex;
                if ((tileId != Tile.Air.TileIndex && World.Instance.GetForegroundLayer(layer).IsValidTile((int)tile.X, (int)tile.Y)) && World.Instance.GetForegroundLayer(layer).GetTile((int)tile.X, (int)tile.Y).OnBreak(this, tile))
                {
                    World.Instance.GetForegroundLayer(layer).SetTile((int)tile.X, (int)tile.Y, Tile.Air);
                }
            }

            if (Input.Instance.MouseRightButtonNewPressed())
            {
                Vector2 tile = (Input.Instance.MousePosition() / GameStateManager.Main.ScreenScaling + World.Instance.CameraPosition) / Tile.SpriteSize;
                if (inventory.Slots()[currentSlot].ItemStack.Item is ItemTile && ((ItemTile)inventory.Slots()[currentSlot].ItemStack.Item).OnPlace(this, tile)/*Need to do the tile - item linking and adding OnPlace to item by default*/)
                {
                    World.Instance.GetForegroundLayer(layer).SetTile((int)tile.X, (int)tile.Y, inventory.Slots()[currentSlot].ItemStack.Item.Tile);
                }
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

            if (Input.Instance.KeyNewPressed(Keys.I))
            {
                //if (currentGui == null) currentGui = inventory;
                //else if (currentGui is GuiInventory) currentGui = null;
                //Martin: GUI needs to always be displayed, we just toggle displaying of the Inventory GUI element with I
                inventory.visible = !inventory.visible;
            }

            if (Input.Instance.KeyNewPressed(Keys.Enter))
            {
                World.Instance.DrawTiles = !World.Instance.DrawTiles;
            }
        }

        public override void Draw(SpriteBatch s, float layerDepth)
        {
            base.Draw(s, layerDepth);
            if(currentGui != null) 
                currentGui.Draw(s);
        }
        #endregion
    }
}
