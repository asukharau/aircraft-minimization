namespace AircraftMinimization;

public sealed partial class FlightSequenceOptimizer
{
    public sealed partial class FlightIndex
    {
        public static FlightIndex Create(IEnumerable<Flight> flights)
        {
            var entries = new List<FlightIndexEntry>();

            var groups = flights
                .OrderBy(
                    flight => flight.Departure.Timestamp
                )
                .GroupBy(
                    flight => (flight.Id.FlightDesignator, flight.Id.OperationalSuffix, flight.NextFlightConnection)
                );

            foreach (var @group in groups) 
            {
                var (flightDesignator, operationalSuffix, nextFlightConnection) = @group.Key;

                entries.Add(
                    FlightIndexEntry.Create(flightDesignator, operationalSuffix, nextFlightConnection, @group)
                );
            }

            return new FlightIndex(entries);
        }
    }
}