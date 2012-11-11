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
        public int SpriteSize = 16;
        
        public Texture2D SpriteFile;
        #endregion

        #region Class Properties
        public static Item[] Items = new Item[4096];
        public static Item Air = new Item(0, 0).SetInventoryType(InventoryType.INV_TILE);
        public static Item Dirt = new Item(1, 0).SetInventoryType(InventoryType.INV_TILE).SetItemName("Dirt");
        public static Item Grass = new Item(2, 1).SetInventoryType(InventoryType.INV_TILE).SetItemName("Grass");
        public static Item Planks = new Item(3, 2).SetInventoryType(InventoryType.INV_TILE).SetItemName("Planks");
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
        public Item SetInventoryType(InventoryType inventoryType)
        {
            this.InventoryType = inventoryType;
            return this;
        }
        
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

        public Item SetItemName(String itemName)
        {
            this.ItemName = itemName;
            return this;
        }

        public Rectangle GetSource()
        {
            int width = SpriteFile.Width / SpriteSize;
            return new Rectangle((ItemIndex % width) * SpriteSize, (ItemIndex / width) * SpriteSize, SpriteSize, SpriteSize);
        }

        public void Draw(SpriteBatch sb, Vector2 position)
        {
            sb.Draw(SpriteFile, position, GetSource(), Color.White);
        }

        public int CompareTo(Object o)
        {
            return 0;
        }
        #endregion

        #region No name yet
        public virtual void OnUse(EntityPlayer player)
        {
        }
        #endregion
    }
}
