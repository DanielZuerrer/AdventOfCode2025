using System.Globalization;

namespace AdventOfCode2025;

public static class Day3
{
    public static string A()
    {
        var input = File.ReadAllLines("Inputs/day3.txt");

        var totalJoltage = 0L;

        foreach (var line in input)
        {
            var joltage = FindMax(line, 2);
            totalJoltage += joltage;
        }

        return totalJoltage.ToString();
    }

    private static long FindMax(string digitString, int numberOfDigits)
    {
        var digits = digitString.Chunk(1).Select(c => int.Parse(c.AsSpan())).ToArray();

        var maxDigit = (long)digits[..^(numberOfDigits - 1)].Max();

        if (numberOfDigits == 1)
        {
            return maxDigit;
        }

        var remainingDigitString = digitString.Substring(
            digitString.IndexOf(
                maxDigit.ToString(CultureInfo.InvariantCulture),
                StringComparison.Ordinal
            ) + 1
        );

        return maxDigit * (long)Math.Pow(10, numberOfDigits - 1)
            + FindMax(remainingDigitString, numberOfDigits - 1);
    }

    public static string B()
    {
        var input = File.ReadAllLines("Inputs/day3.txt");

        var totalJoltage = 0L;

        foreach (var line in input)
        {
            var joltage = FindMax(line, 12);
            totalJoltage += joltage;
        }

        return totalJoltage.ToString();
    }
}
