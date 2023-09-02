using QuikGraph;
using QuikGraph.Algorithms;
using QuikGraph.Algorithms.Services;

namespace AircraftMinimization;

internal sealed class HopcroftKarpMaximumMatchingAlgorithm<TVertex, TEdge> : AlgorithmBase<IUndirectedGraph<TVertex, TEdge>>
    where TVertex : notnull
    where TEdge : IEdge<TVertex>
{
    private readonly List<TEdge> _matchedEdges;

    public IEnumerable<TVertex> LeftVertices { get; }

    public IEnumerable<TVertex> RightVertices { get; }
    
    public VertexFactory<TVertex> VertexFactory { get; }
    
    public TEdge[] MatchedEdges => _matchedEdges.ToArray();
    
    public HopcroftKarpMaximumMatchingAlgorithm(
        IAlgorithmComponent? host,
        IUndirectedGraph<TVertex, TEdge> graph,
        IEnumerable<TVertex> leftVertices,
        IEnumerable<TVertex> rightVertices,
        VertexFactory<TVertex> vertexFactory)
        : base(host, graph)
    {
        LeftVertices = leftVertices;
        RightVertices = rightVertices;
        VertexFactory = vertexFactory;

        _matchedEdges = new List<TEdge>();
    }

    public HopcroftKarpMaximumMatchingAlgorithm(
        IUndirectedGraph<TVertex, TEdge> graph,
        IEnumerable<TVertex> leftVertices,
        IEnumerable<TVertex> rightVertices,
        VertexFactory<TVertex> vertexFactory)
        : this(
            null,
            graph,
            leftVertices,
            rightVertices,
            vertexFactory
        )
    {
    }

    protected override void Initialize()
    {
        base.Initialize();

        _matchedEdges.Clear();
    }

    protected override void InternalCompute()
    {
        var dummyVertex = VertexFactory();
        
        var leftVertexMatching = LeftVertices.ToDictionary(leftVertex => leftVertex, _ => dummyVertex);
        var rightVertexMatching = RightVertices.ToDictionary(rightVertex => rightVertex, _ => dummyVertex);
    
        var distances = new Dictionary<TVertex, int>();
    
        while (BreadthFirstSearch(leftVertexMatching, rightVertexMatching, distances, dummyVertex))
        {
            foreach (var leftVertex in LeftVertices)
            {
                var rightVertex = leftVertexMatching[leftVertex];
    
                if (EqualityComparer<TVertex>.Default.Equals(rightVertex, dummyVertex))
                {
                    DepthFirstSearch(leftVertex, leftVertexMatching, rightVertexMatching, distances, dummyVertex);
                }
            }
        }
    
        foreach (var (leftVertex, rightVertex) in leftVertexMatching)
        {
            if (VisitedGraph.TryGetEdge(leftVertex, rightVertex, out var edge))
            {
                _matchedEdges.Add(edge);
            }
        }
    }
    
    private bool BreadthFirstSearch(Dictionary<TVertex, TVertex> leftVertexMatching, Dictionary<TVertex, TVertex> rightVertexMatching, Dictionary<TVertex, int> distances, TVertex dummyVertex)
    {
        var queue = new Queue<TVertex>();
    
        foreach (var leftVertex in LeftVertices)
        {
            var rightVertex = leftVertexMatching[leftVertex];
            if (EqualityComparer<TVertex>.Default.Equals(rightVertex, dummyVertex))
            {
                distances[leftVertex] = 0;
                queue.Enqueue(leftVertex);
            }
            else
            {
                distances[leftVertex] = int.MaxValue;
            }
        }
    
        distances[dummyVertex] = int.MaxValue;
    
        while (queue.TryDequeue(out var leftVertex))
        {
            if (distances[leftVertex] < distances[dummyVertex])
            {
                foreach (var edge in VisitedGraph.AdjacentEdges(leftVertex))
                {
                    var rightVertex = edge.Target;
    
                    var nextLeftVertex = rightVertexMatching[rightVertex];
    
                    if (distances[nextLeftVertex] == int.MaxValue)
                    {
                        distances[nextLeftVertex] = distances[leftVertex] + 1;
                        queue.Enqueue(nextLeftVertex);
                    }
                }
            }
        }
    
        return distances[dummyVertex] != int.MaxValue;
    }   
    
    private bool DepthFirstSearch(TVertex leftVertex, Dictionary<TVertex, TVertex> leftVertexMatching, Dictionary<TVertex, TVertex> rightVertexMatching, Dictionary<TVertex, int> distances, TVertex dummyVertex)
    {
        if (!EqualityComparer<TVertex>.Default.Equals(leftVertex, dummyVertex))
        {
            foreach (var edge in VisitedGraph.AdjacentEdges(leftVertex))
            {
                var rightVertex = edge.Target;
    
                var nextLeftVertex = rightVertexMatching[rightVertex];
    
                if (distances[nextLeftVertex] == distances[leftVertex] + 1)
                {
                    if (DepthFirstSearch(nextLeftVertex, leftVertexMatching, rightVertexMatching, distances, dummyVertex))
                    {
                        leftVertexMatching[leftVertex] = rightVertex;
                        rightVertexMatching[rightVertex] = leftVertex;
                        return true;
                    }
                }
            }
    
            distances[leftVertex] = int.MaxValue;
            return false;
        }
    
        return true;
    }
}