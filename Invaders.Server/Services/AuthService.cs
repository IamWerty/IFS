using Invaders.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invaders.Server.Services
{
    public class AuthService
    {
        private int _userCounter = 1;

        public User Login(string nick, string pc)
        {
            return new User
            {
                Nickname = nick,
                ComputerName = pc,
                ID = GenerateID(),
                Role = "Client"
            };
        }

        private string GenerateID()
        {
            int num = _userCounter++;
            return $"AAAA-{num:D4}";
        }
    }
}
