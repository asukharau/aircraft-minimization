using System.Diagnostics;

namespace AircraftMinimization;

public sealed partial class FlightSequenceOptimizer
{
    [DebuggerDisplay("Count = {FlightCount}")]
    public sealed partial class FlightIndex
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IList<FlightIndexEntry> _entries;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IDictionary<FlightIdentifier, FlightIndexEntry> _entryByFlightIdIndex;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IDictionary<(FlightDesignator, OperationalSuffix?), List<FlightIndexEntry>> _entriesByEntryKeyIndex;

        public int FlightCount => _entries.Sum(entry => entry.Flights.Length);

        private FlightIndex(IList<FlightIndexEntry> entries)
        {
            _entries = entries;

            _entryByFlightIdIndex = new Dictionary<FlightIdentifier, FlightIndexEntry>(FlightCount);
            _entriesByEntryKeyIndex = new Dictionary<(FlightDesignator, OperationalSuffix?), List<FlightIndexEntry>>(FlightCount);

            foreach (var entry in entries)
            {
                foreach (var flight in entry.Flights)
                {
                    _entryByFlightIdIndex.Add(flight.Id, entry);
                }

                var entryKey = (entry.FlightDesignator, entry.OperationalSuffix);

                if (!_entriesByEntryKeyIndex.TryGetValue(entryKey, out var list))
                {
                    list = new List<FlightIndexEntry> 
                    {
                        entry
                    };

                    _entriesByEntryKeyIndex.Add(entryKey, list);
                }
                else
                {
                    list.Add(entry);
                }
            }            
        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return _entries.SelectMany(entry => entry.Flights);
        }

        public IEnumerable<Flight> GetNextFlights(Flight flight)
        {
            if (!_entryByFlightIdIndex.TryGetValue(flight.Id, out var entry))
            {
                yield break;
            }

            var nextFlightConnection = entry.NextFlightConnection;
            if (nextFlightConnection is null)
            {
                yield break;
            }

            var nextEntryKey = (nextFlightConnection.FlightDesignator, nextFlightConnection.OperationalSuffix);
            if (!_entriesByEntryKeyIndex.TryGetValue(nextEntryKey, out var nextEntries))
            {
                yield break;
            }

            var (minimumLayoverDuration, maximumLayoverDuration) = nextFlightConnection.AircraftRotationLayover ?? AircraftRotationLayover.Zero;

            var minimumNextDepartureTimestamp = flight.Departure.Timestamp + minimumLayoverDuration;
            var maximumNextDepartureTimestamp = flight.Arrival.Timestamp + maximumLayoverDuration;            

            foreach (var nextEntry in nextEntries)
            {
                var lowerBoundIndex = Array.BinarySearch(nextEntry.DepartureTimestamps, minimumNextDepartureTimestamp);
                if (lowerBoundIndex < 0)
                {
                    lowerBoundIndex = ~lowerBoundIndex;
                }
                else
                {
                    lowerBoundIndex++;
                }

                var upperBoundIndex = Array.BinarySearch(nextEntry.DepartureTimestamps, maximumNextDepartureTimestamp);
                if (upperBoundIndex < 0)
                {
                    upperBoundIndex = ~upperBoundIndex;
                }
                else
                {
                    upperBoundIndex++;
                }

                for (var i = lowerBoundIndex; i < upperBoundIndex; ++i)
                {
                    yield return nextEntry.Flights[i];
                }
            }
        }
    }
}