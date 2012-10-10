using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class Animation
    {
        #region Properties
        public float FrameTime;
        public Texture2D Texture;
        public int FrameCount;
        public bool Looping;
        public float FrameWidth
        {
            get
            {
                return (float)(Texture.Width / FrameCount);
            }
        }
        #endregion

        #region Initialization
        public Animation() {}

        /// <summary>
        /// Creates a new animation
        /// </summary>
        /// <param name="frameTime">Time per frame</param>
        /// <param name="frameCount">Amount of frames</param>
        /// <param name="looping">Should the animation loop?</param>
        /// <param name="texturePath">Path to the animation texture</param>
        public Animation(float frameTime, int frameCount, bool looping, string texturePath)
        {
            FrameTime = frameTime;
            FrameCount = frameCount;
            Looping = looping;
            Texture = World.Content.Load<Texture2D>(texturePath);
        }
        #endregion
    }

    public class AnimationPlayer
    {
        #region Properties
        public Dictionary<string, Animation> Animations;
        Animation playingAnimation;
        float frameTime;
        int frameCount;
        Rectangle srcRect;
        #endregion

        #region Methods
        /// <summary>
        /// Plays the given animation
        /// </summary>
        /// <param name="name">The animation name</param>
        public void PlayAnimation(string name)
        {
            Animation animation;
            if (!Animations.TryGetValue(name, out animation))
                throw new KeyNotFoundException("Could not find animation: " + name);
            if (animation == playingAnimation)
                return;

            playingAnimation = animation;
            frameCount = 0;
            frameTime = 0;
            CalculateSourceRectangle();
        }

        /// <summary>
        /// (Re)calculates the source rectangle
        /// </summary>
        private void CalculateSourceRectangle()
        {
            srcRect = new Rectangle(frameCount * (int)playingAnimation.FrameWidth,
               0,
               (int)playingAnimation.FrameWidth,
               playingAnimation.Texture.Height);
        }

        /// <summary>
        /// Updates the animation
        /// </summary>
        public void Update()
        {
            if (playingAnimation == null)
                return;

            frameTime += World.ElapsedSeconds;

            if (frameTime >= playingAnimation.FrameTime)
            {
                frameTime = 0;
                frameCount++;
                if (frameCount >= playingAnimation.FrameCount)
                {
                    if (playingAnimation.Looping)
                        frameCount -= 2;
                    else
                        frameCount--; // Stays on the last frame
                }


            }

            CalculateSourceRectangle();
        }

        /// <summary>
        /// Gives the source rectangle of the current animation
        /// </summary>
        /// <returns>Source rectangle</returns>
        public Rectangle GetSourceRectangle()
        {
            return srcRect;
        }

        /// <summary>
        /// Gives the texture of the current animation
        /// </summary>
        /// <returns>Texture2D</returns>
        public Texture2D GetTexture()
        {
            return playingAnimation.Texture;
        }

        /// <summary>
        /// Gives the origin of the current animation
        /// </summary>
        /// <returns>Origin</returns>
        public Vector2 GetOrigin()
        {
            return new Vector2(srcRect.Width / 2, srcRect.Height / 2);
        }
        #endregion
    }

    public class Sprite
    {
        #region Properties
        AnimationPlayer animationPlayer;
        #endregion

        #region Initialization
        /// <summary>
        /// Creates a sprite with the given animations
        /// </summary>
        /// <param name="animations">A dictionary with animations for the sprite</param>
        public Sprite(Dictionary<string, Animation> animations)
        {
            animationPlayer = new AnimationPlayer
            {
                Animations = animations
            };
        }
        #endregion

        #region Methods
        /// <summary>
        /// Plays the given animation
        /// </summary>
        /// <param name="name">The animation name</param>
        public void PlayAnimation(string name)
        {
            animationPlayer.PlayAnimation(name);
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        public void Update()
        {
            animationPlayer.Update();
        }

        /// <summary>
        /// Draws the sprite with the current frame
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="position">Sprite position</param>
        /// <param name="scale">Sprite scale</param>
        /// <param name="rotation">Sprite rotation</param>
        /// <param name="spriteEffects">SpriteEffects</param>
        /// <param name="color">Drawing Color</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale, float rotation, SpriteEffects spriteEffects, Color color, float layerDepth)
        {
            spriteBatch.Draw(animationPlayer.GetTexture(),
                position,
                animationPlayer.GetSourceRectangle(),
                color,
                rotation,
                animationPlayer.GetOrigin(),
                scale,
                spriteEffects,
                layerDepth);
        }
        #endregion
    }
}
