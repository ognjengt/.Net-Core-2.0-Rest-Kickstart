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

        public Task<Response<User>> GetUserById(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<User>> GetUserByEmail(string email)
        {
            return null;
        }

        public Task<Response<User>> SaveUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> DeleteUserById(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<User>> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
