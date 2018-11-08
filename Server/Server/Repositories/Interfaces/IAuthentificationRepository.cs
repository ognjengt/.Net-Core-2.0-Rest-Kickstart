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
        /// <param name="user">User containing email and password</param>
        /// <returns>AuthentificationResult containing Json Web Token</returns>
        Task<Response<bool>> SignIn(User user);

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>AuthentificationResult containing message if the registration was sucessfull</returns>
        Task<Response<bool>> Register(User user);

        /// <summary>
        /// Creates a Json Web Token
        /// </summary>
        /// <param name="user">User containing information which needs to be stored</param>
        /// <returns>Json Web Token</returns>
        Response<string> CreateToken(User user);
    }
}
