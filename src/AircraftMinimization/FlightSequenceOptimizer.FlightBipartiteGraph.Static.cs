using QuikGraph;

namespace AircraftMinimization;

public sealed partial class FlightSequenceOptimizer
{
    private sealed partial class FlightBipartiteGraph
    {
        public static FlightBipartiteGraph Create(FlightGraph flightGraph)
        {
            var flightCount = flightGraph.FlightCount;

            var vertexCapacity = flightCount * 2;

            var graph = new BidirectionalGraph<FlightBipartiteVertex, Edge<FlightBipartiteVertex>>(true, vertexCapacity);

            var blueVertices = new List<FlightBipartiteVertex>(flightCount);
            var redVertices = new List<FlightBipartiteVertex>(flightCount);

            foreach (var flight in flightGraph.Flights)
            {
                var blueVertex = new FlightBipartiteDataVertex(flight, BipartiteFlightVertexColor.Blue);
                if (graph.AddVertex(blueVertex))
                {
                    blueVertices.Add(blueVertex);
                }

                var targetFlights = flightGraph.GetNextFlights(flight);
                foreach (var targetFlight in targetFlights)
                {
                    var targetVertex = new FlightBipartiteDataVertex(targetFlight, BipartiteFlightVertexColor.Red);

                    if (graph.AddVertex(targetVertex))
                    {
                        redVertices.Add(targetVertex);
                    }

                    graph.AddEdge(new Edge<FlightBipartiteVertex>(blueVertex, targetVertex));
                }

                var redVertex = new FlightBipartiteDataVertex(flight, BipartiteFlightVertexColor.Red);
                if (graph.AddVertex(redVertex))
                {
                    redVertices.Add(redVertex);
                }

                var sourceFlights = flightGraph.GetPreviousFlights(flight);
                foreach (var sourceFlight in sourceFlights)
                {
                    var sourceVertex = new FlightBipartiteDataVertex(sourceFlight, BipartiteFlightVertexColor.Blue);

                    if (graph.AddVertex(sourceVertex))
                    {
                        blueVertices.Add(sourceVertex);
                    }

                    graph.AddEdge(new Edge<FlightBipartiteVertex>(sourceVertex, redVertex));
                }
            }

            var undirectedGraph = new UndirectedBidirectionalGraph<FlightBipartiteVertex, Edge<FlightBipartiteVertex>>(graph);

            return new FlightBipartiteGraph(undirectedGraph, blueVertices, redVertices);
        }
    }
}
