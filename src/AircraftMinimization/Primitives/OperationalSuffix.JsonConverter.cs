using System.Text.Json;
using System.Text.Json.Serialization;

namespace AircraftMinimization;

internal sealed class JsonOperationalSuffixConverter : JsonConverter<OperationalSuffix>
{
    public override OperationalSuffix Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return OperationalSuffix.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, OperationalSuffix value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}