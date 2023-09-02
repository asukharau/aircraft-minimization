using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace AircraftMinimization;

public sealed partial class LocationIdentifier
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal const int Length = 3;

    public static LocationIdentifier Parse(string? str)
    {
        return Parse(str, CultureInfo.CurrentCulture);
    }

    public static LocationIdentifier Parse(string? str, IFormatProvider? provider)
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
        return new LocationIdentifier(code);
    }

    public static bool TryParse(string? str, out LocationIdentifier? result)
    {
        return TryParse(str, CultureInfo.CurrentCulture, out result);
    }

    public static bool TryParse(string? str, IFormatProvider? provider, out LocationIdentifier result)
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

        result = new LocationIdentifier(code);
        return true;
    }

    public static bool operator ==(LocationIdentifier? left, LocationIdentifier? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(LocationIdentifier? left, LocationIdentifier? right)
    {
        return !Equals(left, right);
    }

    public static bool operator <(LocationIdentifier? left, LocationIdentifier? right)
    {
        return Comparer<LocationIdentifier>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(LocationIdentifier? left, LocationIdentifier? right)
    {
        return Comparer<LocationIdentifier>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(LocationIdentifier? left, LocationIdentifier? right)
    {
        return Comparer<LocationIdentifier>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(LocationIdentifier? left, LocationIdentifier? right)
    {
        return Comparer<LocationIdentifier>.Default.Compare(left, right) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsAllowedSymbol(char c)
    {
        return c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
    }
}