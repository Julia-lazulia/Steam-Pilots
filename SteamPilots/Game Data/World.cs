using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SteamPilots
{
    public class World : DrawableGameComponent
    {
        static int WORLD_WIDTH = 5000;
        static int WORLD_HEIGHT = 12000;
        static int DRAW_MARGIN = 64;

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
        Vector2 windDirection;
        Rectangle cameraViewArea;

        Texture2D rectDebugTex;

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
            windDirection = new Vector2(1.0f, 0.0f);
            cameraViewArea = new Rectangle();

        }
        
        /// <summary>
        /// Initializes the player
        /// </summary>
        private void InitializePlayer()
        {
            EntityCloud.Initialize();
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

        protected override void LoadContent()
        {
            //debug texture to help display rectangle outlines
            rectDebugTex = new Texture2D(GraphicsDevice, 1, 1);
            rectDebugTex.SetData(new[] { Color.Red });
            //rectDebugTex = Content.Load<Texture2D>(@"background_1");

            base.LoadContent();
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


            cameraViewArea.X = (int)(cameraPosition.X) - DRAW_MARGIN;
            cameraViewArea.Y = (int)(cameraPosition.Y) - DRAW_MARGIN;
            cameraViewArea.Width = (int)Resolution.X + 2 * DRAW_MARGIN;
            cameraViewArea.Height = (int)Resolution.Y + 2 * DRAW_MARGIN;

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
            
            spriteBatch.Draw(Tile.GetTile(player.currentTile).GetTextureFile(), new Rectangle(10, 100, Tile.TileSize, Tile.TileSize), Tile.GetTile(player.currentTile).GetSource(), Color.White);

            var bw = 2;
            //spriteBatch.Draw(rectDebugTex, cameraViewArea, null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            spriteBatch.Draw(rectDebugTex, new Rectangle(cameraViewArea.Left - (int)CameraPosition.X, cameraViewArea.Top - (int)CameraPosition.Y, bw, cameraViewArea.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f); // Left
            spriteBatch.Draw(rectDebugTex, new Rectangle(cameraViewArea.Right - (int)CameraPosition.X, cameraViewArea.Top - (int)CameraPosition.Y, bw, cameraViewArea.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f); // Right
            spriteBatch.Draw(rectDebugTex, new Rectangle(cameraViewArea.Left - (int)CameraPosition.X, cameraViewArea.Top - (int)CameraPosition.Y, cameraViewArea.Width, bw), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f); // Top
            spriteBatch.Draw(rectDebugTex, new Rectangle(cameraViewArea.Left - (int)CameraPosition.X, cameraViewArea.Bottom - (int)CameraPosition.Y, cameraViewArea.Width, bw), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f); // Bottom

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

        public Rectangle CameraViewRect
        {
            get { return cameraViewArea; }
        }

        public Vector2 WindDirection
        {
            get { return windDirection; } 
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
            get { return WORLD_WIDTH; }
        }

        public static int Height
        {
            get { return WORLD_HEIGHT; }
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
