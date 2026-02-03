using Invaders.Shared.Enums;
using Invaders.Shared.Models;
using Invaders.Shared.Network;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invaders.Client.Network
{
    public class ClientFileService
    {
        private readonly ClientConnection _connection;

        public ClientFileService(ClientConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<FileEntry>> RequestFileList(string path = "/")
        {
            var packet = new Packet
            {
                Type = PacketType.FileListRequest,
                Data = path
            };

            await _connection.Send(packet);
            var response = await _connection.Receive();

            if (response.Type != PacketType.FileListResponse)
                throw new Exception(response.Data);

            return PacketSerializer.Deserialize<List<FileEntry>>(response.Data);
        }
    }
}
