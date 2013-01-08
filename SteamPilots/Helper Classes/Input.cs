using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace SteamPilots
{
    public class Input : GameComponent
    {
        #region Singleton
        static Input instance;
        public static Input Instance
        {
            get
            {
                if (instance == null)
                    throw new NullReferenceException("Input has not yet been initialized");
                return instance;
            }
        }
        #endregion

        #region Properties
        KeyboardState cKstate;
        KeyboardState pKstate;
        MouseState cMstate;
        MouseState pMstate;
        #endregion

        #region Initialization
        private Input(Game game)
            : base(game)
        {}

        public static void Initialize(Game game)
        {
            if (instance != null)
                throw new StackOverflowException("Input.Initialize can only be called once.");
            instance = new Input(game);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Updates the input
        /// </summary>
        /// <param name="gameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            pKstate = cKstate;
            pMstate = cMstate;
            cKstate = Keyboard.GetState();
            cMstate = Mouse.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// Is key pressed?
        /// </summary>
        /// <param name="keys">Key</param>
        /// <returns>Pressed</returns>
        public bool KeyDown(Keys keys)
        {
            return cKstate.IsKeyDown(keys);
        }

        /// <summary>
        /// Is key newly pressed?
        /// </summary>
        /// <param name="keys">Key</param>
        /// <returns>Newly pressed</returns>
        public bool KeyNewPressed(Keys keys)
        {
            return cKstate.IsKeyDown(keys) && pKstate.IsKeyUp(keys);
        }

        /// <summary>
        /// Left mouse button newly pressed?
        /// </summary>
        /// <returns>Newly pressed</returns>
        public bool MouseLeftButtonNewPressed()
        {
            return cMstate.LeftButton == ButtonState.Pressed && pMstate.LeftButton == ButtonState.Released;
        }

        /// <summary>
        /// Right mouse button newly pressed?
        /// </summary>
        /// <returns>Newly pressed</returns>
        public bool MouseRightButtonNewPressed()
        {
            return cMstate.RightButton == ButtonState.Pressed && pMstate.RightButton == ButtonState.Released;
        }

        /// <summary>
        /// Gives the current Mouse position
        /// </summary>
        /// <returns>Mouse position</returns>
        public Vector2 MousePosition()
        {
            return new Vector2((float)cMstate.X, (float)cMstate.Y);
        }

        /// <summary>
        /// Gives the amount of scrolls
        /// </summary>
        /// <returns>Scrolls</returns>
        public int ScrollWheelValue()
        {
            return cMstate.ScrollWheelValue;
        }

        public int ScrolledValue()
        {
            return (cMstate.ScrollWheelValue - pMstate.ScrollWheelValue) / 120;
        }

        /// <summary>
        /// Is the mouse wheel scrolled up?
        /// </summary>
        /// <returns>Scrolled up</returns>
        public bool WheelScrolledUp()
        {
            return cMstate.ScrollWheelValue > pMstate.ScrollWheelValue;
        }

        /// <summary>
        /// Is the mouse wheel scrolled down?
        /// </summary>
        /// <returns>Scrolled down</returns>
        public bool WheelScrolledDown()
        {
            return cMstate.ScrollWheelValue < pMstate.ScrollWheelValue;
        }
        #endregion
    }
}
