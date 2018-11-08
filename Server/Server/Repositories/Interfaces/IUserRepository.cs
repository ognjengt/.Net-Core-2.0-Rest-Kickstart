using MongoDB.Bson;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Returns a user with id provided
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        Task<Response<User>> GetUserById(ObjectId id);

        /// <summary>
        /// Returns a user with email provided
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User</returns>
        Task<Response<User>> GetUserByEmail(string email);

        /// <summary>
        /// Saves the user in the database collection
        /// </summary>
        /// <param name="user">User to save</param>
        /// <returns>True or false depending whether the action completed sucessfully</returns>
        Task<Response<User>> SaveUser(User user);

        /// <summary>
        /// Deletes the specific user from the database
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>True or false depending whether the action completed sucessfully</returns>
        Task<Response<bool>> DeleteUserById(ObjectId id);

        /// <summary>
        /// Updates the user in the database, with the new provided data
        /// </summary>
        /// <param name="user">User data</param>
        /// <returns>Updated user</returns>
        Task<Response<User>> UpdateUser(User user);
    }
}
