using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamPilots
{
    public class ItemStack
    {
        // The itemstack data
        public int ItemId;
        public int StackSize;

        public ItemStack(int ItemId, int StackSize)
        {
            this.ItemId = ItemId;
            this.StackSize = StackSize;
        }
    }
}
