using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class GuiSelection : GuiElement
    {
        Vector2 startPos = Vector2.Zero;

        public GuiSelection(Vector2 startPos)
        {
            tex = World.Content.Load<Texture2D>("Player/Selector");
            boundingBox = new Rectangle((int)startPos.X, (int)startPos.Y, 24, 24);
            this.startPos = startPos;
        }

        public void UpdatePosition(int slot)
        {
            boundingBox.Y = (int)(startPos.Y - (24 * slot));
        }
    }
}
