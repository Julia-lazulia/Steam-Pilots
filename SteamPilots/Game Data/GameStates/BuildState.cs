using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SteamPilots
{
    class BuildState : IGameState
    {
        private bool _isReady;
        Texture2D hangarTex;
        Vector2 hangarPos;

        public void EnterState()
        {
            hangarTex = GameStateManager.Main.Content.Load<Texture2D>("Hangarship2");
            hangarPos = new Vector2(Main.ScreenSize.X / 2 - hangarTex.Width / 2, Main.ScreenSize.Y / 2 - hangarTex.Height / 2);
            _isReady = true;
        }

        public void LeaveState()
        {
            //TODO: Save the building state
        }

        public void Update(GameTime gt)
        {
            if (Input.Instance.KeyNewPressed(Keys.B))
            {
                GameStateManager.ReturnToLastState();
            }            
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(hangarTex, hangarPos, Color.White);
        }

        public bool IsReady
        {
            get
            {
                return _isReady;
            }
            set
            {
                _isReady = value;
            }
        }
    }
}
