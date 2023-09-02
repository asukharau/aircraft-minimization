using QuikGraph;

namespace AircraftMinimization;

public sealed partial class FlightSequenceOptimizer
{
    private sealed partial class FlightGraph
    {
        public static FlightGraph Create(FlightIndex index)
        {
            var vertexCapacity = index.FlightCount;

            var graph = new BidirectionalGraph<Flight, Edge<Flight>>(true, vertexCapacity);

            foreach (var currentFlight in index.GetAllFlights())
            {
                graph.AddVertex(currentFlight);

                foreach (var nextFlightLeg in index.GetNextFlights(currentFlight))
                {
                    graph.AddVerticesAndEdge(new Edge<Flight>(currentFlight, nextFlightLeg));
                }
            }

            return new FlightGraph(graph);
        }
    }
}
