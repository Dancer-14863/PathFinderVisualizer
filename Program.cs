using SplashKitSDK;

namespace PathFinderVisualizer
{
    public class Program
    {
        public static void Main()
        {
            Window shapesWindow;
            shapesWindow = new Window("Path Finding Visualizer", 1366, 768);

            Cell startingCell = null;
            Cell endingCell = null;

            Grid grid = new Grid(10, 10, 900, 755, Color.Black, 50, 1);
            ObstacleDrawer obstacleDrawer = new ObstacleDrawer();
            Solver solver = new Solver();
            do 
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen(Color.White);

                if (SplashKit.MouseClicked(MouseButton.LeftButton) || SplashKit.MouseDown(MouseButton.LeftButton)) 
                {
                    grid.HasBeenClicked(SplashKit.MousePosition(), MouseButton.LeftButton);
                } 
                else if (SplashKit.MouseClicked(MouseButton.RightButton) || SplashKit.MouseDown(MouseButton.RightButton)) 
                {
                    grid.HasBeenClicked(SplashKit.MousePosition(), MouseButton.RightButton);
                } 
                else if (SplashKit.KeyTyped(KeyCode.SKey))
                {
                    Cell clickedCell = grid.GetTargetCell(SplashKit.MousePosition());
                    if (startingCell is Cell && (startingCell != clickedCell))
                    {
                        startingCell.ForegroudColor = Color.White;
                    }
                    startingCell = clickedCell;
                    startingCell.ForegroudColor = Color.Purple;
                }
                else if (SplashKit.KeyTyped(KeyCode.EKey))
                {
                    Cell clickedCell = grid.GetTargetCell(SplashKit.MousePosition());
                    if (endingCell is Cell && (endingCell != clickedCell))
                    {
                        endingCell.ForegroudColor = Color.White;
                    }
                    endingCell = clickedCell;
                    endingCell.ForegroudColor = Color.Purple;
                }

                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    grid.ResetGrid();
                    startingCell = null;
                    endingCell = null;
                }

                if (SplashKit.KeyTyped(KeyCode.TKey))
                {
                    grid.ResetPath();
                    startingCell.ForegroudColor = Color.Purple;
                    endingCell.ForegroudColor = Color.Purple;
                }
                if (SplashKit.KeyTyped(KeyCode.MKey))
                {
                    startingCell = null;
                    endingCell = null;
                    grid.ResetGrid();
                    obstacleDrawer.DrawMaze(grid);
                }

                if (SplashKit.KeyTyped(KeyCode.NKey))
                {
                    startingCell = null;
                    endingCell = null;
                    grid.ResetGrid();
                    obstacleDrawer.Random(grid);
                }

                if (startingCell is Cell && endingCell is Cell && SplashKit.KeyTyped(KeyCode.FKey))
                {
                    solver.FindPathAstar(grid.Cells, startingCell, endingCell);
                }

                if (startingCell is Cell && endingCell is Cell && SplashKit.KeyTyped(KeyCode.GKey))
                {
                    solver.FindPathDiji(grid.Cells, startingCell, endingCell);
                }


                grid.Draw();
                SplashKit.RefreshScreen(60);
            } while(!SplashKit.QuitRequested());
        }
    }
}