using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{

    class GameStateManager
    {
        private IGameState currentState; 

        public GameStateManager(IGameState startState)
        {
            currentState = startState;
            currentState.EnterState();
        }

        public void SwitchState(IGameState newState)
        {
            currentState.LeaveState();
            currentState = newState;
            currentState.EnterState();
        }

        public void Update(GameTime gt)
        {
            currentState.Update(gt);
        }

        public void Update(SpriteBatch sb)
        {
            currentState.Draw(sb);
        }
    }
}
