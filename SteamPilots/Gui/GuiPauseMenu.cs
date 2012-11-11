using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamPilots
{
    public class GuiPauseMenu : GuiManager
    {
        public GuiPauseMenu()
        {
            AddGuiElement(new GuiButton(0, 250, 250, 100, 50, "Continue"));
            AddGuiElement(new GuiText(255, 225, "PAUSED"));
        }
    }
}
