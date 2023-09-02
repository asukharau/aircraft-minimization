namespace AircraftMinimization;

public sealed partial class FlightSequenceOptimizer
{
    private sealed partial class FlightIndexEntry
    {
        public static FlightIndexEntry Create(FlightDesignator flightDesignator, OperationalSuffix? operationalSuffix, FlightConnection? nextFlightConnection, IEnumerable<Flight> orderedEnumerable)
        {
            var flights = new List<Flight>();
            var departureTimestamps = new List<DateTime>();
            var arrivalTimestamps = new List<DateTime>();

            foreach (var flight in orderedEnumerable)
            {
                flights.Add(flight);
                departureTimestamps.Add(flight.Departure.Timestamp);
                arrivalTimestamps.Add(flight.Arrival.Timestamp);
            }

            return new FlightIndexEntry
            {
                FlightDesignator = flightDesignator,
                OperationalSuffix = operationalSuffix,
                NextFlightConnection = nextFlightConnection,
                Flights = flights.ToArray(),
                DepartureTimestamps = departureTimestamps.ToArray(),
                ArrivalTimestamps = arrivalTimestamps.ToArray()
            };
        }
    }
}