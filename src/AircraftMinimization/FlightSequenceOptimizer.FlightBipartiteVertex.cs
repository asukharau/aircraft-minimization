using System.Diagnostics;

namespace AircraftMinimization;

public sealed partial class FlightSequenceOptimizer
{
    private enum BipartiteFlightVertexColor : byte
    {
        Blue,
        Red
    }

    private abstract class FlightBipartiteVertex
    {
    }

    [DebuggerDisplay("SUPER")]
    private sealed class FlightBipartiteSuperVertex : FlightBipartiteVertex, IEquatable<FlightBipartiteSuperVertex>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Guid _id;

        public FlightBipartiteSuperVertex(Guid id)
        {
            _id = id;
        }

        public FlightBipartiteSuperVertex()
            : this(Guid.NewGuid())
        {
        }

        public bool Equals(FlightBipartiteSuperVertex? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return _id == other._id;
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is FlightBipartiteSuperVertex other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }

    [DebuggerDisplay("{" + nameof(Flight) + "." + nameof(AircraftMinimization.Flight.Id) + "} ({" + nameof(Color) + "})")]
    private sealed class FlightBipartiteDataVertex : FlightBipartiteVertex, IEquatable<FlightBipartiteDataVertex>
    {
        public Flight Flight { get; }

        public BipartiteFlightVertexColor? Color { get; }

        public FlightBipartiteDataVertex(Flight flight, BipartiteFlightVertexColor? color = null)
        {
            Flight = flight;
            Color = color;
        }

        public bool Equals(FlightBipartiteDataVertex? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Flight == other.Flight && Color == other.Color;
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is FlightBipartiteDataVertex other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Flight, Color);
        }
    }
}
