using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamPilots
{
    public class GuiItemContainer : GuiContainer
    {
        Dictionary<IInventoryItem, int> items;

        public GuiItemContainer()
        {
            items = new Dictionary<IInventoryItem, int>();
        }

        public void AddItem(IInventoryItem i, int count)
        {
            if (items.ContainsKey(i))
            {
                items[i] += count;
            }
            else
            {
                items.Add(i, count);
            }
        }

        public bool RemoveItem(IInventoryItem i, int count)
        {
            if (items.ContainsKey(i))
            {
                if (items[i] > count)
                {
                    items[i] -= count;
                    return true;
                }
                else if (items[i] == count)
                {
                    items.Remove(i);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
