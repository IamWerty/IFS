using Invaders.Shared.Enums;
using Invaders.Shared.Network;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Invaders.Client.Network;

public class ClientConnection
{
    private TcpClient? _client;
    private NetworkStream? _stream;

    public async Task ConnectAsync(string ip, int port)
    {
        _client = new TcpClient();
        await _client.ConnectAsync(ip, port);
        _stream = _client.GetStream();
    }

    public async Task SendLoginPacket()
    {
        var packet = new Packet
        {
            Type = PacketType.Login,
            Data = "Nick=TestUser;PC=MyComputer"
        };

        byte[] bytes = PacketSerializer.Serialize(packet);
        await _stream.WriteAsync(bytes);

        // Читаємо відповідь
        var reader = new StreamReader(_stream, Encoding.UTF8);
        string json = await reader.ReadLineAsync();

        var response = PacketSerializer.Deserialize(json);
        Console.WriteLine($"[SERVER RESPONSE] {response.Data}");
    }
}
