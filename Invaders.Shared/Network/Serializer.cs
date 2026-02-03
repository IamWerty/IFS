using System.Text;
using System.Text.Json;

namespace Invaders.Shared.Network;

public static class PacketSerializer
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static byte[] Serialize(Packet packet)
    {
        string json = JsonSerializer.Serialize(packet, _options);
        return Encoding.UTF8.GetBytes(json + "\n"); // \n — маркер кінця пакета
    }

    public static Packet Deserialize(string json)
    {
        return JsonSerializer.Deserialize<Packet>(json, _options)!;
    }
    public static T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json)!;
    }
}
