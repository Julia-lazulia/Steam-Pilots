using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamPilots
{
    public class ItemStack
    {
        // The itemstack data
        public Item Item;
        public int StackSize;

        public ItemStack(Item item, int StackSize)
        {
            this.Item = item;
            this.StackSize = StackSize;
        }
    }
}
