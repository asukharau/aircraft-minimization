using System.Diagnostics;
using QuikGraph;
using QuikGraph.Algorithms;
using QuikGraph.Algorithms.ConnectedComponents;
using QuikGraph.Algorithms.Observers;
using QuikGraph.Algorithms.Search;

namespace AircraftMinimization;

public sealed partial class FlightSequenceOptimizer
{
    [DebuggerDisplay("FlightLegCount = {" + nameof(FlightCount) + "}, EdgeCount = {" + nameof(EdgeCount) + "}")]
    private sealed partial class FlightGraph
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly BidirectionalGraph<Flight, Edge<Flight>> _graph;

        public IEnumerable<Flight> Flights => _graph.Vertices;

        public int FlightCount => _graph.VertexCount;

        public bool IsFlightsEmpty => _graph.IsVerticesEmpty;

        public IEnumerable<Edge<Flight>> Edges => _graph.Edges;

        public int EdgeCount => _graph.EdgeCount;

        public bool IsEdgesEmpty => _graph.IsEdgesEmpty;

        public bool IsPath => _graph.VertexCount == 1 || _graph.Vertices.Count(v => _graph.InDegree(v) == 1 && _graph.OutDegree(v) == 1) == _graph.VertexCount - 2; 

        private FlightGraph(BidirectionalGraph<Flight, Edge<Flight>> graph)
        {
            _graph = graph;
        }

        public IEnumerable<Flight> GetPreviousFlights(Flight flight)
        {
            return _graph.InEdges(flight).Select(edge => edge.Source);
        }

        public IEnumerable<Flight> GetNextFlights(Flight flight)
        {
            return _graph.OutEdges(flight).Select(edge => edge.Target);
        }

        public IEnumerable<FlightGraph> Split()
        {
            var algorithm = new WeaklyConnectedComponentsAlgorithm<Flight, Edge<Flight>>(_graph);
            algorithm.Compute();

            foreach (var subgraph in algorithm.Graphs)
            {
                yield return new FlightGraph(subgraph);
            }
        }
        
        public IEnumerable<FlightSequence> GetPathCover()
        {
            if (IsFlightsEmpty)
            {
                yield break;
            }

            if (IsPath)
            {
                var root = _graph.Roots().First();

                if (IsEdgesEmpty)
                {
                    yield return new FlightSequence(new [] {root});
                }
                else
                {
                    var dfs = new DepthFirstSearchAlgorithm<Flight, Edge<Flight>>(_graph);

                    var observer = new VertexRecorderObserver<Flight>();
                    using (observer.Attach(dfs))
                    {
                        dfs.Compute(root);

                        yield return new FlightSequence(observer.Vertices);
                    }
                }
            }
            else
            {
                var bipartiteGraph = FlightBipartiteGraph.Create(this);

                var maximumMatching = bipartiteGraph.GetMaximumMatching();

                var graph = new BidirectionalGraph<Flight, Edge<Flight>>(true, FlightCount);
                foreach (var edge in maximumMatching)
                {
                    graph.AddVerticesAndEdge(edge);
                }

                var flightGraph = new FlightGraph(graph);
                foreach (var component in flightGraph.Split())
                {
                    foreach (var sequence in component.GetPathCover())
                    {
                        yield return sequence;
                    }
                }
            }
        }
    }
}
