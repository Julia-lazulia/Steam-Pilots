using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class BackgroundLayer : Layer
    {
        protected Texture2D backgroundTexture; 

        public BackgroundLayer(float lDepth)
            : base(lDepth)
        {
            backgroundTexture = World.Content.Load<Texture2D>("background_1");
            visible = true;
        }

        // Background
        public override void Draw(SpriteBatch s)
        {
            s.Draw(backgroundTexture, new Rectangle(0, 0, (int)Main.ScreenSize.X, (int)Main.ScreenSize.Y), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, layerDepth);
            base.Draw(s);
        }
    }
}
