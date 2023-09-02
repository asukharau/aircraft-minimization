namespace AircraftMinimization;

public sealed class FlightEvent
{
    public LocationIdentifier Location { get; init; }

    public DateTime Timestamp { get; init; }

    public FlightEvent(LocationIdentifier location, DateTime timestamp)
    {
        Location = location;
        Timestamp = timestamp;
    }

    public FlightEvent()
        : this(default!, DateTime.UnixEpoch)
    {
    }

    public void Deconstruct(out LocationIdentifier location, out DateTime timestamp)
    {
        location = Location;
        timestamp = Timestamp;
    }

    public bool Equals(FlightEvent? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Location == other.Location && Timestamp == other.Timestamp;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is FlightEvent other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Location, Timestamp);
    }

    public override string ToString()
    {
        return $"{Location} {Timestamp}";
    }
}
