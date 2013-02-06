using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class GuiInventory : GuiElement
    {
        public GuiItemContainer container;
        public ItemStack heldItem;

        public GuiInventory()
            : base()
        {
            container = new GuiItemContainer(50);
            container.SetBackground("Player/Inventory", Color.White);
            container.SetBackgroundPosition(new Vector2(Main.ScreenSize.X / 2, Main.ScreenSize.Y / 2));
            container.SetBackgroundSource(new Rectangle(0, 0, 245, 231));
            container.SetOrigin(new Vector2(container.background.Width / 2, container.background.Height / 2));
            for (int index = 0; index < container.slots.Length; index++)
            {
                int xOffset = (int)((((index % 10) * 24) + 9) * Main.guiScale);
                int yOffset;
                if (index < 10)
                    yOffset = (int)(10 * Main.guiScale);
                else
                    yOffset = (int)((((index / 10) * 24) + 42) * Main.guiScale);

                Vector2 position = new Vector2((this.container.backgroundPosition.X - (container.background.Width * Main.guiScale) / 2) + xOffset, (this.container.backgroundPosition.Y - (container.background.Height * Main.guiScale) / 2) + yOffset);
                container.slots[index] = new GuiSlot((int)position.X, (int)position.Y, Item.SpriteSize, Item.SpriteSize);
            }
        }

        public override void Draw(SpriteBatch s)
        {
            container.Draw(s);
        }

        public GuiSlot[] Slots()
        {
            return container.slots;
        }
    }
}
