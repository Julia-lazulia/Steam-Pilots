using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Cloud9
{
    public class GuiMainMenu : GuiMenu
    {
        public GuiMainMenu()
        {
            Buttons.Add(new GuiButton(0, new Vector2(100, 100), "test", new Vector2(16, 16)));
        }
    }
}
