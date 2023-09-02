using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace AircraftMinimization;

public sealed partial class AirlineDesignator
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal const int Length = 2;

    public static AirlineDesignator Parse(string? str)
    {
        return Parse(str, CultureInfo.CurrentCulture);
    }

    public static AirlineDesignator Parse(string? str, IFormatProvider? provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(str);

        if (str.Length != Length)
        {
            throw new FormatException($"The string '{str}' does not have the correct length.");
        }

        if (str.All(IsAllowedSymbol) is false)
        {
            throw new FormatException($"The string '{str}' contains invalid characters.");
        }

        var code = str.ToUpperInvariant();
        return new AirlineDesignator(code);
    }

    public static bool TryParse(string? str, out AirlineDesignator result)
    {
        return TryParse(str, CultureInfo.CurrentCulture, out result);
    }

    public static bool TryParse(string? str, IFormatProvider? provider, out AirlineDesignator result)
    {
        if (string.IsNullOrEmpty(str)) 
        {
            result = default!;
            return false;
        }

        if (str.Length != Length || str.All(IsAllowedSymbol) is false)
        {
            result = default!;
            return false;
        }

        var code = str.ToUpperInvariant();

        result = new AirlineDesignator(code);
        return true;
    }
    public static bool operator ==(AirlineDesignator left, AirlineDesignator right)
    {
        return left.Equals(right);
    }
    public static bool operator !=(AirlineDesignator left, AirlineDesignator right)
    {
        return !left.Equals(right);
    }

    public static bool operator <(AirlineDesignator? left, AirlineDesignator? right)
    {
        return Comparer<AirlineDesignator>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(AirlineDesignator? left, AirlineDesignator? right)
    {
        return Comparer<AirlineDesignator>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(AirlineDesignator? left, AirlineDesignator? right)
    {
        return Comparer<AirlineDesignator>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(AirlineDesignator? left, AirlineDesignator? right)
    {
        return Comparer<AirlineDesignator>.Default.Compare(left, right) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsAllowedSymbol(char c)
    {
        return c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
    }
}