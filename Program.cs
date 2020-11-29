using SplashKitSDK;
using System;

namespace PathFinderVisualizer
{
    public class Program
    {
        static Cell startingCell = null;
        static Cell endingCell = null;
        static Grid grid = new Grid(30, 30, 870, 750, Color.Black, 40, 1);
        static ObstacleDrawer obstacleDrawer = new ObstacleDrawer();
        static Solver solver = new Solver();
        static Sprite red_flag = SplashKit.CreateSprite("resources/red_flag.png");
        static Sprite green_flag = SplashKit.CreateSprite("resources/green_flag.png");
        static Button setStartingPointButton;
        static Button setEndingPointButton;
        static Button solveDijiButton;
        static Button solveAstarButton;

        public static void Main()
        {
            Button activeButton = null;
            Window shapesWindow;
            shapesWindow = new Window("Path Finding Visualizer", 1366, 768);
            InitUI();

            do 
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen(Color.White);
                DrawUI();


                // if (SplashKit.MouseClicked(MouseButton.LeftButton) || SplashKit.MouseDown(MouseButton.LeftButton)) 
                // {
                //     obstacleDrawer.DrawUser(grid, SplashKit.MousePosition(), MouseButton.LeftButton);
                // } 
                // else if (SplashKit.MouseClicked(MouseButton.RightButton) || SplashKit.MouseDown(MouseButton.RightButton)) 
                // {
                //     obstacleDrawer.DrawUser(grid, SplashKit.MousePosition(), MouseButton.RightButton);
                // } 
                if (!(activeButton is Button))
                {
                    if (SplashKit.MouseClicked(MouseButton.LeftButton) && setStartingPointButton.IsAt(SplashKit.MousePosition()))
                    {
                        setStartingPointButton.Active = true;
                        activeButton = setStartingPointButton;
                    }
                    else if (SplashKit.MouseClicked(MouseButton.LeftButton) && setEndingPointButton.IsAt(SplashKit.MousePosition()))
                    {
                        setEndingPointButton.Active = true;
                        activeButton = setEndingPointButton;
                    }
                    else if (SplashKit.MouseClicked(MouseButton.LeftButton) && solveDijiButton.IsAt(SplashKit.MousePosition()))
                    {
                        solveDijiButton.Active = true;
                        activeButton = solveDijiButton;
                    }
                    else if (SplashKit.MouseClicked(MouseButton.LeftButton) && solveAstarButton.IsAt(SplashKit.MousePosition()))
                    {
                        solveAstarButton.Active = true;
                        activeButton = solveAstarButton;
                    }
                }
                else if (SplashKit.MouseClicked(MouseButton.LeftButton) && activeButton.IsAt(SplashKit.MousePosition()))
                {
                    activeButton.Active = false;
                    activeButton = null;
                }

                if (setStartingPointButton.Active && (grid.IsAt(SplashKit.MousePosition()) && SplashKit.MouseClicked(MouseButton.LeftButton)))
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
                    setStartingPointButton.Active = false;
                    activeButton = null;
                } 
                else if (setEndingPointButton.Active && (grid.IsAt(SplashKit.MousePosition()) && SplashKit.MouseClicked(MouseButton.LeftButton)))
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
                    setEndingPointButton.Active = false;
                    activeButton = null;
                }
                else if (solveAstarButton.Active && startingCell is Cell && endingCell is Cell)
                {
                    grid.ResetPath();
                    solver.FindPathAstar(grid.Cells, startingCell, endingCell);

                    solveAstarButton.Active = false;
                    activeButton = null;
                }
                else if (solveDijiButton.Active && startingCell is Cell && endingCell is Cell)
                {
                    grid.ResetPath();
                    solver.FindPathDiji(grid.Cells, startingCell, endingCell);

                    solveDijiButton.Active = false;
                    activeButton = null;
                }


                // if (SplashKit.MouseClicked(MouseButton.LeftButton) || SplashKit.MouseDown(MouseButton.LeftButton)) 
                // {
                //     obstacleDrawer.DrawUser(grid, SplashKit.MousePosition(), MouseButton.LeftButton);
                // } 
                // else if (SplashKit.MouseClicked(MouseButton.RightButton) || SplashKit.MouseDown(MouseButton.RightButton)) 
                // {
                //     obstacleDrawer.DrawUser(grid, SplashKit.MousePosition(), MouseButton.RightButton);
                // } 
                // else if (SplashKit.KeyTyped(KeyCode.SKey))
                // {
                //     Cell clickedCell = grid.GetTargetCell(SplashKit.MousePosition());
                //     if (startingCell is Cell && (startingCell != clickedCell))
                //     {
                //         startingCell.ForegroudColor = Color.White;
                //     }
                //     if (clickedCell is Cell)
                //     {
                //         startingCell = clickedCell;
                //         startingCell.ForegroudColor = Color.LimeGreen;
                //     }
                // }
                // else if (SplashKit.KeyTyped(KeyCode.EKey))
                // {
                //     Cell clickedCell = grid.GetTargetCell(SplashKit.MousePosition());
                //     if (endingCell is Cell && (endingCell != clickedCell))
                //     {
                //         endingCell.ForegroudColor = Color.White;
                //     }
                //     if (clickedCell is Cell)
                //     {
                //         endingCell = clickedCell;
                //         endingCell.ForegroudColor = Color.Red;
                //     }
                // }

                // if (SplashKit.KeyTyped(KeyCode.RKey))
                // {
                //     grid.ResetGrid();
                //     startingCell = null;
                //     endingCell = null;
                // }

