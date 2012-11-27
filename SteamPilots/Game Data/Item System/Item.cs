using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class Item : IGameData, IInventoryItem
    {
        #region Item Properties
        protected String toolTip = "";
        public String ToolTip
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
        protected Tile tile;
        public Tile Tile
        {
            get
            {
                if (tile != null)
                {
                    return tile;
                }
                else
                {
                    throw new NullReferenceException("This item has no tile connected");
                }
            }
            set
            {
                tile = value;
            }
        }
        protected InventoryType inventoryType = InventoryType.INV_ITEM;
        public InventoryType InventoryType
        {
            get
            {
                return inventoryType;
            }
            set
            {
                if (value == InventoryType.INV_TILE)
                {
                    this.SpriteFile = Tile.Tiles[this.ItemIndex].SpriteFile; 
                }
                inventoryType = value;
            }
        }
        public String ItemName = "";
        public int ItemIndex;
        public int TextureIndex;
        public Texture2D SpriteFile;
        #endregion

        #region Class Properties
        public const int SpriteSize = 16;
        public static Item[] Items = new Item[4096];

        // List of all items
        public static Item Dirt = new ItemTile(1, 0).SetInventoryType(InventoryType.INV_TILE).SetTile(Tile.Dirt).SetItemName("Dirt");
        public static Item Grass = new ItemTile(2, 65).SetInventoryType(InventoryType.INV_TILE).SetTile(Tile.Grass).SetItemName("Grass");
        public static Item Planks = new ItemTile(3, 99).SetInventoryType(InventoryType.INV_TILE).SetTile(Tile.Planks).SetItemName("Planks");
        #endregion

        #region Initialization
        public Item(int ItemIndex, int TextureIndex)
        {
            if (Items[ItemIndex] != null)
                throw new ArgumentOutOfRangeException("Id " + ItemIndex + " is already occupied by " + Items[ItemIndex] + " when adding " + this);
            if (SpriteFile == null)
                SpriteFile = World.Content.Load<Texture2D>("items");
            this.ItemIndex = ItemIndex;
            this.TextureIndex = TextureIndex;
            Items[ItemIndex] = this;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the inventory type of the used variable to the given one
        /// </summary>
        /// <param name="inventoryType">The given InventoryType</param>
        /// <returns></returns>
        public Item SetInventoryType(InventoryType inventoryType)
        {
            this.InventoryType = inventoryType;
            return this;
        }
        
        /// <summary>
        /// Sets the sprite file of the used variable to the given one
        /// </summary>
        /// <param name="path">The given path</param>
        /// <returns></returns>
        public Item SetSpriteFile(String path)
        {
            try
            {
                this.SpriteFile = World.Content.Load<Texture2D>(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not find sprite path for item " + this);
            }
            return this;
        }

        /// <summary>
        /// Sets the item name of the used variable to the given one
        /// </summary>
        /// <param name="itemName">The given item name</param>
        /// <returns></returns>
        public Item SetItemName(String itemName)
        {
            this.ItemName = itemName;
            return this;
        }

        /// <summary>
        /// Connects the used variable to the given tile
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public Item SetTile(Tile tile)
        {
            this.tile = tile;
            return this;
        }

        /// <summary>
        /// Gets the connected tile from the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Tile GetTile(Item item)
        {
            return this.Tile;
        }

        /// <summary>
        /// Gets the source rectangle for the texture
        /// </summary>
        /// <returns></returns>
        public Rectangle GetSource()
        {
            int width = SpriteFile.Width / SpriteSize;
            return new Rectangle((ItemIndex % width) * SpriteSize, (ItemIndex / width) * SpriteSize, SpriteSize, SpriteSize);
        }

        /// <summary>
        /// Draws the item on the given position (used in for example guis)
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="position"></param>
        public void Draw(SpriteBatch sb, Vector2 position)
        {
            sb.Draw(SpriteFile, position, GetSource(), Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
        }

        public int CompareTo(Object o)
        {
            return 0;
        }
        #endregion

        #region No name yet
        /// <summary>
        /// Called when the item is used by an entity
        /// </summary>
        /// <param name="player">The entity using it</param>
        /// <returns>Returns if the item is used</returns>
        public virtual bool OnUse(EntityPlayer player)
        {
            return false;
        }
        #endregion
    }
}
