using Microsoft.IdentityModel.Tokens;
using Server.Helpers;
using Server.Models;
using Server.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repositories.Implementations
{
    public class AuthentificationRepository : IAuthentificationRepository
    {
        MongoDriver _mongoDriver;
        IUserRepository _userRepository = new UserRepository();

        public AuthentificationRepository(MongoDriver driverInstance)
        {
            _mongoDriver = driverInstance;
        }

        public Response<string> CreateToken(User user)
        {
            // Create claims for the payload body
            var claimsData = new List<Claim>();
            claimsData.Add(new Claim("Email", user.Email));
            claimsData.Add(new Claim("FullName", user.FullName));

            // The recommended way is to store your secrets in some file that is not accessible by anyone.
            // For more information, look up secrets.json in .Net Core projects.
            string secret = "your_secret_here";

            // Create key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            // Create token
            var token = new JwtSecurityToken(
                issuer: "localhost",
                expires: DateTime.Now.AddYears(1),
                claims: claimsData,
                signingCredentials: signInCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Response<string>().Success("Token successfully created.", tokenString.ToString());
        }

        public async Task<Response<bool>> Register(User user)
        {
            Response<bool> response = new Response<bool>();

            // Check if user with this email already exists
            var existingUser = await _userRepository.GetUserByEmail(user.Email);

            if (existingUser != null)
                return response.Failed("User already exists.", false);

            // Populate other properties
            user.SignUpDate = DateTime.Now;
            user.Password = PasswordCrypter.CryptPassword(user.Password);

            try
            {
                await _mongoDriver.UsersCollection.InsertOneAsync(user);
                return response.Success("User successfully created.", true);
            }
            catch (Exception)
            {
                return response.Failed("Could not create user.", false);
            }
        }

        public Task<Response<bool>> SignIn(User user)
        {
            throw new NotImplementedException();
        }
    }
}
