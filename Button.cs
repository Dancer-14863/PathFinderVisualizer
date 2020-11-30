using SplashKitSDK;

namespace PathFinderVisualizer
{
    public class Button : Rectangle1
    {
        private string _caption;
        private Color _captionColor;
        private Color _activeColor;
        private int _captionSize;
        private Font _captionFont;
        private bool _active;

        public Button(double x, double y, int width, int height, Color color, string caption, int captionSize, Font captionFont, Color captionColor, Color activeColor) : base (x, y, width, height, color)
        {
            _caption = caption;
            _captionSize = captionSize;
            _captionFont = captionFont;
            _captionColor = captionColor;
            _activeColor = activeColor;
            _active = false;
        }

        public Button(double x, double y, int width, int height, Color color, Color activeColor) : base (x, y, width, height, color)
        {
            _activeColor = activeColor;
            _active = false;
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public override void Draw()
        {
            double midPointY = _y + _height / 2;

            if (!_active)
            {
                SplashKit.FillRectangle(_color, _x, _y, _width, _height);
            }
            else 
            {
                SplashKit.FillRectangle(_activeColor, _x, _y, _width, _height);
            }

            if (_caption is string)
            {
                SplashKit.DrawText(_caption, _captionColor, _captionFont, _captionSize, _x + 10, midPointY - 10);
            }
        }

    }
}