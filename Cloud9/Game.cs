using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Cloud9
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public static Vector2 ScreenSize;
        public static bool ScaleToScreen;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
            ScreenSize = new Vector2((float)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * 0.75), (float)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.75));
            graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;
            ScaleToScreen = true;
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            ScreenSize = new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height);
        }
        
        protected override void Initialize()
        {
            World.Initialize(this);

            Components.Add(World.Instance);
            Components.Add(Input.Instance);

            base.Initialize();
        }

        protected override void LoadContent()
        {

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Input.Instance.KeyNewPressed(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            World.Instance.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            // Background
            World.Instance.SpriteBatch.Draw(Content.Load<Texture2D>("background_1"), new Rectangle(0, 0, (int)ScreenSize.X, (int)ScreenSize.Y), Color.White);

            // Debug info
            World.Instance.SpriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont1"), "Fps : " + Math.Round((double)(1f / (float)gameTime.ElapsedGameTime.TotalSeconds)), Vector2.Zero, Color.White);
            World.Instance.SpriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont1"), "Position X : " + (int)World.Instance.Player.Position.X, new Vector2(0f, 25f), Color.White);
            World.Instance.SpriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont1"), "Position Y : " + (int)World.Instance.Player.Position.Y, new Vector2(0f, 50f), Color.White);
            World.Instance.SpriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont1"), "Velocity X : " + (int)World.Instance.Player.Velocity.X, new Vector2(0f, 75f), Color.White);
            World.Instance.SpriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont1"), "Velocity Y : " + (int)World.Instance.Player.Velocity.Y, new Vector2(0f, 100f), Color.White);
            World.Instance.SpriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont1"), string.Concat(new object[]
			{
				"Resolution : ",
				(int)World.Resolution.X,
				"x",
				(int)World.Resolution.Y
			}), new Vector2(0f, 125f), Color.White);

            World.Instance.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
