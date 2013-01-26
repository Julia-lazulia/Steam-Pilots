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
        protected Rectangle source;
        protected Vector2 position = Vector2.Zero;
        protected Vector2 origin = Vector2.Zero;
        protected float rotation = 0f;
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
            Vector2 mousePos = Input.Instance.MousePosition();
        }

        public virtual void Draw(SpriteBatch s)
        {
            s.Draw(tex, position, source, Color.White, rotation, origin, Main.guiScale, SpriteEffects.None, 0.50f);
        }

        public void SetOrigin(Vector2 origin)
        {
            this.origin = origin;
        }

        public bool Contains(Vector2 pos)
        {
            Point a = new Point((int)pos.X, (int)pos.Y);
            return this.boundingBox.Contains(a);
        }

        public void LClick()
        {
            if (LeftClick != null)
                LeftClick(this, EventArgs.Empty);
        }
        public void RClick()
        {
            if (LeftClick != null)
                RightClick(this, EventArgs.Empty);
        }
    }
}
