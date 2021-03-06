﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SteamPilots
{
    public class GuiHotbar : GuiElement
    {
        GuiItemContainer container;
        GuiSelection selector;

        public GuiHotbar()
        {
            int height = 244, width = 32;
            boundingBox = new Rectangle(0, (int)(Main.ScreenSize.Y / 2) - height, height, width);
            position = new Vector2(boundingBox.X, boundingBox.Y);

            container = new GuiItemContainer(10);
            container.SetBackground("Player/Hotbar", Color.White);
            container.SetBackgroundPosition(position);
            for (int index = 0; index < container.slots.Length; index++)
            {
                int xOffset = (int)(9 * Main.guiScale);
                int yOffset = (int)((index * 24) * Main.guiScale);

                Vector2 pos = new Vector2(container.backgroundPosition.X + xOffset, container.backgroundPosition.Y + yOffset);
                container.slots[index] = new GuiSlot((int)pos.X, (int)pos.Y, Item.SpriteSize, Item.SpriteSize);
            }

            selector = new GuiSelection(new Vector2(position.X + 9, position.Y + (24 * 9) /*Temporary to fix scrolling inverted*/));
        }

        public override void Draw(SpriteBatch s)
        {
            container.Draw(s);
            selector.Draw(s);
        }

        public void UpdateSelector(int slot)
        {
            selector.UpdatePosition(slot);
        }

        public override void Update(GameTime gt)
        {
            for (int index = 0; index < container.slots.Length; index++)
                container.slots[index].ItemStack = World.player.inventory.Slots()[index].ItemStack;
        }

        public GuiSlot[] Slots()
        {
            return container.slots;
        }
    }
}
