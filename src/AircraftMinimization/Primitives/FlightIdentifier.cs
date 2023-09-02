using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;

namespace AircraftMinimization;

[JsonConverter(typeof(JsonFlightIdentifierConverter))]
public sealed partial class FlightIdentifier :
    IComparable,
    IComparable<FlightIdentifier>,
    IEquatable<FlightIdentifier>,
    IParsable<FlightIdentifier>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const char DateSeparator = '/';

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const string DateFormat = "ddMMMyy";

    public FlightDesignator FlightDesignator { get; }

    public OperationalSuffix? OperationalSuffix { get; }

    public DateOnly Date { get; }

    public FlightIdentifier(FlightDesignator flightDesignator, OperationalSuffix? operationalSuffix, DateOnly date)
    {
        FlightDesignator = flightDesignator;
        OperationalSuffix = operationalSuffix;
        Date = date;
    }
    
    public FlightIdentifier(FlightDesignator flightDesignator, DateOnly date)
        : this(flightDesignator, null, date)
    {
    }

    public bool Equals(FlightIdentifier? other)
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
               Date == other.Date;
    }

    public int CompareTo(FlightIdentifier? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (other is null)
        {
            return 1;
        }

        var comparison = FlightDesignator.CompareTo(other.FlightDesignator);
        if (comparison != 0)
        {
            return comparison;
        }

        comparison = Nullable.Compare(OperationalSuffix, other.OperationalSuffix);
        if (comparison != 0)
        {
            return comparison;
        }

        comparison = Date.CompareTo(other.Date);
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

        return obj is FlightIdentifier other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(FlightIdentifier)}");
    }

    public override string ToString()
    {
        const int capacity = 15;

        var stringBuilder = new StringBuilder(capacity);

        stringBuilder.Append(FlightDesignator);

        if (OperationalSuffix is not null)
        {
            stringBuilder.Append(OperationalSuffix);
        }

        stringBuilder.Append(DateSeparator);
        stringBuilder.AppendFormat(
            Date.ToString(DateFormat).ToUpperInvariant()
        );

        var str = stringBuilder.ToString();
        return string.Intern(str);
    }
    
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is FlightIdentifier other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FlightDesignator, OperationalSuffix, Date);
    }
}