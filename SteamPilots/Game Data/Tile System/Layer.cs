using System;
using System.Text;

namespace SteamPilots
{
    public class Layer
    {
        #region Properties
        // Layer tiles
        protected byte[] tileData;
        protected bool active;
        protected bool visible;
        protected float layerDepth;

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public float LayerDepth
        {
            get { return layerDepth; }
            private set
            {
                layerDepth = value;
            }
        }

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

        #region Initialisation
        
        /// <summary>
        /// Constructor, initializes layer data
        /// </summary>
        public Layer(float layerDepth)
        {
            this.active = true;
            this.visible = false;
            this.layerDepth = layerDepth;
            tileData = new byte[Width * Height];
        }

        #endregion

        #region Methods
        /// <summary>
        /// Does the layer have tiles?
        /// </summary>
        /// <returns></returns>
        public bool HasTiles()
        {
            return tileData != null;
        }

        /// <summary>
        /// Update the layer
        /// </summary>
        public virtual void Update()
        {
           
        }

        /// <summary>
        /// Draw the layer
        /// </summary>
        public virtual void Draw()
        {
            if (tileData != null)
            {
                int left = (int)(World.Instance.CameraPosition.X / Tile.TileSize);
                int right = (int)((World.Instance.CameraPosition.X + World.Resolution.X) / Tile.TileSize) + 1;
                int top = (int)(World.Instance.CameraPosition.Y / Tile.TileSize);
                int bottom = (int)((World.Instance.CameraPosition.Y + World.Resolution.Y) / Tile.TileSize) + 1;

                if (left < 0)
                    left = 0;
                if (right > Width)
                    right = Width;
                if (top < 0)
                    top = 0;
                if (bottom > Height)
                    bottom = Height;

                for (int x = left; x < right; x++)
                {
                    for (int y = top; y < bottom; y++)
                    {
                        if (IsValidTile(x, y))
                        {
                            Tile tile = GetTile(x, y);
                            if (tile != Tile.Air)
                                if (World.Instance.DrawTiles)
                                    tile.Draw(x, y, this);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get the tile from x and y coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns></returns>
        public Tile GetTile(int x, int y)
        {
            if (!IsValidTile(x, y))
                throw new IndexOutOfRangeException("Tile out of range in Tile.GetTile");
            return Tile.GetTile(tileData[y * Width + x]);
        }

        /// <summary>
        /// Are the coordinates valid?
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns></returns>
        public bool IsValidTile(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        /// <summary>
        /// Set the tiles at the given coordinates
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="tile">Tile to place</param>
        public void SetTile(int x, int y, Tile tile)
        {
            if (!IsValidTile(x, y))
                throw new IndexOutOfRangeException("Tile out of range in Tile.SetTile");
            tileData[y * Width + x] = Tile.GetByte(tile);
        }
        #endregion
    }
}
