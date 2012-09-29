using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Cloud9
{
    public abstract class Gui 
    {
        protected Vector2 Size;

        public void DrawRectangle(Rectangle rectangle, Color color)
        {
            World.Instance.SpriteBatch.Draw(new Texture2D(World.Instance.Game.GraphicsDevice, rectangle.Width, rectangle.Height), rectangle, color);
        }

        public void DrawRectangle(Texture2D texture, Rectangle rectangle, Color color)
        {
            World.Instance.SpriteBatch.Draw(texture, rectangle, color);
        }

        public void DrawText(string text, Vector2 position)
        {
            World.Instance.SpriteBatch.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), text, position, Color.White);
        }

        public void DrawText(string text, Vector2 position, Color color)
        {
            World.Instance.SpriteBatch.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), text, position, color);
        }
    }
}
