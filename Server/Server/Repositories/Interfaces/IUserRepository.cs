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
        /// Returns a user with the provided id
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <param name="hidePassword">Boolean that specifies whether to remove the users password from the response</param>
        /// <returns></returns>
        Task<User> GetUserById(ObjectId id, bool hidePassword);

        /// <summary>
        /// Returns a user with the provided email
        /// </summary>
        /// <param name="email">Users email</param>
        /// <param name="hidePassword">Boolean that specifies whether to remove the users password from the response</param>
        /// <returns></returns>
        Task<User> GetUserByEmail(string email, bool hidePassword);

        /// <summary>
        /// Saves the user in the database collection
        /// </summary>
        /// <param name="user">User to save</param>
        /// <returns>Saved user object</returns>
        Task<User> SaveUser(User user);

        /// <summary>
        /// Deletes the specific user from the database
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>True or false depending whether the action completed sucessfully</returns>
        Task<bool> DeleteUserById(ObjectId id);

        /// <summary>
        /// Updates the user in the database, with the new provided data
        /// </summary>
        /// <param name="user">User data</param>
        /// <returns>Updated user</returns>
        Task<User> UpdateUser(User user);
    }
}
