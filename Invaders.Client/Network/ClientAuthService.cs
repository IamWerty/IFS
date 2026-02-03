using Invaders.Shared.Enums;
using Invaders.Shared.Network;
using System;
using System.Threading.Tasks;

namespace Invaders.Client.Network
{
    public class ClientAuthService
    {
        private readonly ClientConnection _connection;

        public ClientAuthService(ClientConnection connection)
        {
            _connection = connection;
        }

        public async Task<LoginResult> Login(string nick, string pcName)
        {
            var packet = new Packet
            {
                Type = PacketType.Login,
                Data = $"Nick={nick};PC={pcName}"
            };

            await _connection.Send(packet);
            var response = await _connection.Receive();

            if (response.Type == PacketType.Success)
            {
                var parts = response.Data.Split('|');
                return new LoginResult
                {
                    Nick = parts[0],
                    ID = parts[1],
                    Role = parts[2]
                };
            }

            throw new Exception(response.Data);
        }
    }

    public class LoginResult
    {
        public string Nick { get; set; } = "";
        public string ID { get; set; } = "";
        public string Role { get; set; } = "";
    }
}
