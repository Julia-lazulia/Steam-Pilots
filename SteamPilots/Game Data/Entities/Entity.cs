using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SteamPilots
{
    public abstract class Entity : IGameData
    {
        #region Properties
        protected Vector2 position;
        protected Vector2 velocity;
        protected Sprite sprite;
        protected SpriteEffects spriteEffects;
        protected float drawPriority;
        protected int layer;
        protected bool active;
        protected float radius;
        protected bool collidesWithOtherEntities;
        protected bool collidesWithTiles;
        protected float gravityEffect;
        protected Mount mount;
        protected bool isOnGround = false;
        protected int tileWidth;
        protected int tileHeight;
        protected float stepValue = 100f;
        protected bool requiresGroundToStep = true;
        protected int x, y;
        protected Rectangle boundingRect;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public int Layer
        {
            get { return layer; } 
        }

        public bool CollidesWithTiles
        {
            get { return collidesWithTiles; }
        }

        public bool CollidesWithEntities
        {
            get { return collidesWithOtherEntities; }
        }

        public Rectangle BoundingRect
        {
            get { return boundingRect; }
        }
        #endregion

        #region Initialization
        public Entity()
        {
            layer = 2;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Spawns the entity
        /// </summary>
        public virtual void Spawn()
        {
            World.Instance.GetForegroundLayer(layer).GetEntities(active).Add(this);
        }

        public virtual void Destroy()
        {
            World.Instance.GetForegroundLayer(layer).GetEntities(active).Remove(this);
        }

        /// <summary>
        /// Dismounts the entity
        /// </summary>
        public void Dismount()
        {
            mount.DismountRider();
            mount = null;
        }

        /// <summary>
        /// Mounts the entity
        /// </summary>
        /// <param name="mount">Mount</param>
        public void Mount(Mount mount)
        {
            this.mount = mount;
            this.mount.Ride(this);
        }

        /// <summary>
        /// Updates the entity
        /// </summary>
        public virtual void Update()
        {
            if(sprite != null)
                sprite.Update();
            if (collidesWithOtherEntities && mount == null)
            {
                Entity[] entities = World.Instance.GetForegroundLayer(layer).GetEntities(active).ToArray();
                for (int index = 0; index < entities.Length; index++)
                {
                    Entity entity = entities[index];
                    if (entity != this && entity.collidesWithOtherEntities)
                    {
                        float distance = Vector2.Distance(entity.position, position);
                        if (distance < radius + entity.radius)
                            OnCollide(entity);
                    }
                }
            }
            if (mount == null)
            {
                if (velocity.Length() != 0f)
                {
                    position += velocity * World.ElapsedSeconds;
                    position.X = MathHelper.Clamp(position.X, 0f, (float)(World.Width * 16));
                    position.Y = MathHelper.Clamp(position.Y, 0f, (float)(World.Height * 16));
                }
            }
            else
            {
                position = mount.position + mount.GetPlayerOffset();
                velocity = Vector2.Zero;
                spriteEffects = mount.spriteEffects;
                drawPriority = -1f;
            }
            if (collidesWithTiles && World.Instance.GetForegroundLayer(layer).HasTiles() && mount == null)
                DoTileCollision();

         
            if (velocity.Y < 2000f && !isOnGround)
                velocity.Y = velocity.Y + gravityEffect * World.ElapsedSeconds;
             
        }

        /// <summary>
        /// Tile collision
        /// </summary>
        protected virtual void DoTileCollision()
        {
            if (tileWidth % 2 == 0)
                x = (int)Math.Round((double)(position.X / 16f)) - tileWidth / 2;
            else
                x = (int)Math.Floor((double)(position.X / 16F)) - tileWidth / 2;
            if (tileHeight % 2 == 0)
                y = (int)Math.Round((double)(position.Y / 16F)) - tileHeight / 2;
            else
                y = (int)Math.Floor((double)(position.Y / 16F)) - tileHeight / 2;

            int top = y - 1;
            int bot = y + tileHeight + 1;
            int left = x - 1;
            int right = x + tileWidth + 1;
            boundingRect = new Rectangle((int)(position.X - (float)(tileWidth * 16 / 2)), (int)(position.Y - (float)(tileHeight * 16 / 2)), tileWidth * 16, tileHeight * 16);
            bool canStep = true;
            for (int cX = left; cX <= right; cX++)
            {
                for (int cY = y; cY < y + tileHeight; cY++)
                {
                    if (World.Instance.GetForegroundLayer(layer).IsValidTile(cX, cY))
                    {
                        if (World.Instance.GetForegroundLayer(layer).GetTile(cX, cY).Collides)
                        {
                            Rectangle bounds = World.Instance.GetForegroundLayer(layer).GetTile(cX, cY).GetBounds(cX, cY);
                            if (boundingRect.Intersects(bounds))
                            {
                                World.Instance.GetForegroundLayer(layer).GetTile(cX, cY).OnCollide(this);
                                if (bounds.Center.X > boundingRect.Center.X)
                                {
                                    if (velocity.X > 0f)
                                    {
                                        if (cY == y + tileHeight - 1 && canStep && (isOnGround || !requiresGroundToStep))
                                        {
                                            velocity.Y = -stepValue;
                                        }
                                        position.X = (float)(bounds.Left - boundingRect.Width / 2 + 1);
                                        velocity.X = 0f;
                                        canStep = false;
                                    }
                                }
                                else
                                {
                                    if (velocity.X < 0f)
                                    {
                                        if (cY == y + tileHeight - 1 && canStep && (isOnGround || !requiresGroundToStep))
                                        {
                                            velocity.Y = -stepValue;
                                        }
                                        position.X = (float)(bounds.Right + boundingRect.Width / 2);
                                        velocity.X = 0f;
                                        canStep = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            isOnGround = false;
            for (int cY = top; cY <= bot; cY++)
            {
                for (int cX = x; cX < x + tileWidth; cX++)
                {
                    if (World.Instance.GetForegroundLayer(this.layer).IsValidTile(cX, cY))
                    {
                        if (World.Instance.GetForegroundLayer(this.layer).GetTile(cX, cY).Collides)
                        {
                            Rectangle bounds = World.Instance.GetForegroundLayer(layer).GetTile(cX, cY).GetBounds(cX, cY);
                            if (this.boundingRect.Intersects(bounds))
                            {
                                World.Instance.GetForegroundLayer(layer).GetTile(cX, cY).OnCollide(this);
                                if (bounds.Center.Y > boundingRect.Center.Y)
                                {
                                    if (velocity.Y > 0f)
                                    {
                                        isOnGround = true;
                                        position.Y = (float)(bounds.Top - boundingRect.Height / 2 + 1);
                                        velocity.Y = 0f;
                                    }
                                }
                                else
                                {
                                    if (velocity.Y < 0f)
                                    {
                                        position.Y = (float)(bounds.Bottom + boundingRect.Height / 2);
                                        velocity.Y = 0f;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Tells the entity that it collides with the given entity
        /// </summary>
        /// <param name="e">entity</param>
        public virtual void OnCollide(Entity e)
        {
        }

        /// <summary>
        /// Draws the entity
        /// </summary>
        public virtual void Draw(SpriteBatch s, float layerDepth)
        {
            sprite.Draw(s, position - World.Instance.CameraPosition, 1f, 0f, spriteEffects, Color.White, layerDepth);
        }

        /// <summary>
        /// Moves the entity to another layer
        /// </summary>
        /// <param name="targetLayer">Target layer</param>
        public void ChangeLayers(int targetLayer)
        {
            World.Instance.GetForegroundLayer(layer).GetEntities(active).Remove(this);
            layer = targetLayer;
            World.Instance.GetForegroundLayer(layer).GetEntities(active).Add(this);
        }

        /// <summary>
        /// Changes the activity of the entity
        /// </summary>
        public void ChangeActivity()
        {
            World.Instance.GetForegroundLayer(layer).GetEntities(active).Remove(this);
            active = !active;
            World.Instance.GetForegroundLayer(layer).GetEntities(active).Add(this);
        }

        /// <summary>
        /// Compares the entity with the given obj
        /// </summary>
        /// <param name="obj">Obj to compare with</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Entity e = obj as Entity;
            int result;
            if (e != null)
                result = drawPriority.CompareTo(e.drawPriority);
            else
                result = 0;
            return result;
        }
        #endregion
    }
}
