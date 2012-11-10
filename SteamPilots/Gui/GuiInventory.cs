using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class GuiInventory : GuiManager
    {
        public GuiItemContainer container;

        public GuiInventory()
            : base()
        {
            container = new GuiItemContainer();
            container.SetBackground("Player/Inventory", Color.White);
            container.SetBackgroundPosition(new Vector2(50, 50));
            AddGuiElement(container);
        }
    }
}
