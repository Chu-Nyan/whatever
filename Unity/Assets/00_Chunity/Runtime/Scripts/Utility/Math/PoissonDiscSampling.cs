using System.Collections.Generic;
using UnityEngine;

namespace Chunity.Utility
{
    public static class PoissonDiscSampling
    {
        public static List<Vector2> GeneratePoints(string seed, float minDistance, Vector2 regionSize, int attemptNumber = 30)
        {
            System.Random prng = new(seed.GetHashCode());
            float cellSize = minDistance / Mathf.Sqrt(2);
            int[,] grid = new int[Mathf.CeilToInt(regionSize.x / cellSize), Mathf.CeilToInt(regionSize.y / cellSize)];
            List<Vector2> points = new();
            List<Vector2> checkList = new();

            Vector2 start = new((float)(prng.NextDouble() * regionSize.x), (float)(prng.NextDouble() * regionSize.y));
            Register(start, cellSize, points, checkList, grid);

            while (checkList.Count > 0)
            {
                int index = prng.Next(0, checkList.Count);
                Vector2 spawn = checkList[index];
                bool candidateAccepted = false;

                for (int i = 0; i < attemptNumber; i++)
                {
                    float angle = (float)prng.NextDouble() * Mathf.PI * 2;
                    Vector2 dir = new(Mathf.Sin(angle), Mathf.Cos(angle));
                    float dist = minDistance + ((float)prng.NextDouble() * minDistance);
                    Vector2 candidate = spawn + dir * dist;

                    if (CanGenerate(candidate, regionSize, cellSize, minDistance, points, grid) == true)
                    {
                        Register(candidate, cellSize, points, checkList, grid);
                        candidateAccepted = true;
                        break;
                    }
                }

                if (candidateAccepted == false)
                {
                    checkList.RemoveAt(index);
                }
            }

            return points;

        }

        private static void Register(Vector2 item, float cellSize, List<Vector2> points, List<Vector2> checkList, int[,] grid)
        {
            points.Add(item);
            checkList.Add(item);
            grid[(int)(item.x / cellSize), (int)(item.y / cellSize)] = points.Count;
        }

        private static bool CanGenerate(Vector2 candidate, Vector2 regionSize, float cellSize, float radius, List<Vector2> points, int[,] grid)
        {
            if (candidate.x < 0 || candidate.x >= regionSize.x
             || candidate.y < 0 || candidate.y >= regionSize.y)
                return false;

            int cellX = (int)(candidate.x / cellSize);
            int cellY = (int)(candidate.y / cellSize);
            int startX = Mathf.Max(0, cellX - 2);
            int startY = Mathf.Max(0, cellY - 2);
            int endX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
            int endY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);
            float sqrRadius = radius * radius;

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    if (grid[x, y] <= 0)
                        continue;

                    float sqrDst = (candidate - points[grid[x, y] - 1]).sqrMagnitude;
                    if (sqrDst < sqrRadius)
                        return false;
                }
            }

            return true;
        }
    }
}
