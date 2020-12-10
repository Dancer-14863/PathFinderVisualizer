using SplashKitSDK;

namespace PathFinderVisualizer
{
    /// <summary>
    /// Button class derived from the Rectangle1 class.
    /// Used to represent a clickable button
    /// </summary>
    public class Button : Rectangle1
    {
        private string _caption;
        private Color _captionColor;
        private Color _activeColor;
        private int _captionSize;
        private Font _captionFont;
        private bool _active;

        /// <summary>
        /// Constructor method for the Button class.
        /// _active is set to false at initialization.
        /// </summary>
        /// <param name="x">starting X coordinate</param>
        /// <param name="y">starting Y coordinate</param>
        /// <param name="width">width of the button</param>
        /// <param name="height">height of the button</param>
        /// <param name="color">color of the button</param>
        /// <param name="caption">caption string to be displayed inside the button</param>
        /// <param name="captionSize">font size of the caption</param>
        /// <param name="captionFont">font type of the caption</param>
        /// <param name="captionColor">font color of the caption</param>
        /// <param name="activeColor">color to set the button to after it has been clicked</param>
        public Button(double x, double y, int width, int height, Color color, string caption, int captionSize, Font captionFont, Color captionColor, Color activeColor) : base (x, y, width, height, color)
        {
            _caption = caption;
            _captionSize = captionSize;
            _captionFont = captionFont;
            _captionColor = captionColor;
            _activeColor = activeColor;
            _active = false;
        }

        /// <summary>
        /// Constructor method for the button class.
        /// This is used to create buttons without captions
        /// _active is set to false at initialization.
        /// </summary>
        /// <param name="x">starting X coordinate</param>
        /// <param name="y">starting Y coordinate</param>
        /// <param name="width">width of the button</param>
        /// <param name="height">height of the button</param>
        /// <param name="color">color of the button</param>
        /// <param name="activeColor">color to set the button to after it has been clicked</param>
        public Button(double x, double y, int width, int height, Color color, Color activeColor) : base (x, y, width, height, color)
        {
            _activeColor = activeColor;
            _active = false;
        }

        /// <summary>
        /// Property for the active field
        /// </summary>
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        /// <summary>
        /// Draws the button on the screen
        /// </summary>
        public override void Draw()
        {
            double midPointY = _y + _height / 2;

            /*
                If the button is not active draws it with the normal
                color otherwise draws it with the active color
            */
            if (!_active)
            {
                SplashKit.FillRectangle(_color, _x, _y, _width, _height);
            }
            else 
            {
                SplashKit.FillRectangle(_activeColor, _x, _y, _width, _height);
            }

            /*
                If a caption string has been sets draws that text inside the button
            */
            if (_caption is string)
            {
                SplashKit.DrawText(_caption, _captionColor, _captionFont, _captionSize, _x + 10, midPointY - 10);
            }
        }

    }
}