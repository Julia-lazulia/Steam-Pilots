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
        public String text;

        public GuiButton(int id, int x, int y, int width, int height, String text)
        {
            this.tex = new Texture2D(GameStateManager.Main.GraphicsDevice, width, height);
            uint[] black = new uint[width * height];
            for (int i = 0; i < width * height; i++)
                black[i] = new Color(0, 0, 0, 255).PackedValue;
            tex.SetData<uint>(black);
            this.id = id;
            this.position = new Vector2(x, y);
            this.boundingBox = new Rectangle(x, y, width, height);
            this.text = text;
        }

        public override void Draw(SpriteBatch s)
        {
            s.Draw(tex, position, null, Color.White, 0f, Vector2.Zero, Main.guiScale, SpriteEffects.None, 0.025f);
            s.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), text, position, Color.White, 0f, Vector2.Zero, Main.guiScale, SpriteEffects.None, 0.02f);
        }
    }
}
