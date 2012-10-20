using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SteamPilots
{
    public class GuiButton : GuiElement
    {
        public int id;

        public GuiButton(int id, int x, int y, int width, int height)
        {
            this.id = id;
            this.position = new Vector2(x, y);
            this.boundingBox = new Rectangle(x, y, width, height);
        }

        public override void Draw()
        {
            World.Instance.SpriteBatch.Draw(tex, boundingBox, Color.White);
            base.Draw();
        }
    }
}
