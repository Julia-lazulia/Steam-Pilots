﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Cloud9;

namespace Cloud9
{
    public class World : DrawableGameComponent
    {
        #region Singleton
        static World instance;
        public static World Instance
        {
            get
            {
                if (instance == null) throw new NullReferenceException("World has not yet been initialized");
                return instance;
            }
        }
        #endregion

        #region Properties
        public bool DrawTiles = true;
        int resolutionNum;
        int displayModes;
        Layer[] layers;
        GameTime gameTime;
        Vector2 cameraPosition;
        Player player;
        SpriteBatch spriteBatch;
        public static Vector2[] Resolutions = new Vector2[99];
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes the world
        /// </summary>
        /// <param name="game">Game instance</param>
        private World(Game game)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            foreach(DisplayMode displayMode in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
            {
                Resolutions[displayModes] = new Vector2(displayMode.Width, displayMode.Height);
                displayModes++;
            }
        }
        
        /// <summary>
        /// Initializes the player
        /// </summary>
        private void InitializePlayer()
        {
            layers = WorldGen.Generate();
            player = new Player();
            player.Spawn();
            cameraPosition = player.Position - Resolution / 2f;
        }

        /// <summary>
        /// Initializes the world
        /// </summary>
        /// <param name="game">Game instance</param>
        public static void Initialize(Game game)
        {
            if (instance != null)
                throw new StackOverflowException("World.Initialize can only be called once.");
            instance = new World(game);
            Input.Initialize(game);
            instance.InitializePlayer();
        }

        /// <summary>
        /// Updates the world
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            Vector2 targetCameraPosition = player.Position - Resolution / 2f;
            cameraPosition += (targetCameraPosition - cameraPosition) * 10f * ElapsedSeconds;
            foreach (Layer l in layers)
                l.Update();
            if (Input.Instance.WheelScrolledDown() && resolutionNum > 0)
            {
                resolutionNum--;
            }
            if (Input.Instance.WheelScrolledUp() && resolutionNum < Resolutions.Length - 1)
            {
                resolutionNum++;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the world
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Draw(GameTime gameTime)
        {
            this.gameTime = gameTime;
            Matrix matrix = Matrix.Identity;
            if (Cloud9.Game.ScaleToScreen)
                matrix = Matrix.CreateScale(ScreenScaling.X, ScreenScaling.Y, 1f);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, matrix);
            foreach (Layer l in layers)
                l.Draw();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Gets the layer from the given index
        /// </summary>
        /// <param name="layer">Layer index</param>
        /// <returns>Layer</returns> 
        public Layer GetLayer(int layer)
        {
            return layers[layer];
        }
        #endregion

        #region Helper Methods
        public static Vector2 Resolution
        {
            get { return Resolutions[instance.resolutionNum]; }
        }

        public Vector2 CameraPosition
        {
            get { return cameraPosition; }
        }

        public Player Player
        {
            get { return player; }
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public Vector2 ScreenScaling
        {
            get { return new Vector2(Cloud9.Game.ScreenSize.X / Resolution.X, Cloud9.Game.ScreenSize.Y / World.Resolution.Y); }
        }

        public static float ElapsedSeconds
        {
            get { return (float)instance.gameTime.ElapsedGameTime.TotalSeconds; }
        }

        public static int Width
        {
            get { return 5000; }
        }

        public static int Height
        {
            get { return 12000; }
        }

        public static ContentManager Content
        {
            get { return instance.Game.Content; }
        }
        #endregion
    }
}