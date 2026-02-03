using Invaders.Shared.Enums;
using Invaders.Shared.Network;
using System.Net.Sockets;
using System.Text;

namespace Invaders.Server.Core;

public class ClientSession
{
    private readonly TcpClient _client;

    public ClientSession(TcpClient client)
    {
        _client = client;
    }

    public async Task HandleAsync()
    {
        try
        {
            using var stream = _client.GetStream();
            using var reader = new StreamReader(stream, Encoding.UTF8);
            using var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            while (true)
            {
                string json = await reader.ReadLineAsync();
                if (json == null) break;

                var packet = PacketSerializer.Deserialize(json);
                Console.WriteLine($"[PACKET] {packet.Type} | {packet.Data}");

                // Тестова відповідь
                var response = new Packet
                {
                    Type = PacketType.Success,
                    Data = "Packet received!"
                };

                byte[] bytes = PacketSerializer.Serialize(response);
                await stream.WriteAsync(bytes);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
        }
    }
}
