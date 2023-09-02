using System.Diagnostics;
using System.Globalization;

namespace AircraftMinimization;

public readonly partial struct AircraftRotationLayover
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static readonly AircraftRotationLayover ZeroInstance = new(0);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const int MinDays = 0;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const int MaxDays = 9;

    public static ref readonly AircraftRotationLayover Zero => ref ZeroInstance;

    public static IEnumerable<AircraftRotationLayover> GetAll()
    {
        return Enumerable.Range(MinDays, MaxDays).Select(days => new AircraftRotationLayover(days));
    }
    
    public static AircraftRotationLayover FromDays(int days)
    {
        if (days is < MinDays or > MaxDays)
        {
            throw new ArgumentOutOfRangeException(nameof(days));
        }

        return new AircraftRotationLayover(days);
    }

    public static AircraftRotationLayover Parse(string? str)
    {
        return Parse(str, CultureInfo.CurrentCulture);
    }

    public static AircraftRotationLayover Parse(string? str, IFormatProvider? provider)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(str);

        var days = int.Parse(str, provider);
        return FromDays(days);
    }

    public static bool TryParse(string? str, out AircraftRotationLayover result)
    {
        return TryParse(str, CultureInfo.CurrentCulture, out result);
    }

    public static bool TryParse(string? str, IFormatProvider? provider, out AircraftRotationLayover result)
    {
        if (!int.TryParse(str, provider, out var days))
        {
            result = default;
            return false;
        }

        result = FromDays(days);
        return true;
    }
    

    public static bool operator ==(AircraftRotationLayover left, AircraftRotationLayover right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(AircraftRotationLayover left, AircraftRotationLayover right)
    {
        return !left.Equals(right);
    }

    public static bool operator <(AircraftRotationLayover left, AircraftRotationLayover right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(AircraftRotationLayover left, AircraftRotationLayover right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(AircraftRotationLayover left, AircraftRotationLayover right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(AircraftRotationLayover left, AircraftRotationLayover right)
    {
        return left.CompareTo(right) >= 0;
    }
}
