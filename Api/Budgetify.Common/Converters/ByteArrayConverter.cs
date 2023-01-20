namespace Budgetify.Common.Converters;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ByteArrayConverter : JsonConverter<byte[]>
{
    public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        short[]? sByteArray = JsonSerializer.Deserialize<short[]>(ref reader);

        if (sByteArray is null)
        {
            throw new JsonException("Invalid JSON");
        }

        byte[] byteArray = new byte[sByteArray.Length];

        for (int i = 0; i < sByteArray.Length; i++)
        {
            byteArray[i] = (byte)sByteArray[i];
        }

        return byteArray;
    }

    public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (byte v in value)
        {
            writer.WriteNumberValue(v);
        }

        writer.WriteEndArray();
    }
}
