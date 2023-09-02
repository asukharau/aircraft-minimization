using System.Diagnostics;
using System.Text.Json.Serialization;

namespace AircraftMinimization;

[JsonConverter(typeof(JsonAircraftRotationLayoverConverter))]
public readonly partial struct AircraftRotationLayover :
    IComparable,
    IComparable<AircraftRotationLayover>,
    IEquatable<AircraftRotationLayover>,
    IParsable<AircraftRotationLayover>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static readonly TimeSpan OneDay = TimeSpan.FromDays(1) - TimeSpan.FromMinutes(1);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int _days;

    public TimeSpan MinimumLayoverDuration => TimeSpan.FromDays(_days);

    public TimeSpan MaximumLayoverDuration => MinimumLayoverDuration + OneDay;

    private AircraftRotationLayover(int days)
    {
        _days = days;
    }

    public void Deconstruct(out TimeSpan minimumLayoverDuration, out TimeSpan maximumLayoverDuration)
    {
        minimumLayoverDuration = MinimumLayoverDuration;
        maximumLayoverDuration = MaximumLayoverDuration;
    }

    public bool Equals(AircraftRotationLayover other)
    {
        return _days == other._days;
    }

    public int CompareTo(AircraftRotationLayover other)
    {
        return _days.CompareTo(other._days);
    }

    public int CompareTo(object? obj)
    {
        if (obj is null)
        {
            return 1;
        }

        return obj is AircraftRotationLayover other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(AircraftRotationLayover)}");
    }

    public override bool Equals(object? obj)
    {
        return obj is AircraftRotationLayover other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _days.GetHashCode();
    }

    public override string ToString()
    {
        return _days.ToString();
    }
}
