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
            // testing the Cell class which is derived from Rectangle1
            Cell newCell = new Cell(200, 200, 50, 50, Color.Black, Color.White, 10);
            // should return true as the point is within bounds
            point = SplashKit.PointAt(225, 200);
            Assert.IsTrue(newCell.IsAt(point));
            // should return false as the point is out of bounds
            point = SplashKit.PointAt(225, 260);
            Assert.IsFalse(newCell.IsAt(point));
        }
    }
}
