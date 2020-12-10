using SplashKitSDK;
using System.Collections.Generic;

namespace PathFinderVisualizer
{
    /// <summary>
    /// Cell class derived from the Rectangle1 class.
    /// Used to represent a single cell on the grid
    /// </summary>
    public class Cell : Rectangle1
    {
        private Color _foregroundColor;
        private int _padding;
        private List<Cell> _neighboringCells;
        private bool _walkable;

        /// <summary>
        /// Constructor for the Cell class.
        /// </summary>
        /// <param name="x">starting X coordinate</param>
        /// <param name="y">starting Y coordinate</param>
        /// <param name="width">width of the cell</param>
        /// <param name="height">height of the cell</param>
        /// <param name="color">color of the cell</param>
        /// <param name="foregroundColor">foreground color of the cell</param>
        /// <param name="padding">padding of the cell</param>
        public Cell(double x, double y, int width, int height, Color color, Color foregroundColor, int padding) : base (x, y, width, height, color)
        {
            _foregroundColor = foregroundColor;
            _padding = padding;
            _walkable = true;
            _neighboringCells = new List<Cell>();
        }

        /// <summary>
        /// Writeonly property for the foreground color field
        /// </summary>
        public Color ForegroudColor
        {
            set { _foregroundColor = value; }
        }

        /// <summary>
        /// Readonly property for the neighboring cells list
        /// </summary>
        public List<Cell> NeighboringCells
        {
            get { return _neighboringCells; }
        }

        /// <summary>
        /// Property for the Walkable field
        /// </summary>
        public bool Walkable
        {
            get { return _walkable; }
            set 
            {
                /*
                    If the walkable value is false, it means the cell is a wall. The foreground
                    color is set to black to indicate that it is impassable. Otherwise it 
                    is set to white.
                */
                if (value == false)
                {
                    _foregroundColor = Color.Black;
                }
                else
                {
                    _foregroundColor = Color.White;
                }
                _walkable = value;
            }
        }

        /// <summary>
        /// Draws the cell on the screen
        /// </summary>
        public override void Draw()
        {
            // the outer rectangle of the cell, the walls of the cell
            SplashKit.FillRectangle(_color, _x, _y, _width, _height);
            // the inner rectangle
            SplashKit.FillRectangle(_foregroundColor, _x + _padding, _y + _padding, _width - (_padding * 2), _height - (_padding * 2));
        }
    }
}