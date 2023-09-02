using System.Diagnostics;
using QuikGraph;

namespace AircraftMinimization;

public sealed partial class FlightSequenceOptimizer
{
    private sealed partial class FlightBipartiteGraph
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IUndirectedGraph<FlightBipartiteVertex, Edge<FlightBipartiteVertex>> _graph;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList<FlightBipartiteVertex> _blueVertices;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList<FlightBipartiteVertex> _redVertices;

        private FlightBipartiteGraph(IUndirectedGraph<FlightBipartiteVertex, Edge<FlightBipartiteVertex>> graph, IList<FlightBipartiteVertex> blueVertices, IList<FlightBipartiteVertex> redVertices)
        {
            _graph = graph;
            _blueVertices = blueVertices;
            _redVertices = redVertices;
        }
        
        public IEnumerable<Edge<Flight>> GetMaximumMatching()
        {
            var algorithm =
                new HopcroftKarpMaximumMatchingAlgorithm<FlightBipartiteVertex, Edge<FlightBipartiteVertex>>(
                    _graph, _blueVertices, _redVertices, () => new FlightBipartiteSuperVertex()
                );

            algorithm.Compute();

            foreach (var edge in algorithm.MatchedEdges)
            {
                if (edge.Source is not FlightBipartiteDataVertex source)
                {
                    throw new InvalidOperationException();
                }

                if (edge.Target is not FlightBipartiteDataVertex target)
                {
                    throw new InvalidOperationException();
                }

                yield return new Edge<Flight>(source.Flight, target.Flight);
            }
        }
    }
}
