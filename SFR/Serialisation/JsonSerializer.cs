using System.Text;
using System.Text.Json;
using Confluent.Kafka;

namespace SFR.Serialisation;

public class JsonSerializer<T> : ISerializer<T>
{
    public byte[] Serialize(T data, SerializationContext context)
    {
        if (data == null && typeof(T) != typeof(Null)) { return null; }
        var jsonString = JsonSerializer.Serialize(data);
        var bytes = Encoding.ASCII.GetBytes(jsonString);
        return bytes;
    }
}