using NUnit.Framework;
using SplashKitSDK;

namespace PathFinderVisualizer
{
    [TestFixture()]
    public class Rectange1Test
    {
        [Test()]
        public void TestIsAt()
        {
            Point2D point;

            // testing the parent class
            Rectangle1 newRectangle1 = new Rectangle1(100, 100, 40, 40, Color.Black);
            // should return true as the point is within bounds
            point = SplashKit.PointAt(120, 120);
            Assert.IsTrue(newRectangle1.IsAt(point));
            // should return false as the point is out of bounds
            point = SplashKit.PointAt(145, 120);
            Assert.IsFalse(newRectangle1.IsAt(point));

            // testing the Cell class which is derived from Rectangle1
            Cell newCell = new Cell(200, 200, 50, 50, Color.Black, Color.White, 10);
            // should return true as the point is within bounds
            point = SplashKit.PointAt(225, 200);
            Assert.IsTrue(newCell.IsAt(point));
            // should return false as the point is out of bounds
            point = SplashKit.PointAt(225, 230);
            Assert.IsFalse(newCell.IsAt(point));
        }
    }
}
