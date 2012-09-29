using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Cloud9
{
    public class TileTrampoline : Tile
    {
        public TileTrampoline(int index, int tileIndex)
            : base(index, tileIndex)
        {
            
        }

        public override void OnCollide(Entity e)
        {
            e.Velocity += new Vector2(0, -300);
        }
    }
}
