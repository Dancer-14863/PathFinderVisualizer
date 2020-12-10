using SplashKitSDK;

namespace PathFinderVisualizer
{
    /// <summary>
    /// Abstract class used for extending the functionality of the
    /// existing Rectangle class in Splashkit.
    /// </summary>
    public abstract class Rectangle1
    {
        protected double _x, _y;
        protected int _width, _height;
        protected Color _color;

        /// <summary>
        /// Constructor method for the Rectangle1 class
        /// </summary>
        /// <param name="x">starting X coordinate</param>
        /// <param name="y">starting Y coordinate</param>
        /// <param name="width">width of the rectangle</param>
        /// <param name="height">height of the rectangle</param>
        /// <param name="color">color of the rectangle</param>
        public Rectangle1(double x, double y, int width, int height, Color color)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _color = color;
        }

        /// <summary>
        /// Property for the x coordinate field
        /// </summary>
        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        /// <summary>
        /// Property for the y coordinate field
        /// </summary>
        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        /// <summary>
        /// Draws the rectangle shape on the screen
        /// </summary>
        public virtual void Draw()
        {
            SplashKit.FillRectangle(_color, _x, _y, _width, _height);
        }

        /// <summary>
        /// Checks if the passed point is within the bounds 
        /// of the rectangle
        /// </summary>
        /// <param name="pt">Position to check against</param>
        /// <returns>True if the shape is at the passed point, else false</returns>
        public virtual bool IsAt(Point2D pt) 
        {
            bool isAtPoint = false;
            Rectangle rect = new Rectangle();
            rect.X = _x;
            rect.Y = _y;
            rect.Width = _width;
            rect.Height = _height;

            if (SplashKit.PointInRectangle(pt, rect))
            {
                isAtPoint = true;
            }
            
            return isAtPoint;
        }

        /// <summary>
        /// Checks whether the passed mouse button is clicked
        /// at position of the shape
        /// </summary>
        /// <param name="clickedButton">Button to check if clicked</param>
        /// <returns>True if the button is clicked at the shape's position, else false</returns>
        public virtual bool HasBeenClicked(MouseButton clickedButton)
        {
            bool clicked = false;
            if (SplashKit.MouseClicked(clickedButton) && IsAt(SplashKit.MousePosition()))
            {
                clicked = true;
            }
            return clicked;
        }

    }
}