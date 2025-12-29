// See https://aka.ms/new-console-template for more information

using AdventOfCode2025;

var result = args[0] switch
{
    "1A" => Day1.A(),
    "1B" => Day1.B(),
    "2A" => Day2.A(),
    "2B" => Day2.B(),
    "3A" => Day3.A(),
    "3B" => Day3.B(),
    "4A" => Day4.A(),
    "4B" => Day4.B(),
    "5A" => Day5.A(),
    "5B" => Day5.B(),
    "6A" => Day6.A(),
    "6B" => Day6.B(),
    "7A" => Day7.A(),
    "7B" => Day7.B(),
    "8A" => Day8.A(),
    "8B" => Day8.B(),
    "9A" => Day9.A(),
    "9B" => Day9.B(),
    "10A" => Day10.A(),
    "10B" => Day10.B(),
    "11A" => Day11.A(),
    "11B" => Day11.B(),
    "12A" => Day12.A(),
    "12B" => Day12.B(),
    _ => throw new ArgumentOutOfRangeException(),
};

Console.WriteLine($"Solution: {result}");
