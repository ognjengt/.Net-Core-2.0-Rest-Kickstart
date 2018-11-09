using MongoDB.Bson;
using MongoDB.Driver;
using Server.Helpers;
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
        MongoDriver _mongoDriver = MongoDriver.MongoDriverInstance;

        public async Task<User> GetUserById(ObjectId id, bool hidePassword)
        {
            var user = await _mongoDriver.UsersCollection.Find(u => u.Id == id).SingleOrDefaultAsync();
            // Remove password from the response for security reasons
            if (hidePassword && user != null)
            {
                user.Password = "";
            }
            return user;
        }

        public async Task<User> GetUserByEmail(string email, bool hidePassword)
        {
            var user = await _mongoDriver.UsersCollection.Find(u => u.Email == email).SingleOrDefaultAsync();
            // Remove password from the response for security reasons
            if (hidePassword && user != null)
            {
                user.Password = "";
            }
            return user;
        }

        public async Task<User> SaveUser(User user)
        {
            try
            {
                await _mongoDriver.UsersCollection.InsertOneAsync(user);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteUserById(ObjectId id)
        {
            try
            {
                var deleteResult = await _mongoDriver.UsersCollection.DeleteOneAsync(u => u.Id == id);
                if (deleteResult.DeletedCount > 0) return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
                var update = Builders<User>.Update.Set("FullName", user.FullName);
                // In this case we are updating the FullName property of the user
                // You can chain the .Set() method and update multiple properties at once
                var updateResult = await _mongoDriver.UsersCollection.UpdateOneAsync(filter, update);

                user.Password = "";

                if (updateResult.ModifiedCount > 0) return user;
                else return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
