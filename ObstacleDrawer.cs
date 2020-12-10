using System;
using SplashKitSDK;

namespace PathFinderVisualizer
{
    /// <summary>
    /// ObstacleDrawer class. Used to handle the obstacle drawing functionality of the grid
    /// </summary>
    public class ObstacleDrawer
    {
        /// <summary>
        /// Constructor for Obstacle drawer class
        /// </summary>
        public ObstacleDrawer() {}

        /// <summary>
        /// Obstacle drawn by user option
        /// </summary>
        /// <param name="grid">Grid obstacles to be drawn on</param>
        /// <param name="button">Clicked mouse button</param>
        public void DrawUser(Grid grid, MouseButton button)
        {
            grid.HasBeenClicked(button);
        }

        /// <summary>
        /// Randomly draws obstacles on the grid
        /// </summary>
        /// <param name="grid">Grid obstacles to be drawn on</param>
        public void DrawRandom(Grid grid)
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

        /// <summary>
        /// Draws a random maze on the grid
        /// followed guide from https://www.raywenderlich.com/82-procedural-generation-of-mazes-with-unity
        /// </summary>
        /// <param name="grid">Grid obstacles to be drawn on</param>
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