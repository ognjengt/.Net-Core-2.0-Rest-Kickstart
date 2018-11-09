using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Server.Helpers;
using Server.Models;
using Server.Repositories.Implementations;
using Server.Repositories.Interfaces;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserRepository _userRepository = new UserRepository();

        [HttpGet("{id}")]
        [Authorize]
        public async Task<Response<User>> GetUserById([FromRoute]string id)
        {
            Response<User> response = new Response<User>();
            User user = await _userRepository.GetUserById(ObjectId.Parse(id));
            // Remove password from the response for security reasons
            user.Password = "";

            if (user != null) return response.Success("Success", user);
            else return response.Failed("User does not exist.", null);
        }

        [HttpGet("GetUserByEmail")]
        [Authorize]
        public async Task<Response<User>> GetUserByEmail(string email)
        {
            Response<User> response = new Response<User>();
            User user = await _userRepository.GetUserByEmail(email);
            user.Password = "";

            if (user != null) return response.Success("Success", user);
            else return response.Failed("User does not exist.", null);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<Response<bool>> DeleteUserById([FromRoute]string id)
        {
            Response<bool> response = new Response<bool>();

            // Get user email from the token, so we can be sure that the user deleting the entry is himself
            // This is a protection so the one user can't tamper or delete other users data with his token provided
            // If your application has role policy, you can create Authorization middleware, so that only admins can delete users
            // Then you can put [Authorize(Policy = "AdminOnly")] to this endpoint
            string requesterEmail = TokenHandler.GetClaimFromToken("Email", Request.Headers["Authorization"].ToString().Split(' ')[1]);

            User checkingUser = await _userRepository.GetUserById(ObjectId.Parse(id));
            if (checkingUser == null)
                return response.Failed("User does not exist.", false);

            if (requesterEmail != checkingUser.Email)
                return response.Failed("Unauthorized", false);

            bool result = await _userRepository.DeleteUserById(ObjectId.Parse(id));

            if (result) return response.Success("Success", result);
            else return response.Failed("User does not exist.", result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<Response<User>> UpdateUser([FromRoute]string id, [FromBody]User newUserData)
        {
            Response<User> response = new Response<User>();

            // Get user email from the token, so we can be sure that the user updating the entry is himself
            // This is a protection so the one user can't tamper or update other users data with his token provided
            // If your application has role policy, you can create Authorization middleware, so that only admins can update users
            // Then you can put [Authorize(Policy = "AdminOnly")] to this endpoint
            string requesterEmail = TokenHandler.GetClaimFromToken("Email", Request.Headers["Authorization"].ToString().Split(' ')[1]);

            User checkingUser = await _userRepository.GetUserById(ObjectId.Parse(id));
            if (checkingUser == null)
                return response.Failed("User does not exist.", null);

            if (requesterEmail != checkingUser.Email)
                return response.Failed("Unauthorized", null);

            newUserData.Id = ObjectId.Parse(id);
            User updatedUser = await _userRepository.UpdateUser(newUserData);

            if (updatedUser != null) return response.Success("Success", updatedUser);
            else return response.Failed("User does not exist.", null);
        }
    }
}