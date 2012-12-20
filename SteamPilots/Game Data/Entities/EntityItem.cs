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
        // The itemstack which the entity refers to
        public ItemStack ItemStack;
        public float scale; 

        public EntityItem(ItemStack ItemStack, Vector2 Position, float scale)
            : base()
        {
            this.ItemStack = ItemStack;
            this.layer = 2;
            this.position = Vector2.Zero;
            this.collidesWithOtherEntities = true;
            this.collidesWithTiles = true;
            this.position = Position;
            this.active = true;
            this.scale = scale;
            this.tileWidth = 1;
            this.tileHeight = 1;
            this.boundingRect = new Rectangle((int)Position.X, (int)Position.Y, Item.SpriteSize, Item.SpriteSize);
            this.radius = 2f;
            this.velocity = new Vector2(0f, 100f);
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
            Item.Items[ItemStack.Item.ItemIndex].Draw(sb, position - World.Instance.CameraPosition, this.scale);
        }
    }
}
