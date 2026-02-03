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

            await writer.WriteLineAsync("CONNECTED_TO_INVADERS_SERVER");

            while (true)
            {
                string request = await reader.ReadLineAsync();
                if (request == null) break;

                Console.WriteLine($"[REQUEST] {request}");

                await writer.WriteLineAsync($"ECHO: {request}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
        }
        finally
        {
            _client.Close();
            Console.WriteLine("[SERVER] Client disconnected");
        }
    }
}
