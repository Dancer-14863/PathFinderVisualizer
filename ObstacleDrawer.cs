using System;

namespace PathFinderVisualizer
{
    public class ObstacleDrawer
    {
        public ObstacleDrawer() {}

        public void Random(Grid grid)
        {
            Random random = new Random();
            foreach(Cell cell in grid.Cells)
            {
                if (random.NextDouble() > 0.69)
                {
                    cell.Walkable = false;
                }
            }
        }

        // followed guide from https://www.raywenderlich.com/82-procedural-generation-of-mazes-with-unity
        public void DrawMaze(Grid grid)
        {
            Random random = new Random();
            Cell[,] cells = grid.Cells;
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    /*
                        Sets the cells at the boundaries of the grid as walls
                    */
                    if (i == 0 || j == 0 || i == cells.GetLength(0) - 1 || j == cells.GetLength(1) - 1)
                    {
                        cells[i, j].Walkable = false;
                    }
                    else if (i % 2 == 0 && j % 2 == 0)
                    {
                        if (random.NextDouble() > 0.05)
                        {
                            cells[i, j].Walkable = false;

                            int a = 0;
                            int b = 0;
                            if (random.NextDouble() > 0.5)
                            {
                                if (random.NextDouble() < 0.5)
                                {
                                    a = - 1;
                                }
                                else
                                {
                                    a = 1;
                                }

                            }
                            
                            if (a == 0)
                            {
                                if (random.NextDouble() < 0.5)
                                {
                                    b = - 1;
                                }
                                else
                                {
                                    b = 1;
                                }
                            }
                            cells[i + a, j + b].Walkable = false;
                        }
                    }
                }
            }
        }
    }
}