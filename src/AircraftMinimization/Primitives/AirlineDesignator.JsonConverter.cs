using System.Text.Json;
using System.Text.Json.Serialization;

namespace AircraftMinimization;

internal sealed class JsonAirlineDesignatorConverter : JsonConverter<AirlineDesignator>
{
    public override AirlineDesignator? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return AirlineDesignator.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, AirlineDesignator value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}