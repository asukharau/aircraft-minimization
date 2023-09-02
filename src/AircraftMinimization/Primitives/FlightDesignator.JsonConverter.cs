using System.Text.Json;
using System.Text.Json.Serialization;

namespace AircraftMinimization;

internal sealed class JsonFlightDesignatorConverter : JsonConverter<FlightDesignator>
{
    public override FlightDesignator? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return FlightDesignator.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, FlightDesignator value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}