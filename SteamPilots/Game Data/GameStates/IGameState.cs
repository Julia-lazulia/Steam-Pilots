using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public interface IGameState
    {
        bool IsReady { get; set; }

        void EnterState();

        void LeaveState();

        void Update(GameTime gt);

        void Draw(SpriteBatch sb);
    }
}
