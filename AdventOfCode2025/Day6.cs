using System.ComponentModel.Design;

namespace AdventOfCode2025;

public static class Day6
{
    private record Problem(string Operator)
    {
        public List<long> Numbers { get; init; } = [];
    }

    public static string A()
    {
        var input = File.ReadAllLines("Inputs/day6.txt");

        var splitLines = input
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .ToArray();

        var numberOfProblems = splitLines[0].Length;

        var problems = new Problem[numberOfProblems];

        for (var problemIndex = 0; problemIndex < numberOfProblems; problemIndex++)
        {
            problems[problemIndex] = new Problem(splitLines[^1][problemIndex]);

            for (var row = 0; row < input.Length - 1; row++)
            {
                problems[problemIndex].Numbers.Add(long.Parse(splitLines[row][problemIndex]));
            }
        }

        var result = problems.Select(problem =>
            problem.Numbers.Aggregate(
                problem.Operator == "*" ? 1L : 0,
                (solution, number) =>
                {
                    if (problem.Operator == "*")
                    {
                        return solution * number;
                    }

                    return solution + number;
                }
            )
        );

        return result.Sum().ToString();
    }

    public static string B()
    {
        var input = File.ReadAllLines("Inputs/day6.txt");

        var operators = input[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var numbersInput = input[..^1];

        var transposed = new string[numbersInput[0].Length];

        for (var col = numbersInput[0].Length - 1; col >= 0; col--)
        {
            transposed[^(col + 1)] = string.Empty;

            for (var row = 0; row < numbersInput.Length; row++)
            {
                transposed[^(col + 1)] += numbersInput[row][col];
            }
        }

        var problems = operators.Reverse().Select(op => new Problem(op)).ToArray();

        var problemIndex = 0;

        foreach (var number in transposed)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                problemIndex++;
                continue;
            }

            problems[problemIndex].Numbers.Add(long.Parse(number));
        }

        var result = problems.Select(problem =>
            problem.Numbers.Aggregate(
                problem.Operator == "*" ? 1L : 0,
                (solution, number) =>
                {
                    if (problem.Operator == "*")
                    {
                        return solution * number;
                    }

                    return solution + number;
                }
            )
        );

        return result.Sum().ToString();
    }
}
