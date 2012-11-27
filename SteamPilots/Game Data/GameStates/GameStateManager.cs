using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    static class GameStateManager
    {
        static IGameState currentState;
        static IGameState lastState;
        static IGameState nextState;

        public static Main Main;

        public static void Initialize(IGameState startState)
        {
            startState.EnterState();
            nextState = startState;
        }

        public static void SwitchState(IGameState newState)
        {
            currentState.LeaveState();
            newState.EnterState();
            nextState = newState;
        }

        public static void ReturnToLastState()
        {
            currentState.LeaveState();
            if (!lastState.IsReady)
                lastState.EnterState();
            nextState = lastState;
        }

        public static void Update(GameTime gt)
        {
            if (nextState != null)
            {
                if (nextState.IsReady)
                {
                    lastState = currentState;
                    currentState = nextState;
                    nextState = null;
                }
            }
            if (currentState != null)
            {
                currentState.Update(gt);
            }
        }

        public static void Draw(SpriteBatch sb)
        {
            if (currentState != null)
            {
                currentState.Draw(sb);
            }
        }
    }
}
