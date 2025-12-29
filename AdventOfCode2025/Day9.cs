using System.Numerics;
using SkiaSharp;

namespace AdventOfCode2025;

public static class Day9
{
    private record TileCoord(int Col, int Row);

    public static string A()
    {
        var input = File.ReadAllLines("Inputs/day9.txt");

        var redTiles = input
            .Select(line => line.Split(','))
            .Select(xAndY => new TileCoord(int.Parse(xAndY[0]), int.Parse(xAndY[1])))
            .ToArray();

        var maxArea = 0L;

        for (var thisTileI = 0; thisTileI < redTiles.Length; thisTileI++)
        {
            for (var otherTileI = thisTileI + 1; otherTileI < redTiles.Length; otherTileI++)
            {
                // + 1 because bounds are inclusive on tile grid
                var width = Math.Abs(redTiles[thisTileI].Col - redTiles[otherTileI].Col) + 1;
                var height = Math.Abs(redTiles[thisTileI].Row - redTiles[otherTileI].Row) + 1;

                var area = width * height;

                if (area > maxArea)
                {
                    maxArea = area;
                }
            }
        }

        return maxArea.ToString();
    }

    public static string B()
    {
        var input = File.ReadAllLines("Inputs/day9.txt");

        var redTiles = input
            .Select(line => line.Split(','))
            .Select(xAndY => new TileCoord(int.Parse(xAndY[0]), int.Parse(xAndY[1])))
            .ToArray();

        var top1 = redTiles.OrderBy(t => t.Row).First().Row;
        var left1 = redTiles.OrderBy(t => t.Col).First().Col;

        redTiles = redTiles.Select(t => new TileCoord(t.Col - left1, t.Row - top1)).ToArray();

        var bottom1 = redTiles.OrderByDescending(t => t.Row).First().Row;
        var right1 = redTiles.OrderByDescending(t => t.Col).First().Col;

        var grid = new int[bottom1 + 1][];
        for (var row = 0; row < grid.Length; row++)
        {
            grid[row] = new int[right1 + 1];
        }

        for (var i = 0; i < redTiles.Length; i++)
        {
            var redTile = redTiles[i];
            var nextIndex = (i + 1) % redTiles.Length;

            var nextRedTile = redTiles[nextIndex];

            grid[redTile.Row][redTile.Col] = 1;

            // same column
            if (redTile.Col == nextRedTile.Col)
            {
                var topStart = Math.Min(redTile.Row, nextRedTile.Row) + 1;
                var bottomEnd = Math.Max(redTile.Row, nextRedTile.Row) - 1;
                for (var row = topStart; row <= bottomEnd; row++)
                {
                    grid[row][redTile.Col] = 2;
                }
            }
            // same row
            else if (redTile.Row == nextRedTile.Row)
            {
                var leftStart = Math.Min(redTile.Col, nextRedTile.Col) + 1;
                var rightEnd = Math.Max(redTile.Col, nextRedTile.Col) - 1;
                for (var col = leftStart; col <= rightEnd; col++)
                {
                    grid[redTile.Row][col] = 2;
                }
            }
        }

        for (var rowIndex = 0; rowIndex < grid.Length; rowIndex++)
        {
            var row = grid[rowIndex];
            var isInside = false;
            for (var i = 0; i < row.Length; i++)
            {
                if (row[i] > 0 && (i + 1 < row.Length && row[i + 1] == 0))
                {
                    isInside = !isInside;
                }

                if (isInside)
                {
                    row[i] = 2;
                }
            }

            Console.WriteLine($"Filled row {rowIndex + 1} of {grid.Length}");
        }

        // WriteGrid(grid);

        var maxArea = 0L;
        for (var thisTileI = 0; thisTileI < redTiles.Length; thisTileI++)
        {
            for (var otherTileI = thisTileI + 1; otherTileI < redTiles.Length; otherTileI++)
            {
                Console.WriteLine($"Checking tiles {thisTileI + 1} and {otherTileI + 1}");

                var width = Math.Abs(redTiles[thisTileI].Col - redTiles[otherTileI].Col) + 1;
                var height = Math.Abs(redTiles[thisTileI].Row - redTiles[otherTileI].Row) + 1;

                var area = width * height;

                if (maxArea >= area)
                {
                    continue;
                }

                var top = Math.Min(redTiles[thisTileI].Row, redTiles[otherTileI].Row);
                var bottom = Math.Max(redTiles[thisTileI].Row, redTiles[otherTileI].Row);
                var left = Math.Min(redTiles[thisTileI].Col, redTiles[otherTileI].Col);
                var right = Math.Max(redTiles[thisTileI].Col, redTiles[otherTileI].Col);

                var topRow = grid[top];
                var bottomRow = grid[bottom];

                var isInside = true;
                // check top and bottom
                for (var c = left; c <= right; c++)
                {
                    isInside &= topRow[c] > 0;
                    isInside &= bottomRow[c] > 0;

                    if (!isInside)
                    {
                        break;
                    }
                }

                if (!isInside)
                {
                    continue;
                }

                // check left and right
                for (var r = top; r <= bottom; r++)
                {
                    isInside &= grid[r][left] > 0;
                    isInside &= grid[r][right] > 0;

                    if (!isInside)
                    {
                        break;
                    }
                }

                if (!isInside)
                {
                    continue;
                }

                Console.WriteLine($"Found new max with tiles {thisTileI + 1} and {otherTileI + 1}");

                maxArea = area;
            }
        }

        return maxArea.ToString();
    }

    private enum TileColor
    {
        None = 0,
        Red = 1,
        Green = 2,
    }

    private static void WriteGrid(int[][] grid)
    {
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[0].Length; col++)
            {
                var tileColor = grid[row][col];
                Console.ForegroundColor = tileColor switch
                {
                    0 => ConsoleColor.DarkGray,
                    1 => ConsoleColor.Red,
                    2 => ConsoleColor.DarkGreen,
                    _ => throw new ArgumentOutOfRangeException(),
                };

                Console.Write("■");
            }

            Console.WriteLine();
        }
    }

    private static void DrawGrid(int[][] grid)
    {
        var bitmap = new SKBitmap(width: grid[0].Length, height: grid.Length);
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[0].Length; col++)
            {
                var tileColor = grid[row][col];
                var skColor = tileColor switch
                {
                    0 => SKColor.FromHsl(0, 0, 20),
                    1 => SKColor.FromHsl(0, 80, 47),
                    2 => SKColor.FromHsl(180, 80, 47),
                    _ => throw new ArgumentOutOfRangeException(),
                };

                bitmap.SetPixel(x: col, y: row, color: skColor);
            }
        }

        using var stream = new FileStream("./output.bmp", FileMode.Create, FileAccess.Write);
        using var image = SKImage.FromBitmap(bitmap);
        using var encodedImage = image.Encode();
        encodedImage.SaveTo(stream);
    }
}
