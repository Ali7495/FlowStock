using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Usermanagement.Application;

namespace Usermanagement.Infrastructure.Services.Security
{
    public class PasswordService : IPasswordService
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}