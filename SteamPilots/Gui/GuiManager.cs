using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    class GuiManager
    {
        private List<GuiElement> Elements;

        public GuiManager()
        {
            Elements = new List<GuiElement>();
        }

        public void AddGuiElement(GuiElement g)
        {
            Elements.Add(g);
        }

        public void RemoveGuiElement(GuiElement g)
        {
            Elements.Remove(g);
        }

        public void Update(GameTime gt)
        {
            Input tIn = Input.Instance;
            Vector2 mPos = tIn.MousePosition();

            for (var i = 0; i < Elements.Count; i++)
            {
                if (Elements[i].visible)
                {
                    if (Elements[i].Contains(mPos))
                    {
                        if (tIn.MouseLeftButtonNewPressed())
                        {
                            Elements[i].LClick();
                        }
                        if (tIn.MouseRightButtonNewPressed())
                        {
                            Elements[i].RClick();
                        }
                    }
                }
                Elements[i].Update(gt);                
            }

        }
    }
}
