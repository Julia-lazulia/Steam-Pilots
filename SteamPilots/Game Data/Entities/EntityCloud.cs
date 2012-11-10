using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SteamPilots
{
    public class EntityCloud : Entity
    {
        static Texture2D cloudTex;

        public EntityCloud(Vector2 pos, float speed) : base()
        {        
            layer = 0;
            position = pos;
            velocity = World.Instance.WindDirection * speed;
            collidesWithTiles = false;
            collidesWithOtherEntities = false;
            gravityEffect = 0f;
            active = true;
            radius = 2f;
            tileHeight = 2;
            tileWidth = 2;
            spriteEffects = SpriteEffects.None;
            boundingRect = new Rectangle((int)position.X, (int)position.Y, cloudTex.Width, cloudTex.Height);
        }

        public static void Initialize()
        {
            cloudTex = World.Content.Load<Texture2D>(@"Env\cld_small");
        }

        public override void Update()
        {
            base.Update();
            boundingRect.X = (int)position.X;
            boundingRect.Y = (int)position.Y;
        }

        public override void Draw(SpriteBatch sb, float layerDepth)
        {
            if (World.Instance.CameraViewRect.Contains(boundingRect))
            {
                sb.Draw(cloudTex, position - World.Instance.CameraPosition, null, Color.White, 0f, Vector2.Zero, 1f, spriteEffects, layerDepth);
            }
        }
    }
}
