using Invaders.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invaders.Shared.Network
{
    public class Packet
    {
        public PacketType Type { get; set; } = PacketType.Error;
        public string Data { get; set; } = string.Empty;
    }

}
