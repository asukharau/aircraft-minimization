using System.Diagnostics;
using System.Globalization;

namespace AircraftMinimization;

public sealed partial class FlightDesignator
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal const int Length = 6;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const int MinFlightNumber = 0001;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private const int MaxFlightNumber = 9999;

    public static FlightDesignator Create(AirlineDesignator? airlineDesignator, int flightNumber)
    {
        ArgumentNullException.ThrowIfNull(airlineDesignator);

        if (flightNumber is < MinFlightNumber or > MaxFlightNumber)
        {
            throw new ArgumentOutOfRangeException(nameof(flightNumber), flightNumber, $"Flight number must be between {MinFlightNumber} and {MaxFlightNumber}.");
        }

        return new FlightDesignator(airlineDesignator, flightNumber);
    }

    public static FlightDesignator Parse(string? str)
    {
        return Parse(str, CultureInfo.CurrentCulture);
    }

    public static FlightDesignator Parse(string? str, IFormatProvider? formatProvider)
    {
        ArgumentException.ThrowIfNullOrEmpty(str);

        var airlineDesignatorToken = str[..AirlineDesignator.Length];
        var airlineDesignator = AirlineDesignator.Parse(airlineDesignatorToken, formatProvider);

        var flightNumberToken = str[AirlineDesignator.Length..];
        var flightNumber = int.Parse(flightNumberToken, formatProvider);

        return Create(airlineDesignator, flightNumber);
    }

    public static bool TryParse(string? str, out FlightDesignator result)
    {
        return TryParse(str, CultureInfo.CurrentCulture, out result);
    }
    
    public static bool TryParse(string? str, IFormatProvider? formatProvider, out FlightDesignator result)
    {
        ArgumentException.ThrowIfNullOrEmpty(str);

        if (!AirlineDesignator.TryParse(str[..AirlineDesignator.Length], formatProvider, out var airlineDesignator) ||
            !int.TryParse(str[AirlineDesignator.Length..], formatProvider, out var flightNumber)
           )
        {
            result = default!;
            return false;
        }

        result = Create(airlineDesignator, flightNumber);
        return true;
    }

    public static bool operator ==(FlightDesignator left, FlightDesignator right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(FlightDesignator left, FlightDesignator right)
    {
        return !left.Equals(right);
    }

    public static bool operator <(FlightDesignator? left, FlightDesignator? right)
    {
        return Comparer<FlightDesignator>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(FlightDesignator? left, FlightDesignator? right)
    {
        return Comparer<FlightDesignator>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(FlightDesignator? left, FlightDesignator? right)
    {
        return Comparer<FlightDesignator>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(FlightDesignator? left, FlightDesignator? right)
    {
        return Comparer<FlightDesignator>.Default.Compare(left, right) >= 0;
    }
}