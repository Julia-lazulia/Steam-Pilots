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

namespace SteamPilots
{
    public class Main : Game
    {
        GraphicsDeviceManager graphics;
        public static Vector2 ScreenSize;
        public static bool ScaleToScreen;
        public static float guiScale = 2f;
        public static bool hasFocus = false;
        private SpriteBatch spriteBatch;        
        
        public Main()
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
            Window.Title = "Steam Pilots";
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            ScreenSize = new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height);
        }
        
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Input.Initialize(this);
            Components.Add(Input.Instance);
            GameStateManager.Main = this;
            GameStateManager.Initialize(new StartState());            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Texture2D t = new Texture2D(GraphicsDevice, 2, 2);
            t.SetData<Color>(new Color[] { Color.White, Color.White, Color.White, Color.White });
            World.debugTex = t;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            GameStateManager.Update(gameTime);
            hasFocus = this.IsActive;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);
            Matrix matrix = Matrix.Identity;
            if (Main.ScaleToScreen)
            {
                matrix = Matrix.CreateScale(ScreenScaling.X, ScreenScaling.Y, 1f);
            }
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, matrix);

            GameStateManager.Draw(spriteBatch);

            spriteBatch.End();
        }

        public Vector2 ScreenScaling
        {
            get { return new Vector2(ScreenSize.X / Resolution.X, ScreenSize.Y / Resolution.Y); }
        }

        public Vector2 Resolution
        {
            get { return ScreenSize; }
        }
    }
}
