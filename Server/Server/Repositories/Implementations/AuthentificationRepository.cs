using Server.Models;
using Server.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repositories.Implementations
{
    public class AuthentificationRepository : IAuthentificationRepository
    {
        public string CreateToken(User user)
        {
            throw new NotImplementedException();
        }

        public Task<string> Register(User user)
        {
            throw new NotImplementedException();
        }

        public Task<string> SignIn(User user)
        {
            throw new NotImplementedException();
        }
    }
}
