using System.Text.Json;
using System.Text.Json.Serialization;

namespace AircraftMinimization;

internal sealed class JsonAircraftRotationLayoverConverter : JsonConverter<AircraftRotationLayover>
{
    public override AircraftRotationLayover Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return AircraftRotationLayover.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, AircraftRotationLayover value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
