using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cloud9
{
    public abstract class GuiMenu : Gui
    {
        public Collection<GuiButton> Buttons = new Collection<GuiButton>();

        public GuiMenu()
        {
        }

        public void MousePressed(Vector2 position)
        {
            Rectangle mousePosition = new Rectangle((int)position.X, (int)position.Y, 1, 1);
            foreach (GuiButton button in Buttons)
                if (button.collisionRectangle.Intersects(mousePosition)) ButtonPressed(button.Id);
        }

        public void Update()
        {
            if (Input.Instance.MouseLeftButtonNewPressed() || Input.Instance.MouseRightButtonNewPressed())
                MousePressed(Input.Instance.MousePosition());
        }

        public abstract void ButtonPressed(int id);

        public void Draw()
        {
            foreach (GuiButton button in Buttons)
            {
                button.Draw();
                if (button.Hovering(Input.Instance.MousePosition()))
                    Console.WriteLine("Hovering");
            }
        }
    }
}
