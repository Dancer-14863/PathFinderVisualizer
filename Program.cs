using SplashKitSDK;

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
        static Button drawOptionUserButton;
        static Button drawOptionRandomButton;
        static Button drawOptionMazeButton;
        static Button clearGridButton;
        static Button clearPathButton;
        static Slider animationDelaySlider;

        public static void Main()
        {
            /*
                Points to button last clicked by the user. A value of null
                indicates that there is no active button
            */
            Button activeButton = null;
            Window shapesWindow;
            shapesWindow = new Window("Path Finding Visualizer", 1366, 768);
            InitUI();
            solver.TaskDelayTime = (int)animationDelaySlider.SliderValue;

            do 
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen(Color.White);
                DrawUI();

                if (animationDelaySlider.HasBeenClicked(MouseButton.LeftButton))
                {
                    solver.TaskDelayTime = (int)animationDelaySlider.SliderValue;
                }

                // if there is no active button, sets a clicked button as teh active button
                if (!(activeButton is Button) || activeButton == drawOptionUserButton)
                {
                    if (setStartingPointButton.HasBeenClicked(MouseButton.LeftButton))
                    {
                        setStartingPointButton.Active = true;
                        activeButton = setStartingPointButton;
                    }
                    else if (setEndingPointButton.HasBeenClicked(MouseButton.LeftButton))
                    {
                        setEndingPointButton.Active = true;
                        activeButton = setEndingPointButton;
                    }
                    else if (solveDijiButton.HasBeenClicked(MouseButton.LeftButton))
                    {
                        solveDijiButton.Active = true;
                        activeButton = solveDijiButton;
                    }
                    else if (solveAstarButton.HasBeenClicked(MouseButton.LeftButton))
                    {
                        solveAstarButton.Active = true;
                        activeButton = solveAstarButton;
                    }
                    else if (drawOptionUserButton.HasBeenClicked(MouseButton.LeftButton))
                    {
                        drawOptionUserButton.Active = true;
                        activeButton = drawOptionUserButton;
                    }
                    else if (drawOptionRandomButton.HasBeenClicked(MouseButton.LeftButton))
                    {
                        drawOptionRandomButton.Active = true;
                        activeButton = drawOptionRandomButton;
                    }
                    else if (drawOptionMazeButton.HasBeenClicked(MouseButton.LeftButton))
                    {
                        drawOptionMazeButton.Active = true;
                        activeButton = drawOptionMazeButton;
                    }
                    else if (clearGridButton.HasBeenClicked(MouseButton.LeftButton))
                    {
                        clearGridButton.Active = true;
                        activeButton = clearGridButton;
                    }
                    else if (clearPathButton.HasBeenClicked(MouseButton.LeftButton))
                    {
                        clearPathButton.Active = true;
                        activeButton = clearPathButton;
                    }

                    if (drawOptionUserButton.Active && activeButton != drawOptionUserButton)
                    {
                        drawOptionUserButton.Active = false;
                    }
                }
                // if active button is set and is clicked again, removes it as active button
                else if (activeButton.HasBeenClicked(MouseButton.LeftButton))
                {
                    activeButton.Active = false;
                    activeButton = null;
                }

                // depending on the active button different functionality are carried out
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
                else if (solveAstarButton.Active)
                {
                    if (startingCell is Cell && endingCell is Cell)
                    {
                        grid.ResetPath();
                        solver.FindPathAstar(grid.Cells, startingCell, endingCell);
                    }

                    solveAstarButton.Active = false;
                    activeButton = null;
                }
                else if (solveDijiButton.Active)
                {
                    if (startingCell is Cell && endingCell is Cell)
                    {
                        grid.ResetPath();
                        solver.FindPathDiji(grid.Cells, startingCell, endingCell);
                    }

                    solveDijiButton.Active = false;
                    activeButton = null;
                }
                else if (drawOptionUserButton.Active && grid.IsAt(SplashKit.MousePosition()))
                {
                    if (SplashKit.MouseClicked(MouseButton.LeftButton) || SplashKit.MouseDown(MouseButton.LeftButton) && grid.IsAt(SplashKit.MousePosition())) 
                    {
                        obstacleDrawer.DrawUser(grid, MouseButton.LeftButton);
                    } 
                    else if (SplashKit.MouseClicked(MouseButton.RightButton) || SplashKit.MouseDown(MouseButton.RightButton) && grid.IsAt(SplashKit.MousePosition())) 
                    {
                        obstacleDrawer.DrawUser(grid, MouseButton.RightButton);
                    } 
                }
                else if (drawOptionRandomButton.Active)
                {
                    startingCell = null;
                    endingCell = null;
                    grid.ResetGrid();
                    obstacleDrawer.DrawRandom(grid);

                    drawOptionRandomButton.Active = false;
                    activeButton = null;
                }
                else if (drawOptionMazeButton.Active)
                {
                    startingCell = null;
                    endingCell = null;
                    grid.ResetGrid();
                    obstacleDrawer.DrawMaze(grid);

                    drawOptionMazeButton.Active = false;
                    activeButton = null;
                }
                else if (clearGridButton.Active)
                {
                    grid.ResetGrid();
                    startingCell = null;
                    endingCell = null;

                    clearGridButton.Active = false;
                    activeButton = null;
                }
                else if (clearPathButton.Active)
                {
                    grid.ResetPath();
                    if (startingCell is Cell)
                    {
                        startingCell.ForegroudColor = Color.LimeGreen;
                    }
                    if (endingCell is Cell)
                    {
                        endingCell.ForegroudColor = Color.Red;
                    }
                    
                    clearPathButton.Active = false;
                    activeButton = null;
                }


                grid.Draw();
                // draws the flag sprites on the starting and ending cells
                if (startingCell is Cell)
                {
                    green_flag.X = (float)startingCell.X - 2;
                    green_flag.Y = (float)startingCell.Y - 30;
                    green_flag.Draw();
                }
                if (endingCell is Cell)
                {
                    red_flag.X = (float)endingCell.X - 2;
                    red_flag.Y = (float)endingCell.Y - 30;
                    red_flag.Draw();
                }
                SplashKit.RefreshScreen(60);
            } while(!SplashKit.QuitRequested());
        }

        /// <summary>
        /// Initializes the UI buttons
        /// </summary>
        public static void InitUI()
        {
            const int BUTTON_FONT_SIZE = 16;
            Font titleFont = new Font("titleFont", "resources/Roboto-Black.ttf");
            Font buttonFont = new Font("buttonFont", "resources/Roboto-Black.ttf");

            setStartingPointButton = new Button(900, 160, 150, 50, Color.Black, "Starting Cell", BUTTON_FONT_SIZE, buttonFont, Color.White, Color.Red);
            setEndingPointButton = new Button(1100, 160, 150, 50, Color.Black, "Ending Cell", BUTTON_FONT_SIZE, buttonFont, Color.White, Color.Red);
            solveDijiButton = new Button(900, 270, 150, 50, Color.Black, "Dijkstra", BUTTON_FONT_SIZE, buttonFont, Color.White, Color.Red);
            solveAstarButton = new Button(1100, 270, 150, 50, Color.Black, "A*", BUTTON_FONT_SIZE, buttonFont, Color.White, Color.Red);
            drawOptionUserButton = new Button(900, 380, 100, 50, Color.Black, "User", BUTTON_FONT_SIZE, buttonFont, Color.White, Color.Red);
            drawOptionRandomButton = new Button(1050, 380, 100, 50, Color.Black, "Random", BUTTON_FONT_SIZE, buttonFont, Color.White, Color.Red);
            drawOptionMazeButton = new Button(1200, 380, 100, 50, Color.Black, "Maze", BUTTON_FONT_SIZE, buttonFont, Color.White, Color.Red);
            clearGridButton = new Button(900, 490, 150, 50, Color.Black, "Clear Grid", BUTTON_FONT_SIZE, buttonFont, Color.White, Color.Red);
            clearPathButton = new Button(1100, 490, 150, 50, Color.Black, "Clear Path", BUTTON_FONT_SIZE, buttonFont, Color.White, Color.Red);
            animationDelaySlider = new Slider(900, 560, 350, 100, Color.Black, Color.Red, "Animation Speed(ms) :", titleFont, 5, 35);
        }

        /// <summary>
        /// Draws the UI buttons and text
        /// </summary>
        public static void DrawUI()
        {
            const int START_X = 900;
            const int START_Y = 30;
            const int MARGIN = 40;
            const int LINE_SPACE = 20;
            const int TITLE_FONT_SIZE = 32;
            const int TEXT_FONT_SIZE = 16;

            int currentX = START_X;
            int currentY = START_Y;
            Font textFont = new Font("textFont", "resources/Roboto-Regular.ttf");
            Font titleFont = new Font("titleFont", "resources/Roboto-Black.ttf");

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

            currentY += LINE_SPACE + 10;
            currentY += MARGIN * 2;
            SplashKit.DrawText("Select Obstacle Drawing Method", Color.Black, titleFont, TEXT_FONT_SIZE, currentX, currentY);
            drawOptionUserButton.Draw();
            drawOptionRandomButton.Draw();
            drawOptionMazeButton.Draw();

            currentY += LINE_SPACE + 10;
            currentY += MARGIN * 2;
            SplashKit.DrawText("Options", Color.Black, titleFont, TEXT_FONT_SIZE, currentX, currentY);
            clearGridButton.Draw();
            clearPathButton.Draw();
            animationDelaySlider.Draw();
        }
    }
}