using SplashKitSDK;

namespace PathFinderVisualizer
{
    public class Rectangle1
    {
        protected double _x, _y;
        protected int _width, _height;
        protected Color _color;

        public Rectangle1(double x, double y, int width, int height, Color color)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _color = color;
        }

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public Color color
        {
            set { _color = value; }
        }

        public virtual void Draw()
        {
            SplashKit.FillRectangle(_color, _x, _y, _width, _height);
        }

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