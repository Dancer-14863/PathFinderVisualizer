using SplashKitSDK;

namespace PathFinderVisualizer
{
    /// <summary>
    /// Slider class derived from the Rectangle1 class
    /// Used to represent a draggable slider.
    /// The draggable slider consists of a rectangle body and a clickable button which
    /// can be dragged along the width of the slider body. The position of the button on the slider 
    /// determines a value between the min and max as the current value.
    /// </summary>
    public class Slider : Rectangle1
    {
        private Color _buttonColor;
        private Button _sliderButton;
        private int _sliderMinValue;
        private int _sliderMaxValue;
        private double _sliderValue;
        private double _stepValue;
        private string _caption;
        private Font _captionFont;

        /// <summary>
        /// Constructor method for the slider class.
        /// </summary>
        /// <param name="x">starting X coordinate</param>
        /// <param name="y">starting Y coordinate</param>
        /// <param name="width">width of the slider</param>
        /// <param name="height">height of the slider</param>
        /// <param name="color">color of the slider</param>
        /// <param name="buttonColor">color of the clickable button</param>
        /// <param name="caption">string message to be displayed on top of the slider</param>
        /// <param name="captionFont">font type of the caption</param>
        /// <param name="min">min value of the slider</param>
        /// <param name="max">max value of the slider</param>
        public Slider(double x, double y, int width, int height, Color color, Color buttonColor, string caption, Font captionFont, int min, int max) : base (x, y, width, height, color)
        {
            _buttonColor = buttonColor;
            _sliderMinValue = min;
            _sliderMaxValue = max;
            _caption = caption;
            _captionFont = captionFont;

            // initializes the clickable button
            double midY = _y + _height / 2;
            _sliderButton = new Button(_x, midY - height / 8 - 5, _width / 10, _height / 4 + 10, buttonColor, color);
            _sliderValue = min;
            /*
                Calculates the step value, this is the amount by which the slider's
                current value will change when the button's position is changed
            */
            _stepValue = (double)((max - min) + 1) / width;
        }

        /// <summary>
        /// Readonly property for the slider value field
        /// </summary>
        public double SliderValue
        {
            get { return _sliderValue; }
        }

        /// <summary>
        /// Checks if the passed point is withing the bounds of the 
        /// slider
        /// </summary>
        /// <param name="pt">Position to check against</param>
        /// <returns>True if the slider is at the passed point, else false</returns>
        public override bool IsAt(Point2D pt) 
        {
            bool isAtPoint = false;
            double midY = _y + _height / 2;
            Rectangle rect = new Rectangle();
            rect.X = _x;
            rect.Y = midY - _height / 8;
            rect.Width = _width;
            rect.Height = _height / 4;

            if (SplashKit.PointInRectangle(pt, rect))
            {
                isAtPoint = true;
            }
            
            return isAtPoint;
        }

        /// <summary>
        /// Checks if the passed mouse button is being clicked or held down
        /// at the slider's position
        /// </summary>
        /// <param name="clickedButton"></param>
        /// <returns>True if button clicked or heldown at slider position else false</returns>
        public override bool HasBeenClicked(MouseButton clickedButton)
        {
            bool clicked = false;
            if ((SplashKit.MouseClicked(clickedButton) || SplashKit.MouseDown(clickedButton)) && IsAt(SplashKit.MousePosition()))
            {
                clicked = true;
                // changes the slider button's position to mouse location
                _sliderButton.X = SplashKit.MousePosition().X;
                // changes the slider current value according to displacement of the button's position
                _sliderValue = _sliderMinValue + (_stepValue * (_sliderButton.X - _x));
            }
            return clicked;
        }

        /// <summary>
        /// Draws the slider on the screen
        /// </summary>
        public override void Draw()
        {
            double midY = _y + _height / 2;
            // draws the slider caption and the slider's current value
            SplashKit.DrawText($"{_caption} {(int)_sliderValue}", Color.Black, _captionFont, 16, _x, _y);
            SplashKit.FillRectangle(_color, _x, midY - _height / 8, _width, _height / 4);
            _sliderButton.Draw();
        }
    }
}