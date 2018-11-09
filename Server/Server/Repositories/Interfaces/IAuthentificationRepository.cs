using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Repositories.Interfaces
{
    public interface IAuthentificationRepository
    {
        /// <summary>
        /// Signs the user in
        /// </summary>
        /// <param name="user">User with the Email and Password</param>
        /// <returns>Response object where the data property contains Json Web Token</returns>
        Task<Response<string>> SignIn(User user);

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="user">User containing FullName, Email and Password</param>
        /// <returns>Response object</returns>
        Task<Response<bool>> Register(User user);

        /// <summary>
        /// Creates a Json Web Token
        /// </summary>
        /// <param name="user">User containing information which needs to be stored</param>
        /// <returns>Json Web Token</returns>
        string CreateToken(User user);
    }
}
