namespace AircraftMinimization;

public sealed partial class FlightSequenceOptimizer
{
    private sealed partial class FlightIndexEntry
    {
        public FlightDesignator FlightDesignator { get; init; }
        
        public OperationalSuffix? OperationalSuffix { get; init; }

        public FlightConnection? NextFlightConnection { get; init; }

        public Flight[] Flights { get; init; }

        public DateTime[] DepartureTimestamps { get; init; }

        public DateTime[] ArrivalTimestamps { get; init; }

        public FlightIndexEntry()
        {
            FlightDesignator = null!;

            Flights = Array.Empty<Flight>();
            DepartureTimestamps = Array.Empty<DateTime>();
            ArrivalTimestamps = Array.Empty<DateTime>();
        }
    }
}