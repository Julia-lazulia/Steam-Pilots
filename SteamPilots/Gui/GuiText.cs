using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class GuiText : GuiElement
    {
        public float x = 0, y = 0;
        public String text = "";

        public GuiText(float x, float y, String text)
        {
            this.x = x;
            this.y = y;
            this.text = text;
        }

        public override void Draw(SpriteBatch s)
        {
            s.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), text, new Vector2(x, y), Color.Blue);
        }
    }
}
