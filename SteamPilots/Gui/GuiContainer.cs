using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class GuiContainer : GuiElement
    {
        public Texture2D background;
        public Vector2 backgroundPosition;
        protected Rectangle backgroundSource;
        protected Color backgroundColor = Color.White;
        protected List<GuiElement> Elements;
        
        public GuiContainer()
        {
            Elements = new List<GuiElement>();
        }

        public void SetBackground(String path, Color backgroundColor)
        {
            background = World.Content.Load<Texture2D>(path);
            this.backgroundColor = backgroundColor;
        }

        public void SetBackgroundPosition(Vector2 pos)
        {
            backgroundPosition = pos;
        }

        public void SetBackgroundSource(Rectangle rect)
        {
            source = rect;
        }

        public override void Draw(SpriteBatch s)
        {
            if (background != null)
                s.Draw(background, backgroundPosition, source, backgroundColor, 0f, Vector2.Zero, Main.guiScale, SpriteEffects.None, 0.25f);

            for (var i = 0; i < Elements.Count; i++)
            {
                if (Elements[i].visible)
                {
                    Elements[i].Draw(s);
                }
            }
        }

        public void AddGuiElement(GuiElement g)
        {
            Elements.Add(g);
        }

        public void RemoveGuiElement(GuiElement g)
        {
            Elements.Remove(g);
        }

        public override void Update(GameTime gt)
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
