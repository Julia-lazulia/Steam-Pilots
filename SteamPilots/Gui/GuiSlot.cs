using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class GuiSlot : GuiElement
    {
        public ItemStack ItemStack;

        public GuiSlot(int x, int y, int width, int height)
        {
            this.position = new Vector2(x, y);
            this.boundingBox = new Rectangle(x, y, width, height);
            LeftClick += LClick;
        }

        public GuiSlot(int x, int y, int width, int height, ItemStack itemStack)
        {
            this.position = new Vector2(x, y);
            this.boundingBox = new Rectangle(x, y, width, height);
            ItemStack = itemStack;
        }

        public override void Draw(SpriteBatch s)
        {
            if (ItemStack != null)
            {
                Vector2 sizeOffset = new Vector2(7 * Main.guiScale, 5 * Main.guiScale);
                s.Draw(ItemStack.Item.GetTexture(), position, ItemStack.Item.GetSource(), Color.White, 0f, Vector2.Zero, 0.8f * Main.guiScale, SpriteEffects.None, 0.015f);
                s.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), ItemStack.StackSize.ToString(), position + sizeOffset, Color.White, 0f, Vector2.Zero, 0.6f * Main.guiScale, SpriteEffects.None, 0.01f);            
            }
        }

        public Item GetItem()
        {
            return ItemStack.Item;
        }

        void LClick(Object sender, EventArgs args)
        {
            Console.WriteLine("TEST?!");
            if (World.player.heldStack != null)
            {
                if (ItemStack != null)
                {
                    ItemStack copyStack = ItemStack;
                    ItemStack = World.player.heldStack;
                    World.player.heldStack = copyStack;
                }
                else
                {
                    ItemStack = World.player.heldStack;
                    World.player.heldStack = null;
                }
            }
            else
            {
                World.player.heldStack = ItemStack;
                ItemStack = null;
            }
        }
    }
}
