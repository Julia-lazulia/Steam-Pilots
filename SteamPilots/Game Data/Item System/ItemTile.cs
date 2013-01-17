using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class ItemTile : Item, IPlaceable, IInventoryItem
    {
        public string ToolTip { get { return "ItemTileTooltip"; } set { ToolTip = value; } }
        public ItemTile(int ItemIndex, int TextureIndex)
            : base(ItemIndex, TextureIndex)
        {
            
        }

        /// <summary>
        /// Called when a tile is being placed
        /// </summary>
        /// <param name="player">The entity placing it</param>
        /// <param name="tile">The tile position</param>
        /// <returns></returns>
        public bool OnPlace(EntityPlayer player, Vector2 tile)
        {
            if (World.Instance.GetForegroundLayer(player.Layer).IsValidTile((int)tile.X, (int)tile.Y) && World.Instance.GetForegroundLayer(player.Layer).CanPlace((int)tile.X, (int)tile.Y, player.inventory.Slots()[player.currentSlot].ItemStack.Item.Tile) && player.inventory.container.RemoveItemStack(new ItemStack(player.inventory.Slots()[player.currentSlot].ItemStack.Item, 1)))
            {
                World.Instance.GetForegroundLayer(player.Layer).SetTile((int)tile.X, (int)tile.Y, player.inventory.Slots()[player.currentSlot].GetItem().Tile);
                return true;
            }
            return false;
        }
    }
}
