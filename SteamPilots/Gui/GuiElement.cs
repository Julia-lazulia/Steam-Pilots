using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class GuiElement
    {
        protected Rectangle boundingBox;
        protected Vector2 position;
        protected Texture2D tex;
        public Boolean visible = true;

        public event EventHandler LeftClick;
        public event EventHandler RightClick;
        public event EventHandler MouseOn;
        public event EventHandler MouseOff;

        public GuiElement()
        {
        }

        public virtual void Update(GameTime gt)
        {
        }

        public virtual void Draw(SpriteBatch s)
        {
            s.Draw(tex, this.boundingBox, Color.White);
        }

        public bool Contains(Vector2 pos)
        {
            Point a = new Point((int)pos.X, (int)pos.Y);
            return this.boundingBox.Contains(a);
        }

        public void LClick()
        {
            LeftClick(this, EventArgs.Empty);
        }
        public void RClick()
        {
            RightClick(this, EventArgs.Empty);
        }
    }
}
