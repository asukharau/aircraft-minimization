using System.Diagnostics;
using System.Text.Json.Serialization;

namespace AircraftMinimization;

[JsonConverter(typeof(JsonOperationalSuffixConverter))]
public readonly partial struct OperationalSuffix :
    IComparable,
    IComparable<OperationalSuffix>,
    IEquatable<OperationalSuffix>,
    IParsable<OperationalSuffix>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly char _code;

    private OperationalSuffix(char code)
    {
        _code = code;
    }

    public bool Equals(OperationalSuffix other)
    {
        return _code == other._code;
    }

    public int CompareTo(OperationalSuffix other)
    {
        return _code.CompareTo(other._code);
    }

    public int CompareTo(object? obj)
    {
        if (obj is null)
        {
            return 1;
        }

        return obj is OperationalSuffix other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(OperationalSuffix)}");
    }

    public override bool Equals(object? obj)
    {
        return obj is OperationalSuffix other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _code.GetHashCode();
    }

    public override string ToString()
    {
        return _code.ToString();
    }
}