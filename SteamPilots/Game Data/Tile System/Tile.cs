﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class Tile : IGameData, IInventoryItem
    {
        #region Properties
        public const int TileSize = 16;
        public bool Collides;
        public int Index;
        public int TileIndex;
        public bool HasEdges;
        public InventoryType inventoryType;
        public string toolTip;
        public Vector2 Size;
        public String tileName;
        #endregion

        #region Static
        protected static Texture2D terrainTex = null;
        public static Tile[] tiles = new Tile[4096];
        public static Tile Air = new Tile(0, 0).SetCollides(false).SetHasEdges(false);
        public static Tile Dirt = new Tile(1, 0);
        public static Tile Grass = new Tile(2, 65);
        public static Tile Planks = new Tile(3, 99).SetType(InventoryType.INV_TILE);
        public static Tile test = new Tile(4, 99);
        public static Tile test2 = new Tile(5, 99);
        public static Tile test3 = new Tile(6, 99);
        public static Tile test4 = new Tile(7, 99);
        public static Tile test5 = new Tile(8, 99);
        public static Tile test6 = new Tile(9, 99);

        /// <summary>
        /// Get tile from index
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Tile from index</returns>
        public static Tile GetTile(byte index)
        {
            return tiles[index];
        }

        /// <summary>
        /// Get index from tile
        /// </summary>
        /// <param name="tile">tile</param>
        /// <returns></returns>
        public static byte GetByte(Tile tile)
        {
            return (byte)Array.IndexOf<Tile>(tiles, tile);
        }
        #endregion          

        #region Methods
        /// <summary>
        /// Create a new tile
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="tileIndex">Texture index</param>
        public Tile(int index, int tileIndex)
        {
            if(terrainTex == null)
            {
                terrainTex = World.Content.Load<Texture2D>("terrain");
            }
            if (tiles[index] != null)
                throw new ArgumentException("Index " + index + "is already occupied by " + tiles[index] + " when adding " + this);
            else
            {
                tiles[index] = this;
                Index = index;
                TileIndex = tileIndex;
                Collides = true;
                HasEdges = true;
                Size = new Vector2(1, 1);
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
        /// Set has edges
        /// </summary>
        /// <param name="hasEdges">Has edges?</param>
        /// <returns></returns>
        public Tile SetHasEdges(bool hasEdges)
        {
            HasEdges = hasEdges;
            return this;
        }

        /// <summary>
        /// Sets tile width and height using a vector
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Tile SetSize(Vector2 size)
        {
            Size = size;
            return this;
        }

        /// <summary>
        /// Sets the item type
        /// </summary>
        /// <param name="type">Inventory type</param>
        /// <returns></returns>
        public Tile SetType(InventoryType type)
        {
            InventoryType = type;
            return this;
        }

        public Tile SetTileName(string tileName)
        {
            this.tileName = tileName;
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
            return new Rectangle(x * TileSize, y * TileSize, (int)(TileSize * Size.X), (int)(TileSize * Size.Y));
        }

       
        /// <summary>
        /// Draw tile
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="layer">Layer</param>
        public void Draw(int x, int y, Layer layer)
        {
            Rectangle bounds = GetBounds(x, y);
            Rectangle source = GetSource();
            bounds.X -= (int)World.Instance.CameraPosition.X;
            bounds.Y -= (int)World.Instance.CameraPosition.Y;
            World.Instance.SpriteBatch.Draw(GetTextureFile(), bounds, source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, layer.LayerDepth);
        }

        /// <summary>
        /// Get source rectangle
        /// </summary>
        /// <returns></returns>
        public Rectangle GetSource()
        {
            int width = GetTextureFile().Width / TileSize;
            return new Rectangle((TileIndex % width) * TileSize, (TileIndex / width) * TileSize, (int)(TileSize * Size.X), (int)(TileSize * Size.Y));
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

        #region Tile properties < better name?
        /// <summary>
        /// Get texture file from tile
        /// </summary>
        /// <returns></returns>
        public virtual Texture2D GetTextureFile()
        {
            return terrainTex;
        }

        /// <summary>
        /// Activates when an entity collides with the tile
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnCollide(Entity e)
        {
        }
        #endregion

        public int InvImageIndex
        {
            get
            {
                return TileIndex;
            }
            set
            {
                TileIndex = value;
            }
        }

        public string ToolTip
        {
            get
            {
                return toolTip;
            }
            set
            {
                toolTip = value;
            }
        }

        public InventoryType InventoryType
        {
            get
            {
                return inventoryType;
            }
            set
            {
                inventoryType = value;
            }

        }
    }
}