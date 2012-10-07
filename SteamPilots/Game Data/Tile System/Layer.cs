using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class Layer
    {
        #region Properties
        // Layer tiles
        byte[] tileData;

        // Collections with active and inactive entities
        Collection<Entity> InActiveEntities;
        Collection<Entity> ActiveEntities;

        // Width and Height
        int Width
        {
            get { return World.Width; }
        }
        int Height
        {
            get { return World.Height; }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initialize layer
        /// </summary>
        public void InitTileData()
        {
            tileData = new byte[Width * Height];
        }

        /// <summary>
        /// Initialize entity list
        /// </summary>
        public void InitEntityLists()
        {
            InActiveEntities = new Collection<Entity>();
            ActiveEntities = new Collection<Entity>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Does the layer have tiles?
        /// </summary>
        /// <returns></returns>
        public bool HasTiles()
        {
            return tileData != null;
        }

        /// <summary>
        /// Update the layer
        /// </summary>
        public void Update()
        {
            foreach (Entity e in ActiveEntities)
                e.Update();

            ActiveEntities.Update();
            InActiveEntities.Update();
        }

        /// <summary>
        /// Draw the layer
        /// </summary>
        public void Draw()
        {
            Entity[] sortedEntities = ActiveEntities.ToArray();
            Array.Sort<Entity>(sortedEntities);
            foreach (Entity e in sortedEntities)
                e.Draw();

            if (tileData != null)
            {
                int left = (int)(World.Instance.CameraPosition.X / Tile.TileSize);
                int right = (int)((World.Instance.CameraPosition.X + World.Resolution.X) / Tile.TileSize) + 1;
                int top = (int)(World.Instance.CameraPosition.Y / Tile.TileSize);
                int bottom = (int)((World.Instance.CameraPosition.Y + World.Resolution.Y) / Tile.TileSize) + 1;

                if (left < 0)
                    left = 0;
                if (right > Width)
                    right = Width;
                if (top < 0)
                    top = 0;
                if (bottom > Height)
                    bottom = Height;

                for (int x = left; x < right; x++)
                {
                    for (int y = top; y < bottom; y++)
                    {
                        if (IsValidTile(x, y))
                        {
                            Tile tile = GetTile(x, y);
                            if (tile != Tile.Air)
                                if (World.Instance.DrawTiles)
                                    tile.Draw(x, y, this);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get the tile from x and y coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns></returns>
        public Tile GetTile(int x, int y)
        {
            if (!IsValidTile(x, y))
                throw new IndexOutOfRangeException("Tile out of range in Tile.GetTile");
            return Tile.GetTile(tileData[y * Width + x]);
        }

        /// <summary>
        /// Are the coordinates valid?
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns></returns>
        public bool IsValidTile(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        /// <summary>
        /// Is it possible to place the tile at the given coordinates?
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="tile">tile to place</param>
        /// <returns></returns>
        public bool CanPlace(int x, int y, Tile tile)
        {
            Rectangle tileBounds = new Rectangle(x, y, (int)(x + tile.Size.X), (int)(y + tile.Size.Y));
            Entity[] entities = GetEntities(true).ToArray();
            for (int index = 0; index < entities.Length; index++)
            {
                Entity entity = entities[index];
                if (tileBounds.Intersects(entity.BoundingRect)) return false;
            }
            return true;
        }

        /// <summary>
        /// Set the tiles at the given coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="tile">Tile to place</param>
        public void SetTile(int x, int y, Tile tile)
        {
            if(!IsValidTile(x, y))
                throw new IndexOutOfRangeException("Tile out of range in Tile.SetTile");
            tileData[y * Width + x] = Tile.GetByte(tile);
        }

        /// <summary>
        /// Get the entity collection
        /// </summary>
        /// <param name="active">active entities?</param>
        /// <returns></returns>
        public Collection<Entity> GetEntities(bool active)
        {
            if (active)
                return ActiveEntities;
            else
                return InActiveEntities;
        }
        #endregion
    }
}
