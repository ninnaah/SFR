using System.Text.Json;
using Confluent.Kafka;

namespace SFR.Serialisation;

public class JsonDeserializer<T> : IDeserializer<T>
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        var value = JsonSerializer.Deserialize<T>(data);
        return value;
    }
}