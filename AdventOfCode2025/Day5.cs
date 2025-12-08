namespace AdventOfCode2025;

public static class Day5
{
    public static string A()
    {
        var input = File.ReadAllLines("Inputs/day5.txt");

        var divider = input.IndexOf(string.Empty);

        var ranges = input[..divider]
            .Select(r => r.Split('-').Select(long.Parse).ToArray())
            .ToArray();

        var ids = input[(divider + 1)..].Select(long.Parse).ToArray();

        var freshCount = ids.Count(id => ranges.Any(range => id >= range[0] && id <= range[1]));

        return freshCount.ToString();
    }

    public static string B()
    {
        var input = File.ReadAllLines("Inputs/day5.txt");

        var divider = input.IndexOf(string.Empty);

        var ranges = input[..divider]
            .Select(r => r.Split('-').Select(long.Parse).ToArray())
            .ToArray();

        var nonOverlappingRanges = ranges
            .OrderBy(range => range[0])
            .Aggregate(
                new List<long[]>(),
                (nonOverlappingRanges, currentRange) =>
                {
                    // first range
                    if (nonOverlappingRanges.Count == 0)
                    {
                        nonOverlappingRanges.Add(currentRange);
                        return nonOverlappingRanges;
                    }

                    // range does not overlap
                    if (nonOverlappingRanges.Last()[1] < currentRange[0])
                    {
                        nonOverlappingRanges.Add(currentRange);
                        return nonOverlappingRanges;
                    }

                    // range does overlap
                    if (nonOverlappingRanges.Last()[1] < currentRange[1])
                    {
                        nonOverlappingRanges.Last()[1] = currentRange[1];
                        return nonOverlappingRanges;
                    }

                    return nonOverlappingRanges;
                }
            );

        var freshIdCount = nonOverlappingRanges.Sum(range => range[1] - range[0] + 1);

        return freshIdCount.ToString();
    }
}
