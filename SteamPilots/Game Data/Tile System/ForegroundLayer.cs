using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SteamPilots
{
    public class ForegroundLayer : Layer
    {
        #region Properties
        // Layer tiles
        byte[] tileData;

        // Collections with active and inactive entities
        Collection<Entity> InActiveEntities;
        Collection<Entity> ActiveEntities;

        // Width and Height
        int Width
        {
            get { return World.Width; }
        }
        int Height
        {
            get { return World.Height; }
        }
        #endregion

        #region Initialization
        public ForegroundLayer(float lDepth) : base(lDepth)
        {
            InActiveEntities = new Collection<Entity>();
            ActiveEntities = new Collection<Entity>();
            tileData = new byte[Width * Height];
        }     
        #endregion

        #region Methods
        /// <summary>
        /// Update the layer
        /// </summary>
        public override void Update()
        {
            foreach (Entity e in ActiveEntities)
                e.Update();

            ActiveEntities.Update();
            InActiveEntities.Update();
        }

        /// <summary>
        /// Draw the layer
        /// </summary>
        public override void Draw()
        {
            Entity[] sortedEntities = ActiveEntities.ToArray();
            Array.Sort<Entity>(sortedEntities);
            foreach (Entity e in sortedEntities)
                e.Draw(layerDepth);

            base.Draw();
        }
        
        /// <summary>
        /// Is it possible to place the tile at the given coordinates?
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="tile">tile to place</param>
        /// <returns></returns>
        public bool CanPlace(int x, int y, Tile tile)
        {
            if (tile != null)
            {
                Rectangle tileBounds = new Rectangle(x, y, (int)(x + tile.Size.X), (int)(y + tile.Size.Y));
                Entity[] entities = GetEntities(true).ToArray();
                for (int index = 0; index < entities.Length; index++)
                {
                    Entity entity = entities[index];
                    if (tileBounds.Intersects(entity.BoundingRect)) return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the entity collection
        /// </summary>
        /// <param name="active">active entities?</param>
        /// <returns></returns>
        public Collection<Entity> GetEntities(bool active)
        {
            if (active)
                return ActiveEntities;
            else
                return InActiveEntities;
        }
        #endregion
    }
}
