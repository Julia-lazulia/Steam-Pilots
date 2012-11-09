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
        GuiButton button;

        public GuiInventory()
            : base()
        {
            AddGuiElement(button = new GuiButton(0, 50, 50, 100, 100));
            button.visible = true;
        }
    }
}
