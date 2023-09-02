using System.Collections.Concurrent;

namespace AircraftMinimization;

public sealed partial class FlightSequenceOptimizer
{
    public static IEnumerable<FlightSequence> Optimize(IEnumerable<Flight> flights)
    {
        var index = FlightIndex.Create(flights);

        var graph = FlightGraph.Create(index);

        var sequences = new ConcurrentBag<FlightSequence>();
        Parallel.ForEach(graph.Split(), subgraph =>
        {
            foreach (var sequence in subgraph.GetPathCover())
            {
                sequences.Add(sequence);
            }
        });

        return sequences;
    }
}
