using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class ItemTile : Item, IPlaceable
    {
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
            if (World.Instance.GetForegroundLayer(player.Layer).IsValidTile((int)tile.X, (int)tile.Y) && World.Instance.GetForegroundLayer(player.Layer).CanPlace((int)tile.X, (int)tile.Y, Tile.Tiles[player.currentTile]) && InRange(player, tile) && player.inventory.container.RemoveItem(new ItemStack(player.currentTile, 1)))
            {
                World.Instance.GetForegroundLayer(player.Layer).SetTile((int)tile.X, (int)tile.Y, Tile.Tiles[player.currentTile]);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks wether the selected tile is in range of the entity
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="tile">The tile position</param>
        /// <returns></returns>
        public bool InRange(Entity entity, Vector2 tile)
        {
            tile = new Vector2(tile.X * Tile.SpriteSize, tile.Y * Tile.SpriteSize);
            return Math.Ceiling(new Vector2(tile.X - entity.BoundingRect.Center.X, tile.Y - entity.BoundingRect.Center.Y).Length() / 16) < 4;
        }
    }
}
