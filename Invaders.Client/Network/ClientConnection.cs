using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Invaders.Shared.Network;

namespace Invaders.Client.Network
{
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

        public async Task Send(Packet packet)
        {
            var json = PacketSerializer.Serialize(packet);
            var data = Encoding.UTF8.GetBytes(json + "\n");
            await _stream.WriteAsync(data);
        }

        public async Task<Packet> Receive()
        {
            var reader = new StreamReader(_stream!, Encoding.UTF8);
            var json = await reader.ReadLineAsync();
            return PacketSerializer.Deserialize(json!);
        }
    }
}
