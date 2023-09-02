using System.Globalization;

namespace AircraftMinimization;

public sealed partial class FlightIdentifier
{
    public static FlightIdentifier Parse(string? str)
    {
        return Parse(str, CultureInfo.CurrentCulture);
    }

    public static FlightIdentifier Parse(string? str, IFormatProvider? provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(str);

        var tokens = str.Split(DateSeparator);
        if (tokens.Length != 2)
        {
            throw new FormatException("Invalid format.");
        }

        var flightDesignatorToken = tokens[0][..FlightDesignator.Length];
        var flightDesignator = FlightDesignator.Parse(flightDesignatorToken, provider);

        var operationalSuffixToken = tokens[0][FlightDesignator.Length..];
        var operationalSuffix = !string.IsNullOrEmpty(operationalSuffixToken)
            ? AircraftMinimization.OperationalSuffix.Parse(operationalSuffixToken, provider)
            : (OperationalSuffix?) null;

        var dateToken = tokens[1];
        var date = DateOnly.ParseExact(dateToken, DateFormat, provider);

        return new FlightIdentifier(flightDesignator, operationalSuffix, date);        
    }

    public static bool TryParse(string? str, out FlightIdentifier result)
    {
        return TryParse(str, CultureInfo.CurrentCulture, out result);
    }

    public static bool TryParse(string? str, IFormatProvider? provider, out FlightIdentifier result)
    {
        ArgumentException.ThrowIfNullOrEmpty(str);

        var tokens = str.Split(DateSeparator);
        if (tokens.Length != 2)
        {
            result = default!;
            return false;
        }

        var flightDesignatorToken = tokens[0][..FlightDesignator.Length];
        if (!FlightDesignator.TryParse(flightDesignatorToken, provider, out var flightDesignator))
        {
            result = default!;
            return false;
        }

        var operationalSuffixToken = tokens[0][FlightDesignator.Length..];
        OperationalSuffix? operationalSuffix = null;
        if (!string.IsNullOrEmpty(operationalSuffixToken))
        {
            if (!AircraftMinimization.OperationalSuffix.TryParse(operationalSuffixToken, provider, out var suffix))
            {
                result = default!;
                return false;
            }

            operationalSuffix = suffix;
        }

        var dateToken = tokens[1];
        if (!DateOnly.TryParseExact(dateToken, new[] { DateFormat }, provider, DateTimeStyles.None, out var date))
        {
            result = default!;
            return false;
        }

        result = new FlightIdentifier(flightDesignator, operationalSuffix, date);
        return true;
    }
    
    public static bool operator ==(FlightIdentifier? left, FlightIdentifier? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(FlightIdentifier? left, FlightIdentifier? right)
    {
        return !Equals(left, right);
    }

    public static bool operator <(FlightIdentifier? left, FlightIdentifier? right)
    {
        return Comparer<FlightIdentifier>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(FlightIdentifier? left, FlightIdentifier? right)
    {
        return Comparer<FlightIdentifier>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(FlightIdentifier? left, FlightIdentifier? right)
    {
        return Comparer<FlightIdentifier>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(FlightIdentifier? left, FlightIdentifier? right)
    {
        return Comparer<FlightIdentifier>.Default.Compare(left, right) >= 0;
    }
}