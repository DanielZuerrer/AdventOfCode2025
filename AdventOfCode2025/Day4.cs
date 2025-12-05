namespace AdventOfCode2025;

public static class Day4
{
    public static string A()
    {
        var input = File.ReadAllLines("Inputs/day4.txt");

        var grid = input
            .Select(line => line.ToCharArray().Select(c => c is '@' ? 1 : 0).ToArray())
            .ToArray();

        var solution = 0;

        _ = RemoveAndUpdateCount(grid, 1, ref solution);

        return solution.ToString();
    }

    private static int[][] RemoveAndUpdateCount(
        int[][] grid,
        int maxIterations,
        ref int countRemoved
    )
    {
        if (maxIterations <= 0)
        {
            return grid;
        }

        var totalAccessible = 0;

        var newGrid = new int[grid.Length][];

        for (var row = 0; row < grid.Length; row++)
        {
            newGrid[row] = new int[grid[row].Length];

            for (var col = 0; col < grid[0].Length; col++)
            {
                var topRow = row - 1;
                var bottomRow = row + 1;
                var leftCol = col - 1;
                var rightCol = col + 1;

                var topLeftVal = topRow >= 0 && leftCol >= 0 ? grid[topRow][leftCol] : 0;
                var topVal = topRow >= 0 ? grid[topRow][col] : 0;
                var topRightVal =
                    topRow >= 0 && rightCol < grid[0].Length ? grid[topRow][rightCol] : 0;

                var leftVal = leftCol >= 0 ? grid[row][leftCol] : 0;
                var rightVal = rightCol < grid[0].Length ? grid[row][rightCol] : 0;

                var bottomLeftVal =
                    bottomRow < grid.Length && leftCol >= 0 ? grid[bottomRow][leftCol] : 0;
                var bottomVal = bottomRow < grid.Length ? grid[bottomRow][col] : 0;
                var bottomRightVal =
                    bottomRow < grid.Length && rightCol < grid[0].Length
                        ? grid[bottomRow][rightCol]
                        : 0;

                var surroundingSum =
                    topLeftVal
                    + topVal
                    + topRightVal
                    + leftVal
                    + rightVal
                    + bottomLeftVal
                    + bottomVal
                    + bottomRightVal;

                if (grid[row][col] == 1 && surroundingSum < 4)
                {
                    totalAccessible++;
                    newGrid[row][col] = 0;
                    // Console.Write("x");
                }
                else
                {
                    newGrid[row][col] = grid[row][col];

                    // Console.Write(grid[row][col] == 1 ? "@" : ".");
                }
            }

            // Console.Write('\n');
        }

        countRemoved += totalAccessible;

        if (totalAccessible > 0)
        {
            RemoveAndUpdateCount(newGrid, maxIterations - 1, ref countRemoved);
        }

        return newGrid;
    }

    public static string B()
    {
        var input = File.ReadAllLines("Inputs/day4.txt");

        var grid = input
            .Select(line => line.ToCharArray().Select(c => c is '@' ? 1 : 0).ToArray())
            .ToArray();

        var solution = 0;

        _ = RemoveAndUpdateCount(grid, int.MaxValue, ref solution);

        return solution.ToString();
    }
}
