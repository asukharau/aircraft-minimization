using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace AircraftMinimization;

public readonly partial struct OperationalSuffix
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal const int Length = 1;

    public static OperationalSuffix Create(char c)
    {
        if (IsAllowedSymbol(c) is false)
        {
            throw new ArgumentException($"The character '{c}' is not allowed.");
        }

        var normalized = char.ToUpperInvariant(c);

        return new OperationalSuffix(normalized);
    }

    public static OperationalSuffix Parse(string? str)
    {
        return Parse(str, CultureInfo.CurrentCulture);
    }

    public static OperationalSuffix Parse(string? str, IFormatProvider? provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(str);

        if (str.Length != Length)
        {
            throw new FormatException($"The string '{str}' does not have the correct length.");
        }

        return Create(str[0]);
    }

    public static bool TryParse(string? str, out OperationalSuffix result)
    {
        return TryParse(str, CultureInfo.CurrentCulture, out result);
    }

    public static bool TryParse(string? str, IFormatProvider? provider, out OperationalSuffix result)
    {
        if (str?.Length != Length)
        {
            result = default;
            return false;
        }

        var c = str[0];
        if (IsAllowedSymbol(c) is false)
        {
            result = default;
            return false;
        }

        var normalized = char.ToUpperInvariant(c);

        result = new OperationalSuffix(normalized);
        return true;
    }

    public static bool operator ==(OperationalSuffix left, OperationalSuffix right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(OperationalSuffix left, OperationalSuffix right)
    {
        return !left.Equals(right);
    }

    public static bool operator <(OperationalSuffix left, OperationalSuffix right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(OperationalSuffix left, OperationalSuffix right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(OperationalSuffix left, OperationalSuffix right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(OperationalSuffix left, OperationalSuffix right)
    {
        return left.CompareTo(right) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsAllowedSymbol(char c)
    {
        return c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
    }
}