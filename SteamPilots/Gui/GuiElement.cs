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
        protected Vector2 position = Vector2.Zero;
        protected Vector2 origin = Vector2.Zero;
        protected Texture2D tex;
        protected float scale = 1f;
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
            if (Input.Instance.MouseLeftButtonNewPressed() && boundingBox.Contains((int)mousePos.X, (int)mousePos.Y))
                LeftClick((this), new EventArgs());
        }

        public virtual void Draw(SpriteBatch s)
        {
            s.Draw(tex, boundingBox, Color.White);
        }

        public virtual void DrawSelection(SpriteBatch s, Rectangle source)
        {
            s.Draw(tex, boundingBox, source, Color.White);
        }

        public void SetOrigin(Vector2 origin)
        {
            this.origin = origin;
        }

        public void SetScale(float scale)
        {
            this.scale = scale;
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
