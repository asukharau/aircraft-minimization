namespace AircraftMinimization;

public sealed class FlightConnection : IEquatable<FlightConnection>
{
    public FlightDesignator FlightDesignator { get; init; }

    public OperationalSuffix? OperationalSuffix { get; init; }

    public AircraftRotationLayover? AircraftRotationLayover { get; init; }

    public FlightConnection(FlightDesignator flightDesignator, OperationalSuffix? operationalSuffix = null, AircraftRotationLayover? aircraftRotationLayover = null)
    {
        FlightDesignator = flightDesignator;
        OperationalSuffix = operationalSuffix;
        AircraftRotationLayover = aircraftRotationLayover;
    }

    public FlightConnection()
        : this(default!, null, null)
    {
    }

    public void Deconstruct(out FlightDesignator flightDesignator, out OperationalSuffix? operationalSuffix, out AircraftRotationLayover? aircraftRotationLayover)
    {
        flightDesignator = FlightDesignator;
        operationalSuffix = OperationalSuffix;
        aircraftRotationLayover = AircraftRotationLayover;
    }

    public bool Equals(FlightConnection? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return FlightDesignator == other.FlightDesignator &&
               OperationalSuffix == other.OperationalSuffix &&
               AircraftRotationLayover == other.AircraftRotationLayover;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is FlightConnection other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FlightDesignator, OperationalSuffix, AircraftRotationLayover);
    }
}