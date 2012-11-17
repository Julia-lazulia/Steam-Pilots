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
            return new Rectangle(x * SpriteSize, y * SpriteSize, (int)(SpriteSize * TileSize.X), (int)(SpriteSize * TileSize.Y));
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

        public virtual void OnBreak(Entity e)
        {

        }
        #endregion
    }
}