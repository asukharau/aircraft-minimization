using System.Diagnostics;
using System.Text.Json.Serialization;

namespace AircraftMinimization;

[JsonConverter(typeof(JsonAirlineDesignatorConverter))]
public sealed partial class AirlineDesignator : 
    IComparable,
    IComparable<AirlineDesignator>,
    IEquatable<AirlineDesignator>,
    IParsable<AirlineDesignator>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string _code;

    private AirlineDesignator(string code)
    {
        _code = code;
    }

    public bool Equals(AirlineDesignator? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return StringComparer.Ordinal.Equals(_code, other?._code);
    }

    public int CompareTo(AirlineDesignator? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        if (other is null)
        {
            return 1;
        }

        return StringComparer.Ordinal.Compare(_code, other?._code);
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

        return obj is AirlineDesignator other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(AirlineDesignator)}");
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is AirlineDesignator other && Equals(other);
    }

    public override int GetHashCode()
    {
        return StringComparer.Ordinal.GetHashCode(_code);
    }

    public override string ToString()
    {
        return _code;
    }
}