namespace AdventOfCode2025;

public static class Day1
{
    private const int StartingNumber = 50;

    public static string A()
    {
        var input = File.ReadAllLines("Inputs/day1.txt");

        var zeroCounter = 0;

        var currentNumber = StartingNumber;

        foreach (var line in input)
        {
            var direction = line[0];
            var count = int.Parse(line[1..]);

            if (direction == 'R')
            {
                currentNumber += count;
            }
            else if (direction == 'L')
            {
                currentNumber -= count;
            }

            currentNumber %= 100;

            if (currentNumber == 0)
            {
                zeroCounter++;
            }
        }

        return zeroCounter.ToString();
    }

    public static string B()
    {
        var input = File.ReadAllLines("Inputs/day1.txt");

        var zeroCounter = 0;

        var currentNumber = StartingNumber;

        foreach (var line in input)
        {
            var direction = line[0];
            var count = int.Parse(line[1..]);

            for (var i = 0; i < count; i++)
            {
                if (direction == 'R')
                {
                    currentNumber++;
                }
                else if (direction == 'L')
                {
                    currentNumber--;
                }

                currentNumber %= 100;

                if (currentNumber == 0)
                {
                    zeroCounter++;
                }
            }
        }

        return zeroCounter.ToString();
    }
}
