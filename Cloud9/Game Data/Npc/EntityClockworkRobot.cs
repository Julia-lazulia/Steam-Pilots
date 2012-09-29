using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cloud9
{
    public class EntityClockworkRobot : Entity
    {
        public float speed;
        public const float speedReduction = 0.5f;

        /// <summary>
        /// STILL WORKING ON THIS, TESTING PURPOSES ONLY
        /// </summary>

        public override void Spawn()
        {
            speed = 5;
            base.Spawn();
        }

        public override void Update()
        {
            speed -= speedReduction;
            velocity += new Vector2(speed, 0);
            base.Update();
        }
    }
}
