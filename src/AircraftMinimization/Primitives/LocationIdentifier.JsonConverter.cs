using System.Text.Json;
using System.Text.Json.Serialization;

namespace AircraftMinimization;

internal sealed class JsonLocationIdentifierConverter : JsonConverter<LocationIdentifier>
{
    public override LocationIdentifier? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return LocationIdentifier.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, LocationIdentifier value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
