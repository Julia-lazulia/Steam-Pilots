using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SteamPilots
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
        List<Layer> layers;
        GameTime gameTime;
        Vector2 cameraPosition;
        EntityPlayer player;
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
            player = new EntityPlayer();
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
            if (Main.ScaleToScreen)
                matrix = Matrix.CreateScale(ScreenScaling.X, ScreenScaling.Y, 1f);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, matrix);
            foreach (Layer l in layers)
            {
                if (l.Active && l.Visible)
                {
                    l.Draw();
                }
            }

            spriteBatch.Draw(Tile.GetTile(player.currentTile).GetTextureFile(), new Rectangle((int)(100 + cameraPosition.X), (int)(100 + cameraPosition.Y), Tile.TileSize, Tile.TileSize), Tile.GetTile(player.currentTile).GetSource(), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Gets the layer from the given index
        /// </summary>
        /// <param name="layer">Layer index</param>
        /// <returns>Layer</returns> 
        public ForegroundLayer GetForegroundLayer(int layer)
        {
            if(layer < layers.Count && layers[layer] is ForegroundLayer)
            {
                return (ForegroundLayer)layers[layer];
            }
            return null;
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

        public EntityPlayer Player
        {
            get { return player; }
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public Vector2 ScreenScaling
        {
            get { return new Vector2(Main.ScreenSize.X / Resolution.X, Main.ScreenSize.Y / World.Resolution.Y); }
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

        public static GameTime GameTime
        {
            get { return instance.gameTime; }
        }
        #endregion
    }
}
