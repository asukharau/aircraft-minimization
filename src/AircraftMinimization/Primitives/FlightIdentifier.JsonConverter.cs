using System.Text.Json;
using System.Text.Json.Serialization;

namespace AircraftMinimization;

internal sealed class JsonFlightIdentifierConverter : JsonConverter<FlightIdentifier>
{
    public override FlightIdentifier? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return FlightIdentifier.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, FlightIdentifier value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}