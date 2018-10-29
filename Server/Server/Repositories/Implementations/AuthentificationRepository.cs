using Microsoft.IdentityModel.Tokens;
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
        public string CreateToken(User user)
        {
            var claimsData = new List<Claim>();
            claimsData.Add(new Claim("Email", user.Email));
            claimsData.Add(new Claim("FullName", user.FullName));

            // The recommended way is to store your secrets in some file that is not accessible by anyone.
            // For more information, look up secrets.json in .Net Core projects.
            string secret = "your_secret_here";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: "localhost",
                expires: DateTime.Now.AddYears(1),
                claims: claimsData,
                signingCredentials: signInCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString.ToString();
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
