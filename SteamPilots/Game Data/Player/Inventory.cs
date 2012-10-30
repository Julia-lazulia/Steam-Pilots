using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamPilots
{
    public class Inventory
    {
        Dictionary<IInventoryItem, int> items;

        public Inventory()
        {
            items = new Dictionary<IInventoryItem, int>();
        }

        public void Add(IInventoryItem i, int count)
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

        public bool Remove(IInventoryItem i, int count)
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
