using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public interface IPlaceable
    {
        // Called when a tile is being placed
        bool OnPlace(EntityPlayer player, Vector2 tile);

        // Called when a tile is being broken
        bool OnBreak(EntityPlayer player, Vector2 tile);

        // Checks wether the selected tile is in range of the entity
        Boolean InRange(Entity entity, Vector2 tile);
    }
}
