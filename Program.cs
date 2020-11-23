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

            Grid grid = new Grid(30, 30, 870, 750, Color.Black, 40, 1);
            ObstacleDrawer obstacleDrawer = new ObstacleDrawer();
            Solver solver = new Solver();
            Sprite red_flag = SplashKit.CreateSprite("red_flag.png");
            Sprite  green_flag = SplashKit.CreateSprite("green_flag.png");
            do 
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen(Color.White);

                if (SplashKit.MouseClicked(MouseButton.LeftButton) || SplashKit.MouseDown(MouseButton.LeftButton)) 
                {
                    obstacleDrawer.DrawUser(grid, SplashKit.MousePosition(), MouseButton.LeftButton);
                } 
                else if (SplashKit.MouseClicked(MouseButton.RightButton) || SplashKit.MouseDown(MouseButton.RightButton)) 
                {
                    obstacleDrawer.DrawUser(grid, SplashKit.MousePosition(), MouseButton.RightButton);
                } 
                else if (SplashKit.KeyTyped(KeyCode.SKey))
                {
                    Cell clickedCell = grid.GetTargetCell(SplashKit.MousePosition());
                    if (startingCell is Cell && (startingCell != clickedCell))
                    {
                        startingCell.ForegroudColor = Color.White;
                    }
                    if (clickedCell is Cell)
                    {
                        startingCell = clickedCell;
                        startingCell.ForegroudColor = Color.LimeGreen;
                    }
                }
                else if (SplashKit.KeyTyped(KeyCode.EKey))
                {
                    Cell clickedCell = grid.GetTargetCell(SplashKit.MousePosition());
                    if (endingCell is Cell && (endingCell != clickedCell))
                    {
                        endingCell.ForegroudColor = Color.White;
                    }
                    if (clickedCell is Cell)
                    {
                        endingCell = clickedCell;
                        endingCell.ForegroudColor = Color.Red;
                    }
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
                    startingCell.ForegroudColor = Color.LimeGreen;
                    endingCell.ForegroudColor = Color.Red;
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
                    grid.ResetPath();
                    solver.FindPathAstar(grid.Cells, startingCell, endingCell);
                }

                if (startingCell is Cell && endingCell is Cell && SplashKit.KeyTyped(KeyCode.GKey))
                {
                    grid.ResetPath();
                    solver.FindPathDiji(grid.Cells, startingCell, endingCell);
                }


                grid.Draw();
                if (startingCell is Cell)
                {
                    green_flag.X = startingCell.X - 2;
                    green_flag.Y = startingCell.Y - 30;
                    green_flag.Draw();
                }
                if (endingCell is Cell)
                {
                    red_flag.X = endingCell.X - 2;
                    red_flag.Y = endingCell.Y - 30;
                    red_flag.Draw();
                }
                SplashKit.RefreshScreen(60);
            } while(!SplashKit.QuitRequested());
        }
    }
}