using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class Tile : IGameData
    {
        #region Properties
        public bool Collides;
        public int SpriteIndex;
        public int TileIndex;
        public Vector2 TileSize;
        public Texture2D SpriteFile;
        #endregion

        #region Static
        public const int SpriteSize = 16;
        public static Tile[] Tiles = new Tile[4096];
        public static Tile Air = new Tile(0, 0).SetCollides(false);
        public static Tile Dirt = new Tile(1, 0);
        public static Tile Grass = new Tile(2, 65);
        public static Tile Planks = new Tile(3, 99);
        public static float brokenTileScale = 0.8f;
        #endregion

        #region Methods
        /// <summary>
        /// Create a new tile
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="tileIndex">Texture index</param>
        public Tile(int TileIndex, int SpriteIndex)
        {
            if (SpriteFile == null)
            {
                SpriteFile = World.Content.Load<Texture2D>("terrain");
            }
            if (Tiles[TileIndex] != null)
                throw new ArgumentException("Index " + SpriteIndex + "is already occupied by " + Tiles[SpriteIndex] + " when adding " + this);
            else
            {
                Tiles[TileIndex] = this;
                this.SpriteIndex = SpriteIndex;
                this.TileIndex = TileIndex;
                Collides = true;
                TileSize = new Vector2(1, 1);
            }
        }

        /// <summary>
        /// Set block collides
        /// </summary>
        /// <param name="collides">Collides?</param>
        /// <returns></returns>
        public Tile SetCollides(bool collides)
        {
            Collides = collides;
            return this;
        }

        /// <summary>
        /// Sets tile width and height using a vector
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Tile SetSize(Vector2 TileSize)
        {
            this.TileSize = TileSize;
            return this;
        }

        /// <summary>
        /// Get bounds from coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns></returns>
        public Rectangle GetBounds(int x, int y)
        {
            return new Rectangle(x * SpriteSize  , y * SpriteSize, (int)(SpriteSize * TileSize.X), (int)(SpriteSize * TileSize.Y));
        }

        /// <summary>
        /// Draw tile
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="layer">Layer</param>
        public void Draw(int x, int y, Layer layer, SpriteBatch sb)
        {
            Rectangle bounds = GetBounds(x, y);
            Rectangle source = GetSource();
            bounds.X -= (int)World.Instance.CameraPosition.X;
            bounds.Y -= (int)World.Instance.CameraPosition.Y;
            sb.Draw(SpriteFile, bounds, source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, layer.LayerDepth);
        }

        /// <summary>
        /// Get source rectangle
        /// </summary>
        /// <returns></returns>
        public Rectangle GetSource()
        {
            int width = SpriteFile.Width / SpriteSize;
            return new Rectangle((SpriteIndex % width) * SpriteSize, (SpriteIndex / width) * SpriteSize, (int)(SpriteSize * TileSize.X), (int)(SpriteSize * TileSize.Y));
        }

        /// <summary>
        /// Compare tile to obj
        /// </summary>
        /// <param name="obj">Comparing obj</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            return 0;
        }
        #endregion

        #region No name yet
        /// <summary>
        /// Activates when an entity collides with the tile
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnCollide(Entity e)
        {
        }

        /// <summary>
        /// Called when a tile is being broken
        /// </summary>
        /// <param name="player">The entity breaking it</param>
        /// <param name="tile">The tile position</param>
        /// <returns></returns>
        public bool OnBreak(EntityPlayer player, Vector2 tile)
        {
            if(!InRange(player, tile)) return false;
            // Need to change the tile index into getting the item from the tile id
            // tile + World.Instance.CameraPosition
            Vector2 dropP = new Vector2((float)((int)tile.X * Tile.SpriteSize), (float)((int)tile.Y * Tile.SpriteSize));
            //dropP.X -= (int)World.Instance.CameraPosition.X;
            //dropP.Y -= (int)World.Instance.CameraPosition.Y;
            EntityItem droppedItem = new EntityItem(new ItemStack(Item.Items[World.Instance.GetForegroundLayer(player.Layer).GetTile((int)tile.X, (int)tile.Y).TileIndex], 1), new Vector2(dropP.X, dropP.Y), brokenTileScale);
            droppedItem.Spawn();

            //Loop trough surrounding entities and make sure they start falling again 
            Collection<Entity> col = World.Instance.GetForegroundLayer(player.Layer).GetEntities(true);
            var bBox = new Rectangle((int)dropP.X - 20, (int)dropP.Y - 20, SpriteSize * 2, SpriteSize * 2);
            foreach (var item in col)
            {
                if (bBox.Contains(item.BoundingRect))
                {
                    item.Velocity += new Vector2(0, 100f);
                }

            }
            return true;
        }

        public bool InRange(Entity entity, Vector2 tile)
        {
            tile = new Vector2(tile.X * Tile.SpriteSize, tile.Y * Tile.SpriteSize);
            return Math.Ceiling(new Vector2(tile.X - entity.BoundingRect.Center.X, tile.Y - entity.BoundingRect.Center.Y).Length() / 16) < 4;
        }
        #endregion
    }
}