using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cloud9
{
    public class GuiMenu : Gui
    {
        public Collection<GuiButton> Buttons;

        // NEEDS TO HAVE PRESSING BUTTONS AND WHAT TO DO ETC

        public GuiMenu(Collection<GuiButton> buttons)
        {
            Buttons = buttons;
        }

        public void MousePressed(Vector2 position)
        {
        }
    }
}
