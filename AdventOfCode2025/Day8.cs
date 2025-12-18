using System.Globalization;
using System.Numerics;

namespace AdventOfCode2025;

public static class Day8
{
    public static string A()
    {
        var input = File.ReadAllLines("Inputs/day8.txt");

        var junctionBoxes = input
            .Select(
                (line, index) =>
                {
                    var coords = line.Split(',');
                    var position = new Vector3(
                        float.Parse(coords[0]),
                        float.Parse(coords[1]),
                        float.Parse(coords[2])
                    );

                    return new JunctionBox(position, index);
                }
            )
            .ToArray();

        List<BoxDistance> boxDistances = [];

        for (var thisBoxI = 0; thisBoxI < junctionBoxes.Length; thisBoxI++)
        {
            var thisBox = junctionBoxes[thisBoxI];

            for (var otherBoxI = thisBoxI + 1; otherBoxI < junctionBoxes.Length; otherBoxI++)
            {
                var otherBox = junctionBoxes[otherBoxI];
                if (thisBox.Id == otherBox.Id)
                {
                    continue;
                }

                boxDistances.Add(
                    new BoxDistance(
                        thisBox,
                        otherBox,
                        Vector3.Distance(thisBox.Position, otherBox.Position)
                    )
                );
            }
        }

        var circuitNodes = junctionBoxes
            .Select(b => new CircuitNode(b.Id, connections: []))
            .ToArray();

        foreach (var boxDistance in boxDistances.OrderBy(bd => bd.Distance).Take(1000))
        {
            var originBox = boxDistance.Origin;
            var destinationBox = boxDistance.Destination;

            var originNode = circuitNodes[originBox.Id];
            var destinationNode = circuitNodes[destinationBox.Id];

            originNode.Connections.Add(destinationNode);
            destinationNode.Connections.Add(originNode);
        }

        List<List<int>> circuits = [];
        HashSet<int> explored = [];
        foreach (var circuitNode in circuitNodes)
        {
            if (explored.Contains(circuitNode.JunctionBoxId))
            {
                continue;
            }

            circuits.Add([]);

            DepthFirstSearch(circuitNode, circuits.Last(), explored);
        }

        return circuits
            .Select(c => c.Count)
            .OrderDescending()
            .Take(3)
            .Aggregate(1, (acc, curr) => acc * curr)
            .ToString();
    }

    public static string B()
    {
        var input = File.ReadAllLines("Inputs/day8.txt");

        var junctionBoxes = input
            .Select(
                (line, index) =>
                {
                    var coords = line.Split(',');
                    var position = new Vector3(
                        float.Parse(coords[0]),
                        float.Parse(coords[1]),
                        float.Parse(coords[2])
                    );

                    return new JunctionBox(position, index);
                }
            )
            .ToArray();

        List<BoxDistance> boxDistances = [];

        for (var thisBoxI = 0; thisBoxI < junctionBoxes.Length; thisBoxI++)
        {
            var thisBox = junctionBoxes[thisBoxI];

            for (var otherBoxI = thisBoxI + 1; otherBoxI < junctionBoxes.Length; otherBoxI++)
            {
                var otherBox = junctionBoxes[otherBoxI];
                if (thisBox.Id == otherBox.Id)
                {
                    continue;
                }

                boxDistances.Add(
                    new BoxDistance(
                        thisBox,
                        otherBox,
                        Vector3.Distance(thisBox.Position, otherBox.Position)
                    )
                );
            }
        }

        var circuitNodes = junctionBoxes
            .Select(b => new CircuitNode(b.Id, connections: []))
            .ToArray();

        foreach (var boxDistance in boxDistances.OrderBy(bd => bd.Distance))
        {
            var originBox = boxDistance.Origin;
            var destinationBox = boxDistance.Destination;

            var originNode = circuitNodes[originBox.Id];
            var destinationNode = circuitNodes[destinationBox.Id];

            originNode.Connections.Add(destinationNode);
            destinationNode.Connections.Add(originNode);

            List<int> testCircuit = [];
            HashSet<int> explored = [];
            DepthFirstSearch(circuitNodes[0], testCircuit, explored);

            Console.WriteLine($"Count: {testCircuit.Count}");

            if (testCircuit.Count >= circuitNodes.Length)
            {
                return ((long)originBox.Position.X * (long)destinationBox.Position.X).ToString(
                    CultureInfo.InvariantCulture
                );
            }
        }

        return "Fail";
    }

    private record JunctionBox(Vector3 Position, int Id);

    private record BoxDistance(JunctionBox Origin, JunctionBox Destination, float Distance);

    private class CircuitNode(int junctionBoxId, List<CircuitNode> connections)
    {
        public int JunctionBoxId { get; } = junctionBoxId;
        public List<CircuitNode> Connections { get; } = connections;
    };

    private static void DepthFirstSearch(CircuitNode node, List<int> circuit, HashSet<int> explored)
    {
        circuit.Add(node.JunctionBoxId);
        explored.Add(node.JunctionBoxId);

        foreach (var connection in node.Connections)
        {
            if (explored.Contains(connection.JunctionBoxId))
            {
                continue;
            }

            DepthFirstSearch(connection, circuit, explored);
        }
    }
}
