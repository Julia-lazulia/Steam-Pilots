﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SteamPilots
{
    class PauseState : IGameState
    {
        public bool _debug;
        private bool _isready;
        private int _fps;
        private GuiPauseMenu pauseMenu;

        public PauseState(bool _debug)
        {
            _isready = false;
            this._debug = _debug;
        }

        public void EnterState()
        {
            _isready = true;
            pauseMenu = new GuiPauseMenu();
        }

        public void LeaveState()
        {
        }

        public void Update(GameTime gt)
        {
            _fps = (int)(1.0 / gt.ElapsedGameTime.TotalSeconds);
            if (Input.Instance.KeyNewPressed(Keys.Escape))
            {
                GameStateManager.SwitchState(new PlayState(_debug));
            }
            if (Input.Instance.KeyNewPressed(Keys.F3))
            {
                _debug = !_debug;
            }
            pauseMenu.Update(gt);
        }

        public void Draw(SpriteBatch sb)
        {
            if (_debug)
            {
                sb.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), "Fps : " + _fps, Vector2.Zero, Color.White);
                sb.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), "Position: " + World.Instance.Player.Position.ToString(), new Vector2(0f, 25f), Color.White);
                sb.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), "Velocity: " + World.Instance.Player.Velocity.ToString(), new Vector2(0f, 50f), Color.White);
                sb.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), "Mnt. Velocity: " + World.Instance.Player.airShip.Velocity.ToString(), new Vector2(0f, 75f), Color.White);
                sb.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), "Cam. Position: " + World.Instance.CameraPosition.ToString(), new Vector2(0f, 100f), Color.White);
                sb.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), "Cam. Area: " + World.Instance.CameraViewRect.ToString(), new Vector2(0f, 150f), Color.White);
            }
            /*
            World.Instance.SpriteBatch.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), string.Concat(new object[]
			{
				"Resolution : ",
				(int)ScreenSize.X,
				"x",
				(int)ScreenSize.Y
			}), new Vector2(0f, 125f), Color.White);*/
            World.Instance.Draw(sb);
            pauseMenu.Draw(sb);
        }

        public bool IsReady
        {
            get
            {
                return _isready;
            }
            set
            {
                _isready = value;
            }
        }
    }
}
