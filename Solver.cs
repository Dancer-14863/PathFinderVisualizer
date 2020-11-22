using SplashKitSDK;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace PathFinderVisualizer
{
    public class Solver
    {
        public Solver() {}

        //https://medium.com/@nicholas.w.swift/easy-dijkstras-pathfinding-324a51eeb0f
        public async void FindPathDiji(Cell[,] cells, Cell startingCell, Cell endingCell)
        {
            List<Cell> openSet = new List<Cell>();
            IDictionary<Cell, double> distance = new Dictionary<Cell, double>();
            IDictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();
            Cell firstCell = cells[0, 0];

            foreach (Cell cell in cells)
            {
                distance.Add(new KeyValuePair<Cell, double>(cell, double.PositiveInfinity));
                openSet.Add(cell);
            }

            bool isDeadEnd = false;
            distance[startingCell] = 0;
            while (openSet.Count > 0)
            {
			
                Cell current = openSet[0];
                foreach(Cell cell in openSet)
                {
                    if (distance[cell] < distance[current])
                    {
                        current = cell;
                    }
                }

                openSet.Remove(current);

                if (current == endingCell)
                {
                    ReconstructPath(cameFrom, current);
                    break;
                }

                if (isDeadEnd && current == firstCell)
                {
                    break;
                }

                isDeadEnd = true;
                foreach(Cell neighbor in current.NeighboringCells)
                {
                    if (!neighbor.Walkable || !openSet.Contains(neighbor))
                    {
                        continue;
                    }

                    if (neighbor.Walkable)
                    {
                        isDeadEnd = false;
                    }

                    double newDistance = distance[current] + CalculateDistanceManhattan(current, neighbor);

                    if (newDistance < distance[neighbor])
                    {
                        neighbor.ForegroudColor = Color.LimeGreen;
                        distance[neighbor] = newDistance;
                        if (cameFrom.ContainsKey(neighbor))
                        {
                            cameFrom[neighbor] = current;
                        }
                        else
                        {
                            cameFrom.Add(new KeyValuePair<Cell, Cell>(neighbor, current));
                        }
                    }
                }
                current.ForegroudColor = Color.SwinburneRed;
                startingCell.ForegroudColor = Color.Purple;
                endingCell.ForegroudColor = Color.Purple;
                await Task.Delay(10);
            }
        }

        public async void FindPathAstar(Cell[,] cells, Cell startingCell, Cell endingCell)
        {
            List<Cell> openSet = new List<Cell>();
            IDictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();
            IDictionary<Cell, double> gScore = new Dictionary<Cell, double>();
            IDictionary<Cell, double> fScore = new Dictionary<Cell, double>();

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
                foreach(Cell cell in openSet)
                {
                    if (fScore[cell] < fScore[current])
                    {
                        current = cell;
                    }
                    if (fScore[cell] == fScore[current] && CalculateDistanceManhattan(cell, endingCell) < CalculateDistanceManhattan(current, endingCell))
                    {
                        current = cell;
                    }
                }

                openSet.Remove(current);

                if (current == endingCell)
                {
                    ReconstructPath(cameFrom, current);
                    startingCell.ForegroudColor = Color.Purple;
                    endingCell.ForegroudColor = Color.Purple;
                    break;
                }
                


                foreach(Cell neighbor in current.NeighboringCells)
                {
                    if (!neighbor.Walkable)
                    {
                        continue;
                    }

                    double tenativegScore = gScore[current] + CalculateDistanceManhattan(current, neighbor);

                    if (tenativegScore < gScore[neighbor])
                    {
                        neighbor.ForegroudColor = Color.LimeGreen;
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
                current.ForegroudColor = Color.SwinburneRed;
                startingCell.ForegroudColor = Color.Purple;
                endingCell.ForegroudColor = Color.Purple;
                await Task.Delay(10);
            }
        }

        public void ReconstructPath(IDictionary<Cell, Cell> cameFrom, Cell current)
        {
            List<Cell> path = new List<Cell>();
            path.Add(current);
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Insert(0, current);
                current.ForegroudColor = Color.Purple;
            }
        }

        public double CalculateDistanceManhattan(Cell startingCell, Cell endingCell)
        {
            return Math.Abs(startingCell.X - endingCell.X) + Math.Abs(startingCell.Y - endingCell.Y);
        }

    }
}