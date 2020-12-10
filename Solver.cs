using SplashKitSDK;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PathFinderVisualizer
{
    /// <summary>
    /// Solver class. Contains the pathfinding algorithms
    /// </summary>
    public class Solver
    {
        private int _taskDelayTime;

        /// <summary>
        /// Constructor method for the solver class
        /// </summary>
        public Solver() 
        {
            _taskDelayTime = 10;
        }

        /// <summary>
        /// Writeonly property for the task delay time field
        /// </summary>
        public int TaskDelayTime
        {
            set { _taskDelayTime = value; }
        }

        /// <summary>
        /// Dijkstra's algorithm implementation.
        /// </summary>
        /// <param name="cells">2d array containing the cells to visit</param>
        /// <param name="startingCell">Starting cell</param>
        /// <param name="endingCell">Ending/Target cell</param>
        public async void FindPathDiji(Cell[,] cells, Cell startingCell, Cell endingCell)
        {
            List<Cell> openSet = new List<Cell>();
            IDictionary<Cell, double> distance = new Dictionary<Cell, double>();
            IDictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();
            Cell firstCell = cells[0, 0];

            //initializing the dictionary
            foreach (Cell cell in cells)
            {
                distance.Add(new KeyValuePair<Cell, double>(cell, double.PositiveInfinity));
            }

            distance[startingCell] = 0;
            openSet.Add(startingCell);
            while (openSet.Count > 0)
            {
                Cell current = openSet[0];
                // sets the cell with the smallest distance as the current cell
                foreach(Cell cell in openSet)
                {
                    if (distance[cell] <= distance[current])
                    {
                        current = cell;
                    }
                }

                openSet.Remove(current);

                // if current cell is the ending cell, a path has been found
                if (current == endingCell)
                {
                    // draws the shortest path
                    await ReconstructPath(cameFrom, current);
                    startingCell.ForegroudColor = Color.LimeGreen;
                    endingCell.ForegroudColor = Color.Red;
                    break;
                }

                foreach(Cell neighbor in current.NeighboringCells)
                {
                    // checks if the neighbor is not an obstacle
                    if (!neighbor.Walkable)
                    {
                        continue;
                    }

                    double newDistance = distance[current] + CalculateDistanceManhattan(current, neighbor);

                    // if a shorter path to neighbor has been found
                    if (newDistance < distance[neighbor])
                    {
                        if (cameFrom.ContainsKey(neighbor))
                        {
                            cameFrom[neighbor] = current;
                        }
                        else
                        {
                            cameFrom.Add(new KeyValuePair<Cell, Cell>(neighbor, current));
                        }
                        distance[neighbor] = newDistance;

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }

                    }
                }

                /*
                    Scaling is used to decide the color of the current cell. The closer it is to the 
                    starting cell, the greener the cell color. The closer it is to the target cell
                    the redder it is
                */
                double scaling = CalculateScalingFactor(startingCell, endingCell, current);
                current.ForegroudColor = Color.RGBAColor(1 - scaling * 0.45, scaling, 0, 0.75);
                startingCell.ForegroudColor = Color.LimeGreen;
                endingCell.ForegroudColor = Color.Red;
                // used to pause the algorithm so that the changes can be highlighted on the screen
                await Task.Delay(_taskDelayTime);
            }
        }

        /// <summary>
        /// A* algorithm implementation.
        /// </summary>
        /// <param name="cells">2d array containing the cells to visit</param>
        /// <param name="startingCell">Starting cell</param>
        /// <param name="endingCell">Ending/Target cell</param>
        public async void FindPathAstar(Cell[,] cells, Cell startingCell, Cell endingCell)
        {
            List<Cell> openSet = new List<Cell>();
            IDictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();
            IDictionary<Cell, double> gScore = new Dictionary<Cell, double>();
            IDictionary<Cell, double> fScore = new Dictionary<Cell, double>();

            // initializing the dictionaries
            foreach (Cell cell in cells)
            {
                gScore.Add(new KeyValuePair<Cell, double>(cell, double.PositiveInfinity));
                fScore.Add(new KeyValuePair<Cell, double>(cell, double.PositiveInfinity));
            }

            gScore[startingCell] = 0;
            fScore[startingCell] = CalculateDistanceManhattan(startingCell, endingCell);

            openSet.Add(startingCell);

            while (openSet.Count > 0)
            {
                Cell current = openSet[0];
                // sets the cell with the smallest fscore as teh current cell
                foreach(Cell cell in openSet)
                {
                    if (fScore[cell] <= fScore[current])
                    {
                        current = cell;
                    }
                }

                openSet.Remove(current);

                // if path found
                if (current == endingCell)
                {
                    // draws the shortest path
                    await ReconstructPath(cameFrom, current);
                    startingCell.ForegroudColor = Color.LimeGreen;
                    endingCell.ForegroudColor = Color.Red;
                    break;
                }

                foreach(Cell neighbor in current.NeighboringCells)
                {
                    // checks if the neighbor is not an obstacle
                    if (!neighbor.Walkable)
                    {
                        continue;
                    }

                    double tenativegScore = gScore[current] + CalculateDistanceManhattan(current, neighbor);

                    // if a shorter path to neighbor has been found
                    if (tenativegScore < gScore[neighbor])
                    {
                        if (cameFrom.ContainsKey(neighbor))
                        {
                            cameFrom[neighbor] = current;
                        }
                        else
                        {
                            cameFrom.Add(new KeyValuePair<Cell, Cell>(neighbor, current));
                        }
                        gScore[neighbor] = tenativegScore;
                        fScore[neighbor] = gScore[neighbor] + CalculateDistanceManhattan(neighbor, endingCell);

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }

                }
                
                /*
                    Scaling is used to decide the color of the current cell. The closer it is to the 
                    starting cell, the greener the cell color. The closer it is to the target cell
                    the redder it is
                */
                double scaling = CalculateScalingFactor(startingCell, endingCell, current);
                current.ForegroudColor = Color.RGBAColor(1 - scaling * 0.45, scaling, 0, 0.75);
                startingCell.ForegroudColor = Color.LimeGreen;
                endingCell.ForegroudColor = Color.Red;
                await Task.Delay(_taskDelayTime);
            }
        }

        /// <summary>
        /// Reconstructs the path from the starting cell to the current cell
        /// </summary>
        /// <param name="cameFrom">Dictionary containing cell and the cell it was visited from</param>
        /// <param name="current">Target cell</param>
        private async Task ReconstructPath(IDictionary<Cell, Cell> cameFrom, Cell current)
        {
            List<Cell> path = new List<Cell>();
            // reconstructs path
            path.Add(current);
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Insert(0, current);
            }

            // highlights the cells in the path
            foreach(Cell cell in path)
            {
                cell.ForegroudColor = Color.RGBAColor(1, 1, 0, 0.9);
                await Task.Delay(_taskDelayTime);
            }

        }

        /// <summary>
        /// Calculates the scaling factor for the colors of the current cell based on distance to
        /// the starting and ending cell
        /// </summary>
        /// <param name="startingCell">Starting cell</param>
        /// <param name="endingCell">Ending/Target cell</param>
        /// <param name="current">Cell to calculate the scaling factor for</param>
        /// <returns></returns>
        private Double CalculateScalingFactor(Cell startingCell, Cell endingCell, Cell current)
        {
            return (CalculateDistanceManhattan(current, endingCell) / CalculateDistanceManhattan(startingCell, endingCell));
        }

        /// <summary>
        /// Calculates the manhattan distance between the two passed cell
        /// </summary>
        /// <param name="startingCell">starting cell</param>
        /// <param name="endingCell">ending cell</param>
        private double CalculateDistanceManhattan(Cell startingCell, Cell endingCell)
        {
            return Math.Abs(startingCell.X - endingCell.X) + Math.Abs(startingCell.Y - endingCell.Y);
        }
    }
}