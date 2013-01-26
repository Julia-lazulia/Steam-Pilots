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
        public GuiSlot[] slots;

        public GuiItemContainer(int slots)
        {
            this.slots = new GuiSlot[slots];
        }

        /// <summary>
        /// Tries to add an item to the itemlist
        /// </summary>
        /// <param name="ItemStack">Item to be added</param>
        /// <returns>Returns if it's added to the itemlist</returns>
        public bool AddItemStack(ItemStack ItemStack)
        {
            for (int index = 0; index < slots.Length; index++)
            {
                if (slots[index].ItemStack != null && slots[index].ItemStack.Item.ItemIndex == ItemStack.Item.ItemIndex && !(slots[index].ItemStack.StackSize + ItemStack.StackSize > 99))
                {
                    slots[index].ItemStack.StackSize += ItemStack.StackSize;
                    return true;
                }
            }
            for (int index = 0; index < slots.Length; index++)
            {
                if (slots[index].ItemStack == null)
                {
                    slots[index].ItemStack = ItemStack;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Tries to remove an item from the itemlist
        /// </summary>
        /// <param name="ItemStack">Item to be removed</param>
        /// <returns>Returns if it's removed from the itemlist</returns>
        public bool RemoveItemStack(ItemStack ItemStack)
        {
            for (int index = 0; index < slots.Length; index++)
            {
                if (slots[index] != null)
                {
                    if (slots[index].ItemStack.Item.ItemIndex == ItemStack.Item.ItemIndex && slots[index].ItemStack.StackSize > ItemStack.StackSize)
                    {
                        slots[index].ItemStack.StackSize -= ItemStack.StackSize;
                        return true;
                    }
                    else if (slots[index].ItemStack.Item.ItemIndex == ItemStack.Item.ItemIndex && slots[index].ItemStack.StackSize == ItemStack.StackSize)
                    {
                        slots[index].ItemStack = null;
                        return true;
                    }
                }
            }
            return false;
        }

        public override void Draw(SpriteBatch s)
        {
            if (background != null)
                s.Draw(background, backgroundPosition, backgroundSource, backgroundColor, 0f, origin, Main.guiScale, SpriteEffects.None, 0.20f);

            for (int index = 0; index < slots.Length; index++)
            {
                slots[index].Draw(s);
            }
        }
    }
}
