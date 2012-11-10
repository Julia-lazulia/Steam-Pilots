using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SteamPilots
{
    class PlayState : IGameState
    {
        private bool _isready;
        private int _fps;

        public PlayState()
        {
            _isready = false;
        }

        public void EnterState()
        {
            World.Initialize(GameStateManager.Main);
            World.Instance.LoadContent();
            World.Instance.InitializePlayer();
            _isready = true;

        }

        public void LeaveState()
        {
            
        }

        public void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            _fps = (int)(1.0 / gt.ElapsedGameTime.TotalSeconds);
            if(Input.Instance.KeyNewPressed(Keys.B))
            {
                GameStateManager.SwitchState(new BuildState());
            }
            World.Instance.Update(gt);
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            sb.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), "Fps : " + _fps, Vector2.Zero, Color.White);
            sb.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), "Position: " + World.Instance.Player.Position.ToString(), new Vector2(0f, 25f), Color.White);
            sb.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), "Velocity: " + World.Instance.Player.Velocity.ToString(), new Vector2(0f, 50f), Color.White);
            sb.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), "Mnt. Velocity: " + World.Instance.Player.airShip.Velocity.ToString(), new Vector2(0f, 75f), Color.White);
            sb.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), "Cam. Position: " + World.Instance.CameraPosition.ToString(), new Vector2(0f, 100f), Color.White);
            sb.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), "Cam. Area: " + World.Instance.CameraViewRect.ToString(), new Vector2(0f, 150f), Color.White);
            /*
            World.Instance.SpriteBatch.DrawString(World.Content.Load<SpriteFont>("SpriteFont1"), string.Concat(new object[]
			{
				"Resolution : ",
				(int)ScreenSize.X,
				"x",
				(int)ScreenSize.Y
			}), new Vector2(0f, 125f), Color.White);*/
            World.Instance.Draw(sb);
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
