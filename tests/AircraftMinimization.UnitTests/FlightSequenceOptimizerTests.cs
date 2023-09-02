using System.Text.Json;

namespace AircraftMinimization.UnitTests;

public class FlightSequenceOptimizerTests
{
    [Fact]
    public void ShouldReturnEmpty()
    {
        var sequences = FlightSequenceOptimizer.Optimize(Enumerable.Empty<Flight>());
        sequences.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void ShouldReturnSequences(Flight[] flights, FlightIdentifier[][] expected)
    {
        var actual = FlightSequenceOptimizer
            .Optimize(flights)
            .Select(
                sequence => sequence.Select(flight => flight.Id).ToArray()
            )
            .ToArray();

        actual.Should().HaveSameCount(expected);

        foreach (var actualSequence in actual)
        {
            expected.Should().ContainSingle(sequence => sequence.SequenceEqual(actualSequence));
        }
    }

    public static IEnumerable<object[]> GetTestCases()
    {
        {
            var (flights, flightSequences) = GetTestData("TestData/TestCase_01.json");
            yield return new object[] {
                flights,
                flightSequences
            };
        }

        {
            var (flights, flightSequences) = GetTestData("TestData/TestCase_02.json");
            yield return new object[] {
                flights,
                flightSequences
            };
        }
    }

    private static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = null        
    };

    private static (Flight[] Flights, FlightIdentifier[][] FlightSequences) GetTestData(string fileName)
    {
        using var stream = File.OpenRead(fileName);
        using var json = JsonDocument.Parse(stream);

        var flights = json.RootElement.GetProperty("flights").Deserialize<Flight[]>(jsonSerializerOptions) ?? throw new InvalidOperationException("Invalid test data. Flights are null");
        var sequences = json.RootElement.GetProperty("sequences").Deserialize<FlightIdentifier[][]>(jsonSerializerOptions) ?? throw new InvalidOperationException("Invalid test data. Sequences are null");

        return (flights, sequences);
    }
}