using System.Collections;
using System.Numerics;

namespace AdventOfCode2025;

public static class Day7
{
    public static string A()
    {
        var input = File.ReadAllLines("Inputs/day7_example_simple.txt");

        var startIndex = input[0].IndexOf('S');

        List<int> beams = [startIndex];

        var splitCount = 0;

        foreach (var line in input[1..])
        {
            List<int> newBeams = [];

            foreach (var beam in beams)
            {
                if (line[beam] == '^')
                {
                    newBeams.Add(beam - 1);
                    newBeams.Add(beam + 1);
                    splitCount++;
                }
                else
                {
                    newBeams.Add(beam);
                }
            }

            beams = newBeams.Distinct().ToList();
        }

        return splitCount.ToString();
    }

    public static string B()
    {
        var input = File.ReadAllLines("Inputs/day7.txt");

        var startIndex = input[0].IndexOf('S');

        var splitters = input[1..]
            .Where((_, i) => i % 2 == 1)
            .Select(line => new BitArray(line.Select(c => c == '^').ToArray()))
            .ToArray();

        var beams = new long[splitters[0].Length];
        beams[startIndex] = 1;

        foreach (var splitterLine in splitters)
        {
            var beamsBits = new BitArray(beams.Select(beam => beam >= 1).ToArray());

            var hits = splitterLine.And(beamsBits);

            for (var i = 0; i < hits.Count; i++)
            {
                if (hits[i])
                {
                    var beamCount = beams[i];
                    beams[i] = 0;
                    beams[i - 1] += beamCount;
                    beams[i + 1] += beamCount;
                }
            }
        }

        return beams.Sum().ToString();
    }
}
