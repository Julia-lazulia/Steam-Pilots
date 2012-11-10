using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    class StartState : IGameState   
    {
        private bool _isReady = false;
        private Texture2D logoTex = null;
        private Vector2 logoPos;
        private float duration = 2f;

        public void EnterState()
        {
            logoTex = World.Content.Load<Texture2D>("Quickmockuplogo2");
            logoPos = new Vector2(Main.ScreenSize.X / 2 - logoTex.Width / 2, Main.ScreenSize.Y / 2 - logoTex.Height / 2);
            _isReady = true;            
        }

        public void LeaveState()
        {
        }

        public void Update(GameTime gt)
        {
            duration -= (float)gt.ElapsedGameTime.TotalSeconds;
            if (duration <= 0f)
            {
                GameStateManager.SwitchState(new PlayState());
                duration = 1000f;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(logoTex, logoPos, Color.White);
        }

        public bool IsReady
        {
            get
            {
                return _isReady;
            }
            set
            {
                this._isReady = value;
            }
        }
    }
}
