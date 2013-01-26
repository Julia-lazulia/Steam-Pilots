using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SteamPilots
{
    public class World
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
                //if (instance == null) throw new NullReferenceException("World has not yet been initialized");
                return instance;
            }
        }
        #endregion

        #region Properties
        private Main main;
        public bool DrawTiles = true;
        int resolutionNum;
        int displayModes;
        List<Layer> layers;
        GameTime gameTime;
        Vector2 cameraPosition;
        Vector2 windDirection;
        Rectangle cameraViewArea;

        Texture2D rectDebugTex;

        public static EntityPlayer player;
        public static Vector2[] Resolutions = new Vector2[99];
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes the world
        /// </summary>
        /// <param name="game">Game instance</param>
        private World(Main game)
        {
            this.main = game;
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
        public void InitializePlayer()
        {
            EntityCloud.Initialize();
            layers = WorldGen.Generate();
            
            player = new EntityPlayer();
            player.Spawn();
            cameraPosition = player.Position - main.Resolution / 2f;
        }

        /// <summary>
        /// Initializes the world
        /// </summary>
        /// <param name="game">Game instance</param>
        public static void Initialize(Main game)
        {
            if (instance != null)
                throw new StackOverflowException("World.Initialize can only be called once.");
            instance = new World(game);
        }

        public void LoadContent()
        {
            //debug texture to help display rectangle outlines
            rectDebugTex = new Texture2D(main.GraphicsDevice, 1, 1);
            rectDebugTex.SetData(new[] { Color.Red });
            //rectDebugTex = Content.Load<Texture2D>(@"background_1");
        }

        /// <summary>
        /// Updates the world
        /// </summary>
        /// <param name="gameTime">GameTime</param>                  // NEED TO CLEAN THE RESOLUTION STUFF!!
        public void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            Vector2 targetCameraPosition = player.Position - main.Resolution / 2f;
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
            cameraViewArea.Width = (int)main.Resolution.X + 2 * DRAW_MARGIN;
            cameraViewArea.Height = (int)main.Resolution.Y + 2 * DRAW_MARGIN;

        }

        /// <summary>
        /// Draws the world
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public void Draw(SpriteBatch sb)
        {
          
            foreach (Layer l in layers)
            {
                if (l.Active && l.Visible)
                {
                    l.Draw(sb);
                }
            }
           
            var bw = 2;
            //spriteBatch.Draw(rectDebugTex, cameraViewArea, null, Color.Red, 0f, Vector2.Zero, SpriteEffects.None, 1f);
            sb.Draw(rectDebugTex, new Rectangle(cameraViewArea.Left - (int)CameraPosition.X, cameraViewArea.Top - (int)CameraPosition.Y, bw, cameraViewArea.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f); // Left
            sb.Draw(rectDebugTex, new Rectangle(cameraViewArea.Right - (int)CameraPosition.X, cameraViewArea.Top - (int)CameraPosition.Y, bw, cameraViewArea.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f); // Right
            sb.Draw(rectDebugTex, new Rectangle(cameraViewArea.Left - (int)CameraPosition.X, cameraViewArea.Top - (int)CameraPosition.Y, cameraViewArea.Width, bw), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f); // Top
            sb.Draw(rectDebugTex, new Rectangle(cameraViewArea.Left - (int)CameraPosition.X, cameraViewArea.Bottom - (int)CameraPosition.Y, cameraViewArea.Width, bw), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f); // Bottom

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
            get { return GameStateManager.Main.Content; }
        }

        public static GameTime GameTime
        {
            get { return instance.gameTime; }
        }

        
        #endregion
    }
}
