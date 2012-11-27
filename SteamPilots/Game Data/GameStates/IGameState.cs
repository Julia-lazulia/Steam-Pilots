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

        /// <summary>
        /// Enters the state
        /// </summary>
        void EnterState();

        /// <summary>
        /// Leaves the state
        /// </summary>
        void LeaveState();

        /// <summary>
        /// Updates the state
        /// </summary>
        /// <param name="gt"></param>
        void Update(GameTime gt);

        /// <summary>
        /// Draws the state
        /// </summary>
        /// <param name="sb"></param>
        void Draw(SpriteBatch sb);
    }
}
