﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public interface IPlaceable
    {
        // Called when an item is being placed
        bool OnPlace(EntityPlayer player, Vector2 tile);
    }
}
