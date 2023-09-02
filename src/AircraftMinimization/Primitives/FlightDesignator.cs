using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;

namespace AircraftMinimization;

[JsonConverter(typeof(JsonFlightDesignatorConverter))]
public sealed partial class FlightDesignator :
    IComparable,
    IComparable<FlightDesignator>,
    IEquatable<FlightDesignator>,
    IParsable<FlightDesignator>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string FlightNumberFormat = "D4";
    
    public AirlineDesignator AirlineDesignator { get; }

    public int FlightNumber { get; }

    private FlightDesignator(AirlineDesignator airlineDesignator, int flightNumber)
    {
        AirlineDesignator = airlineDesignator;
        FlightNumber = flightNumber;
    }

    public bool Equals(FlightDesignator? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return AirlineDesignator == other.AirlineDesignator && FlightNumber == other.FlightNumber;
    }

    public int CompareTo(FlightDesignator? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (other is null)
        {
            return 1;
        }

        var comparison = AirlineDesignator.CompareTo(other.AirlineDesignator);
        if (comparison == 0)
        {
            comparison = FlightNumber.CompareTo(other.FlightNumber);
        }

        return comparison;
    }

    public int CompareTo(object? obj)
    {
        if (obj is null)
        {
            return 1;
        }

        if (ReferenceEquals(this, obj))
        {
            return 0;
        }

        return obj is FlightDesignator other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(FlightDesignator)}");
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is FlightDesignator other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(AirlineDesignator, FlightNumber);
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append(AirlineDesignator);
        stringBuilder.Append(
            FlightNumber.ToString(FlightNumberFormat)
        );

        var str = stringBuilder.ToString();
        return string.Intern(str);
    }
}
