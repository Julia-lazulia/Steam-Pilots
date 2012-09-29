using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Cloud9
{
    public abstract class Mount : Entity
    {
        #region Properties
        protected Entity rider;
        #endregion

        #region Initialization
        public Mount() { }
        #endregion

        #region Methods
        /// <summary>
        /// Get player offset
        /// </summary>
        /// <returns></returns>
        public abstract Vector2 GetPlayerOffset();

        /// <summary>
        /// Make entity ride the mount
        /// </summary>
        /// <param name="e"></param>
        public void Ride(Entity e)
        {
            rider = e;
            position = rider.Position;
            layer = e.Layer;
        }

        /// <summary>
        /// Dismount the rider
        /// </summary>
        public void DismountRider()
        {
            rider = null;
            velocity = Vector2.Zero;
        }

        /// <summary>
        /// Update mount
        /// </summary>
        public override void Update()
        {
            if (rider is Player)
                UpdateInput();
            base.Update();
            if (rider != null)
                rider.Position = position + GetPlayerOffset();
        }
        
        /// <summary>
        /// Update input
        /// </summary>
        protected virtual void UpdateInput()
        {
        }
        #endregion
    }
}
