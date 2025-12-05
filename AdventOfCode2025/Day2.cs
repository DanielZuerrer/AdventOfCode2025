namespace AdventOfCode2025;

public static class Day2
{
    public static string A()
    {
        var input = File.ReadAllLines("Inputs/day2.txt");

        var ranges = input[0].Split(',');

        long count = 0;

        foreach (var range in ranges)
        {
            var startAndEnd = range.Split('-');
            var start = long.Parse(startAndEnd[0]);
            var end = long.Parse(startAndEnd[1]);

            for (var i = start; i <= end; i++)
            {
                var iString = i.ToString();

                var firstHalf = iString[..(iString.Length / 2)];
                var secondHalf = iString[(iString.Length / 2)..];

                if (firstHalf == secondHalf)
                {
                    count += i;
                }
            }
        }

        return count.ToString();
    }

    public static string B()
    {
        var input = File.ReadAllLines("Inputs/day2.txt");

        var ranges = input[0].Split(',');

        long count = 0;

        foreach (var range in ranges)
        {
            var startAndEnd = range.Split('-');
            var start = long.Parse(startAndEnd[0]);
            var end = long.Parse(startAndEnd[1]);

            for (var i = start; i <= end; i++)
            {
                var iString = i.ToString();

                for (var j = 1; j <= iString.Length / 2; j++)
                {
                    var chunks = iString.Chunk(j).Select(c => new string(c));
                    if (chunks.Distinct().Count() == 1)
                    {
                        count += i;
                        break;
                    }
                }
            }
        }

        return count.ToString();
    }
}
