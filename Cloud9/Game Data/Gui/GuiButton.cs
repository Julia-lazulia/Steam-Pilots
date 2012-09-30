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
        #region Properties
        protected Vector2 Position;
        protected string Text;
        protected Vector2 Size;
        protected Color DrawColor;
        protected int id;
        protected Texture2D texture;

        public Rectangle collisionRectangle;
        public bool enabled;
        public bool draw;

        public Texture2D Texture
        {
            get { return texture; }
        }

        public int Id
        {
            get { return id; }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Create a button
        /// </summary>
        /// <param name="id">Button id</param>
        /// <param name="position">Button position</param>
        /// <param name="text">Button text</param>
        /// <param name="size">Button size</param>
        public GuiButton(int id, Vector2 position, string text, Vector2 size)
        {
            Id = id;
            Position = position;
            Text = text;
            Size = size;
            DrawColor = Color.White;
            enabled = true;
            draw = true;
            collisionRectangle = new Rectangle((int)position.X, (int)position.Y, (int)Size.X, (int)Size.Y);
        }

        /// <summary>
        /// Create a button with special color
        /// </summary>
        /// <param name="id">Button id</param>
        /// <param name="position">Button position</param>
        /// <param name="text">Button text</param>
        /// <param name="size">Button size</param>
        /// <param name="color">Button color</param>
        public GuiButton(int id, Vector2 position, string text, Vector2 size, Color color)
        {
            this.id = id;
            Position = position;
            Text = text;
            Size = size;
            DrawColor = color;
            enabled = true;
            draw = true;
            collisionRectangle = new Rectangle((int)position.X, (int)position.Y, (int)Size.X, (int)Size.Y);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Change the button state
        /// </summary>
        public void ChangeState()
        {
            enabled = !enabled;
        }

        /// <summary>
        /// Change the draw state
        /// </summary>
        /// <param name="draw">Should draw</param>
        public void SetDraw(bool draw)
        {
            this.draw = draw;
        }

        /// <summary>
        /// Draw the button
        /// </summary>
        public void Draw()
        {
            World.Instance.SpriteBatch.Draw(Texture, Position, DrawColor);
            World.Instance.SpriteBatch.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), Text, Position, DrawColor);
        }

        /// <summary>
        /// Is mouse hovering on the button
        /// </summary>
        /// <param name="position">Mouse position</param>
        /// <returns></returns>
        public bool Hovering(Vector2 position)
        {
            return (enabled && draw && position.X >= Position.X && position.Y >= Position.Y && position.X < Position.X + Size.X && position.Y < Position.Y + Size.Y);
        }
        #endregion
    }
}
