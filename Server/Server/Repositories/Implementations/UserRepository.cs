using MongoDB.Bson;
using Server.Models;
using Server.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {

        public Task<User> GetUser(string email)
        {
            throw new NotImplementedException();
        }
        
        public Task<bool> SaveUser(User user)
        {
            throw new NotImplementedException();
        }
        
        public Task<bool> DeleteUser(ObjectId id)
        {
            throw new NotImplementedException();
        }
    }
}
