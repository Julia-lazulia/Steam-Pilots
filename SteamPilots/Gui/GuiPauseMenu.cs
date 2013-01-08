using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SteamPilots
{
    public class GuiPauseMenu : GuiManager
    {
        GuiButton ButtonContinue;

        public GuiPauseMenu()
        {
            AddGuiElement(ButtonContinue = new GuiButton(0, 250, 250, 100, 50, "Continue"));
            ButtonContinue.LeftClick += LeftClickHandler;
            AddGuiElement(new GuiText(255, 225, "PAUSED"));
        }

        void LeftClickHandler(Object sender, EventArgs args)
        {
            GameStateManager.ReturnToLastState();
        }
    }
}
