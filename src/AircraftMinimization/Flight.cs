using System.Diagnostics;

namespace AircraftMinimization;

public sealed class Flight : IEquatable<Flight>
{
    public FlightIdentifier Id { get; init; }

    public FlightEvent Departure { get; init; }

    public FlightEvent Arrival { get; init; }

    public FlightConnection? PreviousFlightConnection { get; init; }

    public FlightConnection? NextFlightConnection { get; init; }

    public Flight(FlightIdentifier id, FlightEvent departure, FlightEvent arrival, FlightConnection? previousFlightConnection = null, FlightConnection? nextFlightConnection = null)
    {
        Id = id;
        Departure = departure;
        Arrival = arrival;
        PreviousFlightConnection = previousFlightConnection;
        NextFlightConnection = nextFlightConnection;
    }

    public Flight()
        : this(default!, default!, default!)
    {
    }

    public bool Equals(Flight? other)
    {
        if (other is null) 
        {
            return false;
        }

        if (ReferenceEquals(this, other)) 
        {
            return true;
        }

        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj)) 
        {
            return true;
        }

        return Equals((Flight) obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Id.ToString();
    }
}