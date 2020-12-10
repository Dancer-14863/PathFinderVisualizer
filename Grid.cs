using SplashKitSDK;

namespace PathFinderVisualizer
{
    /// <summary>
    /// Grid class derived from the Rectangle1 class.
    /// Used to represent a grid made up of multiple cells
    /// </summary>
    public class Grid : Rectangle1
    {
        private int _numberOfCellsInRow;
        private int _cellPadding;
        private Cell[,] _grid;

        /// <summary>
        /// Constructor method for the Grid class.
        /// </summary>
        /// <param name="x">starting X coordinate</param>
        /// <param name="y">starting Y coordinate</param>
        /// <param name="width">width of the grid</param>
        /// <param name="height">height of the grid</param>
        /// <param name="color">color of the grid</param>
        /// <param name="cells">number of cells in the grid</param>
        /// <param name="padding">padding of the cells in the grid</param>
        public Grid(double x, double y, int width, int height, Color color, int cells, int padding) : base (x, y, width, height, color)
        {
            _numberOfCellsInRow = cells;
            _cellPadding = padding;
            SetUpCells();
            LinkCells();
        }

        /// <summary>
        /// Readonly property for teh Cells field
        /// </summary>
        public Cell[,] Cells
        {
            get { return _grid; }
        }


        /// <summary>
        /// Initializes all the cell objects that make up the grid
        /// </summary>
        private void SetUpCells()
        {
            int cellWidth = _width / _numberOfCellsInRow;
            int cellHeight = _height / _numberOfCellsInRow;
            _grid = new Cell[_numberOfCellsInRow, _numberOfCellsInRow];
            double X = _x;
            double Y = _y;

            for (int i = 0; i < _numberOfCellsInRow; i++)
            {
                Y = _y;
                for (int j = 0; j < _numberOfCellsInRow; j++)
                {
                    Cell newCell = new Cell(X, Y, cellWidth, cellHeight, Color.Black, Color.White, _cellPadding);
                    Y += cellHeight;
                    _grid[i, j] = newCell;
                }
                X += cellWidth;
            }
        }

        /// <summary>
        /// Finds the neighboring cells of every cell grid
        /// </summary>
        private void LinkCells()
        {
            /*
                For every cell in the grid, checks all the neighboring cells
                in the four cardinal directions. If neighbors exists within the bounds of the
                grid they are added to cells neighbor list
            */
            for (int i = 0; i < _numberOfCellsInRow; i++)
            {
                for (int j = 0; j < _numberOfCellsInRow; j++)
                {
                    // cell at south
                    if (i + 1 < _numberOfCellsInRow)
                    {
                        _grid[i, j].NeighboringCells.Add(_grid[i + 1, j]);
                    }
                    // cell at north
                    if (i - 1 >= 0)
                    {
                        _grid[i, j].NeighboringCells.Add(_grid[i - 1, j]);
                    }
                    // cell at east
                    if (j + 1 < _numberOfCellsInRow)
                    {
                        _grid[i, j].NeighboringCells.Add(_grid[i, j + 1]);
                    }
                    // cell at west
                    if (j - 1 >= 0)
                    {
                        _grid[i, j].NeighboringCells.Add(_grid[i, j - 1]);
                    }
                }
            }
        }

        /// <summary>
        /// Draws the grid on the screen
        /// </summary>
        public override void Draw()
        {
            foreach(Cell cell in _grid)
            {
                cell.Draw();
            }

        }
        
        /// <summary>
        /// Resets the grid by setting all the cells 
        /// as walkable. This removes any obstacles
        /// </summary>
        public void ResetGrid()
        {
            foreach(Cell cell in _grid)
            {
                cell.Walkable = true;
            }
        }

        /// <summary>
        /// Resets the path drawn on a grid by a pathfinding algorithm.
        /// All cells which are not walkable foreground color is set to white.
        /// </summary>
        public void ResetPath()
        {
            foreach(Cell cell in _grid)
            {
                if (cell.Walkable)
                {
                    cell.ForegroudColor = Color.White;
                }
            }
        }

        /// <summary>
        /// Checks if the grid has been clicked using the passed
        /// mouse button.
        /// </summary>
        /// <param name="clickedButton">Mouse button to check against</param>
        /// <returns>True if the grid has been clicked with the passed mouse button, else false</returns>
        public override bool HasBeenClicked(MouseButton clickedButton)
        {
            bool clicked = false;
            foreach (Cell cell in _grid)
            {
                if (cell.IsAt(SplashKit.MousePosition()))
                {
                    /*
                        If a cell is clicked with the left mouse button it is set 
                        as a obstacle, if it is clicked with the right mouse button it 
                        is made walkable again
                    */
                    switch (clickedButton)
                    {
                        case MouseButton.LeftButton:
                            cell.Walkable = false;
                            clicked = true;
                            break;
                        case MouseButton.RightButton:
                            cell.Walkable = true;
                            clicked = true;
                            break;
                    }
                }
            }
            return clicked;
        }

        /// <summary>
        /// Gets the cell at the passed point 
        /// </summary>
        /// <param name="pt">Point location to find the cell in </param>
        /// <returns>Cell object at the passed location else null</returns>
        public Cell GetTargetCell(Point2D pt)
        {
            Cell clickedCell = null;
            foreach (Cell cell in _grid)
            {
                if (cell.IsAt(pt))
                {
                    clickedCell = cell;
                    break;
                }
            }
            return clickedCell;
        }


    }
}