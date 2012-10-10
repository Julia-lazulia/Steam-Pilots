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
        INV_ENTITY,
    }

    public interface IInventoryItem
    {
        int InvImageIndex { get; set; }
        string ToolTip { get; set; }
        InventoryType InventoryType { get; set; }
    }
}
