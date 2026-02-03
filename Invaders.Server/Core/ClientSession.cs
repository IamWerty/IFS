using Invaders.Server.Services;
using Invaders.Shared.Enums;
using Invaders.Shared.Models;
using Invaders.Shared.Network;
using System.Net.Sockets;
using System.Text;

namespace Invaders.Server.Core;

public class ClientSession
{
    private readonly TcpClient _client;
    private static AuthService _auth = new AuthService();

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

                if (packet.Type == PacketType.Login)
                {
                    var parts = packet.Data.Split(';');
                    string nick = parts[0].Split('=')[1];
                    string pc = parts[1].Split('=')[1];

                    var user = _auth.Login(nick, pc);

                    var response = new Packet
                    {
                        Type = PacketType.Success,
                        Data = $"{user.Nickname}|{user.ID}|{user.Role}"
                    };

                    byte[] bytes = PacketSerializer.Serialize(response);
                    await stream.WriteAsync(bytes);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
        }
    }
}
