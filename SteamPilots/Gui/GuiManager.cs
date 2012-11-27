using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class GuiManager
    {
        private List<GuiElement> Elements;

        public GuiManager()
        {
            Elements = new List<GuiElement>();
        }

        /// <summary>
        /// Adds a new gui element to the list of elements
        /// </summary>
        /// <param name="g"></param>
        public void AddGuiElement(GuiElement g)
        {
            Elements.Add(g);
        }

        /// <summary>
        /// Removes a gui element from the list of elements
        /// </summary>
        /// <param name="g"></param>
        public void RemoveGuiElement(GuiElement g)
        {
            Elements.Remove(g);
        }

        /// <summary>
        /// Updates the gui (it's elements)
        /// </summary>
        /// <param name="gt"></param>
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

        /// <summary>
        /// Draws the gui (it's elements)
        /// </summary>
        /// <param name="s"></param>
        public void Draw(SpriteBatch s)
        {
            for (var i = 0; i < Elements.Count; i++)
            {
                if (Elements[i].visible)
                {
                    Elements[i].Draw(s);
                }
            }
        }
    }
}
