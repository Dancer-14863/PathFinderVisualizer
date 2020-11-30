using SplashKitSDK;

namespace PathFinderVisualizer
{
    public class Slider : Rectangle1
    {
        private Color _buttonColor;
        private Button _sliderButton;
        private int _sliderMinValue;
        private int _sliderMaxValue;
        private int _sliderValue;
        private int _stepValue;

        public Slider(double x, double y, int width, int height, Color color, Color buttonColor, int min, int max) : base (x, y, width, height, color)
        {
            _buttonColor = buttonColor;
            _sliderMinValue = min;
            _sliderMaxValue = max;

            double midY = _y + _height / 2;
            _sliderButton = new Button(_x, midY - height / 8 - 5, _width / 10, _height / 4 + 10, buttonColor, color);
            _sliderValue = min;
        }

        public int SliderValue
        {
            get { return _sliderValue; }
        }

        public Button SliderButton
        {
            get { return _sliderButton; }
        }

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

        public override bool HasBeenClicked(MouseButton clickedButton)
        {
            bool clicked = false;
            if ((SplashKit.MouseClicked(clickedButton) || SplashKit.MouseDown(clickedButton)) && IsAt(SplashKit.MousePosition()))
            {
                clicked = true;
                _sliderButton.X = SplashKit.MousePosition().X;
            }
            return clicked;
        }

        public override void Draw()
        {
            double midY = _y + _height / 2;
            SplashKit.FillRectangle(_color, _x, midY - _height / 8, _width, _height / 4);
            _sliderButton.Draw();
        }
    }
}