using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class GuiItemContainer : GuiContainer
    {
        // The item list containing ItemStacks with data
        public ItemStack[] items;

        public GuiItemContainer()
        {
            items = new ItemStack[80];
        }

        /// <summary>
        /// Tries to add an item to the itemlist
        /// </summary>
        /// <param name="itemstack">Item to be added</param>
        /// <returns>Returns if it's added to the itemlist</returns>
        public bool AddItemStack(ItemStack itemstack)
        {
            for (int index = 0; index < items.Length; index++)
            {
                if (items[index] != null && items[index].Item.ItemIndex == itemstack.Item.ItemIndex)
                {
                    items[index].StackSize += itemstack.StackSize;
                    return true;
                }
            }
            for (int index = 0; index < items.Length; index++)
            {
                if (items[index] == null)
                {
                    items[index] = itemstack;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Tries to remove an item from the itemlist
        /// </summary>
        /// <param name="itemstack">Item to be removed</param>
        /// <returns>Returns if it's removed from the itemlist</returns>
        public bool RemoveItem(ItemStack itemstack)
        {
            for (int index = 0; index < items.Length; index++)
            {
                if (items[index] != null)
                {
                    if (items[index].Item.ItemIndex == itemstack.Item.ItemIndex && items[index].StackSize > itemstack.StackSize)
                    {
                        items[index].StackSize -= itemstack.StackSize;
                        return true;
                    }
                    else if (items[index].Item.ItemIndex == itemstack.Item.ItemIndex && items[index].StackSize == itemstack.StackSize)
                    {
                        items[index] = null;
                        return true;
                    }
                }
            }
            return false;
        }

        public override void Draw(SpriteBatch s)
        {
            if (background != null)
                s.Draw(background, backgroundPosition, null, backgroundColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.20f);
        }
    }
}
