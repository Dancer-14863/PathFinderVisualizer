using SplashKitSDK;
using System.Collections.Generic;

namespace PathFinderVisualizer
{
    public class Cell : Rectangle1
    {
        private Color _foregroundColor;
        private int _padding;
        private List<Cell> _neighboringCells;
        private bool _walkable;

        public Cell(double x, double y, int width, int height, Color color, Color foregroundColor, int padding) : base (x, y, width, height, color)
        {
            _foregroundColor = foregroundColor;
            _padding = padding;
            _walkable = true;
            _neighboringCells = new List<Cell>();
        }

        public bool Walkable
        {
            get { return _walkable; }
            set 
            {
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

        public void ToggleWalkable()
        {
            Walkable = !_walkable;
        }

        public List<Cell> NeighboringCells
        {
            get { return _neighboringCells; }
        }

        public Color ForegroudColor
        {
            set { _foregroundColor = value; }
        }

        public override void Draw()
        {
            // outer rectangle
            SplashKit.FillRectangle(_color, _x, _y, _width, _height);
            SplashKit.FillRectangle(_foregroundColor, _x + _padding, _y + _padding, _width - (_padding * 2), _height - (_padding * 2));
        }
    }
}