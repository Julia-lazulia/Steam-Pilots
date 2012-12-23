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
            container = new GuiItemContainer();
            container.SetBackground("Player/Inventory", Color.White);
            container.SetBackgroundPosition(new Vector2(50, 50));
        }

        public override void Draw(SpriteBatch s)
        {
            container.Draw(s);

            //Draw invetory items 
            //Martin: Draw them here, because we might have different inventory containers that don't have a spacing between the 1st and second item row.

            for (var i = 0; i < container.items.Length; i++)
            {
                if (container.items[i] != null)
                {
                    Item it = container.items[i].Item;

                    //Martin: Next line needs to be modified so we call the getsource on All Items instead of just ItemTile, so probably a GetSource method in Item class that we override in ItemTile           
                    Rectangle source = it.GetSource();

                    Vector2 drawPos;
                    int xOffset = i * 24 + 9;
                    int yOffset;
                    if(i < 10)
                        yOffset = ((i / 24) * 24) + 9;
                    else 
                        yOffset = ((i % 24) * 24) + 19;
                    
                    drawPos = new Vector2(this.container.backgroundPosition.X + xOffset, this.container.backgroundPosition.Y + yOffset);
                    Vector2 drawPosSize = new Vector2(drawPos.X + 7, drawPos.Y + 5);

                    s.Draw(it.GetTexture(), drawPos, source, Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.15f);
                    s.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), container.items[i].StackSize.ToString(), drawPosSize, Color.White, 0f, Vector2.Zero, 0.6f, SpriteEffects.None, 0.05f);
                }
            }
        }
    }
}
