using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamPilots
{
    public enum InventoryType
    {
        INV_TOOL,
        INV_TILE,
        INV_ITEM,
    }

    public interface IInventoryItem
    {
        string ToolTip { get; set; }
        InventoryType InventoryType { get; set; }
    }
}
