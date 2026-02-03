using System;
using System.Collections.Generic;
using System.Text;

namespace Invaders.Shared.Models
{
    public class User
    {
        public string Nickname { get; set; } = "";
        public string ComputerName { get; set; } = "";
        public string ID { get; set; } = "";
        public string Role { get; set; } = "Client";
    }
}
