using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace PathFinderVisualizer
{
    public class Grid : Rectangle1
    {
        private int _numberOfCellsInRow;
        private int _cellPadding;
        private Cell[,] _grid;

        public Grid(float x, float y, int width, int height, Color color, int cells, int padding) : base (x, y, width, height, color)
        {
            _numberOfCellsInRow = cells;
            _cellPadding = padding;
            SetUpCells();
            LinkCells();
        }

        public Cell[,] Cells
        {
            get { return _grid; }
        }

        private void SetUpCells()
        {
            int cellWidth = _width / _numberOfCellsInRow;
            int cellHeight = _height / _numberOfCellsInRow;
            _grid = new Cell[_numberOfCellsInRow, _numberOfCellsInRow];
            float X = _x;
            float Y = _y;

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
        private void LinkCells()
        {
            for (int i = 0; i < _numberOfCellsInRow; i++)
            {
                for (int j = 0; j < _numberOfCellsInRow; j++)
                {
                    if (i + 1 < _numberOfCellsInRow)
                    {
                        _grid[i, j].NeighboringCells.Add(_grid[i + 1, j]);
                    }
                    if (i - 1 >= 0)
                    {
                        _grid[i, j].NeighboringCells.Add(_grid[i - 1, j]);
                    }
                    if (j + 1 < _numberOfCellsInRow)
                    {
                        _grid[i, j].NeighboringCells.Add(_grid[i, j + 1]);
                    }
                    if (j - 1 >= 0)
                    {
                        _grid[i, j].NeighboringCells.Add(_grid[i, j - 1]);
                    }
                }
            }
        }

        public override void Draw()
        {
            foreach(Cell cell in _grid)
            {
                cell.Draw();
            }

        }
        
        public void ResetGrid()
        {
            foreach(Cell cell in _grid)
            {
                cell.Walkable = true;
            }
        }

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

        public void HasBeenClicked(Point2D pt, MouseButton clickedButton)
        {
            foreach (Cell cell in _grid)
            {
                if (cell.IsAt(pt))
                {
                    switch (clickedButton)
                    {
                        case MouseButton.LeftButton:
                            cell.Walkable = false;
                            break;
                        case MouseButton.RightButton:
                            cell.Walkable = true;
                            break;
                    }
                }
            }
        }

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