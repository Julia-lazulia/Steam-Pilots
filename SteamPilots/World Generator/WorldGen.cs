using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SteamPilots
{
    enum IslandSize
    {
        Tiny,
        Small,
        Medium,
        Large,
        Giant
    }

    public static class WorldGen
    {
        // each island size takes up a certain amount of points
        // tiny = 1  (will have a couple of rocks... trees, maybe a pig, its very small
        // small = 3 (maybe a house or something)
        // medium = 5 (a village could be here, if it was small. or a big house with a farm)
        // large = 10 (small city)
        // giant = 20 (big city)

        #region Properties
        static int islandPoints = 10000;
        static Random rnd;
        #endregion

        #region Methods
        /// <summary>
        /// Generate layers
        /// </summary>
        /// <returns>Generated layers</returns>
        public static List<Layer> Generate()
        {
            rnd = new Random(50);
            List<Layer> layers = new List<Layer>();
           
            for (int i = 0; i < 3; i++)
            {
                ForegroundLayer l = new ForegroundLayer(0.25f + (i * 0.05f));
                l.Visible = true;
                layers.Add(l);
            }
            Loop(layers);
            AddClouds((ForegroundLayer)layers[0]);
            layers.Add(new BackgroundLayer(0.55f));
            return layers;
        }

        private static void AddClouds(ForegroundLayer l)
        {
            for(var i = 0; i < 2000;i++)
            {
                var x = rnd.NextDouble() * World.Width;
                var y = rnd.NextDouble() * World.Height;
                var speed = 1f + rnd.NextDouble() * 10f;

                l.GetEntities(true).Add(new EntityCloud(new Vector2((float)x, (float)y), (float)speed));
            }
        }

        /// <summary>
        /// Generate layers
        /// </summary>
        /// <param name="layers">Layers</param>
        private static void Loop(List<Layer> layers)
        {
            if (islandPoints > 0)
            {
                int i = rnd.Next(100);
                int sizeXMin = 0;
                int sizeXMax = 0;
                if (i < 90 && i > 50)
                {
                    islandPoints--;
                    sizeXMin = 25;
                    sizeXMax = 50;
                }
                else if (i < 95 && islandPoints >= 3)
                {
                    islandPoints -= 3;
                    sizeXMin = 50;
                    sizeXMax = 100;
                }
                else if (i < 97 && islandPoints >= 5)
                {
                    islandPoints -= 5;
                    sizeXMin = 100;
                    sizeXMax = 200;
                }
                else if (i < 99 && islandPoints >= 10)
                {
                    islandPoints -= 10;
                    sizeXMin = 200;
                    sizeXMax = 400;
                }
                else if (i >= 99 || islandPoints < 20)
                {
                    islandPoints -= 20;
                    sizeXMin = 400;
                    sizeXMax = 800;
                }
                else
                {
                    Loop(layers);
                    return;
                }
                
                int sizeX = rnd.Next(sizeXMin, sizeXMax);
                GenerateIsland(layers, sizeX);
                Loop(layers);
            }
        }

        /// <summary>
        /// Generate island
        /// </summary>
        /// <param name="layers">Layers</param>
        /// <param name="sizeX">Island width</param>
        private static void GenerateIsland(List<Layer> layers, int sizeX)
        {
            int startX = rnd.Next(0, World.Width - sizeX);
            int endX = startX + sizeX;
            int startY = rnd.Next(30, World.Height - sizeX / 2);
            int endY = startY + sizeX / 2;
            int YPosition = startY + rnd.Next((endY - startY) / 2);
            for (int x = startX; x < endX; x++)
            {
                YPosition = (int)MathHelper.Clamp((float)YPosition, 0f, (float)(World.Height - 1));
                for (int y = YPosition; y < YPosition + Math.Min(x - startX, endX - x); y++)
                {
                    if (y >= World.Height)
                    {
                        break;
                    }
                    for (int layer = 2; layer < layers.Count; layer++)
                    {
                        if (y == YPosition)
                        {
                            layers[layer].SetTile(x, y, Tile.Grass);
                            if (rnd.Next(30) == 0) generateTree(layers, x, y - 1);
                        }
                        else
                            layers[layer].SetTile(x, y, Tile.Dirt);
                    }
                }
                int i = rnd.Next(100);
                if (i > 70)
                {
                    if (i < 90)
                    {
                        YPosition++;
                    }
                    else
                    {
                        if (i < 100)
                        {
                            YPosition--;
                        }
                    }
                }
            }
        }

        private static void generateTree(List<Layer> layers, int x, int y)
        {
            for (int y1 = y; y1 > y - rnd.Next(2) - 4; y1--)
            {
                if (y1 < 0) return;
                layers[2].SetTile(x, y1, Tile.Planks);
                if (rnd.Next(20) == 0 && y1 != y) layers[2].SetTile(x + 1, y1, Tile.Planks);
                if (rnd.Next(20) == 0 && y1 != y) layers[2].SetTile(x - 1, y1, Tile.Planks);
            }
        }
        #endregion
    }
}
