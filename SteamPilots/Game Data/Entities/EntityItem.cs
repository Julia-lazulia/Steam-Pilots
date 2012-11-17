using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class EntityItem : Entity
    {
        public ItemStack ItemStack;

        public EntityItem(ItemStack ItemStack, Vector2 Position)
            : base()
        {
            this.ItemStack = ItemStack;
            this.layer = 1;
            this.position = Vector2.Zero;
            this.collidesWithOtherEntities = true;
            this.collidesWithTiles = true;
            this.position = Position;
            this.active = true;
            this.boundingRect = new Rectangle((int)Position.X, (int)Position.Y, Item.SpriteSize, Item.SpriteSize);
            this.radius = 2f;
        }

        public override void OnCollide(Entity e)
        {
            if (e is EntityPlayer)
            {
                this.Destroy();
                ((EntityPlayer)e).inventory.container.AddItemStack(ItemStack);
            }
            base.OnCollide(e);
        }

        public override void Draw(SpriteBatch sb, float layerDepth)
        {
            Item.Items[ItemStack.ItemId].Draw(sb, position - World.Instance.CameraPosition);
        }
    }
}
