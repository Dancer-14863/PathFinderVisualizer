using SplashKitSDK;

namespace PathFinderVisualizer
{
    public class Button : Rectangle1
    {
        private string _caption;
        private Color _captionColor;
        private Color _hoverColor;
        private int _captionSize;
        private Font _captionFont;
        private bool _active;

        public Button(float x, float y, int width, int height, Color color, string caption, int captionSize, Font captionFont, Color captionColor, Color hoverColor) : base (x, y, width, height, color)
        {
            _caption = caption;
            _captionSize = captionSize;
            _captionFont = captionFont;
            _captionColor = captionColor;
            _hoverColor = hoverColor;
            _active = false;
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public override void Draw()
        {
            base.Draw();
            float midPointY = _y + _height / 2;
            SplashKit.DrawText(_caption, _captionColor, _captionFont, _captionSize, _x + 10, midPointY - 10);
        }

    }
}