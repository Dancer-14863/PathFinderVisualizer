using SplashKitSDK;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            }

            distance[startingCell] = 0;
            openSet.Add(startingCell);
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
                    startingCell.ForegroudColor = Color.LimeGreen;
                    endingCell.ForegroudColor = Color.Red;
                    break;
                }

                foreach(Cell neighbor in current.NeighboringCells)
                {
                    if (!neighbor.Walkable)
                    {
                        continue;
                    }

                    double newDistance = distance[current] + CalculateDistanceManhattan(current, neighbor);

                    if (newDistance < distance[neighbor])
                    {
                        distance[neighbor] = newDistance;
                        if (cameFrom.ContainsKey(neighbor))
                        {
                            cameFrom[neighbor] = current;
                        }
                        else
                        {
                            cameFrom.Add(new KeyValuePair<Cell, Cell>(neighbor, current));
                        }

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }

                    }
                }

                double scaling = CalculateScalingFactor(startingCell, endingCell, current);
                current.ForegroudColor = Color.RGBAColor(1 - scaling * 0.45, scaling, 0, 0.75);
                startingCell.ForegroudColor = Color.LimeGreen;
                endingCell.ForegroudColor = Color.Red;
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
                    startingCell.ForegroudColor = Color.LimeGreen;
                    endingCell.ForegroudColor = Color.Red;
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
                
                double scaling = CalculateScalingFactor(startingCell, endingCell, current);
                current.ForegroudColor = Color.RGBAColor(1 - scaling * 0.45, scaling, 0, 0.75);
                startingCell.ForegroudColor = Color.LimeGreen;
                endingCell.ForegroudColor = Color.Red;
                await Task.Delay(10);
            }
        }

        public async void ReconstructPath(IDictionary<Cell, Cell> cameFrom, Cell current)
        {
            List<Cell> path = new List<Cell>();
            path.Add(current);
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Insert(0, current);
            }
            foreach(Cell cell in path)
            {
                cell.ForegroudColor = Color.RGBAColor(1, 1, 0, 0.9);
                await Task.Delay(10);
            }

        }

        private Double CalculateScalingFactor(Cell startingCell, Cell endingCell, Cell current)
        {
            return (CalculateDistanceManhattan(current, endingCell) / CalculateDistanceManhattan(startingCell, endingCell));
        }

        public double CalculateDistanceManhattan(Cell startingCell, Cell endingCell)
        {
            return Math.Abs(startingCell.X - endingCell.X) + Math.Abs(startingCell.Y - endingCell.Y);
        }
    }
}