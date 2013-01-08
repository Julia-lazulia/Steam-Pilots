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

        public GuiInventory()
            : base()
        {
            container = new GuiItemContainer(80);
            container.SetBackground("Player/Inventory", Color.White);
            container.SetBackgroundPosition(new Vector2(50, 50));
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
