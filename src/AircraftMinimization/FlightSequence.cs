using System.Collections;

namespace AircraftMinimization;

public sealed class FlightSequence : ICollection<Flight>, IReadOnlyCollection<Flight>
{
    private readonly IList<Flight> _flights;

    public int Count => _flights.Count;

    public Flight FirstFlight
    {
        get
        {
            if (_flights.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return _flights[0];
        }
    }

    public Flight LastFlight
    {
        get
        {
            if (_flights.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return _flights[^1];
        }
    }

    public FlightSequence(IList<Flight> flights)
    {
        _flights = flights;
    }

    public FlightSequence(IEnumerable<Flight> flights)
    {
        if (flights is not IList<Flight> list)
        {
            list = flights.ToArray();
        }

        _flights = list;
    }

    public IEnumerator<Flight> GetEnumerator()
    {
        return _flights.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    bool ICollection<Flight>.IsReadOnly => true;

    void ICollection<Flight>.Add(Flight item)
    {
        throw new NotSupportedException();
    }

    void ICollection<Flight>.Clear()
    {
        throw new NotSupportedException();
    }

    bool ICollection<Flight>.Contains(Flight item)
    {
        return _flights.Contains(item);
    }

    void ICollection<Flight>.CopyTo(Flight[] array, int arrayIndex)
    {
        throw new NotSupportedException();
    }

    bool ICollection<Flight>.Remove(Flight item)
    {
        throw new NotSupportedException();
    }
}