                // if (SplashKit.KeyTyped(KeyCode.TKey))
                // {
                //     grid.ResetPath();
                //     if (startingCell is Cell)
                //     {
                //         startingCell.ForegroudColor = Color.LimeGreen;
                //     }
                //     if (endingCell is Cell)
                //     {
                //         endingCell.ForegroudColor = Color.Red;
                //     }
                // }
                // if (SplashKit.KeyTyped(KeyCode.MKey))
                // {
                //     startingCell = null;
                //     endingCell = null;
                //     grid.ResetGrid();
                //     obstacleDrawer.DrawMaze(grid);
                // }

                // if (SplashKit.KeyTyped(KeyCode.NKey))
                // {
                //     startingCell = null;
                //     endingCell = null;
                //     grid.ResetGrid();
                //     obstacleDrawer.Random(grid);
                // }

                // if (startingCell is Cell && endingCell is Cell && SplashKit.KeyTyped(KeyCode.FKey))
                // {
                //     grid.ResetPath();
                //     solver.FindPathAstar(grid.Cells, startingCell, endingCell);
                // }

                // if (startingCell is Cell && endingCell is Cell && SplashKit.KeyTyped(KeyCode.GKey))
                // {
                //     grid.ResetPath();
                //     solver.FindPathDiji(grid.Cells, startingCell, endingCell);
                // }


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

        public static void InitUI()
        {
            const int START_X = 900;
            const int START_Y = 30;
            const int MARGIN = 50;
            const int LINE_SPACE = 20;
            const int TITLE_FONT_SIZE = 32;
            const int TEXT_FONT_SIZE = 16;
            const int BUTTON_FONT_SIZE = 16;

            int currentX = START_X;
            int currentY = START_Y;
            Font textFont = new Font("textFont", "resources/Roboto-Regular.ttf");
            Font titleFont = new Font("titleFont", "resources/Roboto-Black.ttf");
            Font buttonFont = new Font("buttonFont", "resources/Roboto-Black.ttf");

            SplashKit.DrawText("Pathfinding Visualizer", Color.Black, titleFont, TITLE_FONT_SIZE, currentX, currentY);
            currentY += MARGIN;

            SplashKit.DrawText("The purpose of this program is to allow you to visualize how", Color.Black, textFont, TEXT_FONT_SIZE, currentX, currentY);
            currentY += LINE_SPACE;
            SplashKit.DrawText("Dijkstra's and A* pathfinding algorithms function.", Color.Black, textFont, TEXT_FONT_SIZE, currentX, currentY);
            
            currentY += MARGIN;
            SplashKit.DrawText("Set Starting and Ending Cells", Color.Black, titleFont, TEXT_FONT_SIZE, currentX, currentY);
            currentY += LINE_SPACE + 10;
            setStartingPointButton = new Button(currentX, currentY, 150, 50, Color.Black, "Starting Cell", BUTTON_FONT_SIZE, buttonFont, Color.White, Color.Red);
            currentX += 200;

            setEndingPointButton = new Button(currentX, currentY, 150, 50, Color.Black, "Ending Cell", BUTTON_FONT_SIZE, buttonFont, Color.White, Color.Red);

            currentX -= 200;
            currentY += MARGIN * 2;
            SplashKit.DrawText("Select Pathfinding Algorithm", Color.Black, titleFont, TEXT_FONT_SIZE, currentX, currentY);
            currentY += LINE_SPACE + 10;
            solveDijiButton = new Button(currentX, currentY, 150, 50, Color.Black, "Dijkstra", BUTTON_FONT_SIZE, buttonFont, Color.White, Color.Red);
            currentX += 200;

            solveAstarButton = new Button(currentX, currentY, 150, 50, Color.Black, "A*", BUTTON_FONT_SIZE, buttonFont, Color.White, Color.Red);
        }

        public static void DrawUI()
        {
            const int START_X = 900;
            const int START_Y = 30;
            const int MARGIN = 50;
            const int LINE_SPACE = 20;
            const int TITLE_FONT_SIZE = 32;
            const int TEXT_FONT_SIZE = 16;

            int currentX = START_X;
            int currentY = START_Y;
            Font textFont = new Font("textFont", "resources/Roboto-Regular.ttf");
            Font titleFont = new Font("titleFont", "resources/Roboto-Black.ttf");
            Font buttonFont = new Font("buttonFont", "resources/Roboto-Black.ttf");

            SplashKit.DrawText("Pathfinding Visualizer", Color.Black, titleFont, TITLE_FONT_SIZE, currentX, currentY);
            currentY += MARGIN;

            SplashKit.DrawText("The purpose of this program is to allow you to visualize how", Color.Black, textFont, TEXT_FONT_SIZE, currentX, currentY);
            currentY += LINE_SPACE;
            SplashKit.DrawText("Dijkstra's and A* pathfinding algorithms function.", Color.Black, textFont, TEXT_FONT_SIZE, currentX, currentY);
            
            currentY += MARGIN;
            SplashKit.DrawText("Set Starting and Ending Cells", Color.Black, titleFont, TEXT_FONT_SIZE, currentX, currentY);

            currentY += LINE_SPACE + 10;
            currentY += MARGIN * 2;
            SplashKit.DrawText("Select Pathfinding Algorithm", Color.Black, titleFont, TEXT_FONT_SIZE, currentX, currentY);

            setStartingPointButton.Draw();
            setEndingPointButton.Draw();
            solveDijiButton.Draw();
            solveAstarButton.Draw();
        }
    }
}