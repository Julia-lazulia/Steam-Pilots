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
        public GuiSelection selector;

        public GuiInventory()
            : base()
        {
            container = new GuiItemContainer(80);
            container.SetBackground("Player/Inventory", Color.White);
            container.SetBackgroundPosition(new Vector2(50, 50));
            container.SetSource(new Rectangle(0, 0, 245, 231));
            for (int index = 0; index < container.slots.Length; index++)
            {
                int xOffset = index * 24 + 9;
                int yOffset;
                if (index < 10)
                    yOffset = 9;
                else
                    yOffset = ((index % 24) * 24) + 19;

                Vector2 position = new Vector2(this.container.backgroundPosition.X + xOffset, this.container.backgroundPosition.Y + yOffset);
                container.slots[index] = new GuiSlot((int)position.X, (int)position.Y, Item.SpriteSize, Item.SpriteSize);
            }

            selector = new GuiSelection(new Vector2(container.backgroundPosition.X + 33, container.backgroundPosition.Y + 9));
        }

        public override void Draw(SpriteBatch s)
        {
            container.Draw(s);
            selector.DrawSelection(s, new Rectangle(232, 232, 24, 24));
        }

        public void UpdateSelection(int currentSlot)
        {
            selector.UpdatePosition(currentSlot);
        }

        public GuiSlot[] Slots()
        {
            return container.slots;
        }
    }
}
