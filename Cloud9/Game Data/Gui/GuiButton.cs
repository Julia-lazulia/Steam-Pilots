using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cloud9
{
    public class GuiButton : Gui
    {
        /// <summary>
        /// Not sure yet if more is needed, comments and other updates will be made tomorrow
        /// </summary>

        protected Vector2 Position;
        protected string Text;
        protected Vector2 Size;
        protected Color DrawColor;
        protected int Id;

        public bool enabled;
        public bool draw;

        public GuiButton(int id, Vector2 position, string text, Vector2 size)
        {
            Id = id;
            Position = position;
            Text = text;
            Size = size;
            DrawColor = Color.White;
            enabled = true;
            draw = true;
        }

        public GuiButton(int id, Vector2 position, string text, Vector2 size, Color color)
        {
            Id = id;
            Position = position;
            Text = text;
            Size = size;
            DrawColor = color;
            enabled = true;
            draw = true;
        }

        public void ChangeState()
        {
            enabled = !enabled;
        }

        public void SetDraw(bool draw)
        {
            this.draw = draw;
        }

        public void Draw()
        {
            World.Instance.SpriteBatch.Draw(new Texture2D(World.Instance.GraphicsDevice, (int)Size.X, (int)Size.Y), Position, DrawColor);
        }

        public void Draw(Texture2D texture)
        {
            World.Instance.SpriteBatch.Draw(texture, Position, DrawColor);
        }

        public bool Hovering(Vector2 position)
        {
            return (enabled && draw && position.X >= Position.X && position.Y >= Position.Y && position.X < Position.X + Size.X && position.Y < Position.Y + Size.Y);
        }
    }
}
